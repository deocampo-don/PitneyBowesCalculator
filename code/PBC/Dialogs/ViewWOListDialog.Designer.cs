namespace PitneyBowesCalculator
{
    partial class ViewWOListDialog
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
            kryptonTableLayoutPanel1 = new Krypton.Toolkit.KryptonTableLayoutPanel();
            lbPbName = new Krypton.Toolkit.KryptonLabel();
            lbPbNum = new Krypton.Toolkit.KryptonLabel();
            woScrollHost = new Krypton.Toolkit.KryptonPanel();
            woFlowRows = new System.Windows.Forms.FlowLayoutPanel();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            btnClearWo = new Krypton.Toolkit.KryptonButton();
            btnCancelWo = new Krypton.Toolkit.KryptonButton();
            kryptonTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)woScrollHost).BeginInit();
            woScrollHost.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // kryptonTableLayoutPanel1
            // 
            kryptonTableLayoutPanel1.AutoSize = true;
            kryptonTableLayoutPanel1.ColumnCount = 1;
            kryptonTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            kryptonTableLayoutPanel1.Controls.Add(lbPbName, 0, 0);
            kryptonTableLayoutPanel1.Controls.Add(lbPbNum, 0, 1);
            kryptonTableLayoutPanel1.Controls.Add(woScrollHost, 0, 2);
            kryptonTableLayoutPanel1.Controls.Add(tableLayoutPanel1, 0, 3);
            kryptonTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            kryptonTableLayoutPanel1.Location = new System.Drawing.Point(18, 19);
            kryptonTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            kryptonTableLayoutPanel1.Name = "kryptonTableLayoutPanel1";
            kryptonTableLayoutPanel1.RowCount = 4;
            kryptonTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            kryptonTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            kryptonTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            kryptonTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            kryptonTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            kryptonTableLayoutPanel1.Size = new System.Drawing.Size(407, 296);
            kryptonTableLayoutPanel1.StateCommon.Color1 = System.Drawing.Color.Transparent;
            kryptonTableLayoutPanel1.StateCommon.Color2 = System.Drawing.Color.Transparent;
            kryptonTableLayoutPanel1.TabIndex = 0;
            // 
            // lbPbName
            // 
            lbPbName.Location = new System.Drawing.Point(3, 2);
            lbPbName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            lbPbName.Name = "lbPbName";
            lbPbName.Size = new System.Drawing.Size(108, 35);
            lbPbName.StateCommon.Padding = new System.Windows.Forms.Padding(0);
            lbPbName.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lbPbName.TabIndex = 0;
            lbPbName.Values.Text = "CAPONE";
            // 
            // lbPbNum
            // 
            lbPbNum.Location = new System.Drawing.Point(3, 41);
            lbPbNum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            lbPbNum.Name = "lbPbNum";
            lbPbNum.Size = new System.Drawing.Size(66, 29);
            lbPbNum.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lbPbNum.TabIndex = 1;
            lbPbNum.Values.Text = "25034";
            // 
            // woScrollHost
            // 
            woScrollHost.AutoScroll = true;
            woScrollHost.Controls.Add(woFlowRows);
            woScrollHost.Dock = System.Windows.Forms.DockStyle.Fill;
            woScrollHost.Location = new System.Drawing.Point(3, 74);
            woScrollHost.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            woScrollHost.Name = "woScrollHost";
            woScrollHost.Size = new System.Drawing.Size(401, 180);
            woScrollHost.StateCommon.Color1 = System.Drawing.Color.Transparent;
            woScrollHost.TabIndex = 2;
            // 
            // woFlowRows
            // 
            woFlowRows.AutoSize = true;
            woFlowRows.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            woFlowRows.Dock = System.Windows.Forms.DockStyle.Top;
            woFlowRows.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            woFlowRows.Location = new System.Drawing.Point(0, 0);
            woFlowRows.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            woFlowRows.Name = "woFlowRows";
            woFlowRows.Size = new System.Drawing.Size(401, 0);
            woFlowRows.TabIndex = 0;
            woFlowRows.WrapContents = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(btnClearWo, 1, 0);
            tableLayoutPanel1.Controls.Add(btnCancelWo, 0, 0);
            tableLayoutPanel1.Location = new System.Drawing.Point(151, 258);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new System.Drawing.Size(253, 36);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // btnClearWo
            // 
            btnClearWo.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            btnClearWo.Dock = System.Windows.Forms.DockStyle.Fill;
            btnClearWo.Location = new System.Drawing.Point(103, 2);
            btnClearWo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            btnClearWo.Name = "btnClearWo";
            btnClearWo.Size = new System.Drawing.Size(147, 32);
            btnClearWo.StateCommon.Back.Color1 = System.Drawing.Color.Transparent;
            btnClearWo.StateCommon.Back.Color2 = System.Drawing.Color.Transparent;
            btnClearWo.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnClearWo.StateCommon.Border.Rounding = 20F;
            btnClearWo.StateCommon.Border.Width = 1;
            btnClearWo.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(121, 55, 139);
            btnClearWo.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            btnClearWo.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(225, 223, 231);
            btnClearWo.TabIndex = 0;
            btnClearWo.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            btnClearWo.Values.Text = "Clear Selected";
            btnClearWo.Click += btnClearWo_Click;
            // 
            // btnCancelWo
            // 
            btnCancelWo.Anchor = System.Windows.Forms.AnchorStyles.None;
            btnCancelWo.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            btnCancelWo.Location = new System.Drawing.Point(3, 2);
            btnCancelWo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            btnCancelWo.Name = "btnCancelWo";
            btnCancelWo.Size = new System.Drawing.Size(94, 32);
            btnCancelWo.StateCommon.Back.Color1 = System.Drawing.Color.White;
            btnCancelWo.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnCancelWo.StateCommon.Border.Rounding = 20F;
            btnCancelWo.StateCommon.Border.Width = 1;
            btnCancelWo.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(79, 55, 139);
            btnCancelWo.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnCancelWo.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnCancelWo.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnCancelWo.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(225, 223, 231);
            btnCancelWo.TabIndex = 1;
            btnCancelWo.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            btnCancelWo.Values.Text = "Cancel";
            btnCancelWo.Click += btnCancelWo_Click;
            // 
            // ViewWOListDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(443, 334);
            Controls.Add(kryptonTableLayoutPanel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Name = "ViewWOListDialog";
            Padding = new System.Windows.Forms.Padding(18, 19, 18, 19);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "ViewWOListDialog";
            kryptonTableLayoutPanel1.ResumeLayout(false);
            kryptonTableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)woScrollHost).EndInit();
            woScrollHost.ResumeLayout(false);
            woScrollHost.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonTableLayoutPanel kryptonTableLayoutPanel1;
        private Krypton.Toolkit.KryptonLabel lbPbName;
        private Krypton.Toolkit.KryptonLabel lbPbNum;
        private Krypton.Toolkit.KryptonPanel woScrollHost;
        private System.Windows.Forms.FlowLayoutPanel woFlowRows;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Krypton.Toolkit.KryptonButton btnClearWo;
        private Krypton.Toolkit.KryptonButton btnCancelWo;
    }
}