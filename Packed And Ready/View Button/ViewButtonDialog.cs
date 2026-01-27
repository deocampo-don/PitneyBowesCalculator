using Krypton.Toolkit;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace WindowsFormsApp1.Packed_And_Ready.View_Button
{
    public partial class ViewButtonDialog : Form
    {
         
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private int _formRadius = 12;
        public ViewButtonDialog()
        {
            InitializeComponent();

           

            pnlHeader.MouseDown += pnlHeader_MouseDown;

            this.Paint += ViewButtonDialog_Paint;
            

            LoadPalletNumList();
            // this.StartPosition = FormStartPosition.CenterScreen;

            MakeRounded(btnRemovePallets, 10);
            MakeRounded(btnPrintPallets, 10);

            AddPanelBorder(pnlDashboard, Color.Silver, 1);
            AddPanelBorder(pnlDetails, Color.Silver, 1);

         
        }


        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture(); // release the mouse
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0); // move the form
            }
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
        private void ViewButtonDialog_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = this.ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;

            using (GraphicsPath path = GetRoundedRectPath(rect, _formRadius))
            {
                // Clip the form itself
                this.Region = new Region(path);

                // Draw border
                using (Pen pen = new Pen(Color.Gray, 2))
                {
                    pen.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }
        private void LoadPalletNumList()
        {
            pnlPalletNoList.Controls.Clear(); // remove anything already there

            PalletNumList palletList = new PalletNumList
            {
                Dock = DockStyle.Fill
            };

            pnlPalletNoList.Controls.Add(palletList);
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

      
        private void btnRemovePallets_Click(object sender, EventArgs e)
        {
            Form parentForm = this.FindForm(); // get the dashboard form

            using (RemovePallets removepallets = new RemovePallets())
            {
                removepallets.ShowDialog(parentForm); // locks the dashboard correctly
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
          
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
