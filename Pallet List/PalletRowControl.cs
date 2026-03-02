using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Dialogs;
using WindowsFormsApp1.DIalogs;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Services;



namespace WindowsFormsApp1
{
    public partial class PalletRowControl : UserControl
    {

        private PbJobModel _model;
        public PbJobModel BoundJob { get; private set; }

        public event EventHandler<PbJobModel> DeleteRequested;
        public event EventHandler<PbJobModel> PalletChanged;


        public PalletRowControl()
        {
            InitializeComponent();
            InitializeContextMenu();
            this.AutoScaleMode = AutoScaleMode.Dpi;


        }

        private void InitializeContextMenu()
        {
            var menu = new ContextMenuStrip();

            var deleteItem = new ToolStripMenuItem("Delete PB Job");
            deleteItem.Click += DeletePbJob_Click;

            menu.Items.Add(deleteItem);

            // Apply to the whole row
            this.ContextMenuStrip = menu;
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



        private void UpdateButtonsState()
        {
            if (_model == null)
                return;

            // 🔎 Get ONLY the current working pallet (not yet packed)
            var activePallet = _model.Pallets
                .FirstOrDefault(p => p.PackedAt == null);

            bool hasActive = activePallet != null;

            bool activeHasData = hasActive &&
                                 (activePallet.PalletEnvelopeQty > 0 ||
                                  activePallet.PalletScannedWO > 0);

            // -------------------------
            // Add / Pack logic
            // -------------------------
            if (hasActive)
            {
                // Continue adding to existing pallet
                StylePrimaryEnabled(
                    btnAddPallet,
                    "Add To Pallet",
                    Purple1, Purple2, Purple2, Color.White);

                if (activeHasData)
                {
                    StylePrimaryEnabled(
                        btnPackPallet,
                        "Pack Pallet",
                        Blue1, Blue2, Blue1, Color.White);
                }
                else
                {
                    StyleDisabled(
                        btnPackPallet,
                        "Pack Pallet",
                        Grey, Grey, Color.White);
                }
            }
            else
            {
                // All pallets packed → new pallet allowed
                StylePrimaryEnabled(
                    btnAddPallet,
                    "New Pallet",
                    Green1, Green2, Green2, Color.White);

                StyleDisabled(
                    btnPackPallet,
                    "Pack Pallet",
                    Grey, Grey, Color.White);
            }

            // -------------------------
            // View logic
            // -------------------------
            if (activeHasData)
            {
                StyleNeutralEnabled(
                    btnView,
                    "View",
                    Color.White, Color.White,
                    Grey, Color.Black);
            }
            else
            {
                StyleDisabled(
                    btnView,
                    "View",
                    Grey, Grey, Color.White);
            }
        }




        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            if (_model == null)
                return;

            var activePallet = _model.Pallets
                .FirstOrDefault(p => p.PackedAt == null);

            if (activePallet == null)
            {
                MessageBox.Show("No active pallet.");
                return;
            }

            using (var dlg = new ViewWOListDialog())
            {
                dlg.SetItems(activePallet.WorkOrders);

                dlg.ShowDialog(this);

                // 🔥 After dialog closes, sync in-memory model
                if (dlg.DeletedItems != null && dlg.DeletedItems.Any())
                {
                    foreach (var deleted in dlg.DeletedItems)
                        activePallet.WorkOrders.Remove(deleted);

                    UpdateButtonsState();
                    PalletChanged?.Invoke(this, _model);
                }
            }
        }

    

        private void DeletePbJob_Click(object sender, EventArgs e)
        {
            if (_model == null)
                return;

            var confirm = MessageBox.Show(
                $"Delete PB Job \"{_model.JobName}\"?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            DeleteRequested?.Invoke(this, _model);
        }




        public void Bind(PbJobModel model)
        {
            _model = model;
            BoundJob = model;   // ✅ THIS LINE WAS MISSING

            lblPbJobName.Text = model.JobName;
            lblAxRef.Text = model.JobNumber.ToString();

            var activePallet = model.GetActivePallet();

            int envelopeQty = activePallet?.PalletEnvelopeQty ?? 0;
            int scannedWO = activePallet?.PalletScannedWO ?? 0;

            lblEnvelopeQty.Text = $"Envelope Qty: {envelopeQty:N0}";
            lblScannedWOs.Text = $"Scanned Work Orders: {scannedWO:N0}";

            UpdateButtonsState();
        }



        // -----------------------------
        // Actions
        // -----------------------------

        
        private async void btnAddPallet_Click(object sender, EventArgs e)
        {
            if (_model == null)
                return;

            // 🧠 Get existing unpacked pallet OR create new one
            var pallet = _model.GetOrCreateWorkingPallet();

            bool isNewPallet = !_model.Pallets.Contains(pallet);

            // 🔌 Choose lookup strategy
            IWorkOrderLookup lookup = new TestWorkOrderLookup();

            using (var dlg = new AddToPalletDialog(pallet, lookup))
            {
                if (dlg.ShowDialog(this) != DialogResult.OK)
                    return;

                if (pallet.PalletEnvelopeQty == 0 &&
                    pallet.PalletScannedWO == 0)
                {
                    MessageBox.Show("No scanned data.");
                    return;
                }
            }

            // ✅ If this was newly created, attach locally
            if (isNewPallet)
                _model.Pallets.Add(pallet);

            try
            {
                // 💾 If new pallet → save pallet first
                if (isNewPallet)
                {
                    int palletId = await RqliteClient.SavePalletAsync(pallet);
                    pallet.PalletId = palletId;
                }

                // 💾 Save work orders (always)
                await RqliteClient.SaveWorkOrdersAsync(
                    pallet.PalletId,
                    pallet.WorkOrders
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving pallet: " + ex.Message);

                if (isNewPallet)
                    _model.Pallets.Remove(pallet);

                return;
            }

            UpdateButtonsState();

             PalletChanged?.Invoke(this, _model);
        }
        private async void btnPackPallet_Click(object sender, EventArgs e)
        {

            if (_model == null)
                return;

            // 1️⃣ Get active (unpacked) pallet
            var activePallet = _model.Pallets
                .FirstOrDefault(p => p.PackedAt == null);

            if (activePallet == null)
            {
                MessageBox.Show("No active pallet to pack.");
                return;
            }

            // 2️⃣ Do not allow empty pallet
            if (activePallet.PalletEnvelopeQty == 0 &&
                activePallet.PalletScannedWO == 0)
            {
                MessageBox.Show("Cannot pack an empty pallet.");
                return;
            }

            using (var dlg = new PackPalletDIalog())
            {
                if (dlg.ShowDialog(this) != DialogResult.OK)
                    return;

                if (dlg.TrayCount <= 0)
                {
                    MessageBox.Show("Tray count must be greater than 0.");
                    return;
                }

                // 3️⃣ Apply locally first
                activePallet.TrayCount = dlg.TrayCount;
                activePallet.PackedAt  = DateTime.Now;

            }

            try
            {
                // 4️⃣ Save to DB
                await RqliteClient.UpdatePalletPackingAsync(
                    activePallet.PalletId,
                    activePallet.TrayCount,
                    activePallet.PackedAt
                );
            }
            catch (Exception ex)
            {
                // Rollback
                activePallet.PackedAt = null;
                activePallet.TrayCount = 0;

                MessageBox.Show("Error saving pack data: " + ex.Message);
                return;
            }

            // 5️⃣ Refresh UI
            UpdateButtonsState();

            // 6️⃣ Notify parent view
            PalletChanged?.Invoke(this, _model);

        }

    }
}
