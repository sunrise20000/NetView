using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model.ModuleInfo
{
    public class Module_32 : ModuleBase
    {
        public Module_32()
        {
            BitSize = 32;
            DataTypeOfSubItem = EnumType.UDINT;
        }
    }
}
