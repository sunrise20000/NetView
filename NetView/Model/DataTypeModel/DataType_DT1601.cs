using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model.DataTypeModel
{
    public class DataType_DT1601 : DataTypeBase
    {

        /// <summary>
        /// 默认是添加在最后
        /// </summary>
        /// <param name="ModuleType">类型</param>
        public override void AddModule<T>(params T[] Module)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ModuleType">Module的类型</param>
        /// <param name="PosBase0">第几个Module</param>
        public override void InsertModule<T>(T Module, int PosBase0)
        {

        }

        /// <summary>
        /// 移除第几个模块
        /// </summary>
        /// <param name="PosBase0"></param>
        public override void RemoveModule<T>(T Module)
        {

        }

        /// <summary>
        /// 修改模块信息
        /// </summary>
        public override void ModifyModule<T>(T Module)
        {

        }

        /// <summary>
        /// 得到当前数据类型中的Module列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public override T[] GetCurrentModuleList<T>()
        {
            List<T> ModileInfoList = new List<T>();
            List<string> ModuleNameList = new List<string>();
            var n =xmlParse.GetSingleElement(null, "EtherCATInfo", "Descriptions", "Devices", "Device", "Profile", "Dictionary", "DataTypes");
            var es = xmlParse.GetMutifyElement(n, "DataType");
            var Dic = new Dictionary<string, string>();
            Dic.Add("Name", "DT1018");
            var v = xmlParse.GetElementFromMutiElement(es, Dic);
            var ss = xmlParse.GetMutifyElement(v, "SubItem");
            foreach (var e in ss)
            {
                //去掉第一个无用项
                if (!xmlParse.GetSubElementValue(e,"SubIdx").Equals("0"))
                {
                    var Name = xmlParse.GetSubElementValue(e, "Name");
                    var list=Name.Split('_');
                    int nLen = list.Length;
                    if (nLen > 2)
                    {
                        ModuleNameList.Add(list[nLen-2]+list[nLen-1]);
                    }
                }
            }
            var L = ModuleNameList.Distinct();
            foreach (var l in L)
            {
                var name= l.Split('_')[0];
                Type type = Type.GetType("NetView.Model.ModuleInfo."+name);
                T obj = type.Assembly.CreateInstance("NetView.Model.ModuleInfo." + name) as T;
                ModileInfoList.Add(obj);
            }
            return ModileInfoList.ToArray();
        }
    }
}
