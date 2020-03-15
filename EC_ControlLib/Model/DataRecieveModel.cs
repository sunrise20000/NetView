using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerLib.Model
{
	public class DataRecieveModel
	{
		public DataRecieveModel(byte ByteCount, UInt32 RawValue)
		{
			this.ByteCount = ByteCount;
			this.RawValue = RawValue;
		}
		public UInt32 RawValue { get; set; }

		public byte ByteCount { get; set; }


	}
}
