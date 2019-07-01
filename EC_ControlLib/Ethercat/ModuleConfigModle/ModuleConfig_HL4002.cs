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
    public class ModuleConfig_HL4002 : ModuleConfigModleBase
    {
        private Dictionary<byte, string> OutputTypeDic = new Dictionary<byte, string>();
        private Dictionary<byte, string> AccuracyDic = new Dictionary<byte, string>();

        protected override int GuiStringListNumber { get; } = 11;
        public override int ByteArrayExpectLength { get; } = 11;
        public ModuleConfig_HL4002()
        {
            DeviceName = EnumDeviceName.HL4002;
            for (int i = 0; i < 4; i++)
                ModuleSubInfoList.Add(new ModuleConfig_16()
                {
                    IOType = EnumModuleIoType.OUT,
                });
        }

        public byte[] ChOutputTypeArr { get; private set; } = new byte[4];
        public byte[] ChAccuracyArr { get; private set; } = new byte[4];


        public override void FromString(params string[] ParaList)
        {
            OutputTypeDic.Clear();
            AccuracyDic.Clear();
            OutputTypeDic.Add(0x00, "Normal");
            OutputTypeDic.Add(0x01, "4-20mA");
            OutputTypeDic.Add(0x02, "0-20mA");
            for (byte i = 3; i < 11; i++)
                OutputTypeDic.Add(i, $"Reserved{i}");

            AccuracyDic.Add(0x0A, "10bits sampling");
            AccuracyDic.Add(0x0C, "12bits sampling");
            AccuracyDic.Add(0x10, "16bits sampling");
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

            Function = 0x42;

            //GlobalIndex
            GlobalIndex = int.Parse(GuiStringList[2]);

            for (int i = 0; i < 4; i++)
            {
                ChOutputTypeArr[i] = OutputTypeDic.Where(a => a.Value.Equals(GuiStringList[2 * i + 3])).First().Key;
                ChAccuracyArr[i] = AccuracyDic.Where(a => a.Value.Equals(GuiStringList[2 * i + 4])).First().Key;
            }
        }

        public override List<string> ToStringList()
        {
            GuiStringList.Clear();
            //Name_LocalIndex
            GuiStringList.Add($"{DeviceName.ToString()}_{LocalIndex}");
            //Function
            GuiStringList.Add("AOx4Ch. 0-10V");
            //GlobalIndex
            GuiStringList.Add($"{GlobalIndex}");

            for (int i = 0; i < 4; i++)
            {
                GuiStringList.Add($"{OutputTypeDic[ChOutputTypeArr[i]]}");
                GuiStringList.Add($"{AccuracyDic[ChAccuracyArr[i]]}");
            }

            return GuiStringList;
        }

        public override List<byte> ToByteArr()
        {
            base.ToByteArr();

            for (int i = 0; i < 4; i++)
            {
                BtArr.Add(ChOutputTypeArr[i]);
                BtArr.Add(ChAccuracyArr[i]);
            }
            return BtArr;
        }
        public override void FromByteArray(byte[] BtArr)
        {
            base.FromByteArray(BtArr);
            for (int i = 0; i < 4; i++)
            {
                ChOutputTypeArr[i] = BtArr[2 * i + 3];
                ChAccuracyArr[i] = BtArr[2 * i + 4];
            }
        }
        protected ModuleConfig_HL4002(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        public override void GetSubModuleListValueFromBtArr(byte[] BtArr, int StartPos, int Len)
        {
            if (Len != 8)
                throw new Exception("Wrong len to parse HL4002 SubModuleValue");
            for (int i = 0; i < 4; i++)
                ModuleSubInfoList[i].RawData = (UInt32)((BtArr[2 * i] << 8) + BtArr[2 * i + 1]);
        }
    }
}
