
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ControlTest.ModuleConfigModle
{
    [Serializable()]
    public class ModuleCfg_HL2001 : ModuleCfgModleBase
    {
        protected override int GuiStringListNumber { get; } = 3;

        public ModuleCfg_HL2001()
        {
            DeviceName = EnumDeviceName.HL2001;
        }
        public override void FromString(params string[] ParaList)
        {
            if (ParaList.Length != GuiStringListNumber)
                throw new Exception($"Wrong para number when parse {DeviceName.ToString()} formstring");
            var L1 = GuiStringList[0].Split('_');
            //Name
            Enum.TryParse(L1[0], out EnumDeviceName Dn);
            DeviceName = Dn;

            //LocalIndex
            LocalIndex = int.Parse(L1[1]);

            Function = 0x21;
 
            //GlobalIndex
            GlobalIndex = int.Parse(GuiStringList[2]);
     
        }

        public override List<string> ToStringList()
        {
            GuiStringList.Clear();
            //Name_LocalIndex
            GuiStringList.Add($"{DeviceName.ToString()}_{LocalIndex}");
            //Function
            GuiStringList.Add("DO8xDC24V 0.5A");
            //GlobalIndex
            GuiStringList.Add($"{GlobalIndex}");
    
            return GuiStringList;
        }
        public override List<byte> ToByteArr()
        {
            return base.ToByteArr();
        }


        protected ModuleCfg_HL2001(SerializationInfo info, StreamingContext context) : base(info, context)
        {
           
        }
    }
   
}
