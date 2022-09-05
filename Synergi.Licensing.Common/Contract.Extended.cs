using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Synergi.Licensing.Common
{
    public partial class Contract
    {
        #region Properties

        private Customer _customer;
        [DataMember]
        public Customer Customer { get { return _customer; } set { if (!object.ReferenceEquals(this.Customer, value)) { _customer = value; RaisePropertyChanged("Customer"); } } }

        private Package _package;
        [DataMember]
        public Package Package { get { return _package; } set { if (!object.ReferenceEquals(this.Package, value)) { _package = value; RaisePropertyChanged("Package"); } } }

        private List<Module> _additionalModules;
        [DataMember]
        public List<Module> AdditionalModules { get { return _additionalModules; } set { if (!object.ReferenceEquals(this.AdditionalModules, value)) { _additionalModules = value; RaisePropertyChanged("AdditionalModules"); } } }

        [DataMember]
        public string AdditionalModulesString
        {
            get
            {
                string result = string.Empty;
                if (_additionalModules != null)
                {
                    foreach (Module mod in _additionalModules)
                        result += mod.Name + ", ";
                    if (!string.IsNullOrEmpty(result))
                    {
                        result = result.Substring(0, result.Length - 2);
                    }
                }
                return result;
            }
            set { }
        }

        private List<CompetitorSource> _competitorSources;
        [DataMember]
        public List<CompetitorSource> CompetitorSources { get { return _competitorSources; } set { if (!object.ReferenceEquals(this.CompetitorSources, value)) { _competitorSources = value; RaisePropertyChanged("CompetitorSources"); } } }

        [DataMember]
        public string CompetitorSourcesString
        {
            get
            {
                string result = string.Empty;
                if (_competitorSources != null)
                {
                    foreach (CompetitorSource mod in _competitorSources)
                        result += mod.Name + ", ";
                    if (!string.IsNullOrEmpty(result))
                    {
                        result = result.Substring(0, result.Length - 2);
                    }
                }
                return result;
            }
            set { }
        }

        private string _encryptedString;
        [DataMember]
        public string EncryptedString { get { return _encryptedString; } set { if (!object.ReferenceEquals(this.EncryptedString, value)) { _encryptedString = value; RaisePropertyChanged("EncryptedString"); } } }

        #endregion
    }
}
