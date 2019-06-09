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
    public class BusConfig_CANopen : BusConfigBase
    {
        public override string Name { get; protected set; } = "HL6806";
        public override EnumBusType BusType { get; } = EnumBusType.CANopen;
        /// <summary>
        /// 默认文件名
        /// </summary>
        public override  string Type { get; protected set; }= "CANopen Coupler V1.0";

        /// <summary>
        /// 是什么类型的总线
        /// </summary>
        public override string ShortName { get; protected set; } = "CA EDS";
        public BusConfig_CANopen()
        {
           
        }
        protected BusConfig_CANopen(SerializationInfo info, StreamingContext context) : base(info,context)
        {

        }
        
    }
}
