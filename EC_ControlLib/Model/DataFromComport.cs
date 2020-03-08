using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerLib.Model
{
	public class DataFromComport
	{
		private List<byte> data = new List<byte>();
		bool Pc2Card = true;
		private StringBuilder sb = new StringBuilder();
		public DataFromComport(List<byte> data, bool pc2Card=true)
		{
			this.data = data;
			this.Pc2Card = pc2Card;
		}
		public DataFromComport(byte[] data, bool pc2Card = true)
		{
			this.data = new List<byte>(data);
			this.Pc2Card = pc2Card;
		}

		public string TimeSend
		{
			get {
				var dir = Pc2Card ? "PC ——>>>> Card" : "PC <<<<—— Card";
				return $"{DateTime.Now.ToString("MM-dd HH:mm:ss-fff")}\t\t{dir}";
				//yyyyMMddHHmmssfff
			}
		}

		public string Content
		{
			get {
				sb.Clear();
				foreach(var b in data)
				{
					sb.Append(string.Format("{0:X2} ", b));
			
				}
				return sb.ToString();
			}
		}

		public string TotalMsg {
			get {
				return $"{TimeSend} {Content}";
			}
		}

	}
}
