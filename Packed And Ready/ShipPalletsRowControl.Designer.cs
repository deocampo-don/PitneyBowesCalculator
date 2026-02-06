namespace WindowsFormsApp1.Packed_And_Ready
{
    partial class ShipPalletsRowControl
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
            this.btnShipPallets = new System.Windows.Forms.Button();
            this.chkbxSelectAll = new Krypton.Toolkit.KryptonCheckBox();
            this.lblSelectAll = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnShipPallets
            // 
            this.btnShipPallets.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnShipPallets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(222)))), ((int)(((byte)(248)))));
            this.btnShipPallets.CausesValidation = false;
            this.btnShipPallets.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShipPallets.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(80)))), ((int)(((byte)(164)))));
            this.btnShipPallets.FlatAppearance.BorderSize = 0;
            this.btnShipPallets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShipPallets.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShipPallets.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnShipPallets.Location = new System.Drawing.Point(140, 9);
            this.btnShipPallets.Name = "btnShipPallets";
            this.btnShipPallets.Size = new System.Drawing.Size(183, 51);
            this.btnShipPallets.TabIndex = 44;
            this.btnShipPallets.Text = "Ship Pallets";
            this.btnShipPallets.UseVisualStyleBackColor = false;
            this.btnShipPallets.Click += new System.EventHandler(this.btnShipPallets_Click);
            // 
            // chkbxSelectAll
            // 
            this.chkbxSelectAll.Location = new System.Drawing.Point(30, 29);
            this.chkbxSelectAll.Name = "chkbxSelectAll";
            this.chkbxSelectAll.Size = new System.Drawing.Size(22, 16);
            this.chkbxSelectAll.TabIndex = 46;
            this.chkbxSelectAll.Values.Text = "";
            // 
            // lblSelectAll
            // 
            this.lblSelectAll.AutoSize = true;
            this.lblSelectAll.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectAll.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblSelectAll.Location = new System.Drawing.Point(58, 29);
            this.lblSelectAll.Name = "lblSelectAll";
            this.lblSelectAll.Size = new System.Drawing.Size(71, 20);
            this.lblSelectAll.TabIndex = 47;
            this.lblSelectAll.Text = "Select All";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnShipPallets);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(346, 67);
            this.panel1.TabIndex = 49;
            // 
            // ShipPalletsRowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSelectAll);
            this.Controls.Add(this.chkbxSelectAll);
            this.Controls.Add(this.panel1);
            this.Name = "ShipPalletsRowControl";
            this.Size = new System.Drawing.Size(352, 73);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnShipPallets;
        private Krypton.Toolkit.KryptonCheckBox chkbxSelectAll;
        private System.Windows.Forms.Label lblSelectAll;
        private System.Windows.Forms.Panel panel1;
    }
}
