
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ControlTest.ModuleConfigModle
{
    [Serializable()]
    public class ModuleCfg_HL5001 : ModuleCfgModleBase
    {

        protected override int GuiStringListNumber { get; } = 11;
        public ModuleCfg_HL5001()
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
            GetStringFromList(GuiStringList, Name, Function, Plug_Sequence, CounterLimitH, CounterLimitL, ResPara1,
                            ResPara2, ResPara3, ResPara4, ResPara5, ResPara6);

        }

        protected override void SetProfile()
        {
           GuiStringList.Clear();
            GetListFromStr(GuiStringList,Name, Function, Plug_Sequence,CounterLimitH,CounterLimitL, ResPara1,
                            ResPara2, ResPara3, ResPara4, ResPara5, ResPara6);

        }

       
    }
}
