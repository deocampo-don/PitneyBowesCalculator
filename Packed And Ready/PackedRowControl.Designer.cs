namespace WindowsFormsApp1.Packed_And_Ready
{
    partial class PackedRowControl
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
            this.btnView = new System.Windows.Forms.Button();
            this.txtStatus = new Krypton.Toolkit.KryptonLabel();
            this.txtPackDate = new Krypton.Toolkit.KryptonLabel();
            this.txtPBJobNum = new Krypton.Toolkit.KryptonLabel();
            this.txtPBJobName = new Krypton.Toolkit.KryptonLabel();
            this.lblPackDate = new Krypton.Toolkit.KryptonLabel();
            this.lblTrays = new Krypton.Toolkit.KryptonLabel();
            this.txtTrays = new Krypton.Toolkit.KryptonLabel();
            this.lblEnvelopeQty = new Krypton.Toolkit.KryptonLabel();
            this.txtEnvelopeQty = new Krypton.Toolkit.KryptonLabel();
            this.lblPallets = new Krypton.Toolkit.KryptonLabel();
            this.txtPallets = new Krypton.Toolkit.KryptonLabel();
            this.chkbxStatus = new Krypton.Toolkit.KryptonCheckBox();
            this.pnlDashboard = new System.Windows.Forms.Panel();
            this.pnlDashboard.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnView
            // 
            this.btnView.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(80)))), ((int)(((byte)(164)))));
            this.btnView.CausesValidation = false;
            this.btnView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnView.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(80)))), ((int)(((byte)(164)))));
            this.btnView.FlatAppearance.BorderSize = 0;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnView.ForeColor = System.Drawing.Color.White;
            this.btnView.Location = new System.Drawing.Point(331, 293);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(149, 37);
            this.btnView.TabIndex = 43;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.Location = new System.Drawing.Point(377, 79);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(83, 24);
            this.txtStatus.StateCommon.ShortText.Color1 = System.Drawing.Color.Red;
            this.txtStatus.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatus.TabIndex = 42;
            this.txtStatus.Values.Text = "Not Ready";
            // 
            // txtPackDate
            // 
            this.txtPackDate.Location = new System.Drawing.Point(371, 263);
            this.txtPackDate.Name = "txtPackDate";
            this.txtPackDate.Size = new System.Drawing.Size(90, 24);
            this.txtPackDate.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.txtPackDate.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackDate.TabIndex = 41;
            this.txtPackDate.Values.Text = "01/30/2026";
            // 
            // txtPBJobNum
            // 
            this.txtPBJobNum.Location = new System.Drawing.Point(51, 94);
            this.txtPBJobNum.Name = "txtPBJobNum";
            this.txtPBJobNum.Size = new System.Drawing.Size(45, 24);
            this.txtPBJobNum.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.txtPBJobNum.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPBJobNum.TabIndex = 39;
            this.txtPBJobNum.Values.Text = "0000";
            // 
            // txtPBJobName
            // 
            this.txtPBJobName.Location = new System.Drawing.Point(48, 65);
            this.txtPBJobName.Name = "txtPBJobName";
            this.txtPBJobName.Size = new System.Drawing.Size(116, 31);
            this.txtPBJobName.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.txtPBJobName.StateCommon.ShortText.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPBJobName.TabIndex = 38;
            this.txtPBJobName.Values.Text = "CAPONE";
            // 
            // lblPackDate
            // 
            this.lblPackDate.Location = new System.Drawing.Point(382, 239);
            this.lblPackDate.Name = "lblPackDate";
            this.lblPackDate.Size = new System.Drawing.Size(81, 24);
            this.lblPackDate.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.lblPackDate.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPackDate.TabIndex = 37;
            this.lblPackDate.Values.Text = "Pack Date";
            // 
            // lblTrays
            // 
            this.lblTrays.Location = new System.Drawing.Point(48, 221);
            this.lblTrays.Margin = new System.Windows.Forms.Padding(0);
            this.lblTrays.Name = "lblTrays";
            this.lblTrays.Size = new System.Drawing.Size(48, 24);
            this.lblTrays.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.lblTrays.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrays.TabIndex = 35;
            this.lblTrays.Values.Text = "Trays";
            // 
            // txtTrays
            // 
            this.txtTrays.Location = new System.Drawing.Point(41, 239);
            this.txtTrays.Name = "txtTrays";
            this.txtTrays.Size = new System.Drawing.Size(61, 48);
            this.txtTrays.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.txtTrays.StateCommon.ShortText.Font = new System.Drawing.Font("Arial", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrays.TabIndex = 36;
            this.txtTrays.Values.Text = "00";
            // 
            // lblEnvelopeQty
            // 
            this.lblEnvelopeQty.Location = new System.Drawing.Point(47, 143);
            this.lblEnvelopeQty.Margin = new System.Windows.Forms.Padding(0);
            this.lblEnvelopeQty.Name = "lblEnvelopeQty";
            this.lblEnvelopeQty.Size = new System.Drawing.Size(105, 24);
            this.lblEnvelopeQty.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.lblEnvelopeQty.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnvelopeQty.TabIndex = 33;
            this.lblEnvelopeQty.Values.Text = "Envelope Qty";
            // 
            // txtEnvelopeQty
            // 
            this.txtEnvelopeQty.LabelStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            this.txtEnvelopeQty.Location = new System.Drawing.Point(41, 162);
            this.txtEnvelopeQty.Name = "txtEnvelopeQty";
            this.txtEnvelopeQty.Size = new System.Drawing.Size(104, 48);
            this.txtEnvelopeQty.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.txtEnvelopeQty.StateCommon.ShortText.Font = new System.Drawing.Font("Arial", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEnvelopeQty.TabIndex = 34;
            this.txtEnvelopeQty.Values.Text = "0000";
            // 
            // lblPallets
            // 
            this.lblPallets.Location = new System.Drawing.Point(406, 143);
            this.lblPallets.Name = "lblPallets";
            this.lblPallets.Size = new System.Drawing.Size(57, 24);
            this.lblPallets.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.lblPallets.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPallets.TabIndex = 31;
            this.lblPallets.Values.Text = "Pallets";
            // 
            // txtPallets
            // 
            this.txtPallets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPallets.Location = new System.Drawing.Point(370, 147);
            this.txtPallets.Name = "txtPallets";
            this.txtPallets.Size = new System.Drawing.Size(82, 48);
            this.txtPallets.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            this.txtPallets.StateCommon.ShortText.Font = new System.Drawing.Font("Arial", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPallets.TabIndex = 32;
            this.txtPallets.Values.Text = "000";
            // 
            // chkbxStatus
            // 
            this.chkbxStatus.Location = new System.Drawing.Point(458, 33);
            this.chkbxStatus.Name = "chkbxStatus";
            this.chkbxStatus.Size = new System.Drawing.Size(22, 16);
            this.chkbxStatus.TabIndex = 44;
            this.chkbxStatus.Values.Text = "";
            this.chkbxStatus.CheckedChanged += new System.EventHandler(this.chkbxStatus_CheckedChanged);
            // 
            // pnlDashboard
            // 
            this.pnlDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.pnlDashboard.Controls.Add(this.txtStatus);
            this.pnlDashboard.Controls.Add(this.txtPallets);
            this.pnlDashboard.Location = new System.Drawing.Point(20, 15);
            this.pnlDashboard.Name = "pnlDashboard";
            this.pnlDashboard.Size = new System.Drawing.Size(486, 337);
            this.pnlDashboard.TabIndex = 45;
            // 
            // PackedRowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.Controls.Add(this.chkbxStatus);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.txtPackDate);
            this.Controls.Add(this.txtPBJobNum);
            this.Controls.Add(this.txtPBJobName);
            this.Controls.Add(this.lblPackDate);
            this.Controls.Add(this.lblTrays);
            this.Controls.Add(this.txtTrays);
            this.Controls.Add(this.lblEnvelopeQty);
            this.Controls.Add(this.txtEnvelopeQty);
            this.Controls.Add(this.lblPallets);
            this.Controls.Add(this.pnlDashboard);
            this.Name = "PackedRowControl";
            this.Size = new System.Drawing.Size(523, 375);
            this.pnlDashboard.ResumeLayout(false);
            this.pnlDashboard.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnView;
        private Krypton.Toolkit.KryptonLabel txtStatus;
        private Krypton.Toolkit.KryptonLabel txtPackDate;
        private Krypton.Toolkit.KryptonLabel txtPBJobNum;
        private Krypton.Toolkit.KryptonLabel txtPBJobName;
        private Krypton.Toolkit.KryptonLabel lblPackDate;
        private Krypton.Toolkit.KryptonLabel lblTrays;
        private Krypton.Toolkit.KryptonLabel txtTrays;
        private Krypton.Toolkit.KryptonLabel lblEnvelopeQty;
        private Krypton.Toolkit.KryptonLabel txtEnvelopeQty;
        private Krypton.Toolkit.KryptonLabel lblPallets;
        private Krypton.Toolkit.KryptonLabel txtPallets;
        private Krypton.Toolkit.KryptonCheckBox chkbxStatus;
        private System.Windows.Forms.Panel pnlDashboard;
    }
}
