using System.Drawing;

namespace NNPG2_cv4
{
    public class LineShape : IShape
    {
        public Color Primary { get; set; }
        public Color Secondary { get; set; }
        public Color Edge { get; set; }
        public BrushType Mode { get; set; }

        public Point start;
        public Point end;

        public LineShape(Point start, Point end)
        {
            this.start = start;
            this.end = end;
            Primary = Color.White;
            Secondary = Color.Gray;
            Edge = Color.Black;
            Mode = BrushType.Solid;
        }

        public LineShape(Point start, Point end, Color primary, Color secondary, Color edge, BrushType mode)
        {
            this.start = start;
            this.end = end;
            Primary = primary;
            Secondary = secondary;
            Edge = edge;
            Mode = mode;
        }

        override public string ToString()
        {
            return string.Format("Start: {0} End: {1}\n{2}", start, end, Edge);
        }

        public bool Contains(Point p)
        {
            return Library.DistanceLine(p, start, end) <= 15;
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
    }
}
