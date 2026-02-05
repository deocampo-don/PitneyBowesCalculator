using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1.Packed_And_Ready.View_Button
{
    public partial class PalletNumListRowControl : UserControl
    {
        /* -------------------------------------------------------------
         * FIELDS
         * ------------------------------------------------------------- */

        /// <summary>
        /// The PB job model associated with this row.
        /// Stored so the row can display or process pallet info.
        /// </summary>
        private PbJobModel _modelpbjob;
        private int _palletIndex;
        public event Action<int> PalletClicked;

        /* -------------------------------------------------------------
         * CONSTRUCTOR
         * ------------------------------------------------------------- */
        public PalletNumListRowControl()
        {
            InitializeComponent();

            // Add border to the row item for visual separation
            CSSDesign.AddPanelBorder(pnlMain, Color.Silver, 1);
        }


        /* -------------------------------------------------------------
         * DATA BINDING
         * ------------------------------------------------------------- */

        /// <summary>
        /// Binds a PB job model to this row and updates UI elements.
        /// </summary>
        /// 

        public void Bind(PbJobModel model, int palletIndex)
        {
            if (model == null)
                return;
            _palletIndex = palletIndex;

            _modelpbjob = model;

            // Display correct label
            btnPalletNum.Text = $"Pallet # {palletIndex + 1}";


        }


        /* -------------------------------------------------------------
         * UI EVENT HANDLERS
         * ------------------------------------------------------------- */
      
        private void btnPalletNum_Click(object sender, System.EventArgs e)
        {
            // TODO: Raise an event or call a callback to notify parent control
            // Example: Show pallet details, highlight row, etc.

            PalletClicked?.Invoke(_palletIndex);

        }
       

        public void SetSelected(bool isSelected)
        {
            if (isSelected)
            {
                btnPalletNum.BackColor = Color.FromArgb(103, 80, 164); // Violet
                btnPalletNum.ForeColor = Color.White;
            }
            else
            {
                btnPalletNum.BackColor = Color.White;
                btnPalletNum.ForeColor = Color.Black;
            }
        }
    }
}
