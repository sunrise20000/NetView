using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerLib.Ethercat
{
    public class EC_Controller : ControllerBase
    {
        #region Field
        SerialPort Comport = new SerialPort();
        object ComportLock = new object();
        #endregion

        #region UserAPI
        public EC_Controller() {
            IsConnected = false;       
        }
        public override bool IsConnected { get; protected set; }
        public override bool Open(string Port)
        {
            Comport.PortName = Port.ToUpper();
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

        public override bool Connect()
        {
            byte[] Cmd = new byte[] { 0x68, 0x01, 0x02, 0x68, 0x05 };
            var Crc = CRC16(Cmd, 0, Cmd.Length);
            List<byte> FinalCmd = new List<byte>(Cmd);
            FinalCmd.Add(Crc[1]);
            FinalCmd.Add(Crc[0]);
            lock (ComportLock)
            {
                Comport.Write(FinalCmd.ToArray(), 0, FinalCmd.Count);
                return IsConnected=ReadVoidAck(0x68, 0x06, 0x01 ,0x02 ,0x68 ,0x05);
            }
        }

        public override bool DisConnect()
        {
            //68 06 01 FF 68 05 CRC
            byte[] Cmd = new byte[] { 0x68, 0x06, 0x01, 0xFF, 0x68, 0x05, };
            var Crc = CRC16(Cmd, 0, Cmd.Length);
            List<byte> FinalCmd = new List<byte>(Cmd);
            FinalCmd.Add(Crc[1]);
            FinalCmd.Add(Crc[0]);
            lock (ComportLock)
            {
                Comport.Write(FinalCmd.ToArray(), 0, FinalCmd.Count);
                IsConnected = false;
                if (ReadVoidAck(0x68, 0x06, 0x01, 0x02, 0x68, 0x05))
                {
                    IsConnected = false;
                    return true;
                }
                else
                    return false;
            }
        }


        /// <summary>
        /// 获取模块信息列表，从控制器读取
        /// </summary>
        /// <returns></returns>
        public override List<string> GetModuleList()
        {
            //68 08 01 02 68 05 69 96 CRC
            byte[] Cmd = new byte[] { 0x68, 0x08, 0x01,0x02, 0x68, 0x05, 0x69, 0x96 };
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
        /// PureNameList， 将配置信息发送给控制器
        /// </summary>
        /// <param name="ModuleNameList"></param>
        /// <returns></returns>
        public override bool SendModuleList(List<string> ModuleNameList)
        {
            //68 N 01 02 68 05 96 69
            byte[] Cmd = new byte[] { 0x68, 0x08, 0x01, 0x02, 0x68, 0x05, 0x96, 0x69 };
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
                IsConnected = Comport.IsOpen;
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

     

        bool ReadVoidAck(params byte[] ExpectAckByteList)
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
                        if (Length == ExpectAckByteList.Length)
                        {
                            if (CompareList(Recv.ToArray(), 0, ExpectAckByteList, 0, Length))
                            {
                                var CrcCal = CRC16(Recv.ToArray(), 0, Length);
                                if (CrcCal[0] == Recv[Length + 1] && CrcCal[1] == Recv[Length])
                                    return true;
                                else
                                    throw new Exception("CRC check error");
                            }
                        }
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



        bool CompareList(byte[] BtList1, int offset1, byte[] BtList2, int offset2, int length)
        {
            if (BtList1.Count() < offset1 + length || BtList2.Count() < offset2 + length)
                return false;
            bool bRet = true;
            for (int i = 0; i < length; i++)
            {
                bRet &= (BtList1[offset1 + i] == BtList2[offset2 + i]);
            }
            return bRet;
        }
        #endregion
    }
}
