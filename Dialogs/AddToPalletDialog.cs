using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Properties;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1.Dialogs
{
    public partial class AddToPalletDialog : Form
    {
        private readonly Pallet _targetPallet;
        private readonly IWorkOrderLookup _woLookup;
        int Apos;
        int Bpos;

        private readonly List<WorkOrder> _sessionWorkOrders = new List<WorkOrder>();
        public List<WorkOrder> ScannedWorkOrders => _sessionWorkOrders;
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

        //private async void TbWoBarcode_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode != Keys.Enter)
        //        return;

        //    e.Handled = true;
        //    e.SuppressKeyPress = true;

        //    var raw = tbWoBarcode.Text?.Trim();
        //    if (string.IsNullOrEmpty(raw))
        //        return;

        //    if (_scannedCodes.Contains(raw))
        //    {
        //        System.Media.SystemSounds.Beep.Play();
        //        tbWoBarcode.SelectAll();
        //        return;
        //    }

        //    var parts = raw.Split('|');
        //    if (parts.Length != 2)
        //    {
        //        MessageBox.Show("Invalid format. Use WOCode|123");
        //        tbWoBarcode.SelectAll();
        //        return;
        //    }

        //    string woCode = parts[0].Trim();

        //    int envQty = await _woLookup.GetEnvelopeQtyAsync(raw);

        //    if (envQty <= 0)
        //    {
        //        MessageBox.Show("Invalid quantity.");
        //        tbWoBarcode.SelectAll();
        //        return;
        //    }

        //    var workOrder = new WorkOrder(woCode, envQty);
        //    workOrder.RecordScan();

        //    // ⭐ store only in session
        //    _sessionWorkOrders.Add(workOrder);

        //    _scannedCodes.Add(raw);

        //    // ⭐ update session counters
        //    txtEnvelopeQty.Text =
        //        _sessionWorkOrders.Sum(x => x.Quantity).ToString("N0");

        //    txtScannedWO.Text =
        //        _sessionWorkOrders.Count.ToString();

        //    tbWoBarcode.Clear();
        //}
        private async void TbWoBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.Handled = true;
            e.SuppressKeyPress = true;

            var barcodeValue = tbWoBarcode.Text?.Trim();

            if (string.IsNullOrWhiteSpace(barcodeValue))
                return;

            if (_scannedCodes.Contains(barcodeValue))
            {
                System.Media.SystemSounds.Beep.Play();
                tbWoBarcode.SelectAll();
                return;
            }

            var checkValue = await IsValueValid(barcodeValue);

            if (!checkValue.IsValid)
            {
                MessageBox.Show(checkValue.Message);
                tbWoBarcode.SelectAll();
                return;
            }

            bool isCPSBarcodeScanned = (Apos != -1) && (Bpos != -1);

            if (!isCPSBarcodeScanned)
            {
                MessageBox.Show("Invalid CPS barcode.");
                tbWoBarcode.SelectAll();
                return;
            }

            string woID = barcodeValue.Substring(Apos + 1, Bpos - Apos - 1).ToUpper();
            string woNSID = barcodeValue.Substring(0, Apos).ToUpper();

            int envQty = 0;
            string workOrderCode = "";

            try
            {
                if (string.IsNullOrEmpty(Main.CPSConnectionString))
                {
                    MessageBox.Show("CPS Database Connection String is empty!");
                    return;
                }
                pbSpinner.Visible = true;
                pbSpinner.Image = Resources.spinner_32px;
                lbStatus.Visible = true;
                lbStatus.Text = "Checking";
                string queryPath = GetCpsQueryPath();
                string sqlQuery = File.ReadAllText(queryPath);

                sqlQuery = AddFilterClause(sqlQuery, "PWO.ID = @woID AND PWO.NSID = @woNSID");

                using (var connection = new SqlConnection(Main.CPSConnectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@woID", woID);
                        command.Parameters.AddWithValue("@woNSID", woNSID);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows && await reader.ReadAsync())
                            {
                                workOrderCode = reader["WOName"]?.ToString() ?? "";
                                envQty = Convert.ToInt32(reader["items"]);
                                MessageBox.Show(
    $"WOName: {woID}\n" +
    $"items: {woNSID}"
);                              pbSpinner.Image = Resources.check_32px;
                                lbStatus.Visible = true;
                                lbStatus.Text = workOrderCode;
                            }
                            else
                            {
                                MessageBox.Show("Scanned Work Order not found.");
                                return;
                            }
                        }
                    }
                }

                var workOrder = new WorkOrder(workOrderCode, envQty)
                {
                    Barcode = barcodeValue
                };

                workOrder.RecordScan();

                _sessionWorkOrders.Add(workOrder);
                _scannedCodes.Add(barcodeValue);

                txtEnvelopeQty.Text =
                    _sessionWorkOrders.Sum(x => x.Quantity).ToString("N0");

                txtScannedWO.Text =
                    _sessionWorkOrders.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error scanning barcode:\n" + ex.Message);
            }
            finally
            {
                tbWoBarcode.Clear();
                tbWoBarcode.Focus();
            }
        }
        private string GetCpsQueryPath()
        {
            return Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "sql_query",
                "cps_query.sql"
            );
        }

        public static string AddFilterClause(string sql, string clause)
        {
            // Find position of GROUP BY or ORDER BY (case-insensitive)
            int groupByIndex = sql.IndexOf("GROUP BY", StringComparison.OrdinalIgnoreCase);
            int orderByIndex = sql.IndexOf("ORDER BY", StringComparison.OrdinalIgnoreCase);

            // Find earliest clause (GROUP BY or ORDER BY)
            int insertPos = -1;
            if (groupByIndex >= 0 && orderByIndex >= 0)
                insertPos = Math.Min(groupByIndex, orderByIndex);
            else if (groupByIndex >= 0)
                insertPos = groupByIndex;
            else if (orderByIndex >= 0)
                insertPos = orderByIndex;

            string before, after;
            if (insertPos >= 0)
            {
                before = sql.Substring(0, insertPos);
                after = sql.Substring(insertPos);
            }
            else
            {
                before = sql;
                after = "";
            }

            // Add WHERE or AND
            if (before.IndexOf("WHERE", StringComparison.OrdinalIgnoreCase) >= 0)
                return before.TrimEnd() + " AND " + clause + " " + after;
            else
                return before.TrimEnd() + " WHERE " + clause + " " + after;
        }
        private async Task<(bool IsValid, string Message)> IsValueValid(string barcode)
        {
            int spacepos;
            bool valid = false;
            string message = string.Empty;
            barcode = barcode.ToUpper();
            Apos = barcode.IndexOf('A', 0);
            Bpos = barcode.IndexOf('B', 0);
            spacepos = barcode.IndexOf(' ', 0);

            if ((Apos != -1) && (Bpos != -1) && (spacepos == -1))
                valid = true;
            else
            {

                if (!valid && string.IsNullOrEmpty(message))
                    message = "The scanned value isn’t valid. Please check and try again.";
            }

            return (valid, message);
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
            if (!_sessionWorkOrders.Any())
            {
                MessageBox.Show("No scans.");
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
