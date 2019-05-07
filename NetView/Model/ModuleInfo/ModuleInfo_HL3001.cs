using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model.ModuleInfo
{
    public class ModuleInfo_HL3001 : ModuleInfoBase
    {
        public ModuleInfo_HL3001()
        {
            this.Bitsize_DataType = 64;
            this.TotalNum_SubItem = 4;
            this.Type_SubItem = "UINT";
            this.Bitsize_SubItem = 16;
        }
    }
}
