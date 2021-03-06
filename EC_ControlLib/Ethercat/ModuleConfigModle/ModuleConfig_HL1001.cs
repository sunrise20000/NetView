﻿using ControllerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ControllerLib.Ethercat.ModuleConfigModle
{
    [Serializable()]
    public class ModuleConfig_HL1001 : ModuleConfigModleBase
    {
        byte[] TypeList = new byte[] {0x00, 0x01, 0x03,0x07, 0x0F,0x1F,0x3F,0x7F,0xFF};
        string[] TypeStringList = new string[] { "Normal", "DI1 as Alarm", "DI1~2 as Alarm", "DI1~3 as Alarm", "DI1~4 as Alarm" ,
                        "DI1~5 as Alarm","DI1~6 as Alarm","DI1~7 as Alarm","DI1~8 as Alarm"};
        Dictionary<byte, string> TypeDic = new Dictionary<byte, string>();

        protected override int GuiStringListNumber { get; } = 4;
        public override int ByteArrayExpectLength { get; } = 4;

        public ModuleConfig_HL1001()
        {
            DeviceName = EnumDeviceName.HL1001;
            ModuleSubInfoList.Add(new ConfigSubInfo.ModuleConfig_8()
            {
                IOType = EnumModuleIoType.IN,
            });
            TypeDic.Clear();
            for (int i = 0; i < TypeList.Count(); i++)
                TypeDic.Add(TypeList[i], TypeStringList[i]);
        }

        public byte  Type{
            get;
            set;
        }


        public override void FromString(params string[] ParaList)
        {
            TypeDic.Clear();
            for (int i = 0; i < TypeList.Count(); i++)
                TypeDic.Add(TypeList[i], TypeStringList[i]);

            if (ParaList.Length != GuiStringListNumber)
                throw new Exception($"Wrong para number when parse {DeviceName.ToString()} formstring");

            GuiStringList.Clear();
            foreach (var it in ParaList)
                GuiStringList.Add(it);

            var L1 = GuiStringList[0].Split('_');
            //Name
            Enum.TryParse(L1[0], out EnumDeviceName Dn);
            DeviceName = Dn;

            //LocalIndex
            LocalIndex = int.Parse(L1[1]);

            Function = 0x11;

            //GlobalIndex
            GlobalIndex = int.Parse(GuiStringList[2]);

            //Type
            Type = TypeDic.Where(a=>a.Value.Equals(GuiStringList[3])).First().Key;

        }

        /// <summary>
        /// 根据Byte生成StringList
        /// </summary>
        /// <returns></returns>
        public override List<string> ToStringList()
        {
            GuiStringList.Clear();
            //Name_LocalIndex
            GuiStringList.Add($"{DeviceName.ToString()}_{LocalIndex}");
            //Function
            GuiStringList.Add("DI8xDC24V");
            //GlobalIndex
            GuiStringList.Add($"{GlobalIndex}");
            //TypeString
            GuiStringList.Add($"{TypeDic[Type]}");
            
            return GuiStringList;
        }

        public override List<byte> ToByteArr()
        {
            base.ToByteArr();
            BtArr.Add(Type);
            return BtArr;
        }

        public override void FromByteArray(byte[] BtArr)
        {
            base.FromByteArray(BtArr);
            Type = BtArr[3];
        }
        protected ModuleConfig_HL1001(SerializationInfo info, StreamingContext context) : base(info,context)
        {
          
        }

        public override void GetSubModuleListValueFromBtArr(byte[] BtArr, int StartPos, int Len)
        {
            if (Len != 1)
                throw new Exception("Wrong len to parse HL1001 SubModuleValue");
            ModuleSubInfoList[0].RawData = (UInt32)BtArr[0];
        }
    }
}
