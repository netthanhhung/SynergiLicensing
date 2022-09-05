using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synergi.Licensing.Common
{
	public static partial class DeploymentInfo
	{
        // NOTE: Change Guid each deployment too so that stupid cache is avoided. http://createguid.com/ 
        public const string XapGuid = "91AE6130-1EBB-4B3B-A6C0-78345A5E27F1";
        public const string MinRuntimeVersion = "4.0.60310.0";

        public const string Version = "1.0.0.0";
        public const string Date = "19-Mar-2012";

        public new static string ToString()
        {
            const string format = "v{0} - ({1})";
            return string.Format(format, DeploymentInfo.Version, DeploymentInfo.Date);
        }
	}
}
