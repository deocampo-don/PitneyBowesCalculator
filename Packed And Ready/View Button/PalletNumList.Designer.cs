namespace WindowsFormsApp1.Packed_And_Ready.View_Button
{
    partial class PalletNumList
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
            this.btnPalletNum = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnPalletNum
            // 
            this.btnPalletNum.BackColor = System.Drawing.Color.White;
            this.btnPalletNum.FlatAppearance.BorderSize = 0;
            this.btnPalletNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPalletNum.Location = new System.Drawing.Point(3, 3);
            this.btnPalletNum.Name = "btnPalletNum";
            this.btnPalletNum.Size = new System.Drawing.Size(232, 26);
            this.btnPalletNum.TabIndex = 0;
            this.btnPalletNum.Text = "Pallet #";
            this.btnPalletNum.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPalletNum.UseVisualStyleBackColor = false;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(238, 29);
            this.pnlMain.TabIndex = 1;
            // 
            // PalletNumList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnPalletNum);
            this.Controls.Add(this.pnlMain);
            this.Name = "PalletNumList";
            this.Size = new System.Drawing.Size(238, 29);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPalletNum;
        private System.Windows.Forms.Panel pnlMain;
    }
}
