using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class CreatePBJobDialog : Form
    {

        public string JobName => tbPBJobName.Text.Trim();
        public string JobNumber => tbJobNumber.Text.Trim();

        public CreatePBJobDialog()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
            this.KeyPreview = true; this.KeyDown += RoundedModal_KeyDown;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void RoundedModal_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Escape) { this.Close(); } }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
