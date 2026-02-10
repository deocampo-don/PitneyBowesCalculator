namespace WindowsFormsApp1.Packed_And_Ready.View_Button
{
    partial class PalletNumListRowControl
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
            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.chkBox = new System.Windows.Forms.CheckBox();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPalletNum
            // 
            this.btnPalletNum.BackColor = System.Drawing.Color.White;
            this.btnPalletNum.FlatAppearance.BorderSize = 0;
            this.btnPalletNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPalletNum.ForeColor = System.Drawing.Color.Black;
            this.btnPalletNum.Location = new System.Drawing.Point(2, 2);
            this.btnPalletNum.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnPalletNum.Name = "btnPalletNum";
            this.btnPalletNum.Size = new System.Drawing.Size(174, 20);
            this.btnPalletNum.TabIndex = 0;
            this.btnPalletNum.Text = "Pallet #";
            this.btnPalletNum.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPalletNum.UseVisualStyleBackColor = false;
            this.btnPalletNum.Click += new System.EventHandler(this.btnPalletNum_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.chkBox);
            this.pnlMain.Controls.Add(this.btnPalletNum);
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(178, 24);
            this.pnlMain.TabIndex = 1;
            // 
            // flowPanel
            // 
            this.flowPanel.Location = new System.Drawing.Point(0, 0);
            this.flowPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowPanel.Name = "flowPanel";
            this.flowPanel.Size = new System.Drawing.Size(178, 24);
            this.flowPanel.TabIndex = 2;
            // 
            // chkBox
            // 
            this.chkBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkBox.BackColor = System.Drawing.Color.Transparent;
            this.chkBox.Location = new System.Drawing.Point(43, 2);
            this.chkBox.Name = "chkBox";
            this.chkBox.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.chkBox.Size = new System.Drawing.Size(21, 18);
            this.chkBox.TabIndex = 1;
            this.chkBox.UseVisualStyleBackColor = false;
            // 
            // PalletNumListRowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.flowPanel);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "PalletNumListRowControl";
            this.Size = new System.Drawing.Size(178, 24);
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPalletNum;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.FlowLayoutPanel flowPanel;
        private System.Windows.Forms.CheckBox chkBox;
    }
}
