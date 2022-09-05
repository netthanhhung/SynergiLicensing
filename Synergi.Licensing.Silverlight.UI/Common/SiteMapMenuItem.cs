using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using Telerik.Windows.Controls;

namespace Synergi.Licensing.Silverlight.UI
{
    public class SiteMapMenuItem
    {
        public string Url { get; set; }
        public string Target { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public IEnumerable<SiteMapMenuItem> Items { get; set; }
    }
}
