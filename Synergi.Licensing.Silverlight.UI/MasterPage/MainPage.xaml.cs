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
    public partial class MainPage : Page
    {
        /*  ======================================================================            
         *      PAGE MEMBERS
         *  ====================================================================== */
        internal string ContentTypeName { get; set; }

        public bool IsBusy
        {
            get { return uiBusyIndicator.IsBusy; }
            set { uiBusyIndicator.IsBusy = value; }
        }


        /*  ======================================================================            
         *      EVENT HANDLERS
         *  ====================================================================== */
        public MainPage()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            // @@@@@
            //Logger.Instance.Critical("SL.Critical");
            //Logger.Instance.Error("SL.Error");
            //Logger.Instance.Warning("SL.Warning");
            //Logger.Instance.Information("SL.Information");
            //Logger.Instance.Verbose("SL.Verbose");
			
			//Type t = Type.GetType(this.GetType().Namespace + "." + ContentTypeName);
            //UIElement elem = Activator.CreateInstance(t) as UIElement;
            //ContentPlaceholder.Children.Add(elem);
            //if (Globals.UserLogin.IsUserPortalAdministrator)
            //{
            //    //System.Windows.Browser.HtmlPage.Window.Navigate(new Uri("/PortalAdmin/PortalAdmin.aspx", UriKind.Relative));
            //    UriHelper.NavigateTo("#/Administration/PortalAdminPage.xaml");
            //}
        }
    }
}
