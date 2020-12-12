using NetView.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Config
{
    public class DeviceConfigEntry
    {
		public string Name { get; set; }
		public string Icon { get; set; }
        public DeviceConfigEntry[] Child { get; set; }
    }
}
