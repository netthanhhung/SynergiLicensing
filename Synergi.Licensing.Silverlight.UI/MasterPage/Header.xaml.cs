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
using Telerik.Windows.Controls;
using System.Xml.Linq;
using nsTooltips = Silverlight.Controls.ToolTips;

namespace Synergi.Licensing.Silverlight.UI
{
    public partial class Header : UserControl
    {
        public Header()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Header_Loaded);
        }

        void Header_Loaded(object sender, RoutedEventArgs e)
        {
            uiDeploymentInfo.Text = DeploymentInfo.ToString();
            uiBtnLogo.DataContext = this;
            SiteMapHelper.SelectWeb1SiteMapAsync(LoadMenuItems);
        }


        private void LoadMenuItems(XDocument xdocument)
        {
            IEnumerable<SiteMapMenuItem> itemsSource =
                from menuitem in xdocument.Root.Elements(SiteMapHelper.XNameSiteMapNode)
                select ChildMenuItem(menuitem);
            foreach (SiteMapMenuItem parentItem in itemsSource)
            {
                if (parentItem.Title == "Logo")
                {
                    uiContextMenu.ItemsSource = parentItem.Items;
                    break;
                }
            }

        }

        private SiteMapMenuItem ChildMenuItem(XElement siteMapNode)
        {
            bool isEnabled = (string.IsNullOrEmpty((string)siteMapNode.Attribute("disabled")));

            SiteMapMenuItem result =
                new SiteMapMenuItem()
                {
                    Title = (string)siteMapNode.Attribute("title"),
                    Description = (string)siteMapNode.Attribute("description"),
                    Url = (string)siteMapNode.Attribute("url"),
                    IsEnabled = isEnabled,
                    Target = (string)siteMapNode.Attribute("target"),
                    Items =
                        from childNode in siteMapNode.Elements(SiteMapHelper.XNameSiteMapNode)
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
