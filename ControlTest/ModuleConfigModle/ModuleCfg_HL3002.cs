
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
    public class ModuleCfg_HL3002 : ModuleCfgModleBase
    {
        protected override int GuiStringListNumber { get; } = 11;
        public ModuleCfg_HL3002()
        {
       
            DeviceName = EnumDeviceName.HL3002;       
        }

        [TypeConverter(typeof(Tcv))]
        public EnumHL3002Type Ch1_Input_Type { get; set; }
        [TypeConverter(typeof(Tcv))]
        public EnumHL3002Accuracy Ch1_Accuracy { get; set; }

        [TypeConverter(typeof(Tcv))]
        public EnumHL3002Type Ch2_Input_Type { get; set; }
        [TypeConverter(typeof(Tcv))]
        public EnumHL3002Accuracy Ch2_Accuracy { get; set; }

        [TypeConverter(typeof(Tcv))]
        public EnumHL3002Type Ch3_Input_Type { get; set; }
        [TypeConverter(typeof(Tcv))]
        public EnumHL3002Accuracy Ch3_Accuracy { get; set; }

        [TypeConverter(typeof(Tcv))]
        public EnumHL3002Type Ch4_Input_Type { get; set; }
        [TypeConverter(typeof(Tcv))]
        public EnumHL3002Accuracy Ch4_Accuracy { get; set; }


        public override void FromString(params string[] ParaList)
        {
            EnumHL3002Type type;
            EnumHL3002Accuracy acc;
            if (ParaList.Length != GuiStringListNumber)
                throw new Exception($"Wrong para number when parse {DeviceName.ToString()} formstring");
            Name = GuiStringList[0];
            Function = GuiStringList[1];
            Plug_Sequence = GuiStringList[2];

            Enum.TryParse(GuiStringList[3], out type);
            Enum.TryParse(GuiStringList[4], out acc);
            Ch1_Input_Type = type;
            Ch1_Accuracy = acc;

            Enum.TryParse(GuiStringList[5], out type);
            Enum.TryParse(GuiStringList[6], out acc);
            Ch2_Input_Type = type;
            Ch2_Accuracy = acc;


            Enum.TryParse(GuiStringList[7], out type);
            Enum.TryParse(GuiStringList[8], out acc);
            Ch3_Input_Type = type;
            Ch3_Accuracy = acc;

            Enum.TryParse(GuiStringList[9], out type);
            Enum.TryParse(GuiStringList[10], out acc);
            Ch4_Input_Type = type;
            Ch4_Accuracy = acc;

        }

        protected override void SetProfile()
        {
            GuiStringList.Clear();

            GetListFromStr(GuiStringList,
                Name, "AIx4Ch. 4-20mA",
                Function,
                Ch1_Input_Type.ToString(),
                Ch1_Accuracy.ToString(),

                Ch2_Input_Type.ToString(),
                Ch2_Accuracy.ToString(),

                Ch3_Input_Type.ToString(),
                Ch3_Accuracy.ToString(),

                Ch4_Input_Type.ToString(),
                Ch4_Accuracy.ToString());

        }
    }
}
