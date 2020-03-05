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
    public class Status2Image : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool IsMonitor = bool.Parse(value.ToString());
            BitmapImage bitmap = null;
            var basePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
			string imagePath = "";
            switch (parameter.ToString().ToLower())
            {
                case "stop":
					imagePath = IsMonitor ? basePath + @"images\stop.png" : basePath + @"images\stopdis.png";
					bitmap = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
                    break;
                case "start":
					imagePath = IsMonitor ? basePath + @"images\startdis.png" : basePath + @"images\start.png";
					bitmap = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
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
