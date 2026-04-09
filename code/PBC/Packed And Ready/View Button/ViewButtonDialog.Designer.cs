namespace PitneyBowesCalculator.Packed_And_Ready.View_Button
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
            pnlHeader = new Krypton.Toolkit.KryptonPanel();
            btnExit = new Krypton.Toolkit.KryptonButton();
            pnlDashboard = new System.Windows.Forms.Panel();
            txtPackedTime = new Krypton.Toolkit.KryptonLabel();
            txtTrayCount = new Krypton.Toolkit.KryptonLabel();
            txtScannedWO = new Krypton.Toolkit.KryptonLabel();
            txtEnvelopeQty = new Krypton.Toolkit.KryptonLabel();
            lblPackedTime = new Krypton.Toolkit.KryptonLabel();
            lblEnvelopeQty = new Krypton.Toolkit.KryptonLabel();
            lblScannedWO = new Krypton.Toolkit.KryptonLabel();
            lblTrayCount = new Krypton.Toolkit.KryptonLabel();
            txtPBJobNumber = new Krypton.Toolkit.KryptonLabel();
            txtPackDate = new Krypton.Toolkit.KryptonLabel();
            lblPackDate = new Krypton.Toolkit.KryptonLabel();
            txtPBJobName = new Krypton.Toolkit.KryptonLabel();
            pnlPalletNum = new System.Windows.Forms.Panel();
            lvPallet = new PalletNumListViewList();
            pnlDetails = new System.Windows.Forms.Panel();
            lvPalletDetails = new PitneyBowesCalculator.Packed_And_Ready.View_Button.Pallets_Details.PalletDetailsListView();
            groupBox1 = new System.Windows.Forms.GroupBox();
            lbStatus = new Krypton.Toolkit.KryptonLabel();
            pbSpinner = new System.Windows.Forms.PictureBox();
            btnRemovePallet = new Krypton.Toolkit.KryptonButton();
            btnPrintPallet = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)pnlHeader).BeginInit();
            pnlHeader.SuspendLayout();
            pnlDashboard.SuspendLayout();
            pnlPalletNum.SuspendLayout();
            pnlDetails.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbSpinner).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(btnExit);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(3, 19);
            pnlHeader.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new System.Drawing.Size(845, 30);
            pnlHeader.StateNormal.Color1 = System.Drawing.Color.Transparent;
            pnlHeader.StateNormal.Color2 = System.Drawing.Color.Transparent;
            pnlHeader.TabIndex = 54;
            // 
            // btnExit
            // 
            btnExit.Anchor = System.Windows.Forms.AnchorStyles.None;
            btnExit.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            btnExit.Location = new System.Drawing.Point(812, 0);
            btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            btnExit.Name = "btnExit";
            btnExit.Size = new System.Drawing.Size(24, 20);
            btnExit.StateCommon.Back.Color1 = System.Drawing.Color.Transparent;
            btnExit.StateCommon.Back.Color2 = System.Drawing.Color.Transparent;
            btnExit.StateNormal.Back.Color1 = System.Drawing.Color.Transparent;
            btnExit.StateNormal.Back.Color2 = System.Drawing.Color.Transparent;
            btnExit.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnExit.StateTracking.Back.Color1 = System.Drawing.Color.DarkGray;
            btnExit.StateTracking.Back.Color2 = System.Drawing.Color.DarkGray;
            btnExit.TabIndex = 0;
            btnExit.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            btnExit.Values.Image = Properties.Resources.close_img;
            btnExit.Values.Text = "";
            btnExit.Click += btnExit_Click_1;
            // 
            // pnlDashboard
            // 
            pnlDashboard.Controls.Add(txtPackedTime);
            pnlDashboard.Controls.Add(txtTrayCount);
            pnlDashboard.Controls.Add(txtScannedWO);
            pnlDashboard.Controls.Add(txtEnvelopeQty);
            pnlDashboard.Controls.Add(lblPackedTime);
            pnlDashboard.Controls.Add(lblEnvelopeQty);
            pnlDashboard.Controls.Add(lblScannedWO);
            pnlDashboard.Controls.Add(lblTrayCount);
            pnlDashboard.Location = new System.Drawing.Point(238, 164);
            pnlDashboard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            pnlDashboard.Name = "pnlDashboard";
            pnlDashboard.Size = new System.Drawing.Size(592, 57);
            pnlDashboard.TabIndex = 58;
            // 
            // txtPackedTime
            // 
            txtPackedTime.AutoSize = false;
            txtPackedTime.Location = new System.Drawing.Point(501, 32);
            txtPackedTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            txtPackedTime.Name = "txtPackedTime";
            txtPackedTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            txtPackedTime.Size = new System.Drawing.Size(85, 22);
            txtPackedTime.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            txtPackedTime.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            txtPackedTime.TabIndex = 59;
            txtPackedTime.UseWaitCursor = true;
            txtPackedTime.Values.Text = "--:-- AM";
            // 
            // txtTrayCount
            // 
            txtTrayCount.Location = new System.Drawing.Point(556, 4);
            txtTrayCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            txtTrayCount.Name = "txtTrayCount";
            txtTrayCount.Size = new System.Drawing.Size(21, 26);
            txtTrayCount.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            txtTrayCount.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 12F);
            txtTrayCount.TabIndex = 8;
            txtTrayCount.Values.Text = "0";
            // 
            // txtScannedWO
            // 
            txtScannedWO.Location = new System.Drawing.Point(136, 32);
            txtScannedWO.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            txtScannedWO.Name = "txtScannedWO";
            txtScannedWO.Size = new System.Drawing.Size(21, 26);
            txtScannedWO.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            txtScannedWO.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            txtScannedWO.TabIndex = 7;
            txtScannedWO.Values.Text = "0";
            // 
            // txtEnvelopeQty
            // 
            txtEnvelopeQty.Location = new System.Drawing.Point(136, 4);
            txtEnvelopeQty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            txtEnvelopeQty.Name = "txtEnvelopeQty";
            txtEnvelopeQty.Size = new System.Drawing.Size(21, 26);
            txtEnvelopeQty.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            txtEnvelopeQty.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            txtEnvelopeQty.TabIndex = 6;
            txtEnvelopeQty.Values.Text = "0";
            // 
            // lblPackedTime
            // 
            lblPackedTime.AutoSize = false;
            lblPackedTime.Location = new System.Drawing.Point(253, 32);
            lblPackedTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            lblPackedTime.Name = "lblPackedTime";
            lblPackedTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            lblPackedTime.Size = new System.Drawing.Size(254, 22);
            lblPackedTime.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            lblPackedTime.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            lblPackedTime.TabIndex = 4;
            lblPackedTime.Values.Text = "Packed Time :";
            // 
            // lblEnvelopeQty
            // 
            lblEnvelopeQty.Location = new System.Drawing.Point(4, 4);
            lblEnvelopeQty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            lblEnvelopeQty.Name = "lblEnvelopeQty";
            lblEnvelopeQty.Size = new System.Drawing.Size(120, 26);
            lblEnvelopeQty.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            lblEnvelopeQty.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            lblEnvelopeQty.TabIndex = 1;
            lblEnvelopeQty.Values.Text = "Envelope Qty :";
            // 
            // lblScannedWO
            // 
            lblScannedWO.Location = new System.Drawing.Point(4, 32);
            lblScannedWO.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            lblScannedWO.Name = "lblScannedWO";
            lblScannedWO.Size = new System.Drawing.Size(116, 26);
            lblScannedWO.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            lblScannedWO.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            lblScannedWO.TabIndex = 2;
            lblScannedWO.Values.Text = "Scanned WO :";
            // 
            // lblTrayCount
            // 
            lblTrayCount.Location = new System.Drawing.Point(440, 4);
            lblTrayCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            lblTrayCount.Name = "lblTrayCount";
            lblTrayCount.Size = new System.Drawing.Size(102, 26);
            lblTrayCount.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            lblTrayCount.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            lblTrayCount.TabIndex = 3;
            lblTrayCount.Values.Text = "Tray Count :";
            // 
            // txtPBJobNumber
            // 
            txtPBJobNumber.Location = new System.Drawing.Point(28, 116);
            txtPBJobNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            txtPBJobNumber.Name = "txtPBJobNumber";
            txtPBJobNumber.Size = new System.Drawing.Size(74, 34);
            txtPBJobNumber.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            txtPBJobNumber.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            txtPBJobNumber.TabIndex = 53;
            txtPBJobNumber.Values.Text = "00000";
            // 
            // txtPackDate
            // 
            txtPackDate.AutoSize = false;
            txtPackDate.Location = new System.Drawing.Point(648, 123);
            txtPackDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            txtPackDate.Name = "txtPackDate";
            txtPackDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            txtPackDate.Size = new System.Drawing.Size(182, 22);
            txtPackDate.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            txtPackDate.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic);
            txtPackDate.TabIndex = 52;
            txtPackDate.Values.Text = "--/--/----";
            // 
            // lblPackDate
            // 
            lblPackDate.AutoSize = false;
            lblPackDate.Location = new System.Drawing.Point(577, 100);
            lblPackDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            lblPackDate.Name = "lblPackDate";
            lblPackDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            lblPackDate.Size = new System.Drawing.Size(256, 22);
            lblPackDate.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            lblPackDate.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            lblPackDate.TabIndex = 51;
            lblPackDate.Values.Text = "Pack Date";
            // 
            // txtPBJobName
            // 
            txtPBJobName.Location = new System.Drawing.Point(10, 51);
            txtPBJobName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            txtPBJobName.Name = "txtPBJobName";
            txtPBJobName.Size = new System.Drawing.Size(259, 62);
            txtPBJobName.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            txtPBJobName.StateCommon.ShortText.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            txtPBJobName.TabIndex = 50;
            txtPBJobName.Values.Text = "Job Name";
            // 
            // pnlPalletNum
            // 
            pnlPalletNum.Controls.Add(lvPallet);
            pnlPalletNum.Location = new System.Drawing.Point(28, 164);
            pnlPalletNum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            pnlPalletNum.Name = "pnlPalletNum";
            pnlPalletNum.Size = new System.Drawing.Size(206, 426);
            pnlPalletNum.TabIndex = 60;
            // 
            // lvPallet
            // 
            lvPallet.Location = new System.Drawing.Point(5, 4);
            lvPallet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            lvPallet.Name = "lvPallet";
            lvPallet.Size = new System.Drawing.Size(196, 416);
            lvPallet.TabIndex = 0;
            // 
            // pnlDetails
            // 
            pnlDetails.BackColor = System.Drawing.Color.White;
            pnlDetails.Controls.Add(lvPalletDetails);
            pnlDetails.Location = new System.Drawing.Point(237, 221);
            pnlDetails.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            pnlDetails.Name = "pnlDetails";
            pnlDetails.Size = new System.Drawing.Size(592, 368);
            pnlDetails.TabIndex = 5;
            // 
            // lvPalletDetails
            // 
            lvPalletDetails.Location = new System.Drawing.Point(5, 4);
            lvPalletDetails.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            lvPalletDetails.Name = "lvPalletDetails";
            lvPalletDetails.Size = new System.Drawing.Size(582, 357);
            lvPalletDetails.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lbStatus);
            groupBox1.Controls.Add(pbSpinner);
            groupBox1.Controls.Add(btnRemovePallet);
            groupBox1.Controls.Add(btnPrintPallet);
            groupBox1.Controls.Add(pnlPalletNum);
            groupBox1.Controls.Add(pnlHeader);
            groupBox1.Controls.Add(pnlDetails);
            groupBox1.Controls.Add(txtPBJobNumber);
            groupBox1.Controls.Add(txtPackDate);
            groupBox1.Controls.Add(pnlDashboard);
            groupBox1.Controls.Add(lblPackDate);
            groupBox1.Controls.Add(txtPBJobName);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox1.Location = new System.Drawing.Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(851, 638);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            // 
            // lbStatus
            // 
            lbStatus.Location = new System.Drawing.Point(63, 599);
            lbStatus.Name = "lbStatus";
            lbStatus.Size = new System.Drawing.Size(117, 26);
            lbStatus.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lbStatus.TabIndex = 64;
            lbStatus.Values.Text = "kryptonLabel1";
            lbStatus.Visible = false;
            // 
            // pbSpinner
            // 
            pbSpinner.Location = new System.Drawing.Point(28, 596);
            pbSpinner.Name = "pbSpinner";
            pbSpinner.Size = new System.Drawing.Size(29, 30);
            pbSpinner.TabIndex = 63;
            pbSpinner.TabStop = false;
            pbSpinner.Visible = false;
            // 
            // btnRemovePallet
            // 
            btnRemovePallet.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            btnRemovePallet.Location = new System.Drawing.Point(374, 596);
            btnRemovePallet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            btnRemovePallet.Name = "btnRemovePallet";
            btnRemovePallet.Size = new System.Drawing.Size(240, 34);
            btnRemovePallet.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(179, 38, 30);
            btnRemovePallet.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnRemovePallet.StateCommon.Border.Rounding = 5F;
            btnRemovePallet.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            btnRemovePallet.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnRemovePallet.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnRemovePallet.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnRemovePallet.TabIndex = 62;
            btnRemovePallet.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            btnRemovePallet.Values.Text = "Remove Selected Pallet/s";
            btnRemovePallet.Click += btnRemovePallet_Click_1;
            // 
            // btnPrintPallet
            // 
            btnPrintPallet.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            btnPrintPallet.Location = new System.Drawing.Point(632, 596);
            btnPrintPallet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            btnPrintPallet.Name = "btnPrintPallet";
            btnPrintPallet.Size = new System.Drawing.Size(198, 34);
            btnPrintPallet.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(103, 80, 164);
            btnPrintPallet.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnPrintPallet.StateCommon.Border.Rounding = 5F;
            btnPrintPallet.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            btnPrintPallet.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnPrintPallet.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnPrintPallet.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnPrintPallet.TabIndex = 61;
            btnPrintPallet.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            btnPrintPallet.Values.Text = "Print Selected Pallet/s";
            btnPrintPallet.Click += btnPrintPallet_Click_1;
            // 
            // ViewButtonDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(851, 638);
            Controls.Add(groupBox1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Name = "ViewButtonDialog";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "ViewButtonDialog";
            ((System.ComponentModel.ISupportInitialize)pnlHeader).EndInit();
            pnlHeader.ResumeLayout(false);
            pnlDashboard.ResumeLayout(false);
            pnlDashboard.PerformLayout();
            pnlPalletNum.ResumeLayout(false);
            pnlDetails.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbSpinner).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel pnlHeader;
        private System.Windows.Forms.Panel pnlDashboard;
        private Krypton.Toolkit.KryptonLabel lblPackedTime;
        private Krypton.Toolkit.KryptonLabel lblEnvelopeQty;
        private Krypton.Toolkit.KryptonLabel lblScannedWO;
        private Krypton.Toolkit.KryptonLabel lblTrayCount;

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
        private System.Windows.Forms.Panel pnlDetails;
        private Pallets_Details.PalletDetailsListView lvPalletDetails;
        private System.Windows.Forms.Panel pnlPalletNum;
        private System.Windows.Forms.GroupBox groupBox1;
        private Krypton.Toolkit.KryptonButton btnPrintPallets;
        private Krypton.Toolkit.KryptonButton btnRemovePallets;
        private Krypton.Toolkit.Suite.Extended.Panels.KryptonButtonPanel kryptonButtonPanel1;
        private Krypton.Toolkit.KryptonButton btnPrintPallet;
        private Krypton.Toolkit.KryptonButton btnRemovePallet;
        private System.Windows.Forms.PictureBox pbSpinner;
        private Krypton.Toolkit.KryptonLabel lbStatus;
    }
}