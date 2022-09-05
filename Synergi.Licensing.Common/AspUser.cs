using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;

namespace Synergi.Licensing.Common
{
    [DataContract]
    public partial class AspUser : INotifyPropertyChanged
    {
        #region Public Constructors

        public AspUser() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string ApplicationId = "ApplicationId";
            public const string UserId = "UserId";
            public const string Password = "Password";
            public const string PasswordFormat = "PasswordFormat";
            public const string PasswordSalt = "PasswordSalt";
            public const string MobilePIN = "MobilePIN";
            public const string Email = "Email";
            public const string LoweredEmail = "LoweredEmail";
            public const string PasswordQuestion = "PasswordQuestion";
            public const string PasswordAnswer = "PasswordAnswer";
            public const string IsApproved = "IsApproved";
            public const string IsLockedOut = "IsLockedOut";
            public const string CreateDate = "CreateDate";
            public const string LastLoginDate = "LastLoginDate";
            public const string LastPasswordChangedDate = "LastPasswordChangedDate";
            public const string LastLockoutDate = "LastLockoutDate";
            public const string FailedPasswordAttemptCount = "FailedPasswordAttemptCount";
            public const string FailedPasswordAttemptWindowStart = "FailedPasswordAttemptWindowStart";
            public const string FailedPasswordAnswerAttemptCount = "FailedPasswordAnswerAttemptCount";
            public const string FailedPasswordAnswerAttemptWindowStart = "FailedPasswordAnswerAttemptWindowStart";
            public const string Comment = "Comment";

            public const string UserName = "UserName";
            public const string LoweredUserName = "LoweredUserName";
            public const string MobileAlias = "MobileAlias";
            public const string IsAnonymous = "IsAnonymous";
            public const string LastActivityDate = "LastActivityDate";

        }


        #endregion

        #region Properties

        private Guid _applicationId;
        [DataMember]
        public Guid ApplicationId { get { return _applicationId; } set { if (!this.ApplicationId.Equals(value)) { _applicationId = value; RaisePropertyChanged("ApplicationId"); } } }

        private Guid _userId;
        [DataMember]
        public Guid UserId { get { return _userId; } set { if (!this.UserId.Equals(value)) { _userId = value; RaisePropertyChanged("UserId"); } } }

        private string _password;
        [DataMember]
        public string Password { get { return _password; } set { if (!object.ReferenceEquals(this.Password, value)) { _password = value; RaisePropertyChanged("Password"); } } }

        private int _passwordFormat;
        [DataMember]
        public int PasswordFormat { get { return _passwordFormat; } set { if (!this.PasswordFormat.Equals(value)) { _passwordFormat = value; RaisePropertyChanged("PasswordFormat"); } } }

        private string _passwordSalt;
        [DataMember]
        public string PasswordSalt { get { return _passwordSalt; } set { if (!object.ReferenceEquals(this.PasswordSalt, value)) { _passwordSalt = value; RaisePropertyChanged("PasswordSalt"); } } }

        private string _mobilePIN;
        [DataMember]
        public string MobilePIN { get { return _mobilePIN; } set { if (!object.ReferenceEquals(this.MobilePIN, value)) { _mobilePIN = value; RaisePropertyChanged("MobilePIN"); } } }

        private string _email;
        [DataMember]
        public string Email { get { return _email; } set { if (!object.ReferenceEquals(this.Email, value)) { _email = value; RaisePropertyChanged("Email"); } } }

        private string _loweredEmail;
        [DataMember]
        public string LoweredEmail { get { return _loweredEmail; } set { if (!object.ReferenceEquals(this.LoweredEmail, value)) { _loweredEmail = value; RaisePropertyChanged("LoweredEmail"); } } }

        private string _passwordQuestion;
        [DataMember]
        public string PasswordQuestion { get { return _passwordQuestion; } set { if (!object.ReferenceEquals(this.PasswordQuestion, value)) { _passwordQuestion = value; RaisePropertyChanged("PasswordQuestion"); } } }

        private string _passwordAnswer;
        [DataMember]
        public string PasswordAnswer { get { return _passwordAnswer; } set { if (!object.ReferenceEquals(this.PasswordAnswer, value)) { _passwordAnswer = value; RaisePropertyChanged("PasswordAnswer"); } } }

        private bool _isApproved;
        [DataMember]
        public bool IsApproved { get { return _isApproved; } set { if (!this.IsApproved.Equals(value)) { _isApproved = value; RaisePropertyChanged("IsApproved"); } } }

        private bool _isLockedOut;
        [DataMember]
        public bool IsLockedOut { get { return _isLockedOut; } set { if (!this.IsLockedOut.Equals(value)) { _isLockedOut = value; RaisePropertyChanged("IsLockedOut"); } } }

        private DateTime _creationDate;
        [DataMember]
        public DateTime CreationDate { get { return _creationDate; } set { if (!this.CreationDate.Equals(value)) { _creationDate = value; RaisePropertyChanged("CreationDate"); } } }

        private DateTime _lastLoginDate;
        [DataMember]
        public DateTime LastLoginDate { get { return _lastLoginDate; } set { if (!this.LastLoginDate.Equals(value)) { _lastLoginDate = value; RaisePropertyChanged("LastLoginDate"); } } }

        private DateTime _lastPasswordChangedDate;
        [DataMember]
        public DateTime LastPasswordChangedDate { get { return _lastPasswordChangedDate; } set { if (!this.LastPasswordChangedDate.Equals(value)) { _lastPasswordChangedDate = value; RaisePropertyChanged("LastPasswordChangedDate"); } } }

        private DateTime _lastLockoutDate;
        [DataMember]
        public DateTime LastLockoutDate { get { return _lastLockoutDate; } set { if (!this.LastLockoutDate.Equals(value)) { _lastLockoutDate = value; RaisePropertyChanged("LastLockoutDate"); } } }

        private int _failedPasswordAttemptCount;
        [DataMember]
        public int FailedPasswordAttemptCount { get { return _failedPasswordAttemptCount; } set { if (!this.FailedPasswordAttemptCount.Equals(value)) { _failedPasswordAttemptCount = value; RaisePropertyChanged("FailedPasswordAttemptCount"); } } }

        private DateTime _failedPasswordAttemptWindowStart;
        [DataMember]
        public DateTime FailedPasswordAttemptWindowStart { get { return _failedPasswordAttemptWindowStart; } set { if (!this.FailedPasswordAttemptWindowStart.Equals(value)) { _failedPasswordAttemptWindowStart = value; RaisePropertyChanged("FailedPasswordAttemptWindowStart"); } } }

        private int _failedPasswordAnswerAttemptCount;
        [DataMember]
        public int FailedPasswordAnswerAttemptCount { get { return _failedPasswordAnswerAttemptCount; } set { if (!this.FailedPasswordAnswerAttemptCount.Equals(value)) { _failedPasswordAnswerAttemptCount = value; RaisePropertyChanged("FailedPasswordAnswerAttemptCount"); } } }

        private DateTime _failedPasswordAnswerAttemptWindowStart;
        [DataMember]
        public DateTime FailedPasswordAnswerAttemptWindowStart { get { return _failedPasswordAnswerAttemptWindowStart; } set { if (!this.FailedPasswordAnswerAttemptWindowStart.Equals(value)) { _failedPasswordAnswerAttemptWindowStart = value; RaisePropertyChanged("FailedPasswordAnswerAttemptWindowStart"); } } }

        private string _comment;
        [DataMember]
        public string Comment { get { return _comment; } set { if (!object.ReferenceEquals(this.Comment, value)) { _comment = value; RaisePropertyChanged("Comment"); } } }

        private string _userName;
        [DataMember]
        public string UserName { get { return _userName; } set { if (!object.ReferenceEquals(this.UserName, value)) { _userName = value; RaisePropertyChanged("UserName"); } } }

        private string _loweredUserName;
        [DataMember]
        public string LoweredUserName { get { return _loweredUserName; } set { if (!object.ReferenceEquals(this.LoweredUserName, value)) { _loweredUserName = value; RaisePropertyChanged("LoweredUserName"); } } }

        private string _mobileAlias;
        [DataMember]
        public string MobileAlias { get { return _mobileAlias; } set { if (!object.ReferenceEquals(this.MobileAlias, value)) { _mobileAlias = value; RaisePropertyChanged("MobileAlias"); } } }

        private bool _isAnonymous;
        [DataMember]
        public bool IsAnonymous { get { return _isAnonymous; } set { if (!this.IsAnonymous.Equals(value)) { _isAnonymous = value; RaisePropertyChanged("IsAnonymous"); } } }

        private DateTime _lastActivityDate;
        [DataMember]
        public DateTime LastActivityDate { get { return _lastActivityDate; } set { if (!this.LastActivityDate.Equals(value)) { _lastActivityDate = value; RaisePropertyChanged("LastActivityDate"); } } }


        private bool _isOnline;
        [DataMember]
        public bool IsOnline { get { return _isOnline; } set { if (!this.IsOnline.Equals(value)) { _isOnline = value; RaisePropertyChanged("IsOnline"); } } }

        private string _providerName;
        [DataMember]
        public string ProviderName { get { return _providerName; } set { if (!object.ReferenceEquals(this.ProviderName, value)) { _providerName = value; RaisePropertyChanged("ProviderName"); } } }

        private object _providerUserKey;
        [DataMember]
        public object ProviderUserKey { get { return _providerUserKey; } set { if (!object.ReferenceEquals(this.ProviderUserKey, value)) { _providerUserKey = value; RaisePropertyChanged("ProviderUserKey"); } } }        

        [DataMember]
        public string DisplayName
        {
            get
            {
                string displayName = UserName;                
                return displayName;
            }
            set { }
        }        

        private bool _isResetPassword;
        [DataMember]
        public bool IsResetPassword { get { return _isResetPassword; } set { if (!this.IsResetPassword.Equals(value)) { _isResetPassword = value; RaisePropertyChanged("IsResetPassword"); } } }

        private string _newGenPassword;
        [DataMember]
        public string NewGenPassword { get { return _newGenPassword; } set { if (!object.ReferenceEquals(this.NewGenPassword, value)) { _newGenPassword = value; RaisePropertyChanged("NewGenPassword"); } } }

        private string _inputPassword;
        [DataMember]
        public string InputPassword { get { return _inputPassword; } set { if (!object.ReferenceEquals(this.InputPassword, value)) { _inputPassword = value; RaisePropertyChanged("InputPassword"); } } }

        private bool _isSavedQAError;
        [DataMember]
        public bool IsSavedQAError { get { return _isSavedQAError; } set { if (!this.IsSavedQAError.Equals(value)) { _isSavedQAError = value; RaisePropertyChanged("IsSavedQAError"); } } }

        private string _errorMessage;
        [DataMember]
        public string ErrorMessage { get { return _errorMessage; } set { if (!object.ReferenceEquals(this.ErrorMessage, value)) { _errorMessage = value; RaisePropertyChanged("ErrorMessage"); } } }

         private bool _canDelete;
        [DataMember]
        public bool CanDelete { get { return _canDelete; } set { if (!this.CanDelete.Equals(value)) { _canDelete = value; RaisePropertyChanged("CanDelete"); } } }

        private bool _isDeleted;
        [DataMember]
        public bool IsDeleted { get { return _isDeleted; } set { if (!this.IsDeleted.Equals(value)) { _isDeleted = value; RaisePropertyChanged("IsDeleted"); } } }

        private bool _isChanged;
        [DataMember]
        public bool IsChanged { get { return _isChanged; } set { if (!this.IsChanged.Equals(value)) { _isChanged = value; RaisePropertyChanged("IsChanged"); } } }
	

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




