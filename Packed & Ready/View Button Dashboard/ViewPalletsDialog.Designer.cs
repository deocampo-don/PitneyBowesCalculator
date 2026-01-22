namespace WindowsFormsApp1.Packed___Ready.View_Button
{
    partial class ViewPalletsDialog
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
            this.btnClose = new Krypton.Toolkit.KryptonButton();
            this.txtJobNumber = new Krypton.Toolkit.KryptonLabel();
            this.txtPackDate = new Krypton.Toolkit.KryptonLabel();
            this.lblPackDate = new Krypton.Toolkit.KryptonLabel();
            this.txtPBJob = new Krypton.Toolkit.KryptonLabel();
            this.pnlDashboard = new Krypton.Toolkit.KryptonPanel();
            this.lblPackedTime = new Krypton.Toolkit.KryptonLabel();
            this.lblTrayCount = new Krypton.Toolkit.KryptonLabel();
            this.lblScannedWO = new Krypton.Toolkit.KryptonLabel();
            this.lblEnvelopQty = new Krypton.Toolkit.KryptonLabel();
            this.pnlDetails = new Krypton.Toolkit.KryptonPanel();
            this.btnPrintSelected = new Krypton.Toolkit.KryptonButton();
            this.btnRemoveSelected = new Krypton.Toolkit.KryptonButton();
            this.pnlPalletsList = new Krypton.Toolkit.KryptonPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlDashboard)).BeginInit();
            this.pnlDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPalletsList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(975, 37);
            this.pnlHeader.StateCommon.Color1 = System.Drawing.Color.Transparent;
            this.pnlHeader.TabIndex = 41;
            // 
            // btnClose
            // 
            this.btnClose.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            this.btnClose.Location = new System.Drawing.Point(934, 7);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(28, 25);
            this.btnClose.StateNormal.Back.Color1 = System.Drawing.Color.Transparent;
            this.btnClose.TabIndex = 26;
            this.btnClose.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnClose.Values.Image = global::WindowsFormsApp1.Properties.Resources.close_img;
            this.btnClose.Values.Text = "";
            // 
            // txtJobNumber
            // 
            this.txtJobNumber.Location = new System.Drawing.Point(26, 110);
            this.txtJobNumber.Name = "txtJobNumber";
            this.txtJobNumber.Size = new System.Drawing.Size(90, 42);
            this.txtJobNumber.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJobNumber.TabIndex = 40;
            this.txtJobNumber.Values.Text = "00000";
            // 
            // txtPackDate
            // 
            this.txtPackDate.Location = new System.Drawing.Point(855, 128);
            this.txtPackDate.Name = "txtPackDate";
            this.txtPackDate.Size = new System.Drawing.Size(90, 24);
            this.txtPackDate.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackDate.TabIndex = 39;
            this.txtPackDate.Values.Text = "01/30/2026";
            // 
            // lblPackDate
            // 
            this.lblPackDate.Location = new System.Drawing.Point(866, 104);
            this.lblPackDate.Name = "lblPackDate";
            this.lblPackDate.Size = new System.Drawing.Size(81, 24);
            this.lblPackDate.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPackDate.TabIndex = 38;
            this.lblPackDate.Values.Text = "Pack Date";
            // 
            // txtPBJob
            // 
            this.txtPBJob.Location = new System.Drawing.Point(12, 47);
            this.txtPBJob.Name = "txtPBJob";
            this.txtPBJob.Size = new System.Drawing.Size(291, 77);
            this.txtPBJob.StateCommon.ShortText.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPBJob.TabIndex = 37;
            this.txtPBJob.Values.Text = "CAPONE";
            // 
            // pnlDashboard
            // 
            this.pnlDashboard.Controls.Add(this.lblPackedTime);
            this.pnlDashboard.Controls.Add(this.lblTrayCount);
            this.pnlDashboard.Controls.Add(this.lblScannedWO);
            this.pnlDashboard.Controls.Add(this.lblEnvelopQty);
            this.pnlDashboard.Location = new System.Drawing.Point(265, 171);
            this.pnlDashboard.Name = "pnlDashboard";
            this.pnlDashboard.Size = new System.Drawing.Size(680, 73);
            this.pnlDashboard.TabIndex = 42;
            // 
            // lblPackedTime
            // 
            this.lblPackedTime.Location = new System.Drawing.Point(486, 33);
            this.lblPackedTime.Name = "lblPackedTime";
            this.lblPackedTime.Size = new System.Drawing.Size(105, 24);
            this.lblPackedTime.TabIndex = 4;
            this.lblPackedTime.Values.Text = "Packed Time :";
            // 
            // lblTrayCount
            // 
            this.lblTrayCount.Location = new System.Drawing.Point(543, 3);
            this.lblTrayCount.Name = "lblTrayCount";
            this.lblTrayCount.Size = new System.Drawing.Size(93, 24);
            this.lblTrayCount.TabIndex = 3;
            this.lblTrayCount.Values.Text = "Tray Count :";
            // 
            // lblScannedWO
            // 
            this.lblScannedWO.Location = new System.Drawing.Point(5, 33);
            this.lblScannedWO.Name = "lblScannedWO";
            this.lblScannedWO.Size = new System.Drawing.Size(107, 24);
            this.lblScannedWO.TabIndex = 2;
            this.lblScannedWO.Values.Text = "Scanned WO :";
            // 
            // lblEnvelopQty
            // 
            this.lblEnvelopQty.Location = new System.Drawing.Point(5, 3);
            this.lblEnvelopQty.Name = "lblEnvelopQty";
            this.lblEnvelopQty.Size = new System.Drawing.Size(110, 24);
            this.lblEnvelopQty.TabIndex = 1;
            this.lblEnvelopQty.Values.Text = "Envelope Qty :";
            // 
            // pnlDetails
            // 
            this.pnlDetails.Location = new System.Drawing.Point(265, 245);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(680, 380);
            this.pnlDetails.StateCommon.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlDetails.TabIndex = 36;
            // 
            // btnPrintSelected
            // 
            this.btnPrintSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintSelected.Location = new System.Drawing.Point(740, 631);
            this.btnPrintSelected.Name = "btnPrintSelected";
            this.btnPrintSelected.Size = new System.Drawing.Size(208, 34);
            this.btnPrintSelected.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(80)))), ((int)(((byte)(164)))));
            this.btnPrintSelected.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(80)))), ((int)(((byte)(164)))));
            this.btnPrintSelected.StateNormal.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(80)))), ((int)(((byte)(164)))));
            this.btnPrintSelected.StateNormal.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnPrintSelected.StateNormal.Border.Rounding = 5F;
            this.btnPrintSelected.StateNormal.Border.Width = 1;
            this.btnPrintSelected.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnPrintSelected.StateNormal.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrintSelected.StateNormal.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.btnPrintSelected.StatePressed.Back.Color1 = System.Drawing.Color.Black;
            this.btnPrintSelected.StatePressed.Border.Rounding = 5F;
            this.btnPrintSelected.StatePressed.Border.Width = 1;
            this.btnPrintSelected.StateTracking.Back.Color1 = System.Drawing.Color.Black;
            this.btnPrintSelected.TabIndex = 45;
            this.btnPrintSelected.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnPrintSelected.Values.Text = "Print Selected Pallet/s";
            // 
            // btnRemoveSelected
            // 
            this.btnRemoveSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemoveSelected.Location = new System.Drawing.Point(524, 631);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new System.Drawing.Size(208, 34);
            this.btnRemoveSelected.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(38)))), ((int)(((byte)(30)))));
            this.btnRemoveSelected.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(38)))), ((int)(((byte)(30)))));
            this.btnRemoveSelected.StateNormal.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(80)))), ((int)(((byte)(164)))));
            this.btnRemoveSelected.StateNormal.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnRemoveSelected.StateNormal.Border.Rounding = 5F;
            this.btnRemoveSelected.StateNormal.Border.Width = 1;
            this.btnRemoveSelected.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnRemoveSelected.StateNormal.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnRemoveSelected.StateNormal.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.btnRemoveSelected.StatePressed.Back.Color1 = System.Drawing.Color.Black;
            this.btnRemoveSelected.StatePressed.Border.Rounding = 5F;
            this.btnRemoveSelected.StatePressed.Border.Width = 1;
            this.btnRemoveSelected.StateTracking.Back.Color1 = System.Drawing.Color.Black;
            this.btnRemoveSelected.TabIndex = 44;
            this.btnRemoveSelected.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnRemoveSelected.Values.Text = "Remove Selected Pallet/s";
            // 
            // pnlPalletsList
            // 
            this.pnlPalletsList.Location = new System.Drawing.Point(26, 171);
            this.pnlPalletsList.Name = "pnlPalletsList";
            this.pnlPalletsList.Size = new System.Drawing.Size(238, 454);
            this.pnlPalletsList.StateCommon.Color1 = System.Drawing.Color.Silver;
            this.pnlPalletsList.TabIndex = 43;
            // 
            // ViewPalletsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 688);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.txtJobNumber);
            this.Controls.Add(this.txtPackDate);
            this.Controls.Add(this.lblPackDate);
            this.Controls.Add(this.txtPBJob);
            this.Controls.Add(this.pnlDashboard);
            this.Controls.Add(this.pnlDetails);
            this.Controls.Add(this.btnPrintSelected);
            this.Controls.Add(this.btnRemoveSelected);
            this.Controls.Add(this.pnlPalletsList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ViewPalletsDialog";
            this.Text = "ViewPalletsDialog";
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlDashboard)).EndInit();
            this.pnlDashboard.ResumeLayout(false);
            this.pnlDashboard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPalletsList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel pnlHeader;
        private Krypton.Toolkit.KryptonButton btnClose;
        private Krypton.Toolkit.KryptonLabel txtJobNumber;
        private Krypton.Toolkit.KryptonLabel txtPackDate;
        private Krypton.Toolkit.KryptonLabel lblPackDate;
        private Krypton.Toolkit.KryptonLabel txtPBJob;
        private Krypton.Toolkit.KryptonPanel pnlDashboard;
        private Krypton.Toolkit.KryptonLabel lblPackedTime;
        private Krypton.Toolkit.KryptonLabel lblTrayCount;
        private Krypton.Toolkit.KryptonLabel lblScannedWO;
        private Krypton.Toolkit.KryptonLabel lblEnvelopQty;
        private Krypton.Toolkit.KryptonPanel pnlDetails;
        private Krypton.Toolkit.KryptonButton btnPrintSelected;
        private Krypton.Toolkit.KryptonButton btnRemoveSelected;
        private Krypton.Toolkit.KryptonPanel pnlPalletsList;
    }
}