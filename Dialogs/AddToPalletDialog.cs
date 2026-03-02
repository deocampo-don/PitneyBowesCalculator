using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1.Dialogs
{
    public partial class AddToPalletDialog : Form
    {
        private readonly Pallet _targetPallet;
        private readonly IWorkOrderLookup _woLookup;
        int Apos;
        int Bpos;


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


        private async Task<(bool IsValid, string Message)> IsValueValid(string barcode)
        {
            barcode = barcode?.Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(barcode))
                return (false, "Barcode cannot be empty.");

            if (barcode.Length != 21)
                return (false, "Barcode length is invalid.");

            string prefix = barcode.Substring(0, 5);
            string datePart = barcode.Substring(5, 8);
            string typePart = barcode.Substring(13, 3);
            string sequencePart = barcode.Substring(16, 4);

            bool isPrefixValid = prefix.All(char.IsLetter);
            bool isDateValid = datePart.All(char.IsDigit);
            bool isTypeValid = typePart.All(char.IsLetter);
            bool isSequenceValid = sequencePart.All(char.IsDigit);

            if (isPrefixValid && isDateValid && isTypeValid && isSequenceValid)
                return (true, string.Empty);

            return (false, "Invalid CPS barcode format.");
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

        private void tbWoBarcode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
