using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EC_ControlLib.BusConfigModle
{
    [Serializable()]
    public class BusConfig_PROFINET_IO : BusConfigBase
    {
        public override string ShortName { get; protected set; } = "HL6803";

        /// <summary>
        /// 默认文件名
        /// </summary>
        public override string Name { get; set; } = "PROFINET-IO Coupler V1.0";

        /// <summary>
        /// 是什么类型的总线
        /// </summary>
        public override string Type { get; set; } = "PN xml";
        public BusConfig_PROFINET_IO()
        {
        }
        protected BusConfig_PROFINET_IO(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
