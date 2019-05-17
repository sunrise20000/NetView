namespace DragTest
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.treeView_ProductInfo = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel_Product = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tSMItem_BusMedel_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMItem_BusModel_Property = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tSMItem_SubModel_Property = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMItem_SubModel_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.Controls.Add(this.treeView_ProductInfo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1312, 649);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // treeView_ProductInfo
            // 
            this.treeView_ProductInfo.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.treeView_ProductInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_ProductInfo.Location = new System.Drawing.Point(3, 3);
            this.treeView_ProductInfo.Name = "treeView_ProductInfo";
            this.treeView_ProductInfo.Size = new System.Drawing.Size(244, 643);
            this.treeView_ProductInfo.TabIndex = 0;
            this.treeView_ProductInfo.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView_ProductInfo_ItemDrag);
            this.treeView_ProductInfo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView_ProductInfo_MouseClick);
            this.treeView_ProductInfo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeView_ProductInfo_MouseDoubleClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(253, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(806, 643);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel_Product);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(798, 614);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel_Product
            // 
            this.panel_Product.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel_Product.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Product.Location = new System.Drawing.Point(3, 3);
            this.panel_Product.Name = "panel_Product";
            this.panel_Product.Size = new System.Drawing.Size(792, 608);
            this.panel_Product.TabIndex = 1;
            this.panel_Product.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel_Product_DragDrop);
            this.panel_Product.DragEnter += new System.Windows.Forms.DragEventHandler(this.panel_Product_DragEnter);
            this.panel_Product.DragOver += new System.Windows.Forms.DragEventHandler(this.panel_Product_DragOver);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(798, 614);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1065, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSMItem_BusMedel_Add,
            this.tSMItem_BusModel_Property});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 52);
            // 
            // tSMItem_BusMedel_Add
            // 
            this.tSMItem_BusMedel_Add.Name = "tSMItem_BusMedel_Add";
            this.tSMItem_BusMedel_Add.Size = new System.Drawing.Size(108, 24);
            this.tSMItem_BusMedel_Add.Text = "添加";
            this.tSMItem_BusMedel_Add.Click += new System.EventHandler(this.tSMItem_BusMedel_Add_Click);
            // 
            // tSMItem_BusModel_Property
            // 
            this.tSMItem_BusModel_Property.Name = "tSMItem_BusModel_Property";
            this.tSMItem_BusModel_Property.Size = new System.Drawing.Size(108, 24);
            this.tSMItem_BusModel_Property.Text = "属性";
            this.tSMItem_BusModel_Property.Click += new System.EventHandler(this.tSMItem_BusModel_Property_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSMItem_SubModel_Property,
            this.tSMItem_SubModel_Delete});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(211, 80);
            // 
            // tSMItem_SubModel_Property
            // 
            this.tSMItem_SubModel_Property.Name = "tSMItem_SubModel_Property";
            this.tSMItem_SubModel_Property.Size = new System.Drawing.Size(210, 24);
            this.tSMItem_SubModel_Property.Text = "属性";
            this.tSMItem_SubModel_Property.Click += new System.EventHandler(this.tSMItem_SubModel_Property_Click);
            // 
            // tSMItem_SubModel_Delete
            // 
            this.tSMItem_SubModel_Delete.Name = "tSMItem_SubModel_Delete";
            this.tSMItem_SubModel_Delete.Size = new System.Drawing.Size(210, 24);
            this.tSMItem_SubModel_Delete.Text = "删除";
            this.tSMItem_SubModel_Delete.Click += new System.EventHandler(this.tSMItem_SubModel_Delete_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1312, 649);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TreeView treeView_ProductInfo;
        private System.Windows.Forms.Panel panel_Product;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tSMItem_BusMedel_Add;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem tSMItem_BusModel_Property;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tSMItem_SubModel_Property;
        private System.Windows.Forms.ToolStripMenuItem tSMItem_SubModel_Delete;
    }
}

