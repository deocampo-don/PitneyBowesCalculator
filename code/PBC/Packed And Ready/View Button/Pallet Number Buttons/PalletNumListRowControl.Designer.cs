namespace PitneyBowesCalculator.Packed_And_Ready.View_Button
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
            btnPalletNum = new System.Windows.Forms.Button();
            pnlMain = new System.Windows.Forms.Panel();
            chkBox = new System.Windows.Forms.CheckBox();
            flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            pnlMain.SuspendLayout();
            SuspendLayout();
            // 
            // btnPalletNum
            // 
            btnPalletNum.BackColor = System.Drawing.Color.White;
            btnPalletNum.Cursor = System.Windows.Forms.Cursors.Hand;
            btnPalletNum.Dock = System.Windows.Forms.DockStyle.Fill;
            btnPalletNum.FlatAppearance.BorderSize = 0;
            btnPalletNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnPalletNum.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnPalletNum.ForeColor = System.Drawing.Color.Black;
            btnPalletNum.Location = new System.Drawing.Point(0, 0);
            btnPalletNum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            btnPalletNum.Name = "btnPalletNum";
            btnPalletNum.Size = new System.Drawing.Size(190, 35);
            btnPalletNum.TabIndex = 0;
            btnPalletNum.Text = "Pallet #";
            btnPalletNum.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            btnPalletNum.UseVisualStyleBackColor = false;
            btnPalletNum.Click += btnPalletNum_Click;
            // 
            // pnlMain
            // 
            pnlMain.BackColor = System.Drawing.Color.White;
            pnlMain.Controls.Add(chkBox);
            pnlMain.Controls.Add(btnPalletNum);
            pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlMain.Location = new System.Drawing.Point(0, 0);
            pnlMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new System.Drawing.Size(190, 35);
            pnlMain.TabIndex = 1;
            // 
            // chkBox
            // 
            chkBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            chkBox.BackColor = System.Drawing.Color.Transparent;
            chkBox.Cursor = System.Windows.Forms.Cursors.Hand;
            chkBox.Location = new System.Drawing.Point(40, 4);
            chkBox.Margin = new System.Windows.Forms.Padding(4);
            chkBox.Name = "chkBox";
            chkBox.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            chkBox.Size = new System.Drawing.Size(17, 21);
            chkBox.TabIndex = 1;
            chkBox.UseVisualStyleBackColor = false;
            // 
            // flowPanel
            // 
            flowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            flowPanel.Location = new System.Drawing.Point(0, 0);
            flowPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            flowPanel.Name = "flowPanel";
            flowPanel.Size = new System.Drawing.Size(190, 35);
            flowPanel.TabIndex = 2;
            // 
            // PalletNumListRowControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(pnlMain);
            Controls.Add(flowPanel);
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Name = "PalletNumListRowControl";
            Size = new System.Drawing.Size(190, 35);
            pnlMain.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPalletNum;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.FlowLayoutPanel flowPanel;
        private System.Windows.Forms.CheckBox chkBox;
    }
}
