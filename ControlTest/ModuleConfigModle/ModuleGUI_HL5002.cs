
using ControlTest.TypeConvertClass;
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
    public class ModuleGUI_HL5002 : ModuleGUIBase
    {
        Tcv tcv = new Tcv();
        Dictionary<object, string> StrEnumResolution = null;
        Dictionary<object, string> StrEnumRevolution = null;
        protected override int GuiStringListNumber { get; } = 11;
        public ModuleGUI_HL5002()
        {
            DeviceName = EnumDeviceName.HL5002;
            Function = "AbsEncoder SSI";
            StrEnumResolution = tcv.GetEnumValueDesDic(typeof(EnumHL5002Resolution));
            StrEnumRevolution = tcv.GetEnumValueDesDic(typeof(EnumHL5002Revolution));
        }


        [TypeConverter(typeof(TypeConvertClass.Tcv))]
        public EnumHL5002Resolution Resolution { get; set; }

        [TypeConverter(typeof(TypeConvertClass.Tcv))]
        public EnumHL5002Revolution Revolution { get; set; }

        public string PresetValue { get; set; } = "0";

        public string ResPara1 { get; set; } = "";
        public string ResPara2 { get; set; } = "";
        public string ResPara3 { get; set; } = "";
        public string ResPara4 { get; set; } = "";
        public string ResPara5 { get; set; } = "";


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

            Enum.TryParse(StrEnumResolution.Where(a => a.Value.Equals(GuiStringList[3])).First().Key.ToString(), out EnumHL5002Resolution resolution);
            Enum.TryParse(StrEnumRevolution.Where(a => a.Value.Equals(GuiStringList[4])).First().Key.ToString(), out EnumHL5002Revolution revolution);
            Resolution = resolution;
            Revolution = revolution;

            PresetValue= GuiStringList[5];


            ResPara1 = GuiStringList[6];
            ResPara2 = GuiStringList[7];
            ResPara3 = GuiStringList[8];
            ResPara4 = GuiStringList[9];
            ResPara5 = GuiStringList[10];  
        }

        protected override void SetProfile()
        {
            GuiStringList.Clear();
            GetListFromStr(GuiStringList, Name, Function, Plug_Sequence, StrEnumResolution[Resolution], StrEnumRevolution[Revolution], PresetValue,
                    ResPara1, ResPara2, ResPara3, ResPara4, ResPara5);

        }
       
    }
}
