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
        private string FileName="";
        public EthercatSettingMgr(string FileName)
        {
            this.FileName = FileName;
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
                    if (nLen >= 2)
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
            var ListAdjust= AdjustName(DeviceList);
            SaveDT7010(ListAdjust);


            Xml.Save(FileName);
        }

        public void AddModule(ModuleInfoBase Device)
        {

        }

        private List<ModuleInfoBase> AdjustName(List<ModuleInfoBase> ModuleList)
        {
            List<ModuleInfoBase> L = new List<ModuleInfoBase>();
            foreach(var it in ModuleList)
            {
                var ExistModule = L.Where(e=>e.Name.Contains(it.Name));
                if (ExistModule != null)
                {
                    it.Name = $"{it.Name}_{ExistModule.Count()+1}";
                    L.Add(it);
                }
            }
            return L;
        }

        /// <summary>
        /// 总的输出
        /// </summary>
        /// <param name="ListAdjust"></param>
        private void SaveDT1601(List<ModuleInfoBase> ListAdjust)
        {

        }

        /// <summary>
        /// 输出的详细名称
        /// </summary>
        /// <param name="ListAdjust"></param>
        private void SaveDT7010(List<ModuleInfoBase> ListAdjust)
        {
            int BitOffs = 16;
            int SubIdx = 1;

            var n = Xml.GetSingleElement(null, "EtherCATInfo", "Descriptions", "Devices", "Device", "Profile", "Dictionary", "DataTypes");
            var es = Xml.GetMutifyElement(n, "DataType");
            var Dic = new Dictionary<string, string>();
            Dic.Add("Name", "DT7010");
            var v = Xml.GetElementFromMutiElement(es, Dic);
            var ss = Xml.GetMutifyElement(v, "SubItem");
            while (Xml.GetMutifyElement(v, "SubItem").Count() > 1)
            {
                v.Elements().ElementAt(v.Elements().Count() - 1).Remove();
            }

            foreach (var Module in ListAdjust)
            {
                var DesModule = Module.ModuleList.Where(m => m.IOType == EnumModuleIOType.OUT);
                for (int j = 0; j < DesModule.Count(); j++)
                {
                    var Input = DesModule.ElementAt(j);
                    var e = Xml.CreateElement("SubItem");
                    Xml.SetSubElement(e, "SubIdx", "Name", "Type", "BitSize", "BitOffs", "Flags");

                    Xml.SetSubElementValue(e, "SubIdx", $"{SubIdx++}");
                    Xml.SetSubElementValue(e, "Name", $"{Input.Header}{Module.Name}");
                    Xml.SetSubElementValue(e, "Type", $"{Input.DataTypeOfSubItem.ToString()}");
                    Xml.SetSubElementValue(e, "BitSize", $"{Input.BitSize}");
                    Xml.SetSubElementValue(e, "BitOffs", $"{BitOffs}");
                    BitOffs += DesModule.ElementAt(j).BitSize;

                    var ee = Xml.GetSingleElement(e, "Flags");
                    Xml.SetSubElement(ee, "Access", "Category");

                    var eee = Xml.GetSingleElement(ee, "Access");
                    Xml.SetSubElementValue(ee, "Access", "ro");
                    Xml.SetSubElementValue(ee, "Category", "o");

                    Xml.SetSubElement(v, e);
                }
            }

        }
       
    }
}
