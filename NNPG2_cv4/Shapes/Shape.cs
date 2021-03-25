using System.Drawing;

namespace NNPG2_cv4
{
    public interface IShape
    {
        Color Primary { get; set; }
        Color Secondary { get; set; }
        Color Edge { get; set; }
        BrushType Mode { get; set; }

        void Render(Graphics g);

        bool Contains(Point p);

        Point[] ControlPoints();

        void TransformMove(Size addend);

        void TransformScale(Size addend, int index);
    }
}