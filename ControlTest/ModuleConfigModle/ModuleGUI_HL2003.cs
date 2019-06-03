
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ControlTest.ModuleConfigModle
{
    public class ModuleGUI_HL2003 : ModuleGUIBase
    {
        protected override int GuiStringListNumber { get; } = 3;
        public ModuleGUI_HL2003()
        {
            DeviceName = EnumDeviceName.HL2003;
            Function = "DO8xDC24V 2.0A";
        }


        public override void FromString(params string[] ParaList)
        {
            if (ParaList.Length != GuiStringListNumber)
                throw new Exception($"Wrong para number when parse {DeviceName.ToString()} formstring");
            GuiStringList.Clear();
            foreach (var it in ParaList)
                GuiStringList.Add(it);
            Name = GuiStringList[0];
            Function = GuiStringList[1];
            Plug_Sequence = GuiStringList[2];

        }

        protected override void SetProfile()
        {
            GuiStringList.Clear();

            GuiStringList.Add(Name);

            GuiStringList.Add(Function);

            GuiStringList.Add(Plug_Sequence);
        }

    }
}
