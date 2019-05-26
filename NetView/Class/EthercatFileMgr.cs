using NetView.Definations;
using NetView.Model;
using NetView.Model.ModuleInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Class
{
    public class EthercatFileMgr : BusFileMgBase
    {
     
        private List<ModuleInfoBase> ModuleInfoList  = new List<ModuleInfoBase>();
        private  XmlParse Xml = new XmlParse();
        private string FileName="";
        public EthercatFileMgr()
        {
            ExtString = "xml";
        }

        public override void LoadFile(string FileName)
        {
            this.FileName = FileName;
            Xml.LoadXml(FileName);
        }

        public override List<ModuleNameModel> GetDeviceList()
        {
           
            var Output= GetDeviceListOutput();
            var Input = GetDeviceListInput();
            Dictionary<int, ModuleNameModel> NameListDic = new Dictionary<int, ModuleNameModel>();
            foreach (var it in Output)
            {
                if(!NameListDic.Keys.Contains(it.TotolIndex))
                    NameListDic.Add(it.TotolIndex,it);   
            }
            foreach (var it in Input)
            {
                if (!NameListDic.Keys.Contains(it.TotolIndex))
                    NameListDic.Add(it.TotolIndex, it);    
            }
            NameListDic.ToList().Sort((a, b) => a.Key.CompareTo(b.Key));
            var NameModleList=new List<ModuleNameModel>();
            foreach (var it in NameListDic)
            {
                NameModleList.Add(it.Value);
            }
            return NameModleList;
        }

        public override void SaveFile(List<ModuleNameModel> NameModelList, string FileName)
        {
            var ModuleInfoList = new List<ModuleInfoBase>();
            foreach (var it in NameModelList)
            {
                string insName = "NetView.Model.ModuleInfo.ModuleInfo_" + it.PureName;
                Type type = Type.GetType(insName);
                var mi = type.Assembly.CreateInstance(insName);
                ModuleInfoBase obj = mi as ModuleInfoBase;
                obj.Name = it.PureName;
                obj.DeviceType = (EnumDeviceName)Enum.Parse(typeof(EnumDeviceName), it.PureName);
                ModuleInfoList.Add(obj);
            }
            var ListAdjust= AdjustName(ModuleInfoList);

            //输出
            //SaveDT1601(ListAdjust);
            //SaveDT7010(ListAdjust);
            //Savex1601(ListAdjust);
            //Savex7010(ListAdjust);
            //SaveRxPdo(ListAdjust);

            ////输入
            //SaveDT1A00(ListAdjust);
            //SaveDT6001(ListAdjust);
            //Savex1A00(ListAdjust);
            //Savex6001(ListAdjust);
            //SaveTxPdo(ListAdjust);
            SaveDT1(ListAdjust, EnumModuleIOType.OUT);
            SaveDT2(ListAdjust, EnumModuleIOType.OUT);
            Savex1(ListAdjust, EnumModuleIOType.OUT);
            Savex2(ListAdjust, EnumModuleIOType.OUT);
            SavePdo(ListAdjust, EnumModuleIOType.OUT);

            SaveDT1(ListAdjust, EnumModuleIOType.IN);
            SaveDT2(ListAdjust, EnumModuleIOType.IN);
            Savex1(ListAdjust, EnumModuleIOType.IN);
            Savex2(ListAdjust, EnumModuleIOType.IN);
            SavePdo(ListAdjust, EnumModuleIOType.IN);

            Xml.Save(FileName);
        }

        
        #region Output

        /// <summary>
        /// 总的输出
        /// </summary>
        /// <param name="ListAdjust"></param>
        private void SaveDT1601(List<ModuleInfoBase> ListAdjust)
        {
            int BitOffs = 16;
            int SubIdx = 1;

            var n = Xml.GetSingleElement(null, "EtherCATInfo", "Descriptions", "Devices", "Device", "Profile", "Dictionary", "DataTypes");
            var es = Xml.GetMutifyElement(n, "DataType");
            var Dic = new Dictionary<string, string>();
            Dic.Add("Name", "DT1601");
            var v = Xml.GetElementFromMutiElement(es, Dic);
            var ss = Xml.GetMutifyElement(v, "SubItem");
            while (Xml.GetMutifyElement(v, "SubItem").Count() > 1)
            {
                v.Elements().ElementAt(v.Elements().Count() - 1).Remove();
            }

            foreach (var Module in ListAdjust)
            {
                var DesModules = Module.ModuleList.Where(m => m.IOType == EnumModuleIOType.OUT);
                for (int j = 0; j < DesModules.Count(); j++)
                {
                    var DesModule = DesModules.ElementAt(j);
                    var e = Xml.CreateElement("SubItem");
                    Xml.SetSubElement(e, "SubIdx", "Name", "Type", "BitSize", "BitOffs", "Flags");

                    Xml.SetSubElementValue(e, "SubIdx", $"{SubIdx}");
                    Xml.SetSubElementValue(e, "Name", string.Format("SunIndex {0:D3}", SubIdx++));
                    Xml.SetSubElementValue(e, "Type", $"{DesModule.DataTypeOfSubItem.ToString()}");
                    Xml.SetSubElementValue(e, "BitSize", $"{DesModule.BitSize}");
                    Xml.SetSubElementValue(e, "BitOffs", $"{BitOffs}");
                    BitOffs += DesModule.BitSize;

                    var ee = Xml.GetSingleElement(e, "Flags");
                    Xml.SetSubElement(ee, "Access", "Category");

                    var eee = Xml.GetSingleElement(ee, "Access");
                    Xml.SetSubElementValue(ee, "Access", "ro");

                    Xml.SetSubElement(v, e);
                }
            }
        
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
                var DesModules = Module.ModuleList.Where(m => m.IOType == EnumModuleIOType.OUT);
                for (int j = 0; j < DesModules.Count(); j++)
                {
                    var DesModule = DesModules.ElementAt(j);
                    var e = Xml.CreateElement("SubItem");
                    Xml.SetSubElement(e, "SubIdx", "Name", "Type", "BitSize", "BitOffs", "Flags");

                    Xml.SetSubElementValue(e, "SubIdx", $"{SubIdx++}");
                    Xml.SetSubElementValue(e, "Name", $"{DesModule.Header}{Module.Name}");
                    Xml.SetSubElementValue(e, "Type", $"{DesModule.DataTypeOfSubItem.ToString()}");
                    Xml.SetSubElementValue(e, "BitSize", $"{DesModule.BitSize}");
                    Xml.SetSubElementValue(e, "BitOffs", $"{BitOffs}");
                    BitOffs += DesModule.BitSize;

                    var ee = Xml.GetSingleElement(e, "Flags");
                    Xml.SetSubElement(ee, "Access", "Category");

                    var eee = Xml.GetSingleElement(ee, "Access");
                    Xml.SetSubElementValue(ee, "Access", "ro");
                    Xml.SetSubElementValue(ee, "Category", "o");

                    Xml.SetSubElement(v, e);
                }
            }

        }

        private void Savex1601(List<ModuleInfoBase> ListAdjust)
        {
            int SubIdx = 1;
            var n = Xml.GetSingleElement(null, "EtherCATInfo", "Descriptions", "Devices", "Device", "Profile", "Dictionary", "Objects");
            var es = Xml.GetMutifyElement(n, "Object");
            var Dic = new Dictionary<string, string>();
            Dic.Add("Index", "#x1601");
            var v = Xml.GetElementFromMutiElement(es, Dic);

            var infoE = Xml.GetSingleElement(v, "Info");
            var ss = Xml.GetMutifyElement(infoE, "SubItem");

            while (Xml.GetMutifyElement(infoE, "SubItem").Count() > 1)
            {
                infoE.Elements().ElementAt(infoE.Elements().Count() - 1).Remove();
            }

            foreach (var Module in ListAdjust)
            {
                var DesModules = Module.ModuleList.Where(m => m.IOType == EnumModuleIOType.OUT);
                for (int j = 0; j < DesModules.Count(); j++)
                {
                    var DesModule = DesModules.ElementAt(j);
                    var e = Xml.CreateElement("SubItem");
                    Xml.SetSubElement(e, "Name", "Info");

                    Xml.SetSubElementValue(e, "Name", string.Format("SunIndex {0:D3}", SubIdx));
                    var ee=Xml.GetSingleElement(e, "Info");
                    Xml.SetSubElement(ee, "DefaultData");
                    //08011070   0710    01   08
                    Xml.SetSubElementValue(ee, "DefaultData", string.Format("{0:D2}{1:D2}1070", DesModule.BitSize, SubIdx++));
                    Xml.SetSubElement(infoE, e);
                }
            }
            //修改第一个SubItem的DefaultData
            var FirstE=Xml.GetSingleElement(infoE, "SubItem", "Info");
            Xml.SetSubElementValue(FirstE, "DefaultData", $"{SubIdx - 1}");
        }

        private void Savex7010(List<ModuleInfoBase> ListAdjust)
        {
            int SubIdx = 1;
            var n = Xml.GetSingleElement(null, "EtherCATInfo", "Descriptions", "Devices", "Device", "Profile", "Dictionary", "Objects");
            var es = Xml.GetMutifyElement(n, "Object");
            var Dic = new Dictionary<string, string>();
            Dic.Add("Index", "#x7010");
            var v = Xml.GetElementFromMutiElement(es, Dic);

            var infoE = Xml.GetSingleElement(v, "Info");
            var ss = Xml.GetMutifyElement(infoE, "SubItem");

            while (Xml.GetMutifyElement(infoE, "SubItem").Count() > 1)
            {
                infoE.Elements().ElementAt(infoE.Elements().Count() - 1).Remove();
            }

            StringBuilder sb = new StringBuilder();
            foreach (var Module in ListAdjust)
            {
                var DesModules = Module.ModuleList.Where(m => m.IOType == EnumModuleIOType.OUT);
                for (int j = 0; j < DesModules.Count(); j++)
                {
                    var DesModule = DesModules.ElementAt(j);
                    var e = Xml.CreateElement("SubItem");
                    Xml.SetSubElement(e, "Name", "Info");

                    Xml.SetSubElementValue(e, "Name",$"{DesModule.Header}{Module.Name}");
                    var ee = Xml.GetSingleElement(e, "Info");
                    Xml.SetSubElement(ee, "DefaultData");
                    //00-8Bit, 0000-16Bit, 00000000-32Bit
                    sb.Clear();
                    for (int i = 0; i < DesModule.BitSize / 8; i++)
                        sb.Append("00");
                    Xml.SetSubElementValue(ee, "DefaultData", sb.ToString());
                    Xml.SetSubElement(infoE, e);
                    SubIdx++;
                }
            }
            //修改第一个SubItem的DefaultData
            var FirstE = Xml.GetSingleElement(infoE, "SubItem", "Info");
            Xml.SetSubElementValue(FirstE, "DefaultData", $"{SubIdx - 1}");
        }

        private void SaveRxPdo(List<ModuleInfoBase> ListAdjust)
        {
            int SubIdx = 1;
            var n = Xml.GetSingleElement(null, "EtherCATInfo", "Descriptions", "Devices", "Device", "RxPdo");

            while (Xml.GetMutifyElement(n, "Entry").Count() > 0)
            {
                n.Elements().ElementAt(n.Elements().Count() - 1).Remove();
            }

            foreach (var Module in ListAdjust)
            {
                var DesModules = Module.ModuleList.Where(m => m.IOType == EnumModuleIOType.OUT);
                for (int j = 0; j < DesModules.Count(); j++)
                {
                    var DesModule = DesModules.ElementAt(j);
                    var e = Xml.CreateElement("Entry");
                    Xml.SetSubElement(e, "Index", "SubIndex", "BitLen", "Name", "DataType");

                    Xml.SetSubElementValue(e, "Index", $"#x7010");
                    Xml.SetSubElementValue(e, "SubIndex", $"{SubIdx++}");
                    Xml.SetSubElementValue(e, "BitLen", $"{DesModule.BitSize}");
                    Xml.SetSubElementValue(e, "Name", $"{DesModule.Header}{Module.Name}");
                    Xml.SetSubElementValue(e, "DataType", $"{DesModule.DataTypeOfSubItem}");

                    Xml.SetSubElement(n, e);
                }
            }
        }
        #endregion

        #region Input
        private void SaveDT1A00(List<ModuleInfoBase> ListAdjust)
        {
            int BitOffs = 16;
            int SubIdx = 1;

            var n = Xml.GetSingleElement(null, "EtherCATInfo", "Descriptions", "Devices", "Device", "Profile", "Dictionary", "DataTypes");
            var es = Xml.GetMutifyElement(n, "DataType");
            var Dic = new Dictionary<string, string>();
            Dic.Add("Name", "DT1A00");
            var v = Xml.GetElementFromMutiElement(es, Dic);
            var ss = Xml.GetMutifyElement(v, "SubItem");
            while (Xml.GetMutifyElement(v, "SubItem").Count() > 1)
            {
                v.Elements().ElementAt(v.Elements().Count() - 1).Remove();
            }

            foreach (var Module in ListAdjust)
            {
                var DesModules = Module.ModuleList.Where(m => m.IOType == EnumModuleIOType.IN);
                for (int j = 0; j < DesModules.Count(); j++)
                {
                    var DesModule = DesModules.ElementAt(j);
                    var e = Xml.CreateElement("SubItem");
                    Xml.SetSubElement(e, "SubIdx", "Name", "Type", "BitSize", "BitOffs", "Flags");

                    Xml.SetSubElementValue(e, "SubIdx", $"{SubIdx}");
                    Xml.SetSubElementValue(e, "Name", string.Format("SunIndex {0:D3}", SubIdx++));
                    Xml.SetSubElementValue(e, "Type", $"{DesModule.DataTypeOfSubItem.ToString()}");
                    Xml.SetSubElementValue(e, "BitSize", $"{DesModule.BitSize}");
                    Xml.SetSubElementValue(e, "BitOffs", $"{BitOffs}");
                    BitOffs += DesModule.BitSize;

                    var ee = Xml.GetSingleElement(e, "Flags");
                    Xml.SetSubElement(ee, "Access", "Category");

                    var eee = Xml.GetSingleElement(ee, "Access");
                    Xml.SetSubElementValue(ee, "Access", "ro");

                    Xml.SetSubElement(v, e);
                }
            }

        }
        private void SaveDT6001(List<ModuleInfoBase> ListAdjust)
        {
            int BitOffs = 16;
            int SubIdx = 1;

            var n = Xml.GetSingleElement(null, "EtherCATInfo", "Descriptions", "Devices", "Device", "Profile", "Dictionary", "DataTypes");
            var es = Xml.GetMutifyElement(n, "DataType");
            var Dic = new Dictionary<string, string>();
            Dic.Add("Name", "DT6001");
            var v = Xml.GetElementFromMutiElement(es, Dic);
            var ss = Xml.GetMutifyElement(v, "SubItem");
            while (Xml.GetMutifyElement(v, "SubItem").Count() > 1)
            {
                v.Elements().ElementAt(v.Elements().Count() - 1).Remove();
            }

            foreach (var Module in ListAdjust)
            {
                var DesModules = Module.ModuleList.Where(m => m.IOType == EnumModuleIOType.IN);
                for (int j = 0; j < DesModules.Count(); j++)
                {
                    var DesModule = DesModules.ElementAt(j);
                    var e = Xml.CreateElement("SubItem");
                    Xml.SetSubElement(e, "SubIdx", "Name", "Type", "BitSize", "BitOffs", "Flags");

                    Xml.SetSubElementValue(e, "SubIdx", $"{SubIdx++}");
                    Xml.SetSubElementValue(e, "Name", $"{DesModule.Header}{Module.Name}");
                    Xml.SetSubElementValue(e, "Type", $"{DesModule.DataTypeOfSubItem.ToString()}");
                    Xml.SetSubElementValue(e, "BitSize", $"{DesModule.BitSize}");
                    Xml.SetSubElementValue(e, "BitOffs", $"{BitOffs}");
                    BitOffs += DesModule.BitSize;

                    var ee = Xml.GetSingleElement(e, "Flags");
                    Xml.SetSubElement(ee, "Access", "Category");

                    Xml.SetSubElementValue(ee, "Category", "o");
                    Xml.SetSubElementValue(ee, "Access", "ro");
                    

                    Xml.SetSubElement(v, e);
                }
            }
        }

        private void Savex1A00(List<ModuleInfoBase> ListAdjust)
        {
            int SubIdx = 1;
            var n = Xml.GetSingleElement(null, "EtherCATInfo", "Descriptions", "Devices", "Device", "Profile", "Dictionary", "Objects");
            var es = Xml.GetMutifyElement(n, "Object");
            var Dic = new Dictionary<string, string>();
            Dic.Add("Index", "#x1A00");
            var v = Xml.GetElementFromMutiElement(es, Dic);

            var infoE = Xml.GetSingleElement(v, "Info");
            var ss = Xml.GetMutifyElement(infoE, "SubItem");

            while (Xml.GetMutifyElement(infoE, "SubItem").Count() > 1)
            {
                infoE.Elements().ElementAt(infoE.Elements().Count() - 1).Remove();
            }

            foreach (var Module in ListAdjust)
            {
                var DesModules = Module.ModuleList.Where(m => m.IOType == EnumModuleIOType.IN);
                for (int j = 0; j < DesModules.Count(); j++)
                {
                    var DesModule = DesModules.ElementAt(j);
                    var e = Xml.CreateElement("SubItem");
                    Xml.SetSubElement(e, "Name", "Info");

                    Xml.SetSubElementValue(e, "Name", string.Format("SunIndex {0:D3}", SubIdx));
                    var ee = Xml.GetSingleElement(e, "Info");
                    Xml.SetSubElement(ee, "DefaultData");
                    //08011070   0710    01   08
                    Xml.SetSubElementValue(ee, "DefaultData", string.Format("{0:D2}{1:D2}0160", DesModule.BitSize, SubIdx++));
                    Xml.SetSubElement(infoE, e);
                }
            }
            //修改第一个SubItem的DefaultData
            var FirstE = Xml.GetSingleElement(infoE, "SubItem", "Info");
            Xml.SetSubElementValue(FirstE, "DefaultData", $"{SubIdx - 1}");
        }
        private void Savex6001(List<ModuleInfoBase> ListAdjust)
        {
            int SubIdx = 1;
            var n = Xml.GetSingleElement(null, "EtherCATInfo", "Descriptions", "Devices", "Device", "Profile", "Dictionary", "Objects");
            var es = Xml.GetMutifyElement(n, "Object");
            var Dic = new Dictionary<string, string>();
            Dic.Add("Index", "#x6001");
            var v = Xml.GetElementFromMutiElement(es, Dic);

            var infoE = Xml.GetSingleElement(v, "Info");
            var ss = Xml.GetMutifyElement(infoE, "SubItem");

            while (Xml.GetMutifyElement(infoE, "SubItem").Count() > 1)
            {
                infoE.Elements().ElementAt(infoE.Elements().Count() - 1).Remove();
            }

            StringBuilder sb = new StringBuilder();
            foreach (var Module in ListAdjust)
            {
                var DesModules = Module.ModuleList.Where(m => m.IOType == EnumModuleIOType.IN);
                for (int j = 0; j < DesModules.Count(); j++)
                {
                    var DesModule = DesModules.ElementAt(j);
                    var e = Xml.CreateElement("SubItem");
                    Xml.SetSubElement(e, "Name", "Info");

                    Xml.SetSubElementValue(e, "Name", $"{DesModule.Header}{Module.Name}");
                    var ee = Xml.GetSingleElement(e, "Info");
                    Xml.SetSubElement(ee, "DefaultData");
                    //00-8Bit, 0000-16Bit, 00000000-32Bit
                    sb.Clear();
                    for (int i = 0; i < DesModule.BitSize / 8; i++)
                        sb.Append("00");
                    Xml.SetSubElementValue(ee, "DefaultData", sb.ToString());
                    Xml.SetSubElement(infoE, e);
                    SubIdx++;
                }
            }
            //修改第一个SubItem的DefaultData
            var FirstE = Xml.GetSingleElement(infoE, "SubItem", "Info");
            Xml.SetSubElementValue(FirstE, "DefaultData", $"{SubIdx - 1}");
        }
        private void SaveTxPdo(List<ModuleInfoBase> ListAdjust)
        {
            int SubIdx = 1;
            var n = Xml.GetSingleElement(null, "EtherCATInfo", "Descriptions", "Devices", "Device", "TxPdo");

            while (Xml.GetMutifyElement(n, "Entry").Count() > 0)
            {
                n.Elements().ElementAt(n.Elements().Count() - 1).Remove();
            }

            foreach (var Module in ListAdjust)
            {
                var DesModules = Module.ModuleList.Where(m => m.IOType == EnumModuleIOType.IN);
                for (int j = 0; j < DesModules.Count(); j++)
                {
                    var DesModule = DesModules.ElementAt(j);
                    var e = Xml.CreateElement("Entry");
                    Xml.SetSubElement(e, "Index", "SubIndex", "BitLen", "Name", "DataType");

                    Xml.SetSubElementValue(e, "Index", $"#x6001");
                    Xml.SetSubElementValue(e, "SubIndex", $"{SubIdx++}");
                    Xml.SetSubElementValue(e, "BitLen", $"{DesModule.BitSize}");
                    Xml.SetSubElementValue(e, "Name", $"{DesModule.Header}{Module.Name}");
                    Xml.SetSubElementValue(e, "DataType", $"{DesModule.DataTypeOfSubItem}");

                    Xml.SetSubElement(n, e);
                }
            }
        }
        #endregion

        #region Private Method
        private List<ModuleNameModel> GetDeviceListInput()
        {
            List<string> ModuleNameList = new List<string>();
            var n = Xml.GetSingleElement(null, "EtherCATInfo", "Descriptions", "Devices", "Device", "Profile", "Dictionary", "DataTypes");
            var es = Xml.GetMutifyElement(n, "DataType");
            var Dic = new Dictionary<string, string>();
            Dic.Add("Name", "DT6001");
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
                        ModuleNameList.Add($"{list[nLen - 3]}_{list[nLen - 2]}_{list[nLen - 1]}");
                    }
                }
            }
            var L = ModuleNameList.Distinct();
            var NameModleList = new List<ModuleNameModel>();
            foreach (var l in L)
            {
                var list = l.Split('_');
                NameModleList.Add(new ModuleNameModel()
                {
                    TotolIndex = int.Parse(list[0]),
                    PureName = list[1],
                    LocalIndex = int.Parse(list[2])
                });
            }
            return NameModleList;
        }

        private List<ModuleNameModel> GetDeviceListOutput()
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
                        ModuleNameList.Add($"{list[nLen - 3]}_{list[nLen - 2]}_{list[nLen - 1]}");
                    }
                }
            }
            var L = ModuleNameList.Distinct();
            var NameModleList = new List<ModuleNameModel>();
            foreach (var l in L)
            {
                var list = l.Split('_');
                NameModleList.Add(new ModuleNameModel()
                {
                    TotolIndex = int.Parse(list[0]),
                    PureName = list[1],
                    LocalIndex = int.Parse(list[2])
                });
            }
            return NameModleList;
        }
        private List<ModuleInfoBase> AdjustName(List<ModuleInfoBase> ModuleList)
        {
            List<ModuleInfoBase> L = new List<ModuleInfoBase>();
            int i = 0;
            foreach (var it in ModuleList)
            {
                var ExistModule = L.Where(e => e.Name.Contains(it.Name));
                if (ExistModule != null)
                {
                    it.Name = $"{++i}_{it.Name}_{ExistModule.Count() + 1}";
                    L.Add(it);
                }
            }
            return L;
        }
        private void SaveDT1(List<ModuleInfoBase> ListAdjust,EnumModuleIOType IOType)
        {
            int BitOffs = 16;
            int SubIdx = 1;

            var n = Xml.GetSingleElement(null, "EtherCATInfo", "Descriptions", "Devices", "Device", "Profile", "Dictionary", "DataTypes");
            var es = Xml.GetMutifyElement(n, "DataType");
            var Dic = new Dictionary<string, string>();
            Dic.Add("Name", IOType==EnumModuleIOType.OUT? "DT1601" : "DT1A00");
            var v = Xml.GetElementFromMutiElement(es, Dic);
            var ss = Xml.GetMutifyElement(v, "SubItem");
            while (Xml.GetMutifyElement(v, "SubItem").Count() > 1)
            {
                v.Elements().ElementAt(v.Elements().Count() - 1).Remove();
            }

            foreach (var Module in ListAdjust)
            {
                var DesModules = Module.ModuleList.Where(m => m.IOType == IOType);
                for (int j = 0; j < DesModules.Count(); j++)
                {
                    var DesModule = DesModules.ElementAt(j);
                    var e = Xml.CreateElement("SubItem");
                    Xml.SetSubElement(e, "SubIdx", "Name", "Type", "BitSize", "BitOffs", "Flags");

                    Xml.SetSubElementValue(e, "SubIdx", $"{SubIdx}");
                    Xml.SetSubElementValue(e, "Name", string.Format("SunIndex {0:D3}", SubIdx++));
                    Xml.SetSubElementValue(e, "Type", $"{DesModule.DataTypeOfSubItem.ToString()}");
                    Xml.SetSubElementValue(e, "BitSize", $"{DesModule.BitSize}");
                    Xml.SetSubElementValue(e, "BitOffs", $"{BitOffs}");
                    BitOffs += DesModule.BitSize;

                    var ee = Xml.GetSingleElement(e, "Flags");
                    Xml.SetSubElement(ee, "Access", "Category");

                    var eee = Xml.GetSingleElement(ee, "Access");
                    Xml.SetSubElementValue(ee, "Access", "ro");

                    Xml.SetSubElement(v, e);
                }
            }
            Xml.SetSubElementValue(v, "BitSize", $"{BitOffs}");
        }

        /// <summary>
        /// 输出的详细名称
        /// </summary>
        /// <param name="ListAdjust"></param>
        private void SaveDT2(List<ModuleInfoBase> ListAdjust, EnumModuleIOType IOType)
        {
            int BitOffs = 16;
            int SubIdx = 1;

            var n = Xml.GetSingleElement(null, "EtherCATInfo", "Descriptions", "Devices", "Device", "Profile", "Dictionary", "DataTypes");
            var es = Xml.GetMutifyElement(n, "DataType");
            var Dic = new Dictionary<string, string>();
            Dic.Add("Name", IOType==EnumModuleIOType.OUT? "DT7010" : "DT6001");
            var v = Xml.GetElementFromMutiElement(es, Dic);
            var ss = Xml.GetMutifyElement(v, "SubItem");
            while (Xml.GetMutifyElement(v, "SubItem").Count() > 1)
            {
                v.Elements().ElementAt(v.Elements().Count() - 1).Remove();
            }

            foreach (var Module in ListAdjust)
            {
                var DesModules = Module.ModuleList.Where(m => m.IOType == IOType);
                for (int j = 0; j < DesModules.Count(); j++)
                {
                    var DesModule = DesModules.ElementAt(j);
                    var e = Xml.CreateElement("SubItem");
                    Xml.SetSubElement(e, "SubIdx", "Name", "Type", "BitSize", "BitOffs", "Flags");

                    Xml.SetSubElementValue(e, "SubIdx", $"{SubIdx++}");
                    Xml.SetSubElementValue(e, "Name", $"{DesModule.Header}{Module.Name}");
                    Xml.SetSubElementValue(e, "Type", $"{DesModule.DataTypeOfSubItem.ToString()}");
                    Xml.SetSubElementValue(e, "BitSize", $"{DesModule.BitSize}");
                    Xml.SetSubElementValue(e, "BitOffs", $"{BitOffs}");
                    BitOffs += DesModule.BitSize;

                    var ee = Xml.GetSingleElement(e, "Flags");
                    Xml.SetSubElement(ee, "Access", "Category");

                    var eee = Xml.GetSingleElement(ee, "Access");
                    Xml.SetSubElementValue(ee, "Access", "ro");
                    Xml.SetSubElementValue(ee, "Category", "o");

                    Xml.SetSubElement(v, e);
                }
            }
            Xml.SetSubElementValue(v, "BitSize", $"{BitOffs}");

        }

        private void Savex1(List<ModuleInfoBase> ListAdjust, EnumModuleIOType IOType)
        {
            int SubIdx = 1;
            var n = Xml.GetSingleElement(null, "EtherCATInfo", "Descriptions", "Devices", "Device", "Profile", "Dictionary", "Objects");
            var es = Xml.GetMutifyElement(n, "Object");
            var Dic = new Dictionary<string, string>();
            Dic.Add("Index", IOType==EnumModuleIOType.OUT? "#x1601" : "#x1A00");
            var v = Xml.GetElementFromMutiElement(es, Dic);

            var infoE = Xml.GetSingleElement(v, "Info");
            var ss = Xml.GetMutifyElement(infoE, "SubItem");

            while (Xml.GetMutifyElement(infoE, "SubItem").Count() > 1)
            {
                infoE.Elements().ElementAt(infoE.Elements().Count() - 1).Remove();
            }

            foreach (var Module in ListAdjust)
            {
                var DesModules = Module.ModuleList.Where(m => m.IOType == IOType);
                for (int j = 0; j < DesModules.Count(); j++)
                {
                    var DesModule = DesModules.ElementAt(j);
                    var e = Xml.CreateElement("SubItem");
                    Xml.SetSubElement(e, "Name", "Info");

                    Xml.SetSubElementValue(e, "Name", string.Format("SunIndex {0:D3}", SubIdx));
                    var ee = Xml.GetSingleElement(e, "Info");
                    Xml.SetSubElement(ee, "DefaultData");
                    //08011070   0710    01   08
                    Xml.SetSubElementValue(ee, "DefaultData", string.Format("{0:D2}{1:D2}{2}", DesModule.BitSize, SubIdx++, IOType==EnumModuleIOType.OUT? "1070" : "0160"));
                    Xml.SetSubElement(infoE, e);
                }
            }
            //修改第一个SubItem的DefaultData
            var FirstE = Xml.GetSingleElement(infoE, "SubItem", "Info");
            Xml.SetSubElementValue(FirstE, "DefaultData", $"{SubIdx - 1}");
        }

        private void Savex2(List<ModuleInfoBase> ListAdjust, EnumModuleIOType IOType)
        {
            int SubIdx = 1;
            var n = Xml.GetSingleElement(null, "EtherCATInfo", "Descriptions", "Devices", "Device", "Profile", "Dictionary", "Objects");
            var es = Xml.GetMutifyElement(n, "Object");
            var Dic = new Dictionary<string, string>();
            Dic.Add("Index", IOType==EnumModuleIOType.OUT? "#x7010" : "#x6001");
            var v = Xml.GetElementFromMutiElement(es, Dic);

            var infoE = Xml.GetSingleElement(v, "Info");
            var ss = Xml.GetMutifyElement(infoE, "SubItem");

            while (Xml.GetMutifyElement(infoE, "SubItem").Count() > 1)
            {
                infoE.Elements().ElementAt(infoE.Elements().Count() - 1).Remove();
            }

            StringBuilder sb = new StringBuilder();
            foreach (var Module in ListAdjust)
            {
                var DesModules = Module.ModuleList.Where(m => m.IOType == IOType);
                for (int j = 0; j < DesModules.Count(); j++)
                {
                    var DesModule = DesModules.ElementAt(j);
                    var e = Xml.CreateElement("SubItem");
                    Xml.SetSubElement(e, "Name", "Info");

                    Xml.SetSubElementValue(e, "Name", $"{DesModule.Header}{Module.Name}");
                    var ee = Xml.GetSingleElement(e, "Info");
                    Xml.SetSubElement(ee, "DefaultData");
                    //00-8Bit, 0000-16Bit, 00000000-32Bit
                    sb.Clear();
                    for (int i = 0; i < DesModule.BitSize / 8; i++)
                        sb.Append("00");
                    Xml.SetSubElementValue(ee, "DefaultData", sb.ToString());
                    Xml.SetSubElement(infoE, e);
                    SubIdx++;
                }
            }
            //修改第一个SubItem的DefaultData
            var FirstE = Xml.GetSingleElement(infoE, "SubItem", "Info");
            Xml.SetSubElementValue(FirstE, "DefaultData", $"{SubIdx - 1}");
        }

        private void SavePdo(List<ModuleInfoBase> ListAdjust, EnumModuleIOType IOType)
        {
            int SubIdx = 1;
            var n = Xml.GetSingleElement(null, "EtherCATInfo", "Descriptions", "Devices", "Device", IOType==EnumModuleIOType.OUT? "RxPdo" : "TxPdo");

            while (Xml.GetMutifyElement(n, "Entry").Count() > 0)
            {
                n.Elements().ElementAt(n.Elements().Count() - 1).Remove();
            }

            foreach (var Module in ListAdjust)
            {
                var DesModules = Module.ModuleList.Where(m => m.IOType == IOType);
                for (int j = 0; j < DesModules.Count(); j++)
                {
                    var DesModule = DesModules.ElementAt(j);
                    var e = Xml.CreateElement("Entry");
                    Xml.SetSubElement(e, "Index", "SubIndex", "BitLen", "Name", "DataType");

                    Xml.SetSubElementValue(e, "Index", IOType==EnumModuleIOType.OUT? "#x7010" : "#x6001");
                    Xml.SetSubElementValue(e, "SubIndex", $"{SubIdx++}");
                    Xml.SetSubElementValue(e, "BitLen", $"{DesModule.BitSize}");
                    Xml.SetSubElementValue(e, "Name", $"{DesModule.Header}{Module.Name}");
                    Xml.SetSubElementValue(e, "DataType", $"{DesModule.DataTypeOfSubItem}");

                    Xml.SetSubElement(n, e);
                }
            }
        }
        #endregion

    }
}
