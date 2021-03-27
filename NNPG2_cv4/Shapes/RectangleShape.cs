using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace NNPG2_cv4
{
    public class RectangleShape : IShape
    {
        public Brush Fill { get; set; }
        public BrushType Mode { get; set; }
        public Color Primary { get; set; }
        public Color Secondary { get; set; }
        public Pen Edge { get; }
        public Color EdgeColor { get { return Edge.Color; } set { Edge.Color = value; } }
        public float EdgeWidth { get { return Edge.Width; } set { Edge.Width = value; } }
        public Size Size { get { return rect.Size; } }
        public Image Texture { set { texture = value; } }

        private Rectangle rect;

        private Image texture = Image.FromFile(Directory.GetCurrentDirectory() + @"\..\..\rsc\btntreant-result.bmp");

        public RectangleShape(Rectangle rect)
        {
            this.rect = rect;
            Primary = Color.White;
            Secondary = Color.Black;
            Edge = new Pen(Color.White);
            Mode = BrushType.Solid;
        }

        public RectangleShape(Rectangle rect, Color primary, Color secondary, Color edge, BrushType mode)
        {
            this.rect = rect;
            Primary = primary;
            Edge = new Pen(edge);
            Secondary = secondary;
            Mode = mode;
        }

        override public string ToString()
        {
            return string.Format("{0}\nPrimary: {1}\nSecondary: {2}\n{3} Width: [{4}px]\nFill: {5}", rect, Primary, Secondary, EdgeColor, EdgeWidth, Mode);
        }

        public bool Contains(Point p)
        {
            return rect.Contains(p);
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
            g.FillRectangle(Brush(), rect);
            g.DrawRectangle(Edge, rect);
        }

        public void Export(string filepath)
        {
            int addend = (int) (EdgeWidth / 2);

            Bitmap bmp = new Bitmap((int)(Size.Width + EdgeWidth), (int)(Size.Height + EdgeWidth));
            Rectangle isolated = new Rectangle(addend, addend, Size.Width, Size.Height);

            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillRectangle(IsolationBrush(), isolated);
            g.DrawRectangle(Edge, isolated);
            bmp.Save(filepath);

        }

        public IShape DeepCopy()
        {
            return new RectangleShape(rect, Primary, Secondary, EdgeColor, Mode);
        }

        private Brush Brush()
        {
            switch (Mode)
            {
                case BrushType.Solid:
                    return new SolidBrush(Primary);
                case BrushType.Hatch:
                    return new HatchBrush(HatchStyle.Weave, Secondary, Primary);
                case BrushType.Gradient:
                    return new LinearGradientBrush(rect.Location, rect.Location + new Size(0, 50), Primary, Secondary);
                case BrushType.Texture:
                    TextureBrush tb = new TextureBrush(texture, WrapMode.Tile);
                    tb.TranslateTransform(rect.X, rect.Y);
                    return tb;
                default:
                    return null;
            }
        }
        private Brush IsolationBrush()
        {
            switch (Mode)
            {
                case BrushType.Solid:
                    return new SolidBrush(Primary);
                case BrushType.Hatch:
                    return new HatchBrush(HatchStyle.Weave, Secondary, Primary);
                case BrushType.Gradient:
                    return new LinearGradientBrush(new Point(), new Point(50, 50), Primary, Secondary);
                case BrushType.Texture:
                    TextureBrush tb = new TextureBrush(texture, WrapMode.Tile);
                    tb.TranslateTransform(0, 0);
                    return tb;
                default:
                    return null;
            }
        }
    }
}
