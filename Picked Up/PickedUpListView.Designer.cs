namespace WindowsFormsApp1.Picked_Up
{
    partial class PickedUpListView
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
            this.pickedScrollHost = new Krypton.Toolkit.KryptonPanel();
            this.pickflowRows = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pickedScrollHost)).BeginInit();
            this.pickedScrollHost.SuspendLayout();
            this.SuspendLayout();
            // 
            // pickedScrollHost
            // 
            this.pickedScrollHost.AutoScroll = true;
            this.pickedScrollHost.Controls.Add(this.pickflowRows);
            this.pickedScrollHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pickedScrollHost.Location = new System.Drawing.Point(0, 0);
            this.pickedScrollHost.Name = "pickedScrollHost";
            this.pickedScrollHost.Size = new System.Drawing.Size(693, 246);
            this.pickedScrollHost.StateCommon.Color1 = System.Drawing.Color.Transparent;
            this.pickedScrollHost.TabIndex = 0;
            this.pickedScrollHost.Paint += new System.Windows.Forms.PaintEventHandler(this.pickedScrollHost_Paint);
            // 
            // pickflowRows
            // 
            this.pickflowRows.AutoSize = true;
            this.pickflowRows.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pickflowRows.Dock = System.Windows.Forms.DockStyle.Top;
            this.pickflowRows.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pickflowRows.Location = new System.Drawing.Point(0, 0);
            this.pickflowRows.Margin = new System.Windows.Forms.Padding(0);
            this.pickflowRows.Name = "pickflowRows";
            this.pickflowRows.Size = new System.Drawing.Size(693, 0);
            this.pickflowRows.TabIndex = 1;
            this.pickflowRows.WrapContents = false;
            // 
            // PickedUpListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pickedScrollHost);
            this.Name = "PickedUpListView";
            this.Size = new System.Drawing.Size(554, 197);
            ((System.ComponentModel.ISupportInitialize)(this.pickedScrollHost)).EndInit();
            this.pickedScrollHost.ResumeLayout(false);
            this.pickedScrollHost.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel pickedScrollHost;
        private System.Windows.Forms.FlowLayoutPanel pickflowRows;
    }
}
