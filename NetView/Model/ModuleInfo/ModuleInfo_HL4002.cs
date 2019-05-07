using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model.ModuleInfo
{
    public class ModuleInfo_HL4002 : ModuleInfoBase
    {
        public ModuleInfo_HL4002()
        {
            this.Bitsize_DataType = 64;
            this.TotalNum_SubItem = 4;
            this.Type_SubItem = "UINT";
            this.Bitsize_SubItem = 16;
        }
    }
}
