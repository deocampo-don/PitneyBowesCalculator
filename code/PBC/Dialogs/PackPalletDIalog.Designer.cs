namespace WindowsFormsApp1.DIalogs
{
    partial class PackPalletDIalog
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
            btnCancel = new Krypton.Toolkit.KryptonButton();
            label1 = new System.Windows.Forms.Label();
            tbTrays = new Krypton.Toolkit.KryptonTextBox();
            btnOk = new Krypton.Toolkit.KryptonButton();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.02299F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.97701F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.9178085F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.0821915F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(tbTrays, 0, 1);
            tableLayoutPanel1.Controls.Add(btnCancel, 3, 2);
            tableLayoutPanel1.Controls.Add(btnOk, 4, 2);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(11, 25);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            tableLayoutPanel1.Size = new System.Drawing.Size(529, 179);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            btnCancel.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            btnCancel.Location = new System.Drawing.Point(323, 126);
            btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(112, 45);
            btnCancel.StateCommon.Back.Color1 = System.Drawing.Color.Transparent;
            btnCancel.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnCancel.StateCommon.Border.Rounding = 5F;
            btnCancel.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Black;
            btnCancel.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnCancel.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnCancel.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnCancel.TabIndex = 9;
            btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            btnCancel.Values.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label1, 5);
            label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(3, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(335, 41);
            label1.TabIndex = 0;
            label1.Text = "Enter quantity of trays";
            // 
            // tbTrays
            // 
            tableLayoutPanel1.SetColumnSpan(tbTrays, 5);
            tbTrays.CueHint.Color1 = System.Drawing.Color.Silver;
            tbTrays.CueHint.CueHintText = "0";
            tbTrays.CueHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            tbTrays.Dock = System.Windows.Forms.DockStyle.Fill;
            tbTrays.Location = new System.Drawing.Point(11, 53);
            tbTrays.Margin = new System.Windows.Forms.Padding(11, 12, 7, 8);
            tbTrays.Name = "tbTrays";
            tbTrays.RightToLeft = System.Windows.Forms.RightToLeft.No;
            tbTrays.Size = new System.Drawing.Size(511, 57);
            tbTrays.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            tbTrays.StateCommon.Border.Rounding = 5F;
            tbTrays.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            tbTrays.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10);
            tbTrays.TabIndex = 1;
            // 
            // btnOk
            // 
            btnOk.Anchor = System.Windows.Forms.AnchorStyles.Right;
            btnOk.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            btnOk.Location = new System.Drawing.Point(441, 126);
            btnOk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            btnOk.Name = "btnOk";
            btnOk.Size = new System.Drawing.Size(85, 45);
            btnOk.StateCommon.Back.Color1 = System.Drawing.Color.Black;
            btnOk.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnOk.StateCommon.Border.Rounding = 5F;
            btnOk.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            btnOk.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnOk.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnOk.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnOk.TabIndex = 8;
            btnOk.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            btnOk.Values.Text = "OK";
            btnOk.Click += btnOk_Click_1;
            // 
            // PackPalletDIalog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(551, 216);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Name = "PackPalletDIalog";
            Padding = new System.Windows.Forms.Padding(11, 25, 11, 12);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "PackPalletDIalog";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private System.Windows.Forms.Label label1;
        private Krypton.Toolkit.KryptonTextBox tbTrays;
        private Krypton.Toolkit.KryptonButton btnOk;
    }
}