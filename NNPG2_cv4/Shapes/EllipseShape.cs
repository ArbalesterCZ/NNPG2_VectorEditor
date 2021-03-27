using System.Drawing;
using System.Drawing.Drawing2D;

namespace NNPG2_cv4
{
    public class EllipseShape : IShape
    {
        public Brush Fill { get; set; }
        public BrushType Mode { get; set; }
        public float FillAngle { get; set; }
        public Color Primary { get; set; }
        public Color Secondary { get; set; }
        public Pen Edge { get; }
        public Color EdgeColor { get { return Edge.Color; } set { Edge.Color = value; } }
        public float EdgeWidth { get { return Edge.Width; } set { Edge.Width = value; } }
        public Size Size { get 
            {
                float addend = 0;
                if (EdgeEnabled) addend = EdgeWidth;
                return new Size((int)(rect.Width + addend), (int)(rect.Height + addend));
            } }
        public Image Texture { set { texture = value; } }
        public bool EdgeEnabled { get; set; }
        public HatchStyle Hatch { get; set; }

        private Rectangle rect;
        private Image texture = Library.DEFAULT_TEXTURE;

        public EllipseShape(Rectangle rect)
        {
            this.rect = rect;
            Primary = Color.White;
            Secondary = Color.Black;
            Edge = new Pen(Color.Red);
            Mode = BrushType.Solid;
            EdgeWidth = 4;
            EdgeEnabled = true;
        }

        public EllipseShape(Rectangle rect, Color primary, Color secondary, Color edge, float edgeWitdh, bool edgeEnable, BrushType mode)
        {
            this.rect = rect;
            Primary = primary;
            Edge = new Pen(edge, edgeWitdh);
            Secondary = secondary;
            Mode = mode;
            EdgeEnabled = edgeEnable;
        }

        override public string ToString()
        {
            return string.Format("Ellipse {0}x{1}", Size.Width, Size.Height);
        }

        public bool Contains(Point p)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(rect);
            return path.IsVisible(p);
        }

        public Point[] ControlPoints()
        {
            return new Point[2] { rect.Location, new Point(rect.Right, rect.Bottom) };
        }

        public void TransformMove(Size addend)
        {
            rect.Location += addend;
        }

        public void TransformScale(Size addend, int index)
        {
            switch (index)
            {
                case 0:
                    if (rect.Width - addend.Width > 1)
                    {
                        rect.X += addend.Width;
                        rect.Width -= addend.Width;
                    }
                    if (rect.Height - addend.Height > 1)
                    {
                        rect.Y += addend.Height;
                        rect.Height -= addend.Height;
                    }
                    break;
                case 1:
                    if (rect.Width + addend.Width > 1) rect.Width += addend.Width;
                    if (rect.Height + addend.Height > 1) rect.Height += addend.Height;
                    break;
            }
        }

        public void Render(Graphics g)
        {
            g.RenderingOrigin = rect.Location;
            g.FillEllipse(Brush(), rect);
            if (EdgeEnabled) g.DrawEllipse(Edge, rect);
        }

        public void Export(string filepath)
        {
            int addend = 0;
            if (EdgeEnabled) addend = (int)(EdgeWidth / 2);

            Bitmap bmp = new Bitmap(Size.Width, Size.Height);
            Rectangle isolated = new Rectangle(addend, addend, rect.Width, rect.Height);

            Graphics g = Graphics.FromImage(bmp);
            g.RenderingOrigin = rect.Location;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillEllipse(IsolationBrush(addend), isolated);
            if (EdgeEnabled) g.DrawEllipse(Edge, isolated);
            Library.SaveImage(bmp, filepath);
        }

        public IShape DeepCopy()
        {
            return new EllipseShape(rect, Primary, Secondary, EdgeColor, EdgeWidth, EdgeEnabled, Mode);
        }

        private Brush Brush()
        {
            switch (Mode)
            {
                case BrushType.Solid:
                    return new SolidBrush(Primary);
                case BrushType.Hatch:
                    return new HatchBrush(Hatch, Primary, Secondary);
                case BrushType.Gradient:
                    return new LinearGradientBrush(rect, Primary, Secondary, FillAngle);
                case BrushType.Texture:
                    TextureBrush tb = new TextureBrush(texture, WrapMode.Tile);
                    tb.TranslateTransform(rect.X, rect.Y);
                    return tb;
                default:
                    return null;
            }
        }
        private Brush IsolationBrush(int addend)
        {
            switch (Mode)
            {
                case BrushType.Solid:
                    return new SolidBrush(Primary);
                case BrushType.Hatch:
                    return new HatchBrush(Hatch, Primary, Secondary);
                case BrushType.Gradient:
                    return new LinearGradientBrush(new Rectangle(0, 0, rect.Width, rect.Height), Primary, Secondary, FillAngle);
                case BrushType.Texture:
                    TextureBrush tb = new TextureBrush(texture, WrapMode.Tile);
                    tb.TranslateTransform(addend, addend);
                    return tb;
                default:
                    return null;
            }
        }
    }
}
