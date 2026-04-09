namespace PitneyBowesCalculator.Packed_And_Ready.View_Button
{
    partial class ShipPalletsConfirmationDialog
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
            pnlHeader = new System.Windows.Forms.Panel();
            btnExit = new System.Windows.Forms.Button();
            btnNo = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            lblHeader = new System.Windows.Forms.Label();
            btnYes = new System.Windows.Forms.Button();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = System.Drawing.Color.Transparent;
            pnlHeader.Controls.Add(btnExit);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new System.Drawing.Size(433, 30);
            pnlHeader.TabIndex = 7;
            // 
            // btnExit
            // 
            btnExit.Anchor = System.Windows.Forms.AnchorStyles.None;
            btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnExit.ForeColor = System.Drawing.Color.Transparent;
            btnExit.Image = Properties.Resources.close_img;
            btnExit.Location = new System.Drawing.Point(404, 10);
            btnExit.Name = "btnExit";
            btnExit.Size = new System.Drawing.Size(17, 17);
            btnExit.TabIndex = 8;
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click_1;
            // 
            // btnNo
            // 
            btnNo.BackColor = System.Drawing.Color.Gray;
            btnNo.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnNo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            btnNo.ForeColor = System.Drawing.Color.White;
            btnNo.Location = new System.Drawing.Point(248, 135);
            btnNo.Name = "btnNo";
            btnNo.Size = new System.Drawing.Size(70, 34);
            btnNo.TabIndex = 12;
            btnNo.Text = "No";
            btnNo.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label2.Location = new System.Drawing.Point(22, 88);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(212, 21);
            label2.TabIndex = 11;
            label2.Text = "want to ship them out now?";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(22, 69);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(373, 21);
            label1.TabIndex = 10;
            label1.Text = "Looks like all pallets are packed and ready. Do you";
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblHeader.Location = new System.Drawing.Point(22, 33);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new System.Drawing.Size(140, 30);
            lblHeader.TabIndex = 9;
            lblHeader.Text = "Good to go?";
            // 
            // btnYes
            // 
            btnYes.BackColor = System.Drawing.Color.Black;
            btnYes.Cursor = System.Windows.Forms.Cursors.Hand;
            btnYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnYes.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            btnYes.ForeColor = System.Drawing.Color.White;
            btnYes.Location = new System.Drawing.Point(324, 135);
            btnYes.Name = "btnYes";
            btnYes.Size = new System.Drawing.Size(84, 34);
            btnYes.TabIndex = 8;
            btnYes.Text = "Yes";
            btnYes.UseVisualStyleBackColor = false;
            // 
            // ShipPalletsConfirmationDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(433, 190);
            Controls.Add(pnlHeader);
            Controls.Add(btnNo);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblHeader);
            Controls.Add(btnYes);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "ShipPalletsConfirmationDialog";
            Text = "ShipPalletsConfirmationDialog";
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnExit;
    }
}