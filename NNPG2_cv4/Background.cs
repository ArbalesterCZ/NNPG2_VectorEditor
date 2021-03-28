using System.Drawing;
using System.Drawing.Drawing2D;

namespace NNPG2_cv4
{
    public class Background
    {
        public Color Color 
        { 
            get { return color; } 
            set { color = value; brush = new SolidBrush(color); } 
        }
        public Image Image
        {
            get { return image; }
            set { image = value; brush = new TextureBrush(image, WrapMode.Tile); }
        }

        private Color color;
        private Image image;
        private Brush brush;

        public Background(Color color)
        {
            this.color = color;
            brush = new SolidBrush(color);
        }

        public void Render(Graphics g, int width, int height)
        {
            g.FillRectangle(brush, new Rectangle(0, 0, width, height));          
        }
    }
}
