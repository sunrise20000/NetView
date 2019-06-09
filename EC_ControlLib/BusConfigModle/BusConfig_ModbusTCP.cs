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
    public class BusConfig_ModbusTCP : BusConfigBase
    {
        public override EnumBusType BusType { get; } = EnumBusType.ModbusTCP;
        public override string Name { get; protected set; } = "HL6804";

        /// <summary>
        /// 默认文件名
        /// </summary>
        public override string Type { get; protected set; } = "ModbusTCP Coupler V1.0";

        /// <summary>
        /// 是什么类型的总线
        /// </summary>
        public override string ShortName { get; protected set; } = "MT excel";
        public BusConfig_ModbusTCP()
        {
        }
        protected BusConfig_ModbusTCP(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
