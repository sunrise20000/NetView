using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;

namespace NetView.Model
{
    public class ComportSettingModel
    {
        public string ComportName { get; set; }
        public int Baudrate { get; set; }

        public byte Data { get; set; }

        public Parity Parity { get; set; }

        public StopBits Stop { get; set; }
       
    }
}
