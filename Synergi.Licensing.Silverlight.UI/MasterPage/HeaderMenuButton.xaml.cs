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
using System.Xml.Linq;
using Telerik.Windows.Controls;
using nsTooltips = Silverlight.Controls.ToolTips;

namespace Synergi.Licensing.Silverlight.UI
{
    public partial class HeaderMenuButton : UserControl
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
            DependencyProperty.Register("HighlightBrush", typeof(Brush), typeof(HeaderMenuButton),
            new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public Visibility HighlightVisibility
        {
            get { return (Visibility)GetValue(HighlightVisibilityProperty); }
            set { SetValue(HighlightVisibilityProperty, value); }
        }

        public static readonly DependencyProperty HighlightVisibilityProperty =
            DependencyProperty.Register("HighlightVisibility", typeof(Visibility), typeof(HeaderMenuButton),
            new PropertyMetadata(Visibility.Visible));


        public string ModuleName
        {
            get { return InnerText.Replace("[", "").Replace("]", ""); }
        }

        public string ModuleEntryPoint
        {
            get { return (string)GetValue(ModuleEntryPointProperty); }
            set { SetValue(ModuleEntryPointProperty, value); }
        }

        public static readonly DependencyProperty ModuleEntryPointProperty =
            DependencyProperty.Register("ModuleEntryPoint", typeof(string), typeof(HeaderMenuButton),
            new PropertyMetadata(string.Empty));

        public string InnerText
        {
            get { return (string)GetValue(InnerTextProperty); }
            set { SetValue(InnerTextProperty, value); }
        }

        public static readonly DependencyProperty InnerTextProperty =
            DependencyProperty.Register("InnerText", typeof(string), typeof(HeaderMenuButton),
            new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ExtendedTextProperty =
            DependencyProperty.RegisterAttached(
            "InlineList",
            typeof(List<Inline>),
            typeof(HeaderMenuButton),
            new PropertyMetadata(null, OnInlineListPropertyChanged));

        public static void SetExtendedText(DependencyObject obj, List<Inline> extendedText)
        {
            obj.SetValue(ContentProperty, extendedText);
        }

        public static List<Inline> GetExtendedText(DependencyObject obj)
        {
            return (List<Inline>)obj.GetValue(ExtendedTextProperty);
        }


        private static void OnInlineListPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var tb = obj as TextBlock;
            if (tb != null)
            {
                // clear previous inlines
                tb.Inlines.Clear();
                // add new inlines     
                var inlines = e.NewValue as List<Inline>;
                if (inlines != null)
                {
                    inlines.ForEach(inl => tb.Inlines.Add((inl)));
                }
            }
        }

        /*  ======================================================================            
         *      EVENT HANDLERS
         *  ====================================================================== */
        public HeaderMenuButton()
        {
            InitializeComponent();

            //uiContent.Click += new RoutedEventHandler(delegate(object sender, RoutedEventArgs e) { UriHelper.NavigateTo(ModuleEntryPoint); });

            this.Loaded += new RoutedEventHandler(HeaderMenuButton_Loaded);
            //uiContextMenu.EventName = "MouseMove";
            //uiContextMenu.MouseLeave += new MouseEventHandler(uiContextMenu_MouseLeave);
            //this.MouseMove += new MouseEventHandler(HeaderMenuButton_MouseMove);
        }

        void HeaderMenuButton_MouseMove(object sender, MouseEventArgs e)
        {
            uiContextMenu.IsOpen = true;
        }


        void uiContextMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            uiContextMenu.IsOpen = false;
        }

        void HeaderMenuButton_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.ModuleName == "accorLogo")
            {
                uiContent.Style = Application.Current.Resources["uiLogoButton"] as Style;
            }

            uiContent.DataContext = this;
            FontHelper.SetFontType(uiContent, null, 16);
            SiteMapHelper.SelectWeb1SiteMapAsync(LoadMenuItems);
        }

        private void LoadMenuItems(XDocument xdocument)
        {
            IEnumerable<SiteMapMenuItem> itemsSource =
                from menuitem in xdocument.Root.Elements(SiteMapHelper.XNameSiteMapNode)
                select ChildMenuItem(menuitem);
            foreach (SiteMapMenuItem parentItem in itemsSource)
            {
                if (parentItem.Title.ToLower() == this.ModuleName.ToLower())
                {
                    uiContextMenu.ItemsSource = parentItem.Items;
                    break;
                }
            }

        }

		//private SiteMapMenuItem ChildMenuItem(XElement siteMapNode)
		//{
		//    SiteMapMenuItem result =
		//        new SiteMapMenuItem()
		//        {
		//            Title = (string)siteMapNode.Attribute("title"),
		//            Description = (string)siteMapNode.Attribute("description"),
		//            Url = (string)siteMapNode.Attribute("url"),
		//            Target = (string)siteMapNode.Attribute("target"),
		//            IsEnabled = (string.IsNullOrEmpty((string)siteMapNode.Attribute("disabled"))) && CheckRoleAccess((string)siteMapNode.Attribute("roles")),
		//            Items =
		//                from childNode in siteMapNode.Elements(SiteMapHelper.XNameSiteMapNode)
		//                select ChildMenuItem(childNode)
		//        };
		//    return result;
		//}

		private SiteMapMenuItem ChildMenuItem(XElement siteMapNode)
		{
            bool isEnabled = (string.IsNullOrEmpty((string)siteMapNode.Attribute("disabled")));

			SiteMapMenuItem result =
				new SiteMapMenuItem()
				{
					Title = (string)siteMapNode.Attribute("title"),
					Description = (string)siteMapNode.Attribute("description"),
					Url = (string)siteMapNode.Attribute("url"),
					Target = (string)siteMapNode.Attribute("target"),
					IsEnabled = isEnabled,
					Items =
						from childNode in siteMapNode.Elements(SiteMapHelper.XNameSiteMapNode)
                        where isEnabled && (string.IsNullOrEmpty((string)childNode.Attribute("disabled")))
						select ChildMenuItem(childNode)
				};

			return result;
		}	

        private void uiContextMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            RadMenuItem radMenuItem = e.OriginalSource as RadMenuItem;
            if (radMenuItem != null)
            {
                SiteMapMenuItem menuItem = radMenuItem.DataContext as SiteMapMenuItem;
                if (menuItem != null && !string.IsNullOrEmpty(menuItem.Url))
                {
                    nsTooltips.ToolTipService.ClearAllToolTip();
                    if (menuItem.Url.Contains(".xaml"))
                    {
                        UriHelper.NavigateTo("#" + menuItem.Url, menuItem.Target);
                        //UriHelper.NavigateTo(menuItem.Url, menuItem.Target);
                        //if (menuItem.Url.Contains("Sale"))
                        //    UriHelper.NavigateTo("#/Sale/DCRoomForecastPage.xaml", null);
                        //else
                        //    UriHelper.NavigateTo("#/FeedbackChartPage.xaml", null);
                    }
                    else
                    {
                        UriHelper.NavigateTo(menuItem.Url, menuItem.Target);
                    }
                }
            }
        }
    }
}
