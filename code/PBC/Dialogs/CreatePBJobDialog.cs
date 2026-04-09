using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PitneyBowesCalculator
{
    public partial class CreatePBJobDialog : Form
    {

        public string JobName => tbPBJobName.Text.Trim();
        public string JobNumber => tbJobNumber.Text.Trim();
        public bool IsTemp => cbPBTemp.Checked;
        private PbJobModel _job;
        public CreatePBJobDialog()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this);
            ShadowHelper.ApplyShadow(this);
            this.KeyPreview = true; this.KeyDown += RoundedModal_KeyDown;
        }
        public CreatePBJobDialog(PbJobModel job) : this()
        {
            _job = job;

            tbPBJobName.Text = job.JobName;
            tbJobNumber.Text = job.JobNumber.ToString();
            cbPBTemp.Checked = job.IsTemp;
            btnCreatePBJob.Text = "Save Changes";
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void RoundedModal_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Escape) 
            { 
                this.Close(); 
            }
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
