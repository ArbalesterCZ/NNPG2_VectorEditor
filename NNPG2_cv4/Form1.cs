using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace NNPG2_cv4
{
    public partial class Canvas : Form
    {
        private readonly Size CONTROL_POINT_SIZE = new Size(10, 10);
        private readonly Size CONTROL_POINT_SHIFT = new Size(5, 5);

        private readonly ShapeManager SHAPE_MANAGER = new ShapeManager();
        private readonly ColorDialog COLOR_DIALOG = new ColorDialog();

        private readonly Background BACKGROUND = new Background(Color.Black);

        private readonly SaveFileDialog SAVE_DIALOG = new SaveFileDialog();
        private readonly OpenFileDialog OPEN_DIALOG = new OpenFileDialog();

        private readonly string SAVE_FILTER = "JPEG (*.JPG;*.JPEG)|*.jpg;*.JPEG|GIF (*.GIF)|*.gif|PNG (*.PNG)|*.png|BMP (*.BMP)|*.bmp|TIFF (*.TIFF)|*.tiff";
        private readonly string LOAD_FILTER = "Image Files(*.JPG;*.GIF;*.PNG;*.BMP;*.TIFF)|*.JPG;*.JPEG;*.GIF;*.PNG;*.BMP;*.TIFF|All files (*.*)|*.*";

        private PrintDocument printDocument = new PrintDocument();
        private PrintDocument printShape = new PrintDocument();

        private Point start;
        private Point end;

        private Brush virtualBrush;
        private Pen virtualPen;

        private ShapeType addendShape;
        public Canvas()
        {
            InitializeComponent();
            HatchInit();
            PrintInit();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            BrushType[] types = (BrushType[])Enum.GetValues(typeof(BrushType));         
            foreach (BrushType type in types) comboFillType.Items.Add(type);
        }

        private void PrintInit()
        {
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
            printShape.PrintPage += new PrintPageEventHandler(PrintDocument_PrintShape);

            printDocument.DocumentName = "NNPG2 Print";

            printDialogShape.Document = printShape;
            printDialog.Document = printDocument;
        }
        private void ItemPrintShape_Click(object sender, EventArgs e)
        {
            try { if (printDialogShape.ShowDialog() == DialogResult.OK) printShape.Print(); } catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void PrintDocument_PrintShape(object sender, PrintPageEventArgs e)
        {
            SHAPE_MANAGER.Focused.Print(e.Graphics, e.MarginBounds);
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            BACKGROUND.Render(g, e.MarginBounds.X + e.MarginBounds.X +  e.MarginBounds.Width, e.MarginBounds.Y + e.MarginBounds.Y + e.MarginBounds.Height);
            foreach (IShape shape in SHAPE_MANAGER) shape.Render(g);
        }
        private void ItemPrintDialog_Click(object sender, EventArgs e)
        {
            try { if (printDialog.ShowDialog() == DialogResult.OK) printDocument.Print(); } catch (Exception ex) { MessageBox.Show(ex.ToString()); }
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
                    SHAPE_MANAGER.RenderFocusShape(e.Location);
                    if (SHAPE_MANAGER.IsFocused)
                    {
                        start = e.Location;
                        if (SHAPE_MANAGER.IsFocusControlPoint(e.Location)) InitTransformation(e.Location);
                        else MouseMove += new MouseEventHandler(Canvas_ShapeMove);                
                    }
                    break;
                case MouseButtons.Right:
                    SHAPE_MANAGER.RenderFocusShape(e.Location);
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
            SHAPE_MANAGER.Focused.TransformMove(new Size(e.X - start.X, e.Y - start.Y));
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
                    RenderContextMenu(SHAPE_MANAGER.IsFocused);
                    ContextObject.Show(this, e.Location);
                    break;
            }
        }

        private void ItemMoveUp_Click(object sender, EventArgs e)
        {
            SHAPE_MANAGER.MoveUp();
            Refresh();
        }

        private void ItemMoveDown_Click(object sender, EventArgs e)
        {
            SHAPE_MANAGER.MoveDown();
            Refresh();
        }

        private void ItemMoveTop_Click(object sender, EventArgs e)
        {
            SHAPE_MANAGER.MoveTop();
            Refresh();
        }

        private void ItemMoveBot_Click(object sender, EventArgs e)
        {
            SHAPE_MANAGER.MoveBot();
            Refresh();
        }

        private void ItemInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, SHAPE_MANAGER.Focused.ToString(), "Information");
        }

        private void ItemDelete_Click(object sender, EventArgs e)
        {
            SubmitDelete();
        }

        private void ItemColorPrimary_Click(object sender, EventArgs e)
        {
            COLOR_DIALOG.Color = SHAPE_MANAGER.Focused.Primary;
            if (COLOR_DIALOG.ShowDialog() == DialogResult.OK)
            {
                SHAPE_MANAGER.Focused.Primary = COLOR_DIALOG.Color;
                Refresh();
            }
        }

        private void ItemColorSecondary_Click(object sender, EventArgs e)
        {
            COLOR_DIALOG.Color = SHAPE_MANAGER.Focused.Secondary;
            if (COLOR_DIALOG.ShowDialog() == DialogResult.OK)
            {
                SHAPE_MANAGER.Focused.Secondary = COLOR_DIALOG.Color;
                Refresh();
            }
        }

        private void ItemColorEdge_Click(object sender, EventArgs e)
        {
            COLOR_DIALOG.Color = SHAPE_MANAGER.Focused.Edge.Color;
            if (COLOR_DIALOG.ShowDialog() == DialogResult.OK)
            {
                SHAPE_MANAGER.Focused.EdgeColor = COLOR_DIALOG.Color;
                Refresh();
            }
        }

        private void ComboFillType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BrushType brushType = (BrushType)comboFillType.SelectedItem;
            if (brushType == SHAPE_MANAGER.Focused.Mode) return;
            SHAPE_MANAGER.Focused.Mode = brushType;
            RenderContextMenu(SHAPE_MANAGER.IsFocused);
            Refresh();
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
                255 - BACKGROUND.Color.R,
                255 - BACKGROUND.Color.R,
                255 - BACKGROUND.Color.G));
            virtualPen = new Pen(Color.FromArgb(
                255 - BACKGROUND.Color.R,
                255 - BACKGROUND.Color.R,
                255 - BACKGROUND.Color.G), 2.5f)
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
                    SHAPE_MANAGER.Add(new RectangleShape(virtualRect));
                    break;
                case ShapeType.Ellipse:
                    SHAPE_MANAGER.Add(new EllipseShape(virtualRect));
                    break;
                case ShapeType.Line:
                    SHAPE_MANAGER.Add(new LineShape(start, end));
                    break;
            }
            Refresh();
        }

        private void InitTransformation(Point coor)
        {
            Refresh();
            if (!SHAPE_MANAGER.IsFocused) return;
            Point[] points = SHAPE_MANAGER.Focused.ControlPoints();
            for (int i = 0; i < points.Length; i++)
            {
                if (Library.DistancePoint(points[i], coor) <= 15)
                {
                    start = coor;
                    SHAPE_MANAGER.ControlPointIndex = i;
                    MouseMove -= new MouseEventHandler(Canvas_ShapeMove);
                    MouseUp += new MouseEventHandler(Canvas_MouseUpTransformation);
                    MouseMove += new MouseEventHandler(Canvas_ShapeMoveTransformation);
                    return;
                }
            }
        }
        private void Canvas_ShapeMoveTransformation(object sender, MouseEventArgs e)
        {
            IShape shape = SHAPE_MANAGER.Focused;
            Size addend = new Size(e.X - start.X, e.Y - start.Y);
            shape.TransformScale(addend, SHAPE_MANAGER.ControlPointIndex);
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
                    if(SHAPE_MANAGER.IsFocused)
                    {
                        MouseMove -= new MouseEventHandler(Canvas_ShapeMove);
                        SubmitDelete();
                    }
                    break;
                case Keys.D:
                    if (ModifierKeys.HasFlag(Keys.Control)) SHAPE_MANAGER.Duplicate();
                    break;
            }          
        }

        private void SubmitDelete()
        {
            string message = "Do you want to delete the shape?";
            string title = "Delete Shape";
            if (MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                SHAPE_MANAGER.Remove();
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
                SHAPE_MANAGER.Focused.EdgeWidth = result;
                Refresh();
            }
        }

        private void ItemExportCanvas_Click(object sender, EventArgs e)
        {
            SAVE_DIALOG.FileName = string.Format("Canvas {0}x{1}", ClientSize.Width, ClientSize.Height);
            if (Library.FileDialog(SAVE_DIALOG, SAVE_FILTER, out string filepath))
            {
                Bitmap bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
                Render(Graphics.FromImage(bmp));
                Library.SaveImage(bmp, filepath);
            }
        }

        private void ItemExportObject_Click(object sender, EventArgs e)
        {
            SAVE_DIALOG.FileName = SHAPE_MANAGER.Focused.ToString();
            if (Library.FileDialog(SAVE_DIALOG, SAVE_FILTER, out string filepath))
            {
                SHAPE_MANAGER.Focused.Export(filepath);
            }
        }
        private void Render(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            BACKGROUND.Render(g, ClientSize.Width, ClientSize.Height);
            foreach (IShape shape in SHAPE_MANAGER) shape.Render(g);
            if (!SHAPE_MANAGER.IsFocused) return;
            foreach (Point controlPoint in SHAPE_MANAGER.Focused.ControlPoints())
            {
                g.FillEllipse(Brushes.White, new Rectangle(controlPoint - CONTROL_POINT_SHIFT, CONTROL_POINT_SIZE));
                g.DrawEllipse(Pens.Black, new Rectangle(controlPoint - CONTROL_POINT_SHIFT, CONTROL_POINT_SIZE));
            }
        }

        private void ItemBackgroundColor_Click(object sender, EventArgs e)
        {
            COLOR_DIALOG.Color = BACKGROUND.Color;
            if (COLOR_DIALOG.ShowDialog() == DialogResult.OK)
            {
                BACKGROUND.Color = COLOR_DIALOG.Color;
                Refresh();
            }
        }

        private void ItemBackgroundImage_Click(object sender, EventArgs e)
        {
            if (Library.FileDialog(OPEN_DIALOG, LOAD_FILTER, out string filepath))
            {
                BACKGROUND.Image = Image.FromFile(filepath);
                Refresh();
            }
        }

        private void ItemChangeTexture_Click(object sender, EventArgs e)
        {
            if (Library.FileDialog(OPEN_DIALOG, LOAD_FILTER, out string filepath))
            {
                SHAPE_MANAGER.Focused.Texture = Image.FromFile(filepath);
                Refresh();
            }
        }

        private void itemAngle_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(itemAngle.Text.ToString(), out float result))
            {
                SHAPE_MANAGER.Focused.FillAngle = result;
                Refresh();
            }
        }

        private void ItemEdgeEnable_Click(object sender, EventArgs e)
        {
            SHAPE_MANAGER.Focused.EdgeEnabled = !SHAPE_MANAGER.Focused.EdgeEnabled;
        }

        private void ItemHacth_Click(object sender, EventArgs e)
        {
            HatchStyle hatchStyle = (HatchStyle)((ToolStripMenuItem)sender).Tag;
            SHAPE_MANAGER.Focused.Hatch = hatchStyle;
            Bitmap bm = new Bitmap(30, 30);
            Graphics gr = Graphics.FromImage(bm);
            gr.FillRectangle(new HatchBrush(hatchStyle, SHAPE_MANAGER.Focused.Primary, SHAPE_MANAGER.Focused.Secondary), new Rectangle(0, 0, 30, 30));
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
        private void RenderContextMenu(bool isShapeFocused)
        {
            itemSeparator.Visible = isShapeFocused;
            itemExportObject.Visible = isShapeFocused;
            itemPrintObject.Visible = isShapeFocused;
            itemDelete.Visible = isShapeFocused;
            ItemMove.Visible = isShapeFocused;
            itemEdge.Visible = isShapeFocused;
            itemFill.Visible = isShapeFocused;
            itemInfo.Visible = isShapeFocused;

            if (isShapeFocused)
            {
                switch (SHAPE_MANAGER.Focused.Mode)
                {
                    case BrushType.Solid:
                        itemPrimaryColor.Visible = true;
                        itemSecondaryColor.Visible = false;
                        itemChangeTexture.Visible = false;
                        itemAngle.Visible = false;
                        itemHatchStyle.Visible = false;
                        break;
                    case BrushType.Gradient:
                        itemPrimaryColor.Visible = true;
                        itemSecondaryColor.Visible = true;
                        itemChangeTexture.Visible = false;
                        itemAngle.Visible = true;
                        itemHatchStyle.Visible = false;
                        break;
                    case BrushType.Hatch:
                        itemPrimaryColor.Visible = true;
                        itemSecondaryColor.Visible = true;
                        itemChangeTexture.Visible = false;
                        itemAngle.Visible = false;
                        itemHatchStyle.Visible = true;
                        break;
                    case BrushType.Texture:
                        itemPrimaryColor.Visible = false;
                        itemSecondaryColor.Visible = false;
                        itemChangeTexture.Visible = true;
                        itemAngle.Visible = true;
                        itemHatchStyle.Visible = false;
                        break;
                }
                comboFillType.SelectedItem = SHAPE_MANAGER.Focused.Mode;
                itemAngle.Text = SHAPE_MANAGER.Focused.FillAngle.ToString();
                textBoxEdgeWidth.Text = SHAPE_MANAGER.Focused.EdgeWidth.ToString();
                itemEdgeEnable.Checked = SHAPE_MANAGER.Focused.EdgeEnabled;
                bool isLine = SHAPE_MANAGER.Focused is LineShape;

                SetItemColor(itemPrimaryColor, SHAPE_MANAGER.Focused.Primary);
                SetItemColor(itemSecondaryColor, SHAPE_MANAGER.Focused.Secondary);
                SetItemColor(itemEdgeColor, SHAPE_MANAGER.Focused.EdgeColor);

                itemEdgeEnable.Visible = !isLine;
                itemFill.Visible = !isLine;

                Bitmap bm = new Bitmap(30, 30);
                Graphics gr = Graphics.FromImage(bm);
                gr.FillRectangle(new HatchBrush(SHAPE_MANAGER.Focused.Hatch, SHAPE_MANAGER.Focused.Primary, SHAPE_MANAGER.Focused.Secondary), new Rectangle(0, 0, 30, 30));
                itemHatchStyle.Image = Image.FromHbitmap(bm.GetHbitmap());
            }
        }
    }  
}
