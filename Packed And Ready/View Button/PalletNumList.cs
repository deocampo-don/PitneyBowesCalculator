using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Packed_And_Ready.View_Button
{
    public partial class PalletNumList : UserControl
    {
        public PalletNumList()
        {
            InitializeComponent();

            this.Dock = DockStyle.Fill;
            AddPanelBorder(pnlMain, Color.Silver, 1);
        }


        private void AddPanelBorder(Panel panel, Color borderColor, int borderWidth)
        {
            panel.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(
                    e.Graphics,
                    panel.ClientRectangle,
                    borderColor, borderWidth, ButtonBorderStyle.Solid,
                    borderColor, borderWidth, ButtonBorderStyle.Solid,
                    borderColor, borderWidth, ButtonBorderStyle.Solid,
                    borderColor, borderWidth, ButtonBorderStyle.Solid
                );
            };

            // Force the panel to repaint so the border shows immediately
            panel.Invalidate();
        }
    }
}
