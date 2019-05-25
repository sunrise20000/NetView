using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerLib
{
    public class EC_Controller : ControllerBase
    {
        #region Field
        SerialPort Comport = new SerialPort();
        object ComportLock = new object();
        #endregion

        #region UserAPI
        public override bool Open(string Port)
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
            Comport.DiscardInBuffer();
            Comport.DiscardOutBuffer();
            return Comport.IsOpen;
        }
        public override void Connect()
        {
            byte[] Cmd = new byte[] { 68, 01, 02, 68, 05 };
            var Crc = CRC16(Cmd, 0, Cmd.Length);
            List<byte> FinalCmd = new List<byte>(Cmd);
            FinalCmd.Add(Crc[1]);
            FinalCmd.Add(Crc[0]);
            lock (ComportLock)
            {
                Comport.Write(FinalCmd.ToArray(), 0, FinalCmd.Count);
                ReadVoidAck();
            }
        }

        public override List<string> GetModuleList()
        {
            //68 08 01 02 68 05 69 96 CRC
            byte[] Cmd = new byte[] { 68, 08, 01, 02, 68, 05, 69, 96 };
            var Crc = CRC16(Cmd, 0, Cmd.Length);
            List<byte> FinalCmd = new List<byte>(Cmd);
            FinalCmd.Add(Crc[1]);
            FinalCmd.Add(Crc[0]);
            lock (ComportLock)
            {
                Comport.Write(FinalCmd.ToArray(), 0, FinalCmd.Count);
                return ReadModuleListAck();
            }

        }

        /// <summary>
        /// PureNameList
        /// </summary>
        /// <param name="ModuleNameList"></param>
        /// <returns></returns>
        public override bool SendModuleList(List<string> ModuleNameList)
        {
            //68 N 01 02 68 05 96 69
            byte[] Cmd = new byte[] { 68, 08, 01, 02, 68, 05, 96, 69 };
            var Crc = CRC16(Cmd, 0, Cmd.Length);
            List<byte> FinalCmd = new List<byte>(Cmd);
            FinalCmd.Add(Crc[1]);
            FinalCmd.Add(Crc[0]);
            lock (ComportLock)
            {
                Comport.Write(FinalCmd.ToArray(), 0, FinalCmd.Count);
                return ReadVoidAck();
            }
        }

        public override void GetModuleValue(out List<int> InputValueList, out List<int> OutputValueList)
        {
            InputValueList = new List<int>();
            OutputValueList = new List<int>();
        }

        public override void SetModuleValue(List<int> OutputValueList)
        {

        }
        public override void CLose()
        {
            lock (ComportLock)
            {
                Comport.Close();
            }
        }

        public override bool IsOpen()
        {
            lock (ComportLock)
            {
                return Comport.IsOpen;
            }
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

        bool ReadVoidAck()
        {
            var StartTime = DateTime.Now.Ticks;
            List<byte> Recv = new List<byte>();
            bool IsHeaderFound = false;
            int Length = 0;
            while (true)
            {
                byte bt = (byte)Comport.ReadByte();
                if (bt == 0x68)
                {
                    IsHeaderFound = true;
                    Recv.Add(bt);
                }
                if (IsHeaderFound)
                {
                    Recv.Add(bt);
                    if (Recv.Count == 2)                   
                        Length = Recv[1];
                    if (Length == Recv.Count - 2)
                    {
                        var CrcCal = CRC16(Recv.ToArray(), 0, Length);
                        if (CrcCal[0] == Recv[Length+1] && CrcCal[1] == Recv[Length])
                            return true;
                        else
                            throw new Exception("CRC check error");
                    }                  
                }
                if (TimeSpan.FromTicks(DateTime.Now.Ticks - StartTime).TotalMilliseconds > 1000)
                    throw new Exception("Timeout to connect controller");
            }
        }

        /// <summary>
        /// PureName
        /// </summary>
        /// <returns></returns>
        List<string> ReadModuleListAck()
        {
            List<string> ModuleList = new List<string>();
            var StartTime = DateTime.Now.Ticks;
            List<byte> Recv = new List<byte>();
            bool IsHeaderFound = false;
            int Length = 0;
            while (true)
            {
                byte bt = (byte)Comport.ReadByte();
                if (bt == 0x68)
                {
                    IsHeaderFound = true;
                    Recv.Add(bt);
                }
                if (IsHeaderFound)
                {
                    Recv.Add(bt);
                    if (Recv.Count == 2)
                        Length = Recv[1];
                    if (Length == Recv.Count - 2)   //接受完毕
                    {
                        var CrcCal = CRC16(Recv.ToArray(), 0, Length);
                        if (CrcCal[0] == Recv[Length + 1] && CrcCal[1] == Recv[Length])
                        {
                            for (int i = 2; i < Recv.Count-2; i++)
                            {
                                ModuleList.Add(GetModuleFromByte(Recv[i]));
                            }
                            return ModuleList;
                        }
                        else
                            throw new Exception("CRC check error");
                    }
                }
                if (TimeSpan.FromTicks(DateTime.Now.Ticks - StartTime).TotalMilliseconds > 1000)
                    throw new Exception("Timeout to GetModuleList");
            }
        }




        string GetModuleFromByte (byte Bt)
        {
            var strByte = string.Format("{0:X2}",Bt);
            return $"{strByte[0]}00{strByte[1]}";
        }
        #endregion
    }
}
