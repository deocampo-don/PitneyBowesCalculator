using MadMilkman.Ini;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace YourApp.Utils
{
    public class SystemConfig
    {
        public string StartUpScreen { get; set; }
        public string RqClientAddress { get; set; }
        public int RqClientMaxRetries { get; set; }
        public int RqClientDelayMs { get; set; }
        public string LogFileDir { get; set; }
        public string LogFileName { get; set; }
        public string OutputDir { get; set; }
        public int PollTimeMs { get; set; }
        public string DefaultPrinter { get; set; }
        public string PrinterIP { get; set; }
        public int? PrinterPort { get; set; }
        public bool SkipFormAutoPrint { get; set; }
    }

    public static class ConfigUtils
    {
        private static readonly string FilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.ini");

        private const string SectionName = "SYSTEM";

        public static SystemConfig Current { get; private set; }

        public static SystemConfig Load()
        {
            if (!File.Exists(FilePath))
            {
                Current = CreateDefault();
                Save(Current);
                return Current;
            }

            var ini = new IniFile();
            ini.Load(FilePath);

            var section = ini.Sections[SectionName];

            if (section == null)
            {
                Current = CreateDefault();
                Save(Current);
                return Current;
            }

            Current = new SystemConfig
            {
                StartUpScreen = GetValue(section, "startUPScreen"),
                RqClientAddress = DecryptSafe(GetValue(section, "rqClientAddress")),
                RqClientMaxRetries = ParseInt(GetValue(section, "rqClientMaxRetries"), 3),
                RqClientDelayMs = ParseInt(GetValue(section, "rqClientDelayMs"), 1000),
                LogFileDir = GetValue(section, "logfileDir"),
                LogFileName = GetValue(section, "logfileName"),
                OutputDir = GetValue(section, "outputDir"),
                PollTimeMs = ParseInt(GetValue(section, "polltimems"), 10000),
                DefaultPrinter = GetValue(section, "defaultPrinter"),
                PrinterIP = GetValue(section, "printerIP"),
                PrinterPort = ParseNullableInt(GetValue(section, "printerPort")),
                SkipFormAutoPrint = ParseBool(GetValue(section, "SkipFormAutoPrint"), false)
            };

            return Current;
        }

        public static void Save(SystemConfig config)
        {
            var ini = new IniFile();

            if (File.Exists(FilePath))
                ini.Load(FilePath);

            var section = ini.Sections[SectionName] ?? ini.Sections.Add(SectionName);

            SetValue(section, "startUPScreen", config.StartUpScreen);
            SetValue(section, "rqClientAddress", Encrypt(config.RqClientAddress));
            SetValue(section, "rqClientMaxRetries", config.RqClientMaxRetries.ToString());
            SetValue(section, "rqClientDelayMs", config.RqClientDelayMs.ToString());
            SetValue(section, "logfileDir", config.LogFileDir);
            SetValue(section, "logfileName", config.LogFileName);
            SetValue(section, "outputDir", config.OutputDir);
            SetValue(section, "polltimems", config.PollTimeMs.ToString());
            SetValue(section, "defaultPrinter", config.DefaultPrinter ?? "");
            SetValue(section, "printerIP", config.PrinterIP ?? "");
            SetValue(section, "printerPort", config.PrinterPort?.ToString() ?? "");
            SetValue(section, "SkipFormAutoPrint", config.SkipFormAutoPrint.ToString());

            ini.Save(FilePath);

            Current = config;
        }

        public static bool Exists() => File.Exists(FilePath);

        // ==============================
        // Helpers
        // ==============================

        private static string GetValue(IniSection section, string key)
        {
            return section.Keys[key]?.Value ?? "";
        }

        private static void SetValue(IniSection section, string key, string value)
        {
            if (section.Keys[key] == null)
                section.Keys.Add(key, value ?? "");
            else
                section.Keys[key].Value = value ?? "";
        }

        private static int ParseInt(string value, int defaultValue)
        {
            return int.TryParse(value, out var result) ? result : defaultValue;
        }

        private static int? ParseNullableInt(string value)
        {
            if (int.TryParse(value, out var result))
                return result;

            return (int?)null;
        }

        private static bool ParseBool(string value, bool defaultValue)
        {
            return bool.TryParse(value, out var result) ? result : defaultValue;
        }

        // ==============================
        // Default Config
        // ==============================

        private static SystemConfig CreateDefault()
        {
            return new SystemConfig
            {
                StartUpScreen = "US Mail",
                RqClientAddress = "https://10.32.101.160:4004",
                RqClientMaxRetries = 3,
                RqClientDelayMs = 1000,
                LogFileDir = @".\logs\",
                LogFileName = "pbErr",
                OutputDir = @".\out\",
                PollTimeMs = 10000,
                DefaultPrinter = "",
                PrinterIP = "",
                PrinterPort = null,
                SkipFormAutoPrint = false
            };
        }

        // ==============================
        // Encryption
        // ==============================

        private static readonly string Key = "YourSuperSecretKey123!";

        private static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return "";

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(Key.PadRight(32));
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();

            ms.Write(aes.IV, 0, aes.IV.Length);

            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
                sw.Write(plainText);

            return Convert.ToBase64String(ms.ToArray());
        }

        private static string DecryptSafe(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return "";

            try
            {
                var buffer = Convert.FromBase64String(cipherText);

                using var aes = Aes.Create();
                aes.Key = Encoding.UTF8.GetBytes(Key.PadRight(32));

                var iv = new byte[16];
                Array.Copy(buffer, iv, iv.Length);
                aes.IV = iv;

                using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using var ms = new MemoryStream(buffer, 16, buffer.Length - 16);
                using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                using var sr = new StreamReader(cs);

                return sr.ReadToEnd();
            }
            catch
            {
                // fallback if value isn't encrypted
                return cipherText;
            }
        }
    }
}