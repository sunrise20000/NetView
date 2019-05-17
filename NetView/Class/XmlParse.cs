using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace NetView.Class
{
    public class XmlParse
    {
        XDocument xmlDoc = null;
        object xmlLock = new object();
        public void LoadXml(string FilePath)
        {
            xmlDoc = XDocument.Load(FilePath);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NodeStart">母项</param>
        /// <param name="MutifySubNodeName">有多个相同名称的子项目</param>
        /// <returns></returns>
        public IEnumerable<XElement> GetMutifyElement(XElement NodeFather,string MutifySubNodeName)
        {
            lock (xmlLock)
            {
               var nodes = NodeFather.Elements().Where(e=>e.Name.LocalName.Equals(MutifySubNodeName));
               return nodes;
            }
        }

        /// <summary>
        /// 得到单个Element
        /// </summary>
        /// <param name="NodeStart"></param>
        /// <param name="NodeList"></param>
        /// <returns></returns>
        public XElement GetSingleElement(XElement NodeStart, params string[] NodeList)
        {
            lock (xmlLock)
            {
                XElement E = null;
                for(int i=0;i< NodeList.Length;i++)
                {
                    if (NodeStart == null)
                    {
                        if (i == 0)
                            E = xmlDoc.Element(NodeList[i]);
                        else
                            E = E.Element(NodeList[i]);
                    }
                    else
                    {
                        if (i == 0)
                            E = NodeStart.Element(NodeList[i]);
                        else
                            E = E.Element(NodeList[i]);
                    }
                }
                return E;
            }
        }

        /// <summary>
        /// 从多个Element中选择出指定Spec的Element
        /// </summary>
        /// <param name="EleList">多个子项</param>
        /// <param name="SpecDic">Spec，带[]的为属性，不带的默认为子项</param>
        /// <returns></returns>
        public XElement GetElementFromMutiElement(IEnumerable<XElement> EleList, Dictionary<string, string> SpecDic)
        {
            lock (xmlLock)
            {
                foreach (var E in EleList)
                {
                    foreach (var Spec in SpecDic)
                    {
                        //说明是子单元区分
                        if (Spec.Key.First() != '[' && Spec.Key.Last() != ']')
                        {
                           var e= E.Elements().Where(ele => ele.Name.LocalName.Equals(Spec.Key) && ele.Value.Equals(Spec.Value)).FirstOrDefault();
                            if (e != null)
                                return E;
                        }
                        else //说明是属性区分
                        {

                            var a = E.Attributes().Where(att => att.Name.LocalName.Equals(Spec.Key) && att.Value.Equals(Spec.Value)).FirstOrDefault();
                            if (a != null)
                                return E;
                        }
                    }
                }
                return null;
            }
        }

        public string ReadNodeString(string SunItemValue)
        {
            var n = GetSingleElement(null, "EtherCATInfo", "Descriptions", "Devices", "Device", "Profile", "Dictionary", "DataTypes");
            var es = GetMutifyElement(n, "DataType");
            var Dic = new Dictionary<string, string>();
            Dic.Add("Name", "DT1018");
            var v = GetElementFromMutiElement(es, Dic);
            var ss = GetMutifyElement(v, "SubItem");
            Dic.Clear();
            Dic.Add("SubIdx", SunItemValue);
            var x= GetElementFromMutiElement(ss, Dic);
            //var n =(from node in xmlDoc.Element("EtherCATInfo").Element("Descriptions").Element("Devices").Element("Device").Element("Profile").Element("Dictionary").Element("DataTypes").Elements() where node.Element("Name")!=null && node.Element("Name").Value.Equals("DT1018") select node).FirstOrDefault();
            //var v = (from vs in n.Elements() where vs.Name.LocalName.Equals("SubItem") && vs.Element("SubIdx").Value.Equals(SunItemValue) select vs).FirstOrDefault();
            return x.Element("Name").Value;
        }

        public void SetNodeString(string SunItemValue,string NameValue)
        {
            var n = (from node in xmlDoc.Element("EtherCATInfo").Element("Descriptions").Element("Devices").Element("Device").Element("Profile").Element("Dictionary").Element("DataTypes").Elements() where node.Element("Name") != null && node.Element("Name").Value.Equals("DT1018") select node).FirstOrDefault();
            var v = (from vs in n.Elements() where vs.Name.LocalName.Equals("SubItem") && vs.Element("SubIdx").Value.Equals(SunItemValue) select vs).FirstOrDefault();
            v.SetElementValue("Name", NameValue);
            
        }

        public void Save(string FilePath)
        {
            xmlDoc.Save(FilePath);
        }

        public void DeleteNode(string SunItemValue)
        {
            var n = (from node in xmlDoc.Element("EtherCATInfo").Element("Descriptions").Element("Devices").Element("Device").Element("Profile").Element("Dictionary").Element("DataTypes").Elements() where node.Element("Name") != null && node.Element("Name").Value.Equals("DT1018") select node).FirstOrDefault();
            var v = (from vs in n.Elements() where vs.Name.LocalName.Equals("SubItem") && vs.Element("SubIdx").Value.Equals(SunItemValue) select vs).FirstOrDefault();
            if(v!=null)
                v.Remove();
        }

        public void AddNode(string NodeName, string SunItemValue)
        {
            var n = (from node in xmlDoc.Element("EtherCATInfo").Element("Descriptions").Element("Devices").Element("Device").Element("Profile").Element("Dictionary").Element("DataTypes").Elements() where node.Element("Name") != null && node.Element("Name").Value.Equals("DT1018") select node).FirstOrDefault();
            XElement xInsert = new XElement(NodeName, SunItemValue);
            xInsert.SetAttributeValue("Hello", "4545645");
            n.Add(xInsert);
        }

        public string GetSubElementValue(XElement E, string SubElementName)
        {
            lock (xmlLock)
            {
                return E.Element(SubElementName).Value;
            }
        }

        public string GetElementAttribute(XElement E, string AttributeName)
        {
            lock (xmlLock)
            {
                return E.Attribute(AttributeName).Value;
            }
        }
    }
}
