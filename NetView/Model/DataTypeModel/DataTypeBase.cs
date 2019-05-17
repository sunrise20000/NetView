using NetView.Class;
using NetView.Model.ModuleInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NetView.Model.DataTypeModel
{
    public enum EnumDeviceName
    {
        HL1001,
        HL2001,
        HL2002,
        HL2003,
        HL3001,
        HL3002,
        HL4001,
        HL4002,
    }

    public class DataTypeBase
    {
        EnumDeviceName deviceType = EnumDeviceName.HL1001;
        protected static XmlParse xmlParse =null;



        public DataTypeBase(string XmlFile)
        {
            if (xmlParse == null)
            {
                xmlParse = new XmlParse();
                xmlParse.LoadXml(XmlFile);
                ModuleInfoList = GetCurrentModuleList();
            }
        }

        public string DeviceType
        {
            get { return deviceType.ToString(); }
            set { Enum.TryParse(value, out deviceType); }

        }
        public int BitSize { get; set; } = 16;

        public int SubItemNum { get; set; }

        protected List<SubItemModelBaseForDataType> SubItemList { get; private set; } = new List<SubItemModelBaseForDataType>();
        public List<ModuleInfoBase> ModuleInfoList { get; set; }
        /// <summary>
        /// 默认是添加在最后
        /// </summary>
        /// <param name="ModuleType">类型</param>
        public void AddModule(params ModuleInfoBase[] Module)
        {
            foreach(var it in Module)
                ModuleInfoList.Add(it);
        }

        /// <summary>
        /// 插入模块
        /// </summary>
        /// <param name="ModuleType">Module的类型</param>
        /// <param name="PosBase0">第几个Module</param>
        public void InsertModule(ModuleInfoBase Module, int PosBase0)
        {
            if (PosBase0 < ModuleInfoList.Count)
                ModuleInfoList.Insert(PosBase0, Module);
            else
                AddModule(Module);
        }

        /// <summary>
        /// 移除第几个模块
        /// </summary>
        /// <param name="PosBase0"></param>
        public void RemoveModule(string ModuleName)
        {
            var Mi=ModuleInfoList.Where(info => info.Name.Equals(ModuleName)).FirstOrDefault();
            if (Mi != null)
                ModuleInfoList.Remove(Mi);
        }

        /// <summary>
        /// 修改模块信息
        /// </summary>
        public void ModifyModule(string ModuleName, ModuleInfoBase Module)
        {
            for (int i = 0; i < ModuleInfoList.Count; i++)
            {
                if (ModuleInfoList[i].Name.Equals(ModuleName))
                {
                    var OldMouduleInfo = ModuleInfoList[i];
                    ModuleInfoList.Insert(i,Module);
                    ModuleInfoList.Remove(OldMouduleInfo);
                }
            }
        }

        public List<ModuleInfo.ModuleInfoBase> GetCurrentModuleList()
        {
            List<string> ModuleNameList = new List<string>();
            List<ModuleInfo.ModuleInfoBase> ModuleInfoList = new List<ModuleInfo.ModuleInfoBase>();
            var n = xmlParse.GetSingleElement(null, "EtherCATInfo", "Descriptions", "Devices", "Device", "Profile", "Dictionary", "DataTypes");
            var es = xmlParse.GetMutifyElement(n, "DataType");
            var Dic = new Dictionary<string, string>();
            Dic.Add("Name", "DT1601");
            var v = xmlParse.GetElementFromMutiElement(es, Dic);
            var ss = xmlParse.GetMutifyElement(v, "SubItem");
            foreach (var e in ss)
            {
                //去掉第一个无用项
                if (!xmlParse.GetSubElementValue(e, "SubIdx").Equals("0"))
                {
                    var Name = xmlParse.GetSubElementValue(e, "Name");
                    var list = Name.Split('_');
                    int nLen = list.Length;
                    if (nLen > 2)
                    {
                        ModuleNameList.Add(list[nLen - 2] + "_" + list[nLen - 1]);
                    }
                }
            }
            var L = ModuleNameList.Distinct();
            foreach (var l in L)
            {
                var name = l.Split('_')[0];
                string insName = "NetView.Model.ModuleInfo.ModuleInfo_" + name;
                Type type = Type.GetType(insName);
                var mi = type.Assembly.CreateInstance(insName);
                ModuleInfo.ModuleInfoBase obj = mi as ModuleInfo.ModuleInfoBase;
                ModuleInfoList.Add(obj);
            }
            return ModuleInfoList;
        }
    }
}
