using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    partial class PalletListView
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
            this.scrollHost = new Krypton.Toolkit.KryptonPanel();
            this.flowRows = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.scrollHost)).BeginInit();
            this.scrollHost.SuspendLayout();
            this.SuspendLayout();
            // 
            // scrollHost
            // 
            this.scrollHost.AutoScroll = true;
            this.scrollHost.Controls.Add(this.flowRows);
            this.scrollHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollHost.Location = new System.Drawing.Point(0, 0);
            this.scrollHost.Margin = new System.Windows.Forms.Padding(5);
            this.scrollHost.Name = "scrollHost";
            this.scrollHost.Size = new System.Drawing.Size(497, 150);
            this.scrollHost.StateCommon.Color1 = System.Drawing.Color.Transparent;
            this.scrollHost.TabIndex = 0;
            this.scrollHost.Paint += new System.Windows.Forms.PaintEventHandler(this.scrollHost_Paint);
            // 
            // flowRows
            // 
            this.flowRows.AutoSize = true;
            this.flowRows.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowRows.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowRows.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowRows.Location = new System.Drawing.Point(0, 0);
            this.flowRows.Margin = new System.Windows.Forms.Padding(0);
            this.flowRows.Name = "flowRows";
            this.flowRows.Size = new System.Drawing.Size(497, 0);
            this.flowRows.TabIndex = 0;
            this.flowRows.WrapContents = false;
            // 
            // PalletListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scrollHost);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "PalletListView";
            this.Size = new System.Drawing.Size(497, 150);
            ((System.ComponentModel.ISupportInitialize)(this.scrollHost)).EndInit();
            this.scrollHost.ResumeLayout(false);
            this.scrollHost.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel scrollHost;
        private System.Windows.Forms.FlowLayoutPanel flowRows;

    }
}
