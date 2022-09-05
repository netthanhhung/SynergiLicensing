using System;
using System.Net;
using System.Windows;
using System.Windows.Data;
using System.Windows.Automation;

namespace Synergi.Licensing.Silverlight.UI
{
    public class TagToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool result = System.Convert.ToBoolean(value);
            return result ? Visibility.Visible : Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility state = (Visibility)value;
            return state == Visibility.Visible ? true : false;
        }
    }
}
