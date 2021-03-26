using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace NNPG2_cv4
{
    public class EllipseShape : IShape
    {
        public Brush Fill { get; set; }
        public BrushType Mode { get; set; }
        public Color Primary { get; set; }
        public Color Secondary { get; set; }
        public Pen Edge { get; }
        public Color EdgeColor { get { return Edge.Color; } set { Edge.Color = value; } }
        public float EdgeWidth { get { return Edge.Width; } set { Edge.Width = value; } }
        public Size Size { get { return rect.Size; } }

        public Rectangle rect;

        private readonly Image texture = Image.FromFile(Directory.GetCurrentDirectory() + @"\..\..\rsc\btntreant-result.bmp");

        public EllipseShape(Rectangle rect)
        {
            this.rect = rect;
            Primary = Color.White;
            Secondary = Color.Gray;
            Edge = new Pen(Color.White);
            Mode = BrushType.Solid;
        }

        public EllipseShape(Rectangle rect, Color primary, Color secondary, Color edge, BrushType mode)
        {
            this.rect = rect;
            Primary = primary;
            Edge = new Pen(edge);
            Secondary = secondary;
            Mode = mode;
        }

        override public string ToString()
        {
            return string.Format("{0}\nPrimary: {1}\nSecondary: {2}\nEdge: {3}\nFill: {4}", rect, Primary, Secondary, Edge, Mode);
        }

        public Brush Brush()
        {
            switch (Mode)
            {
                case BrushType.Solid:
                    return new SolidBrush(Primary);
                case BrushType.Hatch:
                    return new HatchBrush(HatchStyle.Weave, Secondary, Primary);
                case BrushType.Gradient:
                    return new LinearGradientBrush(rect.Location, rect.Location + new Size(50, 50), Primary, Secondary);
                case BrushType.Texture:
                    TextureBrush tb = new TextureBrush(texture, WrapMode.Tile);
                    tb.TranslateTransform(rect.X, rect.Y);
                    return tb;
                default:
                    return null;
            }
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
            g.FillEllipse(Brush(), rect);
            g.DrawEllipse(Edge, rect);
        }

        public void RenderIsolation(Graphics g)
        {
            Rectangle isolated = new Rectangle(0, 0, rect.Width, rect.Height);
            g.FillEllipse(Brush(), isolated);
            g.DrawEllipse(Edge, isolated);
        }

        public IShape DeepCopy()
        {
            return new EllipseShape(rect, Primary, Secondary, EdgeColor, Mode);
        }
    }
}
