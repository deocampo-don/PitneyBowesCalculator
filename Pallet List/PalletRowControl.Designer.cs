namespace WindowsFormsApp1
{
    partial class PalletRowControl
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
            this.PalletMainPanel = new Krypton.Toolkit.KryptonPanel();
            this.RoundedGroupBox = new Krypton.Toolkit.KryptonGroupBox();
            this.PanelTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.leftPanel = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPbJobName = new Krypton.Toolkit.KryptonLabel();
            this.lblAxRef = new Krypton.Toolkit.KryptonLabel();
            this.midPanel = new Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblEnvelopeQty = new Krypton.Toolkit.KryptonLabel();
            this.lblScannedWOs = new Krypton.Toolkit.KryptonLabel();
            this.rightPanel = new Krypton.Toolkit.KryptonPanel();
            this.tlButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddPallet = new Krypton.Toolkit.KryptonButton();
            this.btnPackPallet = new Krypton.Toolkit.KryptonButton();
            this.kryptonButton3 = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.PalletMainPanel)).BeginInit();
            this.PalletMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RoundedGroupBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RoundedGroupBox.Panel)).BeginInit();
            this.RoundedGroupBox.Panel.SuspendLayout();
            this.RoundedGroupBox.SuspendLayout();
            this.PanelTableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftPanel)).BeginInit();
            this.leftPanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.midPanel)).BeginInit();
            this.midPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightPanel)).BeginInit();
            this.rightPanel.SuspendLayout();
            this.tlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // PalletMainPanel
            // 
            this.PalletMainPanel.Controls.Add(this.RoundedGroupBox);
            this.PalletMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PalletMainPanel.Location = new System.Drawing.Point(0, 0);
            this.PalletMainPanel.Margin = new System.Windows.Forms.Padding(0);
            this.PalletMainPanel.Name = "PalletMainPanel";
            this.PalletMainPanel.Size = new System.Drawing.Size(1335, 79);
            this.PalletMainPanel.StateCommon.Color1 = System.Drawing.Color.Transparent;
            this.PalletMainPanel.StateCommon.Color2 = System.Drawing.Color.Transparent;
            this.PalletMainPanel.TabIndex = 0;
            // 
            // RoundedGroupBox
            // 
            this.RoundedGroupBox.CaptionOverlap = 0D;
            this.RoundedGroupBox.CaptionVisible = false;
            this.RoundedGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RoundedGroupBox.Location = new System.Drawing.Point(0, 0);
            // 
            // RoundedGroupBox.Panel
            // 
            this.RoundedGroupBox.Panel.Controls.Add(this.PanelTableLayout);
            this.RoundedGroupBox.Size = new System.Drawing.Size(1335, 79);
            this.RoundedGroupBox.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(222)))), ((int)(((byte)(248)))));
            this.RoundedGroupBox.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(222)))), ((int)(((byte)(248)))));
            this.RoundedGroupBox.StateCommon.Border.Color1 = System.Drawing.Color.LightGray;
            this.RoundedGroupBox.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(222)))), ((int)(((byte)(248)))));
            this.RoundedGroupBox.StateCommon.Border.Draw = Krypton.Toolkit.InheritBool.True;
            this.RoundedGroupBox.StateCommon.Border.Rounding = 7F;
            this.RoundedGroupBox.StateCommon.Border.Width = 1;
            this.RoundedGroupBox.TabIndex = 1;
            this.RoundedGroupBox.Values.Heading = "\r\n";
            this.RoundedGroupBox.Paint += new System.Windows.Forms.PaintEventHandler(this.RoundedGroupBox_Paint);
            // 
            // PanelTableLayout
            // 
            this.PanelTableLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(222)))), ((int)(((byte)(248)))));
            this.PanelTableLayout.ColumnCount = 5;
            this.PanelTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.98421F));
            this.PanelTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.PanelTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.59427F));
            this.PanelTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.PanelTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.42152F));
            this.PanelTableLayout.Controls.Add(this.leftPanel, 0, 0);
            this.PanelTableLayout.Controls.Add(this.midPanel, 2, 0);
            this.PanelTableLayout.Controls.Add(this.rightPanel, 4, 0);
            this.PanelTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelTableLayout.Location = new System.Drawing.Point(0, 0);
            this.PanelTableLayout.Name = "PanelTableLayout";
            this.PanelTableLayout.RowCount = 1;
            this.PanelTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelTableLayout.Size = new System.Drawing.Size(1329, 73);
            this.PanelTableLayout.TabIndex = 1;
            this.PanelTableLayout.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelTableLayout_Paint_1);
            // 
            // leftPanel
            // 
            this.leftPanel.Controls.Add(this.tableLayoutPanel2);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftPanel.Location = new System.Drawing.Point(3, 3);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(381, 67);
            this.leftPanel.StateCommon.Color1 = System.Drawing.Color.Transparent;
            this.leftPanel.TabIndex = 6;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lblPbJobName, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblAxRef, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(381, 67);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // lblPbJobName
            // 
            this.lblPbJobName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPbJobName.Location = new System.Drawing.Point(3, 3);
            this.lblPbJobName.Name = "lblPbJobName";
            this.lblPbJobName.Size = new System.Drawing.Size(375, 27);
            this.lblPbJobName.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPbJobName.TabIndex = 4;
            this.lblPbJobName.Values.Text = "PB JOB NAME";
            // 
            // lblAxRef
            // 
            this.lblAxRef.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAxRef.Location = new System.Drawing.Point(3, 36);
            this.lblAxRef.Name = "lblAxRef";
            this.lblAxRef.Size = new System.Drawing.Size(375, 28);
            this.lblAxRef.TabIndex = 3;
            this.lblAxRef.Values.Text = "23412";
            // 
            // midPanel
            // 
            this.midPanel.Controls.Add(this.tableLayoutPanel1);
            this.midPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.midPanel.Location = new System.Drawing.Point(420, 3);
            this.midPanel.Name = "midPanel";
            this.midPanel.Size = new System.Drawing.Size(440, 67);
            this.midPanel.StateCommon.Color1 = System.Drawing.Color.Transparent;
            this.midPanel.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblEnvelopeQty, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblScannedWOs, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(440, 67);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblEnvelopeQty
            // 
            this.lblEnvelopeQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEnvelopeQty.Location = new System.Drawing.Point(3, 3);
            this.lblEnvelopeQty.Name = "lblEnvelopeQty";
            this.lblEnvelopeQty.Size = new System.Drawing.Size(434, 27);
            this.lblEnvelopeQty.TabIndex = 0;
            this.lblEnvelopeQty.Values.Text = "Envelope Qty:";
            // 
            // lblScannedWOs
            // 
            this.lblScannedWOs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblScannedWOs.Location = new System.Drawing.Point(3, 36);
            this.lblScannedWOs.Name = "lblScannedWOs";
            this.lblScannedWOs.Size = new System.Drawing.Size(434, 28);
            this.lblScannedWOs.TabIndex = 1;
            this.lblScannedWOs.Values.Text = "Scanned Work Orders:";
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.tlButtons);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(874, 5);
            this.rightPanel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(452, 63);
            this.rightPanel.StateCommon.Color1 = System.Drawing.Color.Transparent;
            this.rightPanel.TabIndex = 4;
            // 
            // tlButtons
            // 
            this.tlButtons.ColumnCount = 5;
            this.tlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.06549F));
            this.tlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.1724F));
            this.tlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.76211F));
            this.tlButtons.Controls.Add(this.btnAddPallet, 0, 0);
            this.tlButtons.Controls.Add(this.btnPackPallet, 2, 0);
            this.tlButtons.Controls.Add(this.kryptonButton3, 4, 0);
            this.tlButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlButtons.Location = new System.Drawing.Point(0, 0);
            this.tlButtons.Name = "tlButtons";
            this.tlButtons.Padding = new System.Windows.Forms.Padding(0, 5, 10, 5);
            this.tlButtons.RowCount = 1;
            this.tlButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlButtons.Size = new System.Drawing.Size(452, 63);
            this.tlButtons.TabIndex = 0;
            // 
            // btnAddPallet
            // 
            this.btnAddPallet.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            this.btnAddPallet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddPallet.Location = new System.Drawing.Point(0, 5);
            this.btnAddPallet.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddPallet.Name = "btnAddPallet";
            this.btnAddPallet.Size = new System.Drawing.Size(159, 53);
            this.btnAddPallet.StateCommon.Back.Color1 = System.Drawing.Color.LimeGreen;
            this.btnAddPallet.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(80)))), ((int)(((byte)(164)))));
            this.btnAddPallet.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnAddPallet.StateCommon.Border.Rounding = 5F;
            this.btnAddPallet.StateCommon.Border.Width = 1;
            this.btnAddPallet.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnAddPallet.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPallet.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.btnAddPallet.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.btnAddPallet.TabIndex = 0;
            this.btnAddPallet.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnAddPallet.Values.Text = "New Pallet";
            this.btnAddPallet.Click += new System.EventHandler(this.btnAddPallet_Click);
            // 
            // btnPackPallet
            // 
            this.btnPackPallet.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            this.btnPackPallet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPackPallet.Enabled = false;
            this.btnPackPallet.Location = new System.Drawing.Point(176, 5);
            this.btnPackPallet.Margin = new System.Windows.Forms.Padding(0);
            this.btnPackPallet.Name = "btnPackPallet";
            this.btnPackPallet.Size = new System.Drawing.Size(152, 53);
            this.btnPackPallet.StateCommon.Back.Color1 = System.Drawing.Color.Silver;
            this.btnPackPallet.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(136)))), ((int)(((byte)(255)))));
            this.btnPackPallet.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnPackPallet.StateCommon.Border.Rounding = 5F;
            this.btnPackPallet.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnPackPallet.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPackPallet.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.btnPackPallet.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.btnPackPallet.TabIndex = 1;
            this.btnPackPallet.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnPackPallet.Values.Text = "Pack Pallet";
            this.btnPackPallet.Click += new System.EventHandler(this.btnPackPallet_Click);
            // 
            // kryptonButton3
            // 
            this.kryptonButton3.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            this.kryptonButton3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonButton3.Location = new System.Drawing.Point(344, 5);
            this.kryptonButton3.Margin = new System.Windows.Forms.Padding(0);
            this.kryptonButton3.Name = "kryptonButton3";
            this.kryptonButton3.Size = new System.Drawing.Size(98, 53);
            this.kryptonButton3.StateCommon.Back.Color1 = System.Drawing.Color.Silver;
            this.kryptonButton3.StateCommon.Border.Color1 = System.Drawing.Color.White;
            this.kryptonButton3.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonButton3.StateCommon.Border.Rounding = 5F;
            this.kryptonButton3.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.kryptonButton3.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonButton3.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonButton3.TabIndex = 2;
            this.kryptonButton3.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton3.Values.Text = "View";
            this.kryptonButton3.Click += new System.EventHandler(this.kryptonButton3_Click);
            // 
            // PalletRowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.PalletMainPanel);
            this.Name = "PalletRowControl";
            this.Size = new System.Drawing.Size(1335, 79);
            ((System.ComponentModel.ISupportInitialize)(this.PalletMainPanel)).EndInit();
            this.PalletMainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RoundedGroupBox.Panel)).EndInit();
            this.RoundedGroupBox.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RoundedGroupBox)).EndInit();
            this.RoundedGroupBox.ResumeLayout(false);
            this.PanelTableLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftPanel)).EndInit();
            this.leftPanel.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.midPanel)).EndInit();
            this.midPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightPanel)).EndInit();
            this.rightPanel.ResumeLayout(false);
            this.tlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel PalletMainPanel;
        private Krypton.Toolkit.KryptonGroupBox RoundedGroupBox;
        private System.Windows.Forms.TableLayoutPanel PanelTableLayout;
        private Krypton.Toolkit.KryptonPanel leftPanel;
        private Krypton.Toolkit.KryptonPanel midPanel;
        private Krypton.Toolkit.KryptonPanel rightPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Krypton.Toolkit.KryptonLabel lblEnvelopeQty;
        private Krypton.Toolkit.KryptonLabel lblScannedWOs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Krypton.Toolkit.KryptonLabel lblPbJobName;
        private Krypton.Toolkit.KryptonLabel lblAxRef;
        private System.Windows.Forms.TableLayoutPanel tlButtons;
        private Krypton.Toolkit.KryptonButton btnAddPallet;
        private Krypton.Toolkit.KryptonButton kryptonButton3;
        private Krypton.Toolkit.KryptonButton btnPackPallet;
    }
}
