using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EC_ControlLib.BusConfigModle
{
    public class BusConfig_EtherCAT : BusConfigBase
    {
        public BusConfig_EtherCAT()
        {
            this.Name = "HL6805";
            this.Type = "EtherCAT Coupler V1.0";
            this.ShortName = "EC xml";
        }
        protected BusConfig_EtherCAT(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
