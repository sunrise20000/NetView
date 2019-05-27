using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlTest
{
    public partial class SubBusModel : ControlBase
    {
        #region 字段定义
        enumSubBusModelType modelType;
        short m_type;//better to change to enum type
        short m_ch1InputOrOutputType;
        short m_ch2InputOrOutputType;
        short m_ch3InputOrOutputType;
        short m_ch4InputOrOutputType;
        short m_ch1Accuracy;
        short m_ch2Accuracy;
        short m_ch3Accuracy;
        short m_ch4Accuracy;
        
        short m_counterLimitH;
        short m_counterLimitL;
        short m_resPara1;
        short m_resPara2;
        short m_resPara3;
        short m_resPara4;
        short m_resPara5;
        short m_resPara6;

        short m_resolution;
        short m_revolution;
        int m_presetValue;

        Dictionary<enumSubBusModelType, string> DictionaryFuncion = new Dictionary<enumSubBusModelType, string>();
        #endregion
        #region 属性定义
        public short Type
        {
            get { return m_type; }
            set { m_type = value; }
        }
        public short Ch1InoutOrOutputType
        {
            get { return m_ch1InputOrOutputType; }
            set { m_ch1InputOrOutputType = value; }
        }
        public short Ch2InoutOrOutputType
        {
            get { return m_ch2InputOrOutputType; }
            set { m_ch2InputOrOutputType = value; }
        }
        public short Ch3InoutOrOutputType
        {
            get { return m_ch3InputOrOutputType; }
            set { m_ch3InputOrOutputType = value; }
        }
        public short Ch4InoutOrOutputType
        {
            get { return m_ch4InputOrOutputType; }
            set { m_ch4InputOrOutputType = value; }
        }
       public short Ch1Accuracy
        {
            get { return m_ch1Accuracy; }
            set { m_ch1Accuracy = value; }
        }
        public short Ch2Accuracy
        {
            get { return m_ch2Accuracy; }
            set { m_ch2Accuracy = value; }
        }
        public short Ch3Accuracy
        {
            get { return m_ch3Accuracy; }
            set { m_ch3Accuracy = value; }
        }
        public short Ch4Accuracy
        {
            get { return m_ch4Accuracy; }
            set { m_ch4Accuracy = value; }
        }
        public short CounterLimitH
        {
            get { return m_counterLimitH; }
            set
            {
                m_counterLimitH = value;
            }
        }
        public short CounterLimitL
        {
            get { return m_counterLimitL; }
            set
            {
                m_counterLimitL = value;
            }
        }
        public short ResPara1
        {
            get { return m_resPara1; }
            set { m_resPara1 = value; }
        }

        public short ResPara2
        {
            get { return m_resPara2; }
            set { m_resPara2 = value; }
        }
        public short ResPara3
        {
            get { return m_resPara3; }
            set { m_resPara3 = value; }
        }
        public short ResPara4
        {
            get { return m_resPara4; }
            set { m_resPara4 = value; }
        }
        public short ResPara5
        {
            get { return m_resPara5; }
            set { m_resPara5 = value; }
        }
        public short ResPara6
        {
            get { return m_resPara6; }
            set { m_resPara6 = value; }
        }

        public short Resolution
        {
            get { return m_resolution; }
            set { m_resolution = value; }
        }
        public short Revolution
        {
            get { return m_revolution; }
            set { m_revolution = value; }
        }

        public int PresetVaule
        {
            get { return m_presetValue; }
            set { m_presetValue = value; }
        }
        #endregion
        public SubBusModel(Point location, enumSubBusModelType modelType) :base(location)
        {
            InitializeComponent();
            this.modelType = modelType;
            Init();
        }
        private void Init()
        {
            switch (modelType)
            {
                case enumSubBusModelType.HL1001:
                    m_function = 0x11;
                    break;
                case enumSubBusModelType.HL2001:
                    m_function = 0x21;
                    break;
                case enumSubBusModelType.HL2002:
                    m_function = 0x22;
                    break;
                case enumSubBusModelType.HL2003:
                    m_function = 0x31;
                    break;
                case enumSubBusModelType.HL3001:
                    m_function = 0x32;
                    break;
                case enumSubBusModelType.HL3002:
                    m_function = 0x33;
                    break;
                case enumSubBusModelType.HL4001:
                    m_function = 0x41;
                    break;
                case enumSubBusModelType.HL4002:
                    m_function = 0x42;
                    break;
                case enumSubBusModelType.HL5001:
                    m_function = 0x51;
                    break;
                case enumSubBusModelType.HL5002:
                    m_function = 0x52;
                    break;
                default:
                    break;
            }
            }
        public string ReturnPropertyInfo()
        {
            string rtn=null;
            string func;
            string index;
            string sequence;
            string type;
            string ch1iotype;
            string ch2iotype;
            string ch3iotype;
            string ch4iotype;
            string ch1accuracy;
            string ch2accuracy;
            string ch3accuracy;
            string ch4accuracy;

            string counterlimith;
            string counterlimitl;
            string respara1;
            string respara2;
            string respara3;
            string respara4;
            string respara5;
            string respara6;
            string resolution;
            string revolution;
            string presetvaule;
            func = IntToHexString(m_function) + " ";
            index = "0x" + int.Parse(m_name.Split('_').ElementAt(1)).ToString("X") + " ";
            sequence = IntToHexString(m_sequence) + " ";
            switch (modelType)
            {
                case enumSubBusModelType.HL1001:
                    
                     type ="0x"+ CommonFunc.ConvertToType(m_type).ToString("X") + " ";
                    rtn = func + index + sequence + type; 
                    break;
                case enumSubBusModelType.HL2001:
                   
                    rtn = func + index + sequence;
                    break;
                case enumSubBusModelType.HL2002:
                   
                    rtn = func + index + sequence;
                    break;
                case enumSubBusModelType.HL2003:
                   
                    rtn = func + index + sequence;
                    break;
                case enumSubBusModelType.HL3001:
                    ch1iotype = IntToHexString((int)m_ch1InputOrOutputType) + " ";
                    ch2iotype = IntToHexString((int)m_ch2InputOrOutputType) + " ";
                    ch3iotype = IntToHexString((int)m_ch3InputOrOutputType) + " ";
                    ch4iotype = IntToHexString((int)m_ch4InputOrOutputType) + " ";

                    ch1accuracy = IntToHexString((int)m_ch1Accuracy) + " ";
                    ch2accuracy = IntToHexString((int)m_ch2Accuracy) + " ";
                    ch3accuracy = IntToHexString((int)m_ch3Accuracy) + " ";
                    ch4accuracy = IntToHexString((int)m_ch4Accuracy) + " ";
                    rtn = func + index + sequence + ch1iotype + ch1accuracy + ch2iotype + ch2accuracy + ch3iotype + ch3accuracy + ch4iotype + ch4accuracy;
                    break;
                case enumSubBusModelType.HL3002:
                    ch1iotype = IntToHexString((int)m_ch1InputOrOutputType) + " ";
                    ch2iotype = IntToHexString((int)m_ch2InputOrOutputType) + " ";
                    ch3iotype = IntToHexString((int)m_ch3InputOrOutputType) + " ";
                    ch4iotype = IntToHexString((int)m_ch4InputOrOutputType) + " ";

                    ch1accuracy = IntToHexString((int)m_ch1Accuracy) + " ";
                    ch2accuracy = IntToHexString((int)m_ch2Accuracy) + " ";
                    ch3accuracy = IntToHexString((int)m_ch3Accuracy) + " ";
                    ch4accuracy = IntToHexString((int)m_ch4Accuracy) + " ";
                    rtn = func + index + sequence + ch1iotype + ch1accuracy + ch2iotype + ch2accuracy + ch3iotype + ch3accuracy + ch4iotype + ch4accuracy;
                    break;
                case enumSubBusModelType.HL4001:
                    ch1iotype = IntToHexString((int)m_ch1InputOrOutputType) + " ";
                    ch2iotype = IntToHexString((int)m_ch2InputOrOutputType) + " ";
                    ch3iotype = IntToHexString((int)m_ch3InputOrOutputType) + " ";
                    ch4iotype = IntToHexString((int)m_ch4InputOrOutputType) + " ";

                    ch1accuracy = CommonFunc.ConvertToAccurary((int)m_ch1Accuracy) + " ";
                    ch2accuracy = CommonFunc.ConvertToAccurary((int)m_ch2Accuracy) + " ";
                    ch3accuracy = CommonFunc.ConvertToAccurary((int)m_ch3Accuracy) + " ";
                    ch4accuracy = CommonFunc.ConvertToAccurary((int)m_ch4Accuracy) + " ";
                    rtn = func + index + sequence + ch1iotype + ch1accuracy + ch2iotype + ch2accuracy + ch3iotype + ch3accuracy + ch4iotype + ch4accuracy;
                    break;
                case enumSubBusModelType.HL4002:
                    ch1iotype = IntToHexString((int)m_ch1InputOrOutputType) + " ";
                    ch2iotype = IntToHexString((int)m_ch2InputOrOutputType) + " ";
                    ch3iotype = IntToHexString((int)m_ch3InputOrOutputType) + " ";
                    ch4iotype = IntToHexString((int)m_ch4InputOrOutputType) + " ";

                    ch1accuracy = CommonFunc.ConvertToAccurary((int)m_ch1Accuracy) + " ";
                    ch2accuracy = CommonFunc.ConvertToAccurary((int)m_ch2Accuracy) + " ";
                    ch3accuracy = CommonFunc.ConvertToAccurary((int)m_ch3Accuracy) + " ";
                    ch4accuracy = CommonFunc.ConvertToAccurary((int)m_ch4Accuracy) + " ";
                    rtn = func + index + sequence + ch1iotype + ch1accuracy + ch2iotype + ch2accuracy + ch3iotype + ch3accuracy + ch4iotype + ch4accuracy;
                    break;
                case enumSubBusModelType.HL5001:
                    counterlimith = IntToHexString(m_counterLimitH) + " ";
                    counterlimitl = IntToHexString(m_counterLimitL) + " ";

                    respara1 = IntToHexString(m_resPara1) + " ";
                    respara2 = IntToHexString(m_resPara2) + " ";
                    respara3 = IntToHexString(m_resPara3) + " ";
                    respara4 = IntToHexString(m_resPara4) + " ";
                    respara5 = IntToHexString(m_resPara5) + " ";
                    respara6 = IntToHexString(m_resPara6) + " ";
                    rtn = func + index + sequence + counterlimith + counterlimitl + respara1 + respara2 + respara3 + respara4 + respara5 + respara6;
                    break;
                case enumSubBusModelType.HL5002:
                    resolution = IntToHexString(CommonFunc.ConvertToResolution(m_resolution)) + " ";
                    revolution = IntToHexString(CommonFunc.ConvertToRevolution(m_revolution)) + " ";
                    presetvaule = IntToHexString(m_presetValue) + " ";
                    respara1 = IntToHexString(m_resPara1) + " ";
                    respara2 = IntToHexString(m_resPara2) + " ";
                    respara3 = IntToHexString(m_resPara3) + " ";
                    respara4 = IntToHexString(m_resPara4) + " ";
                    respara5 = IntToHexString(m_resPara5) + " ";
                    rtn = func + index + sequence + resolution + revolution+ presetvaule + respara1 + respara2 + respara3 + respara4 + respara5;

                    break;
            }
                    return rtn;
        }
        public void SetPropertyInfo(string[] infos)
        {
            if (infos.Length < 3)
            {
                throw new Exception("SetPropertyInfo参数长度异常");
            }
            m_sequence = HexStringToInt(infos[2]);
            switch (modelType)
            {
                case enumSubBusModelType.HL1001:
                    if (infos.Length != 4)
                    {
                        throw new Exception("SetPropertyInfo参数长度异常");
                    }
                    m_type= (short)HexStringToInt(infos[3]);
                    break;
                case enumSubBusModelType.HL2001:
                   
                    break;
                case enumSubBusModelType.HL2002:
                   
                    break;
                case enumSubBusModelType.HL2003:
                   
                    break;
                case enumSubBusModelType.HL3001:
                    if (infos.Length< 10)
                    {
                        throw new Exception("SetPropertyInfo参数长度异常");
                    }
                    m_ch1InputOrOutputType = (short)HexStringToInt(infos[3]);
                    m_ch1Accuracy = (short)HexStringToInt(infos[4]);
                    m_ch1InputOrOutputType = (short)HexStringToInt(infos[5]);
                    m_ch1Accuracy = (short)HexStringToInt(infos[6]);
                    m_ch1InputOrOutputType = (short)HexStringToInt(infos[7]);
                    m_ch1Accuracy = (short)HexStringToInt(infos[8]);
                    m_ch1InputOrOutputType = (short)HexStringToInt(infos[9]);
                    m_ch1Accuracy = (short)HexStringToInt(infos[10]);
                    break;
                case enumSubBusModelType.HL3002:
                    if (infos.Length < 10)
                    {
                        throw new Exception("SetPropertyInfo参数长度异常");
                    }
                    m_ch1InputOrOutputType = (short)HexStringToInt(infos[3]);
                    m_ch1Accuracy = (short)HexStringToInt(infos[4]);
                    m_ch1InputOrOutputType = (short)HexStringToInt(infos[5]);
                    m_ch1Accuracy = (short)HexStringToInt(infos[6]);
                    m_ch1InputOrOutputType = (short)HexStringToInt(infos[7]);
                    m_ch1Accuracy = (short)HexStringToInt(infos[8]);
                    m_ch1InputOrOutputType = (short)HexStringToInt(infos[9]);
                    m_ch1Accuracy = (short)HexStringToInt(infos[10]);
                    break;
                case enumSubBusModelType.HL4001:
                    if (infos.Length < 10)
                    {
                        throw new Exception("SetPropertyInfo参数长度异常");
                    }
                    m_ch1InputOrOutputType = (short)HexStringToInt(infos[3]);
                    m_ch1Accuracy = (short)HexStringToInt(infos[4]);
                    m_ch1InputOrOutputType = (short)HexStringToInt(infos[5]);
                    m_ch1Accuracy = (short)HexStringToInt(infos[6]);
                    m_ch1InputOrOutputType = (short)HexStringToInt(infos[7]);
                    m_ch1Accuracy = (short)HexStringToInt(infos[8]);
                    m_ch1InputOrOutputType = (short)HexStringToInt(infos[9]);
                    m_ch1Accuracy = (short)HexStringToInt(infos[10]);
                    break;
                case enumSubBusModelType.HL4002:
                    if (infos.Length < 10)
                    {
                        throw new Exception("SetPropertyInfo参数长度异常");
                    }
                    m_ch1InputOrOutputType = (short)HexStringToInt(infos[3]);
                    m_ch1Accuracy = (short)HexStringToInt(infos[4]);
                    m_ch2InputOrOutputType = (short)HexStringToInt(infos[5]);
                    m_ch2Accuracy = (short)HexStringToInt(infos[6]);
                    m_ch3InputOrOutputType = (short)HexStringToInt(infos[7]);
                    m_ch3Accuracy = (short)HexStringToInt(infos[8]);
                    m_ch4InputOrOutputType = (short)HexStringToInt(infos[9]);
                    m_ch4Accuracy = (short)HexStringToInt(infos[10]);
                    break;
                case enumSubBusModelType.HL5001:
                    m_counterLimitH = (short)HexStringToInt(infos[3]);
                    m_counterLimitL = (short)HexStringToInt(infos[4]);
                    m_resPara1 = (short)HexStringToInt(infos[5]);
                    m_resPara2 = (short)HexStringToInt(infos[6]);
                    m_resPara3 = (short)HexStringToInt(infos[7]);
                    m_resPara4 = (short)HexStringToInt(infos[8]);
                    m_resPara5 = (short)HexStringToInt(infos[9]);
                    m_resPara6 = (short)HexStringToInt(infos[10]);
                    break;
                case enumSubBusModelType.HL5002:
                    m_resolution =(short) CommonFunc.ResolutionToInt(infos[3]);
                    m_revolution= (short)CommonFunc.ResolutionToInt(infos[4]);
                    m_presetValue = HexStringToInt(infos[5]);
                    m_resPara1 = (short)HexStringToInt(infos[6]);
                    m_resPara2 = (short)HexStringToInt(infos[7]);
                    m_resPara3 = (short)HexStringToInt(infos[8]);
                    m_resPara4 = (short)HexStringToInt(infos[9]);
                    m_resPara5 = (short)HexStringToInt(infos[10]);
                    break;
                default:
                    break;
            }
        }
        private string IntToHexString(int n)
        {
            return "0x" + n.ToString("X");
        }
        private int HexStringToInt(string data)
        {
            int rtn=0;
            if (data.StartsWith("0x"))
            {
                data.Trim();
              string s=  data.Substring(2, data.Length-2);
              rtn = Convert.ToInt32(s, 16);
            }
            else
                throw new Exception("数据格式异常");
            return rtn;
        }
        public override void ShowProperty()
        {
            Form form = new Form();
            switch(modelType)
            {
                case enumSubBusModelType.HL1001:
                    form = new HL1001(this);
                    break;
                case enumSubBusModelType.HL2001:
                    form = new HL2001(this);
                    break;
                case enumSubBusModelType.HL2002:
                    form = new HL2002(this);
                    break;
                case enumSubBusModelType.HL2003:
                    form = new HL2003(this);
                    break;
                case enumSubBusModelType.HL3001:
                    form = new HL3001(this);
                    break;
                case enumSubBusModelType.HL3002:
                    form = new HL3002(this);
                    break;
                case enumSubBusModelType.HL4001:
                    form = new HL4001(this);
                    break;
                case enumSubBusModelType.HL4002:
                    form = new HL4002(this);
                    break;
                case enumSubBusModelType.HL5001:
                    form = new HL5001(this);
                    break;
                case enumSubBusModelType.HL5002:
                    form = new HL5002(this);
                    break;
                default:
                    break;
            }
            form.ShowDialog();
        }
    }
}
