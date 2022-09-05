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
using System.Collections.Generic;

namespace Synergi.Licensing.Silverlight.UI
{
    internal static class FontHelper
    {
        internal static void SetFontType(UIElement control, FontFamily fontFamily, double? fontSize)
        {
            Queue<UIElement> queue = new Queue<UIElement>();
            queue.Enqueue(control);
            do
            {
                UIElement uiElement = queue.Dequeue();

                if (uiElement is Control)
                {
                    Control ctrl = (Control)uiElement;
                    ctrl.FontFamily = fontFamily ?? ctrl.FontFamily;
                    ctrl.FontSize = fontSize ?? ctrl.FontSize;                    
                }
                else if (uiElement is TextBlock)
                {
                    TextBlock tb = (TextBlock)uiElement;
                    tb.FontFamily = fontFamily ?? tb.FontFamily;
                    tb.FontSize = fontSize ?? tb.FontSize;
                }

                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(uiElement); i++)
                {
                    queue.Enqueue(VisualTreeHelper.GetChild(uiElement, i) as UIElement);
                }
            }
            while (queue.Count > 0);
        }
    }
}
