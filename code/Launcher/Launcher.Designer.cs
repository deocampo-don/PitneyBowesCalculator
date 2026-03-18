namespace Launcher
{
    partial class Launcher
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
            btnPbc = new Krypton.Toolkit.KryptonButton();
            btnPl = new Krypton.Toolkit.KryptonButton();
            pictureBox1 = new PictureBox();
            rbPbcDef = new RadioButton();
            rbPlDef = new RadioButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnPbc
            // 
            btnPbc.Location = new Point(378, 359);
            btnPbc.Name = "btnPbc";
            btnPbc.Size = new Size(211, 66);
            btnPbc.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnPbc.StateCommon.Border.Rounding = 5F;
            btnPbc.StateCommon.Content.ShortText.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPbc.TabIndex = 0;
            btnPbc.Values.DropDownArrowColor = Color.Empty;
            btnPbc.Values.Text = "Pitney Bowes App";
            btnPbc.Click += btnPbc_Click;
            // 
            // btnPl
            // 
            btnPl.Location = new Point(608, 359);
            btnPl.Name = "btnPl";
            btnPl.Size = new Size(163, 66);
            btnPl.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnPl.StateCommon.Border.Rounding = 5F;
            btnPl.StateCommon.Content.ShortText.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnPl.TabIndex = 1;
            btnPl.Values.DropDownArrowColor = Color.Empty;
            btnPl.Values.Text = "Post List";
            btnPl.Click += btnPl_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.Location = new Point(751, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(37, 29);
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // rbPbcDef
            // 
            rbPbcDef.AutoSize = true;
            rbPbcDef.BackColor = Color.Transparent;
            rbPbcDef.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rbPbcDef.ForeColor = SystemColors.Control;
            rbPbcDef.Location = new Point(430, 321);
            rbPbcDef.Name = "rbPbcDef";
            rbPbcDef.RightToLeft = RightToLeft.Yes;
            rbPbcDef.Size = new Size(159, 32);
            rbPbcDef.TabIndex = 5;
            rbPbcDef.TabStop = true;
            rbPbcDef.Text = "Set as Default";
            rbPbcDef.UseVisualStyleBackColor = false;
            rbPbcDef.CheckedChanged += rbPbcDef_CheckedChanged;
            // 
            // rbPlDef
            // 
            rbPlDef.AutoSize = true;
            rbPlDef.BackColor = Color.Transparent;
            rbPlDef.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rbPlDef.ForeColor = SystemColors.Control;
            rbPlDef.Location = new Point(612, 321);
            rbPlDef.Name = "rbPlDef";
            rbPlDef.RightToLeft = RightToLeft.Yes;
            rbPlDef.Size = new Size(159, 32);
            rbPlDef.TabIndex = 6;
            rbPlDef.TabStop = true;
            rbPlDef.Text = "Set as Default";
            rbPlDef.UseVisualStyleBackColor = false;
            rbPlDef.CheckedChanged += rbPlDef_CheckedChanged;
            // 
            // Launcher
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(rbPlDef);
            Controls.Add(rbPbcDef);
            Controls.Add(pictureBox1);
            Controls.Add(btnPl);
            Controls.Add(btnPbc);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Launcher";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Krypton.Toolkit.KryptonButton btnPbc;
        private Krypton.Toolkit.KryptonButton btnPl;
        private PictureBox pictureBox1;
        private RadioButton rbPbcDef;
        private RadioButton rbPlDef;
    }
}
