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
		public DisplayFormatFloat(UInt32 rawData) : base(rawData)
		{
			Base = 10;
		}
    }
}
