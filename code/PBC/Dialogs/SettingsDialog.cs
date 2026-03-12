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

namespace WindowsFormsApp1.Dialogs
{
    public partial class SettingsDialog : Form
    {

        public bool SettingsChanged { get; private set; }

        public SettingsDialog()
        {
            InitializeComponent();

            tbDefPrint.Text = Program.AppINI._defaultPrinter;
            tbPrinterIP.Text = Program.AppINI._printerIP;
            tbPrinterPort.Text = Program.AppINI._printerPort;
            tbAppRefFreq.Text = Program.AppINI._appRefresh.ToString();

            // NEW FIELDS
            tbRqClientAdd.Text = Program.AppINI._rqClientAddress;
            tbClientMaxRetry.Text = Program.AppINI._rqClientMaxRetries.ToString();
            tbClientDelay.Text = Program.AppINI._rqClientDelayMs.ToString();
            this.TopLevel = true;
            FormHelper.ApplyRoundedCorners(this, 20);
        }


        private void btnAdmin_Click(object sender, EventArgs e)
        {
            using (var login = new LoginDialog("Login"))
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Admin verified.");

                    using (var frm = new SettingsDialogAdmin())
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            SettingsChanged = true;
                        }
                    }
                }
            }
        }

        private async void btnSettingsSave_Click(object sender, EventArgs e)
        {
            List<string> errors = new List<string>();

            // Validate textboxes visually
            ValidateTextbox(tbRqClientAdd);
            ValidateTextbox(tbAppRefFreq);
            ValidateTextbox(tbClientMaxRetry);
            ValidateTextbox(tbClientDelay);

            if (string.IsNullOrWhiteSpace(tbRqClientAdd.Text))
                errors.Add("rqlite Client Address");

            if (!int.TryParse(tbAppRefFreq.Text, out int refresh))
                errors.Add("App Refresh Frequency (must be number)");

            if (!int.TryParse(tbClientMaxRetry.Text, out int retries))
                errors.Add("Client Max Retries (must be number)");

            if (!int.TryParse(tbClientDelay.Text, out int delay))
                errors.Add("Client Delay (must be number)");

            if (errors.Count > 0)
            {
                MessageBox.Show(
                    "Please fix the following:\n\n" + string.Join("\n", errors),
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            Utils.showStatusAndSpinner(lbStatus, pbSpinner, "Testing RQ client...");

            var test = await RqliteClient.TestRqClientAsync(
                tbRqClientAdd.Text.Trim(),
                3000
            );

            if (!test.Success)
            {
                MessageBox.Show(
                    "RQ Client connection failed:\n\n" + test.Error,
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            Utils.hideStatusAndSpinner(lbStatus, pbSpinner, "Connected");

            // Save settings
            Program.AppINI._defaultPrinter = tbDefPrint.Text.Trim();
            Program.AppINI._printerIP = tbPrinterIP.Text.Trim();
            Program.AppINI._printerPort = tbPrinterPort.Text.Trim();
            Program.AppINI._appRefresh = refresh;

            Program.AppINI._rqClientAddress = tbRqClientAdd.Text.Trim();
            Program.AppINI._rqClientMaxRetries = retries;
            Program.AppINI._rqClientDelayMs = delay;

            string err;
            if (!Program.AppINI.UpdateIni(out err))
            {
                MessageBox.Show(err);
                return;
            }

            MessageBox.Show("Settings saved.");
            SettingsChanged = true;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnSettingsCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
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
    }
}
