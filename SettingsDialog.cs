using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace WindowsFormsApp1
{
    public partial class SettingsDialog : Form
    {
        public SettingsDialog()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
           

        }

        private void SettingsDialog_Load(object sender, EventArgs e)
        {

        }

        private void kryptonTableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void kryptonPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSettingsCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
