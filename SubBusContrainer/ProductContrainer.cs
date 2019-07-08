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
using ControlTest.ModuleConfigModle;

namespace SubBusContrainer
{
    public partial class ProductContrainer : UserControl
    {
        private Graphics m_g;
        private Pen m_pen;
        private Panel _p;
        private PictureBox _pb=new PictureBox();
        private HorizontalCenterLine m_hcLine = null;

        SubBusModel LastSubModel = null;

        BusModel BusModule = null;
       
        Bitmap bitmap;


        public event EventHandler<ModuleAddedArgs> OnProductChangedEvent;


        /// <summary>
        /// Left——》Middle
        /// </summary>
        /// <param name="BusModelName"></param>
        public ProductContrainer()
        {
            InitializeComponent();
            Init();
           // this.SizeChanged += Panel_Product_SizeChanged;
        }

           
        public string BusName
        {
            get
            {
                if(BusModule!=null)
                    return BusModule.Name;
                return "";
            }
            set
            {
                if(BusModule!=null && BusModule.Name!=value)
                    BusModule.Name = value;

            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            this.AllowDrop = true;
            _pb = new PictureBox();
            this.Controls.Add(_pb);
            _pb.Dock = DockStyle.Fill;
           // _pb.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);

            _pb.SendToBack();
            m_hcLine = new HorizontalCenterLine()
            {
                Height = 4,
                Width = _pb.Width,
                Left = 0,
                Top = _pb.Height / 2 - 2,
                PenColor = Color.Green,
            };
            bitmap = new Bitmap(_pb.Width, _pb.Height);
            m_pen = new Pen(Color.Green, 3);
            _pb.Refresh();
            this.SizeChanged += Panel_Product_SizeChanged;
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
            
            List<string> ControlNameList = new List<string>();
            foreach (Control it in this.Controls)
                if ((it as SubBusModel) != null)
                    ControlNameList.Add((it as SubBusModel).Name);

            if (info.ToString().Contains("HL")) //子模块
            {
                if (BusModule == null)
                {
                    MessageBox.Show("Please add a bus first!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var ExistCount = ControlNameList.Where(c => c.Contains(info.ToString())).Count();
                EnumDeviceName subBusModelType = (EnumDeviceName)Enum.Parse(typeof(EnumDeviceName), info.ToString().Substring(0, 6));
                SubBusModel SubBusModule = new SubBusModel(point, subBusModelType, ExistCount + 1, ControlNameList.Count + 1);

                SubBusModule.OnModleDelete += UserControl1_OnModleDelete;
                this.Controls.Add(SubBusModule);
                SubBusModule.BringToFront();
                SubBusModule.ControlMoveEvent += BusModule_ControlMoveEvent;
                BusModule_ControlMoveEvent(new object(), new ControlMoveEventArgs(""));

                OnProductChangedEvent?.Invoke(this, new ModuleAddedArgs() {Module= SubBusModule,IsAdd = true});
            }
            else  //总线
            {
                ChangeBus(info.ToString());
            }
        }

        private void BusModule_ControlMoveEvent(object sender, ControlMoveEventArgs e)
        {
            _pb.Image = bitmap;
            m_g = Graphics.FromImage(bitmap);
            m_g.Clear(this.BackColor);
            bool bDrawHLine = false;
            foreach (var member in Controls)
            {
                ControlBase control = member as ControlBase;
                if (control != null)
                {
                    if (bDrawHLine == false)
                    {
                        bDrawHLine = true;
                        m_g.DrawLine(new Pen(m_hcLine.PenColor, m_hcLine.LineWidth), new PointF(m_hcLine.Left, m_hcLine.Top), new PointF(m_hcLine.Left + m_hcLine.Width, m_hcLine.Top));
                    }
                    DrawLine(control);
                }
            }

           

            
            m_g.Flush();
            m_g.Dispose();
            _pb.Image = bitmap;       
        }

        /// <summary>
        /// Middle->Left
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl1_OnModleDelete(object sender, EventArgs e)
        {

            if (sender is BusModel)
            {
                if (MessageBox.Show("You will delete all the submodules, do you want to delelte all submodules?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    List<ControlBase> ModuleList = new List<ControlBase>();
                    BusModule = null;
                    foreach (var it in this.Controls)
                    {
                        if (it is ControlBase)
                            ModuleList.Add(it as ControlBase);
                    }
                    foreach (var it in ModuleList)
                        it.Dispose();
                    BusModule_ControlMoveEvent(new object(), new ControlMoveEventArgs(""));
                    OnProductChangedEvent?.Invoke(this, new ModuleAddedArgs() { IsAdd = false, Module = sender as ControlBase });
                }
            }
            else
            {
                
                OnProductChangedEvent?.Invoke(this, new ModuleAddedArgs() { IsAdd = false, Module = sender as ControlBase });
                (sender as ControlBase).Dispose();
                BusModule_ControlMoveEvent(new object(), new ControlMoveEventArgs(""));
            }

        }

        public void ChangeBus(string Name)
        {
            if (BusModule != null)
            {
                if (BusModule.Name != Name && MessageBox.Show("Do you want to change bus type?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    this.Controls.Remove(BusModule);
                else
                    return;
            }

            Enum.TryParse(Name, out EnumBusType BusType);
            BusModule = new BusModel(BusType, new Point(100, this.Height / 2 + 100));

            this.Controls.Add(BusModule);
            BusModule.BringToFront();
            BusModule.OnModleDelete += UserControl1_OnModleDelete;
            BusModule.ControlMoveEvent += BusModule_ControlMoveEvent;
            BusModule_ControlMoveEvent(new object(), new ControlMoveEventArgs(""));
            OnProductChangedEvent?.Invoke(this, new ModuleAddedArgs() { IsAdd = true, Module = BusModule });

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

        private void Panel_Product_SizeChanged(object sender, EventArgs e)
        {
            m_hcLine= new HorizontalCenterLine()
            {
                Height = 4,
                Width = _pb.Width,
                Left = 0,
                Top = _pb.Height / 2 - 2,
                PenColor = Color.Green,
            };

            bitmap = new Bitmap(_pb.Width, _pb.Height);

        }

        private void DrawLine(ControlBase control)
        {
            //_pb.Image = bitmap;
            //m_g = Graphics.FromImage(bitmap);
            //m_g.Clear(this.BackColor);
            if (control.Top > this.Height / 2)
            {
                m_g.DrawLine(m_pen, new Point(control.Left + control.Width / 2, control.Top), new Point(control.Left + control.Width / 2, this.Height / 2));
                m_g.FillEllipse(new System.Drawing.SolidBrush(Color.Gray), new Rectangle(control.Left + control.Width / 2 - 4, this.Height / 2 - 7, 10, 10));
            }
            else
            {
                m_g.DrawLine(m_pen, new Point(control.Left + control.Width / 2, control.Top + control.Height), new Point(control.Left + control.Width / 2, this.Height / 2));
                m_g.FillEllipse(new System.Drawing.SolidBrush(Color.Gray), new Rectangle(control.Left + control.Width / 2 - 5, this.Height / 2 - 7, 10, 10));
            }
        }

        /// <summary>
        /// Left——》Middle
        /// </summary>
        /// <param name="subproductname"></param>
        public void AddSubProduct(string subproductname, int LocalIndex, int GlobalIndex)
        {
            //if (CheckExit(subproductname))
            //{
            //    throw new Exception("已存在当前名称的产品");
            //}
            Point point = new Point();
            if (LastSubModel == null)
            {
                point.X = 0;
                point.Y = 20;
            }
            else
            {
                point.X = LastSubModel.Location.X + 30;
                point.Y = LastSubModel.Location.Y+10;
            }
            EnumDeviceName subBusModelType = (EnumDeviceName)Enum.Parse(typeof(EnumDeviceName), subproductname.Substring(0, 6));

            LastSubModel = new SubBusModel(point, subBusModelType, LocalIndex,GlobalIndex);
            LastSubModel.Name = subproductname;

            LastSubModel.OnModleDelete += UserControl1_OnModleDelete;
            this.Controls.Add(LastSubModel);
            LastSubModel.ControlMoveEvent += BusModule_ControlMoveEvent;
            BusModule_ControlMoveEvent(new object(), new ControlMoveEventArgs(""));

            LastSubModel.BringToFront();
            LastSubModel.Focus();

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
        public void DeleteSubProduct(string subproductname)
        {
            foreach (var member in this.Controls)
            {
                SubBusModel _controlBase = member as SubBusModel;
                if (_controlBase != null)
                {
                    if ($"{_controlBase.Name}_{_controlBase.LocalIndex}".Equals(subproductname))
                    {
                        _controlBase.Dispose();
                        BusModule_ControlMoveEvent(new object(), new ControlMoveEventArgs(""));
                        break;
                    }
                }
            }

        }
        public void ReplaceNewList(string BusName, List<Tuple<string,int,int,ModuleGUIBase>> SubModuleInfoList)
        {
            List<SubBusModel> list = new List<SubBusModel>();
            foreach (var member in this.Controls)
            {
                if(member is SubBusModel)
                list.Add(member as SubBusModel);
            }
            foreach (var it in list)
            {
                if (it != null)
                    it.Dispose();
            }
            if (BusModule != null)
            {
                BusModule.Dispose();
                BusModule = null;
            }

            if (BusModule == null)
            {
                Enum.TryParse(BusName, out EnumBusType BusType);
                BusModule = new BusModel(BusType,new Point(100, this.Height / 2 + 100));
                this.Controls.Add(BusModule);
                BusModule.BringToFront();
                BusModule.OnModleDelete += UserControl1_OnModleDelete;
                BusModule.ControlMoveEvent += BusModule_ControlMoveEvent;
                BusModule_ControlMoveEvent(new object(), new ControlMoveEventArgs(""));
                this.BusName = BusName;
            }

          
            LastSubModel = null;
            foreach (var Info in SubModuleInfoList)
            {
                Point point = new Point();
                if (LastSubModel == null)
                {
                    point.X = 0;
                    point.Y = 20;
                }
                else
                {
                    point.X = this.LastSubModel.Location.X + 30;
                    point.Y = this.LastSubModel.Location.Y+10;
                }
                EnumDeviceName subBusModelType = (EnumDeviceName)Enum.Parse(typeof(EnumDeviceName), Info.Item1);
                LastSubModel = new SubBusModel(point, subBusModelType, Info.Item2, Info.Item3);
                LastSubModel.InitGcb(Info.Item4);
                 
                this.Controls.Add(LastSubModel);
                LastSubModel.BringToFront();
                LastSubModel.Focus();
                LastSubModel.OnModleDelete += UserControl1_OnModleDelete;
                LastSubModel.ControlMoveEvent += BusModule_ControlMoveEvent;
                BusModule_ControlMoveEvent(new object(), new ControlMoveEventArgs(""));
            }

        }
        public void ShowProperty(string subproductnameWithLocalIndex)
        {
            foreach (var member in this.Controls)
            {
                ControlBase _controlBase = member as ControlBase;
                if (_controlBase != null)
                {
                    if ($"{_controlBase.Name}_{_controlBase.LocalIndex}" == subproductnameWithLocalIndex)
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
            foreach (var mem in this.Controls)
            {
                SubBusModel SubBusModel = mem as SubBusModel;
                if (SubBusModel != null)
                    controlBasesList.Add(SubBusModel);

            }
            return controlBasesList;
        }
        public void ReName(List<string> NameList)
        {
            List<Tuple<int, int, SubBusModel>> KpList = new List<Tuple<int, int, SubBusModel>>();
            int i = 0;
            foreach (var member in this.Controls)
            {
                SubBusModel SubBm = member as SubBusModel;
                if (SubBm != null)
                {
                    KpList.Add(new Tuple<int, int, SubBusModel>(i++, SubBm.Location.X, SubBm));
                }
            }
            KpList.Sort((a, b) => a.Item2.CompareTo(b.Item2));

            for (i = 0; i < KpList.Count; i++)
            {
                KpList.ElementAt(i).Item3.Name = NameList[i];
            }
        }
    }
    public class HorizontalCenterLine
    {
        public int LineWidth { get; set; } = 4;
        public Color PenColor { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
