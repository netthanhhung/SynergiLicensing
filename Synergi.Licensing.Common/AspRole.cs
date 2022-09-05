using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Synergi.Licensing.Common
{
    [DataContract]
    public partial class AspRole : INotifyPropertyChanged
    {
        #region Public Constructors

        public AspRole() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string ApplicationId = "ApplicationId";
            public const string RoleId = "RoleId";           
            public const string RoleName = "RoleName";           
            public const string LoweredRoleName = "LoweredRoleName";           
            public const string Description = "Description";           
            public const string IsCustom = "IsCustom";
            public const string CanDelete = "CanDelete"; 
        }

        #endregion

        #region Properties
        private Guid _applicationId;
        [DataMember]
        public Guid ApplicationId { get { return _applicationId; } set { if (this.ApplicationId != value) { _applicationId = value; RaisePropertyChanged("ApplicationId"); } } }

        private Guid _roleId;
        [DataMember]
        public Guid RoleId { get { return _roleId; } set { if (this.RoleId != value) { _roleId = value; RaisePropertyChanged("RoleId"); } } }

        private string _roleName;
        [DataMember]
        public string RoleName { get { return _roleName; } set { if (!object.ReferenceEquals(this.RoleName, value)) { _roleName = value; RaisePropertyChanged("RoleName"); } } }

        private string _loweredRoleName;
        [DataMember]
        public string LoweredRoleName { get { return _loweredRoleName; } set { if (!object.ReferenceEquals(this.LoweredRoleName, value)) { _loweredRoleName = value; RaisePropertyChanged("LoweredRoleName"); } } }

        private string _description;
        [DataMember]
        public string Description { get { return _description; } set { if (!object.ReferenceEquals(this.Description, value)) { _description = value; RaisePropertyChanged("Description"); } } }

        private bool _isCustom;
        [DataMember]
        public bool IsCustom { get { return _isCustom; } set { if (this.IsCustom != value) { _isCustom = value; RaisePropertyChanged("IsCustom"); } } }

        private bool _canDelete;
        [DataMember]
        public bool CanDelete { get { return _canDelete; } set { if (this.CanDelete != value) { _canDelete = value; RaisePropertyChanged("CanDelete"); } } }

        private bool _isDeleted;
        [DataMember]
        public bool IsDeleted { get { return _isDeleted; } set { if (this.IsDeleted != value) { _isDeleted = value; RaisePropertyChanged("IsDeleted"); } } }

        private bool _isChanged;
        [DataMember]
        public bool IsChanged { get { return _isChanged; } set { if (this.IsChanged != value) { _isChanged = value; RaisePropertyChanged("IsChanged"); } } }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #endregion
    }
}




