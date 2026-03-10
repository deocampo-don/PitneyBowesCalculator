namespace WindowsFormsApp1.Packed_And_Ready
{
    public partial class PackedRowControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.chkbxStatus = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtStatus = new Krypton.Toolkit.KryptonLabel();
            this.txtPBJobNum = new Krypton.Toolkit.KryptonLabel();
            this.txtPBJobName = new Krypton.Toolkit.KryptonLabel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.txtTrays = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel8 = new Krypton.Toolkit.KryptonLabel();
            this.txtPallets = new Krypton.Toolkit.KryptonLabel();
            this.txtEnvelopeQty = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel5 = new Krypton.Toolkit.KryptonLabel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.kryptonLabel10 = new Krypton.Toolkit.KryptonLabel();
            this.txtPackDate = new Krypton.Toolkit.KryptonLabel();
            this.btnView = new Krypton.Toolkit.KryptonButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.kryptonPanel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.chkbxStatus, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnView, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(13, 12, 13, 6);
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.07692F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.91855F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.26923F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(423, 325);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // kryptonPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.kryptonPanel1, 2);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(17, 106);
            this.kryptonPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(389, 1);
            this.kryptonPanel1.StateCommon.Color1 = System.Drawing.Color.Thistle;
            this.kryptonPanel1.TabIndex = 3;
            // 
            // chkbxStatus
            // 
            this.chkbxStatus.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkbxStatus.AutoSize = true;
            this.chkbxStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkbxStatus.Location = new System.Drawing.Point(388, 16);
            this.chkbxStatus.Margin = new System.Windows.Forms.Padding(4);
            this.chkbxStatus.Name = "chkbxStatus";
            this.chkbxStatus.Size = new System.Drawing.Size(18, 17);
            this.chkbxStatus.TabIndex = 0;
            this.chkbxStatus.UseVisualStyleBackColor = true;
            this.chkbxStatus.CheckedChanged += new System.EventHandler(this.chkbxStatus_CheckedChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.txtStatus, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtPBJobNum, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtPBJobName, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(17, 41);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58.69565F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 41.30435F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(389, 57);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtStatus.Location = new System.Drawing.Point(308, 37);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(4);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(77, 16);
            this.txtStatus.StateCommon.Padding = new System.Windows.Forms.Padding(0);
            this.txtStatus.StateCommon.ShortText.Color1 = System.Drawing.Color.Red;
            this.txtStatus.TabIndex = 0;
            this.txtStatus.Values.Text = "Not Ready";
            // 
            // txtPBJobNum
            // 
            this.txtPBJobNum.Location = new System.Drawing.Point(7, 37);
            this.txtPBJobNum.Margin = new System.Windows.Forms.Padding(7, 4, 4, 4);
            this.txtPBJobNum.Name = "txtPBJobNum";
            this.txtPBJobNum.Size = new System.Drawing.Size(47, 16);
            this.txtPBJobNum.StateCommon.Padding = new System.Windows.Forms.Padding(0);
            this.txtPBJobNum.TabIndex = 1;
            this.txtPBJobNum.Values.Text = "25367";
            // 
            // txtPBJobName
            // 
            this.txtPBJobName.Location = new System.Drawing.Point(4, 4);
            this.txtPBJobName.Margin = new System.Windows.Forms.Padding(4);
            this.txtPBJobName.Name = "txtPBJobName";
            this.txtPBJobName.Size = new System.Drawing.Size(113, 25);
            this.txtPBJobName.StateCommon.Padding = new System.Windows.Forms.Padding(0);
            this.txtPBJobName.StateCommon.ShortText.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPBJobName.TabIndex = 2;
            this.txtPBJobName.Values.Text = "CAPONE";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.28671F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.71329F));
            this.tableLayoutPanel3.Controls.Add(this.txtTrays, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.kryptonLabel8, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.txtPallets, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtEnvelopeQty, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.kryptonLabel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.kryptonLabel5, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel1, 1, 3);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(17, 107);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(389, 155);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // txtTrays
            // 
            this.txtTrays.Location = new System.Drawing.Point(4, 100);
            this.txtTrays.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.txtTrays.Name = "txtTrays";
            this.txtTrays.Size = new System.Drawing.Size(60, 50);
            this.txtTrays.StateCommon.Padding = new System.Windows.Forms.Padding(0);
            this.txtTrays.StateCommon.ShortText.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrays.TabIndex = 5;
            this.txtTrays.Values.Text = "54";
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(13, 79);
            this.kryptonLabel8.Margin = new System.Windows.Forms.Padding(13, 4, 4, 4);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(47, 17);
            this.kryptonLabel8.StateCommon.Padding = new System.Windows.Forms.Padding(0);
            this.kryptonLabel8.StateCommon.ShortText.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel8.TabIndex = 4;
            this.kryptonLabel8.Values.Text = "Trays";
            // 
            // txtPallets
            // 
            this.txtPallets.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtPallets.Location = new System.Drawing.Point(287, 29);
            this.txtPallets.Margin = new System.Windows.Forms.Padding(4);
            this.txtPallets.Name = "txtPallets";
            this.txtPallets.Size = new System.Drawing.Size(60, 42);
            this.txtPallets.StateCommon.Padding = new System.Windows.Forms.Padding(0);
            this.txtPallets.StateCommon.ShortText.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPallets.TabIndex = 3;
            this.txtPallets.Values.Text = "10";
            // 
            // txtEnvelopeQty
            // 
            this.txtEnvelopeQty.Location = new System.Drawing.Point(4, 29);
            this.txtEnvelopeQty.Margin = new System.Windows.Forms.Padding(4);
            this.txtEnvelopeQty.Name = "txtEnvelopeQty";
            this.txtEnvelopeQty.Size = new System.Drawing.Size(128, 42);
            this.txtEnvelopeQty.StateCommon.Padding = new System.Windows.Forms.Padding(0);
            this.txtEnvelopeQty.StateCommon.ShortText.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEnvelopeQty.TabIndex = 2;
            this.txtEnvelopeQty.Values.Text = "16315";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(13, 4);
            this.kryptonLabel4.Margin = new System.Windows.Forms.Padding(13, 4, 4, 4);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(110, 17);
            this.kryptonLabel4.StateCommon.Padding = new System.Windows.Forms.Padding(0);
            this.kryptonLabel4.StateCommon.ShortText.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel4.TabIndex = 0;
            this.kryptonLabel4.Values.Text = "Envelope QTY";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.kryptonLabel5.Location = new System.Drawing.Point(289, 4);
            this.kryptonLabel5.Margin = new System.Windows.Forms.Padding(9, 4, 4, 4);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(61, 17);
            this.kryptonLabel5.StateCommon.ShortText.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel5.TabIndex = 1;
            this.kryptonLabel5.Values.Text = "Pallets";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.kryptonLabel10);
            this.flowLayoutPanel1.Controls.Add(this.txtPackDate);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(250, 104);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(135, 47);
            this.flowLayoutPanel1.TabIndex = 6;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // kryptonLabel10
            // 
            this.kryptonLabel10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.kryptonLabel10.Location = new System.Drawing.Point(0, 0);
            this.kryptonLabel10.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.kryptonLabel10.Name = "kryptonLabel10";
            this.kryptonLabel10.Size = new System.Drawing.Size(97, 19);
            this.kryptonLabel10.StateCommon.Padding = new System.Windows.Forms.Padding(0);
            this.kryptonLabel10.StateCommon.ShortText.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel10.TabIndex = 5;
            this.kryptonLabel10.Values.Text = "Packed Date";
            // 
            // txtPackDate
            // 
            this.txtPackDate.Location = new System.Drawing.Point(0, 25);
            this.txtPackDate.Margin = new System.Windows.Forms.Padding(0);
            this.txtPackDate.Name = "txtPackDate";
            this.txtPackDate.Size = new System.Drawing.Size(83, 19);
            this.txtPackDate.StateCommon.Padding = new System.Windows.Forms.Padding(0);
            this.txtPackDate.StateCommon.ShortText.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackDate.TabIndex = 6;
            this.txtPackDate.Values.Text = "01/01/2026";
            // 
            // btnView
            // 
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            this.btnView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnView.Location = new System.Drawing.Point(260, 278);
            this.btnView.Margin = new System.Windows.Forms.Padding(4, 12, 13, 0);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(137, 34);
            this.btnView.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(80)))), ((int)(((byte)(164)))));
            this.btnView.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnView.StateCommon.Border.Rounding = 5F;
            this.btnView.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnView.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.btnView.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.btnView.TabIndex = 5;
            this.btnView.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnView.Values.Text = "View";
            this.btnView.Click += new System.EventHandler(this.btnView_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(0, -11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(429, 346);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // PackedRowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "PackedRowControl";
            this.Size = new System.Drawing.Size(429, 335);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private System.Windows.Forms.CheckBox chkbxStatus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Krypton.Toolkit.KryptonLabel txtStatus;
        private Krypton.Toolkit.KryptonLabel txtPBJobNum;
        private Krypton.Toolkit.KryptonLabel txtPBJobName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Krypton.Toolkit.KryptonLabel txtTrays;
        private Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private Krypton.Toolkit.KryptonLabel txtPallets;
        private Krypton.Toolkit.KryptonLabel txtEnvelopeQty;
        private Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Krypton.Toolkit.KryptonLabel kryptonLabel10;
        private Krypton.Toolkit.KryptonLabel txtPackDate;
        private Krypton.Toolkit.KryptonButton btnView;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
