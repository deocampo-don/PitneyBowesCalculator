using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.DIalogs
{
    public partial class AddToPalletDialog : Form
    {
        public AddToPalletDialog()
        {
            InitializeComponent();
           
                new Font("Segoe UI", 10f, FontStyle.Regular);

        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
