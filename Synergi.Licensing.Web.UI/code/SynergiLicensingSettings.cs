using System;
using System.Web;
using Synergi.Licensing.Common;
using System.Configuration;

namespace Synergi.Licensing.Web.UI
{
    public partial class SynergiLicensingSettings
    {        
        public static int GetModuleCount
        {
            get { return System.Enum.GetNames(typeof(SynergiLicensingSettings.Modules)).Length; }
        }

        public static String[] GetModuleNames()
        {
            return System.Enum.GetNames(typeof(SynergiLicensingSettings.Modules));
        }

        public static int GetModuleValue(int index)
        {
            return (int)System.Enum.GetValues(typeof(SynergiLicensingSettings.Modules)).GetValue(index);
        }

        //public static void CacheSettings()
        //{
        //    _applicationSettings.CacheSettings();
        //}

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static bool IsEnableAutoLoginComplete()
        {            
            return Convert.ToBoolean(ConfigurationSettings.AppSettings["EnableAutoLoginComplete"]);
        }
    }
}
