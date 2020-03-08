﻿using ControllerLib.Ethercat.ModuleConfigModle;
using ControllerLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerLib
{
    public abstract class ControllerBase
    {
        public abstract byte ControllerID { get;}  
        public abstract bool IsConnected { get; protected set; }
        public abstract bool Open(string Port);
        public abstract bool Connect();
        public abstract bool DisConnect();
        public abstract List<ModuleConfigModleBase> GetModuleList();
		public abstract event EventHandler<bool> OnConnectStateChanged;
		public abstract event EventHandler<DataFromComport> OnDataComeHandler;

        /// <summary>
        /// PureNameList
        /// </summary>
        /// <param name="ModuleNameList"></param>
        /// <returns></returns>
        public abstract bool SendModuleList(List<ModuleConfigModleBase> ModuleNameList);

        public abstract bool GetModuleValue(List<UInt32> ModifyValueList,out List<UInt32> InputValueList, out List<UInt32> OutputValueList);

        public abstract void SetModuleValue(List<UInt32> OutputValueList);

        public abstract bool Hearbeat();

        public abstract void CLose();

    }
}
