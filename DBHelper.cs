using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        public RqliteResult()
        {
            Records = new List<Dictionary<string, object>>();
        }
    }

    // ======================
    // CORE HELPERS
    // ======================
    private static string FormatValue(object value)
    {
        if (value == null) return "NULL";
        if (value is string) return "'" + value.ToString().Replace("'", "''") + "'";
        if (value is bool) return ((bool)value) ? "1" : "0";
        if (value is DateTime)
            return "'" + ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss") + "'";
        return value.ToString();
    }

    private static async Task<RqliteWriteResult> ExecuteAsync(string sql)
    {
        var json = JsonConvert.SerializeObject(new string[] { sql });
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
            RowsAffected = result != null && result.results != null
                ? result.results.Sum(r => r.rows_affected)
                : 0
        };
    }

    public static Task<RqliteWriteResult> CreateTableAsync(
        string table,
        Dictionary<string, string> columns)
    {
        var cols = string.Join(", ", columns.Select(
            c => "\"" + c.Key + "\" " + c.Value));
        return ExecuteAsync(
            "CREATE TABLE IF NOT EXISTS \"" + table + "\" (" + cols + ")"
        );
    }

    public static Task<RqliteWriteResult> InsertAsync(
        string table,
        Dictionary<string, object> data)
    {
        var cols = string.Join(", ", data.Keys.Select(k => "\"" + k + "\""));
        var vals = string.Join(", ", data.Values.Select(FormatValue));
        return ExecuteAsync(
            "INSERT INTO \"" + table + "\" (" + cols + ") VALUES (" + vals + ")"
        );
    }

    public static async Task<RqliteResult> SelectAsync(string table, string where)
    {
        var sql = "SELECT * FROM \"" + table + "\" " + where;
        var json = JsonConvert.SerializeObject(new string[] { sql });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(
            DefaultEndPoint + "/db/query",
            content
        );

        var body = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<QueryResponse>(body);

        var rows = new List<Dictionary<string, object>>();

        if (result != null && result.results != null && result.results.Count > 0)
        {
            var cols = result.results[0].columns;
            foreach (var row in result.results[0].values)
            {
                var dict = new Dictionary<string, object>();
                for (int i = 0; i < cols.Length; i++)
                    dict[cols[i]] = row[i];
                rows.Add(dict);
            }
        }

        return new RqliteResult { Success = true, Records = rows };
    }

    // ======================
    // SCHEMA CREATION
    // ======================
    public static async Task CreatePbSchemaAsync()
    {
        await CreateTableAsync(TableJobs, new Dictionary<string, string>
        {
            { "JobId", "INTEGER PRIMARY KEY AUTOINCREMENT" },
            { "JobName", "VARCHAR(100) NOT NULL" },
            { "JobNumber", "INTEGER NOT NULL" },
            { "PalletCount", "INTEGER NOT NULL" },
            { "isReady", "INTEGER NOT NULL" }
        });

        await CreateTableAsync(TablePallets, new Dictionary<string, string>
        {
            { "PalletId", "INTEGER PRIMARY KEY AUTOINCREMENT" },
            { "JobId", "INTEGER NOT NULL" },
            { "PalletNumber", "INTEGER NOT NULL" },
            { "PackedTime", "DATETIME NOT NULL" },
            { "TrayCount", "INTEGER NOT NULL" },
            { "ScannedWo", "INTEGER NOT NULL" }
        });

        await CreateTableAsync(TablePalletWorkOrders, new Dictionary<string, string>
        {
            { "PalletWorkOrderId", "INTEGER PRIMARY KEY AUTOINCREMENT" },
            { "PalletId", "INTEGER NOT NULL" },
            { "Code", "VARCHAR(50) NOT NULL" },
            { "Quantity", "INTEGER NOT NULL" }
        });
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
