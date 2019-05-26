using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC_ControlLib.BusConfigModle
{
    public class BusConfig_DeviceNet : BusConfigBase
    {
        public BusConfig_DeviceNet()
        {
            this.Name = "HL6807";
            this.Type = "DeviceNet Coupler V1.0";
            this.ShortName = "DE EDS";
        }
    }
}
