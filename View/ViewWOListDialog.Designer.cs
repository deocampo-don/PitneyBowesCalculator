namespace WindowsFormsApp1
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
            this.kryptonTableLayoutPanel1 = new Krypton.Toolkit.KryptonTableLayoutPanel();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.woScrollHost = new Krypton.Toolkit.KryptonPanel();
            this.woFlowRows = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancelWo = new Krypton.Toolkit.KryptonButton();
            this.btnClearWo = new Krypton.Toolkit.KryptonButton();
            this.kryptonTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.woScrollHost)).BeginInit();
            this.woScrollHost.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonTableLayoutPanel1
            // 
            this.kryptonTableLayoutPanel1.AutoSize = true;
            this.kryptonTableLayoutPanel1.ColumnCount = 1;
            this.kryptonTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.kryptonTableLayoutPanel1.Controls.Add(this.kryptonLabel1, 0, 0);
            this.kryptonTableLayoutPanel1.Controls.Add(this.kryptonLabel2, 0, 1);
            this.kryptonTableLayoutPanel1.Controls.Add(this.woScrollHost, 0, 2);
            this.kryptonTableLayoutPanel1.Controls.Add(this.tableLayoutPanel1, 0, 3);
            this.kryptonTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonTableLayoutPanel1.Location = new System.Drawing.Point(20, 20);
            this.kryptonTableLayoutPanel1.Name = "kryptonTableLayoutPanel1";
            this.kryptonTableLayoutPanel1.RowCount = 4;
            this.kryptonTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.kryptonTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.kryptonTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 196F));
            this.kryptonTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.kryptonTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.kryptonTableLayoutPanel1.Size = new System.Drawing.Size(356, 317);
            this.kryptonTableLayoutPanel1.StateCommon.Color1 = System.Drawing.Color.Transparent;
            this.kryptonTableLayoutPanel1.StateCommon.Color2 = System.Drawing.Color.Transparent;
            this.kryptonTableLayoutPanel1.TabIndex = 0;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(3, 3);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(88, 30);
            this.kryptonLabel1.StateCommon.Padding = new System.Windows.Forms.Padding(0);
            this.kryptonLabel1.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "CAPONE";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(3, 39);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(53, 24);
            this.kryptonLabel2.TabIndex = 1;
            this.kryptonLabel2.Values.Text = "25034";
            // 
            // woScrollHost
            // 
            this.woScrollHost.AutoScroll = true;
            this.woScrollHost.Controls.Add(this.woFlowRows);
            this.woScrollHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.woScrollHost.Location = new System.Drawing.Point(3, 69);
            this.woScrollHost.Name = "woScrollHost";
            this.woScrollHost.Size = new System.Drawing.Size(350, 190);
            this.woScrollHost.StateCommon.Color1 = System.Drawing.Color.Transparent;
            this.woScrollHost.TabIndex = 2;
            // 
            // woFlowRows
            // 
            this.woFlowRows.AutoSize = true;
            this.woFlowRows.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.woFlowRows.Dock = System.Windows.Forms.DockStyle.Top;
            this.woFlowRows.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.woFlowRows.Location = new System.Drawing.Point(0, 0);
            this.woFlowRows.Name = "woFlowRows";
            this.woFlowRows.Size = new System.Drawing.Size(350, 0);
            this.woFlowRows.TabIndex = 0;
            this.woFlowRows.WrapContents = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnCancelWo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnClearWo, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(105, 265);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(248, 49);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // btnCancelWo
            // 
            this.btnCancelWo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancelWo.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            this.btnCancelWo.Location = new System.Drawing.Point(3, 5);
            this.btnCancelWo.Name = "btnCancelWo";
            this.btnCancelWo.Size = new System.Drawing.Size(87, 38);
            this.btnCancelWo.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.btnCancelWo.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnCancelWo.StateCommon.Border.Rounding = 20F;
            this.btnCancelWo.StateCommon.Border.Width = 1;
            this.btnCancelWo.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(55)))), ((int)(((byte)(139)))));
            this.btnCancelWo.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.btnCancelWo.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.btnCancelWo.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(223)))), ((int)(((byte)(231)))));
            this.btnCancelWo.TabIndex = 1;
            this.btnCancelWo.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancelWo.Values.Text = "Cancel";
            this.btnCancelWo.Click += new System.EventHandler(this.btnCancelWo_Click);
            // 
            // btnClearWo
            // 
            this.btnClearWo.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            this.btnClearWo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClearWo.Location = new System.Drawing.Point(96, 3);
            this.btnClearWo.Name = "btnClearWo";
            this.btnClearWo.Size = new System.Drawing.Size(149, 43);
            this.btnClearWo.StateCommon.Back.Color1 = System.Drawing.Color.Transparent;
            this.btnClearWo.StateCommon.Back.Color2 = System.Drawing.Color.Transparent;
            this.btnClearWo.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnClearWo.StateCommon.Border.Rounding = 20F;
            this.btnClearWo.StateCommon.Border.Width = 1;
            this.btnClearWo.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(55)))), ((int)(((byte)(139)))));
            this.btnClearWo.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(223)))), ((int)(((byte)(231)))));
            this.btnClearWo.TabIndex = 0;
            this.btnClearWo.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnClearWo.Values.Text = "Clear Selected";
            this.btnClearWo.Click += new System.EventHandler(this.btnClearWo_Click);
            // 
            // ViewWOListDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(396, 357);
            this.Controls.Add(this.kryptonTableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ViewWOListDialog";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewWOListDialog";
            this.Load += new System.EventHandler(this.ViewWOListDialog_Load);
            this.kryptonTableLayoutPanel1.ResumeLayout(false);
            this.kryptonTableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.woScrollHost)).EndInit();
            this.woScrollHost.ResumeLayout(false);
            this.woScrollHost.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonTableLayoutPanel kryptonTableLayoutPanel1;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonPanel woScrollHost;
        private System.Windows.Forms.FlowLayoutPanel woFlowRows;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Krypton.Toolkit.KryptonButton btnClearWo;
        private Krypton.Toolkit.KryptonButton btnCancelWo;
    }
}