
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ControlTest.ModuleConfigModle
{
    [Serializable()]
    public class ModuleCfg_HL5002 : ModuleCfgModleBase
    {
        
        protected override int GuiStringListNumber { get; } = 10;
        public ModuleCfg_HL5002()
        {
            DeviceName = EnumDeviceName.HL5002;
        }


        [TypeConverter(typeof(TypeConvertClass.Tcv))]
        public EnumHL5002Resolution Resolution { get; set; }

        [TypeConverter(typeof(TypeConvertClass.Tcv))]
        public EnumHL5002Revolution Revolution { get; set; }

        public string PresetValue { get; set;}

        public string ResPara1 { get; set; }
        public string ResPara2 { get; set; }
        public string ResPara3 { get; set; }
        public string ResPara4 { get; set; }
        public string ResPara5 { get; set; }


        public override void FromString(params string[] ParaList)
        {
            if (ParaList.Length != 11)
                throw new Exception($"Wrong para number when parse {DeviceName.ToString()} formstring");
            string R1="", R2="";
            GetListFromStr(GuiStringList, Name, Function, Plug_Sequence, R1, R2, PresetValue,
                        ResPara1,ResPara2, ResPara3, ResPara4, ResPara5);
            Enum.TryParse(R1, out EnumHL5002Resolution resolution);
            Enum.TryParse(R1, out EnumHL5002Revolution revolution);
            Resolution = resolution;
            Revolution = revolution;
        }

        protected override void SetProfile()
        {
            GuiStringList.Clear();
            GetStringFromList(GuiStringList, Name, "AbsEncoder SSI", Plug_Sequence, Resolution.ToString(), Revolution.ToString(), PresetValue,
                    ResPara1, ResPara2, ResPara3, ResPara4, ResPara5);

        }
       
    }
}
