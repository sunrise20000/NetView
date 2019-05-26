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
    public class ModuleConfig_HL1001 : ModuleConfigModleBase
    {
        byte[] TypeList = new byte[] {0x00, 0x01, 0x03,0x07, 0x0F,0x1F,0x3F,0x7F,0xFF};
        string[] TypeStringList = new string[] { "Normal", "DI1 as Alarm", "DI1~2 as Alarm", "DI1~3 as Alarm", "DI1~4 as Alarm" ,
                        "DI1~5 as Alarm","DI1~6 as Alarm","DI1~7 as Alarm","DI1~8 as Alarm"};
        Dictionary<byte, string> TypeDic = new Dictionary<byte, string>();

        public ModuleConfig_HL1001()
        {
            GuiStringListNumber = 4;
            DeviceName = EnumDeviceName.HL1001;
            for (int i = 0; i < TypeList.Count(); i++)
                TypeDic.Add(TypeList[i], TypeStringList[i]);
        }

        public byte  Type{
            get;
            set;
        }


        public override void FromString(params string[] ParaList)
        {
            if (ParaList.Length != GuiStringListNumber)
                throw new Exception($"Wrong para number when parse {DeviceName.ToString()} formstring");
            var L1 = GuiStringList[0].Split('_');
            //Name
            //LocalIndex
            LocalIndex = int.Parse(L1[1]);

            Function = 0x11;

            //GlobalIndex
            GlobalIndex = int.Parse(GuiStringList[2]);

            //Type
            Type = byte.Parse(GuiStringList[3]);

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
        protected ModuleConfig_HL1001(SerializationInfo info, StreamingContext context) : base(info,context)
        {

        }
    }
}
