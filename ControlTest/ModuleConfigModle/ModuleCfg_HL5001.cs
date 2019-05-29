
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ControlTest.ModuleConfigModle
{
    [Serializable()]
    public class ModuleCfg_HL5001 : ModuleCfgModleBase
    {

        protected override int GuiStringListNumber { get; } = 11;
        public ModuleCfg_HL5001()
        {
            DeviceName = EnumDeviceName.HL5001;
        }

        public byte CounterLimitH { get; set; }

        public byte CounterLimitL { get; set; }

        public byte[] ResParaArr { get; } = new byte[6];

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

            Function = 0x51;

            //GlobalIndex
            GlobalIndex = int.Parse(GuiStringList[2]);

            //CounterLimitH
            CounterLimitH = byte.Parse(GuiStringList[3]);

            //CounterLimitL
            CounterLimitL= byte.Parse(GuiStringList[4]);

            //ResPara
            for (int i = 0; i < 6; i++)
                ResParaArr[i] = byte.Parse(GuiStringList[i+5]);


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
        protected ModuleCfg_HL5001(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
