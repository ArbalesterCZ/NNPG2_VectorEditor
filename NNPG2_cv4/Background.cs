using System.Drawing;
using System.Drawing.Drawing2D;

namespace NNPG2_cv4
{
    public class Background
    {
        public Color Color 
        { 
            get { return color; } 
            set { color = value; renderImage = false; } 
        }
        public Image Image
        {
            get { return image; }
            set { image = value; renderImage = true; }
        }

        private Color color;
        private Image image;
        private bool renderImage;

        public Background(Color color)
        {
            this.color = color;
        }

        public void Render(Graphics g, int width, int height)
        {
            if (renderImage) g.FillRectangle(new TextureBrush(image, WrapMode.Tile), new Rectangle(0, 0, width, height));
            else g.FillRectangle(new SolidBrush(Color), new Rectangle(0, 0, width, height));             
        }
    }
}
