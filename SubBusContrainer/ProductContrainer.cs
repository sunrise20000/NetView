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
using System.Diagnostics;
using SubBusContrainer.Model;

namespace SubBusContrainer
{
    public partial class ProductContrainer : UserControl
    {
        private Graphics m_g;
        private Pen m_pen;
        private Panel _p;
        // private Image m_image;
        // private Graphics m_imageG;
        public event EventHandler<ProductAddedArgs> OnProductChangedEvent;
        public ProductContrainer(string BusModelName)
        {
            InitializeComponent();

            Init();
            //  m_image = new Bitmap(this.Width, this.Height);

            // m_imageG = Graphics.FromImage(m_image);

            BusModel userControl1 = new BusModel(new Point(100, 100));
            BusName = BusModelName;
            userControl1.Name = BusModelName;
            userControl1.ControlMoveEvent += RefushLine;
            this.Controls.Add(userControl1);
            // DrawLine(userControl1);
            m_g.Flush();
            // this.BackgroundImage = m_image;

            this.SizeChanged += Panel_Product_SizeChanged;
            //  this.panel_Product.VisibleChanged += Panel_Product_VisibleChanged;
        }

        public string BusName{ get; private set; }
        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            this.AllowDrop = true;
            _p = new Panel();
            _p.Height = 4;
            _p.Width = this.Width;
            _p.Left = 0;
            _p.Top = this.Height / 2 - 2;
            _p.BackColor = Color.Red;
            this.Controls.Add(_p);
           

            m_g = this.CreateGraphics();
            m_pen = new Pen(Color.Red, 3);

            //m_g.Flush();
        }

        /// <summary>
        /// Middle——》Left
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel_Product_DragDrop(object sender, DragEventArgs e)
        {
            Point point = this.PointToClient(new Point(e.X, e.Y));

            object info = e.Data.GetData(typeof(string));
            SubBusModel userControl1 = new SubBusModel( point);
            List<string> ControlNameList = new List<string>();
            foreach (Control it in this.Controls)
                if((it as SubBusModel)!=null)
                    ControlNameList.Add((it as SubBusModel).Name);

            var ExistControlName = ControlNameList.Where(c=>c.Contains(info.ToString()));

            userControl1.Name = $"{info.ToString()}_{ExistControlName.Count()+1}";
            userControl1.ControlMoveEvent += RefushLine;
            this.Controls.Add(userControl1);

            DrawLine(userControl1);
            m_g.Flush();

            OnProductChangedEvent?.Invoke(this, new ProductAddedArgs() { ProductName = userControl1.Name, IsAdd = true, });
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
            _p.Width = this.Width;
            _p.Top = this.Height / 2 - 2;
            m_g.Clear(this.BackColor);
            m_g = this.CreateGraphics();
            foreach (var member in this.Controls)
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
            
            _p.Width = this.Width;
            _p.Top = this.Height / 2 - 2;
            m_g = this.CreateGraphics();
            m_g.Clear(this.BackColor);
            
            foreach (var member in this.Controls)
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
            m_g.Clear(this.BackColor);
            if (e.info == "LeftMouseDown")
            {
                foreach (var member in this.Controls)
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
                foreach (var member in this.Controls)
                {
                    ControlBase control = member as ControlBase;
                    if (control != null)
                    {
                        DrawLine(control);
                    }

                }
            }
            m_g.Flush();
        }
        private void RefushLine()
        {

            m_g.Clear(this.BackColor);
            foreach (var member in this.Controls)
            {
                ControlBase control = member as ControlBase;
                if (control != null)
                {
                    DrawLine(control);
                }
            }
            m_g.Flush();
        }
        private void DrawLine(ControlBase control)
        {

            if (control.Top > this.Height / 2)
            {
                m_g.DrawLine(m_pen, new Point(control.Left + control.Width / 2, control.Top), new Point(control.Left + control.Width / 2, this.Height / 2));
                m_g.FillEllipse(new System.Drawing.SolidBrush(Color.Gray), new Rectangle(control.Left + control.Width / 2 - 5, this.Height / 2 - 5, 15, 15));
            }
            else
            {
                m_g.DrawLine(m_pen, new Point(control.Left + control.Width / 2, control.Top + control.Height), new Point(control.Left + control.Width / 2, this.Height / 2));
                m_g.FillEllipse(new System.Drawing.SolidBrush(Color.Gray), new Rectangle(control.Left + control.Width / 2 - 5, this.Height / 2 - 5, 15, 15));
            }
            
        }

        /// <summary>
        /// Left——》Middle
        /// </summary>
        /// <param name="subproductname"></param>
        public void AddSubProduct(string subproductname)
        {
            if (CheckExit(subproductname))
            {
                throw new Exception("已存在当前名称的产品");
            }
            Point point=new Point();
            foreach(var member in this.Controls)
            {
                ControlBase controlBase = member as ControlBase;
                if(controlBase!=null)
                {
                    point.X = controlBase.Left + 20;
                    point.Y = controlBase.Top + 20;
                    break;
                }
            }
            SubBusModel userControl1 = new SubBusModel(point);
            
            userControl1.Name = subproductname;
            userControl1.ControlMoveEvent += RefushLine;
            this.Controls.Add(userControl1);
            userControl1.BringToFront();
            userControl1.Focus();
            DrawLine(userControl1);
            m_g.Flush();
        }
        private bool CheckExit(string subproductname)
        {
            foreach (var member in this.Controls)
            {
                SubBusModel _controlBase = member as SubBusModel;
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

        /// <summary>
        /// 响应Left——》Middle的删除消息
        /// </summary>
        /// <param name="subproductname"></param>
        public void RemoveSubProduct(string subproductname)
        {
            foreach (var member in this.Controls)
            {
                SubBusModel _controlBase = member as SubBusModel;
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
        public void ShowProperty(string subproductname)
        {
            foreach (var member in this.Controls)
            {
                ControlBase _controlBase = member as ControlBase;
                if (_controlBase != null)
                {
                    if (_controlBase.Name == subproductname)
                    {
                        _controlBase.ShowProperty();
                        break;
                    }
                }
            }

        }
        public List<SubBusModel> EnumProduct()
        {
            List<SubBusModel> controlBasesList = new List<SubBusModel>();
            foreach(var mem in this.Controls)
            {
                SubBusModel SubBusModel = mem as SubBusModel;
                if (SubBusModel != null)
                    controlBasesList.Add(SubBusModel);

            }
            return controlBasesList;
        }


        /// <summary>
        /// Middle——》Left  删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel_Product_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (e.Control is SubBusModel)
            {
                SubBusModel SubBusModel = e.Control as SubBusModel;
                OnProductChangedEvent?.Invoke(this, new ProductAddedArgs() { ProductName = SubBusModel.Name, IsAdd = false });
            }
        }
    }
}
