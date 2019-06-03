using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ControlTest.BusConfigModle
{
    public class BusGUI_Base
    {
        /// <summary>
        /// 主要是为了区分保存为哪种输出文件
        /// </summary>
        public virtual string ShortName { get;protected set; }

        public virtual string Name { get; protected set; }
        /// <summary>
        /// 是什么类型的总线
        /// </summary>
        public virtual string Type { get; protected set; }

        public string Function { get; private set; } = "HURRY RomoteIO Slaves";
        public string Corpration { get; private set; } = "Shanghai Hurry Electronics Tech.Co,.Ltd";
        public string Author { get;private set; } = "Hurry Lee";
        public string Date {
            get {
                var DtNow = DateTime.Now;
                 return  $"{DtNow.Year}_{DtNow.Month}_{DtNow.Day}";
            }
            private set { }
        }
        public string Email { get; private set; } = "hurry@hurry-tech.cn";

    }
}
