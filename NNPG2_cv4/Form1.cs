using System;
using System.Drawing;
using System.Windows.Forms;

namespace NNPG2_cv4
{
    public partial class Canvas : Form
    {
        private readonly Size controlPointSize = new Size(10, 10);
        private readonly Size controlPointShift = new Size(5, 5);

        private readonly ShapeManager shapes = new ShapeManager();
        private readonly ColorDialog colorObjectDialog = new ColorDialog();

        private Point start;
        private Point end;

        private Color backgroundColor = Color.WhiteSmoke;

        private Brush virtualBrush;
        private Pen virtualPen;

        private ShapeType addendShape;
        public Canvas()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            BrushType[] types = (BrushType[])Enum.GetValues(typeof(BrushType));         
            foreach (BrushType type in types)
            {
                comboFillType.Items.Add(type);
            }
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillRectangle(new SolidBrush(backgroundColor), 0, 0, Width, Height);
            foreach (IShape shape in shapes)
            {
                if (shape is RectangleShape rs)
                {    
                    g.FillRectangle(rs.Brush(), rs.rect);
                    g.DrawRectangle(new Pen(rs.Edge), rs.rect);
                }
                else if(shape is EllipseShape es)
                {
                    g.FillEllipse(es.Brush(), es.rect);
                    g.DrawEllipse(new Pen(es.Edge), es.rect);
                }
                else if (shape is LineShape ls)
                {
                    g.DrawLine(new Pen(ls.Edge), ls.start, ls.end);
                }
            }
            if (!shapes.IsFocused) return;
            foreach (Point point in shapes.Focused.ControlPoints())
            {
                e.Graphics.FillEllipse(Brushes.White, new Rectangle(point - controlPointShift, controlPointSize));
                e.Graphics.DrawEllipse(Pens.Black, new Rectangle(point - controlPointShift, controlPointSize));
            }
        }
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {             
                case MouseButtons.Left:
                    shapes.RenderFocusShape(e.Location);
                    if (shapes.IsFocused)
                    {    
                        start = e.Location;
                        MouseMove -= new MouseEventHandler(Canvas_ShapeMove);
                        MouseDown -= new MouseEventHandler(Canvas_MouseDownTransformation);
                        MouseMove += new MouseEventHandler(Canvas_ShapeMove);
                        MouseDown += new MouseEventHandler(Canvas_MouseDownTransformation);
                    }
                    break;
                case MouseButtons.Right:
                    shapes.RenderFocusShape(e.Location);
                    break;
                default:
                    return;
            }     
            Refresh();
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Text = "Vector Editor " + e.Location;
        }

        private void Canvas_ShapeMove(object sender, MouseEventArgs e)
        {
            shapes.Focused.TransformMove(new Size(e.X - start.X, e.Y - start.Y));
            start = e.Location;
            Refresh();
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseMove -= new MouseEventHandler(Canvas_ShapeMove);
            }
        }

        private void Canvas_MouseClick(object sender, MouseEventArgs e)
        {
            switch(e.Button)
            {
                case MouseButtons.Right:               
                    if (shapes.IsFocused)
                    {
                        comboFillType.SelectedItem = shapes.Focused.Mode;
                        ContextObject.Show(this, e.Location);
                    }
                    else
                    {
                        ContextCanvas.Show(this, e.Location);
                    }
                    break;
            }
        }

        private void itemMoveUp_Click(object sender, EventArgs e)
        {
            shapes.MoveUp();
            Refresh();
        }

        private void itemMoveDown_Click(object sender, EventArgs e)
        {
            shapes.MoveDown();
            Refresh();
        }

        private void itemMoveTop_Click(object sender, EventArgs e)
        {
            shapes.MoveTop();
            Refresh();
        }

        private void itemMoveBot_Click(object sender, EventArgs e)
        {
            shapes.MoveBot();
            Refresh();
        }

        private void itemBackground_Click(object sender, EventArgs e)
        {
            colorObjectDialog.Color = backgroundColor;
            if (colorObjectDialog.ShowDialog() == DialogResult.OK)
            {
                backgroundColor = colorObjectDialog.Color;
                Refresh();
            }
        }

        private void itemInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, shapes.Focused.ToString(), "Information");
        }

        private void itemDelete_Click(object sender, EventArgs e)
        {
            DeleteMessage();
        }

        private void itemColorMain_Click(object sender, EventArgs e)
        {
            colorObjectDialog.Color = shapes.Focused.Primary;
            if (colorObjectDialog.ShowDialog() == DialogResult.OK)
            {
                shapes.Focused.Primary = colorObjectDialog.Color;
                Refresh();
            }
        }

        private void itemColorSecondary_Click(object sender, EventArgs e)
        {
            colorObjectDialog.Color = shapes.Focused.Secondary;
            if (colorObjectDialog.ShowDialog() == DialogResult.OK)
            {
                shapes.Focused.Secondary = colorObjectDialog.Color;
                Refresh();
            }
        }

        private void itemColorEdge_Click(object sender, EventArgs e)
        {
            colorObjectDialog.Color = shapes.Focused.Edge;
            if (colorObjectDialog.ShowDialog() == DialogResult.OK)
            {
                shapes.Focused.Edge = colorObjectDialog.Color;
                Refresh();
            }
        }

        private void comboFillType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BrushType brushType = (BrushType) comboFillType.SelectedItem;
            if (brushType == shapes.Focused.Mode) return;
            shapes.Focused.Mode = brushType;
            Refresh();
        }

        private void itemAddRectangle_Click(object sender, EventArgs e)
        {
            InitVirtualDrawing(ShapeType.Rectangle);
        }

        private void itemAddEllipse_Click(object sender, EventArgs e)
        {
            InitVirtualDrawing(ShapeType.Ellipse);
        }
        private void itemAddLine_Click(object sender, EventArgs e)
        {
            InitVirtualDrawing(ShapeType.Line);
        }

        private void InitVirtualDrawing(ShapeType type)
        {
            MouseDown -= new MouseEventHandler(Canvas_MouseDown);
            MouseDown += new MouseEventHandler(Canvas_MouseDownVirtual);
            virtualBrush = new SolidBrush(Color.FromArgb(
                128,
                255 - backgroundColor.R,
                255 - backgroundColor.R,
                255 - backgroundColor.G));
            virtualPen = new Pen(Color.FromArgb(
                255 - backgroundColor.R,
                255 - backgroundColor.R,
                255 - backgroundColor.G), 2.5f)
            {
                DashCap = System.Drawing.Drawing2D.DashCap.Flat,
                DashPattern = new float[] { 2.0f, 2.0f }
            };
            addendShape = type;
        }

        private void Canvas_PaintVirtual(object sender, PaintEventArgs e)
        {
            Rectangle virtualRect = Rectangle.FromLTRB(Math.Min(start.X, end.X), Math.Min(start.Y, end.Y), Math.Max(start.X, end.X), Math.Max(start.Y, end.Y));
            switch (addendShape){
                case ShapeType.Rectangle:
                    e.Graphics.FillRectangle(virtualBrush, virtualRect);
                    e.Graphics.DrawRectangle(virtualPen, virtualRect);
                    break;
                case ShapeType.Ellipse:
                    e.Graphics.FillEllipse(virtualBrush, virtualRect);
                    e.Graphics.DrawEllipse(virtualPen, virtualRect);
                    break;
                case ShapeType.Line:
                    e.Graphics.DrawLine(virtualPen, start, end);
                    break;
            }           
        }

        private void Canvas_MouseDownVirtual(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                start = e.Location;
                Paint += new PaintEventHandler(Canvas_PaintVirtual);
                MouseMove += new MouseEventHandler(Canvas_ShapeMoveVirtual);
                MouseUp += new MouseEventHandler(Canvas_MouseUpVirtual);
            }
        }

        private void Canvas_ShapeMoveVirtual(object sender, MouseEventArgs e)
        {
            end = e.Location;
            Refresh();
        }

        private void Canvas_MouseUpVirtual(object sender, MouseEventArgs e)
        {
            MouseUp -= new MouseEventHandler(Canvas_MouseUpVirtual);
            MouseDown -= new MouseEventHandler(Canvas_MouseDownVirtual);
            Paint -= new PaintEventHandler(Canvas_PaintVirtual);
            MouseDown += new MouseEventHandler(Canvas_MouseDown);

            Rectangle virtualRect = Rectangle.FromLTRB(Math.Min(start.X, end.X), Math.Min(start.Y, end.Y), Math.Max(start.X, end.X), Math.Max(start.Y, end.Y));
            if (addendShape == ShapeType.Rectangle)
            {
                shapes.Add(new RectangleShape(virtualRect,
                    Color.White,
                    Color.Black,
                    Color.Black,
                    BrushType.Gradient));
            } else if(addendShape == ShapeType.Ellipse)
                {
                shapes.Add(new EllipseShape(virtualRect,
                    Color.White,
                    Color.Black,
                    Color.Black,
                    BrushType.Gradient));
                } else if (addendShape == ShapeType.Line)
            {
                shapes.Add(new LineShape(start, end,
                    Color.White,
                    Color.Black,
                    Color.Black,
                    BrushType.Solid));
            }

            Refresh();
        }

        private void Canvas_MouseDownTransformation(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Refresh();
                if (!shapes.IsFocused) return;
                Point[] points = shapes.Focused.ControlPoints();
                for (int i = 0; i < points.Length; i++)
                {
                    if(Library.DistancePoint(points[i], e.Location) <= 15)
                    {
                        start = e.Location;
                        shapes.ControlPointIndex = i;
                        MouseMove -= new MouseEventHandler(Canvas_ShapeMove);
                        MouseMove += new MouseEventHandler(Canvas_ShapeMoveTransformation);
                        MouseUp += new MouseEventHandler(Canvas_MouseUpTransformation);
                    }
                }
            }
        }

        private void Canvas_ShapeMoveTransformation(object sender, MouseEventArgs e)
        {
            IShape shape = shapes.Focused;
            Size addend = new Size(e.X - start.X, e.Y - start.Y);
            shape.TransformScale(addend, shapes.ControlPointIndex);
            start = e.Location;
            Refresh();
        }

        private void Canvas_MouseUpTransformation(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseMove -= new MouseEventHandler(Canvas_ShapeMoveTransformation);
                MouseUp -= new MouseEventHandler(Canvas_MouseUpTransformation);
            }
        }

        private void canvas_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    if(shapes.IsFocused)
                    {
                        MouseMove -= new MouseEventHandler(Canvas_ShapeMove);
                        DeleteMessage();
                    }
                    break;
            }          
        }

        private void DeleteMessage()
        {
            string message = "Do you want to delete the shape?";
            string title = "Delete Shape";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            if (MessageBox.Show(message, title, buttons) == DialogResult.Yes)
            {
                shapes.Remove();
                shapes.SetUnfocused();
                Refresh();
            }
        }
    }
}
