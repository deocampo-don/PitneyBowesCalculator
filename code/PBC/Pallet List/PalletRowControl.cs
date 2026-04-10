using Krypton.Toolkit;
using PitneyBowesCalculator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PitneyBowesCalculator.Dialogs;
using PitneyBowesCalculator.DIalogs;


namespace PitneyBowesCalculator
{
    public partial class PalletRowControl : UserControl
    {

        private PbJobModel _model;
        public event EventHandler<PbJobModel> EditRequested;
        public event EventHandler<PbJobModel> SoftDeleteRequested;

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

            var editItem = new ToolStripMenuItem("Edit PB Job");
            editItem.Click += EditPbJob_Click;

            var deleteItem = new ToolStripMenuItem("Delete PB Job");
            deleteItem.Click += DeletePbJob_Click;

            menu.Items.Add(editItem);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(deleteItem);

            this.ContextMenuStrip = menu;
        }

        private void EditPbJob_Click(object sender, EventArgs e)
        {
            if (_model == null)
                return;
            EditRequested?.Invoke(this, _model);
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

        private void DeletePbJob_Click(object sender, EventArgs e)
        {
            if (_model == null)
                return;

            var confirm = MessageDialogBox.ShowDialog(
                "Confirm Delete",
                $"Delete PB Job \"{_model.JobName}\"?",
                MessageBoxButtons.YesNo,
                MessageType.Warning
            );

            if (confirm != DialogResult.Yes)
                return;

            var pallets = _model.Pallets ?? new List<Pallet>();

            bool hasAnyPallets = pallets.Any();

            bool hasNonShipped = pallets.Any(p =>
                p.State == PalletState.Ready ||
                p.State == PalletState.Packed_NotReady ||
                p.State == PalletState.NotReady);

            bool allShipped = hasAnyPallets && pallets.All(p => p.State == PalletState.Shipped);

            if (hasNonShipped)
            {
                MessageDialogBox.ShowDialog(
                    "Cannot Delete",
                    "This job has ongoing or packed pallets.",
                    MessageBoxButtons.OK,
                    MessageType.Warning
                );
                return;
            }

            Utils.WriteUnexpectedError($"Delete job | JobId={_model.JobId}, JobName={_model.JobName}");

            // HARD DELETE
            if (!hasAnyPallets)
            {
                DeleteRequested?.Invoke(this, _model);
                return;
            }

            // SOFT DELETE
            if (allShipped)
            {
                var confirmArchive = MessageDialogBox.ShowDialog(
                    "Archive Job",
                    $"This job already has shipped pallets.\n\n" +
                    $"It cannot be deleted but will be archived instead.\n\n" +
                    $"Proceed?",
                    MessageBoxButtons.YesNo,
                    MessageType.Warning
                );

                if (confirmArchive != DialogResult.Yes)
                    return;

                SoftDeleteRequested?.Invoke(this, _model);
                return;
            }
        }
        public void Bind(PbJobModel model)
        {
            _model = model;
            BoundJob = model;

            lblPbJobName.Text = model.JobName;

            // 🔴 Temp jobs appear in red

            if (model.IsTemp)
            {
                lblPbJobName.StateCommon.ShortText.Color1 = Color.Red;
                lblPbJobName.StateCommon.ShortText.Color2 = Color.Red;
            }
            else
            {
                lblPbJobName.StateCommon.ShortText.Color1 = SystemColors.ControlText;
                lblPbJobName.StateCommon.ShortText.Color2 = SystemColors.ControlText;
            }

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


        private async void btnAddPallet_Click_1(object sender, EventArgs e)
        {
            if (_model == null)
                return;

            // 1️⃣ Find working pallet
            var pallet = _model.Pallets
                .FirstOrDefault(p =>
                    p.State == PalletState.NotReady &&
                    p.PackedAt == null &&
                    p.ShippedAt == null);

            // 2️⃣ Early validation
            if (pallet != null)
            {
                (pallet, _) = await EnsurePalletIsOpenAsync(pallet);
                if (pallet == null)
                    return;

                pallet.WorkOrders = await RqliteClient.LoadWorkOrdersAsync(pallet.PalletId);
            }

            int baseEnvelope = pallet?.WorkOrders.Sum(w => w.Quantity) ?? 0;
            int baseScannedWO = pallet?.WorkOrders.Count ?? 0;

            using (var dlg = new PitneyBowesCalculator.Dialogs.AddToPalletDialog(baseEnvelope, baseScannedWO))
            {
                if (dlg.ShowDialog(this) != DialogResult.OK)
                    return;

                if (!dlg.ScannedWorkOrders.Any())
                {
                    MessageDialogBox.ShowDialog("", "No scanned data.", MessageBoxButtons.OK, MessageType.Error);
                    return;
                }

                try
                {
                    var scannedBarcodes = dlg.ScannedWorkOrders
                        .Select(x => x.Barcode)
                        .ToList();

                    // 3️⃣ Check duplicates
                    var existing = await RqliteClient.GetExistingBarcodesAsync(scannedBarcodes);
                    var duplicates = existing.ToList();

                    var validWorkOrders = dlg.ScannedWorkOrders
                        .Where(x => !duplicates.Contains(x.Barcode))
                        .ToList();

                    if (!validWorkOrders.Any())
                    {
                        MessageDialogBox.ShowDialog("", "All scanned barcodes are duplicates.", MessageBoxButtons.OK, MessageType.Error);
                        return;
                    }

                    // 4️⃣ Create pallet if needed
                    if (pallet == null)
                    {
                        // Guard: ensure job still exists before creating a pallet
                        var freshJob = await RqliteClient.LoadSingleJobGraphAsync(_model.JobId);
                        if (freshJob == null)
                        {
                            MessageDialogBox.ShowDialog(
                                "Job Deleted",
                                "This job has been deleted by another workstation.",
                                MessageBoxButtons.OK,
                                MessageType.Warning
                            );
                            return;
                        }

                        int palletId = await RqliteClient.GetOrCreateWorkingPalletAsync(_model.JobId);

                        pallet = new Pallet
                        {
                            PalletId = palletId,
                            PBJobId = _model.JobId,
                            WorkOrders = new List<WorkOrder>(),
                            State = PalletState.NotReady
                        };

                        _model.Pallets.Add(pallet);
                    }

                    // 5️⃣ Late validation — just check pallet is still open
                    (pallet, _) = await EnsurePalletIsOpenAsync(pallet);
                    if (pallet == null)
                        return;

                    // 6️⃣ Save to DB
                    await RqliteClient.SaveWorkOrdersAsync(pallet.PalletId, validWorkOrders);

                    // 7️⃣ Show duplicates if any
                    if (duplicates.Any())
                    {
                        var message =
                            $"Inserted: {validWorkOrders.Count}\n" +
                            $"Duplicates skipped: {duplicates.Count}\n\n" +
                            $"{string.Join("\n", duplicates)}\n\n" +
                            "Duplicate Barcodes";

                        MessageDialogBox.ShowDialog(
                            "",
                            message,
                            MessageBoxButtons.OK,
                            MessageType.Warning
                        );
                    }

                    // 8️⃣ Refresh from DB — picks up ALL workstations' changes
                    await PBCMain.Instance.RefreshSingleJobAsync(_model.JobId);
                }
                catch (Exception ex)
                {
                    Utils.WriteExceptionError(ex);
                    MessageDialogBox.ShowDialog("", "Error saving pallet: " + ex.Message, MessageBoxButtons.OK, MessageType.Warning);
                }
            }
        }
        private async Task<(Pallet pallet, PbJobModel freshJob)> EnsurePalletIsOpenAsync(Pallet pallet)
        {
            if (_model == null || pallet == null)
                return (null, null);

            var fresh = await RqliteClient.LoadSingleJobGraphAsync(_model.JobId);

            if (fresh == null)
            {
                MessageDialogBox.ShowDialog(
                    "Job Deleted",
                    "This job has been deleted by another workstation.",
                    MessageBoxButtons.OK,
                    MessageType.Warning
                );
                return (null, null);
            }

            var dbPallet = fresh.Pallets
                .FirstOrDefault(p => p.PalletId == pallet.PalletId);

            if (dbPallet == null)
            {
                MessageDialogBox.ShowDialog(
                    "Pallet Deleted",
                    "This pallet has been deleted by another workstation.",
                    MessageBoxButtons.OK,
                    MessageType.Warning
                );
                return (null, null);
            }

            if (dbPallet.PackedAt != null)
            {
                MessageDialogBox.ShowDialog(
                    "Pallet Already Packed",
                    "This pallet was already packed by another workstation.",
                    MessageBoxButtons.OK,
                    MessageType.Warning
                );
                return (null, null);
            }

            pallet.PackedAt = dbPallet.PackedAt;
            pallet.State = dbPallet.State;

            return (pallet, fresh);
        }
        private async void btnPackPallet_Click(object sender, EventArgs e)
        {
            if (_model == null)
                return;

            var activePallet = _model.Pallets
                .FirstOrDefault(p => p.PackedAt == null);

            if (activePallet == null)
            {
                MessageDialogBox.ShowDialog("", "No active pallet to pack.", MessageBoxButtons.OK, MessageType.Info);
                return;
            }

            if (activePallet.PalletEnvelopeQty == 0 &&
                activePallet.PalletScannedWO == 0)
            {
                MessageDialogBox.ShowDialog("", "Cannot pack an empty pallet.", MessageBoxButtons.OK, MessageType.Info);
                return;
            }

            int trayCount;

            (activePallet, _) = await EnsurePalletIsOpenAsync(activePallet);
            if (activePallet == null)
                return;

            using (var dlg = new PackPalletDIalog())
            {
                if (dlg.ShowDialog(this) != DialogResult.OK)
                    return;

                if (dlg.TrayCount <= 0)
                {
                    MessageDialogBox.ShowDialog("", "Tray count must be greater than 0.", MessageBoxButtons.OK, MessageType.Info);
                    return;
                }

                trayCount = dlg.TrayCount;
            }

            try
            {
                var rows = await RqliteClient.UpdatePalletPackingAsync(
                    activePallet.PalletId,
                    trayCount
                );

                if (rows == 0)
                {
                    MessageDialogBox.ShowDialog(
                        "Pallet Already Packed",
                        "This pallet was already packed by another workstation.",
                        MessageBoxButtons.OK,
                        MessageType.Info
                    );

                    await PBCMain.Instance.RefreshSingleJobAsync(_model.JobId);
                    return;
                }

                // ✅ Update model state
                activePallet.TrayCount = trayCount;
                activePallet.PackedAt = DateTime.Now;
                activePallet.State = PalletState.Ready;

                // ✅ Get timestamp AFTER save
                var savedJob = await RqliteClient.LoadSingleJobGraphAsync(_model.JobId);
                if (savedJob?.LastUpdatedRaw != null)
                    PBCMain.Instance.MarkPendingUpdate(_model.JobId, savedJob.LastUpdatedRaw);

             
                await PBCMain.Instance.RefreshSingleJobAsync(_model.JobId);
            }
            catch (Exception ex)
            {
                Utils.WriteExceptionError(ex);
                MessageDialogBox.ShowDialog("", "Error saving pack data: " + ex.Message, MessageBoxButtons.OK, MessageType.Info);
            }
        }

        private async void btnView_Click_1(object sender, EventArgs e)
        {
            if (_model == null)
                return;

            var activePallet = _model.Pallets
                .FirstOrDefault(p => p.PackedAt == null);

            if (activePallet == null)
            {
                MessageDialogBox.ShowDialog("", "No active pallet.", MessageBoxButtons.OK, MessageType.Info);
                return;
            }

            (activePallet, _) = await EnsurePalletIsOpenAsync(activePallet);
            if (activePallet == null)
                return;

            var dbItems = await RqliteClient.LoadWorkOrdersAsync(activePallet.PalletId);
            activePallet.WorkOrders = dbItems;

            using (var dlg = new ViewWOListDialog())
            {
                dlg.SetItems(_model.JobName, _model.JobNumber.ToString(), _model.JobId, dbItems); // ✅ _model.JobId added

                dlg.ShowDialog(this);

                if (dlg.DeletedItems != null && dlg.DeletedItems.Any())
                {
                    (activePallet, _) = await EnsurePalletIsOpenAsync(activePallet);
                    if (activePallet == null)
                        return;

                    var updatedItems = await RqliteClient.LoadWorkOrdersAsync(activePallet.PalletId);

                    if (!updatedItems.Any())
                    {
                        MessageDialogBox.ShowDialog(
                            "Pallet Updated",
                            "All work orders have been removed by another workstation.",
                            MessageBoxButtons.OK,
                            MessageType.Warning
                        );

                        var freshJob = await RqliteClient.LoadSingleJobGraphAsync(_model.JobId);
                        if (freshJob?.LastUpdatedRaw != null)
                            PBCMain.Instance.MarkPendingUpdate(_model.JobId, freshJob.LastUpdatedRaw);
                        return;
                    }

                    activePallet.WorkOrders = updatedItems;
                    UpdateButtonsState();
                    PalletChanged?.Invoke(this, _model);
                }
            }
        }

    }


}
