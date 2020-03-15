using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetView.View
{
	public partial class Windows_CRC : Form
	{
		public Windows_CRC()
		{
			InitializeComponent();
		}

		private void BtnCalc_Click(object sender, EventArgs e)
		{
			try
			{
				var data = textBox_byteArray.Text.Split(' ');
				var byteArr = new List<byte>();
				foreach (var d in data)
				{
					if(d!="")
						byteArr.Add(Convert.ToByte(d, 16));
				}
				var res = CRC16(byteArr.ToArray(),0,byteArr.Count);
				var sb = new StringBuilder();
				for (int i = 0; i < res.Count(); i++)
				{
					sb.Append(string.Format("{0:X2}", res[res.Count()-i-1]));
					sb.Append(" ");
				}
				textBox_result.Text = sb.ToString();
			}
			catch
			{
				MessageBox.Show("Please check your data format is correct");
			}
		}

		public byte[] CRC16(byte[] dataSrc, int offset, int datalength)
		{
			if (offset + datalength > dataSrc.Length)
				throw new Exception("data out of range");
			List<byte> list = new List<byte>();
			for (int i = offset; i < offset + datalength; i++)
			{
				list.Add(dataSrc[i]);
			}
			var data = list.ToArray();
			int len = data.Length;
			if (len > 0)
			{
				ushort crc = 0xFFFF;

				for (int i = 0; i < len; i++)
				{
					crc = (ushort)(crc ^ (data[i]));
					for (int j = 0; j < 8; j++)
					{
						crc = (crc & 1) != 0 ? (ushort)((crc >> 1) ^ 0xA001) : (ushort)(crc >> 1);
					}
				}
				byte hi = (byte)((crc & 0xFF00) >> 8);  //高位置
				byte lo = (byte)(crc & 0x00FF);         //低位置

				return new byte[] { hi, lo };
			}
			return new byte[] { 0, 0 };
		}
	}
}
