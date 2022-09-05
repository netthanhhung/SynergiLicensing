using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Synergi.Licensing.Common
{
    public partial class Customer
    {
        #region Properties

        private ContactInformation _contactInformation;
        [DataMember]
        public ContactInformation ContactInformation { get { return _contactInformation; } set { if (!object.ReferenceEquals(this.ContactInformation, value)) { _contactInformation = value; RaisePropertyChanged("ContactInformation"); } } }

        private ContactInformation _shippingContactInformation;
        [DataMember]
        public ContactInformation ShippingContactInformation { get { return _shippingContactInformation; } set { if (!object.ReferenceEquals(this.ShippingContactInformation, value)) { _shippingContactInformation = value; RaisePropertyChanged("ShippingContactInformation"); } } }

        private List<Contract> _contracts;
        [DataMember]
        public List<Contract> Contracts { get { return _contracts; } set { if (!object.ReferenceEquals(this.Contracts, value)) { _contracts = value; RaisePropertyChanged("Contracts"); } } }

        [DataMember]
        public string NameWithStatus
        {
            get { return this.Name + (this.IsLegacy ? " (Legacy)" : string.Empty); }
            set { }
        }

        #endregion
    }
}
