namespace WindowsFormsApp1
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblWOname = new Krypton.Toolkit.KryptonLabel();
            this.lblWOqty = new Krypton.Toolkit.KryptonLabel();
            this.cbWO = new Krypton.Toolkit.KryptonCheckBox();
            this.pnlDivider = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lblWOname, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblWOqty, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbWO, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(350, 35);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // lblWOname
            // 
            this.lblWOname.Location = new System.Drawing.Point(3, 3);
            this.lblWOname.Name = "lblWOname";
            this.lblWOname.Size = new System.Drawing.Size(166, 24);
            this.lblWOname.TabIndex = 0;
            this.lblWOname.Values.Text = "CXXX26010101PER001";
            // 
            // lblWOqty
            // 
            this.lblWOqty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblWOqty.Location = new System.Drawing.Point(221, 8);
            this.lblWOqty.Name = "lblWOqty";
            this.lblWOqty.Size = new System.Drawing.Size(48, 19);
            this.lblWOqty.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWOqty.TabIndex = 1;
            this.lblWOqty.Values.Text = "12000";
            // 
            // cbWO
            // 
            this.cbWO.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbWO.Location = new System.Drawing.Point(304, 9);
            this.cbWO.Name = "cbWO";
            this.cbWO.Size = new System.Drawing.Size(22, 16);
            this.cbWO.TabIndex = 2;
            this.cbWO.Values.Text = "";
            this.cbWO.CheckedChanged += new System.EventHandler(this.cbWO_CheckedChanged);
            // 
            // pnlDivider
            // 
            this.pnlDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(210)))), ((int)(((byte)(230)))));
            this.pnlDivider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDivider.Location = new System.Drawing.Point(0, 35);
            this.pnlDivider.Name = "pnlDivider";
            this.pnlDivider.Size = new System.Drawing.Size(350, 1);
            this.pnlDivider.TabIndex = 1;
            // 
            // WorkOrderRowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlDivider);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "WorkOrderRowControl";
            this.Size = new System.Drawing.Size(350, 41);
            this.Load += new System.EventHandler(this.WorkOrderRowControl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Krypton.Toolkit.KryptonLabel lblWOname;
        private Krypton.Toolkit.KryptonLabel lblWOqty;
        private Krypton.Toolkit.KryptonCheckBox cbWO;
        private System.Windows.Forms.Panel pnlDivider;
    }
}
