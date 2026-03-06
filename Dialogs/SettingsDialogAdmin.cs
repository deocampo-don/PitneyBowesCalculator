using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using WindowsFormsApp1.Dialogs;
using System.Security.Cryptography;

namespace WindowsFormsApp1
{
    public partial class SettingsDialogAdmin : Form
    {

        private INIClass appINI;
        public static event Action OnCpsConfigUpdated;
        public SettingsDialogAdmin()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
            //this.TransparencyKey = Color.White;
        }

        private async void SettingsDialog_Load(object sender, EventArgs e)
        {
            //tbDefPrinter.Text = appINI._defaultPrinter;
            //tbDirectory.Text = appINI._outputDir;

            // CPS (DATABASE)
            var cps = await RqliteClient.LoadCpsSettingsAsync();
            if (cps != null)
            {
                tbCpsServer.Text = cps.CPSServer;
                tbCpsDb.Text = cps.CPSDatabase;
                tbCpsQuery.Text = cps.CPSQuery;
                tbConnTimeOut.Text = cps.ConnectionTimeout.ToString();
                tglTrustedConnection.Checked = cps.TrustedConnection;
                tglTrustedServerCert.Checked = cps.TrustedServerCertificate;
                tbSqlUser.Text = cps.SqlUser;
                tbSqlPwd.Text = Utils.Decrypt(cps.SqlPassword);
            }
         
        }

     
        private void btnSettingsCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSettingsSave_Click(object sender, EventArgs e)
        {
            List<string> missingFields = new List<string>();

            ValidateTextbox(tbCpsServer);
            ValidateTextbox(tbCpsDb);
            ValidateTextbox(tbCpsQuery);

            if (string.IsNullOrWhiteSpace(tbCpsServer.Text))
                missingFields.Add("CPS Server");

            if (string.IsNullOrWhiteSpace(tbCpsDb.Text))
                missingFields.Add("CPS Database");

            if (string.IsNullOrWhiteSpace(tbCpsQuery.Text))
                missingFields.Add("CPS Query");

            int timeout;
            if (!int.TryParse(tbConnTimeOut.Text.Trim(), out timeout))
                missingFields.Add("Connection Timeout (must be a number)");

            string sqlUser = "";
            string sqlPassword = "";

            if (!tglTrustedConnection.Checked)
            {
                ValidateTextbox(tbSqlUser);
                ValidateTextbox(tbSqlPwd);

                if (string.IsNullOrWhiteSpace(tbSqlUser.Text))
                    missingFields.Add("SQL User");

                if (string.IsNullOrWhiteSpace(tbSqlPwd.Text))
                    missingFields.Add("SQL Password");

                sqlUser = tbSqlUser.Text.Trim();

                // 🔐 Encrypt password before saving
                sqlPassword = Utils.Encrypt(tbSqlPwd.Text.Trim());
            }

            if (missingFields.Count > 0)
            {
                MessageBox.Show(
                    "Please fix the following fields:\n\n" + string.Join("\n", missingFields),
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            Utils.showStatusAndSpinner(lbStatus, pbSpinner, "Testing SQL connection...");
          

            var test = await RqliteClient.TestSqlConnectionAsync(
                tbCpsServer.Text.Trim(),
                tbCpsDb.Text.Trim(),
                timeout,
                tglTrustedConnection.Checked,
                tglTrustedServerCert.Checked,
                sqlUser,
                tbSqlPwd.Text.Trim() // use real password here
            );



            if (!test.Success)
            {
                MessageBox.Show(
                    "SQL connection failed:\n\n" + test.Error,
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                lbStatus.Visible = false;
                pbSpinner.Visible= false;

                return;
            }


            Utils.hideStatusAndSpinner(lbStatus, pbSpinner, "Connected");


            var result = await RqliteClient.SaveSettingsAsync(
                tbCpsServer.Text.Trim(),
                tbCpsDb.Text.Trim(),
                tbCpsQuery.Text.Trim(),
                timeout,
                tglTrustedConnection.Checked,
                tglTrustedServerCert.Checked,
                sqlUser,
                sqlPassword
            );

            if (result.Success)
            {
                MessageBox.Show("Settings saved.");
                this.Close();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
            }
        }

        private void ValidateTextbox(KryptonTextBox tb)
        {
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.StateCommon.Border.Color1 = Color.Red;
                tb.StateCommon.Border.Color2 = Color.Red;
            }
            else
            {
                tb.StateCommon.Border.Color1 = Color.Gray;
                tb.StateCommon.Border.Color2 = Color.Gray;
            }
        }


        private void tglTrustedConnection_CheckedChanged(object sender, EventArgs e)
        {
            if (!tglTrustedConnection.Checked)
            {
                lbSqlPwd.Visible = true;
                lbSqlUser.Visible = true;
                tbSqlPwd.Visible = true;
                tbSqlUser.Visible = true;
            }
            else
            {
                lbSqlPwd.Visible = false;
                lbSqlUser.Visible = false;
                tbSqlPwd.Visible = false;
                tbSqlUser.Visible = false;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            using (var login = new LoginDialog("AddUser"))
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("User Added.");

                    using (var frm = new SettingsDialogAdmin())
                    {
                        frm.ShowDialog();
                    }
                }
            }
        }


    }
}
