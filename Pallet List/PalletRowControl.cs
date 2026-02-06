using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsFormsApp1.Dialogs;
using WindowsFormsApp1.DIalogs;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Packed_And_Ready.View_Button.Pallets_Details;

namespace WindowsFormsApp1
{
    public partial class PalletRowControl : UserControl
    {

        private PbJobModel _model;
        public PalletRowControl()
        {
            InitializeComponent();

        }
        


// ----- Your palette -----
private static readonly Color Green1 = Color.FromArgb(46, 204, 113);
    private static readonly Color Green2 = Color.FromArgb(39, 174, 96);
    private static readonly Color Purple1 = Color.FromArgb(110, 74, 191);
    private static readonly Color Purple2 = Color.FromArgb(110, 74, 191);
    private static readonly Color Blue1 = Color.FromArgb(0, 136, 255);
    private static readonly Color Blue2 = Color.FromArgb(52, 152, 219);
    private static readonly Color Grey = Color.FromArgb(189, 195, 199);

    private static void StylePrimaryEnabled(KryptonButton btn,
        string text, Color back1, Color back2, Color border, Color fore)
    {
        if (btn is null) return;

        btn.Values.Text = text;
        btn.Enabled = true;

        btn.StateCommon.Back.Color1 = back1;
        btn.StateCommon.Back.Color2 = back2;
        btn.StateCommon.Back.ColorStyle = PaletteColorStyle.Linear;

        btn.StateCommon.Border.Color1 = border;
        btn.StateCommon.Border.Color2 = border;
        btn.StateCommon.Border.DrawBorders = PaletteDrawBorders.All;

        btn.StateCommon.Content.ShortText.Color1 = fore;
        btn.StateCommon.Content.ShortText.Color2 = fore;
    }

    private static void StyleNeutralEnabled(KryptonButton btn,
        string text, Color back1, Color back2, Color border, Color fore)
    {
        if (btn is null) return;

        btn.Values.Text = text;
        btn.Enabled = true;

        btn.StateCommon.Back.Color1 = back1;
        btn.StateCommon.Back.Color2 = back2;

        btn.StateCommon.Border.Color1 = border;
        btn.StateCommon.Border.Color2 = border;
        btn.StateCommon.Border.DrawBorders = PaletteDrawBorders.All;

        btn.StateCommon.Content.ShortText.Color1 = fore;
        btn.StateCommon.Content.ShortText.Color2 = fore;
    }

    private static void StyleDisabled(KryptonButton btn,
        string text, Color back, Color border, Color fore)
    {
        if (btn is null) return;

        btn.Values.Text = text;
        btn.Enabled = false;

        btn.StateDisabled.Back.Color1 = back;
        btn.StateDisabled.Back.Color2 = back;

        btn.StateDisabled.Border.Color1 = border;
        btn.StateDisabled.Border.Color2 = border;
        btn.StateDisabled.Border.DrawBorders = PaletteDrawBorders.All;

        btn.StateDisabled.Content.ShortText.Color1 = fore;
        btn.StateDisabled.Content.ShortText.Color2 = fore;
    }

        private void UpdateButtonsByCounts(int? envelopeQty, int? scannedWO)
        {
            int env = envelopeQty ?? 0;
            int swo = scannedWO ?? 0;

            bool hasActivity = (env > 0) || (swo > 0);

            if (hasActivity)
            {
                // Primary: Add To Pallet (purple)
                StylePrimaryEnabled(btnAddPallet, "Add To Pallet", Purple1, Purple2, Purple2, Color.White);

                // Pack: enabled (blue)
                StylePrimaryEnabled(btnPackPallet, "Pack Pallet", Blue1, Blue2, Blue1, Color.White);

                // View: enabled (white w/ grey border)
                StyleNeutralEnabled(btnView, "View", Color.White, Color.White, Grey, Color.Black);
            }
            else
            {
                // Primary: New Pallet (green)
                StylePrimaryEnabled(btnAddPallet, "New Pallet", Green1, Green2, Green2, Color.White);

                // Pack/View: disabled greys
                StyleDisabled(btnPackPallet, "Pack Pallet", Grey, Grey, Color.White);
                StyleDisabled(btnView, "View", Grey, Grey, Color.White);
            }
        }



        private void PanelTableLayout_Paint(object sender, PaintEventArgs e)
        {
            if (_model.Pallets.Count == 0)
            {
                btnAddPallet.Text = "New Pallet";
                btnAddPallet.StateCommon.Back.Color1 = Color.FromArgb(60, 200, 120);
                btnPackPallet.StateCommon.Back.Color1 = Color.FromArgb(198, 198, 198);
                btnView.StateCommon.Back.Color1 = Color.White;
                btnView.StateCommon.Content.ShortText.Color1 = Color.FromArgb(103, 80, 164);
                btnPackPallet.Enabled = false;

            }
        }

        private void RoundedGroupBox_Paint(object sender, PaintEventArgs e)
        {

        }



        private Pallet GetCurrentlySelectedPallet()
        {
            return _model?.Pallets?.FirstOrDefault();
        }
        private void btnAddPallet_Click(object sender, EventArgs e)
        {


            var pallet = GetCurrentlySelectedPallet();
            if (pallet == null)
            {
                MessageBox.Show("Please select a pallet first.");
                return;
            }

            using (var dlg = new AddToPalletDialog(pallet))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    // Refresh UI
                    //LoadDashboard(pallet);
                   // palletDetailsListView1.SetItems(pallet.WorkOrders);
                }
            }
        }


        

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            var items = new List<WorkOrderItem>
    {
        new WorkOrderItem { Code = "CXXX26010101PER0001", Quantity = 150 },
        new WorkOrderItem { Code = "CXXX26010101PER0002", Quantity = 1500 },
        new WorkOrderItem { Code = "CXXX26020103PER0001", Quantity = 1500 },
    };

            using (var dlg = new ViewWOListDialog())
            {
                dlg.SetItems(items);

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    var selected = items.Where(x => x.IsSelected).ToList();
                    // Use selected items
                }
            }
        }

        private void PanelTableLayout_Paint_1(object sender, PaintEventArgs e)
        {

        }


        public void Bind(PbJobModel model)
        {
            _model = model;

            lblPbJobName.Text = model.JobName;
            lblAxRef.Text = model.JobNumber.ToString();

            lblEnvelopeQty.Text =
                $"Envelope Qty: {model.TotalEnvelopeOfJob:N0}";

            lblScannedWOs.Text =
                $"Scanned Work Orders: {model.TotalScannedWOOfJob:N0}";

            // ✅ Disable Pack Pallet for new jobs
            //  btnPackPallet.Enabled = model.TotalScannedWOOfJob > 0;


            // 🔑 Apply the new rule here
            UpdateButtonsByCounts(model?.TotalEnvelopeOfJob, model?.TotalScannedWOOfJob);

        }



        private void btnPackPallet_Click(object sender, EventArgs e)
        {
            using (var dlg = new PackPalletDIalog())
            {
                dlg.ShowDialog(this); // modal, centered to parent
            }
        }


    }

}
