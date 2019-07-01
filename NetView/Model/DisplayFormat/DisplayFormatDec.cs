using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetView.Definations;

namespace NetView.Model.DisplayFormat
{
    class DisplayFormatDec : DisplayFormatBase
    {
        public override uint FromString(string StrDisplay, EnumType DataType)
        {
            var V = GenUint32FromBit((int)DataType);
            this.RawData = Convert.ToUInt32(StrDisplay, 10) & V;
            return this.RawData; 
        }
        public override string GetString(uint RawData, EnumType DataType)
        {
            var V = GenUint32FromBit((int)DataType);
            return string.Format("{0}", RawData & V);
        }
    }
}
