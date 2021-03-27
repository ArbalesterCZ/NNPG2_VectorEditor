using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace NNPG2_cv4
{
    public class LineShape : IShape
    {
        public Brush Fill { get; set; }
        public BrushType Mode { get; set; }
        public Color Primary { get; set; }
        public Color Secondary { get; set; }
        public Pen Edge { get;}
        public Color EdgeColor { get { return Edge.Color; } set { Edge.Color = value; } }
        public float EdgeWidth { get { return Edge.Width; } set { Edge.Width = value; } }
        public Size Size { get { return new Size(Math.Abs(start.X - end.X), Math.Abs(start.Y - end.Y)); } }
        public Image Texture { set { _ = value; } }

        private Point start;
        private Point end;

        public LineShape(Point start, Point end)
        {
            this.start = start;
            this.end = end;
            Edge = new Pen(Color.White);
        }

        public LineShape(Point start, Point end, Color edge, float width)
        {
            this.start = start;
            this.end = end;
            Edge = new Pen(edge, width);
        }

        override public string ToString()
        {
            return string.Format("Start: {0} End: {1}\n{2} Width: [{3}px]", start, end, EdgeColor, EdgeWidth);
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

        public void Export(string filepath)
        {
            Size addend = new Size(Math.Min(start.X, end.X), Math.Min(start.Y, end.Y));
            Size addend2 = new Size((int)(EdgeWidth / 2), (int)(EdgeWidth / 2));

            Bitmap bmp = new Bitmap((int)(Size.Width + EdgeWidth), (int)(Size.Height + EdgeWidth));

            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawLine(Edge, start - addend + addend2, end - addend + addend2);
            bmp.Save(filepath);
        }

        public IShape DeepCopy()
        {
            return new LineShape(start, end, EdgeColor, EdgeWidth);
        }
    }
}
