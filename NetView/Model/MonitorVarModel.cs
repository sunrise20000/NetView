using NetView.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model
{
    public class MonitorVarModel
    {
        public MonitorVarModel()
        {
            IoType = EnumModuleIOType.IN;
            SubModelName = "HL1001";
            DisplayFormat = EnumDisplayFormat.Dec;
            CurValue = "CurValue";
            ModifyValue = "ModifyValue";
        }
        public EnumModuleIOType IoType { get; set; }

        public string SubModelName { get; set; }

        /// <summary>
        /// Hex/Dec/Float
        /// </summary>
        public EnumDisplayFormat DisplayFormat { get; set; }

        public string CurValue { get; set; }

        public string ModifyValue { get; set; }

       
    }
}
