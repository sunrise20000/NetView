using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetView.Model.DataTypeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model.DataTypeModel.Tests
{
    [TestClass()]
    public class DataType_DT1601Tests
    {
        [TestMethod()]
        public void GetCurrentModuleListTest()
        {
            DataTypeBase DB = new DataType_DT1601(@"C:\Users\cn11321\source\repos\NetView\Document\HL6805.xml");
            var Info=DB.GetCurrentModuleList();
            //Assert.Fail();
        }
    }
}