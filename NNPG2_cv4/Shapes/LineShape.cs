using System;
using System.Drawing;

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
        public Size Size { get { return new Size(Math.Abs(start.X - end.X), Math.Abs(start.X - end.X)); } }

        private Point start;
        private Point end;

        public LineShape(Point start, Point end)
        {
            this.start = start;
            this.end = end;
            Edge = new Pen(Color.White);
        }

        public LineShape(Point start, Point end, Color primary, Color secondary, Color edge, float width)
        {
            this.start = start;
            this.end = end;
            Primary = primary;
            Secondary = secondary;
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

        public void RenderIsolation(Graphics g)
        {
            Size addend = new Size(Math.Min(start.X, end.X), Math.Min(start.Y, end.Y));
            g.DrawLine(Edge, start - addend, end - addend);
        }

        public IShape DeepCopy()
        {
            return new LineShape(start, end, Primary, Secondary, EdgeColor, EdgeWidth);
        }
    }
}
