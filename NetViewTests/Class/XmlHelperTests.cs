using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetView.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Class.Tests
{
    [TestClass()]
    public class XmlHelperTests
    {
        [TestMethod()]
        public void XmlHelperTest()
        {
            XmlParse xml = new XmlParse();
            xml.LoadXml("2.xml");
            Console.WriteLine(xml.ReadNodeString("0"));
            xml.SetNodeString("0", "Ricky");
            xml.DeleteNode("2");
            xml.DeleteNode("3");
            xml.AddNode("KKKKK", "JJJJJ");

            xml.Save("3.xml");
            xml.LoadXml("3.xml");

           


            Console.WriteLine(xml.ReadNodeString("0"));
        }
    }
}