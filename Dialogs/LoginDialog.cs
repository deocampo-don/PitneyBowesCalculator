using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

namespace WindowsFormsApp1.Dialogs
{
    public partial class LoginDialog : Form
    {
        public bool IsAuthenticated { get; private set; }

        public LoginDialog()
        {
            InitializeComponent();
        }

        private void btnCanecl_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = tbUserName.Text.Trim();
            string password = tbPass.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Enter username and password.");
                return;
            }

            btnLogin.Enabled = false;

            lbstatus.Text = "Checking credentials...";
            lbstatus.Visible = true;
            string hashedPassword = HashPassword(password);


            bool valid = await RqliteClient.ValidateAdminAsync(hashedPassword, password);

            btnLogin.Enabled = true;
            lbstatus.Visible = false;

            if (valid)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid admin credentials.");
            }
        }



public static string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
}
