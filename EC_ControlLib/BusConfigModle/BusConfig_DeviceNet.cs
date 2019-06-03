using ControllerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EC_ControlLib.BusConfigModle
{
    [Serializable()]
    public class BusConfig_DeviceNet : BusConfigBase
    {
        public override EnumBusType BusType { get; } = EnumBusType.DeviceNet;
        public override string Name { get; protected set; } = "HL6807";

        /// <summary>
        /// 默认文件名
        /// </summary>
        public override string Type { get; protected set; } = "DeviceNet Coupler V1.0";

        /// <summary>
        /// 是什么类型的总线
        /// </summary>
        public override string ShortName { get; protected set; } = "DE EDS";
        public BusConfig_DeviceNet()
        {
          
        }
        protected BusConfig_DeviceNet(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
