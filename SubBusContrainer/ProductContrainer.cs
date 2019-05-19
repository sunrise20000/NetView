using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlTest;
namespace SubBusContrainer
{
    public partial class ProductContrainer : UserControl
    {
        private Graphics m_g;
        private Pen m_pen;
        private Panel _p;
        public event EventHandler<ControlBaseRemoveEventArgs> ControlBaseRemoveEvent;
        
        public ProductContrainer()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            panel_Product.AllowDrop = true;
            _p = new Panel();
            _p.Height = 4;
            _p.Width = panel_Product.Width;
            _p.Left = 0;
            _p.Top = panel_Product.Height / 2 - 2;
            _p.BackColor = Color.Red;
            panel_Product.Controls.Add(_p);
            this.panel_Product.SizeChanged += Panel_Product_SizeChanged;
            this.panel_Product.VisibleChanged += Panel_Product_VisibleChanged;

            m_g = panel_Product.CreateGraphics();
            m_pen = new Pen(Color.Red, 3);

            m_g.Flush();
        }
        private void panel_Product_DragDrop(object sender, DragEventArgs e)
        {
            Point point = panel_Product.PointToClient(new Point(e.X, e.Y));

            object info = e.Data.GetData(typeof(string));
            ControlBase userControl1 = new ControlBase(this.panel_Product, point);
            userControl1.Name = info.ToString();
            userControl1.ControlMoveEvent += RefushLine;
            panel_Product.Controls.Add(userControl1);

            DrawLine(userControl1);
            m_g.Flush();
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
                    if (control != null)
                    {
                        DrawLine(control);
                    }

                }
            }

        }
        private void RefushLine()
        {
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
        private void DrawLine(ControlBase control)
        {
            if (control.Top > panel_Product.Height / 2)
            {
                m_g.DrawLine(m_pen, new Point(control.Left + control.Width / 2, control.Top), new Point(control.Left + control.Width / 2, panel_Product.Height / 2));
                m_g.FillEllipse(new System.Drawing.SolidBrush(Color.Gray), new Rectangle(control.Left + control.Width / 2 - 5, panel_Product.Height / 2 - 5, 10, 10));
            }
            else
            {
                m_g.DrawLine(m_pen, new Point(control.Left + control.Width / 2, control.Top + control.Height), new Point(control.Left + control.Width / 2, panel_Product.Height / 2));
                m_g.FillEllipse(new System.Drawing.SolidBrush(Color.Gray), new Rectangle(control.Left + control.Width / 2 - 5, panel_Product.Height / 2 - 5, 10, 10));

            }
        }
        public void AddSubProduct(string subproductname)
        {
            if (CheckExit(subproductname))
            {
                throw new Exception("已存在当前名称的产品");
            }
            ControlBase userControl1 = new ControlBase(this.panel_Product, new Point(200, 200));
            userControl1.Name = subproductname;
            userControl1.ControlMoveEvent += RefushLine;
            panel_Product.Controls.Add(userControl1);
            DrawLine(userControl1);
            m_g.Flush();
        }
        private bool CheckExit(string subproductname)
        {
            foreach (var member in panel_Product.Controls)
            {
                ControlBase _controlBase = member as ControlBase;
                if (_controlBase != null)
                {
                    if (_controlBase.Name == subproductname)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void RemoveSubProduct(string subproductname)
        {
            foreach (var member in panel_Product.Controls)
            {
                ControlBase _controlBase = member as ControlBase;
                if (_controlBase != null)
                {
                    if (_controlBase.Name == subproductname)
                    {
                        _controlBase.Dispose();
                        break;
                    }
                }
            }
            RefushLine();

        }

        private void panel_Product_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (e.Control is ControlBase)
            {
                ControlBase controlBase = e.Control as ControlBase;
                ControlBaseRemoveEventArgs controlBaseRemoveEventArgs = new ControlBaseRemoveEventArgs();
                controlBaseRemoveEventArgs.ControlName = controlBase.Name;
                OnControlMoveEvent(controlBaseRemoveEventArgs);
            }
        }
        private void OnControlMoveEvent(ControlBaseRemoveEventArgs e)
        {
            if(ControlBaseRemoveEvent!=null)
            {
                ControlBaseRemoveEvent(this, e);
            }
        }
    }
    public  class  ControlBaseRemoveEventArgs:EventArgs
    {
        public string ControlName { get; set; }
    }
}
