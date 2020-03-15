using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetView.Definations;

namespace NetView.Model.DisplayFormat
{
    class DisplayFormatHex : DisplayFormatBase
    {
		public DisplayFormatHex(UInt32 rawData) : base(rawData)
		{
			Base = 16;
		}
		public override string GetString()
		{
			var sb = new StringBuilder();
			sb.Append("0x{0:X");
			sb.Append((this.ByteLen*2).ToString());
			sb.Append("}");
			return string.Format(sb.ToString(), RawData);
		}
	}
}
