using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PitneyBowesCalculator.Packed_And_Ready.View_Button
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
        public event Action<int, bool> SelectionChanged; // 🔑 added


        /* -------------------------------------------------------------
         * CONSTRUCTOR
         * ------------------------------------------------------------- */
        public PalletNumListRowControl()
        {
            InitializeComponent();

            // Add border to the row item for visual separation
            CSSDesign.AddPanelBorder(pnlMain, Color.Silver, 1);


            // Wire checkbox change
            chkBox.CheckedChanged += chkBox_CheckedChanged;

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

            
            bool isShipped = model.ShippedDate.HasValue;
            chkBox.Visible = !isShipped;

            if (isShipped)
            {
                chkBox.Checked = false;   // safety
                chkBox.Enabled = false;
            }
        }


        /* -------------------------------------------------------------
         * UI EVENT HANDLERS
         * ------------------------------------------------------------- */
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool IsSelected
        {
            get => chkBox.Checked;
            set => chkBox.Checked = value;
        }

        public int PalletIndex => _palletIndex;

        private void btnPalletNum_Click(object sender, System.EventArgs e)
        {
            PalletClicked?.Invoke(_palletIndex); 
        }


        public void SetSelected(bool isSelected)
        {

            if (isSelected)
            {
                btnPalletNum.BackColor = Color.FromArgb(103, 80, 164); // Violet
                btnPalletNum.ForeColor = Color.White;
                chkBox.BackColor = Color.FromArgb(103, 80, 164);
            }
            else
            {
                btnPalletNum.BackColor = Color.White;
                btnPalletNum.ForeColor = Color.Black;
                chkBox.BackColor = Color.White;
            }

        }

        private void chkBox_CheckedChanged(object sender, EventArgs e)
        {
            SetSelected(chkBox.Checked); // apply violet style
            SelectionChanged?.Invoke(_palletIndex, chkBox.Checked);
        }

    }
}
