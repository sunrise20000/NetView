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
    public class ModuleConfigModleBase : ISerializable
    {
        protected virtual int GuiStringListNumber { get; }=0;

        protected List<string> GuiStringList = new List<string>();

        protected List<byte> BtArr = new List<byte>();


        public EnumDeviceName DeviceName { get; protected set; }

        public int LocalIndex { get; set; }

        public byte Function { get; protected set; }

        public int GlobalIndex { get; set; }

        public virtual void FromString(params string[] ParaList)
        {
            throw new NotImplementedException();
        }
        public virtual List<string> ToStringList()
        {
            throw new NotImplementedException();
        }

        public virtual List<byte> ToByteArr()
        {
            BtArr.Clear();
            BtArr.Add(Function);
            BtArr.Add((byte)LocalIndex);
            BtArr.Add((byte)GlobalIndex);
            return BtArr;
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ToStringList();
            for (int i = 0; i < GuiStringListNumber; i++)
                info.AddValue($"L{i}", GuiStringList[i]);
        }

        protected ModuleConfigModleBase(SerializationInfo info, StreamingContext context)
        {
            GuiStringList.Clear();
            for (int i = 0; i < GuiStringListNumber; i++)
                GuiStringList.Add(info.GetString($"L{i}"));
            FromString(GuiStringList.ToArray());
        }
        public ModuleConfigModleBase() { }
    }
}
