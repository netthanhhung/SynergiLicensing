using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Synergi.Licensing.Common
{
    public partial class Package
    {
        #region Properties

        private List<Module> _modules;
        [DataMember]
        public List<Module> Modules { get { return _modules; } set { if (!object.ReferenceEquals(this.Modules, value)) { _modules = value; RaisePropertyChanged("Modules"); } } }

        #endregion
    }
}
