using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NNPG2_cv4
{
    public partial class Canvas : Form
    {
        private readonly Size controlPointSize = new Size(10, 10);
        private readonly Size controlPointShift = new Size(5, 5);

        private readonly ShapeManager shapeManager = new ShapeManager();
        private readonly ColorDialog colorObjectDialog = new ColorDialog();

        private readonly Background background = new Background(Color.Black);

        private Point start;
        private Point end;

        private Brush virtualBrush;
        private Pen virtualPen;

        private readonly SaveFileDialog saveDialog = new SaveFileDialog();
        private readonly OpenFileDialog openDialog = new OpenFileDialog();

        private readonly string SAVE_FILTER = "JPEG (*.JPG;*.JPEG)|*.jpg;*.JPEG|GIF (*.GIF)|*.gif|PNG (*.PNG)|*.png|BMP (*.BMP)|*.bmp|TIFF (*.TIFF)|*.tigg";
        private readonly string LOAD_FILTER = "Image Files(*.JPG;*.GIF;*.PNG;*.BMP;*.TIFF)|*.JPG;*.JPEG;*.GIF;*.PNG;*.BMP;*.TIFF|All files (*.*)|*.*";

        private ShapeType addendShape;
        public Canvas()
        {
            InitializeComponent();
            HatchInit();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            BrushType[] types = (BrushType[])Enum.GetValues(typeof(BrushType));         
            foreach (BrushType type in types)
            {
                comboFillType.Items.Add(type);
            }
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Render(e.Graphics);
        }
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    shapeManager.RenderFocusShape(e.Location);
                    if (shapeManager.IsFocused)
                    {
                        start = e.Location;
                        if (shapeManager.IsFocusControlPoint(e.Location)) InitTransformation(e.Location);
                        else MouseMove += new MouseEventHandler(Canvas_ShapeMove);                
                    }
                    break;
                case MouseButtons.Right:
                    shapeManager.RenderFocusShape(e.Location);
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
            shapeManager.Focused.TransformMove(new Size(e.X - start.X, e.Y - start.Y));
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
                    RenderContextMenu(shapeManager.IsFocused);
                    ContextObject.Show(this, e.Location);
                    break;
            }
        }

        private void ItemMoveUp_Click(object sender, EventArgs e)
        {
            shapeManager.MoveUp();
            Refresh();
        }

        private void ItemMoveDown_Click(object sender, EventArgs e)
        {
            shapeManager.MoveDown();
            Refresh();
        }

        private void ItemMoveTop_Click(object sender, EventArgs e)
        {
            shapeManager.MoveTop();
            Refresh();
        }

        private void ItemMoveBot_Click(object sender, EventArgs e)
        {
            shapeManager.MoveBot();
            Refresh();
        }

        private void ItemInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, shapeManager.Focused.ToString(), "Information");
        }

        private void ItemDelete_Click(object sender, EventArgs e)
        {
            SubmitDelete();
        }

        private void ItemColorPrimary_Click(object sender, EventArgs e)
        {
            colorObjectDialog.Color = shapeManager.Focused.Primary;
            if (colorObjectDialog.ShowDialog() == DialogResult.OK)
            {
                shapeManager.Focused.Primary = colorObjectDialog.Color;
                Refresh();
            }
        }

        private void ItemColorSecondary_Click(object sender, EventArgs e)
        {
            colorObjectDialog.Color = shapeManager.Focused.Secondary;
            if (colorObjectDialog.ShowDialog() == DialogResult.OK)
            {
                shapeManager.Focused.Secondary = colorObjectDialog.Color;
                Refresh();
            }
        }

        private void ItemColorEdge_Click(object sender, EventArgs e)
        {
            colorObjectDialog.Color = shapeManager.Focused.Edge.Color;
            if (colorObjectDialog.ShowDialog() == DialogResult.OK)
            {
                shapeManager.Focused.EdgeColor = colorObjectDialog.Color;
                Refresh();
            }
        }

        private void ComboFillType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BrushType brushType = (BrushType)comboFillType.SelectedItem;
            if (brushType == shapeManager.Focused.Mode) return;
            shapeManager.Focused.Mode = brushType;
            RenderContextMenu(shapeManager.IsFocused);
            Refresh();
        }

        private void RenderContextMenu(bool isShapeFocused)
        {
            itemSeparator.Visible = isShapeFocused;
            itemExportObject.Visible = isShapeFocused;
            itemDelete.Visible = isShapeFocused;
            ItemMove.Visible = isShapeFocused;
            itemEdge.Visible = isShapeFocused;
            itemFill.Visible = isShapeFocused;
            itemInfo.Visible = isShapeFocused;

            if (isShapeFocused)
            {
                switch(shapeManager.Focused.Mode)
                {
                    case BrushType.Solid:
                        itemPrimaryColor.Visible = true;
                        itemSecondaryColor.Visible = false;
                        itemChangeTexture.Visible = false;
                        itemAngle.Visible = false;
                        itemHatchStyle.Visible = false;
                        break;
                    case BrushType.Hatch:
                        itemPrimaryColor.Visible = true;
                        itemSecondaryColor.Visible = true;
                        itemChangeTexture.Visible = false;
                        itemAngle.Visible = false;
                        itemHatchStyle.Visible = true;
                        break;
                    case BrushType.Gradient:
                        itemPrimaryColor.Visible = true;
                        itemSecondaryColor.Visible = true;
                        itemChangeTexture.Visible = false;
                        itemAngle.Visible = true;
                        itemAngle.Text = shapeManager.Focused.FillAngle.ToString();
                        itemHatchStyle.Visible = false;
                        break;
                    case BrushType.Texture:
                        itemPrimaryColor.Visible = false;
                        itemSecondaryColor.Visible = false;
                        itemChangeTexture.Visible = true;
                        itemAngle.Visible = false;
                        itemHatchStyle.Visible = false;
                        break;
                }
                comboFillType.SelectedItem = shapeManager.Focused.Mode;
                textBoxEdgeWidth.Text = shapeManager.Focused.EdgeWidth.ToString();
                itemEdgeEnable.Checked = shapeManager.Focused.EdgeEnabled;
                bool isLine = shapeManager.Focused is LineShape;

                SetItemColor(itemPrimaryColor, shapeManager.Focused.Primary);
                SetItemColor(itemSecondaryColor, shapeManager.Focused.Secondary);
                SetItemColor(itemEdgeColor, shapeManager.Focused.EdgeColor);

                itemEdgeEnable.Visible = !isLine;
                itemFill.Visible = !isLine;

                Bitmap bm = new Bitmap(30, 30);
                Graphics gr = Graphics.FromImage(bm);
                gr.FillRectangle(new HatchBrush(shapeManager.Focused.Hatch, shapeManager.Focused.Primary, shapeManager.Focused.Secondary), new Rectangle(0, 0, 30, 30));
                itemHatchStyle.Image = Image.FromHbitmap(bm.GetHbitmap());
            }
        }

        private void SetItemColor(ToolStripMenuItem item, Color color)
        {
            Bitmap bm = new Bitmap(20, 30);
            Graphics g = Graphics.FromImage(bm);
            g.FillRectangle(new SolidBrush(color), new Rectangle(0, 0, 20, 30));
            item.Image = Image.FromHbitmap(bm.GetHbitmap());
        }

        private void ItemAddRectangle_Click(object sender, EventArgs e)
        {
            InitVirtualDrawing(ShapeType.Rectangle);
        }

        private void ItemAddEllipse_Click(object sender, EventArgs e)
        {
            InitVirtualDrawing(ShapeType.Ellipse);
        }
        private void ItemAddLine_Click(object sender, EventArgs e)
        {
            InitVirtualDrawing(ShapeType.Line);
        }

        private void InitVirtualDrawing(ShapeType type)
        {
            MouseDown -= new MouseEventHandler(Canvas_MouseDown);
            MouseDown -= new MouseEventHandler(Canvas_MouseDownVirtual);
            MouseDown += new MouseEventHandler(Canvas_MouseDownVirtual);
            virtualBrush = new SolidBrush(Color.FromArgb(
                128,
                255 - background.Color.R,
                255 - background.Color.R,
                255 - background.Color.G));
            virtualPen = new Pen(Color.FromArgb(
                255 - background.Color.R,
                255 - background.Color.R,
                255 - background.Color.G), 2.5f)
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
            switch (addendShape)
            {
                case ShapeType.Rectangle:
                    shapeManager.Add(new RectangleShape(virtualRect));
                    break;
                case ShapeType.Ellipse:
                    shapeManager.Add(new EllipseShape(virtualRect));
                    break;
                case ShapeType.Line:
                    shapeManager.Add(new LineShape(start, end));
                    break;
            }
            Refresh();
        }

        private void InitTransformation(Point coor)
        {
            Refresh();
            if (!shapeManager.IsFocused) return;
            Point[] points = shapeManager.Focused.ControlPoints();
            for (int i = 0; i < points.Length; i++)
            {
                if (Library.DistancePoint(points[i], coor) <= 15)
                {
                    start = coor;
                    shapeManager.ControlPointIndex = i;
                    MouseMove -= new MouseEventHandler(Canvas_ShapeMove);
                    MouseUp += new MouseEventHandler(Canvas_MouseUpTransformation);
                    MouseMove += new MouseEventHandler(Canvas_ShapeMoveTransformation);
                    return;
                }
            }
        }
        private void Canvas_ShapeMoveTransformation(object sender, MouseEventArgs e)
        {
            IShape shape = shapeManager.Focused;
            Size addend = new Size(e.X - start.X, e.Y - start.Y);
            shape.TransformScale(addend, shapeManager.ControlPointIndex);
            start = e.Location;
            Refresh();
        }

        private void Canvas_MouseUpTransformation(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseUp -= new MouseEventHandler(Canvas_MouseUpTransformation);
                MouseMove -= new MouseEventHandler(Canvas_ShapeMoveTransformation);               
            }
        }
        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    if(shapeManager.IsFocused)
                    {
                        MouseMove -= new MouseEventHandler(Canvas_ShapeMove);
                        SubmitDelete();
                    }
                    break;
                case Keys.D:
                    if (ModifierKeys.HasFlag(Keys.Control)) shapeManager.Duplicate();
                    break;
            }          
        }

        private void SubmitDelete()
        {
            string message = "Do you want to delete the shape?";
            string title = "Delete Shape";
            if (MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                shapeManager.Remove();
                Refresh();
            }
        }

        private void TextBoxOnlyNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == ',') && (sender.ToString().IndexOf(',') != -1))
            {
                e.Handled = true;
            }
        }

        private void TextBoxEdgeWidth_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(sender.ToString(), out float result))
            {
                shapeManager.Focused.EdgeWidth = result;
                Refresh();
            }
        }

        private void ItemExportCanvas_Click(object sender, EventArgs e)
        {
            saveDialog.FileName = string.Format("Canvas {0}x{1}", ClientSize.Width, ClientSize.Height);
            if (Library.FileDialog(saveDialog, SAVE_FILTER, out string filepath))
            {
                Bitmap bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
                Graphics g = Graphics.FromImage(bmp);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Render(g);
                Library.SaveImage(bmp, filepath);
            }
        }

        private void ItemExportObject_Click(object sender, EventArgs e)
        {
            saveDialog.FileName = shapeManager.Focused.ToString();
            if (Library.FileDialog(saveDialog, SAVE_FILTER, out string filepath))
            {
                shapeManager.Focused.Export(filepath);
            }
        }
        private void Render(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            background.Render(g, ClientSize.Width, ClientSize.Height);
            foreach (IShape shape in shapeManager) shape.Render(g);
            if (!shapeManager.IsFocused) return;
            foreach (Point controlPoint in shapeManager.Focused.ControlPoints())
            {
                g.FillEllipse(Brushes.White, new Rectangle(controlPoint - controlPointShift, controlPointSize));
                g.DrawEllipse(Pens.Black, new Rectangle(controlPoint - controlPointShift, controlPointSize));
            }
        }

        private void ItemBackgroundColor_Click(object sender, EventArgs e)
        {
            colorObjectDialog.Color = background.Color;
            if (colorObjectDialog.ShowDialog() == DialogResult.OK)
            {
                background.Color = colorObjectDialog.Color;
                Refresh();
            }
        }

        private void ItemBackgroundImage_Click(object sender, EventArgs e)
        {
            if (Library.FileDialog(openDialog, LOAD_FILTER, out string filepath))
            {
                background.Image = Image.FromFile(filepath);
                Refresh();
            }
        }

        private void ItemChangeTexture_Click(object sender, EventArgs e)
        {
            if (Library.FileDialog(openDialog, LOAD_FILTER, out string filepath))
            {
                shapeManager.Focused.Texture = Image.FromFile(filepath);
                Refresh();
            }
        }

        private void itemAngle_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(itemAngle.Text.ToString(), out float result))
            {
                shapeManager.Focused.FillAngle = result;
                Refresh();
            }
        }

        private void ItemEdgeEnable_Click(object sender, EventArgs e)
        {
            shapeManager.Focused.EdgeEnabled = !shapeManager.Focused.EdgeEnabled;
        }

        private void ItemHacth_Click(object sender, EventArgs e)
        {
            HatchStyle hatchStyle = (HatchStyle)((ToolStripMenuItem)sender).Tag;
            shapeManager.Focused.Hatch = hatchStyle;
            Bitmap bm = new Bitmap(30, 30);
            Graphics gr = Graphics.FromImage(bm);
            gr.FillRectangle(new HatchBrush(hatchStyle, shapeManager.Focused.Primary, shapeManager.Focused.Secondary), new Rectangle(0, 0, 30, 30));
            itemHatchStyle.Image = Image.FromHbitmap(bm.GetHbitmap());
        }
        private void HatchInit()
        {
            for (int i = 0; i < 53; i++)
            {
                HatchStyle hStyle = (HatchStyle)i;
                ToolStripMenuItem newHatchItem = new ToolStripMenuItem();

                Bitmap bm = new Bitmap(30, 30);
                Graphics gr = Graphics.FromImage(bm);
                gr.FillRectangle(new HatchBrush(hStyle, Color.White, Color.Green), new Rectangle(0, 0, 30, 30));
                newHatchItem.Image = Image.FromHbitmap(bm.GetHbitmap());
                newHatchItem.Text = hStyle.ToString();
                newHatchItem.Tag = hStyle;

                newHatchItem.Click += new EventHandler(ItemHacth_Click);

                itemHatchStyle.DropDownItems.Add(newHatchItem);
            }
        }
    }  
}
