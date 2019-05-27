using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlTest
{
    public  enum  enumSubBusModelType
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
    public class CommonFunc
    {
        public static int ConvertToType(int typeindex)
        {
            return (0xff >> (8 - typeindex));
        }
        public static int ConvertToAccurary(int accuraryindex)
        {
            int rtn=0x0a;
            switch (accuraryindex)
            {
                case 0:
                    rtn = 0x0a;
                    break;
                case 1:
                    rtn = 0x0c;
                    break;
                case 2:
                    rtn = 0x10;
                    break;
                default:
                    break;
            }
            return rtn;
        }
        public static int AccuraryToInt(string hexdata)
        {
            int rtn = 0;
            if (hexdata == "0x0c")
                rtn = 1;
            else if (hexdata == "0x10")
                rtn = 2;
            return rtn;
        }
        public static int ConvertToResolution(int resolutionindex)
        {
            if (resolutionindex == 0)
                return 0x00;
            else
                return 0x08 + resolutionindex - 1;
        }
        public static int ResolutionToInt(string hexdata)
        {
            int rtn = 0;
            if (hexdata != "0x00")
            {
                int k = HexStringToInt(hexdata);
                rtn = k + 1 - 8;
            }
               
            return rtn;

        }
        private static int HexStringToInt(string data)
        {
            int rtn = 0;
            if (data.StartsWith("0x"))
            {
                data.Trim();
                string s = data.Substring(2, data.Length - 2);
                rtn = Convert.ToInt32(s, 16);
            }
            else
                throw new Exception("数据格式异常");
            return rtn;
        }
        public static int ConvertToRevolution(int revolutionindex)
        {
            if (revolutionindex == 0)
                return 0x00;
            else
                return 0x08 + revolutionindex - 1;
        }
        
    }
   
}
