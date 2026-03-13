using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public static class Program
    {
        public static INIClass AppINI;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string err;

            // 🔹 Create config path beside the EXE
            string configPath = Path.Combine(Application.StartupPath, "config.ini");

            // 🔹 Initialize INI
            AppINI = new INIClass(configPath);

            // 🔹 Create INI if missing
            if (!File.Exists(configPath))
            {
                AppINI.BuildNewIni(out err);

                if (!string.IsNullOrEmpty(err))
                {
                    MessageBox.Show("Failed to create config.ini\n" + err);
                    return;
                }
            }

            // 🔹 Load values
            if (!AppINI.GetINIVars(out err))
            {
                MessageBox.Show("Failed to load configuration\n" + err);
                return;
            }

            Application.Run(new PBCMain());
        }
    }
}