using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Packed_And_Ready.View_Button;

namespace WindowsFormsApp1.Packed_And_Ready
{
    public partial class PackedRowControl : UserControl
    {
        public PackedRowControl()
        {
            InitializeComponent();

            MakeRounded(btnView, 10); // 20 = roundness

        //    AddPanelBorder(pnlDashboard, Color.Silver, 1);
            MakePanelRounded(pnlDashboard, 12, Color.Gray, 2);
        }


        private void MakeRounded(Button btn, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(btn.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, btn.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            btn.Region = new Region(path);
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
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }
        private void MakePanelRounded(Panel panel, int radius, Color borderColor, int borderWidth)
        {
            panel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                Rectangle rect = panel.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;

                using (GraphicsPath path = GetRoundedRectPath(rect, radius))
                {
                    // Clip panel to rounded shape
                    panel.Region = new Region(path);

                    // Draw border
                    using (Pen pen = new Pen(borderColor, borderWidth))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };

            panel.Resize += (s, e) => panel.Invalidate();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            Form parentForm = this.FindForm(); // get the dashboard form

            using (ViewButtonDialog dlg = new ViewButtonDialog())
            {
                dlg.ShowDialog(parentForm); // locks the dashboard correctly
            }
        }

        private void chkbxStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxStatus.Checked)
            {
                txtStatus.Text = "Ready to Ship";
                txtStatus.StateCommon.ShortText.Color1 =
                            ColorTranslator.FromHtml("#34C759"); // green

            }
            else
            {
                txtStatus.Text = "Not Ready";
                txtStatus.StateCommon.ShortText.Color1 =
                            ColorTranslator.FromHtml("#FF383C"); // red
            }
        }
    }
}
