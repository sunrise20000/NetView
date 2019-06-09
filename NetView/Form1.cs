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
using ControllerLib.BusConfigModle;
using ControlTest;
using ControllerLib.Ethercat.ModuleConfigModle;
using System.Text.RegularExpressions;

namespace NetView
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        AvilableDeviceModel[] DeviceCfg = null;
        string FileOpenPath = @"C:\";
      
        ProductContrainer MiddleControl = null;
        treeviewContrainer LeftControl = null;
        DataTable DTVarMonitor = new DataTable();

        ControllerBase BusController = new EC_Controller();
        ProjectController ProjController = new ProjectController();

        const string FILE_DEMO_XML_FILE = @"Template\Demo.xml";
        ComportSettingModel ComSettingCfgModel = null;
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
            LeftControl.OnBusModulChanged += LeftControl_OnBusModulChanged;
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
            ProjController.BusFileMgr = new EthercatFileMgr();
        }

        /// <summary>
        /// 当总线改变的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftControl_OnBusModulChanged(object sender, SubBusContrainer.Model.ModuleAddedArgs e)
        {
            if (e.IsAdd)
            {
                var T = typeof(BusConfigBase);
                var ClassName = $"ControllerLib.BusConfigModle.BusConfig_{e.Module.Name}";
                var obj = T.Assembly.CreateInstance(ClassName) as BusConfigBase;
                ProjController.BusCfg = obj;
            }
            else
            {
                ProjController.BusCfg = null;
            }

           // e.Name
           //ProjController.BusCfg=new 
           //throw new NotImplementedException();
        }

        private void BarSubIteExportFile_Popup(object sender, EventArgs e)
        {
            if (ProjController.BusCfg != null)
            {
                var C = (sender as BarSubItem).LinksPersistInfo;
                foreach (LinkPersistInfo it in C)
                    it.Item.Enabled = it.Item.Caption.Contains(ProjController.BusCfg.ShortName);
            }
            
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
            ProjController.ModuleConfigList.Clear();

            foreach (var it in MiddleControl.Controls)
            {
                if (it is BusModel)
                {
                    //DoNothing
                }
                else if(it is SubBusModel)
                {
                    SubBusModel SB = it as SubBusModel;
                    var SubBusClassName = $"ControllerLib.Ethercat.ModuleConfigModle.ModuleConfig_{SB.ModuleType.ToString()}";
                    Type T = typeof(ModuleConfigModleBase);
                    dynamic obj = T.Assembly.CreateInstance(SubBusClassName);
                    ModuleConfigModleBase CfgBase = obj as ModuleConfigModleBase;
                    var list = SB.Mcb.ToStringList().ToArray();
                    CfgBase.FromString(list);
                    ProjController.ModuleConfigList.Add(CfgBase);
                }
            }
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
            if (ComSettingCfgModel != null)
                window.ComSetting = ComSettingCfgModel;
            window.ShowDialog();
            ComSettingCfgModel = window.ComSetting;
        }

        private void barButtonItemConnect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string RegStr = @"COM\d{1,2}";
                var IsMatched = Regex.IsMatch(ComSettingCfgModel.ComportName, RegStr);
                if (ComSettingCfgModel != null && IsMatched)
                {
                    BusController.Open(ComSettingCfgModel.ComportName);
                    if (!BusController.IsConnected)
                    {
                        BusController.Connect();
                    }
                    else
                    {
                        //BusController.CLose();
                        BusController.DisConnect();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a comport to connect controller");
                }
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
            var strFilter = $"{ProjController.BusFileMgr.ExtString} File(*.{ProjController.BusFileMgr.ExtString})|*.{ProjController.BusFileMgr.ExtString}";
            sfd.Filter = strFilter;
            sfd.FilterIndex = 2;
            sfd.RestoreDirectory = true;
            sfd.InitialDirectory = FileOpenPath;
            sfd.FileName = $"{ProjController.BusCfg.Name}.{ProjController.BusFileMgr.ExtString}";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileOpenPath = sfd.FileName;
                var NameModelList = new List<ModuleNameModel>();
                foreach (var it in LeftControl.PureNameList)
                    NameModelList.Add(new ModuleNameModel()
                    {
                        PureName = it,
                    });
                ProjController.BusFileMgr.SaveFile(NameModelList, FileOpenPath);
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
