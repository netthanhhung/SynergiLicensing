using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;
using Synergi.Licensing.Common;

namespace Synergi.Licensing.Silverlight.UI
{
    internal static class SiteMapHelper
    {
        /*  ======================================================================            
         *      EVENTS AND DELEGATES
         *  ====================================================================== */
        internal delegate void SelectWebSiteMapCallback(XDocument xdocument);        


        /*  ======================================================================            
         *      PAGE MEMBERS
         *  ====================================================================== */
        private static Dictionary<System.Guid, Delegate> _callbacks = new Dictionary<Guid, Delegate>();
        private static Uri _siteMapUri { get { return UriHelper.FormatUri("/SiteMaps/Web.sitemap.txt?XapGuid=" + DeploymentInfo.XapGuid); } }
        internal static XName XNameSiteMapNode { get { return GetXName("siteMapNode"); } }



        /*  ======================================================================            
         *      EVENT HANDLERS
         *  ====================================================================== */
        static void webSiteMapClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                if (e.Error == null)
                {
                    XDocument xdocument = null;
                    using (Stream s = e.Result)
                    {
                        xdocument = XDocument.Load(s);
                    }
                    ((SelectWebSiteMapCallback)_callbacks[callerKey]).Invoke(xdocument);
                }

                _callbacks.Remove(callerKey);
            }
        }


        /*  ======================================================================            
         *      PAGE FUNCTIONS
         *  ====================================================================== */
        internal static void SelectWeb1SiteMapAsync(SelectWebSiteMapCallback callback)
        {
            System.Guid callerKey = Guid.NewGuid();
            _callbacks.Add(callerKey, callback);

            WebClient webSiteMapClient = new WebClient();
            webSiteMapClient.OpenReadCompleted += new OpenReadCompletedEventHandler(webSiteMapClient_OpenReadCompleted);
            webSiteMapClient.OpenReadAsync(_siteMapUri, callerKey);
        }

        internal static void SelectWebSiteMapAsync(SelectWebSiteMapCallback callback)
        {
            System.Guid callerKey = Guid.NewGuid();
            _callbacks.Add(callerKey, callback);

            WebClient webSiteMapClient = new WebClient();
            webSiteMapClient.OpenReadCompleted += new OpenReadCompletedEventHandler(webSiteMapClient_OpenReadCompleted);
            webSiteMapClient.OpenReadAsync(_siteMapUri, callerKey);
        }

        private static XName GetXName(string elementName)
        {
            return XName.Get(elementName, "http://schemas.microsoft.com/AspNet/SiteMap-File-1.0");
        }
    }
}
