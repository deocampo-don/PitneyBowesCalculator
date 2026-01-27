namespace WindowsFormsApp1.Packed_And_Ready.View_Button
{
    partial class ViewButtonDialog
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
            this.pnlHeader = new Krypton.Toolkit.KryptonPanel();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.btnExit = new Krypton.Toolkit.KryptonButton();
            this.pnlDashboard = new System.Windows.Forms.Panel();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.lblPackedTime = new Krypton.Toolkit.KryptonLabel();
            this.lblEnvelopeQty = new Krypton.Toolkit.KryptonLabel();
            this.lblScannedWO = new Krypton.Toolkit.KryptonLabel();
            this.lblTrayCount = new Krypton.Toolkit.KryptonLabel();
            this.btnPrintPallets = new System.Windows.Forms.Button();
            this.btnRemovePallets = new System.Windows.Forms.Button();
            this.txtPBJobNumber = new Krypton.Toolkit.KryptonLabel();
            this.txtPackDate = new Krypton.Toolkit.KryptonLabel();
            this.lblPackDate = new Krypton.Toolkit.KryptonLabel();
            this.txtPBJobName = new Krypton.Toolkit.KryptonLabel();
            this.pnlPalletNoList = new Krypton.Toolkit.KryptonPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.pnlDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPalletNoList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.kryptonButton1);
            this.pnlHeader.Controls.Add(this.btnExit);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(974, 45);
            this.pnlHeader.StateNormal.Color1 = System.Drawing.Color.White;
            this.pnlHeader.StateNormal.Color2 = System.Drawing.Color.White;
            this.pnlHeader.TabIndex = 54;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            this.kryptonButton1.Location = new System.Drawing.Point(855, 12);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(28, 25);
            this.kryptonButton1.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.kryptonButton1.StateCommon.Back.Color2 = System.Drawing.Color.White;
            this.kryptonButton1.StateTracking.Back.Color1 = System.Drawing.Color.DarkGray;
            this.kryptonButton1.StateTracking.Back.Color2 = System.Drawing.Color.DarkGray;
            this.kryptonButton1.TabIndex = 62;
            this.kryptonButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton1.Values.Image = global::WindowsFormsApp1.Properties.Resources.close_img;
            this.kryptonButton1.Values.Text = "";
            // 
            // btnExit
            // 
            this.btnExit.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            this.btnExit.Location = new System.Drawing.Point(904, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(28, 25);
            this.btnExit.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.btnExit.StateCommon.Back.Color2 = System.Drawing.Color.White;
            this.btnExit.StateTracking.Back.Color1 = System.Drawing.Color.DarkGray;
            this.btnExit.StateTracking.Back.Color2 = System.Drawing.Color.DarkGray;
            this.btnExit.TabIndex = 61;
            this.btnExit.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnExit.Values.Image = global::WindowsFormsApp1.Properties.Resources.close_img;
            this.btnExit.Values.Text = "";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click_1);
            // 
            // pnlDashboard
            // 
            this.pnlDashboard.Controls.Add(this.pnlDetails);
            this.pnlDashboard.Controls.Add(this.lblPackedTime);
            this.pnlDashboard.Controls.Add(this.lblEnvelopeQty);
            this.pnlDashboard.Controls.Add(this.lblScannedWO);
            this.pnlDashboard.Controls.Add(this.lblTrayCount);
            this.pnlDashboard.Location = new System.Drawing.Point(270, 180);
            this.pnlDashboard.Name = "pnlDashboard";
            this.pnlDashboard.Size = new System.Drawing.Size(677, 458);
            this.pnlDashboard.TabIndex = 58;
            // 
            // pnlDetails
            // 
            this.pnlDetails.BackColor = System.Drawing.Color.White;
            this.pnlDetails.Location = new System.Drawing.Point(5, 65);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(666, 388);
            this.pnlDetails.TabIndex = 5;
            // 
            // lblPackedTime
            // 
            this.lblPackedTime.Location = new System.Drawing.Point(486, 35);
            this.lblPackedTime.Name = "lblPackedTime";
            this.lblPackedTime.Size = new System.Drawing.Size(105, 24);
            this.lblPackedTime.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.lblPackedTime.TabIndex = 4;
            this.lblPackedTime.Values.Text = "Packed Time :";
            // 
            // lblEnvelopeQty
            // 
            this.lblEnvelopeQty.Location = new System.Drawing.Point(5, 5);
            this.lblEnvelopeQty.Name = "lblEnvelopeQty";
            this.lblEnvelopeQty.Size = new System.Drawing.Size(110, 24);
            this.lblEnvelopeQty.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.lblEnvelopeQty.TabIndex = 1;
            this.lblEnvelopeQty.Values.Text = "Envelope Qty :";
            // 
            // lblScannedWO
            // 
            this.lblScannedWO.Location = new System.Drawing.Point(5, 35);
            this.lblScannedWO.Name = "lblScannedWO";
            this.lblScannedWO.Size = new System.Drawing.Size(107, 24);
            this.lblScannedWO.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.lblScannedWO.TabIndex = 2;
            this.lblScannedWO.Values.Text = "Scanned WO :";
            // 
            // lblTrayCount
            // 
            this.lblTrayCount.Location = new System.Drawing.Point(543, 5);
            this.lblTrayCount.Name = "lblTrayCount";
            this.lblTrayCount.Size = new System.Drawing.Size(93, 24);
            this.lblTrayCount.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.lblTrayCount.TabIndex = 3;
            this.lblTrayCount.Values.Text = "Tray Count :";
            // 
            // btnPrintPallets
            // 
            this.btnPrintPallets.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnPrintPallets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(80)))), ((int)(((byte)(164)))));
            this.btnPrintPallets.CausesValidation = false;
            this.btnPrintPallets.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintPallets.FlatAppearance.BorderSize = 0;
            this.btnPrintPallets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintPallets.ForeColor = System.Drawing.Color.White;
            this.btnPrintPallets.Location = new System.Drawing.Point(737, 644);
            this.btnPrintPallets.Name = "btnPrintPallets";
            this.btnPrintPallets.Size = new System.Drawing.Size(208, 28);
            this.btnPrintPallets.TabIndex = 56;
            this.btnPrintPallets.Text = "Print Selected Pallet/s";
            this.btnPrintPallets.UseVisualStyleBackColor = false;
            // 
            // btnRemovePallets
            // 
            this.btnRemovePallets.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnRemovePallets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRemovePallets.CausesValidation = false;
            this.btnRemovePallets.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemovePallets.FlatAppearance.BorderSize = 0;
            this.btnRemovePallets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemovePallets.ForeColor = System.Drawing.Color.White;
            this.btnRemovePallets.Location = new System.Drawing.Point(526, 644);
            this.btnRemovePallets.Name = "btnRemovePallets";
            this.btnRemovePallets.Size = new System.Drawing.Size(208, 28);
            this.btnRemovePallets.TabIndex = 57;
            this.btnRemovePallets.Text = "Remove Selected Pallet/s";
            this.btnRemovePallets.UseVisualStyleBackColor = false;
            this.btnRemovePallets.Click += new System.EventHandler(this.btnRemovePallets_Click);
            // 
            // txtPBJobNumber
            // 
            this.txtPBJobNumber.Location = new System.Drawing.Point(26, 119);
            this.txtPBJobNumber.Name = "txtPBJobNumber";
            this.txtPBJobNumber.Size = new System.Drawing.Size(90, 42);
            this.txtPBJobNumber.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.txtPBJobNumber.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPBJobNumber.TabIndex = 53;
            this.txtPBJobNumber.Values.Text = "00000";
            // 
            // txtPackDate
            // 
            this.txtPackDate.Location = new System.Drawing.Point(855, 137);
            this.txtPackDate.Name = "txtPackDate";
            this.txtPackDate.Size = new System.Drawing.Size(90, 24);
            this.txtPackDate.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.txtPackDate.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackDate.TabIndex = 52;
            this.txtPackDate.Values.Text = "01/30/2026";
            // 
            // lblPackDate
            // 
            this.lblPackDate.Location = new System.Drawing.Point(866, 113);
            this.lblPackDate.Name = "lblPackDate";
            this.lblPackDate.Size = new System.Drawing.Size(81, 24);
            this.lblPackDate.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.lblPackDate.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPackDate.TabIndex = 51;
            this.lblPackDate.Values.Text = "Pack Date";
            // 
            // txtPBJobName
            // 
            this.txtPBJobName.Location = new System.Drawing.Point(12, 56);
            this.txtPBJobName.Name = "txtPBJobName";
            this.txtPBJobName.Size = new System.Drawing.Size(322, 77);
            this.txtPBJobName.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.txtPBJobName.StateCommon.ShortText.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPBJobName.TabIndex = 50;
            this.txtPBJobName.Values.Text = "Job Name";
            // 
            // pnlPalletNoList
            // 
            this.pnlPalletNoList.Location = new System.Drawing.Point(26, 180);
            this.pnlPalletNoList.Name = "pnlPalletNoList";
            this.pnlPalletNoList.Size = new System.Drawing.Size(238, 454);
            this.pnlPalletNoList.StateCommon.Color1 = System.Drawing.Color.Transparent;
            this.pnlPalletNoList.StateCommon.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.pnlPalletNoList.TabIndex = 55;
            // 
            // ViewButtonDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 681);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlDashboard);
            this.Controls.Add(this.btnPrintPallets);
            this.Controls.Add(this.btnRemovePallets);
            this.Controls.Add(this.txtPBJobNumber);
            this.Controls.Add(this.txtPackDate);
            this.Controls.Add(this.lblPackDate);
            this.Controls.Add(this.txtPBJobName);
            this.Controls.Add(this.pnlPalletNoList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ViewButtonDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewButtonDialog";
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlDashboard.ResumeLayout(false);
            this.pnlDashboard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPalletNoList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel pnlHeader;
        private System.Windows.Forms.Panel pnlDashboard;
        private System.Windows.Forms.Panel pnlDetails;
        private Krypton.Toolkit.KryptonLabel lblPackedTime;
        private Krypton.Toolkit.KryptonLabel lblEnvelopeQty;
        private Krypton.Toolkit.KryptonLabel lblScannedWO;
        private Krypton.Toolkit.KryptonLabel lblTrayCount;
        private System.Windows.Forms.Button btnPrintPallets;
        private System.Windows.Forms.Button btnRemovePallets;
        private Krypton.Toolkit.KryptonLabel txtPBJobNumber;
        private Krypton.Toolkit.KryptonLabel txtPackDate;
        private Krypton.Toolkit.KryptonLabel lblPackDate;
        private Krypton.Toolkit.KryptonLabel txtPBJobName;
        private Krypton.Toolkit.KryptonPanel pnlPalletNoList;
        private Krypton.Toolkit.KryptonButton btnExit;
        private Krypton.Toolkit.KryptonButton kryptonButton1;
    }
}