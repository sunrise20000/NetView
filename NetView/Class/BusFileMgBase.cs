using NetView.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Class
{
    public class BusFileMgBase
    {
        public string ExtString { get; protected set; }

        public virtual void LoadFile(string FileName)
        {
            throw new NotImplementedException();
        }

        public virtual List<ModuleNameModel> GetDeviceList()
        {
            throw new NotImplementedException();
        }

        public virtual void SaveFile(List<ModuleNameModel> NameModelList, string FileName)
        {
            throw new NotImplementedException();
        }


    }
}
