using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model
{

    public class AvilableDeviceModel
    {
        public string BusType
        {
            get;set;
        }
        public string Category
        {
            get;set;
        }
        public string DeviceName { get; set; }
        public string NodeTip { get; set; }

        public static bool operator ==(AvilableDeviceModel m1, AvilableDeviceModel m2)
        {
            return  m1.BusType.Equals(m2.BusType) &&
                    m1.Category.Equals(m2.Category) &&
                    m1.DeviceName.Equals(m2.DeviceName);
        }
        public static bool operator !=(AvilableDeviceModel m1, AvilableDeviceModel m2)
        {
            return !(m1==m2);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
