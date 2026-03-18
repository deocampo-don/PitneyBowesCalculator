using System;
using System.IO;
using System.Windows.Forms;
using WindowsFormsApp1;
using Post_List_Tool;

namespace Launcher
{
    public partial class Launcher : Form
    {
        private INIClass launcherINI;

        public Launcher()
        {
            InitializeComponent();
        }

        private void Launcher_Load(object sender, EventArgs e)
        {
            InitializeConfig();
            BackgroundImage = Properties.Resources.launcherBg; BackgroundImage = Properties.Resources.launcherBg;
            BackgroundImageLayout = ImageLayout.Stretch;
            // SHIFT override -> force show launcher
            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
                return;

            string defaultApp = launcherINI.StartUpScreen;

            if (string.IsNullOrEmpty(defaultApp))
                return;

            switch (defaultApp.ToUpper())
            {
                case "PBC":
                    LaunchPBC();
                    this.Hide();
                    break;

                case "POSTLIST":
                    LaunchPostList();
                    this.Hide();
                    break;
            }
        }

        private void InitializeConfig()
        {
            string err;

            string configPath = Path.Combine(Application.StartupPath, "config.ini");

            launcherINI = new INIClass(configPath);

            if (!File.Exists(configPath))
            {
                launcherINI.BuildNewIni(out err);

                if (!string.IsNullOrEmpty(err))
                {
                    //MessageBox.Show("Failed to create config.ini\n" + err);
                    MessageDialogBox.ShowDialog("", "Failed to create config.ini\n" + err, MessageBoxButtons.OK, MessageType.Info);
                    return;
                }
            }

            if (!launcherINI.GetINIVars(out err))
            {
                //MessageBox.Show("Failed to load configuration\n" + err);
                MessageDialogBox.ShowDialog("", "Failed to load configuration\n" + err, MessageBoxButtons.OK, MessageType.Info);
            }
        }

        private void LaunchPBC()
        {
            string err;

            string configPath = Path.Combine(Application.StartupPath, "config.ini");

            WindowsFormsApp1.Program.AppINI = new INIClass(configPath);

            if (!File.Exists(configPath))
            {
                WindowsFormsApp1.Program.AppINI.BuildNewIni(out err);

                if (!string.IsNullOrEmpty(err))
                {
                    //MessageBox.Show("Failed to create config.ini\n" + err);
                    MessageDialogBox.ShowDialog("", "Failed to create config.ini\n" + err, MessageBoxButtons.OK, MessageType.Info);
                    return;
                }
            }

            if (!WindowsFormsApp1.Program.AppINI.GetINIVars(out err))
            {

                //MessageBox.Show("Failed to load configuration\n" + err);
                MessageDialogBox.ShowDialog("", "Failed to load configuration\n" + err, MessageBoxButtons.OK, MessageType.Info);
                return;
            }

            var frm = new PBCMain();
            frm.Show();
        }

        private void LaunchPostList()
        {
            var frm = new FrmMenu();
            frm.Show();
        }

        private void btnPbc_Click(object sender, EventArgs e)
        {
            LaunchPBC();
        }

        private void btnPl_Click(object sender, EventArgs e)
        {
            LaunchPostList();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void rbPbcDef_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rbPbcDef.Checked)
        //    {
        //        //launcherINI.StartUpScreen = "PBC";
        //        launcherINI.UpdateIni(out _);
        //    }
        //}
    }
}