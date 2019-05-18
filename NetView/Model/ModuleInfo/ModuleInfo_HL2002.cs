using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model.ModuleInfo
{
    public class ModuleInfo_HL2002 : ModuleInfoBase
    {
        public ModuleInfo_HL2002()
        {
            DeviceType = EnumDeviceName.HL2002;
            Name = DeviceType.ToString();

            ModuleList.Add(new Module_8()
            {
                DeviceType = EnumDeviceName.HL2002,
                DataTypeOfSubItem = EnumType.USINT,
                IOType = EnumModuleIOType.OUT,
                Name = "HL2002",
                Header = "DO24V_",
                NeedIndex = false,
            });
        }
    }
}
