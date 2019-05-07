using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model.ModuleInfo
{
    public class ModuleInfo_HL2002 : ModuleInfoBase
    {
        public ModuleInfo_HL2002()
        {
            this.Bitsize_DataType = 8;
            this.TotalNum_SubItem = 1;
            this.Type_SubItem = "USINT";
            this.Bitsize_SubItem = 8;
        }
    }
}
