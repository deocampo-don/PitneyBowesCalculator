namespace WindowsFormsApp1.Packed_And_Ready.View_Button
{
    partial class RemovePallets
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnYes = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNo = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Controls.Add(this.btnExit);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(462, 39);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.Transparent;
            this.btnExit.Image = global::WindowsFormsApp1.Properties.Resources.close_img;
            this.btnExit.Location = new System.Drawing.Point(408, 7);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(28, 25);
            this.btnExit.TabIndex = 7;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click_1);
            // 
            // btnYes
            // 
            this.btnYes.BackColor = System.Drawing.Color.Black;
            this.btnYes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYes.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYes.ForeColor = System.Drawing.Color.White;
            this.btnYes.Location = new System.Drawing.Point(347, 167);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(70, 60);
            this.btnYes.TabIndex = 2;
            this.btnYes.Text = "Yes";
            this.btnYes.UseVisualStyleBackColor = false;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click_1);
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(17, 51);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(297, 38);
            this.lblHeader.TabIndex = 3;
            this.lblHeader.Text = "Before You Remove...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(383, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Removing this packed pallet will undo it\'s packed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(272, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "status. Would you like to proceed?";
            // 
            // btnNo
            // 
            this.btnNo.BackColor = System.Drawing.Color.Gray;
            this.btnNo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNo.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNo.ForeColor = System.Drawing.Color.White;
            this.btnNo.Location = new System.Drawing.Point(262, 167);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(70, 60);
            this.btnNo.TabIndex = 6;
            this.btnNo.Text = "No";
            this.btnNo.UseVisualStyleBackColor = false;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click_1);
            // 
            // RemovePallets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(462, 263);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RemovePallets";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Button btnExit;
    }
}