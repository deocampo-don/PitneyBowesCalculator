namespace WindowsFormsApp1
{
    partial class CreatePBJobDialog
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
            this.cbPBTemp = new Krypton.Toolkit.KryptonCheckBox();
            this.tbJobNumber = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.tbPBJobName = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.btnCreatePBJob = new Krypton.Toolkit.KryptonButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.cbPBTemp, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbJobNumber, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.kryptonLabel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbPBJobName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.kryptonLabel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCreatePBJob, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 20);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(320, 280);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // cbPBTemp
            // 
            this.cbPBTemp.Location = new System.Drawing.Point(3, 179);
            this.cbPBTemp.Margin = new System.Windows.Forms.Padding(3, 20, 3, 10);
            this.cbPBTemp.Name = "cbPBTemp";
            this.cbPBTemp.Size = new System.Drawing.Size(153, 24);
            this.cbPBTemp.TabIndex = 1;
            this.cbPBTemp.Values.Text = "Save as temporary";
            // 
            // tbJobNumber
            // 
            this.tbJobNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbJobNumber.Location = new System.Drawing.Point(3, 116);
            this.tbJobNumber.Name = "tbJobNumber";
            this.tbJobNumber.Size = new System.Drawing.Size(314, 41);
            this.tbJobNumber.StateCommon.Border.Color1 = System.Drawing.Color.DarkGray;
            this.tbJobNumber.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.tbJobNumber.StateCommon.Border.Rounding = 5F;
            this.tbJobNumber.StateCommon.Border.Width = 2;
            this.tbJobNumber.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbJobNumber.StateCommon.Content.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.tbJobNumber.StateCommon.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.tbJobNumber.TabIndex = 3;
            this.tbJobNumber.Text = "25000";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.kryptonLabel2.Location = new System.Drawing.Point(3, 86);
            this.kryptonLabel2.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(96, 24);
            this.kryptonLabel2.TabIndex = 2;
            this.kryptonLabel2.Values.Text = "Job number:";
            // 
            // tbPBJobName
            // 
            this.tbPBJobName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPBJobName.Location = new System.Drawing.Point(3, 33);
            this.tbPBJobName.Name = "tbPBJobName";
            this.tbPBJobName.Size = new System.Drawing.Size(314, 41);
            this.tbPBJobName.StateCommon.Border.Color1 = System.Drawing.Color.DarkGray;
            this.tbPBJobName.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.tbPBJobName.StateCommon.Border.Rounding = 5F;
            this.tbPBJobName.StateCommon.Border.Width = 2;
            this.tbPBJobName.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPBJobName.StateCommon.Content.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.tbPBJobName.StateCommon.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.tbPBJobName.TabIndex = 1;
            this.tbPBJobName.Text = "NEW PB JOB";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.kryptonLabel1.Location = new System.Drawing.Point(3, 3);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(77, 24);
            this.kryptonLabel1.TabIndex = 1;
            this.kryptonLabel1.Values.Text = "Job name";
            // 
            // btnCreatePBJob
            // 
            this.btnCreatePBJob.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            this.btnCreatePBJob.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCreatePBJob.Location = new System.Drawing.Point(3, 216);
            this.btnCreatePBJob.Name = "btnCreatePBJob";
            this.btnCreatePBJob.Size = new System.Drawing.Size(314, 61);
            this.btnCreatePBJob.StateCommon.Back.Color1 = System.Drawing.Color.Black;
            this.btnCreatePBJob.StateCommon.Border.Color1 = System.Drawing.Color.Black;
            this.btnCreatePBJob.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnCreatePBJob.StateCommon.Border.Rounding = 5F;
            this.btnCreatePBJob.StateCommon.Border.Width = 1;
            this.btnCreatePBJob.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnCreatePBJob.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.btnCreatePBJob.StateCommon.Content.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.btnCreatePBJob.TabIndex = 4;
            this.btnCreatePBJob.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCreatePBJob.Values.Text = "Create New PB Job";
            this.btnCreatePBJob.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // CreatePBJobDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(360, 320);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CreatePBJobDialog";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Krypton.Toolkit.KryptonCheckBox cbPBTemp;
        private Krypton.Toolkit.KryptonTextBox tbJobNumber;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonTextBox tbPBJobName;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonButton btnCreatePBJob;
    }
}