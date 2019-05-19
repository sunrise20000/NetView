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
namespace NetView
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        AvilableDeviceModel[] DeviceCfg = null;
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


            this.uC_Output1.MsgCollect.Add(new MessageModel(Definations.EnumMsgType.Error,"Msg1"));
            this.uC_Output1.MsgCollect.Add(new MessageModel(Definations.EnumMsgType.Info, "Msg1"));
            this.uC_Output1.MsgCollect.Add(new MessageModel(Definations.EnumMsgType.Warning, "Msg1"));


            //添加中间控件
            ProductContrainer MiddleControl = new ProductContrainer();
            MiddleControl.Dock = DockStyle.Fill;
            this.dockPanelMiddle.Controls.Add(MiddleControl);

            //添加侧面控件

        }

        private void TreeViewDevice_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
        }
        private void TreeViewDevice_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //总线是否只有一个
            //一种总线的配置界面是否一致
            //界面上所有的参数对应XML里面什么内容
        }

    }
}
