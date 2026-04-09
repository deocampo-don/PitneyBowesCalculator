using System;
using System.ComponentModel;
using System.Windows.Forms;
using PitneyBowesCalculator.Models;

namespace PitneyBowesCalculator
{
    public partial class WorkOrderRowControl : UserControl
    {
        private WorkOrder _model;

        public WorkOrderRowControl()
        {
            InitializeComponent();
        }

        // Expose selected state (UI only)
        public bool IsSelected => cbWO.Checked;

        // Expose the bound WorkOrder
        public WorkOrder BoundItem => _model;

        public void Bind(WorkOrder model)
        {
            _model = model;

            lblWOname.Text = model.WorkOrderCode;
            lblWOqty.Text = model.Quantity.ToString("N0");

            // Reset checkbox every time dialog loads
            cbWO.Checked = false;
        }
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ShowDivider
        {
            set => pnlDivider.Visible = value;
        }

        public void ClearSelection()
        {
            cbWO.Checked = false;
        }

    }
}
