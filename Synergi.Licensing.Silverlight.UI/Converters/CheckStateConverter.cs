using System;
using System.Net;
using System.Windows;
using System.Windows.Data;
using System.Windows.Automation;

namespace Synergi.Licensing.Silverlight.UI
{
    public class CheckStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool result = (bool)value;
            return result ? ToggleState.On : ToggleState.Off;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ToggleState state = (ToggleState)value;
            return state == ToggleState.On ? true : false;
        }
    }
}
