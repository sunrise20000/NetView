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
    public class MsgType2Image : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            EnumMsgType msg = (EnumMsgType)value;
            BitmapImage bitmap = null;
            var basePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            switch (msg)
            {
                case EnumMsgType.Info:
                    bitmap = new BitmapImage(new Uri(basePath+@"images\infomation.png", UriKind.Absolute));
                    break;
                case EnumMsgType.Warning:
                    bitmap = new BitmapImage(new Uri(basePath+@"images\warning.png", UriKind.Absolute));
                    break;
                case EnumMsgType.Error:
                    bitmap = new BitmapImage(new Uri(basePath+@"images\error.png", UriKind.Absolute));
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
