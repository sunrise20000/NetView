using ControllerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC_ControlLib.Ethercat.ModuleConfigModle
{
    public class ModuleConfigModleBase
    {
        public EnumDeviceName DeviceName { get; protected set; }

        public int LocalIndex { get; set; }

        public int GlobalIndex { get; set; }

    }
}
