using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ControlTest.BusConfigModle
{
    [Serializable()]
    public class CANopen : BusCfgBase
    {
        public override string ShortName { get; protected set; } = "HL6806";

        /// <summary>
        /// 默认文件名
        /// </summary>
        public override  string Name { get; set; }= "CANopen Coupler V1.0";

        /// <summary>
        /// 是什么类型的总线
        /// </summary>
        public override string Type { get; set; } = "CA EDS";
        public CANopen()
        {
           
        }
        protected CANopen(SerializationInfo info, StreamingContext context) : base(info,context)
        {

        }
        
    }
}
