using System;
using System.Net;
using System.Windows;
using System.Windows.Data;
using System.Windows.Automation;

namespace Synergi.Licensing.Silverlight.UI
{
    public class ErrorToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string result = System.Convert.ToString(value);
            return string.IsNullOrEmpty(result) ? Visibility.Collapsed : Visibility.Visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
