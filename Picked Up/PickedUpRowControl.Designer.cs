namespace WindowsFormsApp1.Picked_Up
{
    partial class PickedUpRowControl
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPallets = new Krypton.Toolkit.KryptonLabel();
            this.lblQty = new Krypton.Toolkit.KryptonLabel();
            this.kryptonCheckBox1 = new Krypton.Toolkit.KryptonCheckBox();
            this.lblPBNameCode = new Krypton.Toolkit.KryptonLabel();
            this.lblTrays = new Krypton.Toolkit.KryptonLabel();
            this.lblShipTime = new Krypton.Toolkit.KryptonLabel();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.tableLayoutPanel2.ColumnCount = 7;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 236F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 203F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 177F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 191F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 245F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 242F));
            this.tableLayoutPanel2.Controls.Add(this.lblPallets, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblQty, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.kryptonCheckBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblPBNameCode, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblTrays, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblShipTime, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.kryptonButton1, 6, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1393, 32);
            this.tableLayoutPanel2.TabIndex = 1;
            this.tableLayoutPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel2_Paint);
            // 
            // lblPallets
            // 
            this.lblPallets.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPallets.Location = new System.Drawing.Point(718, 3);
            this.lblPallets.Name = "lblPallets";
            this.lblPallets.Size = new System.Drawing.Size(74, 26);
            this.lblPallets.StateCommon.ShortText.Color1 = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPallets.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPallets.TabIndex = 4;
            this.lblPallets.Values.Text = "Pallets";
            // 
            // lblQty
            // 
            this.lblQty.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblQty.Location = new System.Drawing.Point(338, 3);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(137, 26);
            this.lblQty.StateCommon.ShortText.Color1 = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblQty.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty.TabIndex = 2;
            this.lblQty.Values.Text = "Envelope Qty";
            // 
            // kryptonCheckBox1
            // 
            this.kryptonCheckBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.kryptonCheckBox1.Location = new System.Drawing.Point(38, 8);
            this.kryptonCheckBox1.Name = "kryptonCheckBox1";
            this.kryptonCheckBox1.Size = new System.Drawing.Size(22, 16);
            this.kryptonCheckBox1.TabIndex = 0;
            this.kryptonCheckBox1.Values.Text = "";
            // 
            // lblPBNameCode
            // 
            this.lblPBNameCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPBNameCode.Location = new System.Drawing.Point(102, 3);
            this.lblPBNameCode.Name = "lblPBNameCode";
            this.lblPBNameCode.Size = new System.Drawing.Size(76, 26);
            this.lblPBNameCode.StateCommon.ShortText.Color1 = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPBNameCode.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPBNameCode.TabIndex = 1;
            this.lblPBNameCode.Values.Text = "PB Job";
            // 
            // lblTrays
            // 
            this.lblTrays.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTrays.Location = new System.Drawing.Point(541, 3);
            this.lblTrays.Name = "lblTrays";
            this.lblTrays.Size = new System.Drawing.Size(62, 26);
            this.lblTrays.StateCommon.ShortText.Color1 = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTrays.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrays.TabIndex = 3;
            this.lblTrays.Values.Text = "Trays";
            // 
            // lblShipTime
            // 
            this.lblShipTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblShipTime.Location = new System.Drawing.Point(909, 3);
            this.lblShipTime.Name = "lblShipTime";
            this.lblShipTime.Size = new System.Drawing.Size(176, 26);
            this.lblShipTime.StateCommon.ShortText.Color1 = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblShipTime.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShipTime.TabIndex = 5;
            this.lblShipTime.Values.Text = "Ship Date && Time";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.kryptonButton1.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            this.kryptonButton1.Location = new System.Drawing.Point(1219, 3);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(106, 25);
            this.kryptonButton1.StateCommon.Back.Color1 = System.Drawing.Color.Black;
            this.kryptonButton1.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonButton1.StateCommon.Border.Rounding = 3F;
            this.kryptonButton1.StateCommon.Content.ShortText.Color1 = System.Drawing.SystemColors.Control;
            this.kryptonButton1.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonButton1.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonButton1.TabIndex = 6;
            this.kryptonButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton1.Values.Text = "View";
            // 
            // PickedUpRowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "PickedUpRowControl";
            this.Size = new System.Drawing.Size(1393, 32);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Krypton.Toolkit.KryptonLabel lblPallets;
        private Krypton.Toolkit.KryptonLabel lblQty;
        private Krypton.Toolkit.KryptonCheckBox kryptonCheckBox1;
        private Krypton.Toolkit.KryptonLabel lblPBNameCode;
        private Krypton.Toolkit.KryptonLabel lblTrays;
        private Krypton.Toolkit.KryptonLabel lblShipTime;
        private Krypton.Toolkit.KryptonButton kryptonButton1;
    }
}
