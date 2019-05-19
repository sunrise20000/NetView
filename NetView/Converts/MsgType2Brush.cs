using NetView.Definations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace NetView.Converts
{
    class MsgType2Brush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            EnumMsgType msg = (EnumMsgType)value;
            SolidColorBrush brush = null;
            switch (msg)
            {
                case EnumMsgType.Info:
                    brush = new SolidColorBrush(Color.FromRgb(18, 150, 219));
                    break;
                case EnumMsgType.Warning:
                    brush = new SolidColorBrush(Color.FromRgb(243,153,43));
                    break;
                case EnumMsgType.Error:
                    brush = new SolidColorBrush(Color.FromRgb(208, 2, 27));
                    break;
                default:
                    break;
            }
            return brush; ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
