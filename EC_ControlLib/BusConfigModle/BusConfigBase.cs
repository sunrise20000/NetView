using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC_ControlLib.BusConfigModle
{
    public class BusConfigBase
    {
      
        public string ShortName { get;protected set; }
        public string Name { get; set; }
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
    }
}
