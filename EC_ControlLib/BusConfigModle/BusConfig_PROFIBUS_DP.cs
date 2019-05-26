using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC_ControlLib.BusConfigModle
{
    public class BusConfig_PROFIBUS_DP : BusConfigBase
    {
        public BusConfig_PROFIBUS_DP()
        {
            this.Name = "HL6802";
            this.Type = "PROFIBUS_DP Coupler V1.0";
            this.ShortName = "GSD";
        }
    }
}
