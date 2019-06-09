using ControllerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ControllerLib.Ethercat.ModuleConfigModle
{
    [Serializable()]
    public class ModuleConfig_HL5002 : ModuleConfigModleBase
    {
        
        Dictionary<byte, string> ResolutionDic = new Dictionary<byte, string>();
        Dictionary<byte, string> RevolutionDic = new Dictionary<byte, string>();

        protected override int GuiStringListNumber { get; } = 11;
        public override int ByteArrayExpectLength { get; } = 14;
        public ModuleConfig_HL5002()
        {
            DeviceName = EnumDeviceName.HL5002;
        }

   
        public byte Resolution { get; set; }

        public byte Revolution { get; set; }

        public UInt32 PresetValue { get; set;}

        public byte[] ResParaArr { get; } = new byte[5];


        public override void FromString(params string[] ParaList)
        {
            ResolutionDic.Clear();
            RevolutionDic.Clear();
            ResolutionDic.Add(0, "Normal");
            RevolutionDic.Add(0, "Normal");
            for (byte i = 1; i < 17; i++)
            {
                ResolutionDic.Add(i, $"{i}bits");
                RevolutionDic.Add(i, $"{i}bits");
            }

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

            Function = 0x52;

            //GlobalIndex
            GlobalIndex = int.Parse(GuiStringList[2]);

            //Resolution
            Resolution = ResolutionDic.Where(a=>a.Value.Equals(GuiStringList[3])).First().Key;

            //Revolution
            Revolution = RevolutionDic.Where(a => a.Value.Equals(GuiStringList[4])).First().Key;

            //PresetValue
            if (UInt32.TryParse(GuiStringList[5], out UInt32 presetValue))
                PresetValue = presetValue;
            else
                PresetValue = 0;

            //ResPara
            for (int i = 0; i < 5; i++)
            {
                if (byte.TryParse(GuiStringList[i + 6], out byte resValue))
                    ResParaArr[i] = resValue;
                else
                    ResParaArr[i] = 0;
            }


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
            GuiStringList.Add($"{ResolutionDic[Resolution]}");
            //Revolution
            GuiStringList.Add($"{RevolutionDic[Revolution]}");

            GuiStringList.Add($"{PresetValue}");

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
        protected ModuleConfig_HL5002(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
