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
    internal class INIClass
    {
        const string crlf = "\r\n";
        const string quote = "\"";
        internal string _iniFileName { get; set; }
        internal string _logFileDir { get; set; }
        internal string _logFileName { get; set; }
        internal string _outputDir { get; set; }
        internal string _outputFileName { get; set; }
        internal string _defaultPrinter { get; set; }
        internal string _printerIP { get; set; }
        internal string _printerPort { get; set; }
        internal int _appRefresh { get; set; }
        internal int _fontSize { get; set; }
        internal string _fontName { get; set; }
        internal bool _SkipFormAutoPrint { get; set; }


        internal INIClass(string iniFileName)
        {
            _iniFileName = iniFileName;
            _logFileDir = string.Empty;
            _logFileName = string.Empty;
            _outputDir = string.Empty;
            _outputFileName = string.Empty;
            _defaultPrinter = string.Empty;
            _printerIP = string.Empty;
            _printerPort = string.Empty;
            _appRefresh = 0;
            _fontSize = 0;
            _fontName = string.Empty;
            _SkipFormAutoPrint = false;
        }

        internal bool BuildNewIni(out string errMsg)
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
                                                    new IniKey(configFile, "logFileDir", @".\logs"),
                                                    new IniKey(configFile, "logFileName", @".\appERR"),
                                                    new IniKey(configFile, "outputDir", @".\OUTPUT"),
                                                    new IniKey(configFile, "outputFileName", @"PRESORTMAILFORM"),
                                                    new IniKey(configFile, "defaultPrinter", @""),
                                                    new IniKey(configFile, "printerIP", @""),
                                                    new IniKey(configFile, "printerPort", @""),
                                                    new IniKey(configFile, "appRefresh", @"30"),
                                                    new IniKey(configFile, "fontSize", @"12"),
                                                    new IniKey(configFile, "fontName", @"Arial"),
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
                IniSection pbSection = configFile.Sections["PB"];

                if (result && appSection != null)
                {
                    _logFileDir = appSection.Keys["logfileDir"].Value.Trim();
                    _logFileName = appSection.Keys["logfileName"].Value.Trim();
                    _outputDir = appSection.Keys["outputDir"].Value.Trim();
                    _outputFileName = appSection.Keys["outputFileName"].Value.Trim();
                    _defaultPrinter = appSection.Keys["defaultPrinter"].Value.Trim();
                    _printerIP = appSection.Keys["printerIP"].Value.Trim();
                    _printerPort = appSection.Keys["printerPort"].Value.Trim();
                    _appRefresh = int.Parse(appSection.Keys["appRefresh"].Value.Trim());
                    _fontSize = int.Parse(appSection.Keys["fontSize"].Value.Trim());
                    _fontName = appSection.Keys["fontName"].Value.Trim();
                    _SkipFormAutoPrint = bool.Parse(appSection.Keys["SkipFormAutoPrint"].Value.Trim());

                    Utils.CheckAndCreateDirectory(_outputDir, "");
                    Utils.logFileDirectory = Utils.CheckAndCreateDirectory(_logFileDir, "");
                    Utils.logFileName = $"{_logFileName}_{DateTime.Now.ToString("MMddyyyy")}.log";
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
                //In case LogFile name is empty
                if (String.IsNullOrWhiteSpace(_logFileName))
                    _logFileName = "appErr";

                if (String.IsNullOrWhiteSpace(_logFileDir))
                    _logFileDir = "logs";

                if (String.IsNullOrEmpty(Utils.logFileDirectory))
                    Utils.logFileDirectory = Utils.CheckAndCreateDirectory(_logFileDir, "");

                if (String.IsNullOrEmpty(Utils.logFileName))
                    Utils.logFileName = $"{_logFileName}_{DateTime.Now.ToString("MMddyyyy")}.log";

                Utils.WriteExceptionError(ex);
            }

            return result;
        }


        internal class UserINI
        {
            const string crlf = "\r\n";
            const string quote = "\"";

            internal string _iniFileName { get; set; }
            internal int numUsers { get; set; }
            internal string userNameBase { get; set; }

            public UserINI(string iniFileName)
            {
                _iniFileName = iniFileName;
                this.numUsers = 0;
                this.userNameBase = "user";
            }
            internal bool BuildNewIni(out string errMsg)
            {
                bool result = true;
                string fileName = _iniFileName;
                errMsg = string.Empty;

                try
                {
                    var configFile = new IniFile(
                                            new IniOptions
                                            {
                                                //EncryptionPassword = "USMail_ADMIN123",
                                                CommentStarter = IniCommentStarter.Hash
                                            });

                    //add users section
                    //#fname|lastname|levelnum|badgeid|entrycode
                    configFile.Sections.Add(
                                        new IniSection(configFile, "USERS",
                                            new IniKey(configFile, "numusers", @"2"),
                                            new IniKey(configFile, "usernamebase", @"user"),
                                            new IniKey(configFile, "user1", @"SuperAdmin|SuperAdmin|1|admin123|admin123"), //Default Admin
                                            new IniKey(configFile, "user2", @"Admin|Admin|3|00000|00000")
                                        ));

                    configFile.Save(this._iniFileName);
                }
                catch (Exception ex)
                {
                    errMsg += ex.Message;
                    result = false;
                }

                return result;
            }

            internal bool GetINIVars(INIClass appINI, out string errMsg)
            {
                bool result = true;
                errMsg = string.Empty;
                string currKey = string.Empty;

                try
                {
                    var configFile = new IniFile(
                        new IniOptions
                        {
                            //EncryptionPassword = "USMail_ADMIN123",
                            CommentStarter = IniCommentStarter.Hash
                        });

                    configFile.Load(this._iniFileName);

                    IniSection userSection = configFile.Sections["USERS"];

                    //
                    //configuration section                
                    //
                    currKey = "numusers";
                    this.numUsers = int.Parse(userSection.Keys["numusers"].Value.Trim());
                    currKey = "usernamebase";
                    this.userNameBase = userSection.Keys["usernamebase"].Value.Trim();
                }
                catch (Exception ex)
                {
                    result = false;
                    errMsg += "Failure on:  " + currKey + crlf + ex.Message.ToString();

                    Utils.logFileDirectory = Utils.CheckAndCreateDirectory(appINI._logFileDir, "");
                    Utils.logFileName = $"{appINI._logFileName}_{DateTime.Now.ToString("MMddyyyy")}.log";
                    Utils.WriteExceptionError(ex);
                }
                return result;
            }

            public bool AddKey(string section, string keyName, string value, out string errMsg)
            {
                errMsg = string.Empty;
                bool result = true;
                try
                {
                    var configFile = new IniFile(
                       new IniOptions
                       {
                           //EncryptionPassword = "USMail_ADMIN123",
                           CommentStarter = IniCommentStarter.Hash
                       });

                    configFile.Load(this._iniFileName);

                    IniSection aSection = configFile.Sections[section];
                    IniKey key = aSection.Keys.Add(keyName, value.ToString());
                    configFile.Save(this._iniFileName);
                }
                catch (Exception ex)
                {
                    result = false;
                    errMsg += ex.Message.ToString();
                }
                return result;
            }

            public bool UpdateKey(string section, string keyName, string value, out string errMsg)
            {
                errMsg = string.Empty;
                bool result = true;
                try
                {
                    var configFile = new IniFile(
                       new IniOptions
                       {
                           //EncryptionPassword = "USMail_ADMIN123",
                           CommentStarter = IniCommentStarter.Hash
                       });

                    configFile.Load(this._iniFileName);
                    configFile.Sections[section].Keys[keyName].Value = value.ToString();
                    configFile.Save(this._iniFileName);
                }
                catch (Exception ex)
                {
                    result = false;
                    errMsg += ex.Message.ToString();
                }
                return result;
            }

            public bool UpdateINI(out string errMsg)
            {
                errMsg = string.Empty;
                bool result = true;
                try
                {
                    var configFile = new IniFile(
                       new IniOptions
                       {
                           //EncryptionPassword = "USMail_ADMIN123",
                           CommentStarter = IniCommentStarter.Hash
                       });
                    configFile.Load(this._iniFileName);

                    //
                    //configuration values
                    //
                    configFile.Sections["users"].Keys["numusers"].Value = this.numUsers.ToString();
                    configFile.Sections["users"].Keys["usernamebase"].Value = this.userNameBase.ToString();
                    configFile.Save(this._iniFileName);
                }
                catch (Exception ex)
                {
                    result = false;
                    errMsg += ex.Message.ToString();
                }
                return result;
            }

            //public User LookUpBadge(string badge, out bool found, out string errMsg)
            //{
            //    errMsg = string.Empty;
            //    User result = new User();
            //    found = false;

            //    try
            //    {
            //        var configFile = new IniFile(
            //          new IniOptions
            //          {
            //              //EncryptionPassword = "USMail_ADMIN123",
            //              CommentStarter = IniCommentStarter.Hash
            //          });

            //        configFile.Load(this._iniFileName);
            //        var users = configFile.Sections["users"].Keys;
            //        //#fname|lastname|levelnum|badgeid|entrycode
            //        foreach (var group in users)
            //        {
            //            string[] values = group.Value.Split('|');

            //            if (values.Length == 5)
            //            {
            //                if (values[3].Trim().ToUpper() == badge.Trim().ToUpper())
            //                {
            //                    result.entryCode = values[4];
            //                    result.userLevel = (UserLevel)int.Parse(values[2].Trim());
            //                    result.fullValueString = group.Value;
            //                    result.firstName = values[0].Trim();
            //                    result.lastName = values[1].Trim();
            //                    result.keyName = group.Name;
            //                    result.badgeID = badge;
            //                    found = true;
            //                    break;
            //                }
            //            }
            //        }
            //        if (!found)
            //        {
            //            errMsg += "No user found.";
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        errMsg += ex.Message.ToString();
            //    }
            //    return result;
            //}

            //public User LookUpID(string keyID, out bool found, out string errMsg)
            //{
            //    errMsg = string.Empty;
            //    User result = new User();
            //    found = false;

            //    try
            //    {
            //        var configFile = new IniFile(
            //          new IniOptions
            //          {
            //              //EncryptionPassword = "USMail_ADMIN123",
            //              CommentStarter = IniCommentStarter.Hash
            //          });

            //        configFile.Load(this._iniFileName);
            //        var users = configFile.Sections["users"].Keys;
            //        //#fname|lastname|levelnum|badgeid|entrycode
            //        foreach (var group in users)
            //        {
            //            if (group.Name.Trim().ToUpper() == keyID.Trim().ToUpper())
            //            {
            //                string[] values = group.Value.Split('|');
            //                if (values.Length == 5)
            //                {
            //                    result.entryCode = values[4];
            //                    result.userLevel = (UserLevel)int.Parse(values[2].Trim());
            //                    result.fullValueString = group.Value;
            //                    result.firstName = values[0].Trim();
            //                    result.lastName = values[1].Trim();
            //                    result.keyName = group.Name;
            //                    result.badgeID = values[3].Trim();
            //                    found = true;
            //                    break;
            //                }
            //            }
            //        }
            //        if (!found)
            //        {
            //            errMsg += "No user found.";
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        errMsg += ex.Message.ToString();
            //    }
            //    return result;
            //}

            //public List<User> GetUsers(out string errMsg)
            //{
            //    errMsg = string.Empty;
            //    List<User> result = new List<User>();
            //    try
            //    {
            //        var configFile = new IniFile(
            //        new IniOptions
            //        {
            //            //EncryptionPassword = "USMail_ADMIN123",
            //            CommentStarter = IniCommentStarter.Hash
            //        });

            //        configFile.Load(this._iniFileName);
            //        var users = configFile.Sections["users"].Keys.ToList();

            //        //var results = users.Where(p => users.Any(l => p.Value.Contains(entryCode))).Select(v => v.Name).ToList();


            //        foreach (var key in users)
            //        {
            //            var userValue = key.Value.Split('|');

            //            if (userValue.Length == 5)//only grabbing keys that have 5 elements
            //            {
            //                User user = new User();
            //                user.entryCode = userValue[4];
            //                user.userLevel = (UserLevel)int.Parse(userValue[2].Trim());
            //                user.fullValueString = user.ToString();
            //                user.firstName = userValue[0].Trim();
            //                user.lastName = userValue[1].Trim();
            //                user.badgeID = userValue[3].Trim();
            //                user.keyName = key.Name.Trim();

            //                result.Add(user);
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        errMsg += ex.Message.ToString();
            //    }
            //    return result;
            //}

            public bool DeleteKey(string section, string keyName, out string errMsg)
            {
                errMsg = string.Empty;
                bool result = true;
                try
                {
                    var configFile = new IniFile(
                       new IniOptions
                       {
                           //EncryptionPassword = "USMail_ADMIN123",
                           CommentStarter = IniCommentStarter.Hash
                       });

                    configFile.Load(this._iniFileName);
                    IniSection aSection = configFile.Sections[section];
                    aSection.Keys.Remove(keyName);
                    configFile.Save(this._iniFileName);
                }
                catch (Exception ex)
                {
                    result = false;
                    errMsg += ex.Message.ToString();
                }
                return result;
            }

            public bool DecryptINIFile(out string errMsg)
            {
                string iniFile = _iniFileName;
                bool result = true;
                errMsg = string.Empty;

                try
                {
                    var configFile = new IniFile(
                        new IniOptions
                        {
                            //EncryptionPassword = "USMail_ADMIN123",
                            CommentStarter = IniCommentStarter.Hash
                        });

                    configFile.Load(this._iniFileName);

                    // Write plain (decrypted) INI file.
                    IniFile file2 = new IniFile();

                    foreach (IniSection sec1 in configFile.Sections)
                        file2.Sections.Add(sec1.Copy(file2));

                    string baseName = Path.GetFileNameWithoutExtension(iniFile);
                    string path = Path.GetDirectoryName(iniFile);
                    string newName = path + "\\" + baseName + "_decrypt.ini";
                    file2.Save(newName);
                }
                catch (Exception ex)
                {
                    errMsg += ex.Message.ToString();
                }
                return result;
            }
        }

       
    }
}

