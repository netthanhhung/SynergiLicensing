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

namespace Synergi.Licensing.Silverlight.UI
{
    public partial class SideBarMenu : UserControl
    {
        private const string _navigateCommand = "N";

        /*  ======================================================================            
         *      EVENT HANDLERS
         *  ====================================================================== */
        public SideBarMenu()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(Menu_Loaded);
            uiMenu.ItemClick += new Telerik.Windows.RadRoutedEventHandler(uiMenu_ItemClick);
        }

        void uiMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            RadMenuItem radMenuItem = e.OriginalSource as RadMenuItem;
            if (radMenuItem != null)
            {
                SiteMapMenuItem menuItem = radMenuItem.DataContext as SiteMapMenuItem;
                if (menuItem != null)
                {
                    UriHelper.NavigateTo(menuItem.Url, menuItem.Target);
                }
            }
        }

        void Menu_Loaded(object sender, RoutedEventArgs e)
        {
            SiteMapHelper.SelectWebSiteMapAsync(LoadMenuItems);
        }



        /*  ======================================================================            
         *      PAGE FUNCTIONS
         *  ====================================================================== */
        private SiteMapMenuItem ChildMenuItem(XElement siteMapNode)
        {
            SiteMapMenuItem result =
                new SiteMapMenuItem()
                {
                    Title = (string)siteMapNode.Attribute("title"),
                    Description = (string)siteMapNode.Attribute("description"),
                    Url = (string)siteMapNode.Attribute("url"),
                    IsEnabled = CheckRoleAccess((string)siteMapNode.Attribute("roles")),
                    Target = (string)siteMapNode.Attribute("target"),
                    Items =
                        from childNode in siteMapNode.Elements(SiteMapHelper.XNameSiteMapNode)
                        select ChildMenuItem(childNode)
                };

            return result;
        }

        private bool CheckRoleAccess(string roles)
        {
            return true;
        }

        private void LoadMenuItems(XDocument xdocument)
        {
            IEnumerable<SiteMapMenuItem> itemsSource =
                from menuitem in xdocument.Root.Elements(SiteMapHelper.XNameSiteMapNode)
                select ChildMenuItem(menuitem);

            uiMenu.ItemsSource = itemsSource;
        }
    }
}
