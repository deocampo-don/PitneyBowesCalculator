using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


public static class CSSDesign
{
    public static void ApplyRoundedCorners(Form form, int radius)
    {
        // Remove default border so the region takes effect
        form.FormBorderStyle = FormBorderStyle.None;

        // Build a rounded rectangle path
        GraphicsPath path = new GraphicsPath();
        path.StartFigure();
        path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
        path.AddArc(new Rectangle(form.Width - radius, 0, radius, radius), 270, 90);
        path.AddArc(new Rectangle(form.Width - radius, form.Height - radius, radius, radius), 0, 90);
        path.AddArc(new Rectangle(0, form.Height - radius, radius, radius), 90, 90);
        path.CloseFigure();

        // Apply region
        form.Region = new Region(path);
    }
    //==========================================================================================================================================

    public static void AddPanelBorder(Panel panel, Color borderColor, int borderWidth)
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

        // to use this  --CSSDesign.AddPanelBorder(pnlMain, Color.Silver, 1);
    }
    //==========================================================================================================================================
    public static void MakeRounded(Button btn, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        path.StartFigure();
        path.AddArc(0, 0, radius, radius, 180, 90);
        path.AddArc(btn.Width - radius, 0, radius, radius, 270, 90);
        path.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90);
        path.AddArc(0, btn.Height - radius, radius, radius, 90, 90);
        path.CloseFigure();

        btn.Region = new Region(path);

        //to use this  ---CSSDesign.MakeRounded(btnPrintPallets, 10);
    }

    //==========================================================================================================================================
    public static GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
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
    public static void PaintRoundedForm(
        Form form,
        PaintEventArgs e,
        int radius,
        Color borderColor,
        int borderWidth = 2)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        Rectangle rect = form.ClientRectangle;
        rect.Width -= 1;
        rect.Height -= 1;

        using (GraphicsPath path = CSSDesign.GetRoundedRectPath(rect, radius))
        {
            // Apply rounded region to the form
            form.Region = new Region(path);

            // Draw border
            using (Pen pen = new Pen(borderColor, 1.8f))
            {
                pen.Alignment = PenAlignment.Inset;
                e.Graphics.DrawPath(pen, path);
            }
        }

    }

    //==========================================================================================================================================

    public static  GraphicsPath GetRoundedRectPathPanel(Rectangle rect, int radius)
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
    public static void MakePanelRounded(Panel panel, int radius, Color borderColor, int borderWidth)
    {
        panel.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = panel.ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;

            using (GraphicsPath path = GetRoundedRectPathPanel(rect, radius))
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

    //==========================================================================================================================================

}



