using ControllerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ControllerLib.Ethercat.ModuleConfigModle.ConfigSubInfo;
using System.Text;
using System.Threading.Tasks;

namespace ControllerLib.Ethercat.ModuleConfigModle
{
    [Serializable()]
    public class ModuleConfigModleBase : ISerializable
    {
        protected virtual int GuiStringListNumber { get; }=0;

        public virtual int ByteArrayExpectLength { get; } = 0;

        public List<string> GuiStringList = new List<string>();

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

        public List<ModuleConfigBase> ModuleSubInfoList = new List<ModuleConfigBase>();

        /// <summary>
        ///Insert byte: -----> Function, LocalIndex, GlobalIndex
        /// </summary>
        /// <returns></returns>
        public virtual List<byte> ToByteArr()
        {
            BtArr.Clear();
            BtArr.Add(Function);
            BtArr.Add((byte)LocalIndex);
            BtArr.Add((byte)GlobalIndex);
            return BtArr;
        }

        /// <summary>
        /// 完全是一整个BtArr数组过来，恢复出模块的GlobalIndex与LocalIndex等信息，与后面的构成信息不一致
        /// </summary>
        /// <param name="BtArr"></param>
        public virtual void FromByteArray(byte[] BtArr)
        {
            if (BtArr.Length != ByteArrayExpectLength)
                throw new Exception($"Wrong length of BtArr when parse byteArr to ModuleConfigModule");
            Function = BtArr[0];
            LocalIndex = BtArr[1];
            GlobalIndex = BtArr[2];
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ToStringList();
            for (int i = 0; i < GuiStringListNumber; i++)
                info.AddValue($"L{i}", GuiStringList[i]);
        }

        /// <summary>
        /// 从Byte数组读取到SubModuleList，此函数为了解决模块输入和输出构成的问题，并且用于监视目的用
        /// </summary>
        /// <param name="BtArr"></param>
        /// <param name="StartPos"></param>
        /// <param name="Len"></param>
        public virtual void GetSubModuleListValueFromBtArr(byte[] BtArr, int StartPos, int Len)
        {
            throw new NotImplementedException();
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
