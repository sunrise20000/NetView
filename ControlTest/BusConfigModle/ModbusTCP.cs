using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ControlTest.BusConfigModle
{
    [Serializable()]
    public class ModbusTCP : BusCfgBase
    {
        public override string ShortName { get; protected set; } = "HL6804";

        /// <summary>
        /// 默认文件名
        /// </summary>
        public override string Name { get; set; } = "ModbusTCP Coupler V1.0";

        /// <summary>
        /// 是什么类型的总线
        /// </summary>
        public override string Type { get; set; } = "MT excel";
        public ModbusTCP()
        {
        }
        protected ModbusTCP(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
