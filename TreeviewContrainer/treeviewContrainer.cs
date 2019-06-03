using System;
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

namespace TreeviewContrainer
{
    public partial class treeviewContrainer: UserControl
    {
        private ProductContrainer productContrainer;
        public event EventHandler<SubBusContrainer.Model.ModuleAddedArgs> OnBusModulChanged;
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
            else
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
                OnBusModulChanged?.Invoke(this,e);
            }
            treeView_ProductInfo.ExpandAll();
            //RenameTreeNode();
        }

        private void RenameTreeNode()
        {
            TreeNode DesNode = null;
            foreach (TreeNode it in treeView_ProductInfo.Nodes)
            {
                if (it.Text.Equals(productContrainer.BusName))
                {
                    DesNode = it;
                    Dictionary<int, string> NodeTextDic = new Dictionary<int, string>();
                    int i = 0;
                    foreach (TreeNode node in it.Nodes)
                    {
                        var ModuleName = node.Text.Split('_')[0];
                        var ExistDic = NodeTextDic.Where(d=>d.Value.Contains(ModuleName));
                        var NameInGui = $"{ModuleName}_{ExistDic.Count() + 1}";
                        NodeTextDic.Add(i++, NameInGui);
                        node.Text = NameInGui;
                    }

                    ProductContrainer.ReName(NodeTextDic.Values.ToList());
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
                treeView_ProductInfo.Nodes.Add(new TreeNode(BusName));
            }
            else
            {
                treeView_ProductInfo.Nodes[0].Text = BusName;
            }

            foreach (TreeNode it in treeView_ProductInfo.Nodes)
            {
                if (it.Text.Equals(BusName))
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
        }

        private void treeView_ProductInfo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point point = this.treeView_ProductInfo.PointToScreen(e.Location);
                TreeNode _treeNode = this.treeView_ProductInfo.GetNodeAt(e.Location);
                if (_treeNode != null)
                {
                    if (_treeNode.Level == 0)
                        this.contextMenuStrip1.Show(point);
                    else
                        this.contextMenuStrip2.Show(point);
                    this.treeView_ProductInfo.SelectedNode = _treeNode;
                }
            }
            else
            {
                Point point = this.treeView_ProductInfo.PointToScreen(e.Location);
                TreeNode _treeNode = this.treeView_ProductInfo.GetNodeAt(e.Location);
                if (_treeNode != null)
                {
                    this.treeView_ProductInfo.SelectedNode = _treeNode;
                }
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
            TreeNode _treenode = treeView_ProductInfo.SelectedNode;
            if (_treenode != null)
            {
                List<string> NameList = new List<string>();
                foreach (TreeNode it in _treenode.Nodes)
                    NameList.Add(it.Text);

                var ExistNode = NameList.Where(n=>n.Contains((sender as ToolStripMenuItem).Text));
                var Name = $"{(sender as ToolStripMenuItem).Text}_{ExistNode.Count() + 1}";
                treeView_ProductInfo.SelectedNode.Nodes.Add(Name, Name);
                productContrainer.AddSubProduct((sender as ToolStripMenuItem).Text, ExistNode.Count() + 1, NameList.Count());
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
    }
}
