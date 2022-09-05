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
using System.Windows.Threading;
using Telerik.Windows.Controls;
using Synergi.Licensing.Common;
using Synergi.Licensing.Silverlight.UI.SynergiLicensingService;

namespace Synergi.Licensing.Silverlight.UI
{
    public partial class App : Application
    {
        /*  ======================================================================            
         *      PAGE MEMBERS
         *  ====================================================================== */
        internal class ResourcesObjects
        {
            internal class Styles
            {
                private const string _holdingsVariance = "uiHoldingsVarianceStyle";
                private const string _inputControls = "InputControlStyle";

                public Style HoldingsVariance()
                {
                    return App.Current.Resources[_holdingsVariance] as Style;
                }
            }
        }



        /*  ======================================================================            
         *      EVENT HANDLERS
         *  ====================================================================== */
        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        private StartupEventArgs startupEventArgs;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.startupEventArgs = e;
            // Get global variables
            DataServiceHelper.GetGlobalVariableAsync(GetGlobalVariableCompleted);            
        }

        internal void GetGlobalVariableCompleted(GlobalVariable itemsSource)
        {
            if (itemsSource == null)
            {
                DataServiceHelper.SelectSessionId();
            }
            else
            {
                Globals.UserLogin = itemsSource.UserLogin;
                Globals.AppSettings = itemsSource.AppSettings;
                Globals.ApplicationCurrentDateTime = itemsSource.ApplicationCurrentDateTime;

                //Security section
                Globals.UserLogin.IsUserCustomer = false;
                Globals.UserLogin.IsUserAdministrator = true;                

                // Until their page has been converted to SL, redirect Portal admin to their aspx page (unless just loading the header).
                string typeName = this.startupEventArgs.InitParams["TypeName"];
                if (typeName == "MainPage")
                {
                    Type t = Type.GetType(this.GetType().Namespace + "." + typeName);
                    UIElement elem = Activator.CreateInstance(t) as UIElement;
                    if (elem != null)
                    {
                        if (elem is MainPage)
                        {
                            MainPage mainPage = (MainPage)elem;
                            mainPage.ContentTypeName = this.startupEventArgs.InitParams["ContentTypeName"];
                        }

                        StyleManager.ApplicationTheme = new Office_BlackTheme();
                        FontHelper.SetFontType(elem, new FontFamily("Corbel"), 12);

                        this.RootVisual = elem;

                        // Set keepalive timer to call every 10 minutes to renew asp.net authentication.
                        DispatcherTimer keepAliveTimer = new DispatcherTimer();
                        keepAliveTimer.Interval = new TimeSpan(0, 0, 10, 0, 0);
                        keepAliveTimer.Tick += new EventHandler(keepAliveTimer_Tick);
                        keepAliveTimer.Start();
                    }

                }
            }
        }


        void keepAliveTimer_Tick(object sender, EventArgs e)
        {
            DataServiceHelper.SelectSessionId();
        }

        private void Application_Exit(object sender, EventArgs e)
        {

        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached)
            {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }



        /*  ======================================================================            
         *      PAGE FUNCTIONS
         *  ====================================================================== */
        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
    }
}
