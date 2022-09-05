using System;
using System.Net;
using System.Windows;
using System.Windows.Data;
using System.Windows.Automation;

namespace Synergi.Licensing.Silverlight.UI
{
    public class ActivationTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool result = (bool)value;
            return result ? "Deactivate" : "Activate";
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string state = (string)value;
            return state == "Activate" ? false : true;
        }
    }
}
