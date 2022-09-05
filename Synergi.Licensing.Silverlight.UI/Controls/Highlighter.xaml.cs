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
    public partial class Highlight : UserControl
    {
        /*  ======================================================================            
         *      PAGE MEMBERS
         *  ====================================================================== */
        public Brush HighlightBrush
        {
            get { return (Brush)GetValue(HighlightBrushProperty); }
            set { SetValue(HighlightBrushProperty, value); }
        }

        public static readonly DependencyProperty HighlightBrushProperty =
            DependencyProperty.Register("HighlightBrush", typeof(Brush), typeof(Highlight),
            new PropertyMetadata(new SolidColorBrush(Colors.White)));



        /*  ======================================================================            
         *      EVENT HANDLERS
         *  ====================================================================== */
        public Highlight()
        {
            InitializeComponent();

            uiPath.DataContext = this;
        }
    }
}
