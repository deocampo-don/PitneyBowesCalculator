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
            PanelTableLayout = new System.Windows.Forms.TableLayoutPanel();
            midPanel = new Krypton.Toolkit.KryptonPanel();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            lblEnvelopeQty = new Krypton.Toolkit.KryptonLabel();
            lblScannedWOs = new Krypton.Toolkit.KryptonLabel();
            rightPanel = new Krypton.Toolkit.KryptonPanel();
            tlButtons = new System.Windows.Forms.TableLayoutPanel();
            btnAddPallet = new Krypton.Toolkit.KryptonButton();
            btnPackPallet = new Krypton.Toolkit.KryptonButton();
            btnView = new Krypton.Toolkit.KryptonButton();
            leftPanel = new Krypton.Toolkit.KryptonPanel();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            lblPbJobName = new Krypton.Toolkit.KryptonLabel();
            lblAxRef = new Krypton.Toolkit.KryptonLabel();
            roundedPanel1 = new RoundedPanel();
            PanelTableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)midPanel).BeginInit();
            midPanel.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)rightPanel).BeginInit();
            rightPanel.SuspendLayout();
            tlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)leftPanel).BeginInit();
            leftPanel.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            roundedPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // PanelTableLayout
            // 
            PanelTableLayout.BackColor = System.Drawing.Color.FromArgb(232, 222, 248);
            PanelTableLayout.ColumnCount = 5;
            PanelTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.98421F));
            PanelTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            PanelTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.59427F));
            PanelTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 7F));
            PanelTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.42152F));
            PanelTableLayout.Controls.Add(midPanel, 2, 0);
            PanelTableLayout.Controls.Add(rightPanel, 4, 0);
            PanelTableLayout.Controls.Add(leftPanel, 0, 0);
            PanelTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            PanelTableLayout.Location = new System.Drawing.Point(2, 2);
            PanelTableLayout.Margin = new System.Windows.Forms.Padding(0);
            PanelTableLayout.Name = "PanelTableLayout";
            PanelTableLayout.Padding = new System.Windows.Forms.Padding(6);
            PanelTableLayout.RowCount = 1;
            PanelTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            PanelTableLayout.Size = new System.Drawing.Size(1258, 87);
            PanelTableLayout.TabIndex = 1;
            // 
            // midPanel
            // 
            midPanel.Controls.Add(tableLayoutPanel1);
            midPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            midPanel.Location = new System.Drawing.Point(397, 8);
            midPanel.Margin = new System.Windows.Forms.Padding(2);
            midPanel.Name = "midPanel";
            midPanel.Size = new System.Drawing.Size(415, 71);
            midPanel.StateCommon.Color1 = System.Drawing.Color.Transparent;
            midPanel.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.Controls.Add(lblEnvelopeQty, 0, 0);
            tableLayoutPanel1.Controls.Add(lblScannedWOs, 0, 1);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new System.Drawing.Size(415, 71);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lblEnvelopeQty
            // 
            lblEnvelopeQty.Dock = System.Windows.Forms.DockStyle.Fill;
            lblEnvelopeQty.Location = new System.Drawing.Point(2, 2);
            lblEnvelopeQty.Margin = new System.Windows.Forms.Padding(2);
            lblEnvelopeQty.Name = "lblEnvelopeQty";
            lblEnvelopeQty.Size = new System.Drawing.Size(411, 31);
            lblEnvelopeQty.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblEnvelopeQty.TabIndex = 0;
            lblEnvelopeQty.Values.Text = "Envelope Qty:";
            // 
            // lblScannedWOs
            // 
            lblScannedWOs.Dock = System.Windows.Forms.DockStyle.Fill;
            lblScannedWOs.Location = new System.Drawing.Point(2, 37);
            lblScannedWOs.Margin = new System.Windows.Forms.Padding(2);
            lblScannedWOs.Name = "lblScannedWOs";
            lblScannedWOs.Size = new System.Drawing.Size(411, 32);
            lblScannedWOs.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblScannedWOs.TabIndex = 1;
            lblScannedWOs.Values.Text = "Scanned Work Orders:";
            // 
            // rightPanel
            // 
            rightPanel.Controls.Add(tlButtons);
            rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            rightPanel.Location = new System.Drawing.Point(823, 11);
            rightPanel.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            rightPanel.Name = "rightPanel";
            rightPanel.Size = new System.Drawing.Size(427, 65);
            rightPanel.StateCommon.Color1 = System.Drawing.Color.Transparent;
            rightPanel.TabIndex = 4;
            // 
            // tlButtons
            // 
            tlButtons.ColumnCount = 5;
            tlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.06549F));
            tlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            tlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.1724F));
            tlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            tlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.76211F));
            tlButtons.Controls.Add(btnAddPallet, 0, 0);
            tlButtons.Controls.Add(btnPackPallet, 2, 0);
            tlButtons.Controls.Add(btnView, 4, 0);
            tlButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            tlButtons.Location = new System.Drawing.Point(0, 0);
            tlButtons.Margin = new System.Windows.Forms.Padding(2);
            tlButtons.Name = "tlButtons";
            tlButtons.Padding = new System.Windows.Forms.Padding(0, 5, 9, 5);
            tlButtons.RowCount = 1;
            tlButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlButtons.Size = new System.Drawing.Size(427, 65);
            tlButtons.TabIndex = 0;
            // 
            // btnAddPallet
            // 
            btnAddPallet.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            btnAddPallet.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAddPallet.Dock = System.Windows.Forms.DockStyle.Fill;
            btnAddPallet.Location = new System.Drawing.Point(1, 6);
            btnAddPallet.Margin = new System.Windows.Forms.Padding(1);
            btnAddPallet.Name = "btnAddPallet";
            btnAddPallet.Size = new System.Drawing.Size(149, 53);
            btnAddPallet.StateCommon.Back.Color1 = System.Drawing.Color.LimeGreen;
            btnAddPallet.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(103, 80, 164);
            btnAddPallet.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnAddPallet.StateCommon.Border.Rounding = 5F;
            btnAddPallet.StateCommon.Border.Width = 1;
            btnAddPallet.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            btnAddPallet.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnAddPallet.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnAddPallet.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnAddPallet.TabIndex = 0;
            btnAddPallet.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            btnAddPallet.Values.Text = "New Pallet";
            btnAddPallet.Click += btnAddPallet_Click;
            // 
            // btnPackPallet
            // 
            btnPackPallet.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            btnPackPallet.Cursor = System.Windows.Forms.Cursors.Hand;
            btnPackPallet.Dock = System.Windows.Forms.DockStyle.Fill;
            btnPackPallet.Enabled = false;
            btnPackPallet.Location = new System.Drawing.Point(167, 6);
            btnPackPallet.Margin = new System.Windows.Forms.Padding(1);
            btnPackPallet.Name = "btnPackPallet";
            btnPackPallet.Size = new System.Drawing.Size(142, 53);
            btnPackPallet.StateCommon.Back.Color1 = System.Drawing.Color.Silver;
            btnPackPallet.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(0, 136, 255);
            btnPackPallet.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnPackPallet.StateCommon.Border.Rounding = 5F;
            btnPackPallet.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            btnPackPallet.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            btnPackPallet.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnPackPallet.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnPackPallet.TabIndex = 1;
            btnPackPallet.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            btnPackPallet.Values.Text = "Pack Pallet";
            btnPackPallet.Click += btnPackPallet_Click;
            // 
            // btnView
            // 
            btnView.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            btnView.Cursor = System.Windows.Forms.Cursors.Hand;
            btnView.Dock = System.Windows.Forms.DockStyle.Fill;
            btnView.Location = new System.Drawing.Point(325, 6);
            btnView.Margin = new System.Windows.Forms.Padding(1);
            btnView.Name = "btnView";
            btnView.Size = new System.Drawing.Size(92, 53);
            btnView.StateCommon.Back.Color1 = System.Drawing.Color.Silver;
            btnView.StateCommon.Border.Color1 = System.Drawing.Color.White;
            btnView.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnView.StateCommon.Border.Rounding = 5F;
            btnView.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            btnView.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            btnView.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnView.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnView.TabIndex = 2;
            btnView.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            btnView.Values.Text = "View";
            btnView.Click += kryptonButton3_Click;
            // 
            // leftPanel
            // 
            leftPanel.Controls.Add(tableLayoutPanel2);
            leftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            leftPanel.Location = new System.Drawing.Point(8, 8);
            leftPanel.Margin = new System.Windows.Forms.Padding(2);
            leftPanel.Name = "leftPanel";
            leftPanel.Size = new System.Drawing.Size(359, 71);
            leftPanel.StateCommon.Color1 = System.Drawing.Color.Transparent;
            leftPanel.TabIndex = 6;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(lblPbJobName, 0, 0);
            tableLayoutPanel2.Controls.Add(lblAxRef, 0, 1);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new System.Drawing.Size(359, 71);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // lblPbJobName
            // 
            lblPbJobName.Dock = System.Windows.Forms.DockStyle.Fill;
            lblPbJobName.Location = new System.Drawing.Point(2, 2);
            lblPbJobName.Margin = new System.Windows.Forms.Padding(2);
            lblPbJobName.Name = "lblPbJobName";
            lblPbJobName.Size = new System.Drawing.Size(355, 31);
            lblPbJobName.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            lblPbJobName.TabIndex = 4;
            lblPbJobName.Values.Text = "PB JOB NAME";
            // 
            // lblAxRef
            // 
            lblAxRef.Dock = System.Windows.Forms.DockStyle.Fill;
            lblAxRef.Location = new System.Drawing.Point(2, 37);
            lblAxRef.Margin = new System.Windows.Forms.Padding(2);
            lblAxRef.Name = "lblAxRef";
            lblAxRef.Size = new System.Drawing.Size(355, 32);
            lblAxRef.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            lblAxRef.TabIndex = 3;
            lblAxRef.Values.Text = "23412";
          
            // 
            // roundedPanel1
            // 
            roundedPanel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            roundedPanel1.BorderColor = System.Drawing.Color.Transparent;
            roundedPanel1.BorderRadius = 10;
            roundedPanel1.BorderSize = 3;
            roundedPanel1.Controls.Add(PanelTableLayout);
            roundedPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            roundedPanel1.Location = new System.Drawing.Point(0, 0);
            roundedPanel1.Margin = new System.Windows.Forms.Padding(0);
            roundedPanel1.Name = "roundedPanel1";
            roundedPanel1.Padding = new System.Windows.Forms.Padding(2);
            roundedPanel1.Size = new System.Drawing.Size(1262, 91);
            roundedPanel1.TabIndex = 7;
            // 
            // PalletRowControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Transparent;
            Controls.Add(roundedPanel1);
            Margin = new System.Windows.Forms.Padding(1);
            Name = "PalletRowControl";
            Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);
            Size = new System.Drawing.Size(1274, 91);
            PanelTableLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)midPanel).EndInit();
            midPanel.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)rightPanel).EndInit();
            rightPanel.ResumeLayout(false);
            tlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)leftPanel).EndInit();
            leftPanel.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            roundedPanel1.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
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
        private Krypton.Toolkit.KryptonButton btnView;
        private Krypton.Toolkit.KryptonButton btnPackPallet;
        private RoundedPanel roundedPanel1;
    }
}
