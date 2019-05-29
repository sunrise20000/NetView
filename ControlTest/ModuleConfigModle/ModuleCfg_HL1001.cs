using ControlTest.TypeConvertClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace ControlTest.ModuleConfigModle
{
    public class ModuleCfg_HL1001 : ModuleCfgModleBase
    {


        protected override int GuiStringListNumber { get; } = 4;

        public ModuleCfg_HL1001()
        {
            DeviceName = EnumDeviceName.HL1001;      
        }

        [TypeConverter(typeof(TypeConvertClass.Tcv))]
        public EnumHL1001Type Type
        {
            get;
            set;
        } = EnumHL1001Type.E1;

        public override void FromString(params string[] ParaList)
        {
            if (ParaList.Length != GuiStringListNumber)
                throw new Exception($"Wrong para number when parse {DeviceName.ToString()} formstring");
            Name=GuiStringList[0];
            Function = GuiStringList[1];
            Plug_Sequence= GuiStringList[2];
            Enum.TryParse(GuiStringList[3],out EnumHL1001Type type);
         
            Type = type;   
        }

        protected override void SetProfile()
        {
            GuiStringList.Clear();
            GetListFromStr(GuiStringList, Name, "DI8xDC24V", Function, Type.ToString());
        }  
    }
}
