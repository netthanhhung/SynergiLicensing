using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace Synergi.Licensing.Silverlight.UI
{
    public class StatusToColorConverter : IValueConverter
    {
        static StatusToColorConverter()
        {

        }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string status = (string)value;

            switch (status)
            {
                case "-":
                    return new SolidColorBrush(Colors.Green);
                case "X":
                    return new SolidColorBrush(Colors.Red);
                case "...":
                    return new SolidColorBrush(Color.FromArgb(0xff, 0xd3, 0xe0, 0x58));
                default:
                    return new SolidColorBrush(Color.FromArgb(0xff, 0xaa, 0xaa, 0x00));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
