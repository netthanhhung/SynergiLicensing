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

namespace Synergi.Licensing.Silverlight.UI
{
    public partial class Information : UserControl
    {
        public string InfoMessage
        {
            get { return textInfo.Text; }
            set { textInfo.Text = value; }
        }

        public Information()
        {
            InitializeComponent();
        }
    }
}
