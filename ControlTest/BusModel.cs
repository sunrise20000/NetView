using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ControlTest.BusConfigModle;

namespace ControlTest
{
    public partial class BusModel : ControlBase
    {
        public BusCfgBase Bcb { get; private set; }
        public EnumBusType BusType { get; private set; }
        public BusModel(EnumBusType BusType, Point location, int Width=200, int Height=150):base(location,Width,Height,Color.Teal)
        {
            this.BusType = BusType;
            InitializeComponent();

            //实例化
            var ClassName = $"ControlTest.BusConfigModle.{BusType.ToString()}";
            var T = Type.GetType(ClassName);
            Bcb = T.Assembly.CreateInstance(ClassName) as BusCfgBase;

        }
        public override void ShowProperty()
        {
            WinPropertySetting = new Window_Property(Bcb.Name);
            WinPropertySetting.SelectedObject = Bcb;
            WinPropertySetting.ShowDialog();
        }
    }
}
