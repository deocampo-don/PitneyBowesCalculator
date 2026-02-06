
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsFormsApp1.Models; // adjust to where Pallet/WorkOrder/PalletScanSession live

namespace WindowsFormsApp1.Dialogs
{
    public partial class AddToPalletDialog : Form
    {
        private readonly Pallet _targetPallet;

        // Temporary scan buffer (discarded on Cancel)
        private readonly PalletScanSession _scanSession = new PalletScanSession();

        // Prevent duplicates within this dialog session
        private readonly HashSet<string> _scannedCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // Optional: abstraction to get EnvelopeQty by WO code
        private readonly IWorkOrderLookup _woLookup;

        public AddToPalletDialog(Pallet targetPallet, IWorkOrderLookup woLookup = null)
        {
            InitializeComponent();

            _targetPallet = targetPallet ?? throw new ArgumentNullException(nameof(targetPallet));
            _woLookup = woLookup ?? new NullWorkOrderLookup(); // fallback returns 0 so you see the flow

            // Make sure the textboxes are ready
            txtEnvelopeQty.Text = "0";     // (uses your exact name)
            txtScannedWO.Text = "0";
            tbWoBarcode.Text = string.Empty;

            // Scanner usually “types” and sends Enter — use KeyDown to commit each scan
            tbWoBarcode.KeyDown += TbWoBarcode_KeyDown;

            // Buttons
            btnOk.Click += BtnOK_Click;
            btnCancel.Click += BtnCancel_Click;

            // Optional: focus the barcode box immediately
            this.Shown += (_, __) => tbWoBarcode.Focus();
        }

        private void TbWoBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            e.SuppressKeyPress = true;

            var raw = tbWoBarcode.Text?.Trim();
            if (string.IsNullOrEmpty(raw)) return;

            // Avoid duplicate scans within this dialog session
            if (_scannedCodes.Contains(raw))
            {
                // Optional: blink/notify user
                System.Media.SystemSounds.Beep.Play();
                tbWoBarcode.SelectAll();
                return;
            }

            // Resolve to WO code + envelope qty
            var woCode = ParseWorkOrderCode(raw);
            var envQty = _woLookup.GetEnvelopeQty(woCode); // e.g., DB call; return 0 if unknown

            // If you prefer to reject unknown codes (envQty == 0), uncomment:
            // if (envQty <= 0) { MessageBox.Show("Unknown WO barcode.", "Scan", MessageBoxButtons.OK, MessageBoxIcon.Warning); tbWoBarcode.SelectAll(); return; }

            // Record into the temporary session (not committing to pallet yet)
            _scanSession.RegisterScan(envQty);

            // Track this code to prevent duplicates during this session
            _scannedCodes.Add(raw);

            // Update UI
            txtEnvelopeQty.Text = _scanSession.PendingEnvelopeQty.ToString("N0");
            txtScannedWO.Text = _scanSession.PendingScannedWO.ToString("N0");

            // Prepare for the next scan
            tbWoBarcode.Clear();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            // Discard the temporary state
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            // If nothing scanned, optionally block commit
            if (_scanSession.PendingScannedWO <= 0)
            {
                // You can allow 0 if desired. Otherwise:
                MessageBox.Show("No scans to commit.", "Add to Pallet", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbWoBarcode.Focus();
                return;
            }

            // Create a single WO entry representing this scan batch (or split if your business wants per-code entries)
            var batchWO = new WorkOrder
            {
                Code = BuildBatchCode(), // e.g., "BATCH-20260205-1530"
                EnvelopeQty = _scanSession.PendingEnvelopeQty
            };

            // Respect your domain invariant: 1 scan == +1
            for (int i = 0; i < _scanSession.PendingScannedWO; i++)
                batchWO.RecordScan();

            _targetPallet.WorkOrders.Add(batchWO);

            // You may set packed time on the pallet “now” or when trays are entered elsewhere
            _targetPallet.PackedTime = DateTime.Now;

            DialogResult = DialogResult.OK;
            Close();
        }

        // Extract WO code from the barcode. Adjust to your barcode format.
        private string ParseWorkOrderCode(string barcode)
        {
            // If the barcode is already the code, return as-is.
            // Otherwise, add parsing here (prefix/suffix removal, etc.)
            return barcode?.Trim() ?? string.Empty;
        }

        private string BuildBatchCode() =>
            $"BATCH-{DateTime.Now:yyyyMMdd-HHmmss}";
    }

    // ---------- Lookup abstraction you can wire to DB later ----------
    public interface IWorkOrderLookup
    {
        int GetEnvelopeQty(string woCode);
    }

    public sealed class NullWorkOrderLookup : IWorkOrderLookup
    {
        public int GetEnvelopeQty(string woCode) => 0; // for smoke testing
    }
}
