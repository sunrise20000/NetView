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
    public class BusConfig_PROFIBUS_DP : BusConfigBase
    {
        public override EnumBusType BusType { get; } = EnumBusType.PROFIBUS_DP;
        public override string Name { get; protected set; } = "HL6802";

        /// <summary>
        /// 默认文件名
        /// </summary>
        public override string Type { get; protected set; } = "PROFIBUS_DP Coupler V1.0";

        /// <summary>
        /// 是什么类型的总线
        /// </summary>
        public override string ShortName { get; protected set; } = "GSD";
        public BusConfig_PROFIBUS_DP()
        {
        }
        protected BusConfig_PROFIBUS_DP(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
