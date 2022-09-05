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

namespace Synergi.Licensing.Silverlight.UI
{
    public class SingleClickButton : Button
    {
        internal long _lastTicks = 0;

        // Disable the second click if too soon. 
        protected override void OnClick()
        {
            // Standard Win double click is .5 seconds but this doubles it
            // to allow popups to close etc before another click can register.
            if ((DateTime.Now.Ticks - _lastTicks) > 10000000)
            {
                _lastTicks = DateTime.Now.Ticks;
                base.OnClick();
            }
        }
    }
}
