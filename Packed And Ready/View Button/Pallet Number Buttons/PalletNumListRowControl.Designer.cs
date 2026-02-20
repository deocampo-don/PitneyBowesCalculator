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
            this.chkBox = new System.Windows.Forms.CheckBox();
            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPalletNum
            // 
            this.btnPalletNum.BackColor = System.Drawing.Color.White;
            this.btnPalletNum.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPalletNum.FlatAppearance.BorderSize = 0;
            this.btnPalletNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPalletNum.ForeColor = System.Drawing.Color.Black;
            this.btnPalletNum.Location = new System.Drawing.Point(3, 2);
            this.btnPalletNum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPalletNum.Name = "btnPalletNum";
            this.btnPalletNum.Size = new System.Drawing.Size(211, 25);
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
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(217, 30);
            this.pnlMain.TabIndex = 1;
            // 
            // chkBox
            // 
            this.chkBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkBox.BackColor = System.Drawing.Color.Transparent;
            this.chkBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBox.Location = new System.Drawing.Point(47, 2);
            this.chkBox.Margin = new System.Windows.Forms.Padding(4);
            this.chkBox.Name = "chkBox";
            this.chkBox.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.chkBox.Size = new System.Drawing.Size(19, 22);
            this.chkBox.TabIndex = 1;
            this.chkBox.UseVisualStyleBackColor = false;
            // 
            // flowPanel
            // 
            this.flowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanel.Location = new System.Drawing.Point(0, 0);
            this.flowPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flowPanel.Name = "flowPanel";
            this.flowPanel.Size = new System.Drawing.Size(217, 30);
            this.flowPanel.TabIndex = 2;
            // 
            // PalletNumListRowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.flowPanel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "PalletNumListRowControl";
            this.Size = new System.Drawing.Size(217, 30);
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
