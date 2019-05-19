namespace TreeviewContrainer
{
    partial class treeviewContrainer
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

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.treeView_ProductInfo = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tSMItem_BusMedel_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMItem_BusModel_Property = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tSMItem_SubModel_Property = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMItem_SubModel_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.refrushNameMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.hL1001ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hL2001ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hL2002ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hL2003ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hL3001ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hL3002ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hL4001ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hL4002ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hL5001ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hL5002ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView_ProductInfo
            // 
            this.treeView_ProductInfo.AllowDrop = true;
            this.treeView_ProductInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_ProductInfo.Location = new System.Drawing.Point(0, 0);
            this.treeView_ProductInfo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.treeView_ProductInfo.Name = "treeView_ProductInfo";
            this.treeView_ProductInfo.Size = new System.Drawing.Size(169, 333);
            this.treeView_ProductInfo.TabIndex = 0;
            this.treeView_ProductInfo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView_ProductInfo_MouseClick);
            this.treeView_ProductInfo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeView_ProductInfo_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSMItem_BusMedel_Add,
            this.tSMItem_BusModel_Property,
            this.refrushNameMenu});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 92);
            // 
            // tSMItem_BusMedel_Add
            // 
            this.tSMItem_BusMedel_Add.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hL1001ToolStripMenuItem,
            this.hL2001ToolStripMenuItem,
            this.hL2002ToolStripMenuItem,
            this.hL2003ToolStripMenuItem,
            this.hL3001ToolStripMenuItem,
            this.hL3002ToolStripMenuItem,
            this.hL4001ToolStripMenuItem,
            this.hL4002ToolStripMenuItem,
            this.hL5001ToolStripMenuItem,
            this.hL5002ToolStripMenuItem});
            this.tSMItem_BusMedel_Add.Name = "tSMItem_BusMedel_Add";
            this.tSMItem_BusMedel_Add.Size = new System.Drawing.Size(180, 22);
            this.tSMItem_BusMedel_Add.Text = "Add";
            // 
            // tSMItem_BusModel_Property
            // 
            this.tSMItem_BusModel_Property.Name = "tSMItem_BusModel_Property";
            this.tSMItem_BusModel_Property.Size = new System.Drawing.Size(180, 22);
            this.tSMItem_BusModel_Property.Text = "Property";
            this.tSMItem_BusModel_Property.Click += new System.EventHandler(this.tSMItem_BusModel_Property_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSMItem_SubModel_Property,
            this.tSMItem_SubModel_Delete});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(127, 48);
            // 
            // tSMItem_SubModel_Property
            // 
            this.tSMItem_SubModel_Property.Name = "tSMItem_SubModel_Property";
            this.tSMItem_SubModel_Property.Size = new System.Drawing.Size(180, 22);
            this.tSMItem_SubModel_Property.Text = "Property";
            this.tSMItem_SubModel_Property.Click += new System.EventHandler(this.tSMItem_SubModel_Property_Click);
            // 
            // tSMItem_SubModel_Delete
            // 
            this.tSMItem_SubModel_Delete.Name = "tSMItem_SubModel_Delete";
            this.tSMItem_SubModel_Delete.Size = new System.Drawing.Size(180, 22);
            this.tSMItem_SubModel_Delete.Text = "Delete";
            this.tSMItem_SubModel_Delete.Click += new System.EventHandler(this.tSMItem_SubModel_Delete_Click);
            // 
            // refrushNameMenu
            // 
            this.refrushNameMenu.Name = "refrushNameMenu";
            this.refrushNameMenu.Size = new System.Drawing.Size(180, 22);
            this.refrushNameMenu.Text = "RefrushName";
            this.refrushNameMenu.Click += new System.EventHandler(this.refrushNameMenu_Click);
            // 
            // hL1001ToolStripMenuItem
            // 
            this.hL1001ToolStripMenuItem.Name = "hL1001ToolStripMenuItem";
            this.hL1001ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hL1001ToolStripMenuItem.Tag = "";
            this.hL1001ToolStripMenuItem.Text = "HL1001";
            this.hL1001ToolStripMenuItem.Click += new System.EventHandler(this.tSMItem_BusMedel_Add_Click);
            // 
            // hL2001ToolStripMenuItem
            // 
            this.hL2001ToolStripMenuItem.Name = "hL2001ToolStripMenuItem";
            this.hL2001ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hL2001ToolStripMenuItem.Text = "HL2001";
            this.hL2001ToolStripMenuItem.Click += new System.EventHandler(this.tSMItem_BusMedel_Add_Click);
            // 
            // hL2002ToolStripMenuItem
            // 
            this.hL2002ToolStripMenuItem.Name = "hL2002ToolStripMenuItem";
            this.hL2002ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hL2002ToolStripMenuItem.Text = "HL2002";
            this.hL2002ToolStripMenuItem.Click += new System.EventHandler(this.tSMItem_BusMedel_Add_Click);
            // 
            // hL2003ToolStripMenuItem
            // 
            this.hL2003ToolStripMenuItem.Name = "hL2003ToolStripMenuItem";
            this.hL2003ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hL2003ToolStripMenuItem.Text = "HL2003";
            this.hL2003ToolStripMenuItem.Click += new System.EventHandler(this.tSMItem_BusMedel_Add_Click);
            // 
            // hL3001ToolStripMenuItem
            // 
            this.hL3001ToolStripMenuItem.Name = "hL3001ToolStripMenuItem";
            this.hL3001ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hL3001ToolStripMenuItem.Text = "HL3001";
            this.hL3001ToolStripMenuItem.Click += new System.EventHandler(this.tSMItem_BusMedel_Add_Click);
            // 
            // hL3002ToolStripMenuItem
            // 
            this.hL3002ToolStripMenuItem.Name = "hL3002ToolStripMenuItem";
            this.hL3002ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hL3002ToolStripMenuItem.Text = "HL3002";
            this.hL3002ToolStripMenuItem.Click += new System.EventHandler(this.tSMItem_BusMedel_Add_Click);
            // 
            // hL4001ToolStripMenuItem
            // 
            this.hL4001ToolStripMenuItem.Name = "hL4001ToolStripMenuItem";
            this.hL4001ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hL4001ToolStripMenuItem.Text = "HL4001";
            this.hL4001ToolStripMenuItem.Click += new System.EventHandler(this.tSMItem_BusMedel_Add_Click);
            // 
            // hL4002ToolStripMenuItem
            // 
            this.hL4002ToolStripMenuItem.Name = "hL4002ToolStripMenuItem";
            this.hL4002ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hL4002ToolStripMenuItem.Text = "HL4002";
            this.hL4002ToolStripMenuItem.Click += new System.EventHandler(this.tSMItem_BusMedel_Add_Click);
            // 
            // hL5001ToolStripMenuItem
            // 
            this.hL5001ToolStripMenuItem.Name = "hL5001ToolStripMenuItem";
            this.hL5001ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hL5001ToolStripMenuItem.Text = "HL5001";
            this.hL5001ToolStripMenuItem.Click += new System.EventHandler(this.tSMItem_BusMedel_Add_Click);
            // 
            // hL5002ToolStripMenuItem
            // 
            this.hL5002ToolStripMenuItem.Name = "hL5002ToolStripMenuItem";
            this.hL5002ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hL5002ToolStripMenuItem.Text = "HL5002";
            this.hL5002ToolStripMenuItem.Click += new System.EventHandler(this.tSMItem_BusMedel_Add_Click);
            // 
            // treeviewContrainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView_ProductInfo);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "treeviewContrainer";
            this.Size = new System.Drawing.Size(169, 333);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView_ProductInfo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tSMItem_BusMedel_Add;
        private System.Windows.Forms.ToolStripMenuItem tSMItem_BusModel_Property;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tSMItem_SubModel_Property;
        private System.Windows.Forms.ToolStripMenuItem tSMItem_SubModel_Delete;
        private System.Windows.Forms.ToolStripMenuItem refrushNameMenu;
        private System.Windows.Forms.ToolStripMenuItem hL1001ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hL2001ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hL2002ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hL2003ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hL3001ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hL3002ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hL4001ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hL4002ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hL5001ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hL5002ToolStripMenuItem;
    }
}
