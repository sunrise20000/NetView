using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EC_ControlLib.BusConfigModle
{
    [Serializable()]
    public class BusConfig_EtherCAT : BusConfigBase
    {
        public override string ShortName { get; protected set; } = "HL6805";

        /// <summary>
        /// 默认文件名
        /// </summary>
        public override string Name { get; set; } = "EtherCAT Coupler V1.0";

        /// <summary>
        /// 是什么类型的总线
        /// </summary>
        public override string Type { get; set; } = "EC xml";
        public BusConfig_EtherCAT()
        {
        }
        protected BusConfig_EtherCAT(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
