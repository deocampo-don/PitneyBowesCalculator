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
            using (var login = new LoginDialog())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Admin verified.");

                    using (var frm = new SettingsDialogAdmin())
                    {
                        frm.ShowDialog();
                    }
                }
            }
        }

        private void btnSettingsSave_Click(object sender, EventArgs e)
        {
            string err;

            Program.AppINI._defaultPrinter = tbDefPrint.Text;
            Program.AppINI._printerIP = tbPrinterIP.Text;
            Program.AppINI._printerPort = tbPrinterPort.Text;

            int.TryParse(tbAppRefFreq.Text, out int refresh);
            Program.AppINI._appRefresh = refresh;

            // NEW FIELDS
            Program.AppINI._rqClientAddress = tbRqClientAdd.Text;

            int.TryParse(tbClientMaxRetry.Text, out int retries);
            Program.AppINI._rqClientMaxRetries = retries;

            int.TryParse(tbClientDelay.Text, out int delay);
            Program.AppINI._rqClientDelayMs = delay;

            if (!Program.AppINI.UpdateIni(out err))
            {
                MessageBox.Show(err);
                return;
            }

            MessageBox.Show("Settings saved.");
        }

        private void btnSettingsCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    
    }
}
