
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
            this.ContextObject = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.itemColor = new System.Windows.Forms.ToolStripMenuItem();
            this.itemColorMain = new System.Windows.Forms.ToolStripMenuItem();
            this.itemColorSecondary = new System.Windows.Forms.ToolStripMenuItem();
            this.itemColorEdge = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemMove = new System.Windows.Forms.ToolStripMenuItem();
            this.itemMoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.itemMoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.itemMoveTop = new System.Windows.Forms.ToolStripMenuItem();
            this.itemMoveBot = new System.Windows.Forms.ToolStripMenuItem();
            this.itemFillType = new System.Windows.Forms.ToolStripMenuItem();
            this.comboFillType = new System.Windows.Forms.ToolStripComboBox();
            this.itemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.itemAddShapeObject = new System.Windows.Forms.ToolStripMenuItem();
            this.itemAddRectangleObject = new System.Windows.Forms.ToolStripMenuItem();
            this.itemAddEllipseObject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.itemBackground = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextCanvas = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemAddShapeCanvas = new System.Windows.Forms.ToolStripMenuItem();
            this.itemAddRectangle = new System.Windows.Forms.ToolStripMenuItem();
            this.itemAddEllipse = new System.Windows.Forms.ToolStripMenuItem();
            this.itemAddLine = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.itemBackgroundC = new System.Windows.Forms.ToolStripMenuItem();
            this.itemAddLineObject = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextObject.SuspendLayout();
            this.ContextCanvas.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContextObject
            // 
            this.ContextObject.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ContextObject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemInfo,
            this.itemColor,
            this.ItemMove,
            this.itemFillType,
            this.itemDelete,
            this.toolStripSeparator1,
            this.itemAddShapeObject,
            this.toolStripSeparator4,
            this.itemBackground});
            this.ContextObject.Name = "ContextObject";
            this.ContextObject.Size = new System.Drawing.Size(211, 212);
            // 
            // itemInfo
            // 
            this.itemInfo.Name = "itemInfo";
            this.itemInfo.Size = new System.Drawing.Size(210, 24);
            this.itemInfo.Text = "Information";
            this.itemInfo.Click += new System.EventHandler(this.itemInfo_Click);
            // 
            // itemColor
            // 
            this.itemColor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemColorMain,
            this.itemColorSecondary,
            this.itemColorEdge});
            this.itemColor.Name = "itemColor";
            this.itemColor.Size = new System.Drawing.Size(210, 24);
            this.itemColor.Text = "Color";
            // 
            // itemColorMain
            // 
            this.itemColorMain.Name = "itemColorMain";
            this.itemColorMain.Size = new System.Drawing.Size(161, 26);
            this.itemColorMain.Text = "Primary";
            this.itemColorMain.Click += new System.EventHandler(this.itemColorMain_Click);
            // 
            // itemColorSecondary
            // 
            this.itemColorSecondary.Name = "itemColorSecondary";
            this.itemColorSecondary.Size = new System.Drawing.Size(161, 26);
            this.itemColorSecondary.Text = "Secondary";
            this.itemColorSecondary.Click += new System.EventHandler(this.itemColorSecondary_Click);
            // 
            // itemColorEdge
            // 
            this.itemColorEdge.Name = "itemColorEdge";
            this.itemColorEdge.Size = new System.Drawing.Size(161, 26);
            this.itemColorEdge.Text = "Edge";
            this.itemColorEdge.Click += new System.EventHandler(this.itemColorEdge_Click);
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
            this.ItemMove.Size = new System.Drawing.Size(210, 24);
            this.ItemMove.Text = "Move";
            // 
            // itemMoveUp
            // 
            this.itemMoveUp.Name = "itemMoveUp";
            this.itemMoveUp.Size = new System.Drawing.Size(131, 26);
            this.itemMoveUp.Text = "Up";
            this.itemMoveUp.Click += new System.EventHandler(this.itemMoveUp_Click);
            // 
            // itemMoveDown
            // 
            this.itemMoveDown.Name = "itemMoveDown";
            this.itemMoveDown.Size = new System.Drawing.Size(131, 26);
            this.itemMoveDown.Text = "Down";
            this.itemMoveDown.Click += new System.EventHandler(this.itemMoveDown_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(128, 6);
            // 
            // itemMoveTop
            // 
            this.itemMoveTop.Name = "itemMoveTop";
            this.itemMoveTop.Size = new System.Drawing.Size(131, 26);
            this.itemMoveTop.Text = "Top";
            this.itemMoveTop.Click += new System.EventHandler(this.itemMoveTop_Click);
            // 
            // itemMoveBot
            // 
            this.itemMoveBot.Name = "itemMoveBot";
            this.itemMoveBot.Size = new System.Drawing.Size(131, 26);
            this.itemMoveBot.Text = "Bot";
            this.itemMoveBot.Click += new System.EventHandler(this.itemMoveBot_Click);
            // 
            // itemFillType
            // 
            this.itemFillType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comboFillType});
            this.itemFillType.Name = "itemFillType";
            this.itemFillType.Size = new System.Drawing.Size(210, 24);
            this.itemFillType.Text = "Type";
            // 
            // comboFillType
            // 
            this.comboFillType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFillType.Name = "comboFillType";
            this.comboFillType.Size = new System.Drawing.Size(121, 28);
            this.comboFillType.SelectedIndexChanged += new System.EventHandler(this.comboFillType_SelectedIndexChanged);
            // 
            // itemDelete
            // 
            this.itemDelete.Name = "itemDelete";
            this.itemDelete.Size = new System.Drawing.Size(210, 24);
            this.itemDelete.Text = "Delete";
            this.itemDelete.Click += new System.EventHandler(this.itemDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(207, 6);
            // 
            // itemAddShapeObject
            // 
            this.itemAddShapeObject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemAddRectangleObject,
            this.itemAddEllipseObject,
            this.itemAddLineObject});
            this.itemAddShapeObject.Name = "itemAddShapeObject";
            this.itemAddShapeObject.Size = new System.Drawing.Size(210, 24);
            this.itemAddShapeObject.Text = "Add Shape";
            // 
            // itemAddRectangleObject
            // 
            this.itemAddRectangleObject.Name = "itemAddRectangleObject";
            this.itemAddRectangleObject.Size = new System.Drawing.Size(224, 26);
            this.itemAddRectangleObject.Text = "Rectangle";
            this.itemAddRectangleObject.Click += new System.EventHandler(this.itemAddRectangle_Click);
            // 
            // itemAddEllipseObject
            // 
            this.itemAddEllipseObject.Name = "itemAddEllipseObject";
            this.itemAddEllipseObject.Size = new System.Drawing.Size(224, 26);
            this.itemAddEllipseObject.Text = "Ellipse";
            this.itemAddEllipseObject.Click += new System.EventHandler(this.itemAddEllipse_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(207, 6);
            // 
            // itemBackground
            // 
            this.itemBackground.Name = "itemBackground";
            this.itemBackground.Size = new System.Drawing.Size(210, 24);
            this.itemBackground.Text = "Background";
            this.itemBackground.Click += new System.EventHandler(this.itemBackground_Click);
            // 
            // ContextCanvas
            // 
            this.ContextCanvas.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ContextCanvas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemAddShapeCanvas,
            this.toolStripSeparator3,
            this.itemBackgroundC});
            this.ContextCanvas.Name = "ContextObject";
            this.ContextCanvas.Size = new System.Drawing.Size(158, 58);
            // 
            // itemAddShapeCanvas
            // 
            this.itemAddShapeCanvas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemAddRectangle,
            this.itemAddEllipse,
            this.itemAddLine});
            this.itemAddShapeCanvas.Name = "itemAddShapeCanvas";
            this.itemAddShapeCanvas.Size = new System.Drawing.Size(157, 24);
            this.itemAddShapeCanvas.Text = "Add Shape";
            // 
            // itemAddRectangle
            // 
            this.itemAddRectangle.Name = "itemAddRectangle";
            this.itemAddRectangle.Size = new System.Drawing.Size(224, 26);
            this.itemAddRectangle.Text = "Rectangle";
            this.itemAddRectangle.Click += new System.EventHandler(this.itemAddRectangle_Click);
            // 
            // itemAddEllipse
            // 
            this.itemAddEllipse.Name = "itemAddEllipse";
            this.itemAddEllipse.Size = new System.Drawing.Size(224, 26);
            this.itemAddEllipse.Text = "Ellipse";
            this.itemAddEllipse.Click += new System.EventHandler(this.itemAddEllipse_Click);
            // 
            // itemAddLine
            // 
            this.itemAddLine.Name = "itemAddLine";
            this.itemAddLine.Size = new System.Drawing.Size(224, 26);
            this.itemAddLine.Text = "Line";
            this.itemAddLine.Click += new System.EventHandler(this.itemAddLine_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(154, 6);
            // 
            // itemBackgroundC
            // 
            this.itemBackgroundC.Name = "itemBackgroundC";
            this.itemBackgroundC.Size = new System.Drawing.Size(157, 24);
            this.itemBackgroundC.Text = "Background";
            this.itemBackgroundC.Click += new System.EventHandler(this.itemBackground_Click);
            // 
            // itemAddLineObject
            // 
            this.itemAddLineObject.Name = "itemAddLineObject";
            this.itemAddLineObject.Size = new System.Drawing.Size(224, 26);
            this.itemAddLineObject.Text = "Line";
            this.itemAddLineObject.Click += new System.EventHandler(this.itemAddLine_Click);
            // 
            // Canvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1901, 1033);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Canvas";
            this.Text = "Vector Editor";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.canvas_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseUp);
            this.ContextObject.ResumeLayout(false);
            this.ContextCanvas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip ContextObject;
        private System.Windows.Forms.ToolStripMenuItem ItemMove;
        private System.Windows.Forms.ToolStripMenuItem itemColor;
        private System.Windows.Forms.ToolStripMenuItem itemMoveUp;
        private System.Windows.Forms.ToolStripMenuItem itemMoveDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem itemMoveTop;
        private System.Windows.Forms.ToolStripMenuItem itemMoveBot;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip ContextCanvas;
        private System.Windows.Forms.ToolStripMenuItem itemBackground;
        private System.Windows.Forms.ToolStripMenuItem itemBackgroundC;
        private System.Windows.Forms.ToolStripMenuItem itemInfo;
        private System.Windows.Forms.ToolStripMenuItem itemDelete;
        private System.Windows.Forms.ToolStripMenuItem itemColorMain;
        private System.Windows.Forms.ToolStripMenuItem itemColorSecondary;
        private System.Windows.Forms.ToolStripMenuItem itemColorEdge;
        private System.Windows.Forms.ToolStripMenuItem itemFillType;
        private System.Windows.Forms.ToolStripComboBox comboFillType;
        private System.Windows.Forms.ToolStripMenuItem itemAddShapeCanvas;
        private System.Windows.Forms.ToolStripMenuItem itemAddRectangle;
        private System.Windows.Forms.ToolStripMenuItem itemAddEllipse;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem itemAddShapeObject;
        private System.Windows.Forms.ToolStripMenuItem itemAddRectangleObject;
        private System.Windows.Forms.ToolStripMenuItem itemAddEllipseObject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem itemAddLine;
        private System.Windows.Forms.ToolStripMenuItem itemAddLineObject;
    }
}

