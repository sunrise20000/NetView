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
			this.tSMItem_BusModel_Property = new System.Windows.Forms.ToolStripMenuItem();
			this.refrushNameMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tSMItem_SubModel_Property = new System.Windows.Forms.ToolStripMenuItem();
			this.tSMItem_SubModel_Delete = new System.Windows.Forms.ToolStripMenuItem();
			this.LeftControl_CTX_Menu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.Menu_ModbusRTU = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_Profibus_DP = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_PROFIBUS_IO = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_ModbusTCP = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_EatherCat = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_Canopen = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_DeviceNet = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			this.contextMenuStrip2.SuspendLayout();
			this.LeftControl_CTX_Menu.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeView_ProductInfo
			// 
			this.treeView_ProductInfo.AllowDrop = true;
			this.treeView_ProductInfo.ContextMenuStrip = this.LeftControl_CTX_Menu;
			this.treeView_ProductInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView_ProductInfo.Location = new System.Drawing.Point(0, 0);
			this.treeView_ProductInfo.Margin = new System.Windows.Forms.Padding(2);
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
			this.contextMenuStrip1.Size = new System.Drawing.Size(156, 70);
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
			this.tSMItem_BusMedel_Add.Size = new System.Drawing.Size(155, 22);
			this.tSMItem_BusMedel_Add.Text = "Add";
			// 
			// hL1001ToolStripMenuItem
			// 
			this.hL1001ToolStripMenuItem.Name = "hL1001ToolStripMenuItem";
			this.hL1001ToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.hL1001ToolStripMenuItem.Tag = "";
			this.hL1001ToolStripMenuItem.Text = "HL1001";
			this.hL1001ToolStripMenuItem.Click += new System.EventHandler(this.tSMItem_BusMedel_Add_Click);
			// 
			// hL2001ToolStripMenuItem
			// 
			this.hL2001ToolStripMenuItem.Name = "hL2001ToolStripMenuItem";
			this.hL2001ToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.hL2001ToolStripMenuItem.Text = "HL2001";
			this.hL2001ToolStripMenuItem.Click += new System.EventHandler(this.tSMItem_BusMedel_Add_Click);
			// 
			// hL2002ToolStripMenuItem
			// 
			this.hL2002ToolStripMenuItem.Name = "hL2002ToolStripMenuItem";
			this.hL2002ToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.hL2002ToolStripMenuItem.Text = "HL2002";
			this.hL2002ToolStripMenuItem.Click += new System.EventHandler(this.tSMItem_BusMedel_Add_Click);
			// 
			// hL2003ToolStripMenuItem
			// 
			this.hL2003ToolStripMenuItem.Name = "hL2003ToolStripMenuItem";
			this.hL2003ToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.hL2003ToolStripMenuItem.Text = "HL2003";
			this.hL2003ToolStripMenuItem.Click += new System.EventHandler(this.tSMItem_BusMedel_Add_Click);
			// 
			// hL3001ToolStripMenuItem
			// 
			this.hL3001ToolStripMenuItem.Name = "hL3001ToolStripMenuItem";
			this.hL3001ToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.hL3001ToolStripMenuItem.Text = "HL3001";
			this.hL3001ToolStripMenuItem.Click += new System.EventHandler(this.tSMItem_BusMedel_Add_Click);
			// 
			// hL3002ToolStripMenuItem
			// 
			this.hL3002ToolStripMenuItem.Name = "hL3002ToolStripMenuItem";
			this.hL3002ToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.hL3002ToolStripMenuItem.Text = "HL3002";
			this.hL3002ToolStripMenuItem.Click += new System.EventHandler(this.tSMItem_BusMedel_Add_Click);
			// 
			// hL4001ToolStripMenuItem
			// 
			this.hL4001ToolStripMenuItem.Name = "hL4001ToolStripMenuItem";
			this.hL4001ToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.hL4001ToolStripMenuItem.Text = "HL4001";
			this.hL4001ToolStripMenuItem.Click += new System.EventHandler(this.tSMItem_BusMedel_Add_Click);
			// 
			// hL4002ToolStripMenuItem
			// 
			this.hL4002ToolStripMenuItem.Name = "hL4002ToolStripMenuItem";
			this.hL4002ToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.hL4002ToolStripMenuItem.Text = "HL4002";
			this.hL4002ToolStripMenuItem.Click += new System.EventHandler(this.tSMItem_BusMedel_Add_Click);
			// 
			// hL5001ToolStripMenuItem
			// 
			this.hL5001ToolStripMenuItem.Name = "hL5001ToolStripMenuItem";
			this.hL5001ToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.hL5001ToolStripMenuItem.Text = "HL5001";
			this.hL5001ToolStripMenuItem.Click += new System.EventHandler(this.tSMItem_BusMedel_Add_Click);
			// 
			// hL5002ToolStripMenuItem
			// 
			this.hL5002ToolStripMenuItem.Name = "hL5002ToolStripMenuItem";
			this.hL5002ToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.hL5002ToolStripMenuItem.Text = "HL5002";
			this.hL5002ToolStripMenuItem.Click += new System.EventHandler(this.tSMItem_BusMedel_Add_Click);
			// 
			// tSMItem_BusModel_Property
			// 
			this.tSMItem_BusModel_Property.Name = "tSMItem_BusModel_Property";
			this.tSMItem_BusModel_Property.Size = new System.Drawing.Size(155, 22);
			this.tSMItem_BusModel_Property.Text = "Property";
			this.tSMItem_BusModel_Property.Click += new System.EventHandler(this.tSMItem_BusModel_Property_Click);
			// 
			// refrushNameMenu
			// 
			this.refrushNameMenu.Name = "refrushNameMenu";
			this.refrushNameMenu.Size = new System.Drawing.Size(155, 22);
			this.refrushNameMenu.Text = "RefrushName";
			this.refrushNameMenu.Click += new System.EventHandler(this.refrushNameMenu_Click);
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
			this.tSMItem_SubModel_Property.Size = new System.Drawing.Size(126, 22);
			this.tSMItem_SubModel_Property.Text = "Property";
			this.tSMItem_SubModel_Property.Click += new System.EventHandler(this.tSMItem_SubModel_Property_Click);
			// 
			// tSMItem_SubModel_Delete
			// 
			this.tSMItem_SubModel_Delete.Name = "tSMItem_SubModel_Delete";
			this.tSMItem_SubModel_Delete.Size = new System.Drawing.Size(126, 22);
			this.tSMItem_SubModel_Delete.Text = "Delete";
			this.tSMItem_SubModel_Delete.Click += new System.EventHandler(this.tSMItem_SubModel_Delete_Click);
			// 
			// LeftControl_CTX_Menu
			// 
			this.LeftControl_CTX_Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_ModbusRTU,
            this.Menu_Profibus_DP,
            this.Menu_PROFIBUS_IO,
            this.Menu_ModbusTCP,
            this.Menu_EatherCat,
            this.Menu_Canopen,
            this.Menu_DeviceNet});
			this.LeftControl_CTX_Menu.Name = "LeftControl_CTX_Menu";
			this.LeftControl_CTX_Menu.Size = new System.Drawing.Size(181, 180);
			// 
			// Menu_ModbusRTU
			// 
			this.Menu_ModbusRTU.Name = "Menu_ModbusRTU";
			this.Menu_ModbusRTU.Size = new System.Drawing.Size(180, 22);
			this.Menu_ModbusRTU.Text = "ModbusRTU";
			this.Menu_ModbusRTU.Click += new System.EventHandler(this.Menu_ModbusRTU_Click);
			// 
			// Menu_Profibus_DP
			// 
			this.Menu_Profibus_DP.Name = "Menu_Profibus_DP";
			this.Menu_Profibus_DP.Size = new System.Drawing.Size(180, 22);
			this.Menu_Profibus_DP.Text = "PROFIBUS-DP";
			this.Menu_Profibus_DP.Click += new System.EventHandler(this.Menu_Profibus_DP_Click);
			// 
			// Menu_PROFIBUS_IO
			// 
			this.Menu_PROFIBUS_IO.Name = "Menu_PROFIBUS_IO";
			this.Menu_PROFIBUS_IO.Size = new System.Drawing.Size(180, 22);
			this.Menu_PROFIBUS_IO.Text = "PROFIBUS-IO";
			this.Menu_PROFIBUS_IO.Click += new System.EventHandler(this.Menu_PROFIBUS_IO_Click);
			// 
			// Menu_ModbusTCP
			// 
			this.Menu_ModbusTCP.Name = "Menu_ModbusTCP";
			this.Menu_ModbusTCP.Size = new System.Drawing.Size(180, 22);
			this.Menu_ModbusTCP.Text = "ModbusTCP";
			this.Menu_ModbusTCP.Click += new System.EventHandler(this.Menu_ModbusTCP_Click);
			// 
			// Menu_EatherCat
			// 
			this.Menu_EatherCat.Name = "Menu_EatherCat";
			this.Menu_EatherCat.Size = new System.Drawing.Size(180, 22);
			this.Menu_EatherCat.Text = "EtherCAT";
			this.Menu_EatherCat.Click += new System.EventHandler(this.Menu_EatherCat_Click);
			// 
			// Menu_Canopen
			// 
			this.Menu_Canopen.Name = "Menu_Canopen";
			this.Menu_Canopen.Size = new System.Drawing.Size(180, 22);
			this.Menu_Canopen.Text = "CANopen";
			this.Menu_Canopen.Click += new System.EventHandler(this.Menu_Canopen_Click);
			// 
			// Menu_DeviceNet
			// 
			this.Menu_DeviceNet.Name = "Menu_DeviceNet";
			this.Menu_DeviceNet.Size = new System.Drawing.Size(180, 22);
			this.Menu_DeviceNet.Text = "DeviceNet";
			this.Menu_DeviceNet.Click += new System.EventHandler(this.Menu_DeviceNet_Click);
			// 
			// treeviewContrainer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.treeView_ProductInfo);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "treeviewContrainer";
			this.Size = new System.Drawing.Size(169, 333);
			this.contextMenuStrip1.ResumeLayout(false);
			this.contextMenuStrip2.ResumeLayout(false);
			this.LeftControl_CTX_Menu.ResumeLayout(false);
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
		private System.Windows.Forms.ContextMenuStrip LeftControl_CTX_Menu;
		private System.Windows.Forms.ToolStripMenuItem Menu_ModbusRTU;
		private System.Windows.Forms.ToolStripMenuItem Menu_Profibus_DP;
		private System.Windows.Forms.ToolStripMenuItem Menu_PROFIBUS_IO;
		private System.Windows.Forms.ToolStripMenuItem Menu_ModbusTCP;
		private System.Windows.Forms.ToolStripMenuItem Menu_EatherCat;
		private System.Windows.Forms.ToolStripMenuItem Menu_Canopen;
		private System.Windows.Forms.ToolStripMenuItem Menu_DeviceNet;
	}
}
