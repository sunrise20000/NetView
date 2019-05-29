
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
    public class ModuleCfg_HL4001 : ModuleCfgModleBase
    {
        protected override int GuiStringListNumber { get; } = 11;
        public ModuleCfg_HL4001()
        {
            DeviceName = EnumDeviceName.HL4001;
        }
        [TypeConverter(typeof(Tcv))]
        public EnumHL4001Type Ch1_Output_Type { get; set; }
        [TypeConverter(typeof(Tcv))]
        public EnumHL4001Accuracy Ch1_Accuracy { get; set; }

        [TypeConverter(typeof(Tcv))]
        public EnumHL4001Type Ch2_Output_Type { get; set; }
        [TypeConverter(typeof(Tcv))]
        public EnumHL4001Accuracy Ch2_Accuracy { get; set; }

        [TypeConverter(typeof(Tcv))]
        public EnumHL4001Type Ch3_Output_Type { get; set; }
        [TypeConverter(typeof(Tcv))]
        public EnumHL4001Accuracy Ch3_Accuracy { get; set; }

        [TypeConverter(typeof(Tcv))]
        public EnumHL4001Type Ch4_Output_Type { get; set; }
        [TypeConverter(typeof(Tcv))]
        public EnumHL4001Accuracy Ch4_Accuracy { get; set; }


        public override void FromString(params string[] ParaList)
        {
            EnumHL4001Type type;
            EnumHL4001Accuracy acc;
            if (ParaList.Length != GuiStringListNumber)
                throw new Exception($"Wrong para number when parse {DeviceName.ToString()} formstring");
            Name = GuiStringList[0];
            Function = GuiStringList[1];
            Plug_Sequence = GuiStringList[2];

            Enum.TryParse(GuiStringList[3], out type);
            Enum.TryParse(GuiStringList[4], out acc);
            Ch1_Output_Type = type;
            Ch1_Accuracy = acc;

            Enum.TryParse(GuiStringList[5], out type);
            Enum.TryParse(GuiStringList[6], out acc);
            Ch2_Output_Type = type;
            Ch2_Accuracy = acc;


            Enum.TryParse(GuiStringList[7], out type);
            Enum.TryParse(GuiStringList[8], out acc);
            Ch3_Output_Type = type;
            Ch3_Accuracy = acc;

            Enum.TryParse(GuiStringList[9], out type);
            Enum.TryParse(GuiStringList[10], out acc);
            Ch4_Output_Type = type;
            Ch4_Accuracy = acc;

        }

        protected override void SetProfile()
        {
            GuiStringList.Clear();
            GetListFromStr(GuiStringList,
                Name, "AOx4Ch. 0-10V",
                Function,
                Ch1_Output_Type.ToString(),
                Ch1_Accuracy.ToString(),

                Ch2_Output_Type.ToString(),
                Ch2_Accuracy.ToString(),

                Ch3_Output_Type.ToString(),
                Ch3_Accuracy.ToString(),

                Ch4_Output_Type.ToString(),
                Ch4_Accuracy.ToString());

        }
    }
}
