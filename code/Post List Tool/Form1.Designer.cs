namespace Post_List_Tool
{
    partial class FrmRoyalMail
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRoyalMail));
            label1 = new Label();
            txtBarcode = new TextBox();
            btnAdd_WorkOrder = new Button();
            listBox1 = new ListBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            deleteToolStripMenuItem = new ToolStripMenuItem();
            button2 = new Button();
            dataGridView1 = new DataGridView();
            txtVer = new Label();
            panel1 = new Panel();
            txtRunType = new TextBox();
            label6 = new Label();
            txtDueOut = new TextBox();
            txtTrigram = new TextBox();
            txtDespatchMethod = new TextBox();
            txtCustomerOffer = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label8 = new Label();
            txtscanneditems = new TextBox();
            dgvItems = new DataGridView();
            btn_add_to_PostList = new Button();
            btn_Generate_post_list = new Button();
            label9 = new Label();
            txtCarriers = new TextBox();
            pictureBox1 = new PictureBox();
            txtName = new Label();
            clbdespatches = new CheckedListBox();
            pictureBox2 = new PictureBox();
            contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(63, 1, 86);
            label1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(16, 125);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(203, 17);
            label1.TabIndex = 0;
            label1.Text = "Scan Work Order Barcode:";
            // 
            // txtBarcode
            // 
            txtBarcode.Location = new Point(251, 120);
            txtBarcode.Margin = new Padding(4, 5, 4, 5);
            txtBarcode.MaxLength = 30;
            txtBarcode.Multiline = true;
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(520, 29);
            txtBarcode.TabIndex = 1;
            txtBarcode.TextChanged += txtBarcode_TextChanged;
            // 
            // btnAdd_WorkOrder
            // 
            btnAdd_WorkOrder.Location = new Point(1047, 952);
            btnAdd_WorkOrder.Margin = new Padding(4, 5, 4, 5);
            btnAdd_WorkOrder.Name = "btnAdd_WorkOrder";
            btnAdd_WorkOrder.Size = new Size(100, 35);
            btnAdd_WorkOrder.TabIndex = 2;
            btnAdd_WorkOrder.Text = "Add Old";
            btnAdd_WorkOrder.UseVisualStyleBackColor = true;
            btnAdd_WorkOrder.Visible = false;
            btnAdd_WorkOrder.Click += btnAdd_workOrder;
            // 
            // listBox1
            // 
            listBox1.ContextMenuStrip = contextMenuStrip1;
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(11, 180);
            listBox1.Margin = new Padding(4, 5, 4, 5);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(12, 284);
            listBox1.TabIndex = 3;
            listBox1.Visible = false;
            listBox1.MouseDown += listBox1_MouseDown;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { deleteToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(123, 28);
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(122, 24);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // button2
            // 
            button2.Location = new Point(1047, 805);
            button2.Margin = new Padding(4, 5, 4, 5);
            button2.Name = "button2";
            button2.Size = new Size(100, 57);
            button2.TabIndex = 4;
            button2.Text = "Generate Old";
            button2.UseVisualStyleBackColor = true;
            button2.Visible = false;
            button2.Click += button2_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(4, 771);
            dataGridView1.Margin = new Padding(4, 5, 4, 5);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1035, 285);
            dataGridView1.TabIndex = 5;
            dataGridView1.Visible = false;
            // 
            // txtVer
            // 
            txtVer.AutoSize = true;
            txtVer.BackColor = Color.FromArgb(63, 1, 86);
            txtVer.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtVer.ForeColor = Color.White;
            txtVer.Location = new Point(1155, 54);
            txtVer.Margin = new Padding(4, 0, 4, 0);
            txtVer.Name = "txtVer";
            txtVer.RightToLeft = RightToLeft.Yes;
            txtVer.Size = new Size(108, 17);
            txtVer.TabIndex = 17;
            txtVer.Text = "V2.0 (Build 1)";
            txtVer.TextAlign = ContentAlignment.TopRight;
            txtVer.Click += label7_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(txtRunType);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(txtDueOut);
            panel1.Controls.Add(txtTrigram);
            panel1.Controls.Add(txtDespatchMethod);
            panel1.Controls.Add(txtCustomerOffer);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(11, 562);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(1040, 154);
            panel1.TabIndex = 18;
            panel1.Paint += panel1_Paint;
            // 
            // txtRunType
            // 
            txtRunType.Location = new Point(532, 35);
            txtRunType.Margin = new Padding(4, 5, 4, 5);
            txtRunType.Name = "txtRunType";
            txtRunType.Size = new Size(132, 27);
            txtRunType.TabIndex = 26;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(429, 40);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(78, 17);
            label6.TabIndex = 25;
            label6.Text = "Run Type";
            // 
            // txtDueOut
            // 
            txtDueOut.Location = new Point(805, 31);
            txtDueOut.Margin = new Padding(4, 5, 4, 5);
            txtDueOut.Name = "txtDueOut";
            txtDueOut.Size = new Size(128, 27);
            txtDueOut.TabIndex = 24;
            txtDueOut.TextChanged += txtDueOut_TextChanged;
            // 
            // txtTrigram
            // 
            txtTrigram.Location = new Point(532, 89);
            txtTrigram.Margin = new Padding(4, 5, 4, 5);
            txtTrigram.Name = "txtTrigram";
            txtTrigram.Size = new Size(132, 27);
            txtTrigram.TabIndex = 23;
            // 
            // txtDespatchMethod
            // 
            txtDespatchMethod.Location = new Point(153, 89);
            txtDespatchMethod.Margin = new Padding(4, 5, 4, 5);
            txtDespatchMethod.Name = "txtDespatchMethod";
            txtDespatchMethod.Size = new Size(267, 27);
            txtDespatchMethod.TabIndex = 22;
            // 
            // txtCustomerOffer
            // 
            txtCustomerOffer.Location = new Point(149, 35);
            txtCustomerOffer.Margin = new Padding(4, 5, 4, 5);
            txtCustomerOffer.Name = "txtCustomerOffer";
            txtCustomerOffer.Size = new Size(267, 27);
            txtCustomerOffer.TabIndex = 21;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.Location = new Point(673, 35);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(107, 17);
            label5.TabIndex = 20;
            label5.Text = "Due Out Date";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(429, 94);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(64, 17);
            label4.TabIndex = 19;
            label4.Text = "Trigram";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(-1, 100);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(139, 17);
            label3.TabIndex = 18;
            label3.Text = "Despatch method:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(-1, 35);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(118, 17);
            label2.TabIndex = 17;
            label2.Text = "Cusomer Offer:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.FromArgb(63, 1, 86);
            label8.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.White;
            label8.Location = new Point(1117, 432);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(119, 17);
            label8.TabIndex = 19;
            label8.Text = "Items Scanned:";
            // 
            // txtscanneditems
            // 
            txtscanneditems.Location = new Point(1244, 432);
            txtscanneditems.Margin = new Padding(4, 5, 4, 5);
            txtscanneditems.Name = "txtscanneditems";
            txtscanneditems.Size = new Size(64, 27);
            txtscanneditems.TabIndex = 20;
            txtscanneditems.Text = "0";
            // 
            // dgvItems
            // 
            dgvItems.BackgroundColor = Color.FromArgb(63, 1, 86);
            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.Location = new Point(11, 160);
            dgvItems.Margin = new Padding(4, 5, 4, 5);
            dgvItems.Name = "dgvItems";
            dgvItems.RowHeadersWidth = 51;
            dgvItems.Size = new Size(1099, 380);
            dgvItems.TabIndex = 21;
            dgvItems.CellContentClick += dataGridView2_CellContentClick;
            dgvItems.CellValidating += dgvItems_CellValidating;
            dgvItems.DataError += dgvItems_DataError;
            dgvItems.RowsRemoved += dgvItems_RowsRemoved;
            dgvItems.UserDeletedRow += dgvItems_UserDeletedRow;
            dgvItems.UserDeletingRow += dgvItems_UserDeletingRow;
            // 
            // btn_add_to_PostList
            // 
            btn_add_to_PostList.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_add_to_PostList.Location = new Point(816, 105);
            btn_add_to_PostList.Margin = new Padding(4, 5, 4, 5);
            btn_add_to_PostList.Name = "btn_add_to_PostList";
            btn_add_to_PostList.Size = new Size(100, 46);
            btn_add_to_PostList.TabIndex = 22;
            btn_add_to_PostList.Text = "Add";
            btn_add_to_PostList.UseVisualStyleBackColor = true;
            btn_add_to_PostList.Click += button1_Click;
            // 
            // btn_Generate_post_list
            // 
            btn_Generate_post_list.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Generate_post_list.Location = new Point(1121, 371);
            btn_Generate_post_list.Margin = new Padding(4, 5, 4, 5);
            btn_Generate_post_list.Name = "btn_Generate_post_list";
            btn_Generate_post_list.Size = new Size(100, 57);
            btn_Generate_post_list.TabIndex = 23;
            btn_Generate_post_list.Text = "Generate Post List";
            btn_Generate_post_list.UseVisualStyleBackColor = true;
            btn_Generate_post_list.Click += button3_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.FromArgb(63, 1, 86);
            label9.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.White;
            label9.Location = new Point(1117, 475);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(113, 17);
            label9.TabIndex = 24;
            label9.Text = "Total Carriers:";
            // 
            // txtCarriers
            // 
            txtCarriers.Location = new Point(1244, 475);
            txtCarriers.Margin = new Padding(4, 5, 4, 5);
            txtCarriers.Name = "txtCarriers";
            txtCarriers.Size = new Size(64, 27);
            txtCarriers.TabIndex = 25;
            txtCarriers.Text = "0";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(63, 1, 86);
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(21, 11);
            pictureBox1.Margin = new Padding(4, 5, 4, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(424, 100);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 26;
            pictureBox1.TabStop = false;
            // 
            // txtName
            // 
            txtName.AutoSize = true;
            txtName.BackColor = Color.FromArgb(63, 1, 86);
            txtName.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtName.ForeColor = Color.White;
            txtName.Location = new Point(1151, 11);
            txtName.Margin = new Padding(4, 0, 4, 0);
            txtName.Name = "txtName";
            txtName.Size = new Size(108, 17);
            txtName.TabIndex = 27;
            txtName.Text = "Post List Tool";
            txtName.Click += txtVer_Click;
            // 
            // clbdespatches
            // 
            clbdespatches.FormattingEnabled = true;
            clbdespatches.Location = new Point(1137, 105);
            clbdespatches.Margin = new Padding(4, 5, 4, 5);
            clbdespatches.Name = "clbdespatches";
            clbdespatches.Size = new Size(195, 224);
            clbdespatches.TabIndex = 28;
            clbdespatches.SelectedIndexChanged += clbdespatches_SelectedIndexChanged;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.White;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.InitialImage = (Image)resources.GetObject("pictureBox2.InitialImage");
            pictureBox2.Location = new Point(912, -14);
            pictureBox2.Margin = new Padding(4, 5, 4, 5);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(216, 165);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 29;
            pictureBox2.TabStop = false;
            // 
            // FrmRoyalMail
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(63, 1, 86);
            ClientSize = new Size(1379, 738);
            Controls.Add(pictureBox2);
            Controls.Add(clbdespatches);
            Controls.Add(dgvItems);
            Controls.Add(txtName);
            Controls.Add(pictureBox1);
            Controls.Add(txtCarriers);
            Controls.Add(label9);
            Controls.Add(btn_Generate_post_list);
            Controls.Add(btn_add_to_PostList);
            Controls.Add(txtscanneditems);
            Controls.Add(label8);
            Controls.Add(panel1);
            Controls.Add(txtVer);
            Controls.Add(dataGridView1);
            Controls.Add(button2);
            Controls.Add(listBox1);
            Controls.Add(btnAdd_WorkOrder);
            Controls.Add(txtBarcode);
            Controls.Add(label1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "FrmRoyalMail";
            Text = "Post List Tool";
            FormClosed += FrmRoyalMail_FormClosed;
            Load += Form1_Load;
            Resize += Form1_Resize;
            contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Button btnAdd_WorkOrder;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Label txtVer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtRunType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDueOut;
        private System.Windows.Forms.TextBox txtTrigram;
        private System.Windows.Forms.TextBox txtDespatchMethod;
        private System.Windows.Forms.TextBox txtCustomerOffer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtscanneditems;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Button btn_add_to_PostList;
        private System.Windows.Forms.Button btn_Generate_post_list;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCarriers;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label txtName;
        private System.Windows.Forms.CheckedListBox clbdespatches;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

