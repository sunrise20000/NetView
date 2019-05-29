
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ControlTest.ModuleConfigModle
{
    [Serializable()]
    public class ModuleCfg_HL5002 : ModuleCfgModleBase
    {
        
        Dictionary<byte, string> ResolutionDic = new Dictionary<byte, string>();
        Dictionary<byte, string> RevolutionDic = new Dictionary<byte, string>();

        protected override int GuiStringListNumber { get; } = 10;
        public ModuleCfg_HL5002()
        {
            DeviceName = EnumDeviceName.HL5002;
            ResolutionDic.Add(0, "Normal");
            RevolutionDic.Add(0, "Normal");
            for (byte i = 1; i < 17; i++)
            {
                ResolutionDic.Add(i, $"{i}bits");
                RevolutionDic.Add(i, $"{i}bits");
            }


        }

   
        public byte Resolution { get; set; }

        public byte Revolution { get; set; }

        public UInt32 PresetValue { get; set;}

        public byte[] ResParaArr { get; } = new byte[5];


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

            Function = 0x52;

            //GlobalIndex
            GlobalIndex = int.Parse(GuiStringList[2]);

            //Resolution
            Resolution = byte.Parse(GuiStringList[3]);

            //Revolution
            Revolution = byte.Parse(GuiStringList[4]);

            //PresetValue
            PresetValue = UInt32.Parse(GuiStringList[5]);

            //ResPara
            for (int i = 0; i < 5; i++)
                ResParaArr[i] = byte.Parse(GuiStringList[i + 6]);


        }

        public override List<string> ToStringList()
        {
            GuiStringList.Clear();

            //Name_LocalIndex
            GuiStringList.Add($"{DeviceName.ToString()}_{LocalIndex}");
            //Function
            GuiStringList.Add("AbsEncoder SSI");
            //GlobalIndex
            GuiStringList.Add($"{GlobalIndex}");
            //Resolution
            GuiStringList.Add($"{Resolution}");
            //Revolution
            GuiStringList.Add($"{Revolution}");

            for (int i = 0; i < 5; i++)
                GuiStringList.Add($"{ResParaArr[i]}");

            return GuiStringList;
        }
        public override List<byte> ToByteArr()
        {
            base.ToByteArr();
            BtArr.Add(Resolution);
            BtArr.Add(Revolution);
            BtArr.Add((byte)((PresetValue>>24)& 0xFF));
            BtArr.Add((byte)((PresetValue >> 16) & 0xFF));
            BtArr.Add((byte)((PresetValue >> 8) & 0xFF));
            BtArr.Add((byte)((PresetValue >> 0) & 0xFF));
            for (int i = 0; i < 5; i++)
                BtArr.Add(ResParaArr[i]);


            return BtArr;
        }
        protected ModuleCfg_HL5002(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
