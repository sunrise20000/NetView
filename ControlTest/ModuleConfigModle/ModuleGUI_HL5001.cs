
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ControlTest.ModuleConfigModle
{
    [Serializable()]
    public class ModuleGUI_HL5001 : ModuleGUIBase
    {

        protected override int GuiStringListNumber { get; } = 11;
        public ModuleGUI_HL5001()
        {
            DeviceName = EnumDeviceName.HL5001;
            Function = "Counter 6000";
        }

        public string CounterLimitH { get; set; }

        public string CounterLimitL { get; set; }

        public string ResPara1 { get; set; }
        public string ResPara2 { get; set; }
        public string ResPara3 { get; set; }
        public string ResPara4 { get; set; }
        public string ResPara5 { get; set; }
        public string ResPara6 { get; set; }

        public override void FromString(params string[] ParaList)
        {
            if (ParaList.Length != 11)
                throw new Exception($"Wrong para number when parse {DeviceName.ToString()} formstring");
            GuiStringList.Clear();
            foreach (var it in ParaList)
                GuiStringList.Add(it);

            Name = GuiStringList[0];
            Function = GuiStringList[1];
            Plug_Sequence = GuiStringList[2];
            CounterLimitH = GuiStringList[3];
            CounterLimitL = GuiStringList[4];
            ResPara1 = GuiStringList[5];
            ResPara2 = GuiStringList[6];
            ResPara3 = GuiStringList[7];
            ResPara4 = GuiStringList[8];
            ResPara5 = GuiStringList[9];
            ResPara6 = GuiStringList[10];

        }

        protected override void SetProfile()
        {
           GuiStringList.Clear();
            GetListFromStr(GuiStringList,Name, Function, Plug_Sequence,CounterLimitH,CounterLimitL, ResPara1,
                            ResPara2, ResPara3, ResPara4, ResPara5, ResPara6);

        }

       
    }
}
