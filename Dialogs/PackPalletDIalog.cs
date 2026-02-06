
using System;
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
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(tbTrays.Text.Trim(), out int trays) || trays <= 0)
            {
                MessageBox.Show(
                    "Please enter a valid tray quantity greater than 0.",
                    "Invalid Input",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

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
    }
}
