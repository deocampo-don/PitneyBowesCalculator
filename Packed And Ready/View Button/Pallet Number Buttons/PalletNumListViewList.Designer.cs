namespace WindowsFormsApp1.Packed_And_Ready.View_Button
{
    partial class PalletNumListViewList
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
            this.rowFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.scrollHost = new Krypton.Toolkit.KryptonPanel();
            this.rowFlow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scrollHost)).BeginInit();
            this.SuspendLayout();
            // 
            // rowFlow
            // 
            this.rowFlow.Controls.Add(this.scrollHost);
            this.rowFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rowFlow.Location = new System.Drawing.Point(0, 0);
            this.rowFlow.Name = "rowFlow";
            this.rowFlow.Size = new System.Drawing.Size(299, 179);
            this.rowFlow.TabIndex = 0;
            // 
            // scrollHost
            // 
            this.scrollHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollHost.Location = new System.Drawing.Point(3, 3);
            this.scrollHost.Name = "scrollHost";
            this.scrollHost.Size = new System.Drawing.Size(293, 0);
            this.scrollHost.TabIndex = 0;
            // 
            // PalletNumListViewList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rowFlow);
            this.Name = "PalletNumListViewList";
            this.Size = new System.Drawing.Size(299, 179);
            this.rowFlow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scrollHost)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel rowFlow;
        private Krypton.Toolkit.KryptonPanel scrollHost;
    }
}
