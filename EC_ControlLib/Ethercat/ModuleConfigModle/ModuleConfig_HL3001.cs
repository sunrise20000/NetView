using ControllerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EC_ControlLib.Ethercat.ModuleConfigModle
{
    [Serializable()]
    public class ModuleConfig_HL3001 : ModuleConfigModleBase
    {
        private  Dictionary<byte, string> InputTypeDic = new Dictionary<byte, string>();
        private Dictionary<byte, string> AccuracyDic = new Dictionary<byte, string>();

        protected override int GuiStringListNumber { get; } = 11;
        public ModuleConfig_HL3001()
        {
            DeviceName = EnumDeviceName.HL3001;
            InputTypeDic.Add(0x00, "Normal");
            InputTypeDic.Add(0x01, "0-10V");
            InputTypeDic.Add(0x02, "0-5V");
            for (byte i=3;i< 11;i++)
                InputTypeDic.Add(i, $"Reserved{i}");

            AccuracyDic.Add(0x0A, "10bits sampling");
            AccuracyDic.Add(0x0C, "12bits sampling");
            AccuracyDic.Add(0x10, "16bits sampling");
        }

        public byte[] ChInputTypeArr { get; private set; } = new byte[4];
        public byte[] ChAccuracyArr { get; private set; } = new byte[4];


        public override void FromString(params string[] ParaList)
        {
            if (ParaList.Length != 11)
                throw new Exception($"Wrong para number when parse {DeviceName.ToString()} formstring");
            var L1 = GuiStringList[0].Split('_');
            //Name
            Enum.TryParse(L1[0], out EnumDeviceName Dn);
            DeviceName = Dn;

            //LocalIndex
            LocalIndex = int.Parse(L1[1]);

            Function = 0x31;

            //GlobalIndex
            GlobalIndex = int.Parse(GuiStringList[2]);

            for (int i = 0; i < 4; i++)
            {
                ChInputTypeArr[i] = InputTypeDic.Where(a => a.Value.Equals(GuiStringList[2*i + 3])).First().Key;
                ChAccuracyArr[i] = AccuracyDic.Where(a => a.Value.Equals(GuiStringList[2*i + 4])).First().Key;
            }
        }

        public override List<string> ToStringList()
        {
            GuiStringList.Clear();
            //Name_LocalIndex
            GuiStringList.Add($"{DeviceName.ToString()}_{LocalIndex}");
            //Function
            GuiStringList.Add("AIx4Ch. 0-10V");
            //GlobalIndex
            GuiStringList.Add($"{GlobalIndex}");

            for (int i = 0; i < 4; i++)
            {
                GuiStringList.Add($"{InputTypeDic[ChInputTypeArr[i]]}");
                GuiStringList.Add($"{AccuracyDic[ChAccuracyArr[i]]}");
            }

            return GuiStringList;
        }

        public override List<byte> ToByteArr()
        {
            base.ToByteArr();
            for (int i = 0; i < 4; i++)
            {
                BtArr.Add(ChInputTypeArr[i]);
                BtArr.Add(ChAccuracyArr[i]);
            }
            return BtArr;
        }
        protected ModuleConfig_HL3001(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
