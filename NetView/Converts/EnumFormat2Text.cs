using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using NetView.Definations;
namespace NetView.Converts
{
    class EnumFormat2Text : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((EnumDisplayFormat)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Enum.TryParse(value.ToString(), out EnumDisplayFormat format))
            {
                return format;
            }
            throw new Exception("Can't parse string to EnumDisplayFormat");
        }
    }
}
