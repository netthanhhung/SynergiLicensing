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
using System.Windows.Navigation;
using System.Security.Principal;
using System.ComponentModel;

namespace Synergi.Licensing.Silverlight.UI
{
    public class AuthorizedContentLoader : DependencyObject, INavigationContentLoader
    {

        public AuthorizedContentLoader()
        {
            
        }
     

        private PageResourceContentLoader _loader = new PageResourceContentLoader();
        
        public IAsyncResult BeginLoad(Uri targetUri, Uri currentUri, AsyncCallback userCallback, object asyncState)
        {
            return _loader.BeginLoad(targetUri, currentUri, userCallback, asyncState);
        }

        public bool CanLoad(Uri targetUri, Uri currentUri)
        {
            return true;
            //return AuthenticationContext.CheckAuthenticationAndAuthorization(targetUri, currentUri);
        }

        public void CancelLoad(IAsyncResult asyncResult)
        {
            _loader.CancelLoad(asyncResult);   
        }

        public LoadResult EndLoad(IAsyncResult asyncResult)
        {
            return _loader.EndLoad(asyncResult);
        }
    }
}
