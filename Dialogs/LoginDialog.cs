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
        private string DialogMode;

        public LoginDialog(string frm)
        {
            InitializeComponent();
            this.DialogMode = frm;
        }
        private void LoginDialog_Load(object sender, EventArgs e)
        {
            if (DialogMode == "Login")
            {
                lbVerifyPwd.Visible = false;
                tbVerifyPwd.Visible = false;
                lblAdminLogin.Text = "Admin Login";
            }
            else
            {
                lbVerifyPwd.Visible = true;
                tbVerifyPwd.Visible = true;
                lblAdminLogin.Text = "Add User";
            }
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

            if (DialogMode == "AddUser")
            {
                if (password != tbVerifyPwd.Text.Trim())
                {
                    MessageBox.Show("Passwords do not match.");
                    return;
                }
            }

            btnLogin.Enabled = false;
            lbstatus.Visible = true;

            string hashedPassword = HashPassword(password);

            bool result = false;

            if (DialogMode == "Login")
            {
                Utils.showStatusAndSpinner(lbstatus, pbSpinner, "Validating...");
                result = await RqliteClient.ValidateAdminAsync(username, hashedPassword);
            }
            else
            {
                
                Utils.hideStatusAndSpinner(lbstatus, pbSpinner, "Creating user...");
                result = await RqliteClient.CreateUserAsync(username, hashedPassword);
            }

            btnLogin.Enabled = true;
            lbstatus.Visible = false;

            if (result)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(DialogMode == "Login"
                    ? "Invalid admin credentials."
                    : "Failed to create user.");
            }
        }

        //private async void btnLogin_Click(object sender, EventArgs e)
        //{
        //    string username = tbUserName.Text.Trim();
        //    string password = tbPass.Text.Trim();

        //    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        //    {
        //        MessageBox.Show("Enter username and password.");
        //        return;
        //    }

        //    string hashedPassword = HashPassword(password);

        //    bool created = await RqliteClient.CreateUserAsync(username, hashedPassword);

        //    if (created)
        //    {
        //        MessageBox.Show("Admin created successfully.");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Failed to create admin.");
        //    }
        //}

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
