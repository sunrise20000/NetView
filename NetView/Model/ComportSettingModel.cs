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

        
        public ComportSettingModel()
        {
            PortNames=SerialPort.GetPortNames();
        }
        [Browsable(false)]
        public string[] PortNames { get; private set; }

        [TypeConverter(typeof(MyNameConverter))]
        public string ComportName { get; set; } = "UnDefined";

        public int Baudrate { get; set; }

        public byte Data { get; set; }

        public Parity Parity { get; set; }

        public StopBits Stop { get; set; }


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
    }
}
