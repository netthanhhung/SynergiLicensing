using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Synergi.Licensing.Business;
using System.ServiceModel.Activation;

namespace Synergi.Licensing.Web.UI
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SynergiLicensingServiceExternal
    {
        [OperationContract]
        public string GetEncryptedLicenseInfo(Guid licenseKey)
        {
            return SynergiMethods.GetEncryptedLicenseInfo(licenseKey);
        }
    }
}
