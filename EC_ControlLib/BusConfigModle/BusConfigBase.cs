using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace EC_ControlLib.BusConfigModle
{
    [Serializable()]
    public class BusConfigBase : ISerializable
    {
        /// <summary>
        /// 主要是为了区分保存为哪种输出文件
        /// </summary>
        public string ShortName { get;protected set; }

        /// <summary>
        /// 默认文件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是什么类型的总线
        /// </summary>
        public string Type { get; set; }
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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Type", Type);
        }

        protected BusConfigBase(SerializationInfo info, StreamingContext context)
        {
            Type = info.GetString("Type");
        }
        public BusConfigBase() { }
    }
}
