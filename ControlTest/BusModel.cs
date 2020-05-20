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
        public BusGUI_Base Bcb { get; private set; }
        public EnumBusType BusType { get; private set; }
        public BusModel(EnumBusType BusType, Point location, int Width=200, int Height=150):base(location,Width,Height,Color.Teal)
        {
            this.BusType = BusType;
            InitializeComponent();
            this.Name = BusType.ToString();
            this.DisplayName = this.BusType.ToString();
            //实例化
            var ClassName = $"ControlTest.BusConfigModle.BusGUI_{BusType.ToString()}";
            var T = Type.GetType(ClassName);
            Bcb = T.Assembly.CreateInstance(ClassName) as BusGUI_Base;
            
        }
		protected override string DisplayName
		{
			get
			{
				return this.label1.Text;
			}
			set {
				label1.Text = value;
			}
		}
	
        public override void ShowProperty()
        {
            WinPropertySetting = new Window_Property(Bcb.Name);
            WinPropertySetting.SelectedObject = Bcb;
            WinPropertySetting.ShowDialog();
        }
    }
}
