using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace NNPG2_cv4
{
    public class RectangleShape : IShape
    {
        public BrushType Mode { get { return mode; } set { mode = value; RenderBrush(); } }
        public HatchStyle Hatch { get { return hatch; } set { hatch = value; RenderBrush(); } }
        public float FillAngle { get { return fillAngle; } set { fillAngle = value; RenderBrush(); } }
        public Color Primary { get { return primary; } set { primary = value; RenderBrush(); } }
        public Color Secondary { get { return secondary; } set { secondary = value; RenderBrush(); } }
        public Pen Edge { get; }
        public Color EdgeColor { get { return Edge.Color; } set { Edge.Color = value; } }
        public float EdgeWidth { get { return Edge.Width; } set { Edge.Width = value; } }
        public bool EdgeEnabled { get; set; }
        public Size Size { get
            {
                float addend = 0;
                if (EdgeEnabled) addend = EdgeWidth;
                return new Size((int)(rect.Width + addend), (int)(rect.Height + addend));
            } }
        public Image Texture { set { texture = value; RenderBrush(); } }

        private HatchStyle hatch;
        private Brush brush;
        private Color primary;
        private Color secondary;
        private float fillAngle;
        private BrushType mode;

        private Rectangle rect;
        private Image texture;

        public RectangleShape(Rectangle rect)
        {
            this.rect = rect;
            primary = Color.LightGray;
            secondary = Color.Black;
            mode = BrushType.Solid;
            Edge = new Pen(Color.Red);
            EdgeWidth = 4;
            EdgeEnabled = true;
            texture = Library.DEFAULT_TEXTURE;

            RenderBrush();
        }

        public RectangleShape(Rectangle rect, Color primary, Color secondary, Color edge, float edgeWitdh, bool edgeEnable, BrushType mode, Image texture)
        {
            this.rect = rect;
            this.primary = primary;
            this.secondary = secondary;
            this.mode = mode;
            Edge = new Pen(edge, edgeWitdh);   
            EdgeEnabled = edgeEnable;
            this.texture = texture;
            
            RenderBrush();
        }

        override public string ToString()
        {
            return string.Format("Rectangle {0}x{1}", Size.Width, Size.Height);
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
            RenderBrush();
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
            RenderBrush();
        }

        public void Render(Graphics g)
        {
            g.RenderingOrigin = rect.Location;
            g.FillRectangle(brush, rect);
            if(EdgeEnabled) g.DrawRectangle(Edge, rect);
        }

        public void Print(Graphics g, Rectangle printArea)
        {
            float multiplyFactor = Math.Min((float)printArea.Width / rect.Width, (float)printArea.Height / rect.Height);

            Rectangle isolatedRect = new Rectangle(printArea.X, printArea.Y, (int)(rect.Width * multiplyFactor), (int)(rect.Height * multiplyFactor));
            g.RenderingOrigin = isolatedRect.Location;
            Brush brusprintBrush = brush;
            if (brusprintBrush is TextureBrush)
            {
                TextureBrush tb = new TextureBrush(texture, WrapMode.Tile);
                tb.TranslateTransform(isolatedRect.X, isolatedRect.Y);
                tb.ScaleTransform(multiplyFactor, multiplyFactor);
                tb.RotateTransform(FillAngle);
                brusprintBrush = tb;          
            }
            g.FillRectangle(brusprintBrush, isolatedRect);
            if (EdgeEnabled) g.DrawRectangle(Edge, isolatedRect);
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
            g.FillRectangle(IsolationBrush(addend), isolated);
            if (EdgeEnabled) g.DrawRectangle(Edge, isolated);
            Library.SaveImage(bmp, filepath);
        }

        public IShape DeepCopy()
        {
            return new RectangleShape(rect, primary, secondary, EdgeColor, EdgeWidth, EdgeEnabled, mode, texture);
        }

        private void RenderBrush()
        {
            switch (Mode)
            {
                case BrushType.Solid:
                    brush = new SolidBrush(primary);
                    break;
                case BrushType.Hatch:
                    brush = new HatchBrush(hatch, primary, secondary);
                    break;
                case BrushType.Gradient:
                    brush = new LinearGradientBrush(rect, primary, secondary, fillAngle);
                    break;
                case BrushType.Texture:
                    TextureBrush tb = new TextureBrush(texture, WrapMode.Tile);
                    tb.TranslateTransform(rect.X, rect.Y);
                    tb.RotateTransform(fillAngle);
                    brush = tb;
                    break;
            }
        }
        private Brush IsolationBrush(int addend)
        {
            switch (Mode)
            {
                case BrushType.Solid:
                    return new SolidBrush(primary);
                case BrushType.Hatch:
                    return new HatchBrush(Hatch, primary, secondary);
                case BrushType.Gradient:
                    return new LinearGradientBrush(new Rectangle(0, 0, rect.Width, rect.Height), primary, secondary, fillAngle);
                case BrushType.Texture:
                    TextureBrush tb = new TextureBrush(texture, WrapMode.Tile);
                    tb.TranslateTransform(addend, addend);
                    tb.RotateTransform(fillAngle);
                    return tb;
                default:
                    return null;
            }
        }
    }
}
