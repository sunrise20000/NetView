using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ControlTest.BusConfigModle
{
    [Serializable()]
    public class PROFIBUS_DP : BusCfgBase
    {
        public override string ShortName { get; protected set; } = "HL6802";

        /// <summary>
        /// 默认文件名
        /// </summary>
        public override string Name { get; set; } = "PROFIBUS_DP Coupler V1.0";

        /// <summary>
        /// 是什么类型的总线
        /// </summary>
        public override string Type { get; set; } = "GSD";
        public PROFIBUS_DP()
        {
        }
        protected PROFIBUS_DP(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
