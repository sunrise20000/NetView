using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Definations
{
    public enum EnumMsgType
    {
        Info,
        Warning,
        Error,
    }

    public enum EnumDisplayFormat
    {
        Hex,        //16
        Dec,        //10
        Float,      //float
    }
    public enum EnumModuleIOType
    {
        IN = 1,
        OUT,
    }
    public enum EnumType
    {
        USINT = 8,
        UINT = 16,
        UDINT = 32,
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
}
