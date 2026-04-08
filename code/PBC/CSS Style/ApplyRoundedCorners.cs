using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public static class FormHelper
{
    private const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;
    private const int DWMWCP_ROUND = 2;

    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(
        IntPtr hwnd,
        int attr,
        ref int attrValue,
        int attrSize
    );

    public static void ApplyRoundedCorners(Form form)
    {
        int preference = DWMWCP_ROUND;

        DwmSetWindowAttribute(
            form.Handle,
            DWMWA_WINDOW_CORNER_PREFERENCE,
            ref preference,
            sizeof(int)
        );
    }
}

public class RoundedPanel : Panel
{
    [Category("Appearance")]
    [DefaultValue(20)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int BorderRadius { get; set; } = 20;

    [Category("Appearance")]
    [DefaultValue(2)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int BorderSize { get; set; } = 2;

    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color BorderColor { get; set; } = Color.RoyalBlue;

    public RoundedPanel()
    {
        DoubleBuffered = true;
        ResizeRedraw = true;
      
        //BackColor = Color.White;



        SetStyle(ControlStyles.AllPaintingInWmPaint |
         ControlStyles.UserPaint |
         ControlStyles.OptimizedDoubleBuffer |
         ControlStyles.ResizeRedraw, true);
    }

    protected override void OnResize(EventArgs eventargs)
    {
        base.OnResize(eventargs);

        Rectangle rect = new Rectangle(
            BorderSize,
            BorderSize,
            Width - BorderSize * 2 - 1,
            Height - BorderSize * 2 - 1);

        using (GraphicsPath path = GetRoundedPath(rect, BorderRadius))
        {
            Region = new Region(path);
        }

        Invalidate();
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        base.OnPaintBackground(e);

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        Rectangle rect = new Rectangle(
            BorderSize,
            BorderSize,
            Width - BorderSize * 2 - 1,
            Height - BorderSize * 2 - 1);

        using (GraphicsPath path = GetRoundedPath(rect, BorderRadius))
        using (Pen pen = new Pen(BorderColor, BorderSize))
        {
            e.Graphics.DrawPath(pen, path);
        }
    }

    private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
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
}

public static class ShadowHelper
{
    private const int DWMWA_NCRENDERING_POLICY = 2;
    private const int DWMNCRP_ENABLED = 2;

    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(
        IntPtr hwnd,
        int attr,
        ref int attrValue,
        int attrSize
    );

    [DllImport("dwmapi.dll")]
    private static extern int DwmExtendFrameIntoClientArea(
        IntPtr hwnd,
        ref MARGINS margins
    );

    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    public static void ApplyShadow(Form form)
    {
        if (!Environment.OSVersion.Version.ToString().StartsWith("6") &&
            !Environment.OSVersion.Version.ToString().StartsWith("10"))
            return;

        int val = DWMNCRP_ENABLED;

        DwmSetWindowAttribute(
            form.Handle,
            DWMWA_NCRENDERING_POLICY,
            ref val,
            Marshal.SizeOf(val)
        );

        MARGINS margins = new MARGINS()
        {
            cxLeftWidth = 1,
            cxRightWidth = 1,
            cyTopHeight = 1,
            cyBottomHeight = 1
        };

        DwmExtendFrameIntoClientArea(form.Handle, ref margins);
    }
}