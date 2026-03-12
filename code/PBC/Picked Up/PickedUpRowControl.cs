using System;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Packed_And_Ready.View_Button;

namespace WindowsFormsApp1.Picked_Up
{
    public partial class PickedUpRowControl : UserControl
    {
        private PbJobModel _model;

        public PbJobModel BoundJob { get; private set; }

        public event EventHandler ViewDialogClosed;

        public PickedUpRowControl()
        {
            InitializeComponent();
        }

        public void Bind(PbJobModel model)
        {
            _model = model;
            BoundJob = model;

            lblPBNameCode.Text = model.JobNumber + " " + model.JobName;
            lblQty.Text = model.TotalEnvelopeOfJob.ToString();
            lblTrays.Text = model.TotalTraysOfJob.ToString();
            lblPallets.Text = model.Pallets.Count.ToString();

            lblShipTime.Text = model.ShippedDate.HasValue
                ? model.ShippedDate.Value.ToString("MM/dd/yyyy hh:mm tt")
                : string.Empty;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (_model == null)
                return;

            // 🔥 IMPORTANT: Always deep clone before opening dialog
            var clonedJob = ModelCloner.CloneJob(_model);

            Form parentForm = this.FindForm();

            using (var dlg = new ViewButtonDialog(
                clonedJob,
                hideRemove: true,
                hidePrint: true,
                hideClose: false))
            {
                dlg.ShowDialog(parentForm);

                if (dlg.DataChanged)
                {
                    ViewDialogClosed?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool IsChecked
        {
            get => cbItem.Checked;
            set => cbItem.Checked = value;
        }


    }
}