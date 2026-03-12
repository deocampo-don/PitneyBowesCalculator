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
            lblAdminLogin = new Krypton.Toolkit.KryptonLabel();
            kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            kryptonLabel3 = new Krypton.Toolkit.KryptonLabel();
            tbUserName = new Krypton.Toolkit.KryptonTextBox();
            tbPass = new Krypton.Toolkit.KryptonTextBox();
            btnCanecl = new Krypton.Toolkit.KryptonButton();
            btnLogin = new Krypton.Toolkit.KryptonButton();
            lbstatus = new Krypton.Toolkit.KryptonLabel();
            tbVerifyPwd = new Krypton.Toolkit.KryptonTextBox();
            lbVerifyPwd = new Krypton.Toolkit.KryptonLabel();
            pbSpinner = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbSpinner).BeginInit();
            SuspendLayout();
            // 
            // lblAdminLogin
            // 
            lblAdminLogin.Location = new System.Drawing.Point(14, 14);
            lblAdminLogin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            lblAdminLogin.Name = "lblAdminLogin";
            lblAdminLogin.Size = new System.Drawing.Size(232, 53);
            lblAdminLogin.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 25.8F, System.Drawing.FontStyle.Bold);
            lblAdminLogin.TabIndex = 0;
            lblAdminLogin.Values.Text = "Admin Login";
            // 
            // kryptonLabel2
            // 
            kryptonLabel2.Location = new System.Drawing.Point(21, 82);
            kryptonLabel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            kryptonLabel2.Name = "kryptonLabel2";
            kryptonLabel2.Size = new System.Drawing.Size(87, 26);
            kryptonLabel2.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            kryptonLabel2.TabIndex = 1;
            kryptonLabel2.Values.Text = "Username";
            // 
            // kryptonLabel3
            // 
            kryptonLabel3.Location = new System.Drawing.Point(21, 150);
            kryptonLabel3.Margin = new System.Windows.Forms.Padding(4, 11, 4, 4);
            kryptonLabel3.Name = "kryptonLabel3";
            kryptonLabel3.Size = new System.Drawing.Size(83, 26);
            kryptonLabel3.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            kryptonLabel3.TabIndex = 2;
            kryptonLabel3.Values.Text = "Password";
            // 
            // tbUserName
            // 
            tbUserName.Location = new System.Drawing.Point(28, 112);
            tbUserName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            tbUserName.Name = "tbUserName";
            tbUserName.Size = new System.Drawing.Size(336, 36);
            tbUserName.StateCommon.Border.Rounding = 5F;
            tbUserName.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 13.8F);
            tbUserName.StateCommon.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            tbUserName.TabIndex = 8;
            // 
            // tbPass
            // 
            tbPass.Location = new System.Drawing.Point(28, 180);
            tbPass.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            tbPass.Name = "tbPass";
            tbPass.PasswordChar = '●';
            tbPass.Size = new System.Drawing.Size(336, 36);
            tbPass.StateCommon.Border.Rounding = 5F;
            tbPass.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 13.8F);
            tbPass.StateCommon.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            tbPass.TabIndex = 9;
            tbPass.UseSystemPasswordChar = true;
            // 
            // btnCanecl
            // 
            btnCanecl.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            btnCanecl.Location = new System.Drawing.Point(185, 300);
            btnCanecl.Margin = new System.Windows.Forms.Padding(3, 2, 10, 2);
            btnCanecl.Name = "btnCanecl";
            btnCanecl.Size = new System.Drawing.Size(88, 37);
            btnCanecl.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(227, 227, 227);
            btnCanecl.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnCanecl.StateCommon.Border.Rounding = 5F;
            btnCanecl.StateCommon.Border.Width = 1;
            btnCanecl.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnCanecl.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnCanecl.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnCanecl.TabIndex = 11;
            btnCanecl.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            btnCanecl.Values.Text = "Cancel";
            btnCanecl.Click += btnCanecl_Click;
            // 
            // btnLogin
            // 
            btnLogin.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            btnLogin.Location = new System.Drawing.Point(284, 300);
            btnLogin.Margin = new System.Windows.Forms.Padding(3, 2, 6, 2);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new System.Drawing.Size(80, 37);
            btnLogin.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(103, 80, 164);
            btnLogin.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnLogin.StateCommon.Border.Rounding = 5F;
            btnLogin.StateCommon.Border.Width = 1;
            btnLogin.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            btnLogin.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnLogin.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnLogin.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnLogin.TabIndex = 10;
            btnLogin.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            btnLogin.Values.Text = "Login";
            btnLogin.Click += btnLogin_Click;
            // 
            // lbstatus
            // 
            lbstatus.Location = new System.Drawing.Point(67, 307);
            lbstatus.Margin = new System.Windows.Forms.Padding(0, 4, 4, 4);
            lbstatus.Name = "lbstatus";
            lbstatus.Size = new System.Drawing.Size(88, 20);
            lbstatus.TabIndex = 12;
            lbstatus.Values.Text = "kryptonLabel4";
            lbstatus.Visible = false;
            // 
            // tbVerifyPwd
            // 
            tbVerifyPwd.Location = new System.Drawing.Point(28, 247);
            tbVerifyPwd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            tbVerifyPwd.Name = "tbVerifyPwd";
            tbVerifyPwd.PasswordChar = '●';
            tbVerifyPwd.Size = new System.Drawing.Size(336, 36);
            tbVerifyPwd.StateCommon.Border.Rounding = 5F;
            tbVerifyPwd.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 13.8F);
            tbVerifyPwd.StateCommon.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            tbVerifyPwd.TabIndex = 14;
            tbVerifyPwd.UseSystemPasswordChar = true;
            tbVerifyPwd.Visible = false;
            // 
            // lbVerifyPwd
            // 
            lbVerifyPwd.Location = new System.Drawing.Point(21, 217);
            lbVerifyPwd.Margin = new System.Windows.Forms.Padding(4, 11, 4, 4);
            lbVerifyPwd.Name = "lbVerifyPwd";
            lbVerifyPwd.Size = new System.Drawing.Size(131, 26);
            lbVerifyPwd.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lbVerifyPwd.TabIndex = 13;
            lbVerifyPwd.Values.Text = "Verify Password";
            lbVerifyPwd.Visible = false;
            // 
            // pbSpinner
            // 
            pbSpinner.Location = new System.Drawing.Point(28, 300);
            pbSpinner.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
            pbSpinner.Name = "pbSpinner";
            pbSpinner.Size = new System.Drawing.Size(39, 37);
            pbSpinner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pbSpinner.TabIndex = 15;
            pbSpinner.TabStop = false;
            pbSpinner.Visible = false;
            // 
            // LoginDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(391, 355);
            Controls.Add(pbSpinner);
            Controls.Add(tbVerifyPwd);
            Controls.Add(lbVerifyPwd);
            Controls.Add(lbstatus);
            Controls.Add(btnCanecl);
            Controls.Add(btnLogin);
            Controls.Add(tbPass);
            Controls.Add(tbUserName);
            Controls.Add(kryptonLabel3);
            Controls.Add(kryptonLabel2);
            Controls.Add(lblAdminLogin);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            Name = "LoginDialog";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "LoginDialog";
            Load += LoginDialog_Load;
            ((System.ComponentModel.ISupportInitialize)pbSpinner).EndInit();
            ResumeLayout(false);
            PerformLayout();

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