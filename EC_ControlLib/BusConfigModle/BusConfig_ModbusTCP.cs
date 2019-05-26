using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EC_ControlLib.BusConfigModle
{
    public class BusConfig_ModbusTCP : BusConfigBase
    {
        public BusConfig_ModbusTCP()
        {
            this.Name = "HL6804";
            this.Type = "ModbusTCP Coupler V1.0";
            this.ShortName = "MT excel";
        }
        protected BusConfig_ModbusTCP(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
