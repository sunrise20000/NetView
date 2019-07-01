using ControllerLib;
using ControllerLib.Ethercat.ModuleConfigModle.ConfigSubInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ControllerLib.Ethercat.ModuleConfigModle
{
    [Serializable()]
    public class ModuleConfig_HL5001 : ModuleConfigModleBase
    {

        protected override int GuiStringListNumber { get; } = 11;
        public override int ByteArrayExpectLength { get; } = 11;
        public ModuleConfig_HL5001()
        {
            DeviceName = EnumDeviceName.HL5001;
            ModuleSubInfoList.Add(new ModuleConfig_32()
            {
                IOType = EnumModuleIoType.IN,

            });
            ModuleSubInfoList.Add(new ModuleConfig_32()
            {
                IOType = EnumModuleIoType.IN,
            });
        }

        public byte CounterLimitH { get; set; }

        public byte CounterLimitL { get; set; }

        public byte[] ResParaArr { get; } = new byte[6];

        public override void FromString(params string[] ParaList)
        {
            if (ParaList.Length != 11)
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

            Function = 0x51;

            //GlobalIndex
            GlobalIndex = int.TryParse(GuiStringList[2], out int globalIndex) ? globalIndex : 0;

            //CounterLimitH
            CounterLimitH = byte.TryParse(GuiStringList[3], out byte counterLimitH)? counterLimitH : (byte)0;

            //CounterLimitL
            CounterLimitL= byte.TryParse(GuiStringList[4], out byte counterLimitL) ? counterLimitL : (byte)0;

            //ResPara
            for (int i = 0; i < 6; i++)
                ResParaArr[i] = byte.TryParse(GuiStringList[i+5],out byte resPara)? resPara:(byte)0;
        }

        public override List<string> ToStringList()
        {
            GuiStringList.Clear();
            //Name_LocalIndex
            GuiStringList.Add($"{DeviceName.ToString()}_{LocalIndex}");
            //Function
            GuiStringList.Add("DI8xDC24V");
            //GlobalIndex
            GuiStringList.Add($"{GlobalIndex}");
            //LimitH
            GuiStringList.Add($"{CounterLimitH}");
            //LimitL
            GuiStringList.Add($"{CounterLimitL}");

            for (int i = 0; i < 6; i++)
                GuiStringList.Add($"{ResParaArr[i]}");

            return GuiStringList;
        }

        public override List<byte> ToByteArr()
        {
            base.ToByteArr();
            BtArr.Add(CounterLimitH);
            BtArr.Add(CounterLimitL);
            for (int i = 0; i < 6; i++)
                BtArr.Add(ResParaArr[i]);
            return BtArr;
        }
        public override void FromByteArray(byte[] BtArr)
        {
            base.FromByteArray(BtArr);
            CounterLimitH = BtArr[3];
            CounterLimitL = BtArr[4];
            for (int i = 0; i < 6; i++)
                ResParaArr[i]= BtArr[i+5];
        }
        protected ModuleConfig_HL5001(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        public override void GetSubModuleListValueFromBtArr(byte[] BtArr, int StartPos, int Len)
        {
            if (Len != 8)
                throw new Exception("Wrong len to parse HL4002 SubModuleValue");
            for (int i = 0; i < 2; i++)
                ModuleSubInfoList[i].RawData = (UInt32)((BtArr[4 * i] << 24) + (BtArr[4 * i + 1]<<16) + (BtArr[4 * i + 2] << 8) + (BtArr[4 * i + 3]));
        }
    }
}
