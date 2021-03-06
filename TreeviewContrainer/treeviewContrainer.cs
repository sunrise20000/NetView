﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SubBusContrainer;
using ControlTest;
using ControlTest.ModuleConfigModle;
using ModelLib;

namespace TreeviewContrainer
{
    public partial class treeviewContrainer: UserControl
    {
        private ProductContrainer productContrainer;
        public event EventHandler<SubBusContrainer.Model.ModuleAddedArgs> OnBusModuleChanged;
		public event EventHandler<string> OnBusMenuAddClicked;
		public event EventHandler<string> OnBusMenuDeleteClicked;
		

		public List<string> PureNameList    
        {
            get
            {
                var NameList = new List<string>();
                foreach (TreeNode it in treeView_ProductInfo.Nodes)
                {
                    if (it.Text.Equals("Ethercat"))
                    {
                        foreach (TreeNode node in it.Nodes)
                        {
                            var L = node.Text.Split('_');
                            NameList.Add($"{L[0]}");
                        }
                        break;
                    }
                }
                return NameList;
            }
        }


        public ProductContrainer ProductContrainer
        {
            get { return productContrainer; }
            set
            {
                if (value != null)
                {
                    productContrainer = value;
                    productContrainer.OnProductChangedEvent -= ProductContrainer_OnProductChangedHandler;
                    productContrainer.OnProductChangedEvent += ProductContrainer_OnProductChangedHandler;

                }
            }
        }

        /// <summary>
        /// Middle——》Left
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductContrainer_OnProductChangedHandler(object sender, SubBusContrainer.Model.ModuleAddedArgs e)
        {
            if (e.Module is SubBusModel)
            {
                //TreeNode DesNode = null;
                foreach (TreeNode it in treeView_ProductInfo.Nodes)
                {
                    if (it.Text.Equals(productContrainer.BusName))
                    {
                        //DesNode = it;
                        Dictionary<int, string> NodeTextDic = new Dictionary<int, string>();
                        int i = 0;
                        foreach (TreeNode node in it.Nodes)
                            NodeTextDic.Add(i++, node.Text);
                        if (e.IsAdd)
                        {
                            it.Nodes.Add($"{e.Module.Name}_{e.Module.LocalIndex}", $"{e.Module.Name}_{e.Module.LocalIndex}");
                        }
                        else
                        {
                            for (int j = 0; j < NodeTextDic.Count; j++)
                            {
                                if (NodeTextDic.ElementAt(j).Value.Contains($"{e.Module.Name}_{e.Module.LocalIndex}"))
                                {
                                    it.Nodes.RemoveAt(j);
                                    NodeTextDic.Remove(NodeTextDic.ElementAt(j).Key);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
			else //if(e.Module is SubBusModel)
			{
                if (treeView_ProductInfo.Nodes.Count != 0)
                {
                    if (e.IsAdd)
                        treeView_ProductInfo.Nodes[0].Text = e.Module.Name;
                    else
                        treeView_ProductInfo.Nodes.Clear();
                }
                else
                {
                    treeView_ProductInfo.Nodes.Add(new TreeNode(e.Module.Name));
                }
                OnBusModuleChanged?.Invoke(this,e);
            }
            treeView_ProductInfo.ExpandAll();
			/*if(treeView_ProductInfo.Nodes.Count!=0)
				this.treeView_ProductInfo.ContextMenuStrip = this.contextMenuStrip1;*/
			//RenameTreeNode();
		}


		/// <summary>
		/// 重新命名模块，需要重新排序，别的都不需要变，Pending
		/// </summary>
        private void RenameTreeNode()
        {
            TreeNode DesNode = null;
            foreach (TreeNode it in treeView_ProductInfo.Nodes)
            {
                if (it.Text.Equals(productContrainer.BusName))
                {
                    DesNode = it;
					List<ModifyNameInfoModel> RenameInfoList = new List<ModifyNameInfoModel>();
                    List<string> NodeTextList = new List<string>();
                    int i = 0;
                    foreach (TreeNode node in it.Nodes)
                    {
						NodeTextList.Add(node.Text);
						var moduleName = node.Text.Split('_')[0];
						int oldLocalIndex = int.Parse(node.Text.Split('_')[1]);
						var ExistList = NodeTextList.Where(d=>d.Contains(moduleName));
						int newGlobalIndex = ++i;
						int newLocalIndex = ExistList.Count();

						ModifyNameInfoModel renameInfoModel = new ModifyNameInfoModel()
						{
							ModuleName = moduleName,
							OldLocalIndex = oldLocalIndex,
							NewGlobalIndex = newGlobalIndex,
							NewLocalIndex = newLocalIndex
						};
						node.Text = renameInfoModel.NewName;
						RenameInfoList.Add(renameInfoModel);

					}
                    ProductContrainer.Rename(RenameInfoList);
                    break;
                }
            }     
        }

        /// <summary>
        /// Left----->Middle
        /// </summary>
        /// <param name="BusName"></param>
        /// <param name="ModuleInfoList">ModuleName, LocalIndex, GlobalIndex</param>
        public void ReplaceNewList(string BusName,List<Tuple<string,int,int, ModuleGUIBase>>ModuleInfoList)
        {
			if (treeView_ProductInfo.Nodes.Count == 0)
            {
                treeView_ProductInfo.Nodes.Add(new TreeNode(BusName.Replace("BusConfig_", "")));
            }
            else
            {
                treeView_ProductInfo.Nodes[0].Text = BusName.Replace("BusConfig_", "");
            }

            foreach (TreeNode it in treeView_ProductInfo.Nodes)
            {
                if (it.Text.Equals(BusName.Replace("BusConfig_","")))
                {
                    while (it.Nodes.Count > 0)
                        it.Nodes.RemoveAt(0);
                    ModuleInfoList.Sort((a, b) => a.Item3.CompareTo(b.Item3));
                    foreach (var Info in ModuleInfoList)
                        it.Nodes.Add($"{Info.Item1}_{Info.Item2}");
                    break;
                }       
            }
            treeView_ProductInfo.ExpandAll();
            ProductContrainer.ReplaceNewList(BusName,ModuleInfoList);
        }


        public treeviewContrainer()
        {
            InitializeComponent();
			SetButtonStyle(new Button[] { buttonUp, buttonDown});
		}
		private void SetButtonStyle(Button[] buttons)
		{
			foreach (var bt in buttons)
			{
				bt.Font = new Font("lisu", 15);
				bt.ForeColor = Color.Black;
				bt.Size = new Size(35, 15);
			}
		}

        private void treeView_ProductInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode _treeNode = this.treeView_ProductInfo.GetNodeAt(e.Location);
            this.treeView_ProductInfo.SelectedNode = _treeNode;
            if (_treeNode != null)
            {
                productContrainer.ShowProperty(_treeNode.Text);
            }
        }

        private void tSMItem_BusModel_Property_Click(object sender, EventArgs e)
        {
            TreeNode _treenode = treeView_ProductInfo.SelectedNode;
            if (_treenode != null)
            {
                productContrainer.ShowProperty(_treenode.Text);
            }
        }

        private void tSMItem_SubModel_Property_Click(object sender, EventArgs e)
        {
            TreeNode _treenode = treeView_ProductInfo.SelectedNode;
            if (_treenode != null)
            {
                productContrainer.ShowProperty(_treenode.Text);
            }
        }

        /// <summary>
        /// Left——》Middle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tSMItem_BusMedel_Add_Click(object sender, EventArgs e)
        {
            TreeNodeCollection _treenodes = treeView_ProductInfo.Nodes;
            if (_treenodes != null)
            {
                List<string> NameList = new List<string>();
                foreach (TreeNode it in _treenodes[0].Nodes)
                    NameList.Add(it.Text);

                var ExistNode = NameList.Where(n=>n.Contains((sender as ToolStripMenuItem).Text));
                var Name = $"{(sender as ToolStripMenuItem).Text}_{ExistNode.Count() + 1}";
				_treenodes[0].Nodes.Add(Name, Name);
                productContrainer.AddSubProduct((sender as ToolStripMenuItem).Text, ExistNode.Count() + 1, NameList.Count()+1);
            }
        }

        /// <summary>
        /// Left——》Middle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tSMItem_SubModel_Delete_Click(object sender, EventArgs e)
        {
            TreeNode _treenode = treeView_ProductInfo.SelectedNode;
            if (_treenode != null)
            {
                productContrainer.DeleteSubProduct(_treenode.Text);
                treeView_ProductInfo.Nodes.Remove(_treenode);
            }
        }

        private void refrushNameMenu_Click(object sender, EventArgs e)
        {
            RenameTreeNode();
            
        }

		private void Menu_ModbusRTU_Click(object sender, EventArgs e)
		{

			OnBusMenuAddClicked?.Invoke(this,(sender as ToolStripMenuItem).Text);
		}

		private void Menu_Profibus_DP_Click(object sender, EventArgs e)
		{
			OnBusMenuAddClicked?.Invoke(this, (sender as ToolStripMenuItem).Text);
		}

		private void Menu_PROFIBUS_IO_Click(object sender, EventArgs e)
		{
			OnBusMenuAddClicked?.Invoke(this, (sender as ToolStripMenuItem).Text);
		}

		private void Menu_ModbusTCP_Click(object sender, EventArgs e)
		{
			OnBusMenuAddClicked?.Invoke(this, (sender as ToolStripMenuItem).Text);
		}

		private void Menu_EatherCat_Click(object sender, EventArgs e)
		{
			OnBusMenuAddClicked?.Invoke(this, (sender as ToolStripMenuItem).Text);
		}

		private void Menu_Canopen_Click(object sender, EventArgs e)
		{
			OnBusMenuAddClicked?.Invoke(this, (sender as ToolStripMenuItem).Text);
		}

		private void Menu_DeviceNet_Click(object sender, EventArgs e)
		{
			OnBusMenuAddClicked?.Invoke(this, (sender as ToolStripMenuItem).Text);
		}


		/// <summary>
		/// 处理拖拽排序
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeView_ProductInfo_ItemDrag(object sender, ItemDragEventArgs e)
		{
			Console.WriteLine("Drag");
			treeView_ProductInfo.DoDragDrop(this, DragDropEffects.Move);
		}

		private void treeView_ProductInfo_DragDrop(object sender, DragEventArgs e)
		{
			Console.WriteLine("Drop");
		}

		private void buttonUp_Click(object sender, EventArgs e)
		{
			var treeNodes = treeView_ProductInfo.Nodes;
			if (treeNodes != null)
			{
				var productNodes = treeNodes[0].Nodes;
				var selectNode = treeView_ProductInfo.SelectedNode;
				if (productNodes.Contains(selectNode))
				{
					var index = selectNode.Index;
					if (index > 0)
					{
						var tempNode = productNodes[index - 1];
						var tempNode1 = productNodes[index];

						productNodes.Remove(tempNode);
						productNodes.Remove(tempNode1);
						productNodes.Insert(index-1,tempNode1);
						productNodes.Insert(index,tempNode);
						treeView_ProductInfo.Select();
						treeView_ProductInfo.SelectedNode = productNodes[index - 1];
						//treeView_ProductInfo.Refresh();
					}
					else
					{
						treeView_ProductInfo.Select();
						treeView_ProductInfo.SelectedNode = productNodes[index];
					}
				}
			}
		}

		private void buttonDown_Click(object sender, EventArgs e)
		{
			var treeNodes = treeView_ProductInfo.Nodes;
			if (treeNodes != null)
			{
				var productNodes = treeNodes[0].Nodes;
				var selectNode = treeView_ProductInfo.SelectedNode;
				if (productNodes.Contains(selectNode))
				{
					var index = selectNode.Index;
					if (index < productNodes.Count - 1)
					{
						var tempNode = productNodes[index];
						var tempNode1 = productNodes[index + 1];

						productNodes.Remove(tempNode);
						productNodes.Remove(tempNode1);
						productNodes.Insert(index, tempNode1);
						productNodes.Insert(index + 1, tempNode);
						treeView_ProductInfo.Select();
						treeView_ProductInfo.SelectedNode = productNodes[index + 1];
						//treeView_ProductInfo.Refresh();
					}
					else
					{
						treeView_ProductInfo.Select();
						treeView_ProductInfo.SelectedNode = productNodes[index];
					}
				}
			}
		}
		private void treeView_ProductInfo_MouseUp(object sender, MouseEventArgs e)
		{

			if (e.Button == MouseButtons.Right)
			{
				Point point = this.treeView_ProductInfo.PointToScreen(e.Location);
				var nodesCount = treeView_ProductInfo.Nodes.Count;
				if (nodesCount != 0)
				{
					TreeNode _treeNode = this.treeView_ProductInfo.SelectedNode;
					if (_treeNode != null)
					{
						if (_treeNode.Level == 0)
							this.contextMenuStrip1.Show(point);
						else if (_treeNode.Level == 1)
							this.contextMenuStrip2.Show(point);
					}
				}
				else
				{
					this.LeftControl_CTX_Menu.Show(point);
				}
			}
		}
	}
}
