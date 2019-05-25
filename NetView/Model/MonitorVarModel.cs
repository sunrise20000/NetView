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
            IsInput = true;
            SubModelName = "Just for Test";
            DisplayFormat = EnumDisplayFormat.Dec;
            StatusValue = "123";
        }
        public bool IsInput { get; set; }

        public string SubModelName { get; set; }

        /// <summary>
        /// Hex/Dec/Float
        /// </summary>
        public EnumDisplayFormat DisplayFormat { get; set; }

        public string StatusValue { get; set; }

        public string ModifyValue { get; set; }

       
    }
}
