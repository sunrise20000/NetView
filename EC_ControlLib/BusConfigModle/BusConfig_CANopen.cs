using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EC_ControlLib.BusConfigModle
{
    public class BusConfig_CANopen : BusConfigBase
    {
        public BusConfig_CANopen()
        {
            this.Name = "HL6806";
            this.Type = "CANopen Coupler V1.0";
            this.ShortName = "CA EDS";
        }
        protected BusConfig_CANopen(SerializationInfo info, StreamingContext context) : base(info,context)
        {

        }
        
    }
}
