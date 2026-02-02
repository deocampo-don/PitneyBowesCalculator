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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.txtValue = new System.Windows.Forms.Label();
            this.txtWOName = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.txtValue);
            this.pnlMain.Controls.Add(this.txtWOName);
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(664, 25);
            this.pnlMain.TabIndex = 0;
            // 
            // txtValue
            // 
            this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValue.AutoSize = true;
            this.txtValue.Location = new System.Drawing.Point(613, 4);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(35, 16);
            this.txtValue.TabIndex = 1;
            this.txtValue.Text = "1500";
            // 
            // txtWOName
            // 
            this.txtWOName.AutoSize = true;
            this.txtWOName.Location = new System.Drawing.Point(15, 5);
            this.txtWOName.Name = "txtWOName";
            this.txtWOName.Size = new System.Drawing.Size(152, 16);
            this.txtWOName.TabIndex = 0;
            this.txtWOName.Text = "CXXX26010102PER0001";
            // 
            // PalletDetailsRowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "PalletDetailsRowControl";
            this.Size = new System.Drawing.Size(664, 25);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label txtWOName;
        private System.Windows.Forms.Label txtValue;
    }
}
