using Krypton.Toolkit;
using MadMilkman.Ini;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PitneyBowesCalculator.Properties;

namespace PitneyBowesCalculator
{
    internal static class Utils
    {
        private static readonly string AppKey = "PITNEYBOWESAPPsupersecretkey0123";
        const string crlf = "\r\n";
        const string quote = "\"";
        internal static string logFileDirectory;
        internal static string logFileName;
        private static System.Windows.Forms.Timer _connectionStatusTimer;
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("0123456789abcdef0123456789abcdef"); // 32 bytes for AES-256
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("abcdef9876543210"); // 16 bytes for AES

        /// <summary>
        /// Return the current version of the Tool.
        /// </summary>
        internal static string AssemblyVersion
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
                if (attributes.Length == 0)
                {
                    return String.Empty;
                }
                return ((AssemblyFileVersionAttribute)attributes[0]).Version;
            }
        }

        /// <summary>
        /// Add a text to a log file when an unexpected error appends without log view for the user.
        /// </summary>
        /// <param name="message">Error message</param>
        internal static void WriteUnexpectedError(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string callerPath = null, [CallerMemberName] string caller = null)
        {
            const string tab = "\t";
            if (!String.IsNullOrEmpty(logFileDirectory) && Directory.Exists(logFileDirectory))
            {
                //File.AppendAllText(logFileDirectory + "_" + FormattedDateMMDDYYYY() + ".log",
                //    DateTime.Now.ToString() + tab + message + " " + callerPath + ":line " + lineNumber + " (method:" + caller + ") " + Environment.NewLine);

                string logFullPath = Path.Combine(logFileDirectory, logFileName);
                if (!string.IsNullOrEmpty(message))
                    File.AppendAllText(logFullPath, $"{DateTime.Now.ToString()}{tab}message:{message} - {callerPath}:line {lineNumber} (method:{caller}) " + Environment.NewLine);
            }
        }

        /// <summary>
        /// To pass the exception and append on log file  without log view for the user.
        /// </summary>
        /// <param name="ex">Exception</param>
        internal static void WriteExceptionError(Exception ex)
        {
            const string tab = "\t";
            if (!String.IsNullOrEmpty(logFileDirectory) && Directory.Exists(logFileDirectory))
            {

                string stackTrace = DateTime.Now.ToString() + tab + "[Error] " + ex.StackTrace.ToString().Trim() + Environment.NewLine;
                string message = DateTime.Now.ToString() + tab + "Exception Message: " + ex.Message + Environment.NewLine;

                //File.AppendAllText(logFileDirectory + "_" + FormattedDateMMDDYYYY() + ".log", stackTrace + message);
                string logFullPath = Path.Combine(logFileDirectory, logFileName);
                File.AppendAllText(logFullPath, stackTrace + message);
            }
        }


        internal static string CheckAndCreateDirectory(string fileDirectory, string folderName)
        {
            //updated by nathan
            if (String.IsNullOrEmpty(fileDirectory))
            {
                //if (!String.IsNullOrEmpty(folderName))
                fileDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\logs\"; // + folderName; //give a default log directory.
                //else
                //    fileDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\" + DateTime.Now.ToString("yyyyMMdd"); //give a default log directory.
            }

            //always end the dir name with a \
            if (!fileDirectory.EndsWith(@"\"))
                fileDirectory += @"\";

            if (!String.IsNullOrEmpty(folderName) && !fileDirectory.Contains(folderName))
                fileDirectory += folderName + @"\";

            //try and create if it doesn't exist
            if (!Directory.Exists(fileDirectory))
                Directory.CreateDirectory(fileDirectory);


            //logFileDirectory = fileDirectory;

            return fileDirectory;
        }

        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return "";

            byte[] key = Encoding.UTF8.GetBytes(AppKey);
            byte[] iv = new byte[16];

            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            using var ms = new MemoryStream();

            using (var encryptor = aes.CreateEncryptor())
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            } // writer + crypto stream properly close here

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string encryptedText)
        {
            if (string.IsNullOrWhiteSpace(encryptedText))
                return "";

            try
            {
                byte[] key = Encoding.UTF8.GetBytes(AppKey);
                byte[] iv = new byte[16];
                byte[] buffer = Convert.FromBase64String(encryptedText);

                using var aes = Aes.Create();
                aes.Key = key;
                aes.IV = iv;

                using var decryptor = aes.CreateDecryptor();
                using var ms = new MemoryStream(buffer);
                using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                using var sr = new StreamReader(cs);

                return sr.ReadToEnd();
            }
            catch
            {
                // fallback if value already plain text
                return encryptedText;
            }
        }

        public static async Task<(CpsConfig config, string connectionString)> LoadCPSConfig()
        {
            var config = await RqliteClient.LoadCpsConfigFromDB() ?? GetDefaultConfig();

            config.CpsQuery = ResolveQuery(config.CpsQuery);

            string connStr = BuildConnectionString(config);

            return (config, connStr);
        }

        private static string BuildConnectionString(CpsConfig config)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = config.CpsServer,
                InitialCatalog = config.CpsDb,
                ConnectTimeout = config.ConnTimeOut,
                TrustServerCertificate = config.TrustedServerCert,
                IntegratedSecurity = config.TrustedConn
            };

            if (!config.TrustedConn)
            {
                builder.UserID = config.SqlUser;
                builder.Password = Decrypt(config.SqlPwd);
            }

            return builder.ConnectionString;
        }
        private static string ResolveQuery(string query)
        {
            if (!query.EndsWith(".sql", StringComparison.OrdinalIgnoreCase))
                return query;

            string queryPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "sql_query",
                query
            );

            if (!File.Exists(queryPath))
                throw new FileNotFoundException($"SQL query file not found:\n{queryPath}");

            return File.ReadAllText(queryPath);
        }
        private static CpsConfig GetDefaultConfig()
        {
            return new CpsConfig
            {
                CpsDb = "OCS",
                CpsQuery = "cps_query.sql",
                CpsServer = "USCHSERV36.corp.idemia.com",
                TrustedConn = true,
                TrustedServerCert = true,
                ConnTimeOut = 100,
                SqlUser = "",
                SqlPwd = ""
            };
        }
        public static string AddFilterClause(string sql, string clause)
        {
            // Find position of GROUP BY or ORDER BY (case-insensitive)
            int groupByIndex = sql.IndexOf("GROUP BY", StringComparison.OrdinalIgnoreCase);
            int orderByIndex = sql.IndexOf("ORDER BY", StringComparison.OrdinalIgnoreCase);

            // Find earliest clause (GROUP BY or ORDER BY)
            int insertPos = -1;
            if (groupByIndex >= 0 && orderByIndex >= 0)
                insertPos = Math.Min(groupByIndex, orderByIndex);
            else if (groupByIndex >= 0)
                insertPos = groupByIndex;
            else if (orderByIndex >= 0)
                insertPos = orderByIndex;

            string before, after;
            if (insertPos >= 0)
            {
                before = sql.Substring(0, insertPos);
                after = sql.Substring(insertPos);
            }
            else
            {
                before = sql;
                after = "";
            }

            // Add WHERE or AND
            if (before.IndexOf("WHERE", StringComparison.OrdinalIgnoreCase) >= 0)
                return before.TrimEnd() + " AND " + clause + " " + after;
            else
                return before.TrimEnd() + " WHERE " + clause + " " + after;
        }

  
        public static void GenerateReport(List<PbJobModel> jobs,DateTimePicker dvFrom, DateTimePicker dvTo)
        {
            if (jobs == null || !jobs.Any())
                return;

            // 🔥 Suggested filename using date range
            string fileName = $"PB_Report_{dvFrom.Value:yyyyMMdd}_to_{dvTo.Value:yyyyMMdd}.csv";

            using (var sfd = new SaveFileDialog())
            {
                sfd.Title = "Save Report";
                sfd.Filter = "CSV files (*.csv)|*.csv";
                sfd.FileName = fileName;
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                var lines = new List<string>();

                // Header
                lines.Add("PB Job,Envelope Qty,Trays,Pallets,Ship Date");

                foreach (var job in jobs)
                {
                    lines.Add(string.Join(",",
                        Safe($"{job.JobNumber} {job.JobName}"),
                        job.TotalEnvelopeOfJob,
                        job.TotalTraysOfJob,
                        job.Pallets.Count,
                        job.ShippedDate?.ToString("MM/dd/yyyy hh:mm tt")
                    ));
                }

                // 🔥 Optional totals row
                lines.Add("");
                lines.Add(string.Join(",",
                    "TOTAL",
                    jobs.Sum(j => j.TotalEnvelopeOfJob),
                    jobs.Sum(j => j.TotalTraysOfJob),
                    jobs.Sum(j => j.Pallets.Count),
                    ""
                ));

                File.WriteAllLines(sfd.FileName, lines);

                // Open file after saving
                Process.Start(new ProcessStartInfo
                {
                    FileName = sfd.FileName,
                    UseShellExecute = true
                });
            }
        }

        private static string Safe(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            return $"\"{value.Replace("\"", "\"\"")}\"";
        }
        public static void hideStatusAndSpinner(KryptonLabel lb, PictureBox pb, string stat)
        {
            if (lb.InvokeRequired)
            {
                lb.Invoke(new Action(() => hideStatusAndSpinner(lb, pb, stat)));
                return;
            }

            lb.Text = stat;
            lb.ForeColor = Color.Green;
            lb.Visible = true;

            pb.Visible = true;
            pb.Image = Resources.check_32px;

            // stop old timer if running
            _connectionStatusTimer?.Stop();

            _connectionStatusTimer = new System.Windows.Forms.Timer();
            _connectionStatusTimer.Interval = 2000;

            _connectionStatusTimer.Tick += (s, e) =>
            {
                lb.Visible = false;
                pb.Visible = false;

                _connectionStatusTimer.Stop();
            };

            _connectionStatusTimer.Start();
        }

        public static void errorStatusAndSpinner(KryptonLabel lb, PictureBox pb, string stat)
        {
            if (lb.InvokeRequired)
            {
                lb.Invoke(new Action(() => errorStatusAndSpinner(lb, pb, stat)));
                return;
            }

            lb.Text = stat;
            lb.ForeColor = Color.Green;
            lb.Visible = true;

            pb.Visible = true;
            pb.Image = Image.FromFile("Resources/error1.png");

            // stop old timer if running
            _connectionStatusTimer?.Stop();

            _connectionStatusTimer = new System.Windows.Forms.Timer();
            _connectionStatusTimer.Interval = 2000;

            _connectionStatusTimer.Tick += (s, e) =>
            {
                lb.Visible = false;
                pb.Visible = false;

                _connectionStatusTimer.Stop();
            };

            _connectionStatusTimer.Start();
        }
        public static void showStatusAndSpinner(KryptonLabel lb, PictureBox pb, string stat)
        {
            if (lb.InvokeRequired)
            {
                lb.Invoke(new Action(() => showStatusAndSpinner(lb, pb, stat)));
                return;
            }

            lb.Text = stat;
            lb.ForeColor = Color.Red;
            lb.Visible = true;

            pb.Visible = true;
            pb.Image = Resources.spinner_32px;
        }

        public static void hideNow(KryptonLabel lb, PictureBox pb)
        {
            if (lb.InvokeRequired)
            {
                lb.Invoke(new Action(() => hideNow(lb, pb)));
                return;
            }

            lb.Visible = false;
            pb.Visible = false;

            _connectionStatusTimer?.Stop();
        }


    }



    public enum MessageType
    {
        Info,
        Warning,
        Error,
        Success
    }

    public class MessageDialogBox : Form
    {
    

        private static bool _dialogOpen = false;

        public MessageDialogBox(string title, string message, MessageBoxButtons messageButton, MessageType messageType)
        {
            Color color = Color.White;
            //byte[] icon = null;
            System.Drawing.Image icon = null;
            DialogResult dialogResult = DialogResult.OK;
            _dialogOpen = true;

            switch (messageType)
            {
                case MessageType.Info:
                    color = Color.FromArgb(133, 102, 193);
                    icon = Image.FromFile("Resources/info-50.png");
                    break;
                case MessageType.Warning:
                    color = Color.FromArgb(225, 173, 1);
                    icon = Image.FromFile("Resources/warning-50.png");
                    break;
                case MessageType.Error:
                    color = Color.FromArgb(228, 14, 15);
                    icon = Image.FromFile("Resources/error1.png");
                    break;
                case MessageType.Success:
                    color = Color.FromArgb(67, 108, 20);
                    icon = Image.FromFile("Resources/success-50.img");
                    break;
                default:
                    break;
            }

            this.Text = title;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.Padding = new Padding(20);

            // Main vertical layout (Icon + Message, then OK button)
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

            // Horizontal layout for icon + message
            var contentLayout = new TableLayoutPanel
            {
                ColumnCount = 2,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };
            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            // Action button layout using FlowLayoutPanel (for tight horizontal alignment)
            var buttonPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 5, 5, 5), // Padding to give bottom space
                Margin = new Padding(0),
                Anchor = AnchorStyles.Right,
                WrapContents = false
            };

            // Icon (centered vertically using anchor)
            PictureBox pb = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                //Image = ByteToImage(Properties.Resources.success),
                Image = icon, //ByteToImage(icon),
                Size = new Size(50, 50),
                Anchor = AnchorStyles.Top,
                Margin = new Padding(10)
            };
            contentLayout.Controls.Add(pb, 0, 0);

            // Message label
            Label lblMessage = new Label
            {
                Text = message,
                AutoSize = true,
                MaximumSize = new Size(400, 0),
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Montserrat", 11, FontStyle.Bold),
                ForeColor = Color.Black,
                Margin = new Padding(10, 10, 10, 10)
            };
            contentLayout.Controls.Add(lblMessage, 1, 0);

            // Shared button style
            Font buttonFont = new Font("Montserrat", 12, FontStyle.Regular);
            Padding buttonMargin = new Padding(2); // spacing between buttons

            // OK Button
            System.Windows.Forms.Button btnOk = new System.Windows.Forms.Button
            {
                Text = "OK",
                Size = new Size(100, 35),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(24, 25, 29),
                //FlatStyle = FlatStyle.Flat,
                Font = buttonFont,
                Margin = buttonMargin,
                Visible = messageButton == MessageBoxButtons.OK || messageButton == MessageBoxButtons.OKCancel ||
                          messageButton == MessageBoxButtons.YesNo || messageButton == MessageBoxButtons.YesNoCancel,
                DialogResult = DialogResult.OK
            };

            //Override default Text and DialogResult
            if (messageButton == MessageBoxButtons.YesNo || messageButton == MessageBoxButtons.YesNoCancel)
            {
                btnOk.Text = "YES";
                btnOk.DialogResult = DialogResult.Yes;
            }
            btnOk.FlatAppearance.BorderSize = 1;
            btnOk.FlatAppearance.BorderColor = Color.White; // Color.FromArgb(53, 87, 16);
            btnOk.Click += (sender, e) => this.Close();

            System.Windows.Forms.Button btnCancel = new System.Windows.Forms.Button
            {
                Text = "Cancel",
                Size = new Size(100, 35),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(24, 25, 29),
                //FlatStyle = FlatStyle.Flat,
                Font = buttonFont,
                Margin = buttonMargin,
                Visible = messageButton == MessageBoxButtons.OKCancel || messageButton == MessageBoxButtons.YesNo ||
                          messageButton == MessageBoxButtons.YesNoCancel,
                DialogResult = DialogResult.Cancel
            };
            //Override default Text and DialogResult
            if (messageButton == MessageBoxButtons.YesNo || messageButton == MessageBoxButtons.YesNoCancel)
            {
                btnCancel.Text = "No";
                btnCancel.DialogResult = DialogResult.No;
            }
            btnCancel.FlatAppearance.BorderSize = 1;
            btnCancel.FlatAppearance.BorderColor = Color.White; // Color.FromArgb(53, 87, 16);
            btnCancel.Click += (sender, e) => this.Close();

            System.Windows.Forms.Button btnContinue = new System.Windows.Forms.Button
            {
                Text = messageButton == MessageBoxButtons.YesNoCancel ? "Cancel" : "Retry",
                Size = new Size(120, 35),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(24, 25, 29),
                //FlatStyle = FlatStyle.Flat,
                Font = buttonFont,
                Margin = buttonMargin,
                DialogResult = messageButton == MessageBoxButtons.YesNoCancel ? DialogResult.Cancel : DialogResult.Retry,
                Visible = messageButton == MessageBoxButtons.AbortRetryIgnore || messageButton == MessageBoxButtons.YesNoCancel
            };
            btnContinue.FlatAppearance.BorderSize = 1;
            btnContinue.FlatAppearance.BorderColor = Color.White;
            btnContinue.Click += (sender, e) => this.Close();

            // Add buttons to flow panel
            buttonPanel.Controls.Add(btnContinue);
            buttonPanel.Controls.Add(btnCancel);
            buttonPanel.Controls.Add(btnOk);

            // Add controls
            mainLayout.Controls.Add(contentLayout, 0, 0);
            mainLayout.Controls.Add(buttonPanel, 0, 1);
            this.Controls.Add(mainLayout);
            this.Controls.Add(mainLayout);

            // 🔥 ADD THIS BLOCK HERE
            var owner = Form.ActiveForm;

        
        }

        public static DialogResult ShowDialog(string title, string message, MessageBoxButtons messageButton, MessageType messageType)
        {
            if (_dialogOpen) return DialogResult.OK; // Don’t open new if one is already up

            try
            {
                using (var dialog = new MessageDialogBox(title, message, messageButton, messageType))
                {
                    return dialog.ShowDialog();
                }
            }
            finally
            {
                _dialogOpen = false; // Reset after close
            }
        }
    }
}


