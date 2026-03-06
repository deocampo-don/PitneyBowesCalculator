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
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new Krypton.Toolkit.KryptonLabel();
            this.tbUserName = new Krypton.Toolkit.KryptonTextBox();
            this.tbPass = new Krypton.Toolkit.KryptonTextBox();
            this.btnCanecl = new Krypton.Toolkit.KryptonButton();
            this.btnLogin = new Krypton.Toolkit.KryptonButton();
            this.lbstatus = new Krypton.Toolkit.KryptonLabel();
            this.SuspendLayout();
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(12, 12);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(232, 53);
            this.kryptonLabel1.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 25.8F, System.Drawing.FontStyle.Bold);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Admin Login";
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
            this.btnCanecl.Location = new System.Drawing.Point(159, 220);
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
            this.btnLogin.Location = new System.Drawing.Point(244, 220);
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
            this.lbstatus.Location = new System.Drawing.Point(211, 73);
            this.lbstatus.Name = "lbstatus";
            this.lbstatus.Size = new System.Drawing.Size(88, 20);
            this.lbstatus.TabIndex = 12;
            this.lbstatus.Values.Text = "kryptonLabel4";
            this.lbstatus.Visible = false;
            // 
            // LoginDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 270);
            this.Controls.Add(this.lbstatus);
            this.Controls.Add(this.btnCanecl);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.tbPass);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.kryptonLabel3);
            this.Controls.Add(this.kryptonLabel2);
            this.Controls.Add(this.kryptonLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LoginDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private Krypton.Toolkit.KryptonTextBox tbUserName;
        private Krypton.Toolkit.KryptonTextBox tbPass;
        private Krypton.Toolkit.KryptonButton btnCanecl;
        private Krypton.Toolkit.KryptonButton btnLogin;
        private Krypton.Toolkit.KryptonLabel lbstatus;
    }
}