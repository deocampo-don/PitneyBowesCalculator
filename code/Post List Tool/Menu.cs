using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Post_List_Tool
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
            this.Font = new Font("Microsoft Sans Serif", 8.25F);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var UKMailForm = new FrmUKmail();
            UKMailForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)

        {
            var RoyalMailForm = new FrmRoyalMail();
            RoyalMailForm.Show();
            this.Hide();
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
          /*  switch (Properties.Settings.Default.StartUPScreen)
            {
                case ("Royal Mail"):
                    var RoyalMailForm = new FrmRoyalMail();
                    RoyalMailForm.Show();
                    this.Hide();
                    break;
                case ("UK MAIL"):
                    var UKMailForm = new FrmUKmail();
                    UKMailForm.Show();
                    this.Hide();
                    break;
                default:
                    break;

            }*/
        }

        private void FrmMenu_Shown(object sender, EventArgs e)
        {

          //  MessageBox.Show(Properties.Settings.Default.StartUPScreen);
            switch (Properties.Settings.Default.StartUPScreen)
            {
                case ("Royal Mail"):
                    var RoyalMailForm = new FrmRoyalMail();
                    RoyalMailForm.Show();
                    this.Hide();
                    break;
                case ("UK MAIL"):
                    var UKMailForm = new FrmUKmail();
                    UKMailForm.Show();
                    this.Hide();
                    break;
                default:
                    break;

            }
        }
    }
}
