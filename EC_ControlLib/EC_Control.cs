using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC_ControlLib
{
    public class EC_Controler
    {
        #region Field
        SerialPort Comport = new SerialPort();
        object ComportLock = new object();
        #endregion

        #region UserAPI
        public bool Open(int Port)
        {
            Comport.PortName = $"COM{Port}";
            Comport.BaudRate = 9600;
            Comport.Parity = Parity.Even;
            Comport.DataBits = 7;
            Comport.StopBits = StopBits.One;
            Comport.ReadTimeout = 1000;
            Comport.WriteTimeout = 1000;
            Comport.ReadBufferSize = 1024;
            Comport.WriteBufferSize = 1024;
            if (Comport.IsOpen)
                Comport.Close();
            Comport.Open();
            return Comport.IsOpen;
        }
        public void Connect()
        {
            byte[] Cmd = new byte[] { 68, 01, 02 ,68 ,05 };
            var Crc = CRC16(Cmd,0,Cmd.Length);
            List<byte> FinalCmd = new List<byte>(Cmd);
            FinalCmd.Add(Crc[1]);
            FinalCmd.Add(Crc[0]);
            lock (ComportLock)
            {
                Comport.Write(FinalCmd.ToArray(), 0, FinalCmd.Count);
            }
   
        }
        public void CLose()
        {
            Comport.Close();
        }

        /// <summary>
        /// 读取D寄存器的值
        /// </summary>
        /// <param name="strRegisterName"></param>
        /// <returns></returns>
        public Int16 ReadInt(string strRegisterName)
        {
            if (Comport == null || !Comport.IsOpen)
                throw new Exception("请检查串口状态!");
            Int32 dwAddress = -1;
            int nLen = strRegisterName.Length;
            if (!strRegisterName.Substring(0, 1).ToUpper().Equals("D") || nLen < 2)
            {
                throw new Exception("寄存器地址输入错误");
            }
            dwAddress = Int32.Parse(strRegisterName.Substring(1, nLen - 1)); //D寄存器地址

            //地址计算
            AddressToAscii(REGISTER_TYPE.D, dwAddress, out byte[] addressArray);
            byte[] dataSend = new byte[] { (byte)CMD.STX, (byte)CMD.R, addressArray[3], addressArray[2], addressArray[1], addressArray[0], 0x30, 0x32, (byte)CMD.ETX };
            CheckSum(dataSend, 1, dataSend.Length - 1, out byte sum0, out byte sum1);
            var dataSendFinall = new List<byte>(dataSend);
            dataSendFinall.Add(sum1);
            dataSendFinall.Add(sum0);

            //byte[] dataSendFinall =new byte[] { (byte)CMD.STX, (byte)CMD.R, addressArray[3], addressArray[2], addressArray[1], addressArray[0], 0x30, 0x32, (byte)CMD.ETX, sum1, sum0 };
            //发送数据
            lock (ComportLock)
            {
                Comport.Write(dataSendFinall.ToArray(), 0, dataSendFinall.Count);
                return ReadIntAck();
            }

        }
        public Int32 ReadDint(string strRegisterStartName)
        {
            if (Comport == null || !Comport.IsOpen)
                throw new Exception("请检查串口状态!");
            Int32 dwAddress = -1;
            int nLen = strRegisterStartName.Length;
            if (!strRegisterStartName.Substring(0, 1).ToUpper().Equals("D") || nLen < 2)
            {
                throw new Exception("寄存器地址输入错误");
            }
            dwAddress = Int32.Parse(strRegisterStartName.Substring(1, nLen - 1)); //D寄存器地址

            //地址计算
            AddressToAscii(REGISTER_TYPE.D, dwAddress, out byte[] addressArray);

            byte[] dataSend = new byte[] { (byte)CMD.STX, (byte)CMD.R, addressArray[3], addressArray[2], addressArray[1], addressArray[0], 0x30, 0x34, (byte)CMD.ETX };
            CheckSum(dataSend, 1, dataSend.Length - 1, out byte sum0, out byte sum1);
            var dataSendFinall = new List<byte>(dataSend);
            dataSendFinall.Add(sum1);
            dataSendFinall.Add(sum0);

            //byte[] dataSendFinall =new byte[] { (byte)CMD.STX, (byte)CMD.R, addressArray[3], addressArray[2], addressArray[1], addressArray[0], 0x30, 0x32, (byte)CMD.ETX, sum1, sum0 };
            //发送数据
            lock (ComportLock)
            {
                Comport.Write(dataSendFinall.ToArray(), 0, dataSendFinall.Count);
                return ReadDintAck();
            }

        }
        public bool WriteInt(string strRegisterName, Int16 value)
        {
            if (Comport == null || !Comport.IsOpen)
                throw new Exception("请检查串口状态!");
            Int32 dwAddress = -1;
            int nLen = strRegisterName.Length;
            if (!strRegisterName.Substring(0, 1).ToUpper().Equals("D") || nLen < 2)
            {
                throw new Exception("寄存器地址输入错误");
            }
            dwAddress = Int32.Parse(strRegisterName.Substring(1, nLen - 1)); //D寄存器地址
            //地址计算
            AddressToAscii(REGISTER_TYPE.D, dwAddress, out byte[] addressArray);
            Dec2Ascii((byte)(value & 0xFF), out byte ascii0, out byte ascii1);
            Dec2Ascii((byte)((value >> 8) & 0xFF), out byte ascii2, out byte ascii3);
            byte[] dataSend = new byte[] { (byte)CMD.STX, (byte)CMD.W, addressArray[3], addressArray[2], addressArray[1], addressArray[0], 0x30, 0x32, ascii1, ascii0, ascii3, ascii2, (byte)CMD.ETX };
            CheckSum(dataSend, 1, dataSend.Length - 1, out byte sum0, out byte sum1);
            List<byte> dataSendFinall = new List<byte>(dataSend);
            dataSendFinall.Add(sum1);
            dataSendFinall.Add(sum0);

            //发送数据
            lock (ComportLock)
            {
                Comport.Write(dataSendFinall.ToArray(), 0, dataSendFinall.Count);
                return ReadVoidAck();
            }
        }
        public bool WriteDint(string strRegisterName, Int32 value)
        {
            if (Comport == null || !Comport.IsOpen)
                throw new Exception("请检查串口状态!");
            Int32 dwAddress = -1;
            int nLen = strRegisterName.Length;
            if (!strRegisterName.Substring(0, 1).ToUpper().Equals("D") || nLen < 2)
            {
                throw new Exception("寄存器地址输入错误");
            }
            dwAddress = Int32.Parse(strRegisterName.Substring(1, nLen - 1)); //D寄存器地址
            //地址计算
            AddressToAscii(REGISTER_TYPE.D, dwAddress, out byte[] addressArray);
            Dec2Ascii((byte)(value & 0xFF), out byte ascii0, out byte ascii1);
            Dec2Ascii((byte)((value >> 8) & 0xFF), out byte ascii2, out byte ascii3);
            Dec2Ascii((byte)((value >> 16) & 0xFF), out byte ascii4, out byte ascii5);
            Dec2Ascii((byte)((value >> 24) & 0xFF), out byte ascii6, out byte ascii7);

            byte[] dataSend = new byte[] { (byte)CMD.STX, (byte)CMD.W, addressArray[3], addressArray[2], addressArray[1], addressArray[0], 0x30, 0x34, ascii1, ascii0, ascii3, ascii2, ascii5, ascii4, ascii7, ascii6, (byte)CMD.ETX };
            CheckSum(dataSend, 1, dataSend.Length - 1, out byte sum0, out byte sum1);
            List<byte> dataSendFinall = new List<byte>(dataSend);
            dataSendFinall.Add(sum1);
            dataSendFinall.Add(sum0);

            //发送数据
            lock (ComportLock)
            {
                Comport.Write(dataSendFinall.ToArray(), 0, dataSendFinall.Count);
                return ReadVoidAck();
            }
        }

        public bool IsOpen()
        {
            return Comport.IsOpen;
        }

        #endregion

        #region Private Method

        public byte[] CRC16(byte[] dataSrc, int offset, int datalength)
        {
            if (offset + datalength > dataSrc.Length)
                throw new Exception("data out of range");
            List<byte> list = new List<byte>();
            for (int i = offset; i < offset + datalength; i++)
            {
                list.Add(dataSrc[i]);
            }
            var data = list.ToArray();
            int len = data.Length;
            if (len > 0)
            {
                ushort crc = 0xFFFF;

                for (int i = 0; i < len; i++)
                {
                    crc = (ushort)(crc ^ (data[i]));
                    for (int j = 0; j < 8; j++)
                    {
                        crc = (crc & 1) != 0 ? (ushort)((crc >> 1) ^ 0xA001) : (ushort)(crc >> 1);
                    }
                }
                byte hi = (byte)((crc & 0xFF00) >> 8);  //高位置
                byte lo = (byte)(crc & 0x00FF);         //低位置

                return new byte[] { hi, lo };
            }
            return new byte[] { 0, 0 };
        }

        /// <summary>
        /// 将三菱PLC的地址映射转为字节数组
        /// </summary>
        /// <param name="nType"></param>
        /// <param name="dwAddress"></param>
        /// <param name="outAddress"></param>
        void AddressToAscii(REGISTER_TYPE nType, Int32 dwAddress, out byte[] outAddress)
        {
            outAddress = new byte[4];
            switch (nType)
            {
                case REGISTER_TYPE.D:
                    {
                        dwAddress = dwAddress * 2 + 0x1000;
                        var strAddress = string.Format("{0:X4}", dwAddress).ToUpper();
                        for (int i = 0; i < 4; i++)
                            outAddress[3 - i] = (byte)strAddress[i];
                    }
                    break;
                default:
                    break;

            }
        }

        /// <summary>
        /// 将16进制字符串转为10进制数
        /// </summary>
        /// <param name="szHexData"></param>
        /// <param name="nStartPos"></param>
        /// <param name="nEndPos"></param>
        /// <returns></returns>
        Int32 HexStr2Dec(string szHexData, int nStartPos, int nEndPos)   //仅支持大写字母
        {
            Int32 dwRet = 0;
            for (int i = nStartPos; i < nEndPos; i++)
            {
                int x = HexCh2Dec(szHexData[i]);
                var y = Math.Pow(16.0f, nEndPos - nStartPos - i - 1);
                dwRet += (Int32)((double)x * y);
            }
            return dwRet;
        }

        /// <summary>
        /// 将Hex字符转为10进制形式
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        byte HexCh2Dec(char ch)
        {
            switch (ch)
            {
                case 'A':
                case 'a':
                    return 10;
                case 'B':
                case 'b':
                    return 11;
                case 'C':
                case 'c':
                    return 12;
                case 'D':
                case 'd':
                    return 13;
                case 'E':
                case 'e':
                    return 14;
                case 'F':
                case 'f':
                    return 15;
                default:
                    return byte.Parse(ch.ToString());
            }
        }

        /// <summary>
        /// 将10进制的数字，表示为16进制的字符串标示
        /// 比如10=“0A”
        /// </summary>
        /// <param name="nData"></param>
        /// <param name="ascii0"></param>
        /// <param name="ascii1"></param>
        void Dec2Ascii(byte nData, out byte ascii0, out byte ascii1)
        {
            var strData = string.Format("{0:X2}", nData);
            ascii0 = (byte)strData[1];
            ascii1 = (byte)strData[0];
        }

        /// <summary>
        /// 不需要返回值的时候的读取
        /// </summary>
        /// <returns></returns>
        bool ReadVoidAck()
        {
            var StartTime = DateTime.Now.Ticks;
            int i = 0;
            List<byte> Recv = new List<byte>();
            while (true)
            {
                if (Comport.BytesToRead > 0)
                {
                    var ch =(byte)Comport.ReadByte();
                    Recv.Add(ch);
                    i++;
                    if (i == 7)
                    {
                        var CrcCal=CRC16()
                    }
                }
                if (TimeSpan.FromTicks(DateTime.Now.Ticks - StartTime).TotalMilliseconds > 1000)
                    throw new Exception("通信超时");
            }
        }

        /// <summary>
        /// 读取int的返回值
        /// </summary>
        /// <returns></returns>
        Int16 ReadIntAck()
        {
            var StartTime = DateTime.Now.Ticks;
            List<byte> listRead = new List<byte>();
            bool IsHeaderFound = false;
            while (true)
            {
                if (Comport.BytesToRead > 0)
                {
                    var ch = Comport.ReadByte();
                    if (ch == (byte)CMD.STX)
                    {
                        IsHeaderFound = true;
                    }
                    if (IsHeaderFound)
                    {
                        listRead.Add((byte)ch);

                        if (listRead.Count == 6 && ch == (byte)CMD.ETX)
                        {
                            StringBuilder sb = new StringBuilder();

                            sb.Append((char)listRead[3]);
                            sb.Append((char)listRead[4]);
                            sb.Append((char)listRead[1]);
                            sb.Append((char)listRead[2]);
                            return Convert.ToInt16(sb.ToString(), 16);
                        }
                    }
                }
                if (TimeSpan.FromTicks(DateTime.Now.Ticks - StartTime).TotalMilliseconds > 1000)
                    throw new Exception("通信超时");
            }
        }

        /// <summary>
        /// 读取Dint的返回值
        /// </summary>
        /// <returns></returns>
        Int32 ReadDintAck()
        {
            var StartTime = DateTime.Now.Ticks;
            List<byte> listRead = new List<byte>();
            bool IsHeaderFound = false;
            while (true)
            {
                if (Comport.BytesToRead > 0)
                {
                    var ch = Comport.ReadByte();
                    if (ch == (byte)CMD.STX)
                    {
                        IsHeaderFound = true;
                    }
                    if (IsHeaderFound)
                    {
                        listRead.Add((byte)ch);

                        if (listRead.Count == 10 && ch == (byte)CMD.ETX)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append((char)listRead[7]);
                            sb.Append((char)listRead[8]);
                            sb.Append((char)listRead[5]);
                            sb.Append((char)listRead[6]);

                            sb.Append((char)listRead[3]);
                            sb.Append((char)listRead[4]);
                            sb.Append((char)listRead[1]);
                            sb.Append((char)listRead[2]);

                            return Convert.ToInt32(sb.ToString(), 16);
                        }
                    }
                }

                if (TimeSpan.FromTicks(DateTime.Now.Ticks - StartTime).TotalMilliseconds > 1000)
                    throw new Exception("通信超时");
            }
        }
        #endregion
    }
}
