using ControlTest.TypeConvertClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace ControlTest.ModuleConfigModle
{
    public class ModuleGUI_HL1001 : ModuleGUIBase
    {


        protected override int GuiStringListNumber { get; } = 4;
        Tcv tcv = new Tcv();
        Dictionary<object, string> StrEnumType = null;
        public ModuleGUI_HL1001()
        {
            DeviceName = EnumDeviceName.HL1001;
            Function = "DI8xDC24V";
            StrEnumType = tcv.GetEnumValueDesDic(typeof(EnumHL1001Type));
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
            GuiStringList.Clear();
            foreach (var it in ParaList)
                GuiStringList.Add(it);

            Name =GuiStringList[0];
            Function = GuiStringList[1];
            Plug_Sequence= GuiStringList[2];   
            
            Enum.TryParse(StrEnumType.Where(a => a.Value.Equals(GuiStringList[3])).FirstOrDefault().Key.ToString(), out EnumHL1001Type type);        
            Type = type;   
        }

        protected override void SetProfile()
        {
            GuiStringList.Clear();
            GetListFromStr(GuiStringList, Name, Function, Plug_Sequence, StrEnumType[Type]);
        }  
    }
}
