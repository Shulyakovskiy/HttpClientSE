using System.Drawing.Drawing2D;
using JetBrains.Annotations;

namespace Infrastructure.ObjectExtension.FormsExtension
{
    [UsedImplicitly]
    public static class FormRectangle
    {
        [UsedImplicitly]
        public static GraphicsPath CreateRoundedRectangle(float x, float y, float width,
           float height, float d)
        {
            var path = new GraphicsPath();
            float r = d / 2f;
            path.AddLine(x + r, y, x + width - r, y);
            path.AddArc(x + width - d, y, d, d, 270, 90);
            path.AddLine(x + width, y + r, x + width, y + height - r);
            path.AddArc(x + width - d, y + height - d, d, d, 0, 90);
            path.AddLine(x + width - r, y + height, x + r, y + height);
            path.AddArc(x, y + height - d, d, d, 90, 90);
            path.AddLine(x, y + height - r, x, y + r);
            path.AddArc(x, y, d, d, 180, 90);

            return path;
        }
    }
}
/* Use Extension
 
      public Form1()
        {
            InitializeComponent();
            var path = CreateRoundedRectangle(0, 0, 365, 329, 50);
            this.Region = new Region(path);
        }
 * ---------------------*
 0, 0     - Position Form Actual
 365, 329 - Widht, Height
 50       - Rectangle 
 */