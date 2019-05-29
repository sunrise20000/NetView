﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ControlTest.ModuleConfigModle
{
    public class ModuleCfg_HL2003 : ModuleCfgModleBase
    {
        protected override int GuiStringListNumber { get; } = 3;
        public ModuleCfg_HL2003()
        {
            DeviceName = EnumDeviceName.HL2003;
        }


        public override void FromString(params string[] ParaList)
        {
            if (ParaList.Length != GuiStringListNumber)
                throw new Exception($"Wrong para number when parse {DeviceName.ToString()} formstring");
            Name = GuiStringList[0];
            Function = GuiStringList[1];
            Plug_Sequence = GuiStringList[2];

        }

        protected override void SetProfile()
        {
            GuiStringList.Clear();

            GuiStringList.Add(Name);

            GuiStringList.Add("DO8xDC24V 2.0A");

            GuiStringList.Add(Function);
        }

    }
}