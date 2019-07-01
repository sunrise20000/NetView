using ControllerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerLib.Ethercat.ModuleConfigModle.ConfigSubInfo
{
   
    public class ModuleConfigBase
    {
        public EnumDeviceName DeviceType { get; set; }
        public string Name{ get; set; }
        public EnumModuleIoType IOType { get; set; }

        public int BitSize { get; protected set; }

        public UInt32 RawData { get; set; }  //存储当前值

    }
}
