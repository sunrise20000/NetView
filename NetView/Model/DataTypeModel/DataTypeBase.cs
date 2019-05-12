using NetView.Class;
using NetView.Model.ModuleInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NetView.Model.DataTypeModel
{
    public enum EnumDeviceName
    {
        HL1001,
        HL2001,
        HL2002,
        HL2003,
        HL3001,
        HL3002,
        HL4001,
        HL4002,
    }

    public class DataTypeBase
    {
        EnumDeviceName deviceType = EnumDeviceName.HL1001;
        protected readonly static XmlParse xmlParse = new XmlParse();
        public DataTypeBase()
        {
         
        }

        public string DeviceType
        {
            get { return deviceType.ToString(); }
            set { Enum.TryParse(value, out deviceType); }

        }
        public int BitSize { get; set; } = 16;

        public int SubItemNum { get; set; }

        protected List<SubItemModelBaseForDataType> SubItemList { get; private set; } = new List<SubItemModelBaseForDataType>();

        /// <summary>
        /// 默认是添加在最后
        /// </summary>
        /// <param name="ModuleType">类型</param>
        public virtual void AddModule<T>(params T[] Module) where T : ModuleInfoBase
        {

        }

        /// <summary>
        /// 插入模块
        /// </summary>
        /// <param name="ModuleType">Module的类型</param>
        /// <param name="PosBase0">第几个Module</param>
        public virtual void InsertModule<T>(T Module, int PosBase0) where T : ModuleInfoBase
        {

        }

        /// <summary>
        /// 移除第几个模块
        /// </summary>
        /// <param name="PosBase0"></param>
        public virtual void RemoveModule<T>(T Module) where T : ModuleInfoBase
        {

        }

        /// <summary>
        /// 修改模块信息
        /// </summary>
        public virtual void ModifyModule<T>(T Module) where T : ModuleInfoBase
        {

        }

        public virtual T[] GetCurrentModuleList<T>() where T : ModuleInfoBase
        {
            
            return null;
        }
    }
}
