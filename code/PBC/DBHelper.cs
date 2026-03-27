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

           
                Utils.WriteUnexpectedError(
                    $"Rqlite query retry {attempt}/{maxRetries}"
                );

                
                if (attempt >= maxRetries)
                {
                    Utils.WriteUnexpectedError(
                        $"Rqlite query failed after {attempt} attempts"
                    );

                    Utils.WriteExceptionError(ex);
                    throw;
                }

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
    private static async Task<object?> ExecuteScalarAsync(string sql)
    {
        var json = JsonConvert.SerializeObject(new[] { sql });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(
            DefaultEndPoint + "/db/query?level=strong",
            content
        );

        var body = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception(body);

        var result = JsonConvert.DeserializeObject<QueryResponse>(body);

        var value = result?.results?
            .FirstOrDefault()?
            .values?
            .FirstOrDefault()?
            .FirstOrDefault();

        return value;
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
            var result = await SelectAsync(
     "users",
     $"WHERE Username = '{username}' AND PassWord = '{password}' LIMIT 1"
 );

            return result?.Records?.Count > 0;
        }
        catch (Exception ex)
        {
            Utils.WriteUnexpectedError($"ValidateAdmin failed | Username={username}");
            Utils.WriteExceptionError(ex);
            return false;
        }
    }
    public static async Task<bool> CreateUserAsync(string username, string password)
    {
        try
        {
            // ⚠️ Still not ideal, but keeping your current pattern
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
        catch (Exception ex)
        {
            // ✅ Proper logging
            Utils.WriteUnexpectedError($"CreateUser failed | Username={username}");
            Utils.WriteExceptionError(ex);

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
                        lastUpdated = row[1] == null
    ? (DateTime?)null
    : Convert.ToDateTime(row[1]);

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
        //MessageBox.Show("Encrypted password: " + sqlPwd);
        MessageDialogBox.ShowDialog("", "Encrypted password: " + sqlPwd, MessageBoxButtons.OK, MessageType.Info);


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
    LastUpdated = strftime('%Y-%m-%d %H:%M:%f','now','localtime')
WHERE Id = {jobId}
  AND LastUpdated = {lastUpdatedValue};
";

        var result = await ExecuteAsync(sql);
        return result.RowsAffected;
    }

    public static async Task DeleteWorkOrdersAndMaybePalletAsync(
    List<int> workOrderIds,
    int palletId)
    {
        var idList = string.Join(",", workOrderIds);

        // ✅ get jobId FIRST (safe)
        var result = await QueryAsync($@"
SELECT PBJobId
FROM {TablePallets}
WHERE Id = {palletId}
LIMIT 1;
");

        if (result.Records.Count == 0)
            return;

        int jobId = Convert.ToInt32(result.Records[0]["PBJobId"]);

        // ✅ single atomic execution (no CTE)
        string sql = $@"

DELETE FROM {TablePalletWorkOrders}
WHERE Id IN ({idList});

DELETE FROM {TablePallets}
WHERE Id = {palletId}
AND NOT EXISTS (
    SELECT 1 FROM {TablePalletWorkOrders} WHERE PalletId = {palletId}
);

UPDATE {TableJobs}
SET LastUpdated = strftime('%Y-%m-%d %H:%M:%f','now','localtime')
WHERE Id = {jobId};
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
            Utils.WriteUnexpectedError(
            $"SQL connection test failed | Server={server}, DB={db}, Trusted={trustedConn}, Timeout={timeout}"
        );
            Utils.WriteExceptionError(ex);
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
        await ExecuteAsync($@"
DELETE FROM {TablePalletWorkOrders}
WHERE PalletId IN (
    SELECT Id FROM {TablePallets} WHERE PBJobId = {jobId}
);

DELETE FROM {TablePallets}
WHERE PBJobId = {jobId};

DELETE FROM {TableJobs}
WHERE Id = {jobId};
");
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
SET LastUpdated = strftime('%Y-%m-%d %H:%M:%f','now','localtime')
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
SET LastUpdated = strftime('%Y-%m-%d %H:%M:%f','now','localtime')
WHERE Id = {jobId};
";

        await ExecuteAsync(sql);
    }
    public static async Task UndoPackedPalletAsync(List<int> palletIds, int jobId)
    {
        if (palletIds == null || palletIds.Count == 0)
        {
            Debug.WriteLine("UndoPackedPalletAsync: palletIds is empty.");
            return;
        }

        var ids = string.Join(",", palletIds);

        Debug.WriteLine($"UndoPackedPalletAsync: palletIds = {ids}");
        Debug.WriteLine($"UndoPackedPalletAsync: jobId = {jobId}");

        string sql = $@"
UPDATE {TablePallets}
SET State = {(int)PalletState.NotReady},
    PackedAt = NULL
WHERE PalletNumber IN ({ids})
AND State = {(int)PalletState.Ready};

UPDATE {TableJobs}
SET LastUpdated = strftime('%Y-%m-%d %H:%M:%f','now','localtime')
WHERE Id = {jobId};
";

        var result = await ExecuteAsync(sql);
   }
    public static async Task<DateTime?> TogglePalletReadyAsync(
     int jobId,
     PalletState fromState,
     PalletState toState)
    {
        string sql = $@"
UPDATE {TablePallets}
SET State = {(int)toState}
WHERE PBJobId = {jobId}
AND State = {(int)fromState};

UPDATE {TableJobs}
SET LastUpdated = strftime('%Y-%m-%d %H:%M:%f','now','localtime')
WHERE Id = {jobId};
";

        await ExecuteAsync(sql);

        // 🔥 immediately fetch exact timestamp for THIS job
        var result = await QueryAsync($@"
SELECT LastUpdated
FROM {TableJobs}
WHERE Id = {jobId}
LIMIT 1;
");

        if (result.Records.Count == 0)
            return null;

        return DateTime.Parse(result.Records[0]["LastUpdated"].ToString());
    }
    public static async Task<int> UpdatePalletPackingAsync(
    int palletId,
    int trayCount)
    {
        string sql = $@"
    UPDATE {TablePallets}
    SET
        TrayCount = {trayCount},
        PackedAt = datetime('now','localtime'),
        State = {(int)PalletState.Ready}
    WHERE Id = {palletId}
      AND PackedAt IS NULL
      AND State = {(int)PalletState.NotReady};

    UPDATE {TableJobs}
    SET LastUpdated = strftime('%Y-%m-%d %H:%M:%f','now','localtime')
    WHERE Id = (
        SELECT PBJobId
        FROM {TablePallets}
        WHERE Id = {palletId}
    );
    ";

        var result = await ExecuteAsync(sql);

        return result.RowsAffected;
    }

    //    public static async Task<DateTime?> UpdatePalletPackingAsync(
    //        int palletId,
    //        int trayCount)
    //    {
    //        string updateSql = $@"
    //UPDATE {TablePallets}
    //SET
    //    TrayCount = {trayCount},
    //    PackedAt = strftime('%Y-%m-%d %H:%M:%f','now','localtime'),
    //    State = {(int)PalletState.Ready}
    //WHERE Id = {palletId}
    //  AND PackedAt IS NULL
    //  AND State = {(int)PalletState.NotReady};

    //UPDATE {TableJobs}
    //SET LastUpdated = strftime('%Y-%m-%d %H:%M:%f','now','localtime')
    //WHERE Id = (
    //    SELECT PBJobId
    //    FROM {TablePallets}
    //    WHERE Id = {palletId}
    //);
    //";

    //        var writeResult = await ExecuteAsync(updateSql);

    //        if (writeResult.RowsAffected == 0)
    //            return null;

    //        string querySql = $@"
    //SELECT LastUpdated
    //FROM {TableJobs}
    //WHERE Id = (
    //    SELECT PBJobId
    //    FROM {TablePallets}
    //    WHERE Id = {palletId}
    //);
    //";

    //        var queryResult = await QueryAsync(querySql);

    //        var row = queryResult?.Records?.FirstOrDefault();

    //        if (row == null)
    //            return null;

    //        if (!row.TryGetValue("LastUpdated", out var value) || value == null)
    //            return null;

    //        return Convert.ToDateTime(value.ToString());
    //    }
    public static async Task DeletePalletsAsync(IEnumerable<int> palletIds)
    {
        if (palletIds == null || !palletIds.Any())
            return;

        var idList = string.Join(",", palletIds);

        // get jobIds first
        var result = await QueryAsync($@"
SELECT DISTINCT PBJobId
FROM {TablePallets}
WHERE Id IN ({idList});
");

        var jobIds = result.Records
            .Select(r => Convert.ToInt32(r["PBJobId"]))
            .Distinct()
            .ToList();

        if (!jobIds.Any())
            return;

        var jobIdList = string.Join(",", jobIds);

        await ExecuteAsync($@"
DELETE FROM {TablePalletWorkOrders}
WHERE PalletId IN ({idList});

DELETE FROM {TablePallets}
WHERE Id IN ({idList});

UPDATE {TableJobs}
SET LastUpdated = strftime('%Y-%m-%d %H:%M:%f','now','localtime')
WHERE Id IN ({jobIdList});
");
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
    IsActive,
    LastUpdated
)
VALUES
(
    {FormatValue(job.JobName)},
    {job.JobNumber},
    {(job.IsTemp ? 1 : 0)},
    1,
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

    public static async Task<PbJobModel?> GetJobByNumberAsync(int jobNumber)
    {
        var sql = $@"
        SELECT Id, JobName, JobNumber, IsTemp, IsActive, LastUpdated
        FROM PBJOB
        WHERE JobNumber = {jobNumber}
        LIMIT 1;
    ";

        var result = await QueryAsync(sql);

        var row = result.Records.FirstOrDefault();
        if (row == null)
            return null;

        return new PbJobModel
        {
            JobId = Convert.ToInt32(row["Id"]),
            JobName = row["JobName"]?.ToString(),
            JobNumber = Convert.ToInt32(row["JobNumber"]),
            IsTemp = Convert.ToInt32(row["IsTemp"]) == 1,
            IsActive = Convert.ToInt32(row["IsActive"]) == 1,
            LastUpdated = DateTime.Parse(row["LastUpdated"].ToString())
        };
    }

    public static async Task ReactivatePbJobAsync(int jobId)
    {
        var sql = $@"
        UPDATE PBJOB
        SET IsActive = 1,
            LastUpdated = strftime('%Y-%m-%d %H:%M:%f','now','localtime')
        WHERE Id = {jobId};
    ";

        await ExecuteAsync(sql);
    }

    public static async Task ReactivatePbJobAndRenameAsync(int jobId, string newName)
    {
        var sql = $@"
    UPDATE PBJOB
    SET IsActive = 1,
        LastUpdated = strftime('%Y-%m-%d %H:%M:%f','now','localtime'),
        JobName = '{newName}'
    WHERE Id = {jobId};
    ";
        Utils.WriteUnexpectedError($"Renaming JobId={jobId} to '{newName}'");
        await ExecuteAsync(sql);
    }
    public static async Task ShipPalletsAsync(IEnumerable<int> jobIds)
    {
        if (jobIds == null || !jobIds.Any())
            return;

        var idList = string.Join(",", jobIds);
        var shippedAt = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

        string sql = $@"
UPDATE PALLET
SET 
    ShippedAt = '{shippedAt}',
    State = {(int)PalletState.Shipped},
    JobNameSnapshot = (
        SELECT JobName 
        FROM PBJOB 
        WHERE PBJOB.Id = PALLET.PBJobId
    )
WHERE PBJobId IN ({idList})
AND State = {(int)PalletState.Ready}
AND ShippedAt IS NULL;

UPDATE PBJOB
SET LastUpdated = strftime('%Y-%m-%d %H:%M:%f','now','localtime')
WHERE Id IN ({idList});
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

UPDATE {TablePalletWorkOrders}
SET PalletId = {targetPalletId}
WHERE PalletId IN ({idList});

DELETE FROM {TablePallets}
WHERE Id IN ({idList});

UPDATE {TablePallets}
SET State = {(int)PalletState.NotReady}
WHERE Id = {targetPalletId};

UPDATE {TableJobs}
SET LastUpdated = strftime('%Y-%m-%d %H:%M:%f','now','localtime')
WHERE Id = (
    SELECT PBJobId
    FROM {TablePallets}
    WHERE Id = {targetPalletId}
);
";

        Debug.WriteLine("=== MERGE PALLETS SQL ===");
        Debug.WriteLine(sql);

        var result = await ExecuteAsync(sql);

        Debug.WriteLine($"Rows affected: {result.RowsAffected}");
    }

    public static async Task<int?> GetActivePalletIdAsync(int jobId)
    {
        string sql = $@"
SELECT Id
FROM {TablePallets}
WHERE PBJobId = {jobId}
AND State = {(int)PalletState.NotReady}
AND PackedAt IS NULL
LIMIT 1;
";

        var result = await ExecuteScalarAsync(sql);

        if (result == null)
            return null;

        return Convert.ToInt32(result);
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
    j.IsActive,
    j.LastUpdated,

    p.Id AS PalletId,
    p.PalletNumber,
    p.PackedAt,
    p.ShippedAt,
    p.State,
    p.JobNameSnapshot,
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
                    IsActive = Convert.ToInt32(row["IsActive"]) == 1,
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
                    WorkOrders = new List<WorkOrder>(),

                    JobNameSnapshot = row["JobNameSnapshot"]?.ToString()

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

            Utils.WriteUnexpectedError(
                $"Rqlite connection test failed | Address={address}, TimeoutMs={timeoutMs}");
            Utils.WriteExceptionError(ex);

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
        var existing = await QueryAsync($@"
SELECT Id
FROM {TablePallets}
WHERE PBJobId = {jobId}
AND PackedAt IS NULL
ORDER BY Id DESC
LIMIT 1
");

        if (existing.Records.Count > 0)
            return Convert.ToInt32(existing.Records[0]["Id"]);

        await ExecuteAsync($@"
INSERT INTO {TablePallets}
(PBJobId, PalletNumber, State, TrayCount)
VALUES ({jobId}, 0, {(int)PalletState.NotReady}, 0);

UPDATE {TablePallets}
SET PalletNumber = last_insert_rowid()
WHERE Id = last_insert_rowid();

UPDATE {TableJobs}
SET LastUpdated = strftime('%Y-%m-%d %H:%M:%f','now','localtime')
WHERE Id = {jobId};
");

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

        return Convert.ToInt32(inserted.Records[0]["Id"]);
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
        j.IsActive,
        j.LastUpdated,

        p.Id AS PalletId,
        p.PalletNumber,
        p.PackedAt,
        p.ShippedAt,
        p.State,
        p.JobNameSnapshot,
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
                var snapshotName = row["JobNameSnapshot"]?.ToString();

                job = new PbJobModel
                {
                    JobId = jobId,
                    JobName = row["JobName"]?.ToString(),
                    JobNumber = Convert.ToInt32(row["JobNumber"]),
                    IsTemp = Convert.ToInt32(row["IsTemp"]) == 1,
                    IsActive = Convert.ToInt32(row["IsActive"]) == 1,
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
    public static async Task SoftDeletePbJobAsync(int jobId)
    {
        var sql = $@"
        UPDATE PBJOB
        SET IsActive = 0,
            LastUpdated = strftime('%Y-%m-%d %H:%M:%f','now','localtime')
        WHERE Id = {jobId};
    ";

        await ExecuteAsync(sql);
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
    SET LastUpdated = strftime('%Y-%m-%d %H:%M:%f','now','localtime')
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
SET LastUpdated = strftime('%Y-%m-%d %H:%M:%f','now','localtime')
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
            using (var cts = new CancellationTokenSource(3000))
            {
                var response = await httpClient.GetAsync("/status", cts.Token);
                return response.IsSuccessStatusCode;
            }
        }
        catch (TaskCanceledException ex)
        {
            // ✅ Timeout-specific (useful signal)
            Utils.WriteUnexpectedError("Rqlite availability check timeout (3s)");
            Utils.WriteExceptionError(ex);

            return false;
        }
        catch (Exception ex)
        {
            // ⚠️ Only log meaningful failures
            Utils.WriteUnexpectedError("Rqlite availability check failed");
            Utils.WriteExceptionError(ex);

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
        public int rows_affected { get; set;     }
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
