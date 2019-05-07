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
        
        public void LoadXml(string FilePath)
        {
            xmlDoc = XDocument.Load(FilePath);
        }

        public string ReadNodeString(string SunItemValue)
        {
            var n =(from node in xmlDoc.Element("EtherCATInfo").Element("Descriptions").Element("Devices").Element("Device").Element("Profile").Element("Dictionary").Element("DataTypes").Elements() where node.Element("Name")!=null && node.Element("Name").Value.Equals("DT1018") select node).FirstOrDefault();
            var v = (from vs in n.Elements() where vs.Name.LocalName.Equals("SubItem") && vs.Element("SubIdx").Value.Equals(SunItemValue) select vs).FirstOrDefault();
            return v.Element("Name").Value;
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
    }
}
