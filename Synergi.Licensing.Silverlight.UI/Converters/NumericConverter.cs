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
using System.Globalization;
using System.Windows.Data;
using Synergi.Licensing.Common;

namespace Synergi.Licensing.Silverlight.UI
{
    public class NumericConverter : IValueConverter
    {
        private const string _div100 = "/100";
        private const string _mul100 = "*100";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }
            var input = (Decimal)value;
            string format = Utilities.ToString(parameter);
            if (format[0] == '-')
            {
                input *= -1M;
                format = format.Substring(1);
            }
            if (format.IndexOf(_div100) != -1)
            {
                input /= 100;
                format = format.Replace(_div100, string.Empty);
            }
            if (format.IndexOf(_mul100) != -1)
            {
                input *= 100;
                format = format.Replace(_mul100, string.Empty);
            }

            return input.ToString(format, CultureInfo.CurrentCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


