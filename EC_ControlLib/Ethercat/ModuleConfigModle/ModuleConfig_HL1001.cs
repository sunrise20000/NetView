using ControllerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC_ControlLib.Ethercat.ModuleConfigModle
{
    public class ModuleConfig_HL1001 : ModuleConfigModleBase
    {
        byte[] TypeList = new byte[] {0x00, 0x01, 0x03,0x07, 0x0F,0x1F,0x3F,0x7F,0xFF};
        public ModuleConfig_HL1001()
        {
            DeviceName = EnumDeviceName.HL1001;
        }
        public string Function { get; private set; } = "DI8xDC24V";

        public byte TypeValue {
            get;
            set;
        }

        public void SetTypeByIndex(int Index)
        {
            this.TypeValue = TypeList[Index];
        }


    }
}
