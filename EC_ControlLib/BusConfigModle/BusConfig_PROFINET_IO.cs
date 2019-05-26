using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC_ControlLib.BusConfigModle
{
    public class BusConfig_PROFINET_IO : BusConfigBase
    {
        public BusConfig_PROFINET_IO()
        {
            this.Name = "HL6803";
            this.Type = "PROFINET-IO Coupler V1.0";
            this.ShortName = "PN xml";
        }
    }
}
