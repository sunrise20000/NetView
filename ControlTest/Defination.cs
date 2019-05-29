using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlTest
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

    #region HL1001

   
    /// <summary>
    ///  string[] TypeStringList = new string[] { "Normal", "DI1 as Alarm", "DI1~2 as Alarm", "DI1~3 as Alarm", "DI1~4 as Alarm" ,
    ///                "DI1~5 as Alarm","DI1~6 as Alarm","DI1~7 as Alarm","DI1~8 as Alarm"};///
    /// </summary>
    public enum EnumHL1001Type
    {
        [Description("Normal")]
        E1,

        [Description("DI1 as Alarm")]
        E2,

        [Description("DI1~2 as Alarm")]
        E3,

        [Description("DI1~3 as Alarm")]
        E4,

        [Description("DI1~4 as Alarm")]
        E5,

        [Description("DI1~5 as Alarm")]
        E6,

        [Description("DI1~6 as Alarm")]
        E7,

        [Description("DI1~7 as Alarm")]
        E8,

        [Description("DI1~8 as Alarm")]
        E9,
    }
    #endregion

    #region HL3001
    public enum EnumHL3001Type
    {
        [Description("Normal")]
        E1,

        [Description("0-10V")]
        E2,

        [Description("0-5V")]
        E3,

        [Description("Reserved3")]
        E4,

        [Description("Reserved4")]
        E5,

        [Description("Reserved5")]
        E6,

        [Description("Reserved6")]
        E7,

        [Description("Reserved7")]
        E8,

        [Description("Reserved8")]
        E9,

        [Description("Reserved9")]
        E10,
        [Description("Reserved10")]
        E11,
    }
    public enum EnumHL3001Accuracy
    {
        [Description("10bits sampling")]
        E1,

        [Description("12bits sampling")]
        E2,

        [Description("16bits sampling")]
        E3,
    }
    #endregion

    #region HL3002
    public enum EnumHL3002Type
    {
        [Description("Normal")]
        E1,

        [Description("4-20mA")]
        E2,

        [Description("0-20mA")]
        E3,

        [Description("Reserved3")]
        E4,

        [Description("Reserved4")]
        E5,

        [Description("Reserved5")]
        E6,

        [Description("Reserved6")]
        E7,

        [Description("Reserved7")]
        E8,

        [Description("Reserved8")]
        E9,

        [Description("Reserved9")]
        E10,
        [Description("Reserved10")]
        E11,
    }
    public enum EnumHL3002Accuracy
    {
        [Description("10bits sampling")]
        E1,

        [Description("12bits sampling")]
        E2,

        [Description("16bits sampling")]
        E3,
    }
    #endregion

    #region HL4001
    public enum EnumHL4001Type
    {
        [Description("Normal")]
        E1,

        [Description("0-10V")]
        E2,

        [Description("0-5V")]
        E3,

        [Description("Reserved3")]
        E4,

        [Description("Reserved4")]
        E5,

        [Description("Reserved5")]
        E6,

        [Description("Reserved6")]
        E7,

        [Description("Reserved7")]
        E8,

        [Description("Reserved8")]
        E9,

        [Description("Reserved9")]
        E10,
        [Description("Reserved10")]
        E11,
    }
    public enum EnumHL4001Accuracy
    {
        [Description("10bits sampling")]
        E1,

        [Description("12bits sampling")]
        E2,

        [Description("16bits sampling")]
        E3,
    }
    #endregion

    #region HL4002
    public enum EnumHL4002Type
    {
        [Description("Normal")]
        E1,

        [Description("4-20mA")]
        E2,

        [Description("0-20mA")]
        E3,

        [Description("Reserved3")]
        E4,

        [Description("Reserved4")]
        E5,

        [Description("Reserved5")]
        E6,

        [Description("Reserved6")]
        E7,

        [Description("Reserved7")]
        E8,

        [Description("Reserved8")]
        E9,

        [Description("Reserved9")]
        E10,
        [Description("Reserved10")]
        E11,
    }
    public enum EnumHL4002Accuracy
    {
        [Description("10bits sampling")]
        E1,

        [Description("12bits sampling")]
        E2,

        [Description("16bits sampling")]
        E3,
    }
    #endregion

    #region HL5002
    public enum EnumHL5002Resolution
    {
        [Description("Normal")]
        E1,

        [Description("8Bits")]
        E2,

        [Description("9Bits")]
        E3,

        [Description("10Bits")]
        E4,

        [Description("11Bits")]
        E5,

        [Description("12Bits")]
        E6,

        [Description("13Bits")]
        E7,

        [Description("14Bits")]
        E8,

        [Description("15Bits")]
        E9,

        [Description("16Bits")]
        E10,
    }
    public enum EnumHL5002Revolution
    {
        [Description("Normal")]
        E1,

        [Description("8Bits")]
        E2,

        [Description("9Bits")]
        E3,

        [Description("10Bits")]
        E4,

        [Description("11Bits")]
        E5,

        [Description("12Bits")]
        E6,

        [Description("13Bits")]
        E7,

        [Description("14Bits")]
        E8,

        [Description("15Bits")]
        E9,

        [Description("16Bits")]
        E10,
    }
    #endregion
}
