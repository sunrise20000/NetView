﻿using NetView.Config;
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

namespace NetView
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        AvilableDeviceModel[] DeviceCfg = null;
        string FileOpenPath = @"C:\";
        EthercatSettingMgr EthercatMgr = new EthercatSettingMgr();


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
            ProductContrainer MiddleControl = new ProductContrainer("Ethercat");
            MiddleControl.Dock = DockStyle.Fill;
            this.dockPanelMiddle.Controls.Add(MiddleControl);

            //添加侧面控件
            treeviewContrainer LeftControl = new treeviewContrainer();
            LeftControl.Dock = DockStyle.Fill;
            this.dockPanelLeft.Controls.Add(LeftControl);
            LeftControl.ProductContrainer = MiddleControl;
            

        }

        private void TreeViewDevice_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
        }
        private void TreeViewDevice_ItemDrag(object sender, ItemDragEventArgs e)
        {
            string ProductName= (e.Item as TreeNode).Text;
            var list=ProductName.Split(' ');
            var nLen = list.Length;
            if (nLen > 1)
            {
                treeViewDevice.DoDragDrop(list[0], DragDropEffects.Copy);
            }
        }

        /// <summary>
        /// New
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EthercatMgr.LoadXmlFile(@"Template\Demo.xml");
        }

        private void barButtonItemOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            var strFilter = $"{EthercatSettingMgr.ExtString} File(*.{EthercatSettingMgr.ExtString})|*.{EthercatSettingMgr.ExtString}";
            ofd.Filter = strFilter;
            ofd.ValidateNames = true; // 验证用户输入是否是一个有效的Windows文件名
            ofd.CheckFileExists = true; //验证路径的有效性
            ofd.CheckPathExists = true;//验证路径的有效性
            ofd.InitialDirectory = FileOpenPath;
            if (ofd.ShowDialog() == DialogResult.OK) //用户点击确认按钮，发送确认消息
            {
                FileOpenPath = ofd.FileName;//获取在文件对话框中选定的路径或者字符串
                EthercatMgr.LoadXmlFile(FileOpenPath);
                var List = EthercatMgr.GetDeviceList();
                foreach (var it in List)
                    Console.WriteLine(it.Name);
            }
        }

        private void barButtonItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            var strFilter = $"{EthercatSettingMgr.ExtString} File(*.{EthercatSettingMgr.ExtString})|*.{EthercatSettingMgr.ExtString}";
            sfd.Filter = strFilter;
            sfd.FilterIndex = 2;
            sfd.RestoreDirectory = true;
            sfd.InitialDirectory = FileOpenPath;
            sfd.FileName = "Untitled.xml";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileOpenPath = sfd.FileName;
                EthercatMgr.SaveFile(new List<Model.ModuleInfo.ModuleInfoBase>(), FileOpenPath);
            }
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

        }

        private void barButtonItemConnect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItemCheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItemStart_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItemStop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItemUpload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItemDownLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItemMonitor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

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
    }
}
