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
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            txtEnvelopeQty = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            tbWoBarcode = new Krypton.Toolkit.KryptonTextBox();
            label2 = new System.Windows.Forms.Label();
            txtScannedWO = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            pbSpinner = new Krypton.Toolkit.KryptonPictureBox();
            lbStatus = new Krypton.Toolkit.KryptonLabel();
            btnOk = new Krypton.Toolkit.KryptonButton();
            btnCancel = new Krypton.Toolkit.KryptonButton();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbSpinner).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.7385616F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.9934635F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.9607849F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.8300648F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.150327F));
            tableLayoutPanel1.Controls.Add(txtEnvelopeQty, 1, 2);
            tableLayoutPanel1.Controls.Add(label3, 2, 2);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(tbWoBarcode, 0, 1);
            tableLayoutPanel1.Controls.Add(label2, 0, 2);
            tableLayoutPanel1.Controls.Add(txtScannedWO, 3, 2);
            tableLayoutPanel1.Location = new System.Drawing.Point(11, 25);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            tableLayoutPanel1.Size = new System.Drawing.Size(612, 152);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // txtEnvelopeQty
            // 
            txtEnvelopeQty.AutoSize = true;
            txtEnvelopeQty.Font = new System.Drawing.Font("Segoe UI", 14F);
            txtEnvelopeQty.Location = new System.Drawing.Point(186, 110);
            txtEnvelopeQty.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtEnvelopeQty.Name = "txtEnvelopeQty";
            txtEnvelopeQty.Size = new System.Drawing.Size(27, 32);
            txtEnvelopeQty.TabIndex = 12;
            txtEnvelopeQty.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 14F);
            label3.Location = new System.Drawing.Point(290, 110);
            label3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(157, 32);
            label3.TabIndex = 11;
            label3.Text = "Scanned WO:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label1, 5);
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8000011F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(3, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(377, 39);
            label1.TabIndex = 0;
            label1.Text = "Scan PER Work Order";
            // 
            // tbWoBarcode
            // 
            tableLayoutPanel1.SetColumnSpan(tbWoBarcode, 5);
            tbWoBarcode.CueHint.Color1 = System.Drawing.Color.Silver;
            tbWoBarcode.CueHint.CueHintText = "PER Work Order Barcode";
            tbWoBarcode.CueHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            tbWoBarcode.Dock = System.Windows.Forms.DockStyle.Fill;
            tbWoBarcode.Location = new System.Drawing.Point(11, 51);
            tbWoBarcode.Margin = new System.Windows.Forms.Padding(11, 12, 7, 8);
            tbWoBarcode.Name = "tbWoBarcode";
            tbWoBarcode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            tbWoBarcode.Size = new System.Drawing.Size(594, 46);
            tbWoBarcode.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            tbWoBarcode.StateCommon.Border.Rounding = 5F;
            tbWoBarcode.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10);
            tbWoBarcode.TabIndex = 1;
            tbWoBarcode.TextChanged += tbWoBarcode_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 14F);
            label2.Location = new System.Drawing.Point(7, 110);
            label2.Margin = new System.Windows.Forms.Padding(7, 5, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(167, 32);
            label2.TabIndex = 10;
            label2.Text = "Envelope QTY:";
            // 
            // txtScannedWO
            // 
            txtScannedWO.AutoSize = true;
            txtScannedWO.Font = new System.Drawing.Font("Segoe UI", 14F);
            txtScannedWO.Location = new System.Drawing.Point(455, 110);
            txtScannedWO.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtScannedWO.Name = "txtScannedWO";
            txtScannedWO.Size = new System.Drawing.Size(27, 32);
            txtScannedWO.TabIndex = 13;
            txtScannedWO.Text = "0";
            // 
            // panel1
            // 
            panel1.Controls.Add(pbSpinner);
            panel1.Controls.Add(lbStatus);
            panel1.Controls.Add(btnOk);
            panel1.Controls.Add(btnCancel);
            panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel1.Location = new System.Drawing.Point(11, 179);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(611, 57);
            panel1.TabIndex = 1;
            // 
            // pbSpinner
            // 
            pbSpinner.Anchor = System.Windows.Forms.AnchorStyles.Right;
            pbSpinner.Image = Properties.Resources.spinner_32px;
            pbSpinner.Location = new System.Drawing.Point(7, 5);
            pbSpinner.Margin = new System.Windows.Forms.Padding(4, 5, 0, 5);
            pbSpinner.Name = "pbSpinner";
            pbSpinner.Size = new System.Drawing.Size(44, 52);
            pbSpinner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pbSpinner.TabIndex = 17;
            pbSpinner.TabStop = false;
            pbSpinner.Visible = false;
            // 
            // lbStatus
            // 
            lbStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            lbStatus.Location = new System.Drawing.Point(55, 20);
            lbStatus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            lbStatus.Name = "lbStatus";
            lbStatus.Size = new System.Drawing.Size(77, 25);
            lbStatus.StateCommon.Padding = new System.Windows.Forms.Padding(0);
            lbStatus.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            lbStatus.TabIndex = 18;
            lbStatus.Values.Text = "Checking";
            lbStatus.Visible = false;
            // 
            // btnOk
            // 
            btnOk.Anchor = System.Windows.Forms.AnchorStyles.Right;
            btnOk.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            btnOk.Location = new System.Drawing.Point(494, 8);
            btnOk.Name = "btnOk";
            btnOk.Size = new System.Drawing.Size(111, 46);
            btnOk.StateCommon.Back.Color1 = System.Drawing.Color.Black;
            btnOk.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnOk.StateCommon.Border.Rounding = 5F;
            btnOk.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            btnOk.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnOk.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnOk.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnOk.TabIndex = 15;
            btnOk.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            btnOk.Values.Text = "OK";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            btnCancel.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            btnCancel.Location = new System.Drawing.Point(382, 8);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(99, 46);
            btnCancel.StateCommon.Back.Color1 = System.Drawing.Color.Transparent;
            btnCancel.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnCancel.StateCommon.Border.Rounding = 5F;
            btnCancel.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Black;
            btnCancel.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnCancel.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnCancel.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnCancel.TabIndex = 16;
            btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            btnCancel.Values.Text = "Cancel";
            // 
            // AddToPalletDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(633, 248);
            Controls.Add(panel1);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "AddToPalletDialog";
            Padding = new System.Windows.Forms.Padding(11, 25, 11, 12);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "AddToPallet";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbSpinner).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private Krypton.Toolkit.KryptonTextBox tbWoBarcode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label txtEnvelopeQty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtScannedWO;
        private System.Windows.Forms.Panel panel1;
        private Krypton.Toolkit.KryptonPictureBox pbSpinner;
        private Krypton.Toolkit.KryptonLabel lbStatus;
        private Krypton.Toolkit.KryptonButton btnOk;
        private Krypton.Toolkit.KryptonButton btnCancel;
    }
}