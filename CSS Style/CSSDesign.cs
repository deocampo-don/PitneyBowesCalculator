using Krypton.Toolkit;
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

    public static void ApplyTabColors(KryptonCheckButton b)
    {
        Color purple = Color.FromArgb(110, 74, 191);
        var tabFont = new Font("Segoe UI Semibold", 17f, FontStyle.Regular);
        var pad = new Padding(0, 6, 0, 6);

        b.StateCommon.Content.Padding = pad;
        b.StateCommon.Content.ShortText.Font = tabFont;
        b.AutoSize = true;

        // CHECKED states: background/border visible
        b.StateCheckedNormal.Back.Draw = InheritBool.True;
        b.StateCheckedNormal.Back.Color1 = purple;
        b.StateCheckedNormal.Back.Color2 = purple;
        b.StateCheckedNormal.Border.Draw = InheritBool.True;
        b.StateCheckedNormal.Border.DrawBorders = PaletteDrawBorders.All;
        b.StateCheckedNormal.Border.Color1 = purple;
        b.StateCheckedNormal.Border.Color2 = purple;
        b.StateCheckedNormal.Border.Rounding = 7;
        b.StateCheckedNormal.Content.ShortText.Color1 = Color.White;

        // Checked hover (keep purple)
        b.StateCheckedTracking.Back.Draw = InheritBool.True;
        b.StateCheckedTracking.Back.Color1 = purple;
        b.StateCheckedTracking.Back.Color2 = purple;
        b.StateCheckedTracking.Border.Draw = InheritBool.True;
        b.StateCheckedTracking.Border.DrawBorders = PaletteDrawBorders.All;
        b.StateCheckedTracking.Border.Color1 = purple;
        b.StateCheckedTracking.Border.Color2 = purple;
        b.StateCheckedTracking.Border.Rounding = 7;
        b.StateCheckedTracking.Content.ShortText.Color1 = Color.White;

        // Pressed on checked
        b.StateCheckedPressed.Back.Draw = InheritBool.True;
        b.StateCheckedPressed.Back.Color1 = purple;
        b.StateCheckedPressed.Back.Color2 = purple;
        b.StateCheckedPressed.Border.Draw = InheritBool.True;
        b.StateCheckedPressed.Border.DrawBorders = PaletteDrawBorders.All;
        b.StateCheckedPressed.Border.Color1 = purple;
        b.StateCheckedPressed.Border.Color2 = purple;
        b.StateCheckedPressed.Border.Rounding = 7;
        b.StateCheckedPressed.Content.ShortText.Color1 = Color.White;

        // Normal state
        b.StateNormal.Back.Draw = InheritBool.False;
        b.StateNormal.Border.Draw = InheritBool.False;
        b.StateNormal.Content.ShortText.Color1 = Color.Black;

        // Hover state
        b.StateTracking.Back.Draw = InheritBool.False;
        b.StateTracking.Border.Draw = InheritBool.False;
        b.StateTracking.Content.ShortText.Color1 = Color.Black;

        // Pressed state (when not checked)
        b.StatePressed.Back.Draw = InheritBool.False;
        b.StatePressed.Border.Draw = InheritBool.False;
        b.StatePressed.Content.ShortText.Color1 = Color.Black;
    }

    public static void MakeTitleBarButton(KryptonButton b)
    {
        // Disable background in pressed/disabled states
        b.StatePressed.Back.Draw = InheritBool.False;
        b.StateDisabled.Back.Draw = InheritBool.False;

        // Disable borders in pressed/disabled states
        b.StatePressed.Border.Draw = InheritBool.False;
        b.StateDisabled.Border.Draw = InheritBool.False;

        // Tight padding for icons
        b.StateCommon.Content.Padding = Padding.Empty;

        // No focus rectangle
        b.TabStop = false;
    }
    //==========================================================================================================================================

}



