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
using PitneyBowesCalculator;

namespace PitneyBowesCalculator.Dialogs
{
    public partial class LoginDialog : Form
    {
        public bool IsAuthenticated { get; private set; }
        private string DialogMode;

        public LoginDialog(string frm)
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this);
            ShadowHelper.ApplyShadow(this);
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

        // ✅ Consistent version
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = tbUserName.Text.Trim();
            string password = tbPass.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageDialogBox.ShowDialog("", "Enter username and password.", MessageBoxButtons.OK, MessageType.Error);
                return;
            }

            if (DialogMode == "AddUser" && password != tbVerifyPwd.Text.Trim())
            {
                MessageDialogBox.ShowDialog("", "Passwords do not match.", MessageBoxButtons.OK, MessageType.Error);
                return;
            }

            btnLogin.Enabled = false;
            string hashedPassword = HashPassword(password);
            bool result = false;

            try
            {
                if (DialogMode == "Login")
                {
                    Utils.showStatusAndSpinner(lbstatus, pbSpinner, "Validating...");
                    result = await RqliteClient.ValidateAdminAsync(username, hashedPassword);
                }
                else
                {
                    Utils.showStatusAndSpinner(lbstatus, pbSpinner, "Creating user...");
                    result = await RqliteClient.CreateUserAsync(username, hashedPassword);
                }
            }
            catch (Exception ex)
            {
                Utils.WriteExceptionError(ex);
                MessageDialogBox.ShowDialog("", "An error occurred: " + ex.Message, MessageBoxButtons.OK, MessageType.Error);
                return;
            }
            finally
            {
                btnLogin.Enabled = true;
                Utils.hideStatusAndSpinner(lbstatus, pbSpinner, "");
            }

            if (result)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageDialogBox.ShowDialog(
                    "",
                    DialogMode == "Login"
                        ? "Invalid admin credentials."
                        : "Failed to create user.",
                    MessageBoxButtons.OK,
                    MessageType.Error);
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
