using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ControlTest;
using System.Diagnostics;
namespace DragTest
{
    public partial class Form1 : Form
    {
        private Graphics m_g;
        private Pen m_pen;
        Panel _p;
        
        public Form1()
        {
            InitializeComponent();
            
            Init();
        }
        private void Init()
        {           
            treeView_ProductInfo.AllowDrop = true;
            panel_Product.AllowDrop = true;
             _p = new Panel();
            _p.Height = 4;
            _p.Width = panel_Product.Width;
            _p.Left = 0;
            _p.Top = panel_Product.Height / 2 - 2;
            _p.BackColor = Color.Red;
            panel_Product.Controls.Add(_p);
            this.panel_Product.SizeChanged += Panel_Product_SizeChanged;
            string _name = DateTime.Now.TimeOfDay.ToString();
            treeView_ProductInfo.Nodes.Add(_name);
            ControlBase _controlbase = new ControlBase(this.panel_Product, new Point(50, 50));
            _controlbase.ControlMoveEvent += RefushLine;
            _controlbase.Name = _name;
            this.panel_Product.Controls.Add(_controlbase);

            m_g = panel_Product.CreateGraphics();
            m_pen = new Pen(Color.Red, 3);
            DrawLine(_controlbase);
            m_g.Flush();
            
            panel_Product.VisibleChanged += Panel_Product_VisibleChanged;
        }

        private void Panel_Product_VisibleChanged(object sender, EventArgs e)
        {
            _p.Width = panel_Product.Width;
            _p.Top = panel_Product.Height / 2 - 2;
            m_g.Clear(panel_Product.BackColor);
            m_g = panel_Product.CreateGraphics();
            foreach (var member in panel_Product.Controls)
            {
                ControlBase control = member as ControlBase;
                if (control != null)
                {
                    DrawLine(control);
                }
            }
        }

        private void Panel_Product_SizeChanged(object sender, EventArgs e)
        {
            _p.Width = panel_Product.Width;
            _p.Top= panel_Product.Height / 2 - 2;
            m_g.Clear(panel_Product.BackColor);
            m_g = panel_Product.CreateGraphics();
            foreach (var member in panel_Product.Controls)
            {
                ControlBase control = member as ControlBase;
                if (control != null )
                {
                    DrawLine(control);
                }

            }
        }

        private void panel_Product_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
                e.Effect = DragDropEffects.Copy;
        }

        private void panel_Product_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void panel_Product_DragDrop(object sender, DragEventArgs e)
        {
            Point point = panel_Product.PointToClient(new Point(e.X, e.Y));

            object info = e.Data.GetData(typeof(string));
            ControlBase userControl1 = new ControlBase(this.panel_Product,point);
            userControl1.Name = info.ToString();
            userControl1.ControlMoveEvent += RefushLine;
            panel_Product.Controls.Add(userControl1);
           
            DrawLine(userControl1);
            m_g.Flush();

        }

        private void treeView_ProductInfo_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (treeView_ProductInfo.SelectedNode == null)
                return;
            treeView_ProductInfo.DoDragDrop(treeView_ProductInfo.SelectedNode.Text, DragDropEffects.Copy);
        }
        private void RefushLine(object sender, ControlMoveEventArgs e)
        {
            m_g.Clear(panel_Product.BackColor);
            
            if (e.info == "LeftMouseDown")
            {
                foreach (var member in panel_Product.Controls)
                {
                    ControlBase control = member as ControlBase;
                    if (control != null && control != (ControlBase)sender)
                    {
                        DrawLine(control);
                    }

                }
                
            }
            else if (e.info == "LeftMouseUp")
            {
                foreach (var member in panel_Product.Controls)
                {
                    ControlBase control = member as ControlBase;
                    if (control != null )
                    {
                        DrawLine(control);
                    }

                }
            }
           
        }
    
        private void DrawLine(ControlBase control)
        {
            if (control.Top > panel_Product.Height / 2)
            {
                m_g.DrawLine(m_pen, new Point(control.Left + control.Width / 2, control.Top), new Point(control.Left + control.Width / 2, panel_Product.Height / 2));
                m_g.FillEllipse(new System.Drawing.SolidBrush(Color.Gray), new Rectangle(control.Left + control.Width / 2 -5, panel_Product.Height / 2 -5, 10, 10));
            }
            else
            {
                m_g.DrawLine(m_pen, new Point(control.Left + control.Width / 2, control.Top + control.Height), new Point(control.Left + control.Width / 2, panel_Product.Height / 2));
                m_g.FillEllipse(new System.Drawing.SolidBrush(Color.Gray), new Rectangle(control.Left + control.Width / 2 -5, panel_Product.Height / 2 -5, 10, 10));
                
            }
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("123", "waring");
        }

        private void tSMItem_BusMedel_Add_Click(object sender, EventArgs e)
        {
            TreeNode _treenode = treeView_ProductInfo.SelectedNode;
            if(_treenode!=null)
            {
                treeView_ProductInfo.SelectedNode.Nodes.Add(DateTime.Now.TimeOfDay.ToString());
              
            }
        }

        private void tSMItem_BusModel_Property_Click(object sender, EventArgs e)
        {

        }

        private void treeView_ProductInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode _treeNode = this.treeView_ProductInfo.GetNodeAt(e.Location);
            if (_treeNode != null)
            {
                if (_treeNode.Level == 0)
                    MessageBox.Show("BusModel");
                else
                    MessageBox.Show("SubModel");

            }
        }

        private void tSMItem_SubModel_Property_Click(object sender, EventArgs e)
        {
            TreeNode _treenode = treeView_ProductInfo.SelectedNode;
            if (_treenode != null)
            {
                foreach (var member in panel_Product.Controls)
                {
                    ControlBase _c = member as ControlBase;
                    if (_c != null)
                    {
                        if (_c.Name == _treenode.Text)
                        {
                            _c.ShowProperty();
                        }
                    }

                }
            }

        }

        private void tSMItem_SubModel_Delete_Click(object sender, EventArgs e)
        {
            TreeNode _treenode = treeView_ProductInfo.SelectedNode;
            if (_treenode != null)
            {
                foreach (var member in panel_Product.Controls)
                {
                    ControlBase _c = member as ControlBase;
                    if (_c != null)
                    {
                        if (_c.Name == _treenode.Text)
                        {
                            panel_Product.Controls.Remove(_c);
                            
                            break;
                        }
                    }

                }
                _treenode.Parent.Nodes.Remove(_treenode);
                m_g.Clear(panel_Product.BackColor);
                foreach (var member in panel_Product.Controls)
                {
                    ControlBase control = member as ControlBase;
                    if (control != null)
                    {
                        DrawLine(control);
                    }

                }
            }

        }
    }
}
