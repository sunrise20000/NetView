using NetView.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model.ModuleInfo
{
   

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
