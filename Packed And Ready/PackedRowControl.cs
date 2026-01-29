
using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Packed_And_Ready.View_Button;

namespace WindowsFormsApp1.Packed_And_Ready
{
    public partial class PackedRowControl : UserControl
    {
        private PackedReady _model;

        // Expose an event so parent can react to View button
        public event EventHandler ViewClicked;

        public PackedRowControl()
        {
            InitializeComponent();

            CSSDesign.MakeRounded(btnView, 10);
            CSSDesign.MakePanelRounded(pnlDashboard, 12, Color.Gray, 2);

            // sensible defaults
            JobName = "Job Name";
            JobNumber = 0;
            EnvelopeQty = 0;
            Trays = 0;
            Pallets = 0;
            PackDate = DateTime.Today;
            IsReady= false;
            
        }

        // Optional bind from a model if you have one
        public void Bind(PackedReady model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));

            JobName = model.JobName;       // ensure your model has these properties
            JobNumber = model.JobNumber;
            EnvelopeQty = model.EnvelopeQty;
            Trays = model.Trays;
            Pallets = model.Pallets;
            PackDate = model.ShipDateTime;
            IsReady = model.IsReady;
        }

        // ==== Public properties that keep the UI in sync ====

        private string _jobName;
        public string JobName
        {
            get => _jobName;
            set
            {
                _jobName = value ?? string.Empty;
                // Heading shows the job name
                txtPBJobName.Text = _jobName;
            }
        }

        private int _jobNumber;
        public int JobNumber
        {
            get => _jobNumber;
            set
            {
                _jobNumber = value;
                // If you want zero‑padding: $"{_jobNumber:D4}"
                txtPBJobNum.Text = _jobNumber.ToString();
            }
        }

        private int _envelopeQty;
        public int EnvelopeQty
        {
            get => _envelopeQty;
            set
            {
                _envelopeQty = value;
                txtEnvelopeQty.Text = _envelopeQty.ToString();
            }
        }

        private int _trays;
        public int Trays
        {
            get => _trays;
            set
            {
                _trays = value;
                txtTrays.Text = _trays.ToString();
            }
        }

        private int _pallets;
        public int Pallets
        {
            get => _pallets;
            set
            {
                _pallets = value;
                txtPallets.Text = _pallets.ToString();
            }
        }

        private DateTime _packDate = DateTime.Today;
        public DateTime PackDate
        {
            get => _packDate;
            set
            {
                _packDate = value;
                txtPackDate.Text = _packDate.ToString("MM/dd/yyyy");
            }
        }

        private bool _isReady;
        public bool IsReady
        {
            get => _isReady;
            set
            {
                _isReady = value;
                chkbxStatus.Checked = _isReady;
                txtStatus.Text = _isReady ? "Ready to Ship" : "Not Ready";
                txtStatus.StateCommon.ShortText.Color1 =
                    ColorTranslator.FromHtml(_isReady ? "#34C759" : "#FF383C");
            }
        }

        // === UI events ===

        private void btnView_Click(object sender, EventArgs e)
        {
            // Raise an event in case parent wants to intercept
            ViewClicked?.Invoke(this, EventArgs.Empty);

            // Keep your dialog behavior
            Form parentForm = this.FindForm();
            using (ViewButtonDialog dlg = new ViewButtonDialog())
            {
                dlg.ShowDialog(parentForm);
            }
        }

        private void chkbxStatus_CheckedChanged(object sender, EventArgs e)
        {
            // Keep property as the source of truth
            IsReady = chkbxStatus.Checked;
        }
    }
}
