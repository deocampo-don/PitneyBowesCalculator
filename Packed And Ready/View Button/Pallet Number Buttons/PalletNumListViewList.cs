
using System;
using System.Windows.Forms;

namespace WindowsFormsApp1.Packed_And_Ready.View_Button
{
    public partial class PalletNumListViewList : UserControl
    {
        /* -------------------------------------------------------------
         * FIELDS
         * ------------------------------------------------------------- */

        /// <summary>
        /// Tracks currently loaded job (optional).
        /// </summary>
        private PbJobModel _item;


        /* -------------------------------------------------------------
         * CONSTRUCTOR
         * ------------------------------------------------------------- */

        public PalletNumListViewList()
        {
            InitializeComponent();
        }

        private int _selectedIndex = -1;
        /* -------------------------------------------------------------
         * PUBLIC METHODS
         * ------------------------------------------------------------- */

        /// <summary>
        /// Clears existing rows and adds one button/row for each pallet.
        /// </summary>
        public void SetItems(PbJobModel job)
        {
            if (job == null || job.Pallets == null)
                return;

            _item = job; // keep reference if needed later

            rowFlow.SuspendLayout();
            rowFlow.Controls.Clear();

            for (int i = 0; i < job.Pallets.Count; i++)
            {
                var row = new PalletNumListRowControl();
                row.Bind(job, i);

                // Forward pallet click event from row → up to the dialog
                row.PalletClicked += Row_PalletClicked;

                // Optional: spacing
                row.Margin = new Padding(0, 0, 10, 0);

                rowFlow.Controls.Add(row);
            }

            rowFlow.ResumeLayout(true);

            // Debug (optional)
            // MessageBox.Show("Rows added: " + rowFlow.Controls.Count);
        }


        /// <summary>
        /// Adds a single pallet row programmatically.
        /// </summary>
        public void AddRow(PalletNumListRowControl row)
        {
            if (row == null)
                return;

            row.Margin = new Padding(0, 0, 10, 6);
            rowFlow.Controls.Add(row);
        }


        /* -------------------------------------------------------------
         * EVENT BUBBLING → SEND PALLET CLICK UPWARD
         * ------------------------------------------------------------- */
        public event Action<int> PalletClicked;

        private void Row_PalletClicked(int palletIndex)
        {
            _selectedIndex = palletIndex;
            HighlightSelectedButton();
            PalletClicked?.Invoke(palletIndex);
        }

        private void HighlightSelectedButton()
        {
            for (int i = 0; i < rowFlow.Controls.Count; i++)
            {
                if (rowFlow.Controls[i] is PalletNumListRowControl row)
                {
                    if (i == _selectedIndex)
                        row.SetSelected(true);   // selected
                    else
                        row.SetSelected(false);  // not selected
                }
            }
        }
    }
}

