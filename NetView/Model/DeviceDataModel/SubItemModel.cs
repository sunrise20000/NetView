using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model.DeviceDataModel
{
    public class SubItemModelBaseForDataType
    {
        public int SubIdx { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string BitSize { get; set; }

        public int BitOffs { get; set; }

        public string Access { get; private set; } = "ro";

        public string Category { get; private set; } = "o";

       
    }
}
