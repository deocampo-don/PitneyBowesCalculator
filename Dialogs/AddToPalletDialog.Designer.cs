namespace WindowsFormsApp1.Dialogs
{
    partial class AddToPalletDialog
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtScannedWO = new System.Windows.Forms.Label();
            this.txtEnvelopeQty = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbWoBarcode = new Krypton.Toolkit.KryptonTextBox();
            this.btnOk = new Krypton.Toolkit.KryptonButton();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.02299F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.97701F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tableLayoutPanel1.Controls.Add(this.txtScannedWO, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtEnvelopeQty, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbWoBarcode, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnOk, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 3, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(11, 20);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(529, 178);
            this.tableLayoutPanel1.TabIndex = 0;
           // this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // txtScannedWO
            // 
            this.txtScannedWO.AutoSize = true;
            this.txtScannedWO.Location = new System.Drawing.Point(344, 98);
            this.txtScannedWO.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.txtScannedWO.Name = "txtScannedWO";
            this.txtScannedWO.Size = new System.Drawing.Size(14, 16);
            this.txtScannedWO.TabIndex = 13;
            this.txtScannedWO.Text = "0";
            // 
            // txtEnvelopeQty
            // 
            this.txtEnvelopeQty.AutoSize = true;
            this.txtEnvelopeQty.Location = new System.Drawing.Point(136, 98);
            this.txtEnvelopeQty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.txtEnvelopeQty.Name = "txtEnvelopeQty";
            this.txtEnvelopeQty.Size = new System.Drawing.Size(14, 16);
            this.txtEnvelopeQty.TabIndex = 12;
            this.txtEnvelopeQty.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(231, 98);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Scanned WO:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 5);
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(314, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Scan PER Work Order";
            // 
            // tbWoBarcode
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tbWoBarcode, 5);
            this.tbWoBarcode.CueHint.Color1 = System.Drawing.Color.Silver;
            this.tbWoBarcode.CueHint.CueHintText = "PER Work Order Barcode";
            this.tbWoBarcode.CueHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbWoBarcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbWoBarcode.Location = new System.Drawing.Point(11, 42);
            this.tbWoBarcode.Margin = new System.Windows.Forms.Padding(11, 10, 7, 6);
            this.tbWoBarcode.Name = "tbWoBarcode";
            this.tbWoBarcode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbWoBarcode.Size = new System.Drawing.Size(511, 46);
            this.tbWoBarcode.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom)
            | Krypton.Toolkit.PaletteDrawBorders.Left)
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.tbWoBarcode.StateCommon.Border.Rounding = 5F;
            this.tbWoBarcode.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10);
            this.tbWoBarcode.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnOk.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            this.btnOk.Location = new System.Drawing.Point(466, 130);
            this.btnOk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(60, 37);
            this.btnOk.StateCommon.Back.Color1 = System.Drawing.Color.Black;
            this.btnOk.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom)
            | Krypton.Toolkit.PaletteDrawBorders.Left)
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnOk.StateCommon.Border.Rounding = 5F;
            this.btnOk.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnOk.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.btnOk.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.btnOk.TabIndex = 8;
            this.btnOk.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnOk.Values.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 98);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 4, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Envelope QTY:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            this.btnCancel.Location = new System.Drawing.Point(366, 130);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 37);
            this.btnCancel.StateCommon.Back.Color1 = System.Drawing.Color.Transparent;
            this.btnCancel.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom)
            | Krypton.Toolkit.PaletteDrawBorders.Left)
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnCancel.StateCommon.Border.Rounding = 5F;
            this.btnCancel.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnCancel.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.btnCancel.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // AddToPalletDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(551, 208);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AddToPalletDialog";
            this.Padding = new System.Windows.Forms.Padding(11, 20, 11, 10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddToPallet";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private Krypton.Toolkit.KryptonTextBox tbWoBarcode;
        private Krypton.Toolkit.KryptonButton btnOk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label txtEnvelopeQty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtScannedWO;
        private Krypton.Toolkit.KryptonButton btnCancel;
    }
}