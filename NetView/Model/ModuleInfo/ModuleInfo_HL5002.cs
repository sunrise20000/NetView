using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model.ModuleInfo
{
    public class ModuleInfo_HL5002 : ModuleInfoBase
    {
        public ModuleInfo_HL5002()
        {
            this.Bitsize_DataType = 32;
            this.TotalNum_SubItem = 1;
            this.Type_SubItem = "UINT";
            this.Bitsize_SubItem = 32;
        }
    }
}
