using System.Drawing;

namespace NNPG2_cv4
{
    public interface IShape
    {
        Brush Fill { get; set; }
        BrushType Mode { get; set; }
        Color Primary { get; set; }
        Color Secondary { get; set; }
        Pen Edge { get;}
        Color EdgeColor { get; set; }
        float EdgeWidth { get; set; }
        Size Size { get; }

        IShape DeepCopy();

        void Render(Graphics g);

        void RenderIsolation(Graphics g);

        bool Contains(Point p);

        Point[] ControlPoints();

        void TransformMove(Size addend);

        void TransformScale(Size addend, int index);
    }
}