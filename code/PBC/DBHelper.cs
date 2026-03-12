using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;
public class CpsConfig
{
    public string CpsDb { get; set; }
    public string CpsQuery { get; set; }
    public string CpsServer { get; set; }
    public int ConnTimeOut { get; set; }
    public bool TrustedConn { get; set; }
    public bool TrustedServerCert { get; set; }
    public string SqlUser { get; set; }
    public string SqlPwd { get; set; }
}
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
        int maxRetries = Program.AppINI._rqClientMaxRetries;
        int delayMs = Program.AppINI._rqClientDelayMs;
        int attempt = 0;

        while (true)
        {
            try
            {
                var json = JsonConvert.SerializeObject(new[] { sql });
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(
                    DefaultEndPoint + "/db/query",
                    content
                );

                response.EnsureSuccessStatusCode();

                var body = await response.Content.ReadAsStringAsync();

                return new RqliteResult
                {
                    Success = true,
                    Records = ParseQueryResponse(body)
                };
            }
            catch (HttpRequestException ex)
            {
                attempt++;

                Console.WriteLine($"Query retry {attempt}: {ex.Message}");

                if (attempt >= maxRetries)
                    throw;

                await Task.Delay(delayMs);
            }
        }
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
    private static string Escape(string value)
    {
        return value?.Replace("'", "''") ?? "";
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

        if (!response.IsSuccessStatusCode)
        {
            return new RqliteResult
            {
                Success = false,
                Records = new List<Dictionary<string, object>>()
            };
        }

        return new RqliteResult
        {
            Success = true,
            Records = ParseQueryResponse(body)
        };
    }

    public static async Task<bool> ValidateAdminAsync(string username, string password)
    {
        try
        {
            // Prevent simple SQL injection
            username = username.Replace("'", "''");
            password = password.Replace("'", "''");

            var result = await SelectAsync(
                "users",
                $"WHERE Username = '{username}' AND PassWord = '{password}' LIMIT 1"
            );

            return result?.Records?.Count > 0;
        }
        catch
        {
            return false;
        }
    }

    public static async Task<bool> CreateUserAsync(string username, string password)
    {
        try
        {
            // Prevent simple SQL injection
            username = username.Replace("'", "''");
            password = password.Replace("'", "''");

            string sql = $@"
        INSERT INTO users (Username, PassWord, IsAdmin, IsActive)
        VALUES ('{username}', '{password}', 1, 1)";

            var json = JsonConvert.SerializeObject(new[] { sql });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/db/execute", content);

            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
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
    public static async Task<CpsConfig?> LoadCpsConfigFromDB()
    {
        var result = await SelectAsync("settings", "LIMIT 1");

        if (result == null || !result.Success || result.Records == null || result.Records.Count == 0)
            return null;

        var record = result.Records.First();

        int.TryParse(record["ConnTimeOut"]?.ToString(), out int timeout);
        int.TryParse(record["TrustedConn"]?.ToString(), out int trustedConn);
        int.TryParse(record["TrustedServerCert"]?.ToString(), out int trustedCert);

        return new CpsConfig
        {
            CpsDb = record["CpsDb"]?.ToString()?.Trim() ?? "",
            CpsQuery = record["CpsQuery"]?.ToString()?.Trim() ?? "",
            CpsServer = record["CpsServer"]?.ToString()?.Trim() ?? "",
            ConnTimeOut = timeout,
            TrustedConn = trustedConn == 1,
            TrustedServerCert = trustedCert == 1,

            // New fields
            SqlUser = record["SqlUser"]?.ToString()?.Trim() ?? "",
            SqlPwd = record["SqlPwd"]?.ToString()?.Trim() ?? ""
        };
    }

    public static async Task<HashSet<string>> GetExistingBarcodesAsync(IEnumerable<string> barcodes)
    {
        var barcodeList = barcodes
            .Where(b => !string.IsNullOrWhiteSpace(b))
            .Select(b => $"'{Escape(b)}'")
            .ToList();

        if (!barcodeList.Any())
            return new HashSet<string>();

        string sql =
            $"SELECT Barcode FROM workorders WHERE Barcode IN ({string.Join(",", barcodeList)})";

        var result = await QueryAsync(sql);

        var existing = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        if (result?.Records != null)
        {
            foreach (var r in result.Records)
                existing.Add(r["Barcode"]?.ToString());
        }

        return existing;
    }
    public static async Task<RqliteWriteResult> SaveSettingsAsync(
        string server,
        string db,
        string query,
        int timeout,
        bool trustedConn,
        bool trustedCert,
        string sqlUser,
        string sqlPwd)
    {
        MessageBox.Show("Encrypted password: " + sqlPwd);
        
    
        string sql = $@"
INSERT INTO settings
(
    Id,
    CpsDb,
    CpsQuery,
    CpsServer,
    ConnTimeOut,
    TrustedConn,
    TrustedServerCert,
    SqlUser,
    SqlPwd,
    LastUpdated
)
VALUES
(
    1,
    '{Escape(db)}',
    '{Escape(query)}',
    '{Escape(server)}',
    {timeout},
    {(trustedConn ? 1 : 0)},
    {(trustedCert ? 1 : 0)},
    '{Escape(sqlUser)}',
    '{Escape(sqlPwd)}',
    datetime('now')
)
ON CONFLICT(Id) DO UPDATE SET
    CpsDb = excluded.CpsDb,
    CpsQuery = excluded.CpsQuery,
    CpsServer = excluded.CpsServer,
    ConnTimeOut = excluded.ConnTimeOut,
    TrustedConn = excluded.TrustedConn,
    TrustedServerCert = excluded.TrustedServerCert,
    SqlUser = excluded.SqlUser,
    SqlPwd = excluded.SqlPwd,
    LastUpdated = datetime('now');
";

        return await ExecuteAsync(sql);
    }
    public static async Task<int> UpdatePbJobAsync(
        int jobId,
        string jobName,
        int jobNumber,
        bool isTemp,
        DateTime? expectedLastUpdated)
    {
        string lastUpdatedValue = expectedLastUpdated == null
            ? "NULL"
            : $"'{expectedLastUpdated.Value:yyyy-MM-dd HH:mm:ss}'";

        string sql = $@"
UPDATE PBJOB
SET
    JobName = {FormatValue(jobName)},
    JobNumber = {jobNumber},
    IsTemp = {(isTemp ? 1 : 0)},
    LastUpdated = datetime('now','localtime')
WHERE Id = {jobId}
  AND LastUpdated = {lastUpdatedValue};
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
    SET LastUpdated = datetime('now','localtime')
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

    public static async Task<(bool Success, string Error)> TestSqlConnectionAsync(
  string server,
  string db,
  int timeout,
  bool trustedConn,
  bool trustedCert,
  string sqlUser,
  string sqlPwd)
    {
        try
        {
            string connString;

            if (trustedConn)
            {
                connString =
                    $"Server={server};Database={db};Trusted_Connection=True;" +
                    $"TrustServerCertificate={trustedCert};Connection Timeout={timeout};";
            }
            else
            {
                connString =
                    $"Server={server};Database={db};User Id={sqlUser};Password={sqlPwd};" +
                    $"TrustServerCertificate={trustedCert};Connection Timeout={timeout};";
            }

            using (var conn = new SqlConnection(connString))
            {
                await conn.OpenAsync();
            }

            return (true, null);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
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
    DELETE FROM WORKORDER
    WHERE PalletId IN (
        SELECT PalletId FROM PALLET WHERE PBJobId = {jobId}
    );

    DELETE FROM PALLET
    WHERE PBJobId = {jobId};

    DELETE FROM {TableJobs}
    WHERE Id = {jobId};
    ";

        await ExecuteAsync(sql);
    }
    public static async Task SetJobReadyAsync(int jobId)
    {
        string sql = $@"
UPDATE PALLET
SET State = {(int)PalletState.Ready}
WHERE PBJobId = {jobId}
AND State = {(int)PalletState.Packed_NotReady}
AND PackedAt IS NOT NULL;

UPDATE PBJOB
SET LastUpdated = datetime('now','localtime')
WHERE Id = {jobId};
";

        await ExecuteAsync(sql);
    }
    public static async Task SetJobNotReadyAsync(int jobId)
    {
        string sql = $@"
UPDATE PALLET
SET State = {(int)PalletState.Packed_NotReady}
WHERE PBJobId = {jobId}
AND State = {(int)PalletState.Ready}
AND PackedAt IS NOT NULL;

UPDATE PBJOB
SET LastUpdated = datetime('now','localtime')
WHERE Id = {jobId};
";

        await ExecuteAsync(sql);
    }
    public static async Task<int> UpdatePalletPackingAsync(
     int palletId,
     int trayCount)
    {
        string sql1 = $@"
UPDATE {TablePallets}
SET
    TrayCount = {trayCount},
    PackedAt = datetime('now','localtime'),
    State = {(int)PalletState.Ready}
WHERE Id = {palletId}
  AND PackedAt IS NULL
  AND State = {(int)PalletState.NotReady};
";

        var result = await ExecuteAsync(sql1);

        if (result.RowsAffected > 0)
        {
            string sql2 = $@"
UPDATE {TableJobs}
SET LastUpdated = datetime('now','localtime')
WHERE Id = (
    SELECT PBJobId
    FROM {TablePallets}
    WHERE Id = {palletId}
)";
            await ExecuteAsync(sql2);
        }

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
        SET LastUpdated = datetime('now','localtime')
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


    public class CpsSettings
    {
        public string CPSServer { get; set; }
        public string CPSDatabase { get; set; }
        public string CPSQuery { get; set; }
        public int ConnectionTimeout { get; set; }
        public bool TrustedConnection { get; set; }
        public bool TrustedServerCertificate { get; set; }
        public string SqlUser { get; set; }
        public string SqlPassword { get; set; }
    }

    public static async Task<CpsSettings> LoadCpsSettingsAsync()
    {
        var result = await SelectAsync(TableAppSettings, "WHERE Id = 1");

        if (result.Records.Count == 0)
            return null;

        var row = result.Records[0];

        return new CpsSettings
        {
            CPSServer = row["CpsServer"]?.ToString(),
            CPSDatabase = row["CpsDb"]?.ToString(),
            CPSQuery = row["CpsQuery"]?.ToString(),
            ConnectionTimeout = row["ConnTimeOut"] == null ? 30 : Convert.ToInt32(row["ConnTimeOut"]),
            TrustedConnection = Convert.ToInt32(row["TrustedConn"]) == 1,
            TrustedServerCertificate = Convert.ToInt32(row["TrustedServerCert"]) == 1,
            SqlUser = row["SqlUser"]?.ToString(),
            SqlPassword = row["SqlPwd"]?.ToString()
        };
    }


    // ======================
    // WRITE OPERATIONS
    // ======================
    public static async Task<int> InsertPbJobAsync(PbJobModel job)
    {
        string sql = $@"
INSERT OR IGNORE INTO PBJob
(
    JobName,
    JobNumber,
    IsTemp,
    LastUpdated
)
VALUES
(
    {FormatValue(job.JobName)},
    {job.JobNumber},
    {(job.IsTemp ? 1 : 0)},
    datetime('now','localtime')
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
);

UPDATE PBJOB
SET LastUpdated = datetime('now','localtime')
WHERE Id IN ({idList})
AND changes() > 0;
";

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

        var sources = sourcePalletIds
            .Where(x => x != targetPalletId)
            .Distinct()
            .ToList();

        if (!sources.Any())
            return;

        var idList = string.Join(",", sources);

        string sql = $@"

-- 1️⃣ Move workorders
UPDATE {TablePalletWorkOrders}
SET PalletId = {targetPalletId}
WHERE PalletId IN ({idList});

-- 2️⃣ Delete source pallets (but not shipped)
DELETE FROM {TablePallets}
WHERE Id IN ({idList})
AND State != {(int)PalletState.Shipped};

-- 3️⃣ Reset target pallet state (contents changed)
UPDATE {TablePallets}
SET State = {(int)PalletState.NotReady}
WHERE Id = {targetPalletId};

-- 4️⃣ Update parent job timestamp
UPDATE {TableJobs}
SET LastUpdated = datetime('now','localtime')
WHERE Id = (
    SELECT PBJobId
    FROM {TablePallets}
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



    public static async Task<(bool Success, string Error)> TestRqClientAsync(string address, int timeoutMs)
    {
        try
        {
            ServicePointManager.Expect100Continue = false;

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            using var client = new HttpClient(handler)
            {
                BaseAddress = new Uri(address.Trim()),
                Timeout = TimeSpan.FromMilliseconds(timeoutMs)
            };

            using var cts = new CancellationTokenSource(timeoutMs);

            var response = await client.GetAsync("/status", cts.Token);

            return (response.IsSuccessStatusCode, null);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }
    //public static async Task<int> GetOrCreateWorkingPalletAsync(int jobId)
    //{
    //    var existing = await QueryAsync($@"
    //    SELECT Id
    //    FROM {TablePallets}
    //    WHERE PBJobId = {jobId}
    //    AND State = {(int)PalletState.NotReady}
    //    AND PackedAt IS NULL
    //    LIMIT 1
    //");

    //    if (existing.Records.Count > 0)
    //        return Convert.ToInt32(existing.Records[0]["Id"]);

    //    await ExecuteAsync($@"
    //    INSERT INTO {TablePallets}
    //    (PBJobId, State, TrayCount)
    //    VALUES ({jobId}, {(int)PalletState.NotReady}, 0);

    //    UPDATE {TablePallets}
    //    SET PalletNumber = Id
    //    WHERE Id = last_insert_rowid();

    //    UPDATE {TableJobs}
    //    SET LastUpdated = CURRENT_TIMESTAMP
    //    WHERE Id = {jobId};
    //");

    //    var result = await QueryAsync("SELECT last_insert_rowid() AS Id");

    //    if (result.Records.Count == 0)
    //        throw new Exception("Failed to locate active pallet.");

    //    return Convert.ToInt32(result.Records[0]["Id"]);
    //}

    public static async Task<int> GetJobsCountAsync()
    {
        var result = await QueryAsync("SELECT COUNT(*) AS Count FROM PBJob");

        return Convert.ToInt32(result.Records[0]["Count"]);
    }

    public static async Task<int> GetOrCreateWorkingPalletAsync(int jobId)
    {
        Debug.WriteLine($"[PALLET] Request pallet for Job {jobId}");

        // 1️⃣ Check existing pallet
        var existing = await QueryAsync($@"
SELECT Id
FROM {TablePallets}
WHERE PBJobId = {jobId}
AND PackedAt IS NULL
ORDER BY Id DESC
LIMIT 1
");

        if (existing.Records.Count > 0)
        {
            int palletId = Convert.ToInt32(existing.Records[0]["Id"]);
            Debug.WriteLine($"[PALLET] Using existing pallet {palletId}");
            return palletId;
        }

        Debug.WriteLine("[PALLET] No active pallet found. Creating new pallet...");

        // 2️⃣ Insert pallet
        await ExecuteAsync($@"
INSERT INTO {TablePallets}
(PBJobId, PalletNumber, State, TrayCount)
VALUES
(
    {jobId},
    0,
    {(int)PalletState.NotReady},
    0
);
");

        // 3️⃣ Get the pallet we just inserted
        var inserted = await QueryAsync($@"
SELECT Id
FROM {TablePallets}
WHERE PBJobId = {jobId}
AND PackedAt IS NULL
ORDER BY Id DESC
LIMIT 1
");

        if (inserted.Records.Count == 0)
            throw new Exception("Failed to retrieve inserted pallet.");

        int palletIdNew = Convert.ToInt32(inserted.Records[0]["Id"]);

        Debug.WriteLine($"[PALLET] Inserted pallet {palletIdNew}");

        // 4️⃣ Set PalletNumber = Id
        await ExecuteAsync($@"
UPDATE {TablePallets}
SET PalletNumber = {palletIdNew}
WHERE Id = {palletIdNew};
");

        // 5️⃣ Update PBJob timestamp
        await ExecuteAsync($@"
UPDATE {TableJobs}
SET LastUpdated = datetime('now','localtime')
WHERE Id = {jobId};
");

        return palletIdNew;
    }
    //    public static async Task<int> GetOrCreateWorkingPalletAsync(int jobId)
    //    {
    //        Debug.WriteLine("[PALLET] Request pallet for Job " + jobId);

    //        // 1️⃣ Try existing pallet
    //        var existing = await QueryAsync($@"
    //SELECT Id
    //FROM {TablePallets}
    //WHERE PBJobId = {jobId}
    //AND PackedAt IS NULL
    //ORDER BY Id DESC
    //LIMIT 1
    //");

    //        if (existing.Records.Count > 0)
    //        {
    //            int palletId = Convert.ToInt32(existing.Records[0]["Id"]);
    //            Debug.WriteLine($"[PALLET] Using existing pallet {palletId}");
    //            return palletId;
    //        }

    //        Debug.WriteLine("[PALLET] No active pallet found. Creating new pallet...");

    //        // 2️⃣ Insert pallet
    //        await ExecuteAsync($@"
    //INSERT INTO {TablePallets}
    //(PBJobId, PalletNumber, State, TrayCount)
    //VALUES
    //(
    //    {jobId},
    //    0,
    //    {(int)PalletState.NotReady},
    //    0
    //);
    //");

    //        // 3️⃣ Fetch the pallet we just inserted
    //        var inserted = await QueryAsync($@"
    //SELECT Id
    //FROM {TablePallets}
    //WHERE PBJobId = {jobId}
    //AND PackedAt IS NULL
    //ORDER BY Id DESC
    //LIMIT 1
    //");

    //        if (inserted.Records.Count == 0)
    //            throw new Exception("Failed to retrieve inserted pallet.");

    //        int palletIdNew = Convert.ToInt32(inserted.Records[0]["Id"]);

    //        Debug.WriteLine($"[PALLET] Inserted pallet {palletIdNew}");

    //        // 4️⃣ Set PalletNumber = Id
    //        await ExecuteAsync($@"
    //UPDATE {TablePallets}
    //SET PalletNumber = {palletIdNew}
    //WHERE Id = {palletIdNew};
    //");

    //        // 5️⃣ Update job timestamp
    //        await ExecuteAsync($@"
    //UPDATE {TableJobs}
    //SET LastUpdated = datetime('now','localtime')
    //WHERE Id = {jobId};
    //");

    //        return palletIdNew;
    //    }
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
    SET LastUpdated = datetime('now','localtime')
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


    //    public static async Task<RqliteWriteResult> SaveWorkOrdersAsync(
    //    int palletId,
    //    List<WorkOrder> workOrders)
    //    {
    //        if (workOrders == null || !workOrders.Any())
    //            return new RqliteWriteResult { Success = true, RowsAffected = 0 };

    //        var sqlBuilder = new StringBuilder();

    //        foreach (var wo in workOrders)
    //        {
    //            sqlBuilder.AppendLine($@"
    //INSERT OR IGNORE INTO {TablePalletWorkOrders}
    //(PalletId, Barcode, WorkOrder, Quantity)
    //VALUES (
    //    {palletId},
    //    {FormatValue(wo.Barcode)},
    //    {FormatValue(wo.WorkOrderCode)},
    //    {wo.Quantity}
    //);");
    //        }
    //        sqlBuilder.AppendLine($@"
    //UPDATE {TableJobs}
    //SET LastUpdated = datetime('now','localtime')
    //WHERE Id = (
    //    SELECT PBJobId
    //    FROM {TablePallets}
    //    WHERE Id = {palletId}
    //);
    //");
    //        return await ExecuteAsync(sqlBuilder.ToString());
    //    }

    public static async Task<RqliteWriteResult> SaveWorkOrdersAsync(
    int palletId,
    List<WorkOrder> workOrders)
    {
        if (workOrders == null || !workOrders.Any())
            return new RqliteWriteResult { Success = true, RowsAffected = 0 };

        var sql = new StringBuilder();

        sql.AppendLine($@"
INSERT OR IGNORE INTO {TablePalletWorkOrders}
(PalletId, Barcode, WorkOrder, Quantity)
VALUES");

        for (int i = 0; i < workOrders.Count; i++)
        {
            var wo = workOrders[i];

            sql.Append($@"(
{palletId},
{FormatValue(wo.Barcode)},
{FormatValue(wo.WorkOrderCode)},
{wo.Quantity}
)");

            if (i < workOrders.Count - 1)
                sql.AppendLine(",");
            else
                sql.AppendLine(";");
        }

        sql.AppendLine($@"
UPDATE {TableJobs}
SET LastUpdated = datetime('now','localtime')
WHERE Id = (
    SELECT PBJobId
    FROM {TablePallets}
    WHERE Id = {palletId}
);");

        return await ExecuteAsync(sql.ToString());
    }


    public static async Task<bool> IsDatabaseAvailableAsync()
    {
        try
        {
            using (var cts = new CancellationTokenSource(3000)) // 3 seconds safer
            {
                var response = await httpClient.GetAsync("/status", cts.Token);

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
