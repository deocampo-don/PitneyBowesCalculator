namespace WindowsFormsApp1.Packed_And_Ready.View_Button
{
    partial class PalletDetailsRowControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlMain = new System.Windows.Forms.Panel();
            txtValue = new System.Windows.Forms.Label();
            txtWOName = new System.Windows.Forms.Label();
            pnlMain.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(txtValue);
            pnlMain.Controls.Add(txtWOName);
            pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlMain.Location = new System.Drawing.Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new System.Drawing.Size(581, 29);
            pnlMain.TabIndex = 0;
            // 
            // txtValue
            // 
            txtValue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            txtValue.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            txtValue.Location = new System.Drawing.Point(475, 5);
            txtValue.Name = "txtValue";
            txtValue.Size = new System.Drawing.Size(68, 19);
            txtValue.TabIndex = 1;
            txtValue.Text = "00000000";
            txtValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtWOName
            // 
            txtWOName.Anchor = System.Windows.Forms.AnchorStyles.None;
            txtWOName.AutoSize = true;
            txtWOName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            txtWOName.Location = new System.Drawing.Point(12, 5);
            txtWOName.Name = "txtWOName";
            txtWOName.Size = new System.Drawing.Size(176, 21);
            txtWOName.TabIndex = 0;
            txtWOName.Text = "CXXX26010102PER0001";
            // 
            // PalletDetailsRowControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(pnlMain);
            Name = "PalletDetailsRowControl";
            Size = new System.Drawing.Size(581, 29);
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label txtWOName;
        private System.Windows.Forms.Label txtValue;
    }
}
