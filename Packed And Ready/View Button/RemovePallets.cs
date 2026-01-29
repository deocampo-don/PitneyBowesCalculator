using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Packed_And_Ready.View_Button
{
    public partial class RemovePallets : Form
    {

        private int _formRadius = 12;
        public RemovePallets()
        {
            InitializeComponent();
            btnNo.Focus();

            CSSDesign.ApplyRoundedCorners(this, 20);
            Paint += RemovePallets_Paint;

        }
        private void RemovePallets_Paint(object sender, PaintEventArgs e)
        {
            CSSDesign.PaintRoundedForm(
                this,
                e,
                _formRadius,
                Color.Gray

            );
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    

        private void btnNo_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
