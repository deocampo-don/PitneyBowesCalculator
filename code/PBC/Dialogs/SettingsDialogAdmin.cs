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
using PitneyBowesCalculator.Dialogs;
using System.Security.Cryptography;
using PitneyBowesCalculator;

namespace PitneyBowesCalculator
{
    public partial class SettingsDialogAdmin : Form
    {

        private INIClass appINI;
        public static event Action OnCpsConfigUpdated;
        public SettingsDialogAdmin()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this);
            ShadowHelper.ApplyShadow(this);

        }

        private async void SettingsDialog_Load(object sender, EventArgs e)
        {
            try
            {
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
                else
                {
                    MessageDialogBox.ShowDialog(
                        "Database Offline",
                        "Could not load settings — database is unreachable.\n\nYou can still update the connection settings and save.",
                        MessageBoxButtons.OK,
                        MessageType.Warning);
                }

                tglAllowDupli.Checked = RqliteClient.AllowDuplicateBarcodes;
            }
            catch (Exception ex)
            {
                MessageDialogBox.ShowDialog(
                    "Load Error",
                    "An error occurred while loading settings:\n\n" + ex.Message,
                    MessageBoxButtons.OK,
                    MessageType.Error);
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

            }
            sqlPassword = Utils.Encrypt(tbSqlPwd.Text.Trim());

            if (missingFields.Count > 0)
            {


                MessageDialogBox.ShowDialog(
                    "Validation",
                    "Please fix the following fields:\n\n" + string.Join("\n", missingFields),
                    MessageBoxButtons.OK,
                    MessageType.Warning
                );
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

                MessageDialogBox.ShowDialog(
                    "Connection Error",
                    "SQL connection failed:\n\n" + test.Error,
                    MessageBoxButtons.OK,
                    MessageType.Error);

                lbStatus.Visible = false;
                pbSpinner.Visible = false;



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

                MessageDialogBox.ShowDialog("", "Settings saved.", MessageBoxButtons.OK, MessageType.Info);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {

                MessageDialogBox.ShowDialog("", result.ErrorMessage, MessageBoxButtons.OK, MessageType.Error);
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

                    MessageDialogBox.ShowDialog("", "User Added.", MessageBoxButtons.OK, MessageType.Info);

                    using (var frm = new SettingsDialogAdmin())
                    {
                        frm.ShowDialog();
                    }
                }
            }
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tglAllowDupli_CheckedChanged(object sender, EventArgs e)
        {
            
                RqliteClient.AllowDuplicateBarcodes =tglAllowDupli.Checked;
         
        }
    }
}
