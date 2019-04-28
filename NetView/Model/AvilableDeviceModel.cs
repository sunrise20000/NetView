using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model
{
    public enum EnumBusType
    {
        ModbusRTU,
        Profibus,
        FreeSerial,
    }

    public enum EnumBusRelationship
    {
        Slave,
        Master,
    }
    public class AvilableDeviceModel
    {
        private EnumBusType busType = EnumBusType.ModbusRTU;

        private EnumBusRelationship busRelationShaip = EnumBusRelationship.Slave;
        public string BusType
        {
            set {
                Enum.TryParse(value, out busType);
            }
            get { return busType.ToString(); }
        }
        public string BusRelationship
        {
            set
            {
                Enum.TryParse(value, out busRelationShaip);
            }
            get { return busRelationShaip.ToString(); }
        }
        public string CompanyName { get; set; }
        public string DeviceName { get; set; }
        public string NodeTip { get; set; }

        public static bool operator ==(AvilableDeviceModel m1, AvilableDeviceModel m2)
        {
            return  m1.BusType.Equals(m2.BusType) &&
                    m1.BusRelationship.Equals(m2.BusRelationship) &&
                    m1.CompanyName.Equals(m2.CompanyName) &&
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
