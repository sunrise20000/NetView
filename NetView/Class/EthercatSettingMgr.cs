using NetView.Model.ModuleInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Class
{
    public class EthercatSettingMgr
    {
        private List<ModuleInfoBase> ModuleInfoList  = new List<ModuleInfoBase>();
        private  XmlParse Xml = null;
        public EthercatSettingMgr(string FileName)
        {
            Xml = new XmlParse();
            Xml.LoadXml(FileName);
        }

        public List<ModuleInfoBase> GetDeviceList()
        {
            List<string> ModuleNameList = new List<string>();
            var n = Xml.GetSingleElement(null, "EtherCATInfo", "Descriptions", "Devices", "Device", "Profile", "Dictionary", "DataTypes");
            var es = Xml.GetMutifyElement(n, "DataType");
            var Dic = new Dictionary<string, string>();
            Dic.Add("Name", "DT7010");
            var v = Xml.GetElementFromMutiElement(es, Dic);
            var ss = Xml.GetMutifyElement(v, "SubItem");
            foreach (var e in ss)
            {
                //去掉第一个无用项
                if (!Xml.GetSubElementValue(e, "SubIdx").Equals("0"))
                {
                    var Name = Xml.GetSubElementValue(e, "Name");
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
                ModuleInfoBase obj = mi as ModuleInfoBase;
                obj.Name = l;
                obj.DeviceType = (EnumDeviceName)Enum.Parse(typeof(EnumDeviceName), name);
                ModuleInfoList.Add(obj);
            }
            return ModuleInfoList;
        }

        public void SaveFile(List<ModuleInfoBase> DeviceList)
        {

        }

        public void AddModule(ModuleInfoBase Device)
        {

        }
 



    }
}
