using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ControlTest.BusConfigModle
{
    [Serializable()]
    public class DeviceNet : BusCfgBase
    {
        public override string ShortName { get; protected set; } = "HL6807";

        /// <summary>
        /// 默认文件名
        /// </summary>
        public override string Name { get; set; } = "DeviceNet Coupler V1.0";

        /// <summary>
        /// 是什么类型的总线
        /// </summary>
        public override string Type { get; set; } = "DE EDS";
        public DeviceNet()
        {
          
        }
        protected DeviceNet(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
