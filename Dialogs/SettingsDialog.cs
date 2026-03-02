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

namespace WindowsFormsApp1
{
    public partial class SettingsDialog : Form
    {

        private INIClass appINI;
        public SettingsDialog()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
        

        }

        private async void SettingsDialog_Load(object sender, EventArgs e)
        {
            tbDefPrinter.Text = appINI._defaultPrinter;
            tbDirectory.Text = appINI._outputDir;

            // CPS (DATABASE)
            var cps = await RqliteClient.LoadCpsSettingsAsync();
            if (cps != null)
            {
                tbCpsServer.Text = cps.CPSServer;
                tbCpsDb.Text = cps.CPSDatabase;
                tbCpsQuery.Text = cps.CPSQuery;
                tbConnTimeOut.Text = cps.ConnectionTimeout.ToString();
                toggleTrustedConnection.Checked = cps.TrustedConnection;
                toggleTrustedServerCert.Checked = cps.TrustedServerCertificate;
            }
        }

     
        private void btnSettingsCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSettingsSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Save CPS (DATABASE - rqlite)
                await RqliteClient.SaveCpsSettingsAsync(
                    tbCpsServer.Text,
                    tbCpsDb.Text,
                    tbCpsQuery.Text,
                    int.TryParse(tbConnTimeOut.Text, out int t) ? t : 30,
                    toggleTrustedConnection.Checked,
                    toggleTrustedServerCert.Checked
                );

                // 2. Save LOCAL (INI - workstation specific)
                //string err;
                //if (!appINI.SaveLocalSettings(
                //    tbDefPrinter.Text,
                //    tbDirectory.Text,
                //    out err))
                //{
                //    MessageBox.Show("Failed to save local settings: " + err);
                //    return;
                //}

                MessageBox.Show("Settings saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Save failed: " + ex.Message);
            }
        }
    }
}
