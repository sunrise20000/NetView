using NetView.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model.ModuleInfo
{
    public class ModuleInfo_HL4002 : ModuleInfoBase
    {
        public ModuleInfo_HL4002()
        {
            DeviceType = EnumDeviceName.HL4002;
            Name = DeviceType.ToString();
            for (int i = 0; i < 4; i++)
                ModuleList.Add(new Module_16()
                {
                    DeviceType = EnumDeviceName.HL4002,
                    DataTypeOfSubItem = EnumType.UINT,
                    IOType = EnumModuleIOType.OUT,
                    Name = "HL4002",
                    Header = $"AO_420mA_Ch{i+1}_",
                    NeedIndex = true,
                });
        }
    }
}
