namespace WindowsFormsApp1.Packed_And_Ready
{
    partial class PackedListView
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
            this.packedFlowRow = new System.Windows.Forms.FlowLayoutPanel();
            this.packedScrollHost = new Krypton.Toolkit.KryptonPanel();
            this.packedFlowRow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packedScrollHost)).BeginInit();
            this.SuspendLayout();
            // 
            // packedFlowRow
            // 
            this.packedFlowRow.AutoScroll = true;
            this.packedFlowRow.Controls.Add(this.packedScrollHost);
            this.packedFlowRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packedFlowRow.Location = new System.Drawing.Point(0, 0);
            this.packedFlowRow.Name = "packedFlowRow";
            this.packedFlowRow.Size = new System.Drawing.Size(616, 429);
            this.packedFlowRow.TabIndex = 0;
            // 
            // packedScrollHost
            // 
            this.packedScrollHost.AutoScroll = true;
            this.packedScrollHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packedScrollHost.Location = new System.Drawing.Point(5, 5);
            this.packedScrollHost.Margin = new System.Windows.Forms.Padding(5);
            this.packedScrollHost.Name = "packedScrollHost";
            this.packedScrollHost.Size = new System.Drawing.Size(576, 0);
            this.packedScrollHost.StateCommon.Color1 = System.Drawing.Color.Transparent;
            this.packedScrollHost.TabIndex = 0;
            // 
            // PackedListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.packedFlowRow);
            this.Name = "PackedListView";
            this.Size = new System.Drawing.Size(616, 429);
            this.packedFlowRow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.packedScrollHost)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel packedFlowRow;
        private Krypton.Toolkit.KryptonPanel packedScrollHost;
    }
}
