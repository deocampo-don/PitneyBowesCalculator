using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public static class FormHelper
{
    public static void ApplyRoundedCorners(Form form, int radius)
    {
        form.FormBorderStyle = FormBorderStyle.None;

        GraphicsPath path = new GraphicsPath();
        path.StartFigure();
        path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
        path.AddArc(new Rectangle(form.Width - radius, 0, radius, radius), 270, 90);
        path.AddArc(new Rectangle(form.Width - radius, form.Height - radius, radius, radius), 0, 90);
        path.AddArc(new Rectangle(0, form.Height - radius, radius, radius), 90, 90);
        path.CloseFigure();

        form.Region = new Region(path);
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