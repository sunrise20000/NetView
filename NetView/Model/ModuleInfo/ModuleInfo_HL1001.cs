using NetView.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model.ModuleInfo
{
    public class ModuleInfo_HL1001 : ModuleInfoBase
    {

        public ModuleInfo_HL1001()
        {
            
            DeviceType = EnumDeviceName.HL1001;
            Name = DeviceType.ToString();

            ModuleList.Add(new Module_8() {
                DeviceType = EnumDeviceName.HL1001,
                DataTypeOfSubItem = EnumType.USINT,
                IOType = EnumModuleIOType.IN,
                Name = "HL1001",
                Header = "DI24V_",
                NeedIndex = false,
            });
        }
    }
}
