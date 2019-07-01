using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetView.Definations;

namespace NetView.Model.DisplayFormat
{
    class DisplayFormatFloat : DisplayFormatBase
    {
        public override uint FromString(string StrDisplay, EnumType DataType)
        {
            var V = GenUint32FromBit((int)DataType);
            this.RawData = Convert.ToUInt32(StrDisplay, 10) & V;
            return this.RawData;
        }
        public override string GetString(uint RawData, EnumType DataType)
        {
            StringBuilder strMask = new StringBuilder();
            for (int i = 0; i < (int)DataType / 4; i++)
            {
                strMask.Append("F");
            }
            var V = Convert.ToUInt32(strMask.ToString(), 16);
            return string.Format("{0:X2}", RawData & V);
        }
    }
}
