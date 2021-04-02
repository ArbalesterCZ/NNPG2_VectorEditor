
namespace NNPG2_cv4
{
    partial class Canvas
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Canvas));
            this.ContextObject = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.itemFill = new System.Windows.Forms.ToolStripMenuItem();
            this.comboFillType = new System.Windows.Forms.ToolStripComboBox();
            this.itemPrimaryColor = new System.Windows.Forms.ToolStripMenuItem();
            this.itemSecondaryColor = new System.Windows.Forms.ToolStripMenuItem();
            this.itemChangeTexture = new System.Windows.Forms.ToolStripMenuItem();
            this.itemHatchStyle = new System.Windows.Forms.ToolStripMenuItem();
            this.itemAngle = new System.Windows.Forms.ToolStripTextBox();
            this.itemEdge = new System.Windows.Forms.ToolStripMenuItem();
            this.itemEdgeColor = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxEdgeWidth = new System.Windows.Forms.ToolStripTextBox();
            this.itemEdgeEnable = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemMove = new System.Windows.Forms.ToolStripMenuItem();
            this.itemMoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.itemMoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.itemMoveTop = new System.Windows.Forms.ToolStripMenuItem();
            this.itemMoveBot = new System.Windows.Forms.ToolStripMenuItem();
            this.itemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.itemSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.itemAddShapeObject = new System.Windows.Forms.ToolStripMenuItem();
            this.itemAddRectangleObject = new System.Windows.Forms.ToolStripMenuItem();
            this.itemAddEllipseObject = new System.Windows.Forms.ToolStripMenuItem();
            this.itemAddLineObject = new System.Windows.Forms.ToolStripMenuItem();
            this.itemTransform = new System.Windows.Forms.ToolStripMenuItem();
            this.itemTransformToRectangle = new System.Windows.Forms.ToolStripMenuItem();
            this.itemTransformToEllipse = new System.Windows.Forms.ToolStripMenuItem();
            this.itemPrintObject = new System.Windows.Forms.ToolStripMenuItem();
            this.itemExportObject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.itemPrintSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.itemPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.itemBackground = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.printDialogShape = new System.Windows.Forms.PrintDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.ContextObject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // ContextObject
            // 
            this.ContextObject.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ContextObject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemInfo,
            this.itemFill,
            this.itemEdge,
            this.ItemMove,
            this.itemDelete,
            this.itemSeparator,
            this.itemAddShapeObject,
            this.itemTransform,
            this.itemPrintObject,
            this.itemExportObject,
            this.toolStripSeparator4,
            this.itemPrintSetting,
            this.itemPrint,
            this.toolStripMenuItem2,
            this.itemBackground});
            this.ContextObject.Name = "ContextObject";
            this.ContextObject.Size = new System.Drawing.Size(163, 302);
            // 
            // itemInfo
            // 
            this.itemInfo.Name = "itemInfo";
            this.itemInfo.Size = new System.Drawing.Size(162, 22);
            this.itemInfo.Text = "Information";
            this.itemInfo.Click += new System.EventHandler(this.ItemInfo_Click);
            // 
            // itemFill
            // 
            this.itemFill.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comboFillType,
            this.itemPrimaryColor,
            this.itemSecondaryColor,
            this.itemChangeTexture,
            this.itemHatchStyle,
            this.itemAngle});
            this.itemFill.Name = "itemFill";
            this.itemFill.Size = new System.Drawing.Size(162, 22);
            this.itemFill.Text = "Fill";
            // 
            // comboFillType
            // 
            this.comboFillType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFillType.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.comboFillType.Name = "comboFillType";
            this.comboFillType.Size = new System.Drawing.Size(121, 23);
            this.comboFillType.Tag = "Fill Style";
            this.comboFillType.ToolTipText = "Fill Style";
            this.comboFillType.SelectedIndexChanged += new System.EventHandler(this.ComboFillType_SelectedIndexChanged);
            // 
            // itemPrimaryColor
            // 
            this.itemPrimaryColor.Name = "itemPrimaryColor";
            this.itemPrimaryColor.Size = new System.Drawing.Size(181, 22);
            this.itemPrimaryColor.Text = "Primary";
            this.itemPrimaryColor.Click += new System.EventHandler(this.ItemColorPrimary_Click);
            // 
            // itemSecondaryColor
            // 
            this.itemSecondaryColor.Name = "itemSecondaryColor";
            this.itemSecondaryColor.Size = new System.Drawing.Size(181, 22);
            this.itemSecondaryColor.Text = "Secondary";
            this.itemSecondaryColor.Click += new System.EventHandler(this.ItemColorSecondary_Click);
            // 
            // itemChangeTexture
            // 
            this.itemChangeTexture.Name = "itemChangeTexture";
            this.itemChangeTexture.Size = new System.Drawing.Size(181, 22);
            this.itemChangeTexture.Text = "Change Texture";
            this.itemChangeTexture.Click += new System.EventHandler(this.ItemChangeTexture_Click);
            // 
            // itemHatchStyle
            // 
            this.itemHatchStyle.Name = "itemHatchStyle";
            this.itemHatchStyle.Size = new System.Drawing.Size(181, 22);
            this.itemHatchStyle.Text = "Hatch Style";
            // 
            // itemAngle
            // 
            this.itemAngle.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.itemAngle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.itemAngle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.itemAngle.MaxLength = 5;
            this.itemAngle.Name = "itemAngle";
            this.itemAngle.Size = new System.Drawing.Size(100, 23);
            this.itemAngle.Tag = "Angle";
            this.itemAngle.ToolTipText = "Angle";
            this.itemAngle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxOnlyNumber_KeyPress);
            this.itemAngle.TextChanged += new System.EventHandler(this.itemAngle_TextChanged);
            // 
            // itemEdge
            // 
            this.itemEdge.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemEdgeColor,
            this.textBoxEdgeWidth,
            this.itemEdgeEnable});
            this.itemEdge.Name = "itemEdge";
            this.itemEdge.Size = new System.Drawing.Size(162, 22);
            this.itemEdge.Text = "Edge";
            // 
            // itemEdgeColor
            // 
            this.itemEdgeColor.Name = "itemEdgeColor";
            this.itemEdgeColor.Size = new System.Drawing.Size(160, 22);
            this.itemEdgeColor.Text = "Color";
            this.itemEdgeColor.Click += new System.EventHandler(this.ItemColorEdge_Click);
            // 
            // textBoxEdgeWidth
            // 
            this.textBoxEdgeWidth.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBoxEdgeWidth.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxEdgeWidth.MaxLength = 4;
            this.textBoxEdgeWidth.Name = "textBoxEdgeWidth";
            this.textBoxEdgeWidth.Size = new System.Drawing.Size(100, 23);
            this.textBoxEdgeWidth.Tag = "Width";
            this.textBoxEdgeWidth.ToolTipText = "Width";
            this.textBoxEdgeWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxOnlyNumber_KeyPress);
            this.textBoxEdgeWidth.TextChanged += new System.EventHandler(this.TextBoxEdgeWidth_TextChanged);
            // 
            // itemEdgeEnable
            // 
            this.itemEdgeEnable.Checked = true;
            this.itemEdgeEnable.CheckOnClick = true;
            this.itemEdgeEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.itemEdgeEnable.Name = "itemEdgeEnable";
            this.itemEdgeEnable.Size = new System.Drawing.Size(160, 22);
            this.itemEdgeEnable.Text = "Enable";
            this.itemEdgeEnable.Click += new System.EventHandler(this.ItemEdgeEnable_Click);
            // 
            // ItemMove
            // 
            this.ItemMove.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemMoveUp,
            this.itemMoveDown,
            this.toolStripSeparator2,
            this.itemMoveTop,
            this.itemMoveBot});
            this.ItemMove.Name = "ItemMove";
            this.ItemMove.Size = new System.Drawing.Size(162, 22);
            this.ItemMove.Text = "Move";
            // 
            // itemMoveUp
            // 
            this.itemMoveUp.Name = "itemMoveUp";
            this.itemMoveUp.Size = new System.Drawing.Size(105, 22);
            this.itemMoveUp.Text = "Up";
            this.itemMoveUp.Click += new System.EventHandler(this.ItemMoveUp_Click);
            // 
            // itemMoveDown
            // 
            this.itemMoveDown.Name = "itemMoveDown";
            this.itemMoveDown.Size = new System.Drawing.Size(105, 22);
            this.itemMoveDown.Text = "Down";
            this.itemMoveDown.Click += new System.EventHandler(this.ItemMoveDown_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(102, 6);
            // 
            // itemMoveTop
            // 
            this.itemMoveTop.Name = "itemMoveTop";
            this.itemMoveTop.Size = new System.Drawing.Size(105, 22);
            this.itemMoveTop.Text = "Top";
            this.itemMoveTop.Click += new System.EventHandler(this.ItemMoveTop_Click);
            // 
            // itemMoveBot
            // 
            this.itemMoveBot.Name = "itemMoveBot";
            this.itemMoveBot.Size = new System.Drawing.Size(105, 22);
            this.itemMoveBot.Text = "Bot";
            this.itemMoveBot.Click += new System.EventHandler(this.ItemMoveBot_Click);
            // 
            // itemDelete
            // 
            this.itemDelete.Name = "itemDelete";
            this.itemDelete.Size = new System.Drawing.Size(162, 22);
            this.itemDelete.Text = "Delete";
            this.itemDelete.Click += new System.EventHandler(this.ItemDelete_Click);
            // 
            // itemSeparator
            // 
            this.itemSeparator.Name = "itemSeparator";
            this.itemSeparator.Size = new System.Drawing.Size(159, 6);
            // 
            // itemAddShapeObject
            // 
            this.itemAddShapeObject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemAddRectangleObject,
            this.itemAddEllipseObject,
            this.itemAddLineObject});
            this.itemAddShapeObject.Name = "itemAddShapeObject";
            this.itemAddShapeObject.Size = new System.Drawing.Size(162, 22);
            this.itemAddShapeObject.Text = "Add Shape";
            // 
            // itemAddRectangleObject
            // 
            this.itemAddRectangleObject.Name = "itemAddRectangleObject";
            this.itemAddRectangleObject.Size = new System.Drawing.Size(126, 22);
            this.itemAddRectangleObject.Text = "Rectangle";
            this.itemAddRectangleObject.Click += new System.EventHandler(this.ItemAddRectangle_Click);
            // 
            // itemAddEllipseObject
            // 
            this.itemAddEllipseObject.Name = "itemAddEllipseObject";
            this.itemAddEllipseObject.Size = new System.Drawing.Size(126, 22);
            this.itemAddEllipseObject.Text = "Ellipse";
            this.itemAddEllipseObject.Click += new System.EventHandler(this.ItemAddEllipse_Click);
            // 
            // itemAddLineObject
            // 
            this.itemAddLineObject.Name = "itemAddLineObject";
            this.itemAddLineObject.Size = new System.Drawing.Size(126, 22);
            this.itemAddLineObject.Text = "Line";
            this.itemAddLineObject.Click += new System.EventHandler(this.ItemAddLine_Click);
            // 
            // itemTransform
            // 
            this.itemTransform.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemTransformToRectangle,
            this.itemTransformToEllipse});
            this.itemTransform.Name = "itemTransform";
            this.itemTransform.Size = new System.Drawing.Size(162, 22);
            this.itemTransform.Text = "Transform Shape";
            // 
            // itemTransformToRectangle
            // 
            this.itemTransformToRectangle.Name = "itemTransformToRectangle";
            this.itemTransformToRectangle.Size = new System.Drawing.Size(141, 22);
            this.itemTransformToRectangle.Text = "To Rectangle";
            this.itemTransformToRectangle.Click += new System.EventHandler(this.ItemTransformToRectangle_Click);
            // 
            // itemTransformToEllipse
            // 
            this.itemTransformToEllipse.Name = "itemTransformToEllipse";
            this.itemTransformToEllipse.Size = new System.Drawing.Size(141, 22);
            this.itemTransformToEllipse.Text = "To Ellipse";
            this.itemTransformToEllipse.Click += new System.EventHandler(this.ItemTransformToEllipse_Click);
            // 
            // itemPrintObject
            // 
            this.itemPrintObject.Name = "itemPrintObject";
            this.itemPrintObject.Size = new System.Drawing.Size(162, 22);
            this.itemPrintObject.Text = "Print Shape";
            this.itemPrintObject.Click += new System.EventHandler(this.ItemPrintShape_Click);
            // 
            // itemExportObject
            // 
            this.itemExportObject.Name = "itemExportObject";
            this.itemExportObject.Size = new System.Drawing.Size(162, 22);
            this.itemExportObject.Text = "Export Shape";
            this.itemExportObject.Click += new System.EventHandler(this.ItemExportObject_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(159, 6);
            // 
            // itemPrintSetting
            // 
            this.itemPrintSetting.Name = "itemPrintSetting";
            this.itemPrintSetting.Size = new System.Drawing.Size(162, 22);
            this.itemPrintSetting.Text = "Print Setting";
            this.itemPrintSetting.Click += new System.EventHandler(this.ItemPrintSetting_Click);
            // 
            // itemPrint
            // 
            this.itemPrint.Name = "itemPrint";
            this.itemPrint.Size = new System.Drawing.Size(162, 22);
            this.itemPrint.Text = "Print";
            this.itemPrint.Click += new System.EventHandler(this.ItemPrintDialog_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(162, 22);
            this.toolStripMenuItem2.Text = "Export";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.ItemExportCanvas_Click);
            // 
            // itemBackground
            // 
            this.itemBackground.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            this.itemBackground.Name = "itemBackground";
            this.itemBackground.Size = new System.Drawing.Size(162, 22);
            this.itemBackground.Text = "Background";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(107, 22);
            this.toolStripMenuItem3.Text = "Color";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.ItemBackgroundColor_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(107, 22);
            this.toolStripMenuItem4.Text = "Image";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.ItemBackgroundImage_Click);
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // printDialogShape
            // 
            this.printDialogShape.UseEXDialog = true;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // Canvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 441);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Canvas";
            this.Text = "Vector Editor";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Canvas_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseUp);
            this.ContextObject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip ContextObject;
        private System.Windows.Forms.ToolStripMenuItem ItemMove;
        private System.Windows.Forms.ToolStripMenuItem itemMoveUp;
        private System.Windows.Forms.ToolStripMenuItem itemMoveDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem itemMoveTop;
        private System.Windows.Forms.ToolStripMenuItem itemMoveBot;
        private System.Windows.Forms.ToolStripSeparator itemSeparator;
        private System.Windows.Forms.ToolStripMenuItem itemBackground;
        private System.Windows.Forms.ToolStripMenuItem itemInfo;
        private System.Windows.Forms.ToolStripMenuItem itemDelete;
        private System.Windows.Forms.ToolStripMenuItem itemAddShapeObject;
        private System.Windows.Forms.ToolStripMenuItem itemAddRectangleObject;
        private System.Windows.Forms.ToolStripMenuItem itemAddEllipseObject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem itemAddLineObject;
        private System.Windows.Forms.ToolStripMenuItem itemFill;
        private System.Windows.Forms.ToolStripMenuItem itemPrimaryColor;
        private System.Windows.Forms.ToolStripMenuItem itemSecondaryColor;
        private System.Windows.Forms.ToolStripMenuItem itemEdge;
        private System.Windows.Forms.ToolStripMenuItem itemEdgeColor;
        private System.Windows.Forms.ToolStripTextBox textBoxEdgeWidth;
        private System.Windows.Forms.ToolStripComboBox comboFillType;
        private System.Windows.Forms.ToolStripMenuItem itemExportObject;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem itemChangeTexture;
        private System.Windows.Forms.ToolStripTextBox itemAngle;
        private System.Windows.Forms.ToolStripMenuItem itemEdgeEnable;
        private System.Windows.Forms.ToolStripMenuItem itemHatchStyle;
        private System.Windows.Forms.ToolStripMenuItem itemPrintObject;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Windows.Forms.PrintDialog printDialogShape;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem itemTransform;
        private System.Windows.Forms.ToolStripMenuItem itemTransformToRectangle;
        private System.Windows.Forms.ToolStripMenuItem itemTransformToEllipse;
        private System.Windows.Forms.ToolStripMenuItem itemPrint;
        private System.Windows.Forms.ToolStripMenuItem itemPrintSetting;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
    }
}

