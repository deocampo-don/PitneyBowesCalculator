namespace WindowsFormsApp1.Dialogs
{
    partial class LoginDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblAdminLogin = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new Krypton.Toolkit.KryptonLabel();
            this.tbUserName = new Krypton.Toolkit.KryptonTextBox();
            this.tbPass = new Krypton.Toolkit.KryptonTextBox();
            this.btnCanecl = new Krypton.Toolkit.KryptonButton();
            this.btnLogin = new Krypton.Toolkit.KryptonButton();
            this.lbstatus = new Krypton.Toolkit.KryptonLabel();
            this.tbVerifyPwd = new Krypton.Toolkit.KryptonTextBox();
            this.lbVerifyPwd = new Krypton.Toolkit.KryptonLabel();
            this.pbSpinner = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbSpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAdminLogin
            // 
            this.lblAdminLogin.Location = new System.Drawing.Point(12, 12);
            this.lblAdminLogin.Name = "lblAdminLogin";
            this.lblAdminLogin.Size = new System.Drawing.Size(232, 53);
            this.lblAdminLogin.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 25.8F, System.Drawing.FontStyle.Bold);
            this.lblAdminLogin.TabIndex = 0;
            this.lblAdminLogin.Values.Text = "Admin Login";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(18, 71);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(87, 26);
            this.kryptonLabel2.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel2.TabIndex = 1;
            this.kryptonLabel2.Values.Text = "Username";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(18, 139);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(83, 26);
            this.kryptonLabel3.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel3.TabIndex = 2;
            this.kryptonLabel3.Values.Text = "Password";
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(22, 98);
            this.tbUserName.Margin = new System.Windows.Forms.Padding(2);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(288, 36);
            this.tbUserName.StateCommon.Border.Rounding = 5F;
            this.tbUserName.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 13.8F);
            this.tbUserName.StateCommon.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.tbUserName.TabIndex = 8;
            // 
            // tbPass
            // 
            this.tbPass.Location = new System.Drawing.Point(22, 170);
            this.tbPass.Margin = new System.Windows.Forms.Padding(2);
            this.tbPass.Name = "tbPass";
            this.tbPass.PasswordChar = '●';
            this.tbPass.Size = new System.Drawing.Size(288, 36);
            this.tbPass.StateCommon.Border.Rounding = 5F;
            this.tbPass.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 13.8F);
            this.tbPass.StateCommon.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.tbPass.TabIndex = 9;
            this.tbPass.UseSystemPasswordChar = true;
            // 
            // btnCanecl
            // 
            this.btnCanecl.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            this.btnCanecl.Location = new System.Drawing.Point(159, 293);
            this.btnCanecl.Margin = new System.Windows.Forms.Padding(2, 2, 8, 2);
            this.btnCanecl.Name = "btnCanecl";
            this.btnCanecl.Size = new System.Drawing.Size(75, 32);
            this.btnCanecl.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.btnCanecl.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnCanecl.StateCommon.Border.Rounding = 5F;
            this.btnCanecl.StateCommon.Border.Width = 1;
            this.btnCanecl.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCanecl.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.btnCanecl.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.btnCanecl.TabIndex = 11;
            this.btnCanecl.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCanecl.Values.Text = "Cancel";
            this.btnCanecl.Click += new System.EventHandler(this.btnCanecl_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            this.btnLogin.Location = new System.Drawing.Point(244, 293);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2, 2, 5, 2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(68, 32);
            this.btnLogin.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(80)))), ((int)(((byte)(164)))));
            this.btnLogin.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnLogin.StateCommon.Border.Rounding = 5F;
            this.btnLogin.StateCommon.Border.Width = 1;
            this.btnLogin.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnLogin.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.btnLogin.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.btnLogin.TabIndex = 10;
            this.btnLogin.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnLogin.Values.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lbstatus
            // 
            this.lbstatus.Location = new System.Drawing.Point(56, 298);
            this.lbstatus.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.lbstatus.Name = "lbstatus";
            this.lbstatus.Size = new System.Drawing.Size(88, 20);
            this.lbstatus.TabIndex = 12;
            this.lbstatus.Values.Text = "kryptonLabel4";
            this.lbstatus.Visible = false;
           
            // 
            // tbVerifyPwd
            // 
            this.tbVerifyPwd.Location = new System.Drawing.Point(22, 242);
            this.tbVerifyPwd.Margin = new System.Windows.Forms.Padding(2);
            this.tbVerifyPwd.Name = "tbVerifyPwd";
            this.tbVerifyPwd.PasswordChar = '●';
            this.tbVerifyPwd.Size = new System.Drawing.Size(288, 36);
            this.tbVerifyPwd.StateCommon.Border.Rounding = 5F;
            this.tbVerifyPwd.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 13.8F);
            this.tbVerifyPwd.StateCommon.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.tbVerifyPwd.TabIndex = 14;
            this.tbVerifyPwd.UseSystemPasswordChar = true;
            this.tbVerifyPwd.Visible = false;
            // 
            // lbVerifyPwd
            // 
            this.lbVerifyPwd.Location = new System.Drawing.Point(18, 211);
            this.lbVerifyPwd.Name = "lbVerifyPwd";
            this.lbVerifyPwd.Size = new System.Drawing.Size(131, 26);
            this.lbVerifyPwd.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVerifyPwd.TabIndex = 13;
            this.lbVerifyPwd.Values.Text = "Verify Password";
            this.lbVerifyPwd.Visible = false;
            // 
            // pbSpinner
            // 
            this.pbSpinner.Location = new System.Drawing.Point(22, 293);
            this.pbSpinner.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pbSpinner.Name = "pbSpinner";
            this.pbSpinner.Size = new System.Drawing.Size(34, 32);
            this.pbSpinner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSpinner.TabIndex = 15;
            this.pbSpinner.TabStop = false;
            this.pbSpinner.Visible = false;
            // 
            // LoginDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 346);
            this.Controls.Add(this.pbSpinner);
            this.Controls.Add(this.tbVerifyPwd);
            this.Controls.Add(this.lbVerifyPwd);
            this.Controls.Add(this.lbstatus);
            this.Controls.Add(this.btnCanecl);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.tbPass);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.kryptonLabel3);
            this.Controls.Add(this.kryptonLabel2);
            this.Controls.Add(this.lblAdminLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LoginDialog";
            this.Load += new System.EventHandler(this.LoginDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbSpinner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonLabel lblAdminLogin;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private Krypton.Toolkit.KryptonTextBox tbUserName;
        private Krypton.Toolkit.KryptonTextBox tbPass;
        private Krypton.Toolkit.KryptonButton btnCanecl;
        private Krypton.Toolkit.KryptonButton btnLogin;
        private Krypton.Toolkit.KryptonLabel lbstatus;
        private Krypton.Toolkit.KryptonTextBox tbVerifyPwd;
        private Krypton.Toolkit.KryptonLabel lbVerifyPwd;
        private System.Windows.Forms.PictureBox pbSpinner;
    }
}