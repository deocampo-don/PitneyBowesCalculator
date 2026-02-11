using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1.Dialogs
{
    public partial class AddToPalletDialog : Form
    {
        private readonly Pallet _targetPallet;
        private readonly IWorkOrderLookup _woLookup;

        private readonly HashSet<string> _scannedCodes =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public AddToPalletDialog(Pallet targetPallet, IWorkOrderLookup woLookup)
        {
            InitializeComponent();

            _targetPallet = targetPallet
                ?? throw new ArgumentNullException(nameof(targetPallet));

            _woLookup = woLookup
                ?? throw new ArgumentNullException(nameof(woLookup));

            txtEnvelopeQty.Text = "0";
            txtScannedWO.Text = "0";
            tbWoBarcode.Text = string.Empty;

            tbWoBarcode.KeyDown += TbWoBarcode_KeyDown;
            btnOk.Click += BtnOK_Click;
            btnCancel.Click += BtnCancel_Click;

            this.Shown += (_, __) => tbWoBarcode.Focus();
        }

        // =====================================================
        // 🔎 SCAN HANDLER (Option B: Create WO per scan)
        // =====================================================

        private async void TbWoBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.Handled = true;
            e.SuppressKeyPress = true;

            var raw = tbWoBarcode.Text?.Trim();
            if (string.IsNullOrEmpty(raw))
                return;

            if (_scannedCodes.Contains(raw))
            {
                System.Media.SystemSounds.Beep.Play();
                tbWoBarcode.SelectAll();
                return;
            }

            // Format: WODATA|123
            var parts = raw.Split('|');
            if (parts.Length != 2)
            {
                MessageBox.Show("Invalid format. Use WOCode|123");
                tbWoBarcode.SelectAll();
                return;
            }

            string woCode = parts[0].Trim();

            int envQty = await _woLookup.GetEnvelopeQtyAsync(raw);

            if (envQty <= 0)
            {
                MessageBox.Show("Invalid quantity.");
                tbWoBarcode.SelectAll();
                return;
            }

            // ✅ Create real WorkOrder per scan
            var workOrder = new WorkOrder(woCode, envQty);

            workOrder.RecordScan();

            _targetPallet.WorkOrders.Add(workOrder);

            _scannedCodes.Add(raw);

            // 🔄 Update UI totals
            txtEnvelopeQty.Text =
                _targetPallet.PalletEnvelopeQty.ToString("N0");

            txtScannedWO.Text =
                _targetPallet.PalletScannedWO.ToString("N0");

            tbWoBarcode.Clear();
        }

        // =====================================================
        // OK / CANCEL
        // =====================================================

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (_targetPallet.WorkOrders.Count == 0)
            {
                MessageBox.Show("No scans to commit.");
                tbWoBarcode.Focus();
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
