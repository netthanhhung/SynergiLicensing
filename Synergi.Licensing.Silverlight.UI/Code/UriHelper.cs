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
using System.Windows.Browser;

namespace Synergi.Licensing.Silverlight.UI
{
    internal static class UriHelper
    {
        internal static Uri FormatUri(string page)
        {
            Uri uri = HtmlPage.Document.DocumentUri;
            UriBuilder builder = new UriBuilder(uri.Scheme, uri.Host, uri.Port, page);
            Uri result = new Uri(HttpUtility.UrlDecode(builder.Uri.ToString()));

            return result;
        }

        internal static void NavigateTo(string page, string target)
        {
            Globals.IsBusy = false;
            Uri uri = page.StartsWith("http") ? new Uri(page) : FormatUri(page);
            if (string.IsNullOrEmpty(target))
            {
                HtmlPage.Window.Navigate(uri); 
            }
            else
            {
                HtmlPage.Window.Navigate(uri, target); 
            }
        }

        internal static void NavigateTo(string page, bool newWindow)
        {
            string target = newWindow ? "_blank" : string.Empty;
            NavigateTo(page, target);
        }

        internal static void NavigateTo(string page)
        {
            NavigateTo(page, false);
        }
    }
}
