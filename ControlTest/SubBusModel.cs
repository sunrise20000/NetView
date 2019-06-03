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
        public ModuleGUIBase Mcb { get; set; }
        public EnumDeviceName ModuleType { get; private set; }


        public SubBusModel(Point location, EnumDeviceName modelType, int LocalIndex, int GlobalIndex, int Width = 140, int Height = 60) : base(location, Width, Height, Color.LightBlue)
        {
            ModuleType = modelType;
            InitializeComponent();
            this.Name = modelType.ToString();
            this.LocalIndex = LocalIndex;
            this.GlobalIndex = GlobalIndex;
            this.DisplayName = $"{Name}_{LocalIndex}";

            //实例化
            var ClassName = $"ControlTest.ModuleConfigModle.ModuleGUI_{modelType.ToString()}";
            var T = Type.GetType(ClassName);
            Mcb = T.Assembly.CreateInstance(ClassName) as ModuleGUIBase;
            //Mcb.Name = modelType.ToString();
            Mcb.Name = DisplayName;
            Mcb.Plug_Sequence = $"{this.GlobalIndex}";

            //Mcb.FromString()
        }

        public void InitGcb(ModuleGUIBase mcb)
        {
            this.Mcb = mcb;
        }
        public override void ShowProperty()
        {
            WinPropertySetting = new Window_Property(Mcb.Name);
            WinPropertySetting.SelectedObject = Mcb;
            WinPropertySetting.ShowDialog();
        }
    }
}
