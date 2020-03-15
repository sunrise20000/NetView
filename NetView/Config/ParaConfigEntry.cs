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
		public uint TransmitDelay
		{
			get; set;
		}

		public uint Bandarate { get; set; }

		public uint  Data { get; set; }

		public uint Parity { get; set; }

		public uint Stop { get; set; }
		//public 
	}
}
