using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubBusContrainer.Model
{
    public class ModuleAddedArgs
    {
        public bool IsAdd { get; set; }
        public ControlTest.ControlBase Module { get; set; }
    }
}
