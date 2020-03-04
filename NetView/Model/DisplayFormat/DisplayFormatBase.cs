using NetView.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model.DisplayFormat
{
    public class DisplayFormatBase
    {
		public int Base { get; protected set; }
        protected UInt32 RawData = 0;
		public DisplayFormatBase(UInt32 rawData)
		{
			this.RawData = rawData;
		}
		public void SetRawDataFromInt(UInt32 rawData)
		{
			this.RawData = rawData;
		}
		public void SetRawDataFromString(string strData,int BaseFrom)
		{
			this.RawData = Convert.ToUInt32(strData,BaseFrom);
		}
		public virtual string GetString()
        {
			return RawData.ToString();
		}
		public UInt32 GetRawData()
		{
			return this.RawData;
		}

    }
}
