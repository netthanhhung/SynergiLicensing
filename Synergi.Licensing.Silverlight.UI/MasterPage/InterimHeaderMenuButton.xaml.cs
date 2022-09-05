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
    public partial class InterimHeaderMenuButton : UserControl
    {
        internal event RoutedEventHandler Click;

        public Brush HighlightBrush
        {
            get { return (Brush)GetValue(HighlightBrushProperty); }
            set { SetValue(HighlightBrushProperty, value); }
        }

        public static readonly DependencyProperty HighlightBrushProperty =
            DependencyProperty.Register("HighlightBrush", typeof(Brush), typeof(InterimHeaderMenuButton),
            new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public Visibility HighlightVisibility
        {
            get { return (Visibility)GetValue(HighlightVisibilityProperty); }
            set { SetValue(HighlightVisibilityProperty, value); }
        }

        public static readonly DependencyProperty HighlightVisibilityProperty =
            DependencyProperty.Register("HighlightVisibility", typeof(Visibility), typeof(InterimHeaderMenuButton),
            new PropertyMetadata(Visibility.Visible));


        public string ModuleName
        {
            get { return (string)GetValue(ModuleNameProperty); }
            set { SetValue(ModuleNameProperty, value); }
        }

        public static readonly DependencyProperty ModuleNameProperty =
            DependencyProperty.Register("ModuleName", typeof(string), typeof(InterimHeaderMenuButton), 
            new PropertyMetadata(string.Empty));

        public string ModuleEntryPoint
        {
            get { return (string)GetValue(ModuleEntryPointProperty); }
            set { SetValue(ModuleEntryPointProperty, value); }
        }

        public static readonly DependencyProperty ModuleEntryPointProperty =
            DependencyProperty.Register("ModuleEntryPoint", typeof(string), typeof(InterimHeaderMenuButton),
            new PropertyMetadata(string.Empty));


        public InterimHeaderMenuButton()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(InterimHeaderMenuButton_Loaded);
            uiContent.Click += new RoutedEventHandler(delegate(object sender, RoutedEventArgs e) { UriHelper.NavigateTo(ModuleEntryPoint, null); });            
        }

        void InterimHeaderMenuButton_Loaded(object sender, RoutedEventArgs e)
        {
            uiContent.DataContext = this;
            FontHelper.SetFontType(uiContent, null, 16);
        }
    }
}
