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
            scrollHost = new Krypton.Toolkit.KryptonPanel();
            rowsContainer = new Panel();
            ((System.ComponentModel.ISupportInitialize)scrollHost).BeginInit();
            scrollHost.SuspendLayout();
            SuspendLayout();
            // 
            // scrollHost
            // 
            scrollHost.AutoScroll = true;
            scrollHost.Controls.Add(rowsContainer);
            scrollHost.Dock = DockStyle.Fill;
            scrollHost.Location = new System.Drawing.Point(0, 0);
            scrollHost.Name = "scrollHost";
            scrollHost.Padding = new Padding(11, 14, 0, 9);
            scrollHost.Size = new System.Drawing.Size(435, 141);
            scrollHost.StateCommon.Color1 = System.Drawing.Color.Transparent;
            scrollHost.TabIndex = 0;
            // 
            // rowsContainer
            // 
            rowsContainer.AutoSize = true;
            rowsContainer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rowsContainer.Dock = DockStyle.Top;
            rowsContainer.Location = new System.Drawing.Point(11, 14);
            rowsContainer.Name = "rowsContainer";
            rowsContainer.Size = new System.Drawing.Size(424, 0);
            rowsContainer.TabIndex = 0;
            // 
            // PalletListView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(scrollHost);
            Margin = new Padding(4, 5, 4, 5);
            Name = "PalletListView";
            Size = new System.Drawing.Size(435, 141);
            ((System.ComponentModel.ISupportInitialize)scrollHost).EndInit();
            scrollHost.ResumeLayout(false);
            scrollHost.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel scrollHost;
        private Panel rowsContainer;
    }
}
