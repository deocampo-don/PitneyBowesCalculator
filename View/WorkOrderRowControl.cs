using System;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
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

            lblWOname.Text = model.WoCode;
            lblWOqty.Text = model.EnvelopeQty.ToString("N0");

            // Reset checkbox every time dialog loads
            cbWO.Checked = false;
        }

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
