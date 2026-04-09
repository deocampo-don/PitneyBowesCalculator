namespace PitneyBowesCalculator
{
    partial class WorkOrderRowControl
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
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            lblWOname = new Krypton.Toolkit.KryptonLabel();
            lblWOqty = new Krypton.Toolkit.KryptonLabel();
            cbWO = new Krypton.Toolkit.KryptonCheckBox();
            pnlDivider = new System.Windows.Forms.Panel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel1.Controls.Add(lblWOname, 0, 0);
            tableLayoutPanel1.Controls.Add(lblWOqty, 1, 0);
            tableLayoutPanel1.Controls.Add(cbWO, 2, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(399, 38);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lblWOname
            // 
            lblWOname.Dock = System.Windows.Forms.DockStyle.Left;
            lblWOname.Location = new System.Drawing.Point(3, 3);
            lblWOname.Name = "lblWOname";
            lblWOname.Size = new System.Drawing.Size(172, 32);
            lblWOname.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblWOname.TabIndex = 0;
            lblWOname.Values.Text = "CXXX26010101PER001";
            // 
            // lblWOqty
            // 
            lblWOqty.Anchor = System.Windows.Forms.AnchorStyles.None;
            lblWOqty.Location = new System.Drawing.Point(251, 6);
            lblWOqty.Name = "lblWOqty";
            lblWOqty.Size = new System.Drawing.Size(55, 26);
            lblWOqty.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            lblWOqty.TabIndex = 1;
            lblWOqty.Values.Text = "12000";
            // 
            // cbWO
            // 
            cbWO.Anchor = System.Windows.Forms.AnchorStyles.None;
            cbWO.Location = new System.Drawing.Point(349, 12);
            cbWO.Name = "cbWO";
            cbWO.Size = new System.Drawing.Size(19, 13);
            cbWO.TabIndex = 2;
            cbWO.Values.Text = "";
            // 
            // pnlDivider
            // 
            pnlDivider.BackColor = System.Drawing.Color.FromArgb(215, 210, 230);
            pnlDivider.Dock = System.Windows.Forms.DockStyle.Top;
            pnlDivider.Location = new System.Drawing.Point(0, 0);
            pnlDivider.Margin = new System.Windows.Forms.Padding(0);
            pnlDivider.Name = "pnlDivider";
            pnlDivider.Size = new System.Drawing.Size(399, 1);
            pnlDivider.TabIndex = 1;
            // 
            // WorkOrderRowControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(pnlDivider);
            Controls.Add(tableLayoutPanel1);
            Name = "WorkOrderRowControl";
            Size = new System.Drawing.Size(399, 38);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Krypton.Toolkit.KryptonLabel lblWOname;
        private Krypton.Toolkit.KryptonLabel lblWOqty;
        private Krypton.Toolkit.KryptonCheckBox cbWO;
        private System.Windows.Forms.Panel pnlDivider;
    }
}
