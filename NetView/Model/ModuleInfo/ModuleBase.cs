using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model.ModuleInfo
{
    public enum EnumModuleIOType
    {
        IN=1,
        OUT,
    }
    public enum EnumType
    {
        USINT = 8,
        UINT = 16,
        UDINT = 32,
    }

    public class ModuleBase
    {
        public EnumDeviceName DeviceType { get; set; }
        public string Name{ get; set; }
        public EnumModuleIOType IOType { get; set; }


        public int BitSize { get; protected set; }

        public EnumType DataTypeOfSubItem { get; set; }
      
        public string Header { get; set; }

        public bool NeedIndex { get; set; }

    }
}
