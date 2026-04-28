using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PitneyBowesCalculator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            return "'" + dt.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ") + "'";
        return value.ToString();
    }

    public static DateTime? ParseUtc(string value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        return DateTimeOffset
            .Parse(value)
            .ToLocalTime()
            .DateTime;
    }

    //public static async Task<RqliteResult> QueryAsync(string sql)
    //{
    //    int maxRetries = Program.AppINI._rqClientMaxRetries;
    //    int delayMs = Program.AppINI._rqClientDelayMs;
    //    int attempt = 0;

    //    while (true)
    //    {
    //        try
    //        {
    //            var json = JsonConvert.SerializeObject(new[] { sql });
    //            var content = new StringContent(json, Encoding.UTF8, "application/json");

    //            var response = await httpClient.PostAsync(
    //                DefaultEndPoint + "/db/query",
    //                content
    //            );

    //            response.EnsureSuccessStatusCode();

    //            var body = await response.Content.ReadAsStringAsync();

    //            return new RqliteResult
    //            {
    //                Success = true,
    //                Records = ParseQueryResponse(body)
    //            };
    //        }
    //        catch (HttpRequestException ex)
    //        {

    //            attempt++;


    //            Utils.WriteUnexpectedError(
    //                $"Rqlite query retry {attempt}/{maxRetries}"
    //            );


    //            if (attempt >= maxRetries)
    //            {
    //                Utils.WriteUnexpectedError(
    //                    $"Rqlite query failed after {attempt} attempts"
    //                );

    //                Utils.WriteExceptionError(ex);
    //                throw;
    //            }

    //            await Task.Delay(delayMs);
    //        }
    //    }
    //}

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

                var parsed = ParseQueryResponse(body);
                var records = new List<Dictionary<string, object>>();

                if (parsed?.values != null && parsed.columns != null)
                {
                    for (int i = 0; i < parsed.values.Length; i++)
                    {
                        var dict = new Dictionary<string, object>();

                        for (int c = 0; c < parsed.columns.Length; c++)
                        {
                            dict[parsed.columns[c]] = parsed.values[i][c];
                        }

                        records.Add(dict);
                    }
                }

                return new RqliteResult
                {
                    Success = true,
                    Records = records
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

        var settings = new JsonSerializerSettings
        {
            DateParseHandling = DateParseHandling.None
        };

        var result = JsonConvert.DeserializeObject<RootObject>(body, settings);

        var value = result?.results?
            .FirstOrDefault()?
            .values?
            .FirstOrDefault()?
            .FirstOrDefault();

        if (value is JValue jv)
            return jv.Value;

        return value;
    }
    private static string Escape(string value)
    {
        return value?.Replace("'", "''") ?? "";
    }


    public static async Task<RqliteResult> SelectAsync(string table, string where = "")
    {
        var sql = $"SELECT * FROM \"{table}\" {where}";
        return await QueryAsync(sql);
    }

    public static async Task<bool> ValidateAdminAsync(string username, string password)
    {
        try
        {
            var result = await SelectAsync(
                "users",
                $"WHERE Username = '{Escape(username)}' AND PassWord = '{Escape(password)}' LIMIT 1"
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
    public static async Task<List<(int JobId, string LastUpdatedRaw)>> LoadJobUpdateInfoAsync()
    {
        var sql = $"SELECT Id, LastUpdated FROM {TableJobs}";

        // ✅ Use QueryAsync instead of raw PostAsync
        // This goes through ParseQueryResponse which normalizes timestamp format
        var result = await QueryAsync(sql);

        var list = new List<(int, string)>();

        foreach (var row in result.Records)
        {
            int id = Convert.ToInt32(row["Id"]);
            string raw = row["LastUpdated"]?.ToString();
            list.Add((id, raw));
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
        var now = UtcNowString();


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
    '{now}'
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
    LastUpdated = '{now}';
";

        return await ExecuteAsync(sql);
    }

    public static async Task<(int rowsAffected, string newLastUpdated)> UpdatePbJobAsync(
     int jobId,
     string jobName,
     int jobNumber,
     bool isTemp,
     string expectedLastUpdatedRaw)
    {
        try
        {
            var now = UtcNowString();

            // ✅ Use strftime to normalize both sides of the comparison
            // This handles any precision stored in DB regardless of origin
            string whereLastUpdated = string.IsNullOrWhiteSpace(expectedLastUpdatedRaw)
                ? "LastUpdated IS NULL"
                : $"strftime('%Y-%m-%dT%H:%M:%fZ', LastUpdated) = strftime('%Y-%m-%dT%H:%M:%fZ', '{Escape(expectedLastUpdatedRaw)}')";

            string sql = $@"
UPDATE PBJOB
SET
    JobName = {FormatValue(jobName)},
    JobNumber = {jobNumber},
    IsTemp = {(isTemp ? 1 : 0)},
    LastUpdated = '{now}'
WHERE Id = {jobId}
  AND {whereLastUpdated};
";

            var result = await ExecuteAsync(sql);
            return (result.RowsAffected, result.RowsAffected == 1 ? now : null);
        }
        catch (Exception e)
        {
            Utils.WriteExceptionError(e);
            throw;
        }
    }

    // ✅ Fix — capture at top like all other methods
    public static async Task DeleteWorkOrdersAndMaybePalletAsync(
        List<int> workOrderIds,
        int palletId)
    {
        var idList = string.Join(",", workOrderIds);

        var result = await QueryAsync($@"
SELECT PBJobId FROM {TablePallets}
WHERE Id = {palletId}
LIMIT 1;
");

        if (result.Records.Count == 0)
            return;

        int jobId = Convert.ToInt32(result.Records[0]["PBJobId"]);
        var now = UtcNowString(); // ✅ capture once

        string sql = $@"
DELETE FROM {TablePalletWorkOrders}
WHERE Id IN ({idList});

DELETE FROM {TablePallets}
WHERE Id = {palletId}
AND NOT EXISTS (
    SELECT 1 FROM {TablePalletWorkOrders} WHERE PalletId = {palletId}
);

UPDATE {TableJobs}
SET LastUpdated = '{now}'
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


    public static async Task<string> GetJobsLastUpdatedAsync()
    {
        var result = await QueryAsync(
            $"SELECT MAX(LastUpdated) || '' AS LastUpdated FROM {TableJobs}"
        );

        if (result.Records.Count == 0)
            return null;

        var raw = result.Records[0]["LastUpdated"]?.ToString();

       

        return raw;
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

    public static async Task UndoPackedPalletAsync(List<int> palletIds, int jobId)
    {
        if (palletIds == null || palletIds.Count == 0)
            return;

        var ids = string.Join(",", palletIds);
        var now = UtcNowString();

        string sql = $@"
UPDATE {TablePallets}
SET State = {(int)PalletState.NotReady},
    PackedAt = NULL
WHERE PalletNumber IN ({ids})
AND State = {(int)PalletState.Ready};

UPDATE {TableJobs}
SET LastUpdated = '{now}'
WHERE Id = {jobId};
";

        await ExecuteAsync(sql);
    }
    public static async Task<string> TogglePalletReadyAsync(
    int jobId,
    PalletState fromState,
    PalletState toState)
    {
        var now = UtcNowString(); // ✅ capture once

        string sql = $@"
UPDATE {TablePallets}
SET State = {(int)toState}
WHERE PBJobId = {jobId}
AND State = {(int)fromState};

UPDATE {TableJobs}
SET LastUpdated = '{now}'
WHERE Id = {jobId};
";

        await ExecuteAsync(sql);

        // ✅ Return the exact timestamp we wrote — no extra DB round trip needed
        return now;
    }

    public static async Task<int> UpdatePalletPackingAsync(int palletId, int trayCount)
    {
        var now = UtcNowString();

        // 1️⃣ Pack pallet — optimistic lock
        var packResult = await ExecuteAsync($@"
UPDATE {TablePallets}
SET
    TrayCount = {trayCount},
    PackedAt = '{now}',
    State = {(int)PalletState.Ready}
WHERE Id = {palletId}
  AND PackedAt IS NULL
  AND State = {(int)PalletState.NotReady};
");

        // ❌ Already packed by another workstation
        if (packResult.RowsAffected == 0)
            return 0;

        // 2️⃣ Update job timestamp — same 'now'
        await ExecuteAsync($@"
UPDATE {TableJobs}
SET LastUpdated = '{now}'
WHERE Id = (
    SELECT PBJobId FROM {TablePallets} WHERE Id = {palletId}
);
");

        return 1;
    }

    public static string UtcNowString()
    {
        return DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
    }

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
SET LastUpdated = '{UtcNowString()}'
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
    '{UtcNowString()}'
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
        var raw = row["LastUpdated"]?.ToString();

        return new PbJobModel
        {
            JobId = Convert.ToInt32(row["Id"]),
            JobName = row["JobName"]?.ToString(),
            JobNumber = Convert.ToInt32(row["JobNumber"]),
            IsTemp = Convert.ToInt32(row["IsTemp"]) == 1,
            IsActive = Convert.ToInt32(row["IsActive"]) == 1,
            LastUpdatedRaw = raw,              // ✅ comma
            LastUpdated = ParseUtc(raw)        // ✅ last line no comma needed
        };
    }
    public static async Task ReactivatePbJobAsync(int jobId)
    {
        var sql = $@"
UPDATE PBJOB
SET IsActive = 1,
    LastUpdated = '{UtcNowString()}'
WHERE Id = {jobId};
";

        await ExecuteAsync(sql);
    }
    public static async Task ReactivatePbJobAndRenameAsync(int jobId, string newName)
    {
        var sql = $@"
UPDATE PBJOB
SET IsActive = 1,
    LastUpdated = '{UtcNowString()}',
    JobName = '{Escape(newName)}'
WHERE Id = {jobId};
";
        Utils.WriteUnexpectedError($"Renaming JobId={jobId} to '{newName}'");
        await ExecuteAsync(sql);
    }


    //    public static async Task ShipPalletsAsync(IEnumerable<int> jobIds)
    //    {
    //        if (jobIds == null || !jobIds.Any())
    //            return;

    //        var idList = string.Join(",", jobIds);
    //        var now = UtcNowString();

    //        string sql = $@"
    //UPDATE PALLET
    //SET 
    //    ShippedAt = '{now}',
    //    State = {(int)PalletState.Shipped},
    //    JobNameSnapshot = (
    //        SELECT JobName 
    //        FROM PBJOB 
    //        WHERE PBJOB.Id = PALLET.PBJobId
    //    )
    //WHERE PBJobId IN ({idList})
    //AND State = {(int)PalletState.Ready}
    //AND ShippedAt IS NULL;

    //UPDATE PBJOB
    //SET LastUpdated = '{now}'
    //WHERE Id IN ({idList});
    //";

    //        await ExecuteAsync(sql);
    //    }
    public static async Task<int> ShipPalletsAsync(IEnumerable<int> jobIds)
    {
        if (jobIds == null || !jobIds.Any())
            return 0;

        var idList = string.Join(",", jobIds);
        var now = UtcNowString();

        string sql = $@"
UPDATE PALLET
SET 
    ShippedAt = '{now}',
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
SET LastUpdated = '{now}'
WHERE Id IN ({idList});
";

        var result = await ExecuteAsync(sql);
        return result.RowsAffected;
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
SET LastUpdated = '{UtcNowString()}'
WHERE Id = (
    SELECT PBJobId
    FROM {TablePallets}
    WHERE Id = {targetPalletId}
);
";

 

        var result = await ExecuteAsync(sql);

    
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

                var raw = row["LastUpdated"]?.ToString();

                job = new PbJobModel
                {
                    JobId = jobId,
                    JobName = row["JobName"]?.ToString(),
                    JobNumber = Convert.ToInt32(row["JobNumber"]),
                    IsTemp = Convert.ToInt32(row["IsTemp"]) == 1,
                    IsActive = Convert.ToInt32(row["IsActive"]) == 1,
                    LastUpdatedRaw = raw,              
                    LastUpdated = ParseUtc(raw),       

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
                    PackedAt = ParseUtc(row["PackedAt"]?.ToString()),
                    ShippedAt = ParseUtc(row["ShippedAt"]?.ToString()),
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

    public static async Task<int> GetJobsCountAsync()
    {
        var result = await QueryAsync("SELECT COUNT(*) AS Count FROM PBJob");

        return Convert.ToInt32(result.Records[0]["Count"]);
    }

    public static async Task<int> GetOrCreateWorkingPalletAsync(int jobId)
    {
        // Fix #3 — INSERT OR IGNORE prevents duplicate active pallets
        // Requires: CREATE UNIQUE INDEX uq_one_active_pallet ON PALLET(PBJobId)
        // WHERE PackedAt IS NULL AND ShippedAt IS NULL
        await ExecuteAsync($@"
INSERT OR IGNORE INTO {TablePallets}
(PBJobId, PalletNumber, State, TrayCount)
SELECT {jobId}, 0, {(int)PalletState.NotReady}, 0
WHERE NOT EXISTS (
    SELECT 1 FROM {TablePallets}
    WHERE PBJobId = {jobId}
    AND PackedAt IS NULL
);

UPDATE {TablePallets}
SET PalletNumber = Id
WHERE PBJobId = {jobId}
AND PackedAt IS NULL
AND PalletNumber = 0;

UPDATE {TableJobs}
SET LastUpdated = '{UtcNowString()}'
WHERE Id = {jobId};
");

        var result = await QueryAsync($@"
SELECT Id
FROM {TablePallets}
WHERE PBJobId = {jobId}
AND PackedAt IS NULL
ORDER BY Id ASC
LIMIT 1
");

        if (result.Records.Count == 0)
            throw new Exception("Failed to retrieve or create active pallet.");

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
                // ✅ Simple — ParseQueryResponse now normalizes everything
                var raw = row["LastUpdated"]?.ToString();

                job = new PbJobModel
                {
                    JobId = jobId,
                    JobName = row["JobName"]?.ToString(),
                    JobNumber = Convert.ToInt32(row["JobNumber"]),
                    IsTemp = Convert.ToInt32(row["IsTemp"]) == 1,
                    IsActive = Convert.ToInt32(row["IsActive"]) == 1,
                    LastUpdatedRaw = raw,
                    LastUpdated = ParseUtc(raw),
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
                        PackedAt = ParseUtc(row["PackedAt"]?.ToString()),
                        ShippedAt = ParseUtc(row["ShippedAt"]?.ToString()),
                        State = (PalletState)Convert.ToInt32(row["State"]),
                        JobNameSnapshot = row["JobNameSnapshot"]?.ToString(),
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

    private static QueryResult ParseQueryResponse(string json)
    {
        var settings = new JsonSerializerSettings
        {
            DateParseHandling = DateParseHandling.None // 🔥 CRITICAL
        };

        var root = JsonConvert.DeserializeObject<RootObject>(json, settings);

        if (root?.results == null || root.results.Length == 0)
            return new QueryResult();

        var r = root.results[0];

        if (r.values == null || r.columns == null)
            return new QueryResult();

        // 🔥 Normalize ALL values to raw strings (no Date parsing, no formatting)
        for (int i = 0; i < r.values.Length; i++)
        {
            for (int c = 0; c < r.values[i].Length; c++)
            {
                var val = r.values[i][c];

                // preserve EXACT string from DB
                r.values[i][c] = val is JValue jv ? jv.Value : val;
            }
        }

        return new QueryResult
        {
            columns = r.columns,
            values = r.values
        };
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
            LastUpdated = '{UtcNowString()}'
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
    SET LastUpdated = '{UtcNowString()}'
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
SET LastUpdated = '{UtcNowString()}'
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



    private class QueryResult
    {
        public string[] columns { get; set; }
        public object[][] values { get; set; }
    }

    private class RootObject
    {
        public Result[] results { get; set; }
    }

    private class Result
    {
        public string[] columns { get; set; }
        public object[][] values { get; set; }
    }
}
