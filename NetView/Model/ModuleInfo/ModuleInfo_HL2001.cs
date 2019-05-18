using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model.ModuleInfo
{
    public class ModuleInfo_HL2001 : ModuleInfoBase
    {
        public ModuleInfo_HL2001()
        {

            DeviceType = EnumDeviceName.HL2001;
            Name = DeviceType.ToString();

            ModuleList.Add(new Module_8()
            {
                DeviceType = EnumDeviceName.HL2001,
                DataTypeOfSubItem = EnumType.USINT,
                IOType = EnumModuleIOType.OUT,
                Name = "HL2001",
            });
        }
    }
}
