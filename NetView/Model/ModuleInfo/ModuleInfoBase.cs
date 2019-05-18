using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model.ModuleInfo
{
    public enum EnumDeviceName
    {
        HL1001,
        HL2001,
        HL2002,
        HL2003,
        HL3001,
        HL3002,
        HL4001,
        HL4002,
        HL5001,
        HL5002,
    }

    public class ModuleInfoBase
    {
        public EnumDeviceName DeviceType { get; set; }
        public string Name { get; set; }
        public List<ModuleBase> ModuleList { get; }= new List<ModuleBase>();
    }
}
