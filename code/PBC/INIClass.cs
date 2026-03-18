using MadMilkman.Ini;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace WindowsFormsApp1
{
    public class INIClass
    {
        const string crlf = "\r\n";
        const string quote = "\"";
        internal string _iniFileName { get; set; }
        internal string _logFileDir { get; set; }
        internal string _logFileName { get; set; }
        internal string _outputDir { get; set; }
   
        internal string _defaultPrinter { get; set; }
        internal string _printerIP { get; set; }
        internal string _printerPort { get; set; }
        internal int _appRefresh { get; set; }
        internal int _fontSize { get; set; }
        internal string _fontName { get; set; }
        internal bool _SkipFormAutoPrint { get; set; }
        internal string _startUpScreen { get; set; }
        public string StartUpScreen => _startUpScreen;
        internal string _rqClientAddress { get; set; }
        internal int _rqClientMaxRetries { get; set; }
        internal int _rqClientDelayMs { get; set; }

        public INIClass(string iniFileName)
        {
            _iniFileName = iniFileName;
            _logFileDir = string.Empty;
            _logFileName = string.Empty;
            _outputDir = string.Empty;
       
            _defaultPrinter = string.Empty;
            _printerIP = string.Empty;
            _printerPort = string.Empty;
            _appRefresh = 0;
            _fontSize = 0;
            _fontName = string.Empty;
            _SkipFormAutoPrint = false;
        }

        public bool BuildNewIni(out string errMsg)
        {
            string fileName = _iniFileName;
            bool result = true;
            errMsg = string.Empty;

            try
            {
                var configFile = new IniFile(
                                 new IniOptions
                                 {
                                     CommentStarter = IniCommentStarter.Hash
                                 });

                configFile.Sections.Add(
    new IniSection(configFile, "APP",
        new IniKey(configFile, "startUpScreen", ""),
        new IniKey(configFile, "rqClientAddress", ""),
        new IniKey(configFile, "rqClientMaxRetries", ""),
        new IniKey(configFile, "rqClientDelayMs", ""),

        new IniKey(configFile, "logFileDir", ".\\logs"),
        new IniKey(configFile, "logFileName", ".\\appERR"),
        new IniKey(configFile, "outputDir", ".\\OUTPUT"),

        new IniKey(configFile, "defaultPrinter", ""),
        new IniKey(configFile, "printerIP", ""),
        new IniKey(configFile, "printerPort", ""),
        new IniKey(configFile, "appRefresh", ""),
        new IniKey(configFile, "SkipFormAutoPrint", "False")
));



                configFile.Save(fileName);
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
                result = false;
            }

            return result;
        }
        public bool UpdateIni(out string errMsg)
        {
            bool result = true;
            errMsg = string.Empty;

            try
            {
                var configFile = new IniFile(
                    new IniOptions
                    {
                        CommentStarter = IniCommentStarter.Hash
                    });

                configFile.Load(_iniFileName);

                var appSection = configFile.Sections["APP"];
                appSection.Keys["startUpScreen"].Value = _startUpScreen;
                appSection.Keys["rqClientAddress"].Value = _rqClientAddress;
                appSection.Keys["rqClientMaxRetries"].Value = _rqClientMaxRetries.ToString();
                appSection.Keys["rqClientDelayMs"].Value = _rqClientDelayMs.ToString();
                appSection.Keys["logFileDir"].Value = _logFileDir;
                appSection.Keys["logFileName"].Value = _logFileName;
                appSection.Keys["outputDir"].Value = _outputDir;
             
                appSection.Keys["defaultPrinter"].Value = _defaultPrinter;
                appSection.Keys["printerIP"].Value = _printerIP;
                appSection.Keys["printerPort"].Value = _printerPort;
                appSection.Keys["appRefresh"].Value = _appRefresh.ToString();
                appSection.Keys["SkipFormAutoPrint"].Value = _SkipFormAutoPrint.ToString();

                configFile.Save(_iniFileName);
            }
            catch (Exception ex)
            {
                result = false;
                errMsg = ex.Message;
            }

            return result;
        }

        public bool GetINIVars(out string errMsg)
        {
            bool result = true;
            errMsg = string.Empty;

            try
            {
                var configFile = new IniFile(
                    new IniOptions
                    {
                        CommentStarter = IniCommentStarter.Hash
                    });

                configFile.Load(this._iniFileName);
                IniSection appSection = configFile.Sections["APP"];

                if (result && appSection != null)
                {
                    var key = appSection.Keys["startUpScreen"];
                    _startUpScreen = key != null ? key.Value.Trim() : "";
                    _rqClientAddress = appSection.Keys["rqClientAddress"]?.Value?.Trim() ?? "";

                    int.TryParse(appSection.Keys["rqClientMaxRetries"]?.Value, out int retries);
                    _rqClientMaxRetries = retries;

                    int.TryParse(appSection.Keys["rqClientDelayMs"]?.Value, out int delay);
                    _rqClientDelayMs = delay;

                    _logFileDir = appSection.Keys["logFileDir"]?.Value?.Trim() ?? "";
                    _logFileName = appSection.Keys["logFileName"]?.Value?.Trim() ?? "";
                    _outputDir = appSection.Keys["outputDir"]?.Value?.Trim() ?? "";
                    _defaultPrinter = appSection.Keys["defaultPrinter"]?.Value?.Trim() ?? "";
                    _printerIP = appSection.Keys["printerIP"]?.Value?.Trim() ?? "";
                    _printerPort = appSection.Keys["printerPort"]?.Value?.Trim() ?? "";

                    int.TryParse(appSection.Keys["appRefresh"].Value.Trim(), out int refresh);
                    _appRefresh = refresh;

                    bool.TryParse(appSection.Keys["SkipFormAutoPrint"].Value.Trim(), out bool skip);
                    _SkipFormAutoPrint = skip;

                    Utils.CheckAndCreateDirectory(_outputDir, "");
                    Utils.logFileDirectory = Utils.CheckAndCreateDirectory(_logFileDir, "");
                    Utils.logFileName = $"{_logFileName}_{DateTime.Now:MMddyyyy}.log";
                }
                else
                {
                    result = false;
                    errMsg = "Missing [APP] section in the configuration file.";
                }
            }
            catch (Exception ex)
            {
                result = false;
                errMsg += "Unexpected error occurred. Please check the logs and contact Admin support.";

                if (String.IsNullOrWhiteSpace(_logFileName))
                    _logFileName = "appErr";

                if (String.IsNullOrWhiteSpace(_logFileDir))
                    _logFileDir = "logs";

                if (String.IsNullOrEmpty(Utils.logFileDirectory))
                    Utils.logFileDirectory = Utils.CheckAndCreateDirectory(_logFileDir, "");

                if (String.IsNullOrEmpty(Utils.logFileName))
                    Utils.logFileName = $"{_logFileName}_{DateTime.Now:MMddyyyy}.log";

                Utils.WriteExceptionError(ex);
            }

            return result;
        }
        public void SetStartUpScreen(string value)
        {
            _startUpScreen = value;
        }
        public bool UpdateStartUpScreen(out string errMsg)
        {
            errMsg = "";
            try
            {
                var configFile = new IniFile(new IniOptions { CommentStarter = IniCommentStarter.Hash });
                configFile.Load(_iniFileName);

                var appSection = configFile.Sections["APP"];
                appSection.Keys["startUpScreen"].Value = _startUpScreen ?? "";

                configFile.Save(_iniFileName);
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }

    }
}

