using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Data.SqlClient;
using PitneyBowesCalculator;
namespace PitneyBowesCalculator.Dialogs
{
    public partial class AddToPalletDialog : Form
    {
     
        int Apos;
        int Bpos;
        public static event Action OnCpsConfigUpdated;
        private readonly List<WorkOrder> _sessionWorkOrders = new List<WorkOrder>();
        public List<WorkOrder> ScannedWorkOrders => _sessionWorkOrders;
        private readonly HashSet<string> _scannedCodes =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private string _preparedCpsQuery;
        private int _baseEnvelopeQty;
        private int _baseScannedWO;
        private bool _scanInProgress = false;
        public AddToPalletDialog(int envelopeQty, int scannedWO)
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this);
            ShadowHelper.ApplyShadow(this);
            txtEnvelopeQty.Text = "0";
            txtScannedWO.Text = "0";
            tbWoBarcode.Text = string.Empty;

            tbWoBarcode.KeyDown += TbWoBarcode_KeyDown;
            btnOk.Click += BtnOK_Click;
            btnCancel.Click += BtnCancel_Click;
            this.Shown += (_, __) => tbWoBarcode.Focus();
            PrepareCpsQuery();
            SettingsDialogAdmin.OnCpsConfigUpdated += async () =>
            {
                await PBCMain.LoadCPSConfig();
                PrepareCpsQuery();
            };
            _baseEnvelopeQty = envelopeQty;
            _baseScannedWO = scannedWO;
            txtEnvelopeQty.Text = envelopeQty.ToString("N0");
            txtScannedWO.Text = scannedWO.ToString();


        }

        private async void TbWoBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (_scanInProgress)
                return;

            _scanInProgress = true;

            e.Handled = true;
            e.SuppressKeyPress = true;

            var barcodeValue = tbWoBarcode.Text?.Trim();

            if (string.IsNullOrWhiteSpace(barcodeValue))
            {
                _scanInProgress = false;
                return;
            }

            if (_scannedCodes.Contains(barcodeValue))
            {
                System.Media.SystemSounds.Beep.Play();
                tbWoBarcode.SelectAll();
              
                MessageDialogBox.ShowDialog(
                    "",
                    "You've already scanned this barcode!",
                    MessageBoxButtons.OK,
                    MessageType.Info
                 );
                _scanInProgress = false;
                return;
            }

            try
            {
                var checkValue = await IsValueValid(barcodeValue);

                if (!checkValue.IsValid)
                {
                    
                    MessageDialogBox.ShowDialog("", checkValue.Message, MessageBoxButtons.OK, MessageType.Info);
                    tbWoBarcode.SelectAll();
                    _scanInProgress = false;
                    return;
                }

                bool isCPSBarcodeScanned = (Apos != -1) && (Bpos != -1);

                if (!isCPSBarcodeScanned)
                {
                    
                    MessageDialogBox.ShowDialog("Error", "Invalid CPS barcode.", MessageBoxButtons.OK, MessageType.Error);
                    tbWoBarcode.SelectAll();
                    _scanInProgress = false;
                    return;
                }

                string woID = barcodeValue.Substring(Apos + 1, Bpos - Apos - 1).ToUpper();
                string woNSID = barcodeValue.Substring(0, Apos).ToUpper();

                int envQty = 0;
                string workOrderCode = "";

                Utils.showStatusAndSpinner(lbStatus, pbSpinner, "Checking...");

                using (var connection = new SqlConnection(PBCMain.CPSConnectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(_preparedCpsQuery, connection))
                    {
                        command.Parameters.AddWithValue("@woID", woID);
                        command.Parameters.AddWithValue("@woNSID", woNSID);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows && await reader.ReadAsync())
                            {
                                workOrderCode = reader["WOName"]?.ToString() ?? "";
                                envQty = Convert.ToInt32(reader["items"]);
                            }
                            else
                            {
                                
                                MessageDialogBox.ShowDialog("", "Scanned Work Order not found.", MessageBoxButtons.OK, MessageType.Info);
                                Utils.errorStatusAndSpinner(lbStatus, pbSpinner, "Not found!");
                                return;
                            }
                        }
                    }
                }

                Utils.hideStatusAndSpinner(lbStatus, pbSpinner, workOrderCode);

                var workOrder = new WorkOrder(workOrderCode, envQty)
                {
                    Barcode = barcodeValue
                };

                workOrder.RecordScan();

                _sessionWorkOrders.Add(workOrder);
                _scannedCodes.Add(barcodeValue);

                int sessionEnv = _sessionWorkOrders.Sum(x => x.Quantity);
                int sessionWO = _sessionWorkOrders.Count;

                txtEnvelopeQty.Text = (_baseEnvelopeQty + sessionEnv).ToString("N0");
                txtScannedWO.Text = (_baseScannedWO + sessionWO).ToString();
            }
            catch (SqlException ex)
            {
                Utils.WriteUnexpectedError($"SQL error during scan | Barcode={barcodeValue}");
                Utils.WriteExceptionError(ex);

                if (ex.Number == 18456) // Login failed
                {
                    MessageDialogBox.ShowDialog(
                        "Database Authentication Error",
                        "Invalid database credentials.\nPlease check CPS configuration.",
                        MessageBoxButtons.OK,
                        MessageType.Error
                    );
                }
                else if (ex.Number == -2) // Timeout
                {
                    MessageDialogBox.ShowDialog(
                        "Database Timeout",
                        "The database took too long to respond.\nPlease try again.",
                        MessageBoxButtons.OK,
                        MessageType.Warning
                    );
                }
                else
                {
                    MessageDialogBox.ShowDialog(
                        "Database Error",
                        $"Cannot query CPS database.\n\n{ex.Message}",
                        MessageBoxButtons.OK,
                        MessageType.Error
                    );
                }
            }
            catch (InvalidOperationException ex)
            {
                // Covers connection state issues
                Utils.WriteUnexpectedError($"DB operation error | Barcode={barcodeValue}");
                Utils.WriteExceptionError(ex);

                MessageDialogBox.ShowDialog(
                    "Database Error",
                    ex.Message,
                    MessageBoxButtons.OK,
                    MessageType.Error
                );
            }
            catch (Exception ex)
            {
                // Real scanning / unexpected errors
                Utils.WriteUnexpectedError($"Scan failed | Barcode={barcodeValue}");
                Utils.WriteExceptionError(ex);

                MessageDialogBox.ShowDialog(
                    "Scan Error",
                    $"Error scanning barcode.\n\n{ex.Message}",
                    MessageBoxButtons.OK,
                    MessageType.Error
                );
            }
            finally
            {
                tbWoBarcode.Clear();
                tbWoBarcode.Focus();
                _scanInProgress = false;
            }
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

        private void PrepareCpsQuery()
        {
            if (string.IsNullOrWhiteSpace(PBCMain.DbCpsConfig?.CpsQuery))
                throw new Exception("CPS query is not configured.");

            _preparedCpsQuery = Utils.AddFilterClause(
                PBCMain.DbCpsConfig.CpsQuery,
                "PWO.ID = @woID AND PWO.NSID = @woNSID"
            );
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
               
                MessageDialogBox.ShowDialog("", "No scans.", MessageBoxButtons.OK, MessageType.Info);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
     
    }
}
