namespace WindowsFormsApp1.Packed_And_Ready.View_Button.Pallets_Details
{
    partial class PalletDetailsListView
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
            this.flowRow = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlPalletDetailsView = new Krypton.Toolkit.KryptonPanel();
            this.flowRow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPalletDetailsView)).BeginInit();
            this.SuspendLayout();
            // 
            // flowRow
            // 
            this.flowRow.Controls.Add(this.pnlPalletDetailsView);
            this.flowRow.Location = new System.Drawing.Point(0, 0);
            this.flowRow.Name = "flowRow";
            this.flowRow.Size = new System.Drawing.Size(316, 194);
            this.flowRow.TabIndex = 0;
            // 
            // pnlPalletDetailsView
            // 
            this.pnlPalletDetailsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPalletDetailsView.Location = new System.Drawing.Point(3, 3);
            this.pnlPalletDetailsView.Name = "pnlPalletDetailsView";
            this.pnlPalletDetailsView.Size = new System.Drawing.Size(100, 0);
            this.pnlPalletDetailsView.TabIndex = 0;
            // 
            // PalletDetailsListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowRow);
            this.Name = "PalletDetailsListView";
            this.Size = new System.Drawing.Size(316, 194);
            this.flowRow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlPalletDetailsView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowRow;
        private Krypton.Toolkit.KryptonPanel pnlPalletDetailsView;
    }
}
