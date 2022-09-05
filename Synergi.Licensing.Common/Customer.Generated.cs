using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Synergi.Licensing.Common
{
    [DataContract]
    public partial class Customer : Record
    {
        #region Public Constructors

        public Customer() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string CustomerId = "CustomerId";
            public const string Name = "Name";
            public const string BusinessName = "BusinessName";
            public const string ContactInformationId = "ContactInformationId";
            public const string ShippingContactInformationId = "ShippingContactInformationId";
            public const string IsLegacy = "IsLegacy";

        }

        #endregion

        #region Properties

        [DataMember]
        public int CustomerId { get { return Utilities.ToInt(RecordId); } set { RecordId = value; RaisePropertyChanged("CustomerId"); } }

        private string _name;
        [DataMember]
        public string Name { get { return _name; } set { if (!object.ReferenceEquals(this.Name, value)) { _name = value; RaisePropertyChanged("Name"); } } }

        private string _businessName;
        [DataMember]
        public string BusinessName { get { return _businessName; } set { if (!object.ReferenceEquals(this.BusinessName, value)) { _businessName = value; RaisePropertyChanged("BusinessName"); } } }

        private int? _contactInformationId;
        [DataMember]
        public int? ContactInformationId { get { return _contactInformationId; } set { if (!object.ReferenceEquals(this.ContactInformationId, value)) { _contactInformationId = value; RaisePropertyChanged("ContactInformationId"); } } }

        private int? _shippingContactInformationId;
        [DataMember]
        public int? ShippingContactInformationId { get { return _shippingContactInformationId; } set { if (!object.ReferenceEquals(this.ShippingContactInformationId, value)) { _shippingContactInformationId = value; RaisePropertyChanged("ShippingContactInformationId"); } } }

        private bool _isLegacy;
        [DataMember]
        public bool IsLegacy { get { return _isLegacy; } set { if (!this.IsLegacy.Equals(value)) { _isLegacy = value; RaisePropertyChanged("IsLegacy"); } } }

        #endregion

    }
}
