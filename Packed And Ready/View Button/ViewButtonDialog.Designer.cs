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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewButtonDialog));
            this.pnlHeader = new Krypton.Toolkit.KryptonPanel();
            this.btnExit = new Krypton.Toolkit.KryptonButton();
            this.pnlDashboard = new System.Windows.Forms.Panel();
            this.txtPackedTime = new Krypton.Toolkit.KryptonLabel();
            this.txtTrayCount = new Krypton.Toolkit.KryptonLabel();
            this.txtScannedWO = new Krypton.Toolkit.KryptonLabel();
            this.txtEnvelopeQty = new Krypton.Toolkit.KryptonLabel();
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
            this.kryptonGroup1 = new Krypton.Toolkit.KryptonGroup();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.kryptonPrintDialog1 = new Krypton.Toolkit.KryptonPrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.pnlPalletNum = new System.Windows.Forms.Panel();
            this.lvPallet = new WindowsFormsApp1.Packed_And_Ready.View_Button.PalletNumListViewList();
            this.lvPalletDetails = new WindowsFormsApp1.Packed_And_Ready.View_Button.Pallets_Details.PalletDetailsListView();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.pnlDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1.Panel)).BeginInit();
            this.kryptonGroup1.Panel.SuspendLayout();
            this.kryptonGroup1.SuspendLayout();
            this.pnlDetails.SuspendLayout();
            this.pnlPalletNum.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.btnExit);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(967, 46);
            this.pnlHeader.StateNormal.Color1 = System.Drawing.Color.Transparent;
            this.pnlHeader.StateNormal.Color2 = System.Drawing.Color.Transparent;
            this.pnlHeader.TabIndex = 54;
            // 
            // btnExit
            // 
            this.btnExit.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(904, 12);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(28, 25);
            this.btnExit.StateCommon.Back.Color1 = System.Drawing.Color.Transparent;
            this.btnExit.StateCommon.Back.Color2 = System.Drawing.Color.Transparent;
            this.btnExit.StateNormal.Back.Color1 = System.Drawing.Color.Transparent;
            this.btnExit.StateNormal.Back.Color2 = System.Drawing.Color.Transparent;
            this.btnExit.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.btnExit.StateTracking.Back.Color1 = System.Drawing.Color.DarkGray;
            this.btnExit.StateTracking.Back.Color2 = System.Drawing.Color.DarkGray;
            this.btnExit.TabIndex = 0;
            this.btnExit.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnExit.Values.Image = global::WindowsFormsApp1.Properties.Resources.close_img;
            this.btnExit.Values.Text = "";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click_1);
            // 
            // pnlDashboard
            // 
            this.pnlDashboard.Controls.Add(this.txtPackedTime);
            this.pnlDashboard.Controls.Add(this.txtTrayCount);
            this.pnlDashboard.Controls.Add(this.txtScannedWO);
            this.pnlDashboard.Controls.Add(this.txtEnvelopeQty);
            this.pnlDashboard.Controls.Add(this.lblPackedTime);
            this.pnlDashboard.Controls.Add(this.lblEnvelopeQty);
            this.pnlDashboard.Controls.Add(this.lblScannedWO);
            this.pnlDashboard.Controls.Add(this.lblTrayCount);
            this.pnlDashboard.Location = new System.Drawing.Point(270, 173);
            this.pnlDashboard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlDashboard.Name = "pnlDashboard";
            this.pnlDashboard.Size = new System.Drawing.Size(677, 61);
            this.pnlDashboard.TabIndex = 58;
            // 
            // txtPackedTime
            // 
            this.txtPackedTime.Location = new System.Drawing.Point(587, 34);
            this.txtPackedTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPackedTime.Name = "txtPackedTime";
            this.txtPackedTime.Size = new System.Drawing.Size(68, 24);
            this.txtPackedTime.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.txtPackedTime.TabIndex = 59;
            this.txtPackedTime.Values.Text = "--:-- AM";
            // 
            // txtTrayCount
            // 
            this.txtTrayCount.Location = new System.Drawing.Point(635, 5);
            this.txtTrayCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTrayCount.Name = "txtTrayCount";
            this.txtTrayCount.Size = new System.Drawing.Size(20, 24);
            this.txtTrayCount.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.txtTrayCount.TabIndex = 8;
            this.txtTrayCount.Values.Text = "0";
            // 
            // txtScannedWO
            // 
            this.txtScannedWO.Location = new System.Drawing.Point(112, 34);
            this.txtScannedWO.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtScannedWO.Name = "txtScannedWO";
            this.txtScannedWO.Size = new System.Drawing.Size(20, 24);
            this.txtScannedWO.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.txtScannedWO.TabIndex = 7;
            this.txtScannedWO.Values.Text = "0";
            // 
            // txtEnvelopeQty
            // 
            this.txtEnvelopeQty.Location = new System.Drawing.Point(112, 5);
            this.txtEnvelopeQty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtEnvelopeQty.Name = "txtEnvelopeQty";
            this.txtEnvelopeQty.Size = new System.Drawing.Size(20, 24);
            this.txtEnvelopeQty.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.txtEnvelopeQty.TabIndex = 6;
            this.txtEnvelopeQty.Values.Text = "0";
            // 
            // lblPackedTime
            // 
            this.lblPackedTime.AutoSize = false;
            this.lblPackedTime.Location = new System.Drawing.Point(356, 34);
            this.lblPackedTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblPackedTime.Name = "lblPackedTime";
            this.lblPackedTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblPackedTime.Size = new System.Drawing.Size(238, 24);
            this.lblPackedTime.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.lblPackedTime.TabIndex = 4;
            this.lblPackedTime.Values.Text = "Packed Time :";
            // 
            // lblEnvelopeQty
            // 
            this.lblEnvelopeQty.Location = new System.Drawing.Point(5, 5);
            this.lblEnvelopeQty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblEnvelopeQty.Name = "lblEnvelopeQty";
            this.lblEnvelopeQty.Size = new System.Drawing.Size(110, 24);
            this.lblEnvelopeQty.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.lblEnvelopeQty.TabIndex = 1;
            this.lblEnvelopeQty.Values.Text = "Envelope Qty :";
            // 
            // lblScannedWO
            // 
            this.lblScannedWO.Location = new System.Drawing.Point(5, 34);
            this.lblScannedWO.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblScannedWO.Name = "lblScannedWO";
            this.lblScannedWO.Size = new System.Drawing.Size(107, 24);
            this.lblScannedWO.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.lblScannedWO.TabIndex = 2;
            this.lblScannedWO.Values.Text = "Scanned WO :";
            // 
            // lblTrayCount
            // 
            this.lblTrayCount.Location = new System.Drawing.Point(543, 5);
            this.lblTrayCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.btnPrintPallets.Location = new System.Drawing.Point(738, 637);
            this.btnPrintPallets.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrintPallets.Name = "btnPrintPallets";
            this.btnPrintPallets.Size = new System.Drawing.Size(208, 28);
            this.btnPrintPallets.TabIndex = 56;
            this.btnPrintPallets.Text = "Print Selected Pallet/s";
            this.btnPrintPallets.UseVisualStyleBackColor = false;
            this.btnPrintPallets.Click += new System.EventHandler(this.btnPrintPallets_Click);
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
            this.btnRemovePallets.Location = new System.Drawing.Point(526, 637);
            this.btnRemovePallets.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRemovePallets.Name = "btnRemovePallets";
            this.btnRemovePallets.Size = new System.Drawing.Size(208, 28);
            this.btnRemovePallets.TabIndex = 57;
            this.btnRemovePallets.Text = "Remove Selected Pallet/s";
            this.btnRemovePallets.UseVisualStyleBackColor = false;
            this.btnRemovePallets.Click += new System.EventHandler(this.btnRemovePallets_Click);
            // 
            // txtPBJobNumber
            // 
            this.txtPBJobNumber.Location = new System.Drawing.Point(30, 122);
            this.txtPBJobNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPBJobNumber.Name = "txtPBJobNumber";
            this.txtPBJobNumber.Size = new System.Drawing.Size(90, 42);
            this.txtPBJobNumber.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.txtPBJobNumber.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPBJobNumber.TabIndex = 53;
            this.txtPBJobNumber.Values.Text = "00000";
            // 
            // txtPackDate
            // 
            this.txtPackDate.AutoSize = false;
            this.txtPackDate.Location = new System.Drawing.Point(738, 130);
            this.txtPackDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPackDate.Name = "txtPackDate";
            this.txtPackDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPackDate.Size = new System.Drawing.Size(208, 24);
            this.txtPackDate.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.txtPackDate.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackDate.TabIndex = 52;
            this.txtPackDate.Values.Text = "--/--/----";
            // 
            // lblPackDate
            // 
            this.lblPackDate.AutoSize = false;
            this.lblPackDate.Location = new System.Drawing.Point(738, 106);
            this.lblPackDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblPackDate.Name = "lblPackDate";
            this.lblPackDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblPackDate.Size = new System.Drawing.Size(211, 24);
            this.lblPackDate.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.lblPackDate.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPackDate.TabIndex = 51;
            this.lblPackDate.Values.Text = "Pack Date";
            // 
            // txtPBJobName
            // 
            this.txtPBJobName.Location = new System.Drawing.Point(3, 50);
            this.txtPBJobName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPBJobName.Name = "txtPBJobName";
            this.txtPBJobName.Size = new System.Drawing.Size(322, 77);
            this.txtPBJobName.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.txtPBJobName.StateCommon.ShortText.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPBJobName.TabIndex = 50;
            this.txtPBJobName.Values.Text = "Job Name";
            // 
            // kryptonGroup1
            // 
            this.kryptonGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroup1.Location = new System.Drawing.Point(0, 0);
            // 
            // kryptonGroup1.Panel
            // 
            this.kryptonGroup1.Panel.Controls.Add(this.pnlPalletNum);
            this.kryptonGroup1.Panel.Controls.Add(this.pnlDetails);
            this.kryptonGroup1.Panel.Controls.Add(this.btnClose);
            this.kryptonGroup1.Panel.Controls.Add(this.pnlHeader);
            this.kryptonGroup1.Panel.Controls.Add(this.pnlDashboard);
            this.kryptonGroup1.Panel.Controls.Add(this.txtPBJobName);
            this.kryptonGroup1.Panel.Controls.Add(this.btnPrintPallets);
            this.kryptonGroup1.Panel.Controls.Add(this.lblPackDate);
            this.kryptonGroup1.Panel.Controls.Add(this.btnRemovePallets);
            this.kryptonGroup1.Panel.Controls.Add(this.txtPackDate);
            this.kryptonGroup1.Panel.Controls.Add(this.txtPBJobNumber);
            this.kryptonGroup1.Size = new System.Drawing.Size(973, 681);
            this.kryptonGroup1.StateCommon.Back.Color1 = System.Drawing.Color.Transparent;
            this.kryptonGroup1.StateCommon.Back.Color2 = System.Drawing.Color.Transparent;
            this.kryptonGroup1.StateCommon.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonGroup1.StateCommon.Border.Color1 = System.Drawing.Color.Gray;
            this.kryptonGroup1.StateCommon.Border.Color2 = System.Drawing.Color.Gray;
            this.kryptonGroup1.StateCommon.Border.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonGroup1.StateCommon.Border.Rounding = 7F;
            this.kryptonGroup1.TabIndex = 0;
            // 
            // pnlDetails
            // 
            this.pnlDetails.BackColor = System.Drawing.Color.White;
            this.pnlDetails.Controls.Add(this.lvPalletDetails);
            this.pnlDetails.Location = new System.Drawing.Point(269, 235);
            this.pnlDetails.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(677, 392);
            this.pnlDetails.TabIndex = 5;
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(80)))), ((int)(((byte)(164)))));
            this.btnClose.CausesValidation = false;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(848, 637);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(84, 28);
            this.btnClose.TabIndex = 59;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // kryptonPrintDialog1
            // 
            this.kryptonPrintDialog1.AllowPrintToFile = false;
            this.kryptonPrintDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("kryptonPrintDialog1.Icon")));
            this.kryptonPrintDialog1.Title = null;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // pnlPalletNum
            // 
            this.pnlPalletNum.Controls.Add(this.lvPallet);
            this.pnlPalletNum.Location = new System.Drawing.Point(30, 173);
            this.pnlPalletNum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlPalletNum.Name = "pnlPalletNum";
            this.pnlPalletNum.Size = new System.Drawing.Size(236, 454);
            this.pnlPalletNum.TabIndex = 60;
            // 
            // lvPallet
            // 
            this.lvPallet.Location = new System.Drawing.Point(6, 5);
            this.lvPallet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvPallet.Name = "lvPallet";
            this.lvPallet.Size = new System.Drawing.Size(224, 443);
            this.lvPallet.TabIndex = 0;
            // 
            // lvPalletDetails
            // 
            this.lvPalletDetails.Location = new System.Drawing.Point(6, 5);
            this.lvPalletDetails.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvPalletDetails.Name = "lvPalletDetails";
            this.lvPalletDetails.Size = new System.Drawing.Size(665, 381);
            this.lvPalletDetails.TabIndex = 0;
            // 
            // ViewButtonDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(973, 681);
            this.Controls.Add(this.kryptonGroup1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ViewButtonDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewButtonDialog";
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlDashboard.ResumeLayout(false);
            this.pnlDashboard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1.Panel)).EndInit();
            this.kryptonGroup1.Panel.ResumeLayout(false);
            this.kryptonGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).EndInit();
            this.kryptonGroup1.ResumeLayout(false);
            this.pnlDetails.ResumeLayout(false);
            this.pnlPalletNum.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel pnlHeader;
        private System.Windows.Forms.Panel pnlDashboard;
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
        private Krypton.Toolkit.KryptonButton btnExit;
        private Krypton.Toolkit.KryptonLabel txtPackedTime;
        private Krypton.Toolkit.KryptonLabel txtTrayCount;
        private Krypton.Toolkit.KryptonLabel txtScannedWO;
        private Krypton.Toolkit.KryptonLabel txtEnvelopeQty;
        private PalletNumListViewList lvPallet;
        private Krypton.Toolkit.KryptonGroup kryptonGroup1;
        private System.Windows.Forms.Button btnClose;
        private Krypton.Toolkit.KryptonPrintDialog kryptonPrintDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Panel pnlDetails;
        private Pallets_Details.PalletDetailsListView lvPalletDetails;
        private System.Windows.Forms.Panel pnlPalletNum;
    }
}