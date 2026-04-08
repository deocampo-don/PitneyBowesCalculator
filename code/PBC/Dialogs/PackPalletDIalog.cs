
using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1.DIalogs
{
    public partial class PackPalletDIalog : Form
    {
        // ✅ Expose the result
        public int TrayCount { get; private set; }

        public PackPalletDIalog()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this);
            ShadowHelper.ApplyShadow(this);
            // Default UI state
            tbTrays.Text = "0";
            tbTrays.SelectAll();

            // Events
            btnOk.Click += btnOk_Click;
            btnCancel.Click += btnCancel_Click;

            // Optional: press Enter to confirm
            tbTrays.KeyDown += tbTrays_KeyDown;
        }

        private void tbTrays_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                btnOk.PerformClick();
            }
            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            string input = tbTrays.Text.Trim();

            // Step 1: Must contain digits only
            if (!input.All(char.IsDigit))
            {
                MessageDialogBox.ShowDialog(
                    "Invalid Input",
                    "Tray count must contain digits only.",
                    MessageBoxButtons.OK,
                    MessageType.Warning);

                tbTrays.Focus();
                tbTrays.SelectAll();
                return;
            }


            if (!int.TryParse(tbTrays.Text.Trim(), out int trays) || trays <= 0)
            {
          
                MessageDialogBox.ShowDialog(
                    "Invalid Input",
                    "Please enter a valid tray quantity greater than 0.",
                    MessageBoxButtons.OK,
                    MessageType.Warning);


                tbTrays.Focus();
                tbTrays.SelectAll();
                return;
            }
            

            // ✅ Set result and close
            TrayCount = trays;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click_1(object sender, EventArgs e)
        {

        }
    }
}
