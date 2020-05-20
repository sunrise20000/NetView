using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControllerLib.Ethercat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ControllerLib.Ethercat.Tests
{
    [TestClass()]
    public class EC_ControllerTests
    {
        [TestMethod()]
        public void GetModuleListTest()
        {
            var model = new EC_Controller();
            model.Open("COM4");
			model.Connect();
			var x = model.GetModuleList();
			if (x.Count == 0)
				Assert.Fail();
			Console.WriteLine($"{x[0].ToByteArr()}  {x[0].DeviceName}");

			//for (var i = 0; i < 50; i++)
			//{
			//	var b =model.GetModuleValue(new List<uint>{ 0x55 }, out List<uint> inList, out List<uint> outList);
			//	if (!b ||(inList.Count != 1 || outList.Count != 1))
			//		Assert.Fail();
			//	Console.WriteLine($"Output = {inList[0]},Input = {outList[0]}");
			//}

			//model.GetModuleValue(new List<uint>() { 0x55 }, out List<uint> inList, out List<uint> outList);
            model.CLose();

        }
    }
}