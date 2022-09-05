using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Synergi.Licensing.Common
{
    [DataContract]
    public partial class Contract : Record
    {
        #region Public Constructors

        public Contract() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string ContractId = "ContractId";
            public const string CustomerId = "CustomerId";
            public const string LicenseKey = "LicenseKey";
            public const string Date = "Date";
            public const string DomainName = "DomainName";
            public const string SecondDomainName = "SecondDomainName";
            public const string ActivatedDate = "ActivatedDate";
            public const string ExpiredDate = "ExpiredDate";
            public const string RealEndDate = "RealEndDate";
            public const string NbrSites = "NbrSites";
            public const string Description = "Description";
            public const string PackageId = "PackageId";
            public const string TotalPrice = "TotalPrice";
            public const string IsLegacy = "IsLegacy";
        }

        #endregion

        #region Properties


        [DataMember]
        public int ContractId { get { return Utilities.ToInt(RecordId); } set { RecordId = value; RaisePropertyChanged("ContractId"); } }

        private int _customerId;
        [DataMember]
        public int CustomerId { get { return _customerId; } set { if (!this.CustomerId.Equals(value)) { _customerId = value; RaisePropertyChanged("CustomerId"); } } }

        private Guid _licenseKey;
        [DataMember]
        public Guid LicenseKey { get { return _licenseKey; } set { if (!this.LicenseKey.Equals(value)) { _licenseKey = value; RaisePropertyChanged("LicenseKey"); } } }

        private DateTime _date;
        [DataMember]
        public DateTime Date { get { return _date; } set { if (!object.ReferenceEquals(this.Date, value)) { _date = value; RaisePropertyChanged("Date"); } } }

        private string _domainName;
        [DataMember]
        public string DomainName { get { return _domainName; } set { if (!object.ReferenceEquals(this.DomainName, value)) { _domainName = value; RaisePropertyChanged("DomainName"); } } }

        private string _secondDomainName;
        [DataMember]
        public string SecondDomainName { get { return _secondDomainName; } set { if (!object.ReferenceEquals(this.SecondDomainName, value)) { _secondDomainName = value; RaisePropertyChanged("SecondDomainName"); } } }

        private DateTime _activatedDate;
        [DataMember]
        public DateTime ActivatedDate { get { return _activatedDate; } set { if (!object.ReferenceEquals(this.ActivatedDate, value)) { _activatedDate = value; RaisePropertyChanged("ActivatedDate"); } } }

        private DateTime? _expiredDate;
        [DataMember]
        public DateTime? ExpiredDate { get { return _expiredDate; } set { if (!object.ReferenceEquals(this.ExpiredDate, value)) { _expiredDate = value; RaisePropertyChanged("ExpiredDate"); } } }

        private DateTime? _realEndDate;
        [DataMember]
        public DateTime? RealEndDate { get { return _realEndDate; } set { if (!object.ReferenceEquals(this.RealEndDate, value)) { _realEndDate = value; RaisePropertyChanged("RealEndDate"); } } }

        private int _nbrSites;
        [DataMember]
        public int NbrSites { get { return _nbrSites; } set { if (!this.NbrSites.Equals(value)) { _nbrSites = value; RaisePropertyChanged("NbrSites"); } } }

        private string _description;
        [DataMember]
        public string Description { get { return _description; } set { if (!object.ReferenceEquals(this.Description, value)) { _description = value; RaisePropertyChanged("Description"); } } }

        private int _packageId;
        [DataMember]
        public int PackageId { get { return _packageId; } set { if (!this.PackageId.Equals(value)) { _packageId = value; RaisePropertyChanged("PackageId"); } } }

        private decimal _totalPrice;
        [DataMember]
        public decimal TotalPrice { get { return _totalPrice; } set { if (!this.TotalPrice.Equals(value)) { _totalPrice = value; RaisePropertyChanged("TotalPrice"); } } }

        private bool _isLegacy;
        [DataMember]
        public bool IsLegacy { get { return _isLegacy; } set { if (!this.IsLegacy.Equals(value)) { _isLegacy = value; RaisePropertyChanged("IsLegacy"); } } }

        #endregion

    }
}
