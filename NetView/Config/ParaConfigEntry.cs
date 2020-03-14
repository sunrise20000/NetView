using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Config
{
	public class ParaConfigEntry
	{
		public uint ReceiveTimeout
		{
			get; set;
		}
		public string TransmitDelay
		{
			get; set;
		}

		public uint BandarateIndex { get; set; }

		public uint  Data { get; set; }

		public uint ParityIndex { get; set; }

		public uint StopIndex { get; set; }
		//public 
	}
}
