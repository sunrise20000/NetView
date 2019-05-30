
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ControlTest.ModuleConfigModle
{
    public class ModuleCfgModleBase
    {
        protected virtual int GuiStringListNumber { get; }=0;

        protected List<string> GuiStringList = new List<string>();

        protected void GetStringFromList(List<string> L, params string[] Str)
        {
            if (L.Count != Str.Count())
                throw new Exception("Can't GetStringFromList");
            for (int i = 0; i < L.Count; i++)
                Str[i] = L[i];
        }
        protected void GetListFromStr(List<string> L, params string[] Str)
        {
            L.Clear();
            if (GuiStringListNumber != Str.Count())
                throw new Exception("Can't GetStringFromList");
            for (int i = 0; i < GuiStringListNumber; i++)
                L.Add(Str[i]);
        }

        [Browsable(false)]
        public EnumDeviceName DeviceName { get; protected set; }

        [ReadOnly(true)]
        public string Name { get; set; } = "";

        [ReadOnly(true)]
        public string Function { get; protected set; } = "";

        [ReadOnly(true)]
        public string Plug_Sequence { get; set; } = "";


        protected virtual void SetProfile()
        {

        }

        public virtual void FromString(params string[] ParaList)
        {
            throw new NotImplementedException();
        }
        public  List<string> ToStringList()
        {
            SetProfile();
            return GuiStringList;
        }

    }
}
