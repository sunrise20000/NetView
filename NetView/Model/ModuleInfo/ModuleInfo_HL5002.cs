using NetView.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model.ModuleInfo
{
    public class ModuleInfo_HL5002 : ModuleInfoBase
    {
        public ModuleInfo_HL5002()
        {
            DeviceType = EnumDeviceName.HL5002;
            Name = DeviceType.ToString();
            ModuleList.Add(new Module_32()
            {
                DeviceType = EnumDeviceName.HL5001,
                DataTypeOfSubItem = EnumType.UDINT,
                IOType = EnumModuleIOType.IN,
                Name = "HL5002",
                Header = "AbsEncodeSSI_",
                NeedIndex = false,
            });

            ModuleList.Add(new Module_16()
            {
                DeviceType = EnumDeviceName.HL5001,
                DataTypeOfSubItem = EnumType.UINT,
                IOType = EnumModuleIOType.OUT,
                Name = "HL5002",
                Header = "AbsEncode_Cmd_",
                NeedIndex = false,
            });
           
        }
    }
}
