using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControllerLib.Ethercat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerLib.Ethercat.Tests
{
    [TestClass()]
    public class EC_ControllerTests
    {
        [TestMethod()]
        public void GetModuleListTest()
        {
            var model = new EC_Controller();
            model.Open("COM1");
            var x=model.GetModuleList();
            model.CLose();
            if (x.Count == 0)
                Assert.Fail();

        }
    }
}