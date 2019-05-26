using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EC_ControlLib.BusConfigModle
{
    public class BusConfig_ModbusRTU : BusConfigBase
    {
        public BusConfig_ModbusRTU()
        {
            this.Name = "HL6801";
            this.Type = "ModbusRTU Coupler V1.0";
            this.ShortName = "MB excel";
        }
        protected BusConfig_ModbusRTU(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
