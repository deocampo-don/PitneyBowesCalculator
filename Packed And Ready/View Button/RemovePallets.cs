
using System;
using System.Drawing;
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
            CSSDesign.PaintRoundedForm(this, e, _formRadius, Color.Gray);
        }

        private void btnExit_Click(object sender, EventArgs e) => Close();
        private void btnNo_Click_1(object sender, EventArgs e) => Close();

     

        private void btnYes_Click_1(object sender, EventArgs e)
        {
            // ✅ Important: return Yes so parent knows to proceed
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
    }
}
