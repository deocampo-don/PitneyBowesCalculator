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
    public const string TableJobs = "PBJOB";
    public const string TablePallets = "PALLET";
    public const string TablePalletWorkOrders = "WORKORDERS";
    public const string TableAppSettings = "SETTINGS";

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

    public static async Task<RqliteResult> QueryAsync(string sql)
    {
        var json = JsonConvert.SerializeObject(new[] { sql });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(
            DefaultEndPoint + "/db/query",
            content
        );

        var body = await response.Content.ReadAsStringAsync();

        return new RqliteResult
        {
            Success = true,
            Records = ParseQueryResponse(body)
        };
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

        return new RqliteResult
        {
            Success = true,
            Records = ParseQueryResponse(body)
        };
    }

    public static async Task UpdateJobShippedDateAsync(int jobId, DateTime shippedDate)
    {

        string sql = $@"
    UPDATE PALLET
    SET State = {(int)PalletState.Shipped},
        ShippedAt = CURRENT_TIMESTAMP
    WHERE PBJobId = {jobId};
    ";

        await ExecuteAsync(sql);
    }
    public static async Task<List<(int JobId, DateTime? LastUpdated)>>
      LoadJobUpdateInfoAsync()
    {
        var sql = $"SELECT Id, LastUpdated FROM {TableJobs}";
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
    public static async Task<int> UpdatePbJobAsync(
     int jobId,
     string jobName,
     int jobNumber,
     bool isTemp,
     DateTime? expectedLastUpdated)
    {
        string sql = $@"
    UPDATE PBJOB
    SET
        JobName = {FormatValue(jobName)},
        JobNumber = {jobNumber},
        IsTemp = {(isTemp ? 1 : 0)},
        LastUpdated = CURRENT_TIMESTAMP
    WHERE Id = {jobId}
      AND LastUpdated = {FormatValue(expectedLastUpdated)};
    ";

        var result = await ExecuteAsync(sql);
        return result.RowsAffected;
    }
    public static async Task DeleteWorkOrdersAsync(IEnumerable<int> workOrderIds)
    {
        if (workOrderIds == null || !workOrderIds.Any())
            return;

        var idList = string.Join(",", workOrderIds);

        string sql = $@"
    DELETE FROM {TablePalletWorkOrders}
    WHERE Id IN ({idList});

    UPDATE {TableJobs}
    SET LastUpdated = CURRENT_TIMESTAMP
    WHERE Id IN (
        SELECT PBJobId
        FROM {TablePallets}
        WHERE Id IN (
            SELECT PalletId
            FROM {TablePalletWorkOrders}
            WHERE Id IN ({idList})
        )
    );
    ";

        await ExecuteAsync(sql);
    }


    public static async Task<DateTime?> GetJobsLastUpdatedAsync()
    {
        var result = await QueryAsync(
            $"SELECT MAX(LastUpdated) AS LastUpdated FROM {TableJobs}"
        );

        if (result.Records.Count == 0)
            return null;

        var value = result.Records[0]["LastUpdated"];

        if (value == null)
            return null;

        return DateTime.Parse(value.ToString());
    }

    public static async Task DeletePbJobAsync(int jobId)
    {
        string sql = $@"
        DELETE FROM {TableJobs}
        WHERE Id = {jobId};
    ";

        await ExecuteAsync(sql);
    }

    public static async Task<int> UpdatePalletPackingAsync(
    int palletId,
    int trayCount)
    {
        string sql = $@"
    UPDATE {TablePallets}
    SET
        TrayCount = {trayCount},
        PackedAt = CURRENT_TIMESTAMP,
        State = {(int)PalletState.Packed}
    WHERE Id = {palletId}
      AND PackedAt IS NULL;

    UPDATE {TableJobs}
    SET LastUpdated = CURRENT_TIMESTAMP
    WHERE Id = (
        SELECT PBJobId
        FROM {TablePallets}
        WHERE Id = {palletId}
    );
    ";

        var result = await ExecuteAsync(sql);
        return result.RowsAffected;
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

        // PBJob
        await ExecuteAsync(@"
    CREATE TABLE IF NOT EXISTS PBJob (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        JobName VARCHAR(100) NOT NULL,
        JobNumber INTEGER NOT NULL UNIQUE,
        IsTemp INTEGER NOT NULL,
        LastUpdated DATETIME DEFAULT CURRENT_TIMESTAMP
    );
    ");

        // PALLET
        await ExecuteAsync(@"
    CREATE TABLE IF NOT EXISTS PALLET (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        PBJobId INTEGER NOT NULL,
        PalletNumber INTEGER NOT NULL,
        PackedAt DATETIME,
        ShippedAt DATETIME,
        State INTEGER DEFAULT 0,
        TrayCount INTEGER NOT NULL DEFAULT 0,

        FOREIGN KEY (PBJobId) REFERENCES PBJob(Id) ON DELETE CASCADE
    );
    ");

        // WORKORDERS
        await ExecuteAsync(@"
    CREATE TABLE IF NOT EXISTS WORKORDERS (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        PalletId INTEGER NOT NULL,
        Barcode VARCHAR(50) NOT NULL UNIQUE,
        WorkOrder VARCHAR(50) NOT NULL,
        Quantity INTEGER NOT NULL,

        FOREIGN KEY (PalletId) REFERENCES PALLET(Id) ON DELETE CASCADE
    );
    ");

        // AppSettings
        await ExecuteAsync(@"
    CREATE TABLE IF NOT EXISTS AppSettings (
        Id INTEGER PRIMARY KEY,
        CPSServer TEXT,
        CPSDatabase TEXT,
        CPSQuery TEXT,
        ConnectionTimeout INTEGER,
        TrustedConnection INTEGER,
        TrustedServerCertificate INTEGER
    );
    ");

        // Indexes
        await ExecuteAsync("CREATE INDEX IF NOT EXISTS idx_pallet_pbjobid ON PALLET(PBJobId);");

        await ExecuteAsync("CREATE INDEX IF NOT EXISTS idx_pallet_job_state ON PALLET(PBJobId, State);");

        await ExecuteAsync("CREATE INDEX IF NOT EXISTS idx_workorders_palletid ON WORKORDERS(PalletId);");

        await ExecuteAsync(@"
CREATE UNIQUE INDEX IF NOT EXISTS idx_one_active_pallet
ON PALLET(PBJobId)
WHERE State = 0;
");
    }

    public static async Task<RqliteWriteResult> SaveCpsSettingsAsync(
    string server,
    string database,
    string query,
    int timeout,
    bool trustedConnection,
    bool trustedServerCert)
    {
        string sql = $@"
    INSERT OR REPLACE INTO {TableAppSettings}
    (Id, CPSServer, CPSDatabase, CPSQuery, ConnectionTimeout, TrustedConnection, TrustedServerCertificate)
    VALUES
    (
        1,
        {FormatValue(server)},
        {FormatValue(database)},
        {FormatValue(query)},
        {timeout},
        {(trustedConnection ? 1 : 0)},
        {(trustedServerCert ? 1 : 0)}
    );";

        return await ExecuteAsync(sql);
    }

    public class CpsSettings
    {
        public string CPSServer { get; set; }
        public string CPSDatabase { get; set; }
        public string CPSQuery { get; set; }
        public int ConnectionTimeout { get; set; }
        public bool TrustedConnection { get; set; }
        public bool TrustedServerCertificate { get; set; }
    }

    public static async Task<CpsSettings> LoadCpsSettingsAsync()
    {
        var result = await SelectAsync(TableAppSettings, "WHERE Id = 1");

        if (result.Records.Count == 0)
            return null;

        var row = result.Records[0];

        return new CpsSettings
        {
            CPSServer = row["CPSServer"]?.ToString(),
            CPSDatabase = row["CPSDatabase"]?.ToString(),
            CPSQuery = row["CPSQuery"]?.ToString(),
            ConnectionTimeout = row["ConnectionTimeout"] == null ? 30 : Convert.ToInt32(row["ConnectionTimeout"]),
            TrustedConnection = Convert.ToInt32(row["TrustedConnection"]) == 1,
            TrustedServerCertificate = Convert.ToInt32(row["TrustedServerCertificate"]) == 1
        };
    }


    // ======================
    // WRITE OPERATIONS
    // ======================
    public static async Task<int> InsertPbJobAsync(PbJobModel job)
    {
        string sql = $@"
    INSERT OR IGNORE INTO PBJob
    (JobName, JobNumber, IsTemp)
    VALUES
    (
        {FormatValue(job.JobName)},
        {job.JobNumber},
        {(job.IsTemp ? 1 : 0)}
    );
    ";

        var write = await ExecuteAsync(sql);

        if (write.RowsAffected == 0)
            throw new Exception("JobNumber already exists.");

        var result = await QueryAsync(
            $"SELECT Id FROM PBJob WHERE JobNumber = {job.JobNumber}"
        );

        return Convert.ToInt32(result.Records[0]["Id"]);
    }

    public static async Task<bool> JobNumberExistsAsync(int jobNumber)
    {
        var result = await QueryAsync(
            $"SELECT 1 FROM {TableJobs} WHERE JobNumber = {jobNumber} LIMIT 1"
        );

        return result.Records.Count > 0;
    }

    // ======================
    // BATCH SHIP JOBS (New)
    // ======================
    //public static async Task ShipJobsAsync(IEnumerable<int> jobIds)
    //{
    //    if (jobIds == null || !jobIds.Any())
    //        return;

    //    var idList = string.Join(",", jobIds);

    //    string sql = $@"
    //UPDATE PALLET
    //SET ShippedAt = CURRENT_TIMESTAMP,
    //    State = {(int)PalletState.Shipped}
    //WHERE PBJobId IN ({idList})";

    //    await ExecuteAsync(sql);
    //}

    public static async Task ShipJobsAsync(IEnumerable<int> jobIds)
    {
        if (jobIds == null || !jobIds.Any())
            return;

        var idList = string.Join(",", jobIds);
        var shippedAt = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

        string sql = $@"
UPDATE PALLET
SET ShippedAt = '{shippedAt}',
    State = {(int)PalletState.Shipped}
WHERE PBJobId IN ({idList})
AND State = {(int)PalletState.Packed}
AND NOT EXISTS (
    SELECT 1
    FROM PALLET p2
    WHERE p2.PBJobId = PALLET.PBJobId
    AND p2.State IN (
        {(int)PalletState.NotReady},
        {(int)PalletState.Ready},
        {(int)PalletState.Packed_NotReady}
    )
);";

        await ExecuteAsync(sql);
    }

    // =====================================================
    // MERGE PALLETS (MOVE WO ONLY, DELETE SOURCE PALLETS)
    // =====================================================
    public static async Task MergePalletsIntoAsync(
        IEnumerable<int> sourcePalletIds,
        int targetPalletId)
    {
        if (sourcePalletIds == null || !sourcePalletIds.Any())
            return;

        var idList = string.Join(",", sourcePalletIds);

        string sql = $@"
-- 1️⃣ Move WorkOrders into target pallet
UPDATE {TablePalletWorkOrders}
SET PalletId = {targetPalletId}
WHERE PalletId IN ({idList});

-- 2️⃣ Delete source pallets
DELETE FROM {TablePallets}
WHERE Id IN ({idList});

-- 3️⃣ Update parent job timestamp
UPDATE {TableJobs}
SET LastUpdated = CURRENT_TIMESTAMP
WHERE Id = (
    SELECT PBJobId FROM {TablePallets}
    WHERE Id = {targetPalletId}
);
";

        await ExecuteAsync(sql);
    }

    // ======================
    // READ OPERATIONS (⭐ ADDED)
    // ======================
    public static async Task<List<PbJobModel>> LoadJobsAsync()
    {
        var result = await QueryAsync(@"
SELECT
    j.Id AS JobId,
    j.JobName,
    j.JobNumber,
    j.IsTemp,
    j.LastUpdated,

    p.Id AS PalletId,
    p.PalletNumber,
    p.PackedAt,
    p.ShippedAt,
    p.State,
    p.TrayCount,

    w.Id AS WorkOrderId,
    w.Barcode,
    w.WorkOrder,
    w.Quantity

FROM PBJOB j
LEFT JOIN PALLET p ON p.PBJobId = j.Id
LEFT JOIN WORKORDERS w ON w.PalletId = p.Id
ORDER BY j.Id, p.PalletNumber
");

        var jobs = new Dictionary<int, PbJobModel>();
        var pallets = new Dictionary<int, Pallet>();

        // ⭐ Prevent duplicate workorders instantly
        var workOrdersSeen = new HashSet<int>();

        foreach (var row in result.Records)
        {
            int jobId = Convert.ToInt32(row["JobId"]);

            if (!jobs.TryGetValue(jobId, out var job))
            {
                job = new PbJobModel
                {
                    JobId = jobId,
                    JobName = row["JobName"]?.ToString(),
                    JobNumber = Convert.ToInt32(row["JobNumber"]),
                    IsTemp = Convert.ToInt32(row["IsTemp"]) == 1,
                    LastUpdated = row["LastUpdated"] == null
                        ? (DateTime?)null
                        : Convert.ToDateTime(row["LastUpdated"]),
                    Pallets = new List<Pallet>()
                };

                jobs[jobId] = job;
            }

            if (row["PalletId"] == null)
                continue;

            int palletId = Convert.ToInt32(row["PalletId"]);

            if (!pallets.TryGetValue(palletId, out var pallet))
            {
                pallet = new Pallet
                {
                    PalletId = palletId,
                    PBJobId = jobId,
                    PalletNumber = Convert.ToInt32(row["PalletNumber"]),
                    TrayCount = Convert.ToInt32(row["TrayCount"]),
                    PackedAt = row["PackedAt"] == null ? (DateTime?)null : Convert.ToDateTime(row["PackedAt"]),
                    ShippedAt = row["ShippedAt"] == null ? (DateTime?)null : Convert.ToDateTime(row["ShippedAt"]),
                    State = (PalletState)Convert.ToInt32(row["State"]),
                    WorkOrders = new List<WorkOrder>()
                };

                pallets[palletId] = pallet;
                job.Pallets.Add(pallet);
            }

            if (row["WorkOrderId"] == null)
                continue;

            int woId = Convert.ToInt32(row["WorkOrderId"]);

            // ⭐ O(1) duplicate protection
            if (!workOrdersSeen.Add(woId))
                continue;

            pallet.WorkOrders.Add(new WorkOrder(
                row["WorkOrder"]?.ToString(),
                Convert.ToInt32(row["Quantity"])
            )
            {
                Id = woId,
                PalletId = palletId,
                Barcode = row["Barcode"]?.ToString()
            });
        }

        // ⭐ Derive job shipment date from pallets
        // ⭐ Derive job shipment date from pallets
        foreach (var job in jobs.Values)
        {
            job.ShippedDate = job.Pallets
                .Where(p => p.ShippedAt.HasValue)
                .Select(p => p.ShippedAt)
                .FirstOrDefault();
        }
    
        return jobs.Values.ToList();

    }
    public static async Task<int> GetOrCreateWorkingPalletAsync(int jobId)
    {
        Console.WriteLine($"[PALLET] Start GetOrCreateWorkingPalletAsync for Job {jobId}");

        var existing = await QueryAsync($@"
        SELECT Id
        FROM {TablePallets}
        WHERE PBJobId = {jobId}
        AND State = {(int)PalletState.NotReady}
        LIMIT 1
    ");

        if (existing.Records.Count > 0)
        {
            int palletId = Convert.ToInt32(existing.Records[0]["Id"]);
            Console.WriteLine($"[PALLET] Found existing pallet {palletId}");
            return palletId;
        }

        Console.WriteLine("[PALLET] No active pallet found. Creating new pallet...");

        var numberResult = await QueryAsync($@"
        SELECT IFNULL(MAX(PalletNumber),0) + 1 AS NextNum
        FROM {TablePallets}
        WHERE PBJobId = {jobId}
    ");

        int nextNumber = Convert.ToInt32(numberResult.Records[0]["NextNum"]);

        Console.WriteLine($"[PALLET] Next pallet number: {nextNumber}");

        await ExecuteAsync($@"
        INSERT INTO {TablePallets}
        (PBJobId, PalletNumber, State, TrayCount)
        VALUES (
            {jobId},
            {nextNumber},
            {(int)PalletState.NotReady},
            0
        );
    ");

        var result = await QueryAsync($@"
        SELECT Id
        FROM {TablePallets}
        WHERE PBJobId = {jobId}
        AND State = {(int)PalletState.NotReady}
        LIMIT 1
    ");

        if (result.Records.Count == 0)
            throw new Exception("Failed to locate active pallet.");

        return Convert.ToInt32(result.Records[0]["Id"]);
    }
    public static async Task<PbJobModel> LoadSingleJobGraphAsync(int jobId)
    {
        var result = await QueryAsync($@"
    SELECT
        j.Id AS JobId,
        j.JobName,
        j.JobNumber,
        j.IsTemp,
        j.LastUpdated,

        p.Id AS PalletId,
        p.PalletNumber,
        p.PackedAt,
        p.ShippedAt,
        p.State,
        p.TrayCount,

        w.Id AS WorkOrderId,
        w.Barcode,
        w.WorkOrder,
        w.Quantity

    FROM PBJOB j
    LEFT JOIN PALLET p ON p.PBJobId = j.Id
    LEFT JOIN WORKORDERS w ON w.PalletId = p.Id
    WHERE j.Id = {jobId}
    ORDER BY p.PalletNumber
    ");

        PbJobModel job = null;
        var pallets = new Dictionary<int, Pallet>();

        foreach (var row in result.Records)
        {
            if (job == null)
            {
                job = new PbJobModel
                {
                    JobId = jobId,
                    JobName = row["JobName"]?.ToString(),
                    JobNumber = Convert.ToInt32(row["JobNumber"]),
                    IsTemp = Convert.ToInt32(row["IsTemp"]) == 1,
                    LastUpdated = row["LastUpdated"] == null
                        ? (DateTime?)null
                        : Convert.ToDateTime(row["LastUpdated"]),
                    Pallets = new List<Pallet>()
                };
            }

            if (row["PalletId"] != null)
            {
                int palletId = Convert.ToInt32(row["PalletId"]);

                if (!pallets.TryGetValue(palletId, out var pallet))
                {
                    pallet = new Pallet
                    {
                        PalletId = palletId,
                        PBJobId = jobId,
                        PalletNumber = Convert.ToInt32(row["PalletNumber"]),
                        TrayCount = Convert.ToInt32(row["TrayCount"]),
                        PackedAt = row["PackedAt"] == null ? (DateTime?)null : Convert.ToDateTime(row["PackedAt"]),
                        ShippedAt = row["ShippedAt"] == null ? (DateTime?)null : Convert.ToDateTime(row["ShippedAt"]),
                        State = (PalletState)Convert.ToInt32(row["State"]),
                        WorkOrders = new List<WorkOrder>()
                    };

                    pallets[palletId] = pallet;
                    job.Pallets.Add(pallet);
                }

                if (row["WorkOrderId"] != null)
                {
                    pallet.WorkOrders.Add(new WorkOrder(
                        row["WorkOrder"]?.ToString(),
                        Convert.ToInt32(row["Quantity"])
                    )
                    {
                        Id = Convert.ToInt32(row["WorkOrderId"]),
                        PalletId = palletId,
                        Barcode = row["Barcode"]?.ToString()
                    });
                }
            }
        }

        return job;
    }

    private static List<Dictionary<string, object>> ParseQueryResponse(string body)
    {
        var result = JsonConvert.DeserializeObject<QueryResponse>(body);
        var rows = new List<Dictionary<string, object>>();

        if (result?.results?.Count > 0)
        {
            var r = result.results[0];

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

        return rows;
    }
    //public static async Task<List<Pallet>> LoadPalletsAsync(int jobId)
    //{
    //    var result = await SelectAsync(TablePallets, $"WHERE PBJobId = {jobId}");
    //    var pallets = new List<Pallet>();

    //    foreach (var row in result.Records)
    //    {
    //        pallets.Add(new Pallet
    //        {
    //            PalletId = Convert.ToInt32(row["Id"]),
    //            PBJobId = jobId,
    //            PalletNumber = Convert.ToInt32(row["PalletNumber"]),
    //            TrayCount = Convert.ToInt32(row["TrayCount"]),
    //            PackedAt = row["PackedAt"] == null ? (DateTime?)null : Convert.ToDateTime(row["PackedAt"]),
    //            ShippedAt = row["ShippedAt"] == null ? (DateTime?)null : Convert.ToDateTime(row["ShippedAt"]),
    //            State = (PalletState)Convert.ToInt32(row["State"]),
    //            WorkOrders = new List<WorkOrder>()
    //        });
    //    }

    //    return pallets;
    //}

    //public static async Task<List<WorkOrder>> LoadWorkOrdersAsync(int palletId)
    //{
    //    var result = await SelectAsync(TablePalletWorkOrders, $"WHERE PalletId = {palletId}");
    //    var workOrders = new List<WorkOrder>();

    //    foreach (var row in result.Records)
    //    {
    //        workOrders.Add(new WorkOrder(
    //            row["WorkOrder"].ToString(),
    //            Convert.ToInt32(row["Quantity"])
    //        )
    //        {
    //            PalletId = palletId,
    //            Barcode = row["Barcode"]?.ToString()
    //        });
    //    }

    //    return workOrders;
    //}

    public static async Task<List<WorkOrder>> LoadWorkOrdersAsync(int palletId)
    {
        var result = await QueryAsync($@"
    SELECT Id, Barcode, WorkOrder, Quantity
    FROM WORKORDERS
    WHERE PalletId = {palletId}
    ");

        var list = new List<WorkOrder>();

        foreach (var row in result.Records)
        {
            list.Add(new WorkOrder(
                row["WorkOrder"]?.ToString(),
                Convert.ToInt32(row["Quantity"]))
            {
                Id = Convert.ToInt32(row["Id"]),
                PalletId = palletId,
                Barcode = row["Barcode"]?.ToString()
            });
        }

        return list;
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
            PBJobId = Convert.ToInt32(row["JobId"]),
            PalletNumber = Convert.ToInt32(row["PalletNumber"]),
            TrayCount = Convert.ToInt32(row["TrayCount"]),
            PackedAt = row["PackedTime"] == null ? (DateTime?)null : DateTime.Parse(row["PackedTime"].ToString()),
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
                Id = Convert.ToInt32(row["PalletWorkOrderId"])
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
            .GroupBy(p => p.PBJobId)
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
    (PBJobId, PalletNumber, PackedAt, TrayCount, State)
    VALUES (
        {pallet.PBJobId},
        {pallet.PalletNumber},
        {FormatValue(pallet.PackedAt)},
        {pallet.TrayCount},
        {(int)pallet.State}
    );

    UPDATE {TableJobs}
    SET LastUpdated = CURRENT_TIMESTAMP
    WHERE Id = {pallet.PBJobId};
    ";

        await ExecuteAsync(sql);

        var result = await QueryAsync(
            $"SELECT Id FROM {TablePallets} WHERE PBJobId = {pallet.PBJobId} ORDER BY Id DESC LIMIT 1"
        );

        if (result.Records.Count > 0)
            return Convert.ToInt32(result.Records[0]["Id"]);

        return 0;
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
            var barcode = string.IsNullOrWhiteSpace(wo.Barcode)
                ? $"STATIC-{Guid.NewGuid()}"
                : wo.Barcode;

            sqlBuilder.AppendLine($@"
    INSERT OR IGNORE INTO {TablePalletWorkOrders}
    (PalletId, Barcode, WorkOrder, Quantity)
    VALUES (
        {palletId},
        {FormatValue(barcode)},
        {FormatValue(wo.WorkOrderCode)},
        {wo.Quantity}
    );
    ");
        }

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
