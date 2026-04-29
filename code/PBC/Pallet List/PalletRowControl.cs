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
            if (!PBCMain.Instance.  EnsureConnected()) return;
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
            if (!PBCMain.Instance.EnsureConnected()) return;
            if (_model == null) return;

            // Fix #1 — double-click guard
            btnAddPallet.Enabled = false;
            try
            {
                // 1️⃣ Find working pallet (state = NotReady, not packed)
                var pallet = _model.Pallets
                    .FirstOrDefault(p =>
                        p.State == PalletState.NotReady &&
                        p.PackedAt == null &&
                        p.ShippedAt == null);

                // 2️⃣ Early validation: Ensure pallet is open and not packed
                if (pallet != null)
                {
                    var (earlyChecked, _, earlyStatus) = await EnsurePalletIsOpenAsync(pallet);
                    if (earlyStatus != PalletCheckResult.Open)
                    {
                        await HandlePalletCheckFailure(earlyStatus);
                        return;
                    }

                    pallet = earlyChecked;
                    pallet.WorkOrders = await RqliteClient.LoadWorkOrdersAsync(pallet.PalletId);
                }

                int baseEnvelope = pallet?.WorkOrders.Sum(w => w.Quantity) ?? 0;
                int baseScannedWO = pallet?.WorkOrders.Count ?? 0;

                // 3️⃣ Open the dialog to scan work orders
                using (var dlg = new PitneyBowesCalculator.Dialogs.AddToPalletDialog(baseEnvelope, baseScannedWO))
                {
                    if (dlg.ShowDialog(this) != DialogResult.OK)
                        return;

                    // If no scanned work orders, show an error and exit
                    if (!dlg.ScannedWorkOrders.Any())
                    {
                        MessageDialogBox.ShowDialog("", "No scanned data.", MessageBoxButtons.OK, MessageType.Error);
                        return;
                    }

                    try
                    {
                        // 4️⃣ Check for duplicate work orders based on the setting
                        List<string> duplicates = new List<string>();
                        List<WorkOrder> validWorkOrders;

                        if (RqliteClient.AllowDuplicateBarcodes)
                        {
                            // If duplicates are allowed, just take all scanned work orders
                            validWorkOrders = dlg.ScannedWorkOrders.ToList();
                        }
                        else
                        {
                            // If duplicates are NOT allowed, query the database for existing barcodes
                            var scannedBarcodes = dlg.ScannedWorkOrders
                                .Select(x => x.Barcode)
                                .ToList();

                            // Query the database to get existing barcodes
                            var existing = await RqliteClient.GetExistingBarcodesAsync(scannedBarcodes);
                            duplicates = existing.ToList();

                            // Filter out the work orders that are not duplicates
                            validWorkOrders = dlg.ScannedWorkOrders
                                .Where(x => !duplicates.Contains(x.Barcode))
                                .ToList();

                            // If there are no valid work orders after filtering, show a message
                            if (!validWorkOrders.Any())
                            {
                                MessageDialogBox.ShowDialog("", "All scanned barcodes are duplicates.", MessageBoxButtons.OK, MessageType.Error);
                                return;
                            }
                        }

                        // 5️⃣ Create pallet if needed (if no pallet exists)
                        if (pallet == null)
                        {
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

                        // 6️⃣ Late validation: Ensure the pallet is still open (not packed)
                        var (checkedPallet, _, palletStatus) = await EnsurePalletIsOpenAsync(pallet);
                        if (palletStatus != PalletCheckResult.Open)
                        {
                            if (palletStatus == PalletCheckResult.PalletPacked)
                            {
                                var result = MessageDialogBox.ShowDialog(
                                    "Pallet Already Packed",
                                    "This pallet was already packed by another workstation.\n\n" +
                                    "Would you like to add your scanned work orders to a new pallet instead?",
                                    MessageBoxButtons.YesNo,
                                    MessageType.Warning
                                );

                                if (result != DialogResult.Yes)
                                {
                                    // Refresh job and exit if user doesn't want to migrate work orders
                                    SetButtonsEnabled(false);
                                    try { await PBCMain.Instance.RefreshSingleJobAsync(_model.JobId); }
                                    finally { SetButtonsEnabled(true); }
                                    return;
                                }

                                // Create new pallet and save work orders
                                int newPalletId = await RqliteClient.GetOrCreateWorkingPalletAsync(_model.JobId);

                                var recheck = await RqliteClient.GetExistingBarcodesAsync(
                                    validWorkOrders.Select(x => x.Barcode).ToList()
                                );

                                var safeWorkOrders = validWorkOrders
                                    .Where(x => !recheck.Contains(x.Barcode))
                                    .ToList();

                                if (!safeWorkOrders.Any())
                                {
                                    MessageDialogBox.ShowDialog(
                                        "",
                                        "All scanned work orders were already added by another workstation.",
                                        MessageBoxButtons.OK,
                                        MessageType.Warning
                                    );
                                    // Fix #4: Refresh job and exit
                                    SetButtonsEnabled(false);
                                    try { await PBCMain.Instance.RefreshSingleJobAsync(_model.JobId); }
                                    finally { SetButtonsEnabled(true); }
                                    return;
                                }

                                // Save work orders to the new pallet
                                await RqliteClient.SaveWorkOrdersAsync(newPalletId, safeWorkOrders);
                                // Refresh UI after saving
                                SetButtonsEnabled(false);
                                try { await PBCMain.Instance.RefreshSingleJobAsync(_model.JobId); }
                                finally { SetButtonsEnabled(true); }
                                return;
                            }

                            // Handle other pallet statuses (e.g., pallet deleted, etc.)
                            await HandlePalletCheckFailure(palletStatus);
                            return;
                        }

                        // Update the pallet and save work orders to the current pallet
                        pallet = checkedPallet;

                        // 7️⃣ Save work orders to the pallet
                        await RqliteClient.SaveWorkOrdersAsync(pallet.PalletId, validWorkOrders);

                        // 8️⃣ Show duplicates if any
                        if (duplicates.Any())
                        {
                            var duplicateNames = dlg.ScannedWorkOrders
                                .Where(x => duplicates.Contains(x.Barcode))
                                .Select(x => x.WorkOrderCode)
                                .ToList();

                            var message =
                                $"Inserted: {validWorkOrders.Count}\n" +
                                $"Duplicates skipped: {duplicates.Count}\n\n" +
                                $"{string.Join("\n", duplicateNames)}\n\n" +
                                "Duplicate workorder/s";

                            MessageDialogBox.ShowDialog(
                                "",
                                message,
                                MessageBoxButtons.OK,
                                MessageType.Warning
                            );
                        }

                        // 9️⃣ Refresh the job after saving
                        SetButtonsEnabled(false);
                        try { await PBCMain.Instance.RefreshSingleJobAsync(_model.JobId); }
                        finally { SetButtonsEnabled(true); }
                    }
                    catch (Exception ex)
                    {
                        Utils.WriteUnexpectedError($"AddToPallet failed | JobId={_model?.JobId}");
                        Utils.WriteExceptionError(ex);
                        MessageDialogBox.ShowDialog("", "Error saving pallet: " + ex.Message, MessageBoxButtons.OK, MessageType.Warning);
                    }
                }
            }
            finally
            {
                // Always re-enable the button at the end
                btnAddPallet.Enabled = true;
            }
        }





        private async Task<(Pallet pallet, PbJobModel freshJob, PalletCheckResult status)>
     EnsurePalletIsOpenAsync(Pallet pallet)
        {
            var freshJob = await RqliteClient.LoadSingleJobGraphAsync(_model.JobId);

            if (freshJob == null)
                return (null, null, PalletCheckResult.JobDeleted);

            var dbPallet = freshJob.Pallets
                .FirstOrDefault(p => p.PalletId == pallet.PalletId);

            if (dbPallet == null)
                return (null, freshJob, PalletCheckResult.PalletDeleted);

            if (dbPallet.PackedAt != null)
                return (null, freshJob, PalletCheckResult.PalletPacked);

            pallet.PackedAt = dbPallet.PackedAt;
            pallet.State = dbPallet.State;
            return (pallet, freshJob, PalletCheckResult.Open);
        }

        private async Task HandlePalletCheckFailure(PalletCheckResult status)
        {
            switch (status)
            {
                case PalletCheckResult.JobDeleted:
                    MessageDialogBox.ShowDialog(
                        "Job Deleted",
                        "This job has been deleted by another workstation.",
                        MessageBoxButtons.OK,
                        MessageType.Warning
                    );
                    break;

                case PalletCheckResult.PalletDeleted:
                    MessageDialogBox.ShowDialog(
                        "Pallet Deleted",
                        "This pallet has been deleted or emptied by another workstation.",
                        MessageBoxButtons.OK,
                        MessageType.Warning
                    );
                    break;

                case PalletCheckResult.PalletPacked:
                    MessageDialogBox.ShowDialog(
                        "Pallet Already Packed",
                        "This pallet was already packed by another workstation.",
                        MessageBoxButtons.OK,
                        MessageType.Warning
                    );
                    break;
            }

            SetButtonsEnabled(false);
            try { await PBCMain.Instance.RefreshSingleJobAsync(_model.JobId); }
            finally { SetButtonsEnabled(true); }
        }
        private async void btnPackPallet_Click(object sender, EventArgs e)
        {
            if (!PBCMain.Instance.EnsureConnected()) return;
            if (_model == null) return;

            // Fix #1 — double-click guard
            btnPackPallet.Enabled = false;
            try
            {
                var activePallet = _model.Pallets
                    .FirstOrDefault(p => p.PackedAt == null);

                if (activePallet == null)
                {
                    MessageDialogBox.ShowDialog("", "No active pallet to pack.", MessageBoxButtons.OK, MessageType.Info);
                    return;
                }

                if (activePallet.PalletEnvelopeQty == 0 && activePallet.PalletScannedWO == 0)
                {
                    MessageDialogBox.ShowDialog("", "Cannot pack an empty pallet.", MessageBoxButtons.OK, MessageType.Info);
                    return;
                }

                var (checkedPallet, _, palletStatus) = await EnsurePalletIsOpenAsync(activePallet);

                if (palletStatus != PalletCheckResult.Open)
                {
                    await HandlePalletCheckFailure(palletStatus);
                    return;
                }

                activePallet = checkedPallet;

                using (var dlg = new PackPalletDIalog())
                {
                    if (dlg.ShowDialog(this) != DialogResult.OK)
                        return;

                    if (dlg.TrayCount <= 0)
                    {
                        MessageDialogBox.ShowDialog("", "Tray count must be greater than 0.", MessageBoxButtons.OK, MessageType.Info);
                        return;
                    }

                    int trayCount = dlg.TrayCount;

                    try
                    {
                        // Fix #2 — optimistic lock on DB side
                        var rows = await RqliteClient.UpdatePalletPackingAsync(activePallet.PalletId, trayCount);

                        if (rows == 0)
                        {
                            // ✅ FIX — distinguish deleted vs packed
                            var freshJob = await RqliteClient.LoadSingleJobGraphAsync(_model.JobId);
                            bool palletStillExists = freshJob?.Pallets.Any(p => p.PalletId == activePallet.PalletId) ?? false;

                            if (!palletStillExists)
                            {
                                MessageDialogBox.ShowDialog(
                                    "Pallet Deleted",
                                    "This pallet was deleted by another workstation.",
                                    MessageBoxButtons.OK,
                                    MessageType.Warning
                                );
                            }
                            else
                            {
                                MessageDialogBox.ShowDialog(
                                    "Pallet Already Packed",
                                    "This pallet was already packed by another workstation.",
                                    MessageBoxButtons.OK,
                                    MessageType.Warning
                                );
                            }

                            // Fix #4 — refresh either way
                            SetButtonsEnabled(false);
                            try { await PBCMain.Instance.RefreshSingleJobAsync(_model.JobId); }
                            finally { SetButtonsEnabled(true); }
                            return;
                        }

                        activePallet.TrayCount = trayCount;
                        activePallet.PackedAt = DateTime.Now;
                        activePallet.State = PalletState.Ready;

                        var savedJob = await RqliteClient.LoadSingleJobGraphAsync(_model.JobId);
                        if (savedJob?.LastUpdatedRaw != null)
                            PBCMain.Instance.MarkPendingUpdate(_model.JobId, savedJob.LastUpdatedRaw);

                        // Fix #4
                        SetButtonsEnabled(false);
                        try { await PBCMain.Instance.RefreshSingleJobAsync(_model.JobId); }
                        finally { SetButtonsEnabled(true); }

                        try
                        {
                            var freshJob = await RqliteClient.LoadSingleJobGraphAsync(_model.JobId);
                            if (freshJob != null)
                            {
                                
                                int packedBeforeThis = freshJob.Pallets
                                    .Count(p => p.ShippedAt == null
                                             && p.PackedAt != null
                                             && p.PalletId != activePallet.PalletId);

                                int index = packedBeforeThis + 1;

                                var palletToPrint = freshJob.Pallets
                                    .FirstOrDefault(p => p.PalletId == activePallet.PalletId && p.PackedAt != null);
                                string printJobName = !string.IsNullOrEmpty(palletToPrint.JobNameSnapshot) ? palletToPrint.JobNameSnapshot : freshJob.JobName;

                                if (palletToPrint != null)
                                {
                                    await Task.Run(() => PrintEngine.Print(ev =>
                                    {
                                        PrintLayouts.DrawPalletLabel(ev, freshJob, palletToPrint, index);
                                    }, printJobName));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Utils.WriteUnexpectedError($"Print pallet failed | JobId={_model?.JobId}");
                            Utils.WriteExceptionError(ex);
                            MessageDialogBox.ShowDialog("", "Error printing pallet: " + ex.Message, MessageBoxButtons.OK, MessageType.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        Utils.WriteUnexpectedError($"PackPallet failed | JobId={_model?.JobId}, PalletId={activePallet?.PalletId}");
                        Utils.WriteExceptionError(ex);
                        MessageDialogBox.ShowDialog("", "Error saving pack data: " + ex.Message, MessageBoxButtons.OK, MessageType.Info);
                    }
                }
            }
            finally
            {
                // Fix #1 — always re-enable
                btnPackPallet.Enabled = true;
            }
        }
        private async void btnView_Click_1(object sender, EventArgs e)
        {
            if (!PBCMain.Instance.EnsureConnected()) return;
            if (_model == null) return;

            btnView.Enabled = false;
            try
            {
                var activePallet = _model.Pallets.FirstOrDefault(p => p.PackedAt == null);

                if (activePallet == null)
                {
                    MessageDialogBox.ShowDialog("", "No active pallet.", MessageBoxButtons.OK, MessageType.Info);
                    return;
                }

                var (checkedPallet, _, palletStatus) = await EnsurePalletIsOpenAsync(activePallet);

                if (palletStatus != PalletCheckResult.Open)
                {
                    await HandlePalletCheckFailure(palletStatus);
                    return;
                }

                activePallet = checkedPallet;

                var dbItems = await RqliteClient.LoadWorkOrdersAsync(activePallet.PalletId);
                activePallet.WorkOrders = dbItems;

                using (var dlg = new ViewWOListDialog())
                using (var cts = new System.Threading.CancellationTokenSource())
                {
                    // ✅ Fix — pass cts so dialog can cancel the timer before deleting
                    dlg.SetCancellationSource(cts);

                    var refreshTimer = new System.Windows.Forms.Timer { Interval = 5000 };

                    refreshTimer.Tick += async (s, args) =>
                    {
                        if (cts.Token.IsCancellationRequested)
                            return;

                        var freshJob = await RqliteClient.LoadSingleJobGraphAsync(_model.JobId);

                        if (cts.Token.IsCancellationRequested)
                            return;

                        if (freshJob == null)
                        {
                            refreshTimer.Stop();
                            cts.Cancel();
                            dlg.DialogResult = DialogResult.Abort;
                            dlg.Close();
                            return;
                        }

                        var freshPallet = freshJob.Pallets
                            .FirstOrDefault(p => p.PalletId == activePallet.PalletId);

                        if (freshPallet == null)
                        {
                            refreshTimer.Stop();
                            cts.Cancel();
                            dlg.DialogResult = DialogResult.Abort;
                            dlg.Close();
                            return;
                        }

                        if (freshPallet.PackedAt != null)
                        {
                            refreshTimer.Stop();
                            cts.Cancel();
                            dlg.DialogResult = DialogResult.Abort;
                            dlg.Close();
                            return;
                        }

                        var latest = freshJob.Pallets
                            .FirstOrDefault(p => p.PalletId == activePallet.PalletId)?
                            .WorkOrders ?? new List<WorkOrder>();

                        dlg.RefreshItems(latest);
                    };

                    refreshTimer.Start();
                    dlg.SetItems(_model.JobName, _model.JobNumber.ToString(), _model.JobId, dbItems);
                    dlg.ShowDialog(this);

                    cts.Cancel();
                    refreshTimer.Stop();
                    refreshTimer.Dispose();

                    // ✅ Fix — unregister stale callback so poll goes through RefreshSingleJobAsync directly
                    PBCMain.Instance.UnregisterStaleCallback(_model.JobId);

                    var freshJobAfter = await RqliteClient.LoadSingleJobGraphAsync(_model.JobId);

                    if (freshJobAfter == null)
                    {
                        await HandlePalletCheckFailure(PalletCheckResult.JobDeleted);
                        return;
                    }

                    var freshPalletAfter = freshJobAfter.Pallets
                        .FirstOrDefault(p => p.PalletId == activePallet.PalletId);

                    if (freshPalletAfter == null)
                    {
                        if (dlg.DeletedItems == null || !dlg.DeletedItems.Any())
                            await HandlePalletCheckFailure(PalletCheckResult.PalletDeleted);
                        else
                        {
                            SetButtonsEnabled(false);
                            try { await PBCMain.Instance.RefreshSingleJobAsync(_model.JobId); }
                            finally { SetButtonsEnabled(true); }
                        }
                        return;
                    }

                    if (freshPalletAfter.PackedAt != null && (dlg.DeletedItems == null || !dlg.DeletedItems.Any()))
                    {
                        MessageDialogBox.ShowDialog(
                            "Pallet Packed",
                            "This pallet was packed by another workstation while you had it open.",
                            MessageBoxButtons.OK,
                            MessageType.Info
                        );
                    }

                    var updatedItems = await RqliteClient.LoadWorkOrdersAsync(activePallet.PalletId);
                    activePallet.WorkOrders = updatedItems;

                    UpdateButtonsState();
                    PalletChanged?.Invoke(this, _model);

                    SetButtonsEnabled(false);
                    try { await PBCMain.Instance.RefreshSingleJobAsync(_model.JobId); }
                    finally { SetButtonsEnabled(true); }
                }
            }
            catch (Exception ex)
            {
                Utils.WriteUnexpectedError($"ViewWO failed | JobId={_model?.JobId}");
                Utils.WriteExceptionError(ex);
                MessageDialogBox.ShowDialog("", "Error loading work orders: " + ex.Message, MessageBoxButtons.OK, MessageType.Warning);
            }
            finally
            {
                btnView.Enabled = true;
            }
        }

        public enum PalletCheckResult
        {
            Open,
            JobDeleted,
            PalletDeleted,
            PalletPacked
        }

        private void SetButtonsEnabled(bool enabled)
        {
            btnAddPallet.Enabled = enabled;
            btnPackPallet.Enabled = enabled;
            btnView.Enabled = enabled;
        }
    }
}
    