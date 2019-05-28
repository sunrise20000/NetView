using NetView.Config;
using NetView.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubBusContrainer;
using TreeviewContrainer;
using NetView.Class;
using System.IO;
using System.Windows.Forms.Integration;
using NetView.View;
using DevExpress.XtraBars.Docking;
using ControllerLib;
using ControllerLib.Ethercat;
using DevExpress.XtraBars;
using NetView.Definations;
using EC_ControlLib.BusConfigModle;
using ControlTest;

namespace NetView
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        AvilableDeviceModel[] DeviceCfg = null;
        string FileOpenPath = @"C:\";
      
        ProductContrainer MiddleControl = null;
        treeviewContrainer LeftControl = null;
        DataTable DTVarMonitor = new DataTable();

        BusFileMgBase BusFileMgr = new EthercatFileMgr();
        ControllerBase BusController = new EC_Controller();
        BusConfigBase BusCfgBase = new BusConfig_EtherCAT();

        ProjectController ProjController = new ProjectController();

        const string FILE_DEMO_XML_FILE = @"Template\Demo.xml";

        public Form1()
        {
            InitializeComponent();
            LoadCfg();
            InitCtrl();
        }
        private void LoadCfg()
        {
            try
            {
                Config.ConfigMgr.Instance.LoadConfig();
                DeviceCfg = ConfigMgr.Instance.DeviceCfgEntry.Device;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
        }
        private void InitCtrl()
        {
            ImageList imgList = new ImageList();

            imgList.Images.Add(new Bitmap(@"images/Category.png"));
            imgList.Images.Add(new Bitmap(@"images/Bus.png"));
            imgList.Images.Add(new Bitmap(@"images/SlaveMaster.png"));
            imgList.Images.Add(new Bitmap(@"images/Company.png"));
            imgList.Images.Add(new Bitmap(@"images/Device.png"));

            treeViewDevice.ImageList = imgList;

            var BustypeList = new List<string>();
            for (int i = 0; i < DeviceCfg.Length; i++)
                BustypeList.Add(DeviceCfg[i].BusType);

            var RootNode = new TreeNode("Device");
            RootNode.ImageIndex = 0;
            foreach (var BusType in BustypeList.Distinct())
            {
                var BusRelationshipList = new List<string>();
                var BusTypeNode = new TreeNode(BusType);
                BusTypeNode.ImageIndex = 1;
                var DevBusType = DeviceCfg.Where(d => d.BusType.Equals(BusType));
                for (int i = 0; i < DevBusType.Count(); i++)
                    BusRelationshipList.Add(DevBusType.ElementAt(i).Category);
                foreach (var Relationship in BusRelationshipList.Distinct())
                {
                    var DeviceNameList = new List<string>();
                    var BusRelationshipNode = new TreeNode(Relationship);
                    BusRelationshipNode.ImageIndex = 2;
                    var DevBusRelationship = DevBusType.Where(d => d.Category.Equals(Relationship));
                    for (int i = 0; i < DevBusRelationship.Count(); i++)
                        DeviceNameList.Add(DevBusRelationship.ElementAt(i).DeviceName);
                    foreach (var Dev in DeviceNameList.Distinct())
                    {
                        var DeviceNameNode = new TreeNode(Dev);
                        DeviceNameNode.ImageIndex = 4;
                        BusRelationshipNode.Nodes.Add(DeviceNameNode);
                    }
                    BusTypeNode.Nodes.Add(BusRelationshipNode);
                }
                RootNode.Nodes.Add(BusTypeNode);
            }
            treeViewDevice.Nodes.Add(RootNode);
            treeViewDevice.ExpandAll();
            treeViewDevice.ItemDrag += TreeViewDevice_ItemDrag;
            treeViewDevice.NodeMouseDoubleClick += TreeViewDevice_NodeMouseDoubleClick;
            this.barSubIteExportFile.Popup += BarSubIteExportFile_Popup;


            //添加中间控件
            MiddleControl = new ProductContrainer();
            this.dockPanelMiddle.Controls.Add(MiddleControl);
            MiddleControl.Dock = DockStyle.Fill;

            //添加侧面控件
            LeftControl = new treeviewContrainer();
            this.dockPanelLeft.Controls.Add(LeftControl);
            LeftControl.Dock = DockStyle.Fill;
            LeftControl.ProductContrainer = MiddleControl;

  
            //VarMonitor
            UC_VarMonitor ucMonitor = new UC_VarMonitor();
            elementHost2.Child = ucMonitor;
            ucMonitor.OnStartMonitorEventHandler += UcMonitor_OnStartMonitorEventHandler;
            ucMonitor.OnStopMonitorEventHandler += UcMonitor_OnStopMonitorEventHandler;
            ucMonitor.OnModifyValueEventHandler += UcMonitor_OnModifyValueEventHandler;
            var VarCollect = ucMonitor.VarCollect;


            //for (int i = 0; i < 3; i++)
            //{
            VarCollect.Add(new MonitorVarModel() { IoType = Definations.EnumModuleIOType.IN });
            VarCollect.Add(new MonitorVarModel() { IoType = Definations.EnumModuleIOType.IN });
            VarCollect.Add(new MonitorVarModel() { IoType = Definations.EnumModuleIOType.IN });
            //    VarCollect.Add(new MonitorVarModel() { IoType = Definations.EnumModuleIOType.OUT });
            //    VarCollect.Add(new MonitorVarModel() { IoType = Definations.EnumModuleIOType.OUT });
            //    VarCollect.Add(new MonitorVarModel() { IoType = Definations.EnumModuleIOType.IN });
            //    VarCollect.Add(new MonitorVarModel() { IoType = Definations.EnumModuleIOType.OUT });
            //    VarCollect.Add(new MonitorVarModel() { IoType = Definations.EnumModuleIOType.OUT });
            //    VarCollect.Add(new MonitorVarModel() { IoType = Definations.EnumModuleIOType.IN });
            //}

            this.elementHost1.BackColorTransparent = true;
            this.elementHost2.BackColorTransparent = true;
            dockManager1.ActivePanel = dockPanelMiddle;
            
            //dockManager1.RemovePanel(dockPanelVarMonitor);

            ProjController.BusCfg =BusCfgBase ;
            ProjController.BusFileMgr = BusFileMgr;
        }

        private void BarSubIteExportFile_Popup(object sender, EventArgs e)
        {
            var C = (sender as BarSubItem).LinksPersistInfo;
            foreach (LinkPersistInfo it in C)
                it.Item.Enabled = it.Item.Caption.Contains(BusCfgBase.ShortName);
            
        }

        private void TreeViewDevice_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
        }

        private void TreeViewDevice_ItemDrag(object sender, ItemDragEventArgs e)
        {
            string ProductName = (e.Item as TreeNode).Text;
            var list = ProductName.Split(' ');
            var nLen = list.Length;
            treeViewDevice.DoDragDrop(list[0].Replace("-", "_"), DragDropEffects.Copy);  
        }
  
        private void barButtonItemOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProjController.OpenProject();
            LeftControl.ReplaceNewList(ProjController.BusName, ProjController.SubBusNameWithIndexList);
        }

        private void barButtonItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProjController.ModuleConfigList.Add(new EC_ControlLib.Ethercat.ModuleConfigModle.ModuleConfig_HL2002() { GlobalIndex = 1, LocalIndex = 1 });
            ProjController.ModuleConfigList.Add(new EC_ControlLib.Ethercat.ModuleConfigModle.ModuleConfig_HL2003() { GlobalIndex = 2, LocalIndex = 1 });
            ProjController.ModuleConfigList.Add(new EC_ControlLib.Ethercat.ModuleConfigModle.ModuleConfig_HL2002() { GlobalIndex = 3, LocalIndex = 2 });
            ProjController.ModuleConfigList.Add(new EC_ControlLib.Ethercat.ModuleConfigModle.ModuleConfig_HL2003() { GlobalIndex = 4, LocalIndex = 2 });

            var StrBusName =;
  

            ProjController.SaveProject();
        }

        private void barButtonItemCut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItemCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItemPaste_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItemSetting_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Window_ComSetting window = new Window_ComSetting();
            window.ShowDialog();
        }

        private void barButtonItemConnect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (!BusController.IsConnected)
                    BusController.Open("1");
                else
                    BusController.CLose();
            }            
            catch (Exception ex)
            {
                MessageBox.Show($"Error when connect to controller:{ex.Message}","Error",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
            }
}

        private void barButtonItemUpload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                BusController.GetModuleList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error when upload to controller:{ex.Message}","Error",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
            }
        }

        private void barButtonItemDownLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            { 
            BusController.SendModuleList(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error when download to controller:{ex.Message}", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void barButtonItemMonitor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dockPanelVarMonitor.Visibility = dockPanelVarMonitor.Visibility == DockVisibility.Visible ? DockVisibility.Hidden : DockVisibility.Visible;
            if (dockPanelVarMonitor.Visibility == DockVisibility.Visible)
                dockManager1.ActivePanel = dockPanelVarMonitor;
                      
        }

        private void MenuSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItemArrangWindow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dockPanelLeft.Show();
            this.dockPanelMiddle.Show();
            this.dockPanelRight.Show();
            this.dockPanelDown.Show();
        }

        #region VarMonitor

        private void UcMonitor_OnModifyValueEventHandler(object sender, EventArgs e)
        {
            
        }

        private void UcMonitor_OnStopMonitorEventHandler(object sender, EventArgs e)
        {
           
        }

        private void UcMonitor_OnStartMonitorEventHandler(object sender, EventArgs e)
        {
           
        }


        #endregion

        private void barButtonItemNewProject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var BusClassName = $"BusConfig_{e.Item.Caption.Replace("-","_")}";
            MiddleControl.BusName = e.Item.Caption;
            //BusConfigBase=new 
        }

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemExportFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            var strFilter = $"{BusFileMgr.ExtString} File(*.{BusFileMgr.ExtString})|*.{BusFileMgr.ExtString}";
            sfd.Filter = strFilter;
            sfd.FilterIndex = 2;
            sfd.RestoreDirectory = true;
            sfd.InitialDirectory = FileOpenPath;
            sfd.FileName = $"Untitled.{BusFileMgr.ExtString}";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileOpenPath = sfd.FileName;
                var NameModelList = new List<ModuleNameModel>();
                foreach (var it in LeftControl.PureNameList)
                    NameModelList.Add(new ModuleNameModel()
                    {
                        PureName = it,
                    });
                BusFileMgr.SaveFile(NameModelList, FileOpenPath);
            }
        }

        private void ShowMessage(EnumMsgType MsgType,string Msg)
        {
            this.uC_Output1.MsgCollect.Add(new MessageModel(MsgType, Msg));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //dockPanelVarMonitor.Visibility = DockVisibility.Hidden;
        }
    }
}
