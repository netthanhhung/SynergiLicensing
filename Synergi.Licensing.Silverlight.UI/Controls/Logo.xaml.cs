using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Synergi.Licensing.Common;

namespace Synergi.Licensing.Silverlight.UI
{
    public partial class Logo : UserControl
    {
        public Logo()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Logo_Loaded);
        }

        void Logo_Loaded(object sender, RoutedEventArgs e)
        {
            uiDeploymentInfo.Text = DeploymentInfo.ToString();
        }
    }
}
