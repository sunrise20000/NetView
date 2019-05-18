using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model.ModuleInfo
{
    public class ModuleInfo_HL4001 : ModuleInfoBase
    {
        public ModuleInfo_HL4001()
        {
            DeviceType = EnumDeviceName.HL4001;
            Name = DeviceType.ToString();
            for (int i = 0; i < 4; i++)
                ModuleList.Add(new Module_16()
                {
                    DeviceType = EnumDeviceName.HL4001,
                    DataTypeOfSubItem = EnumType.UINT,
                    IOType = EnumModuleIOType.IN,
                    Name = "HL4001",
                });
        }
    }
}
