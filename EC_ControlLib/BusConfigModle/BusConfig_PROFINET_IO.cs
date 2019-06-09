using ControllerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ControllerLib.BusConfigModle
{
    [Serializable()]
    public class BusConfig_PROFINET_IO : BusConfigBase
    {
        public override EnumBusType BusType { get; } = EnumBusType.PROFINET_IO;
        public override string Name { get; protected set; } = "HL6803";

        /// <summary>
        /// 默认文件名
        /// </summary>
        public override string Type { get; protected set; } = "PROFINET-IO Coupler V1.0";

        /// <summary>
        /// 是什么类型的总线
        /// </summary>
        public override string ShortName { get; protected set; } = "PN xml";
        public BusConfig_PROFINET_IO()
        {
        }
        protected BusConfig_PROFINET_IO(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
