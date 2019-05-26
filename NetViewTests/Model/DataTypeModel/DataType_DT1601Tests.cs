using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetView.Class;
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
            EthercatFileMgr Mgr = new EthercatFileMgr();
            Mgr.LoadFile(@"C:\Users\Public\projs\NetView\NetView\Document\HL6805.xml");
            var L = Mgr.GetDeviceList();
            foreach (var it in L)
                Console.WriteLine(it.PureName);
            //Assert.Fail();
        }

        [TestMethod()]
        public void SaveListTest()
        {
            EthercatFileMgr Mgr = new EthercatFileMgr();
            Mgr.LoadFile(@"C:\Users\Public\projs\NetView\NetView\Document\HL6805.xml");
            //Mgr.SaveFile(new List<ModuleInfo.ModuleInfoBase>() {
            //    new ModuleInfo.ModuleInfo_HL2001(),
            //    new ModuleInfo.ModuleInfo_HL4002(),
            //    new ModuleInfo.ModuleInfo_HL2001(),
            //    new ModuleInfo.ModuleInfo_HL5002(),
            //    new ModuleInfo.ModuleInfo_HL4002(),
            //    new ModuleInfo.ModuleInfo_HL2002(),
            //    new ModuleInfo.ModuleInfo_HL2001(),
            //    new ModuleInfo.ModuleInfo_HL2002(),
            //    new ModuleInfo.ModuleInfo_HL2001(),
            //}, @"C:\Users\Public\projs\NetView\NetView\Document\HL6805.xml");
            //Assert.Fail();
        }
    }
}