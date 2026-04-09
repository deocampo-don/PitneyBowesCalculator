namespace PitneyBowesCalculator.Packed_And_Ready.View_Button
{
    partial class RemovePallets
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
            pnlHeader = new System.Windows.Forms.Panel();
            btnExit = new Krypton.Toolkit.KryptonButton();
            lblHeader = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            btnCancel1 = new Krypton.Toolkit.KryptonButton();
            btnYes1 = new Krypton.Toolkit.KryptonButton();
            btnNo1 = new Krypton.Toolkit.KryptonButton();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label2 = new System.Windows.Forms.Label();
            pnlHeader.SuspendLayout();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = System.Drawing.Color.Transparent;
            pnlHeader.Controls.Add(btnExit);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(3, 16);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new System.Drawing.Size(455, 30);
            pnlHeader.TabIndex = 0;
            // 
            // btnExit
            // 
            btnExit.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            btnExit.Location = new System.Drawing.Point(419, 2);
            btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            btnExit.Name = "btnExit";
            btnExit.Size = new System.Drawing.Size(28, 25);
            btnExit.StateCommon.Back.Color1 = System.Drawing.Color.Transparent;
            btnExit.StateCommon.Back.Color2 = System.Drawing.Color.Transparent;
            btnExit.StateNormal.Back.Color1 = System.Drawing.Color.Transparent;
            btnExit.StateNormal.Back.Color2 = System.Drawing.Color.Transparent;
            btnExit.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnExit.StateTracking.Back.Color1 = System.Drawing.Color.DarkGray;
            btnExit.StateTracking.Back.Color2 = System.Drawing.Color.DarkGray;
            btnExit.TabIndex = 11;
            btnExit.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            btnExit.Values.Image = Properties.Resources.close_img;
            btnExit.Values.Text = "";
            btnExit.Click += btnExit_Click;
            // 
            // lblHeader
            // 
            lblHeader.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblHeader.AutoSize = true;
            lblHeader.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblHeader.Location = new System.Drawing.Point(19, 58);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new System.Drawing.Size(271, 32);
            lblHeader.TabIndex = 3;
            lblHeader.Text = "Before You Continue...";
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(20, 98);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(411, 21);
            label1.TabIndex = 4;
            label1.Text = "How would you like to proceed with the selected pallet(s)?";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Segoe UI", 12F);
            label5.Location = new System.Drawing.Point(20, 197);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(0, 21);
            label5.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI", 12F);
            label4.Location = new System.Drawing.Point(19, 174);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(372, 21);
            label4.TabIndex = 9;
            label4.Text = "Delete – Removes the selected pallet(s) from the job.";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label3.Location = new System.Drawing.Point(19, 151);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(427, 21);
            label3.TabIndex = 8;
            label3.Text = "Unpack - Removes the packed status of the selected pallet(s)";
            // 
            // panel1
            // 
            panel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panel1.BackColor = System.Drawing.Color.Transparent;
            panel1.Controls.Add(btnCancel1);
            panel1.Controls.Add(btnYes1);
            panel1.Controls.Add(btnNo1);
            panel1.Location = new System.Drawing.Point(3, 232);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(455, 54);
            panel1.TabIndex = 7;
            // 
            // btnCancel1
            // 
            btnCancel1.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            btnCancel1.Cursor = System.Windows.Forms.Cursors.Hand;
            btnCancel1.Location = new System.Drawing.Point(155, 9);
            btnCancel1.Name = "btnCancel1";
            btnCancel1.OverrideDefault.Back.Color1 = System.Drawing.Color.Gray;
            btnCancel1.OverrideDefault.Back.Color2 = System.Drawing.Color.Gray;
            btnCancel1.OverrideDefault.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnCancel1.OverrideDefault.Back.Draw = Krypton.Toolkit.InheritBool.True;
            btnCancel1.OverrideDefault.Border.Color1 = System.Drawing.Color.Black;
            btnCancel1.OverrideDefault.Border.Color2 = System.Drawing.Color.Black;
            btnCancel1.OverrideDefault.Border.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnCancel1.OverrideDefault.Border.Draw = Krypton.Toolkit.InheritBool.True;
            btnCancel1.OverrideDefault.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnCancel1.OverrideDefault.Border.Rounding = 5F;
            btnCancel1.OverrideDefault.Content.AdjacentGap = 5;
            btnCancel1.OverrideDefault.Content.Draw = Krypton.Toolkit.InheritBool.True;
            btnCancel1.OverrideDefault.Content.DrawFocus = Krypton.Toolkit.InheritBool.True;
            btnCancel1.OverrideFocus.Back.Color1 = System.Drawing.Color.Silver;
            btnCancel1.OverrideFocus.Back.Color2 = System.Drawing.Color.Silver;
            btnCancel1.OverrideFocus.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnCancel1.OverrideFocus.Border.Color1 = System.Drawing.Color.Black;
            btnCancel1.OverrideFocus.Border.Color2 = System.Drawing.Color.Black;
            btnCancel1.OverrideFocus.Border.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnCancel1.OverrideFocus.Border.Draw = Krypton.Toolkit.InheritBool.True;
            btnCancel1.OverrideFocus.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnCancel1.OverrideFocus.Border.Rounding = 5F;
            btnCancel1.Size = new System.Drawing.Size(78, 34);
            btnCancel1.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            btnCancel1.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            btnCancel1.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnCancel1.StateNormal.Back.Color1 = System.Drawing.Color.Gray;
            btnCancel1.StateNormal.Back.Color2 = System.Drawing.Color.Gray;
            btnCancel1.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnCancel1.StateNormal.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnCancel1.StateNormal.Border.Rounding = 5F;
            btnCancel1.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.White;
            btnCancel1.StateNormal.Content.ShortText.Color2 = System.Drawing.Color.White;
            btnCancel1.StateNormal.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnCancel1.StateTracking.Back.Color1 = System.Drawing.Color.Silver;
            btnCancel1.StateTracking.Back.Color2 = System.Drawing.Color.Silver;
            btnCancel1.StateTracking.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnCancel1.StateTracking.Border.Rounding = 5F;
            btnCancel1.TabIndex = 12;
            btnCancel1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            btnCancel1.Values.Text = "Cancel";
            btnCancel1.Click += btnCancel1_Click;
            // 
            // btnYes1
            // 
            btnYes1.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            btnYes1.Cursor = System.Windows.Forms.Cursors.Hand;
            btnYes1.Location = new System.Drawing.Point(349, 9);
            btnYes1.Name = "btnYes1";
            btnYes1.OverrideDefault.Back.Color1 = System.Drawing.Color.Black;
            btnYes1.OverrideDefault.Back.Color2 = System.Drawing.Color.Black;
            btnYes1.OverrideDefault.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnYes1.OverrideDefault.Back.Draw = Krypton.Toolkit.InheritBool.True;
            btnYes1.OverrideDefault.Border.Color1 = System.Drawing.Color.Black;
            btnYes1.OverrideDefault.Border.Color2 = System.Drawing.Color.Black;
            btnYes1.OverrideDefault.Border.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnYes1.OverrideDefault.Border.Draw = Krypton.Toolkit.InheritBool.True;
            btnYes1.OverrideDefault.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnYes1.OverrideDefault.Border.Rounding = 5F;
            btnYes1.OverrideDefault.Content.AdjacentGap = 5;
            btnYes1.OverrideDefault.Content.Draw = Krypton.Toolkit.InheritBool.True;
            btnYes1.OverrideDefault.Content.DrawFocus = Krypton.Toolkit.InheritBool.True;
            btnYes1.OverrideFocus.Back.Color1 = System.Drawing.Color.DimGray;
            btnYes1.OverrideFocus.Back.Color2 = System.Drawing.Color.DimGray;
            btnYes1.OverrideFocus.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnYes1.OverrideFocus.Border.Color1 = System.Drawing.Color.Black;
            btnYes1.OverrideFocus.Border.Color2 = System.Drawing.Color.Black;
            btnYes1.OverrideFocus.Border.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnYes1.OverrideFocus.Border.Draw = Krypton.Toolkit.InheritBool.True;
            btnYes1.OverrideFocus.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnYes1.OverrideFocus.Border.Rounding = 5F;
            btnYes1.Size = new System.Drawing.Size(78, 34);
            btnYes1.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            btnYes1.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            btnYes1.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnYes1.StateNormal.Back.Color1 = System.Drawing.Color.Black;
            btnYes1.StateNormal.Back.Color2 = System.Drawing.Color.Black;
            btnYes1.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnYes1.StateNormal.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnYes1.StateNormal.Border.Rounding = 5F;
            btnYes1.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.White;
            btnYes1.StateNormal.Content.ShortText.Color2 = System.Drawing.Color.White;
            btnYes1.StateNormal.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnYes1.StateTracking.Back.Color1 = System.Drawing.Color.DimGray;
            btnYes1.StateTracking.Back.Color2 = System.Drawing.Color.DimGray;
            btnYes1.StateTracking.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnYes1.StateTracking.Border.Rounding = 5F;
            btnYes1.TabIndex = 13;
            btnYes1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            btnYes1.Values.Text = "Yes";
            btnYes1.Click += btnYes1_Click;
            // 
            // btnNo1
            // 
            btnNo1.ButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            btnNo1.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNo1.Location = new System.Drawing.Point(253, 9);
            btnNo1.Name = "btnNo1";
            btnNo1.OverrideDefault.Back.Color1 = System.Drawing.Color.Gray;
            btnNo1.OverrideDefault.Back.Color2 = System.Drawing.Color.Gray;
            btnNo1.OverrideDefault.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnNo1.OverrideDefault.Back.Draw = Krypton.Toolkit.InheritBool.True;
            btnNo1.OverrideDefault.Border.Color1 = System.Drawing.Color.Black;
            btnNo1.OverrideDefault.Border.Color2 = System.Drawing.Color.Black;
            btnNo1.OverrideDefault.Border.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnNo1.OverrideDefault.Border.Draw = Krypton.Toolkit.InheritBool.True;
            btnNo1.OverrideDefault.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnNo1.OverrideDefault.Border.Rounding = 5F;
            btnNo1.OverrideDefault.Content.AdjacentGap = 5;
            btnNo1.OverrideDefault.Content.Draw = Krypton.Toolkit.InheritBool.True;
            btnNo1.OverrideDefault.Content.DrawFocus = Krypton.Toolkit.InheritBool.True;
            btnNo1.OverrideFocus.Back.Color1 = System.Drawing.Color.Silver;
            btnNo1.OverrideFocus.Back.Color2 = System.Drawing.Color.Silver;
            btnNo1.OverrideFocus.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnNo1.OverrideFocus.Border.Color1 = System.Drawing.Color.Black;
            btnNo1.OverrideFocus.Border.Color2 = System.Drawing.Color.Black;
            btnNo1.OverrideFocus.Border.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnNo1.OverrideFocus.Border.Draw = Krypton.Toolkit.InheritBool.True;
            btnNo1.OverrideFocus.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnNo1.OverrideFocus.Border.Rounding = 5F;
            btnNo1.Size = new System.Drawing.Size(77, 34);
            btnNo1.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            btnNo1.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            btnNo1.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnNo1.StateNormal.Back.Color1 = System.Drawing.Color.Gray;
            btnNo1.StateNormal.Back.Color2 = System.Drawing.Color.Gray;
            btnNo1.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnNo1.StateNormal.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnNo1.StateNormal.Border.Rounding = 5F;
            btnNo1.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.White;
            btnNo1.StateNormal.Content.ShortText.Color2 = System.Drawing.Color.White;
            btnNo1.StateNormal.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnNo1.StateTracking.Back.Color1 = System.Drawing.Color.Silver;
            btnNo1.StateTracking.Back.Color2 = System.Drawing.Color.Silver;
            btnNo1.StateTracking.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            btnNo1.StateTracking.Border.Rounding = 5F;
            btnNo1.TabIndex = 12;
            btnNo1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            btnNo1.Values.Text = "No";
            btnNo1.Click += btnNo1_Click;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBox1.Controls.Add(panel1);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(pnlHeader);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(lblHeader);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new System.Drawing.Point(1, -5);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(461, 289);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label2.Location = new System.Drawing.Point(20, 119);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(0, 21);
            label2.TabIndex = 5;
            // 
            // RemovePallets
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(462, 287);
            Controls.Add(groupBox1);
            Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "RemovePallets";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            pnlHeader.ResumeLayout(false);
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private Krypton.Toolkit.KryptonButton btnExit;
        private Krypton.Toolkit.KryptonButton btnYes1;
        private Krypton.Toolkit.KryptonButton btnNo1;
        private Krypton.Toolkit.KryptonButton btnCancel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
    }
}