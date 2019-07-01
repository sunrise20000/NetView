using NetView.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model.DisplayFormat
{
    class DisplayFormatBase
    {

        protected UInt32 RawData = 0;
        protected UInt32 GenUint32FromBit(int BitSize)
        {
            StringBuilder strMask = new StringBuilder();
            for (int i = 0; i < (int)BitSize / 4; i++)
            {
                strMask.Append("F");
            }
            return Convert.ToUInt32(strMask.ToString(), 16);
        }
        public EnumDisplayFormat DataFormat { get; } = EnumDisplayFormat.Hex;

        public virtual string GetString(UInt32 RawData, EnumType DataType)
        {
            throw new NotImplementedException();
        }

        public virtual UInt32 FromString(string StrDisplay, EnumType DataType)
        {
            throw new NotImplementedException();
        }

    }
}
