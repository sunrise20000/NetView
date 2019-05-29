using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ControlTest.ModuleConfigModle;

namespace ControlTest
{
    public partial class SubBusModel : ControlBase
    {
        public ModuleCfgModleBase Mcm { get; private set; }
        public EnumDeviceName ModuleType { get; private set; }
        public SubBusModel(Point location, EnumDeviceName modelType, int Width = 140, int Height = 60) : base(location, Width, Height, Color.LightBlue)
        {
            ModuleType = modelType;
            InitializeComponent();
            //实例化
            var ClassName = $"ControlTest.ModuleConfigModle.ModuleCfg_{modelType.ToString()}";
            var T = Type.GetType(ClassName);
            Mcm = T.Assembly.CreateInstance(ClassName) as ModuleCfgModleBase;
            
        }
        public override void ShowProperty()
        {
            WinPropertySetting = new Window_Property();
            WinPropertySetting.SelectedObject = Mcm;
            WinPropertySetting.ShowDialog();
        }
    }
}
