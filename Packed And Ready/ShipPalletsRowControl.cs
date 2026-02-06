
using System;
using System.Windows.Forms;
using WindowsFormsApp1.Packed_And_Ready.View_Button; 

namespace WindowsFormsApp1.Packed_And_Ready
{
    public partial class ShipPalletsRowControl : UserControl
    {
        public ShipPalletsRowControl()
        {
            InitializeComponent();
            CSSDesign.MakeRounded(btnShipPallets, 10);
        }

        // ⚠️ Make sure btnShipPallets.Click is wired in Designer to this handler
        private void btnShipPallets_Click(object sender, EventArgs e)
        {
            using (var dlg = new ShipPalletsConfirmationDialog())
            {
                // center on parent control (this user control is hosted on Main)
                dlg.StartPosition = FormStartPosition.CenterParent;

                var result = dlg.ShowDialog(this); // modal, blocks until closed

                if (result == DialogResult.OK)
                {
                    // TODO: perform your "ship pallets" action here or raise an event to Main
                    // e.g., ShipSelectedJobs(); or ItemsChanged?.Invoke(...)
                }
                // else Cancel/No/Exit: do nothing
            }
        }
    }
}
