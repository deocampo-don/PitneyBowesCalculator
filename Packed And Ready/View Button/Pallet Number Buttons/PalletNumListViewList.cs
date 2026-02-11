using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1.Packed_And_Ready.View_Button
{
    public partial class PalletNumListViewList : UserControl
    {
        /* -------------------------------------------------------------
         * FIELDS
         * ------------------------------------------------------------- */

        /// <summary>
        /// The PB job model containing pallet data.
        /// </summary>
        private PbJobModel _job;

        /// <summary>
        /// Cached row controls for selection and refresh handling.
        /// </summary>
        private readonly List<PalletNumListRowControl> _rows =
            new List<PalletNumListRowControl>();


        /* -------------------------------------------------------------
         * EVENTS
         * ------------------------------------------------------------- */

        /// <summary>
        /// Raised when a pallet row button is clicked.
        /// </summary>
        public event Action<int> PalletClicked;


        /* -------------------------------------------------------------
         * CONSTRUCTOR
         * ------------------------------------------------------------- */

        public PalletNumListViewList()
        {
            InitializeComponent();
        }



        /* -------------------------------------------------------------
         * DATA BINDING
         * ------------------------------------------------------------- */

        /// <summary>
        /// Initial binding of pallets for a PB job.
        /// </summary>
        public void SetItems(PbJobModel job)
        {
            _job = job;
            BuildRows();
        }

        /// <summary>
        /// Rebuilds pallet rows after add/remove operations.
        /// </summary>
        public void RefreshItems(PbJobModel job)
        {
            _job = job;
            BuildRows();
        }


        /* -------------------------------------------------------------
         * ROW CREATION
         * ------------------------------------------------------------- */

        private void BuildRows()
        {
            SuspendLayout();

            // Clear existing UI
            foreach (var row in _rows)
                row.Dispose();

            _rows.Clear();
            rowFlow.Controls.Clear();

            if (_job?.Pallets != null)
            {
                for (int i = 0; i < _job.Pallets.Count; i++)
                {
                    var row = new PalletNumListRowControl();
                    row.Bind(_job, i);

                    // Relay pallet click to parent dialog
                    row.PalletClicked += idx =>
                    {
                        HighlightRow(idx);
                        PalletClicked?.Invoke(idx);
                    };

                    _rows.Add(row);
                    rowFlow.Controls.Add(row);
                }
            }

            ResumeLayout();
        }


        /* -------------------------------------------------------------
         * SELECTION / CHECKBOX HANDLING
         * ------------------------------------------------------------- */

        /// <summary>
        /// Returns indices of all checked pallets.
        /// Used by Remove Pallet/s logic.
        /// </summary>
        public List<int> GetSelectedIndices()
        {
            return _rows
                .Where(r => r.IsSelected)
                .Select(r => r.PalletIndex)
                .OrderBy(i => i)
                .ToList();
        }

        
        public void ClearSelection()
        {
            foreach (var row in _rows)
                row.IsSelected = false;
        }


        /* -------------------------------------------------------------
         * VISUAL SELECTION (CLICKED PALLET)
         * ------------------------------------------------------------- */
        //select first pallet as default
        public void SelectFirstPallet()
        {
            if (_rows.Count == 0)
                return;

            HighlightRow(0);
            PalletClicked?.Invoke(0); // load details panel
        }

        public void SelectPallet(int index)
        {
            if (index < 0 || index >= _rows.Count) return;
            HighlightRow(index);
            PalletClicked?.Invoke(index);
        }

        public void SelectLastPallet()
        {
            if (_rows.Count == 0) return;
            SelectPallet(_rows.Count - 1);
        }
        private void HighlightRow(int palletIndex)
        {
            foreach (var row in _rows)
                row.SetSelected(row.PalletIndex == palletIndex);
        }
    }
}