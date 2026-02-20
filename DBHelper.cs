using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public static class RqliteClient
{
    // ======================
    // CONFIG
    // ======================
    public static HttpClient httpClient;
    public static string DefaultEndPoint;

    // ======================
    // TABLE NAMES
    // ======================
    public const string TableJobs = "PBJobs";
    public const string TablePallets = "Pallets";
    public const string TablePalletWorkOrders = "PalletWorkOrders";

    // ======================
    // RESULT MODELS
    // ======================
    public class RqliteWriteResult
    {
        public bool Success { get; set; }
        public int RowsAffected { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class RqliteResult
    {
        public bool Success { get; set; }
        public List<Dictionary<string, object>> Records { get; set; }
            = new List<Dictionary<string, object>>();
    }

    // ======================
    // CORE HELPERS
    // ======================
    private static string FormatValue(object value)
    {
        if (value == null || value == DBNull.Value) return "NULL";
        if (value is string s) return "'" + s.Replace("'", "''") + "'";
        if (value is bool b) return b ? "1" : "0";
        if (value is DateTime dt)
            return "'" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "'";
        return value.ToString();
    }

    private static async Task<RqliteWriteResult> ExecuteAsync(string sql)
    {
        var json = JsonConvert.SerializeObject(new[] { sql });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(
            DefaultEndPoint + "/db/execute?transaction=true",
            content
        );

        var body = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return new RqliteWriteResult
            {
                Success = false,
                ErrorMessage = body,
                RowsAffected = 0
            };
        }

        var result = JsonConvert.DeserializeObject<WriteResponse>(body);

        return new RqliteWriteResult
        {
            Success = true,
            RowsAffected = result?.results?.Sum(r => r.rows_affected) ?? 0
        };
    }

    public static Task<RqliteWriteResult> CreateTableAsync(
        string table,
        Dictionary<string, string> columns)
    {
        var cols = string.Join(", ",
            columns.Select(c => $"\"{c.Key}\" {c.Value}")
        );

        return ExecuteAsync(
            $"CREATE TABLE IF NOT EXISTS \"{table}\" ({cols})"
        );
    }

    public static Task<RqliteWriteResult> CreateIndexAsync(
        string indexName,
        string table,
        string column)
    {
        return ExecuteAsync(
            $"CREATE INDEX IF NOT EXISTS {indexName} ON \"{table}\"(\"{column}\")"
        );
    }

    public static Task<RqliteWriteResult> InsertAsync(
        string table,
        Dictionary<string, object> data)
    {
        var cols = string.Join(", ", data.Keys.Select(k => $"\"{k}\""));
        var vals = string.Join(", ", data.Values.Select(FormatValue));

        return ExecuteAsync(
            $"INSERT INTO \"{table}\" ({cols}) VALUES ({vals})"
        );
    }

    public static async Task<RqliteResult> SelectAsync(string table, string where = "")
    {
        var sql = $"SELECT * FROM \"{table}\" {where}";
        var json = JsonConvert.SerializeObject(new[] { sql });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(
            DefaultEndPoint + "/db/query",
            content
        );

        var body = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<QueryResponse>(body);

        var rows = new List<Dictionary<string, object>>();

        if (result?.results?.Count > 0)
        {
            var r = result.results[0];

            // 🔑 rqlite returns columns but NULL values when no rows exist
            if (r.values != null)
            {
                for (int i = 0; i < r.values.Length; i++)
                {
                    var dict = new Dictionary<string, object>();
                    for (int c = 0; c < r.columns.Length; c++)
                        dict[r.columns[c]] = r.values[i][c];
                    rows.Add(dict);
                }
            }
        }


        return new RqliteResult { Success = true, Records = rows };
    }

    public static async Task UpdateJobShippedDateAsync(int jobId, DateTime shippedDate)
    {
        string sql = $@"
        UPDATE {TableJobs}
        SET ShippedDate = '{shippedDate:yyyy-MM-dd HH:mm:ss}',
            LastUpdated = CURRENT_TIMESTAMP
        WHERE JobId = {jobId}";

        await ExecuteAsync(sql);
    }
    public static async Task<List<(int JobId, DateTime? LastUpdated)>>
      LoadJobUpdateInfoAsync()
    {
        var sql = $"SELECT JobId, LastUpdated FROM {TableJobs}";
        var json = JsonConvert.SerializeObject(new[] { sql });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(
            DefaultEndPoint + "/db/query",
            content
        );

        var body = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<QueryResponse>(body);

        var list = new List<(int, DateTime?)>();

        if (result?.results?.Count > 0)
        {
            var r = result.results[0];

            if (r.values != null)
            {
                foreach (var row in r.values)
                {
                    int id = Convert.ToInt32(row[0]);

                    DateTime? lastUpdated = null;
                    if (row[1] != null)
                        lastUpdated = DateTime.Parse(row[1].ToString());

                    list.Add((id, lastUpdated));
                }
            }
        }

        return list;
    }


    public static async Task UpdateJobReadyAsync(int jobId, bool isReady)
    {
        string sql = $@"
        UPDATE {TableJobs}
        SET IsReady = {(isReady ? 1 : 0)},
            LastUpdated = CURRENT_TIMESTAMP
        WHERE JobId = {jobId}";

        await ExecuteAsync(sql);
    }


    public static async Task DeleteWorkOrdersAsync(
       IEnumerable<int> palletWorkOrderIds)
    {
        if (palletWorkOrderIds == null || !palletWorkOrderIds.Any())
            return;

        var idList = string.Join(",", palletWorkOrderIds);

        string sql = $@"
        -- update parent jobs first
        UPDATE {TableJobs}
        SET LastUpdated = CURRENT_TIMESTAMP
        WHERE JobId IN (
            SELECT DISTINCT p.JobId
            FROM {TablePallets} p
            JOIN {TablePalletWorkOrders} w
                ON p.PalletId = w.PalletId
            WHERE w.PalletWorkOrderId IN ({idList})
        );

        DELETE FROM {TablePalletWorkOrders}
        WHERE PalletWorkOrderId IN ({idList});
    ";

        await ExecuteAsync(sql);
    }




    public static async Task DeletePbJobAsync(int jobId)
    {
        string sql = $@"
        DELETE FROM {TableJobs}
        WHERE JobId = {jobId};
    ";

        await ExecuteAsync(sql);
    }

    public static async Task UpdatePalletPackingAsync(
        int palletId,
        int trayCount,
        DateTime? packedTime)
    {
        string sql = $@"
        UPDATE {TablePallets}
        SET TrayCount = {trayCount},
            PackedTime = {FormatValue(packedTime)}
        WHERE PalletId = {palletId};

        UPDATE {TableJobs}
        SET LastUpdated = CURRENT_TIMESTAMP
        WHERE JobId = (
            SELECT JobId FROM {TablePallets}
            WHERE PalletId = {palletId}
        );
    ";

        await ExecuteAsync(sql);
    }



    // ======================
    // BATCH DELETE PALLETS (New)
    // ======================
    public static async Task DeletePalletsAsync(IEnumerable<int> palletIds)
    {
        if (palletIds == null || !palletIds.Any())
            return;

        var idList = string.Join(",", palletIds);

        string sql = $@"
        -- update parent jobs first
        UPDATE {TableJobs}
        SET LastUpdated = CURRENT_TIMESTAMP
        WHERE JobId IN (
            SELECT DISTINCT JobId
            FROM {TablePallets}
            WHERE PalletId IN ({idList})
        );

        DELETE FROM {TablePalletWorkOrders}
        WHERE PalletId IN ({idList});

        DELETE FROM {TablePallets}
        WHERE PalletId IN ({idList});
    ";

        await ExecuteAsync(sql);
    }




    // ======================
    // SCHEMA CREATION
    // ======================
    public static async Task CreatePbSchemaAsync()
    {
        await ExecuteAsync("PRAGMA foreign_keys = ON;");

        await CreateTableAsync(TableJobs, new Dictionary<string, string>
{
    { "JobId", "INTEGER PRIMARY KEY AUTOINCREMENT" },
    { "JobName", "VARCHAR(100) NOT NULL" },
    { "JobNumber", "INTEGER NOT NULL" },
    { "IsReady", "INTEGER NOT NULL" },
    { "PackedDate", "DATETIME" },
    { "ShippedDate", "DATETIME" },
    { "LastUpdated", "DATETIME DEFAULT CURRENT_TIMESTAMP" } // ✅ Added
});

        await CreateTableAsync(TablePallets, new Dictionary<string, string>
{
    { "PalletId", "INTEGER PRIMARY KEY AUTOINCREMENT" },
    { "JobId", "INTEGER NOT NULL REFERENCES PBJobs(JobId) ON DELETE CASCADE" },
    { "PalletNumber", "INTEGER NOT NULL" },
    { "PackedTime", "DATETIME" },
    { "TrayCount", "INTEGER NOT NULL" }
});

        await CreateTableAsync(TablePalletWorkOrders, new Dictionary<string, string>
{
    { "PalletWorkOrderId", "INTEGER PRIMARY KEY AUTOINCREMENT" },
    { "PalletId", "INTEGER NOT NULL REFERENCES Pallets(PalletId) ON DELETE CASCADE" },
    { "WoCode", "VARCHAR(50) NOT NULL" },
    { "Quantity", "INTEGER NOT NULL" }
});


        await CreateIndexAsync("idx_pallets_jobid", TablePallets, "JobId");
        await CreateIndexAsync("idx_pwo_palletid", TablePalletWorkOrders, "PalletId");
    }

    // ======================
    // WRITE OPERATIONS
    // ======================
    public static async Task<int> InsertPbJobAsync(PbJobModel job)
    {
        var data = new Dictionary<string, object>
        {
            { "JobName", job.JobName },
            { "JobNumber", job.JobNumber },
            { "IsReady", job.IsReady },
            { "PackedDate", DBNull.Value },
            { "ShippedDate", DBNull.Value }
        };

        await InsertAsync(TableJobs, data);

        var result = await SelectAsync(
            "sqlite_sequence",
            "WHERE name = 'PBJobs'"
        );

        return result.Records.Count > 0
            ? Convert.ToInt32(result.Records[0]["seq"])
            : 0;
    }

    // ======================
    // BATCH SHIP JOBS (New)
    // ======================
    public static async Task ShipJobsAsync(IEnumerable<int> jobIds)
    {
        if (jobIds == null || !jobIds.Any())
            return;

        var idList = string.Join(",", jobIds);
        string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string sql = $@"
        UPDATE {TableJobs}
        SET IsReady = 1,
            ShippedDate = '{now}',
            LastUpdated = CURRENT_TIMESTAMP
        WHERE JobId IN ({idList})";

        await ExecuteAsync(sql);
    }


    // ======================
    // READ OPERATIONS (⭐ ADDED)
    // ======================
    public static async Task<List<PbJobModel>> LoadJobsAsync()
    {
        var result = await SelectAsync(TableJobs);
        var jobs = new List<PbJobModel>();

        foreach (var row in result.Records)
        {
            jobs.Add(new PbJobModel
            {
                JobId = Convert.ToInt32(row["JobId"]),
                JobName = row["JobName"].ToString(),
                JobNumber = Convert.ToInt32(row["JobNumber"]),
                IsReady = Convert.ToInt32(row["IsReady"]) == 1,
                PackDate = row["PackedDate"] == null ? (DateTime?)null : DateTime.Parse(row["PackedDate"].ToString()),
                ShippedDate = row["ShippedDate"] == null ? (DateTime?)null : DateTime.Parse(row["ShippedDate"].ToString()),
                LastUpdated = row["LastUpdated"] == null
    ? (DateTime?)null
    : DateTime.Parse(row["LastUpdated"].ToString()),

                Pallets = new List<Pallet>()
            });
        }

        var shippedCount = jobs.Count(j => j.ShippedDate.HasValue);

        return jobs;


    }

    public static async Task<PbJobModel> LoadSingleJobGraphAsync(int jobId)
    {
        var jobs = await LoadJobsAsync();
        var job = jobs.FirstOrDefault(j => j.JobId == jobId);

        if (job == null)
            return null;

        job.Pallets = await LoadPalletsAsync(jobId);

        foreach (var pallet in job.Pallets)
        {
            pallet.WorkOrders = await LoadWorkOrdersAsync(pallet.PalletId);
        }

        return job;
    }


    public static async Task<List<Pallet>> LoadPalletsAsync(int jobId)
    {
        var result = await SelectAsync(TablePallets, $"WHERE JobId = {jobId}");
        var pallets = new List<Pallet>();

        foreach (var row in result.Records)
        {
            pallets.Add(new Pallet
            {
                PalletId = Convert.ToInt32(row["PalletId"]),
                JobId = jobId,
                PalletNumber = Convert.ToInt32(row["PalletNumber"]),
                TrayCount = Convert.ToInt32(row["TrayCount"]),
                PackedTime = row["PackedTime"] == null ? (DateTime?)null : DateTime.Parse(row["PackedTime"].ToString()),
                WorkOrders = new List<WorkOrder>()
            });
        }

        return pallets;
    }

    public static async Task<List<WorkOrder>> LoadWorkOrdersAsync(int palletId)
    {
        var result = await SelectAsync(TablePalletWorkOrders, $"WHERE PalletId = {palletId}");
        var workOrders = new List<WorkOrder>();

        foreach (var row in result.Records)
        {
            workOrders.Add(new WorkOrder(
                row["WoCode"].ToString(),
                Convert.ToInt32(row["Quantity"])
            )
            {
                PalletId = palletId,
                PalletWorkOrderId = Convert.ToInt32(row["PalletWorkOrderId"])
            });
        }

        return workOrders;
    }

    public static async Task<List<PbJobModel>> LoadFullJobGraphAsync()
    {
        var jobs = await LoadJobsAsync();

        if (!jobs.Any())
            return jobs;

        var jobIds = string.Join(",", jobs.Select(j => j.JobId));

        var palletResult = await SelectAsync(
            TablePallets,
            $"WHERE JobId IN ({jobIds})");

        var allPallets = palletResult.Records.Select(row => new Pallet
        {
            PalletId = Convert.ToInt32(row["PalletId"]),
            JobId = Convert.ToInt32(row["JobId"]),
            PalletNumber = Convert.ToInt32(row["PalletNumber"]),
            TrayCount = Convert.ToInt32(row["TrayCount"]),
            PackedTime = row["PackedTime"] == null ? (DateTime?)null : DateTime.Parse(row["PackedTime"].ToString()),
            WorkOrders = new List<WorkOrder>()
        }).ToList();

        if (allPallets.Any())
        {
            var palletIds = string.Join(",", allPallets.Select(p => p.PalletId));

            var woResult = await SelectAsync(
                TablePalletWorkOrders,
                $"WHERE PalletId IN ({palletIds})");

            var allWorkOrders = woResult.Records.Select(row => new WorkOrder(
                row["WoCode"].ToString(),
                Convert.ToInt32(row["Quantity"]))
            {
                PalletId = Convert.ToInt32(row["PalletId"]),
                PalletWorkOrderId = Convert.ToInt32(row["PalletWorkOrderId"])
            }).ToList();

            var woLookup = allWorkOrders
                .GroupBy(w => w.PalletId)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var pallet in allPallets)
            {
                if (woLookup.ContainsKey(pallet.PalletId))
                    pallet.WorkOrders = woLookup[pallet.PalletId];
            }
        }

        var palletLookup = allPallets
            .GroupBy(p => p.JobId)
            .ToDictionary(g => g.Key, g => g.ToList());

        foreach (var job in jobs)
        {
            if (palletLookup.ContainsKey(job.JobId))
                job.Pallets = palletLookup[job.JobId];
            else
                job.Pallets = new List<Pallet>();
        }

        return jobs;
    }


    public static async Task<int> SavePalletAsync(Pallet pallet)
    {
        var sql = $@"
        INSERT INTO {TablePallets}
        (JobId, PalletNumber, PackedTime, TrayCount)
        VALUES (
            {pallet.JobId},
            {pallet.PalletNumber},
            {FormatValue(pallet.PackedTime)},
            {pallet.TrayCount}
        );

        UPDATE {TableJobs}
        SET LastUpdated = CURRENT_TIMESTAMP
        WHERE JobId = {pallet.JobId};
    ";

        await ExecuteAsync(sql);

        var result = await SelectAsync(
            "sqlite_sequence",
            "WHERE name = 'Pallets'"
        );

        return result.Records.Count > 0
            ? Convert.ToInt32(result.Records[0]["seq"])
            : 0;
    }


    public static async Task SaveWorkOrdersAsync(
      int palletId,
      List<WorkOrder> workOrders)
    {
        if (workOrders == null || !workOrders.Any())
            return;

        var sqlBuilder = new StringBuilder();

        foreach (var wo in workOrders)
        {
            sqlBuilder.AppendLine($@"
            INSERT INTO {TablePalletWorkOrders}
            (PalletId, WoCode, Quantity)
            VALUES (
                {palletId},
                '{wo.WoCode.Replace("'", "''")}',
                {wo.EnvelopeQty}
            );
        ");
        }

        // 🔥 update parent job timestamp
        sqlBuilder.AppendLine($@"
        UPDATE {TableJobs}
        SET LastUpdated = CURRENT_TIMESTAMP
        WHERE JobId = (
            SELECT JobId FROM {TablePallets}
            WHERE PalletId = {palletId}
        );
    ");

        await ExecuteAsync(sqlBuilder.ToString());
    }



    public static async Task<bool> IsDatabaseAvailableAsync()
    {
        try
        {
            using (var cts = new System.Threading.CancellationTokenSource(1000))
            {
                var response = await httpClient.GetAsync(
                    DefaultEndPoint + "/status",
                    cts.Token
                );

                return response.IsSuccessStatusCode;
            }
        }
        catch
        {
            return false;
        }
    }


    // ======================
    // RESPONSE DTOs
    // ======================
    private class WriteResponse
    {
        public List<WriteResult> results { get; set; }
    }

    private class WriteResult
    {
        public int rows_affected { get; set; }
    }

    private class QueryResponse
    {
        public List<QueryResult> results { get; set; }
    }

    private class QueryResult
    {
        public string[] columns { get; set; }
        public object[][] values { get; set; }
    }
}
