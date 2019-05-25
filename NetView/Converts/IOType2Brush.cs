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
    class IOType2Brush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            EnumModuleIOType msg = (EnumModuleIOType)value;
            switch (msg)
            {
                case EnumModuleIOType.IN:
                    return new SolidColorBrush(Color.FromRgb(128, 150, 128));
                case EnumModuleIOType.OUT:
                    return new SolidColorBrush(Color.FromRgb(255, 255, 255));

                default:
                    break;
            }
            return new SolidColorBrush(Color.FromRgb(128, 150, 128));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
