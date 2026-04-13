using System;
using System.IO;
using System.Windows.Forms;
using PitneyBowesCalculator;
using Post_List_Tool;

namespace Launcher
{
    public partial class Launcher : Form
    {
        private INIClass launcherINI;
        private bool _isInitializing = true;
        private bool _isUpdatingCheckbox = false;
        public Launcher()
        {
            InitializeComponent();
        }

        private void Launcher_Load(object sender, EventArgs e)
        {
            _isInitializing = true;

            InitializeConfig();

            BackgroundImage = Properties.Resources.launcherBg;
            BackgroundImageLayout = ImageLayout.Stretch;

            // Load saved default app
            string defaultApp = launcherINI?.StartUpScreen ?? "";

            rbPbcDef.Checked = defaultApp.Equals("PBC", StringComparison.OrdinalIgnoreCase);
            rbPlDef.Checked = defaultApp.Equals("POSTLIST", StringComparison.OrdinalIgnoreCase);

            _isInitializing = false;

            // SHIFT key -> show launcher always
            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
                return;

            if (string.IsNullOrEmpty(defaultApp))
                return;

            // Auto-launch
            // Auto-launch
            switch (defaultApp.ToUpper())
            {
                case "PBC":
                    this.Hide();     // ✅ Hide first
                    LaunchPBC();     // ✅ Then start child app
                    break;

                case "POSTLIST":
                    this.Hide();
                    LaunchPostList();
                    break;
            }

        }

        private void InitializeConfig()
        {
            string err;
            string configPath = ConfigPath.GetMainConfigPath();

            launcherINI = new INIClass(configPath);

            if (!File.Exists(configPath))
            {
                launcherINI.BuildNewIni(out err);
                if (!string.IsNullOrEmpty(err))
                {
                    MessageDialogBox.ShowDialog("",
                        "Failed to create config.ini\n" + err,
                        MessageBoxButtons.OK,
                        MessageType.Info);
                    return;
                }
            }

            if (!launcherINI.GetINIVars(out err))
            {
                MessageDialogBox.ShowDialog("",
                    "Failed to load configuration\n" + err,
                    MessageBoxButtons.OK,
                    MessageType.Info);
            }
        }

        private void LaunchPBC()
        {
            string err;
            string configPath = ConfigPath.GetMainConfigPath();

            PitneyBowesCalculator.Program.AppINI = new INIClass(configPath);

            if (!File.Exists(configPath))
            {
                PitneyBowesCalculator.Program.AppINI.BuildNewIni(out err);
                if (!string.IsNullOrEmpty(err))
                {
                    MessageDialogBox.ShowDialog("", "Failed to create config.ini\n" + err, MessageBoxButtons.OK, MessageType.Info);
                    return;
                }
            }

            if (!PitneyBowesCalculator.Program.AppINI.GetINIVars(out err))
            {
                MessageDialogBox.ShowDialog("", "Failed to load configuration\n" + err, MessageBoxButtons.OK, MessageType.Info);
                return;
            }

            new PBCMain().Show();
            this.Hide();
        }

        private void LaunchPostList()
        {
            var frm = new FrmMenu();
            frm.Show();
            this.Hide();
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



        private void rbPbcDef_CheckedChanged(object sender, EventArgs e)
        {
            if (_isInitializing || !rbPbcDef.Checked) return;

            launcherINI.SetStartUpScreen("PBC");
            launcherINI.UpdateStartUpScreen(out _);
            launcherINI.GetINIVars(out _);
        }

        private void rbPlDef_CheckedChanged(object sender, EventArgs e)
        {
            if (_isInitializing || !rbPlDef.Checked) return;

            launcherINI.SetStartUpScreen("POSTLIST");
            launcherINI.UpdateStartUpScreen(out _);
            launcherINI.GetINIVars(out _);
        }

        public static class ConfigPath
        {
            public static string GetMainConfigPath()
            {
                string path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "PBC",
                    "config.ini"
                );

                Directory.CreateDirectory(Path.GetDirectoryName(path));
                return path;
            }
        }
    }
}