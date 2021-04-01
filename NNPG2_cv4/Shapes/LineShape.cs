using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace NNPG2_cv4
{
    public class LineShape : IShape
    {
        public BrushType Mode { get; set; }
        public float FillAngle { get; set; }
        public Color Primary { get; set; }
        public Color Secondary { get; set; }
        public Pen Edge { get;}
        public Color EdgeColor { get { return Edge.Color; } set { Edge.Color = value; } }
        public float EdgeWidth { get { return Edge.Width; } set { Edge.Width = value; } }
        public Size Size { get { return new Size((int)(Math.Abs(start.X - end.X) + EdgeWidth), (int)(Math.Abs(start.Y - end.Y) + EdgeWidth)); } }
        public Image Texture { set { _ = value; } }
        public bool EdgeEnabled { get; set; }
        public HatchStyle Hatch { get; set; }

        private Point start;
        private Point end;

        public LineShape(Point start, Point end)
        {
            this.start = start;
            this.end = end;
            Edge = new Pen(Color.Red, 4);
        }

        public LineShape(Point start, Point end, Color edge, float width)
        {
            this.start = start;
            this.end = end;
            Edge = new Pen(edge, width);
        }

        override public string ToString()
        {
            return string.Format("Line {0}x{1}", Size.Width, Size.Height);
        }

        public bool Contains(Point p)
        {
            return Library.DistanceLine(p, start, end) <= EdgeWidth / 2 + 10;
        }

        public Point[] ControlPoints()
        {
            return new Point[2] { start, end};
        }

        public void TransformMove(Size addend)
        {
            start += addend;
            end += addend;
        }

        public void TransformScale(Size addend, int index)
        {
            switch (index)
            {
                case 0:
                    start += addend;
                    break;
                case 1:
                    end += addend;
                    break;
            }
        }

        public void Render(Graphics g)
        {
            g.DrawLine(Edge, start, end);
        }

        public void Print(Graphics g, Rectangle printArea)
        {
            float multiplyFactor = Math.Min((float)printArea.Width / Size.Width, (float)printArea.Height / Size.Height);
            Rectangle isolatedRect = new Rectangle(printArea.X, printArea.Y, (int)(Size.Width * multiplyFactor), (int)(Size.Height * multiplyFactor));

            Point rightPoint;
            Point leftPoint;
            if (start.X > end.X) { rightPoint = start; leftPoint = end; } else { rightPoint = end; leftPoint = start; }

            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (leftPoint.Y < rightPoint.Y) g.DrawLine(Edge, isolatedRect.Location, new Point(isolatedRect.Right, isolatedRect.Bottom));
            else g.DrawLine(Edge, new Point(isolatedRect.Left, isolatedRect.Bottom), new Point(isolatedRect.Right, isolatedRect.Top));
        }
        public void Export(string filepath)
        {
            Size addend = new Size(Math.Min(start.X, end.X), Math.Min(start.Y, end.Y));
            Size addend2 = new Size((int)(EdgeWidth / 2), (int)(EdgeWidth / 2));

            Bitmap bmp = new Bitmap(Size.Width, Size.Height);

            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawLine(Edge, start - addend + addend2, end - addend + addend2);
            Library.SaveImage(bmp, filepath);
        }

        public IShape DeepCopy()
        {
            return new LineShape(start, end, EdgeColor, EdgeWidth);
        }

        public RectangleShape TransformToRectangle()
        {
            Size rectSize = new Size(Math.Abs(start.X - end.X), Math.Abs(start.Y - end.Y));
            int left = start.X < end.X ? start.X : end.X;
            int top = start.Y < end.Y ? start.Y : end.Y;
            Point location = new Point(left, top);

            return new RectangleShape(new Rectangle(location, rectSize), Primary, Secondary, EdgeColor, EdgeWidth, true, Mode, Library.DEFAULT_TEXTURE);
        }

        public EllipseShape TransformToEllipse()
        {
            Size rectSize = new Size(Math.Abs(start.X - end.X), Math.Abs(start.Y - end.Y));
            int left = start.X < end.X ? start.X : end.X;
            int top = start.Y < end.Y ? start.Y : end.Y;
            Point location = new Point(left, top);

            return new EllipseShape(new Rectangle(location, rectSize), Primary, Secondary, EdgeColor, EdgeWidth, true, Mode, Library.DEFAULT_TEXTURE);
        }
    }
}
