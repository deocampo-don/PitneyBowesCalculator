using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public static class FormHelper
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
}
