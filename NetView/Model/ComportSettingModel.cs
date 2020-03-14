using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.ComponentModel;

namespace NetView.Model
{
    public class ComportSettingModel
    {
        [Browsable(false)]
        public int[] BaudrateList { get { return new int[] { 9600, 19200, 38400, 57600, 115200, 250000, 500000, 1000000 }; } }

		[Category("Comport Setting")]
		public ComportSettingModel()
        {
            PortNames=SerialPort.GetPortNames();
        }

		[Browsable(false)]
        public string[] PortNames { get; private set; }

		[Category("Comport Setting")]
		[TypeConverter(typeof(MyNameConverter))]
        public string ComportName { get; set; } = "UnDefined";


		[Category("Comport Setting")]
		[TypeConverter(typeof(MyIntConverter))]
        public int Baudrate { get; set; } = 115200;

		[Category("Comport Setting")]
		public byte Data { get; set; } = 8;

		[Category("Comport Setting")]
		public Parity Parity { get; set; }

		[Category("Comport Setting")]
		public StopBits Stop { get; set; }

		[Category("Communication Setting")]
		[Description("uinit is ms")]
		public uint TransmitDelay { get; set; } = 500;

		[Category("Communication Setting")]
		[Description("uinit is ms")]
		public uint ReceiveTimeout { get; set; } = 1000;


		public void RefreshComport()
        {
            PortNames = SerialPort.GetPortNames();
        }


        private class MyNameConverter : StringConverter
        {
            public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                if (context != null && context.Instance is ComportSettingModel)
                {
                    List<string> values = new System.Collections.Generic.List<string>();

                    foreach (var it in (context.Instance as ComportSettingModel).PortNames)
                        values.Add(it);
                    var c= new StandardValuesCollection(values);

                    return c;

                }
                return base.GetStandardValues(context);
            }

            //true: disable text editting.    false: enable text editting;
            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return true;
            }

            //content of drop-down 
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
        }

        private class MyIntConverter : Int32Converter
        {
            public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                if (context != null && context.Instance is ComportSettingModel)
                {
                    List<int> values = new System.Collections.Generic.List<int>();

                    foreach (var it in (context.Instance as ComportSettingModel).BaudrateList)
                        values.Add(it);
                    var c = new StandardValuesCollection(values);

                    return c;

                }
                return base.GetStandardValues(context);
            }

            //true: disable text editting.    false: enable text editting;
            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return true;
            }

            //content of drop-down 
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
        }
    }
}
