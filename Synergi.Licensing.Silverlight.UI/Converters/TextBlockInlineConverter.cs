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
using System.Globalization;
using System.Collections.Generic;

namespace Synergi.Licensing.Silverlight.UI
{
    public class TextBlockInlineConverter : IValueConverter
    {
        private static class Constants
        {
            public static string StressColorKey = "StressCharacterColour";
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = (Color) App.Current.Resources[Constants.StressColorKey];
            var inlines = new List<Inline>();
            if (value != null)
            {
                string text = value.ToString(); // as format 'abc[c]def'
                int startPos = text.IndexOf("[");
                int endPos = text.IndexOf("]");

                if (startPos >= 0 && endPos > 0 && endPos > startPos)
                {
                    string part = text.Substring(0, startPos);
                    if (!string.IsNullOrEmpty(part))
                    {
                        inlines.Add(new Run() { Text = part });
                    }

                    part = text.Substring(startPos + 1, endPos - startPos - 1);
                    if (!string.IsNullOrEmpty(part))
                    {
                        inlines.Add(new Run() { Text = part, Foreground = new SolidColorBrush(color) });
                    }

                    if (endPos + 1 < text.Length && text.Length - 1 - endPos >= 0)
                    {
                        part = text.Substring(endPos + 1, text.Length - 1 - endPos);
                        if (!string.IsNullOrEmpty(part))
                        {
                            inlines.Add(new Run() { Text = part });
                        }
                    }
                }
                else
                {
                    inlines.Add(new Run() { Text = text });
                }
            }
            return inlines;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
