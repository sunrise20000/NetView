﻿using ControlTest.ModuleConfigModle;
using ControllerLib.BusConfigModle;
using ControllerLib.Ethercat.ModuleConfigModle;
using NetView.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetView.Class
{
    public class ProjectController
    {
        private string FileFullPathName =@".\";
        public BusConfigBase BusCfg { get; set; }

        public List<ModuleConfigModleBase> ModuleConfigList { get; set; } = new List<ModuleConfigModleBase>();

        public BusFileMgBase BusFileMgr { get; set; }

        public ProjectController()
        {

        }
        public ProjectController(BusConfigBase BusCfg, BusFileMgBase BusFileMgr)
        {
            this.BusCfg = BusCfg;
            this.BusFileMgr = BusFileMgr;
        }

        public bool OpenProject()
        {
            ModuleConfigList.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            var strFilter = $"hcp File(*.hcp)|*.hcp";
            ofd.Filter = strFilter;
            ofd.ValidateNames = true; // 验证用户输入是否是一个有效的Windows文件名
            ofd.CheckFileExists = true; //验证路径的有效性
            ofd.CheckPathExists = true;//验证路径的有效性
            ofd.InitialDirectory = FileFullPathName;
            if (ofd.ShowDialog() == DialogResult.OK) //用户点击确认按钮，发送确认消息
            {
                FileFullPathName = ofd.FileName;//获取在文件对话框中选定的路径或者字符串

                IFormatter formatter = new BinaryFormatter();
                FileStream s = new FileStream(FileFullPathName, FileMode.Open);
                if (s.Position <s.Length)
                {
                    BusCfg = (BusConfigBase)formatter.Deserialize(s);
                    while (s.Position < s.Length)
                    {
                        ModuleConfigList.Add(formatter.Deserialize(s) as ModuleConfigModleBase);
                    }
                    s.Close();
					return true;
                }
            }
			return false;
        }

        public void SaveProject()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            var strFilter = $"hcp File(*.hcp)|*.hcp";
            sfd.Filter = strFilter;
            sfd.FilterIndex = 2;
            sfd.RestoreDirectory = true;
            sfd.InitialDirectory = FileFullPathName;
            sfd.FileName = $"{BusCfg.ShortName}.hcp";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileFullPathName = sfd.FileName;
                var NameModelList = new List<ModuleNameModel>();
                IFormatter formatter = new BinaryFormatter();
                FileStream s = new FileStream(FileFullPathName, FileMode.Create);
                formatter.Serialize(s, BusCfg);
                for(int i=0;i< ModuleConfigList.Count;i++)
                    formatter.Serialize(s, ModuleConfigList[i]);
                s.Close();               
            }
        }

		/// <summary>
		/// Bus的名称，不是类的名称
		/// </summary>
        public string BusName { get {
                return BusCfg.Type.ToString();
            } }
        public List<Tuple<string, int, int, ModuleGUIBase>> SubBusNameWithIndexList(bool FromStringList=true)
        {
            
            var L = new List<Tuple<string,int,int, ModuleGUIBase>>();
            Type T = typeof(ModuleGUIBase);
          
            foreach (var it in ModuleConfigList)
            {
                var ClassName = $"ControlTest.ModuleConfigModle.ModuleGUI_{it.DeviceName.ToString()}";
                var Mcb = T.Assembly.CreateInstance(ClassName) as ModuleGUIBase;
                if (FromStringList)
                    Mcb.FromString(it.GuiStringList.ToArray());
                else
                    Mcb.FromString(it.ToStringList().ToArray());
                L.Add(new Tuple<string, int, int, ModuleGUIBase>(it.DeviceName.ToString(), it.LocalIndex, it.GlobalIndex, Mcb));
            }
            return L;
            
        }

    }
}
