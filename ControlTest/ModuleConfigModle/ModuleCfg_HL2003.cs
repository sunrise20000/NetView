
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ControlTest.ModuleConfigModle
{
    [Serializable()]
    public class ModuleCfg_HL2003 : ModuleCfgModleBase
    {
        protected override int GuiStringListNumber { get; } = 3;
        public ModuleCfg_HL2003()
        {
            DeviceName = EnumDeviceName.HL2003;
        }
  

        public override void FromString(params string[] ParaList)
        {
            if (ParaList.Length != 3)
                throw new Exception($"Wrong para number when parse {DeviceName.ToString()} formstring");
            var L1 = GuiStringList[0].Split('_');
            //Name
            Enum.TryParse(L1[0], out EnumDeviceName Dn);
            DeviceName = Dn;

            //LocalIndex
            LocalIndex = int.Parse(L1[1]);

            Function = 0x23;

            //GlobalIndex
            GlobalIndex = int.Parse(GuiStringList[2]);

        }

        public override List<string> ToStringList()
        {
            GuiStringList.Clear();
            //Name_LocalIndex
            GuiStringList.Add($"{DeviceName.ToString()}_{LocalIndex}");
            //Function
            GuiStringList.Add("DO8xDC24V 2.0A");
            //GlobalIndex
            GuiStringList.Add($"{GlobalIndex}");

            return GuiStringList;
        }
        public override List<byte> ToByteArr()
        {
            return base.ToByteArr();
        }

        protected ModuleCfg_HL2003(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

    }
}
