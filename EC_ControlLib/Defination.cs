using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerLib
{
    enum EnumControllerType
    {
        EtherCatController=0x05,
    }

    public enum EnumDeviceName
    {
        HL1001,
        HL2001,
        HL2002,
        HL2003,
        HL3001,
        HL3002,
        HL4001,
        HL4002,
        HL5001,
        HL5002,
    }
    public enum EnumBusType
    {
        CANopen,
        DeviceNet,
        EtherCAT,
        ModbusRTU,
        ModbusTCP,
        PROFIBUS_DP,
        PROFINET_IO,
    }

    public enum EnumModuleIoType
    {
        IN,
        OUT,
    }
    
}
