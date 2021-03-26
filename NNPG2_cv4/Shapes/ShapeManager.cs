using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace NNPG2_cv4
{
    public class ShapeManager : IEnumerable<IShape>
    {       
        public int ControlPointIndex { get; set; }
        public IShape Focused { get { return shapes[index]; } }
        public bool IsFocused { get { return index != -1; } }

        private int index = -1;
        private readonly List<IShape> shapes = new List<IShape>();

        public void Duplicate()
        {
            if(IsFocused)
            {
                shapes.Add(Focused.DeepCopy());
                index = shapes.Count - 1;
            }
        }

        public void Add(IShape shape)
        {
            shapes.Add(shape);
        }

        public void Remove()
        {
            shapes.Remove(shapes[index]);
            index = -1;
        }

        public void MoveUp()
        {
            if (index == shapes.Count - 1) return;
            IShape tmp = shapes[index];
            shapes[index] = shapes[++index];
            shapes[index] = tmp;
        }

        public void MoveDown()
        {
            if (index == 0) return;
            IShape tmp = shapes[index];
            shapes[index] = shapes[--index];
            shapes[index] = tmp;
        }
        public void MoveBot()
        {
            IShape[] tmpList = new IShape[shapes.Count];
            IShape tmp = shapes[index];
            shapes.Remove(shapes[index]);
            shapes.CopyTo(tmpList, 1);
         
            shapes.Clear();
            shapes.AddRange(tmpList);
            index = 0;
            shapes[index] = tmp;
        }
        public void MoveTop()
        {
            IShape[] tmpList = new IShape[shapes.Count];
            IShape tmp = shapes[index];
            shapes.Remove(shapes[index]);
            shapes.CopyTo(tmpList);

            shapes.Clear();
            shapes.AddRange(tmpList);
            index = shapes.Count - 1;
            shapes[index] = tmp;           
        }

        public void RenderFocusShape(Point coor)
        {
            if (IsFocused)
            {
                foreach (Point cp in Focused.ControlPoints())
                {
                    if (Library.DistancePoint(cp, coor) <= 15)
                    {
                        return;
                    }
                }
            }
            for (int i = shapes.Count() - 1; i >= 0; i--)
            {
                if (shapes[i].Contains(coor))
                {
                    index = i;
                    return;
                }
            } 
            index = -1;
        }

        public IEnumerator<IShape> GetEnumerator()
        {
            return ((IEnumerable<IShape>)shapes).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return shapes.GetEnumerator();
        }
    }
}
