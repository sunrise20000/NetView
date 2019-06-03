using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ControlTest.BusConfigModle
{
    public class BusGUI_PROFIBUS_DP : BusGUI_Base
    {
        public override string Name { get; protected set; } = "HL6802";

        /// <summary>
        /// 默认文件名
        /// </summary>
        public override string Type { get; protected set; } = "PROFIBUS_DP Coupler V1.0";


        /// <summary>
        /// 是什么类型的总线
        /// </summary>
        public override string ShortName { get; protected set; } = "GSD";
   
    }
}
