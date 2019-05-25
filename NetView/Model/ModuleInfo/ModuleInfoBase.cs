using NetView.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model.ModuleInfo
{
   

    public class ModuleInfoBase
    {
        public EnumDeviceName DeviceType { get; set; }
        public string Name { get; set; }
        public List<ModuleBase> ModuleList { get; }= new List<ModuleBase>();

    }
}
