using NetView.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model.ModuleInfo
{
    public class ModuleInfo_HL5001 : ModuleInfoBase
    {
        public ModuleInfo_HL5001()
        {
            DeviceType = EnumDeviceName.HL5001;
            Name = DeviceType.ToString();
      
            ModuleList.Add(new Module_32()
            {
                DeviceType = EnumDeviceName.HL5001,
                DataTypeOfSubItem = EnumType.UDINT,
                IOType = EnumModuleIOType.IN,
                Name = "HL5001",
                Header= "Counter600_H_"

            });
            ModuleList.Add(new Module_32()
            {
                DeviceType = EnumDeviceName.HL5001,
                DataTypeOfSubItem = EnumType.UDINT,
                IOType = EnumModuleIOType.IN,
                Name = "HL5001",
                Header = "Counter600_L_"
            });
        }
    }
}
