using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model.ModuleInfo
{
    public class ModuleInfoBase
    {
        public int Bitsize_DataType { get; set; }

        public string Type_SubItem { get; set; }

        public int TotalNum_SubItem { get; set; }

        public int Bitsize_SubItem { get; set; }

        public string Name { get; set; }
    }
}
