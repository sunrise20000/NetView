using NetView.Definations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace NetView.Converts
{
    public class IOType2Image : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            EnumModuleIOType type = (EnumModuleIOType)value;
            BitmapImage bitmap = null;
            var basePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            switch (type)
            {
                case EnumModuleIOType.IN:
                    bitmap = new BitmapImage(new Uri(basePath+@"images\in.png", UriKind.Absolute));
                    break;
                case EnumModuleIOType.OUT:
                    bitmap = new BitmapImage(new Uri(basePath+@"images\out.png", UriKind.Absolute));
                    break;
                
                default:
                    break;
            }
            return bitmap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
