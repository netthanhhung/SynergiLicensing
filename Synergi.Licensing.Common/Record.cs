using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Synergi.Licensing.Common
{
    [DataContract]
    public abstract partial class Record : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

		protected Record() {}

		public virtual string TypeName { get { return this.GetType().Name; } }
        public const string Unknown = "(Unknown)";
        public const string NewRecord = "New Record.";

		private long? _recordId;
		private byte[] _concurrency;
		private DateTime? _dateCreated;
		private DateTime? _dateUpdated;
		private string _createdBy;
		private string _updatedBy;

        [DataMember]
		public long? RecordId
		{
            get { return _recordId; }
			set { _recordId = value; }
		}

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember]
        public byte[] Concurrency
		{
			get { return _concurrency; }
			set { _concurrency = value; }
		}

        [DataMember]
		public DateTime? DateCreated
		{
			get { return _dateCreated; }
			set { _dateCreated = value; }
		}

        [DataMember]
		public DateTime? DateUpdated
		{
			get { return _dateUpdated; }
			set { _dateUpdated = value; }
		}

        [DataMember]
		public string CreatedBy
		{
			get { return _createdBy ?? string.Empty; }
			set { _createdBy = value; }
		}

        [DataMember]
		public string UpdatedBy
		{
			get { return _updatedBy ?? string.Empty; }
			set { _updatedBy = value; }
		}

        [DataMember]
        public string CreatedAndUpdatedText
        {
            get
            {
                if (null != _dateCreated)    // If we actually have an existing record
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    sb.Append("Created ");
                    sb.Append(_dateCreated != null ? _dateCreated.GetValueOrDefault().ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : Unknown);
                    sb.Append(" by ");
                    sb.Append(CreatedBy.Length > 0 ? CreatedBy : Unknown);
                    sb.Append(", last updated ");
                    sb.Append(_dateUpdated != null ? _dateUpdated.ToString() : Unknown);
                    sb.Append(" by ");
                    sb.Append(UpdatedBy.Length > 0 ? UpdatedBy : Unknown);
                    sb.Append(".");
                    return sb.ToString();
                }
                else
                    return NewRecord;
            }
            set { }
        }

        [DataMember]
        public string LastUpdatedText
        {
            get
            {
                if (null != _dateUpdated)    // If we actually have an existing record
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    sb.Append("Last updated ");
                    sb.Append(_dateUpdated != null ? _dateUpdated.ToString() : Unknown);
                    sb.Append(" by ");
                    sb.Append(UpdatedBy.Length > 0 ? UpdatedBy : Unknown);
                    sb.Append(".");
                    return sb.ToString();
                }
                else
                {
                    return NewRecord;
                }
            }
            set { }
        }

        [DataMember]
        public string RecordInformationText
        {
            get { return CreatedAndUpdatedText + System.Environment.NewLine + "Record No. " + (this.NullableRecordId != null ? this.NullableRecordId.ToString() : Unknown); }
            set { }
        }

        public long? NullableRecordId
        {
            get { return RecordId > 0 ? RecordId : null; }
        }

        private bool _canDelete;
        [DataMember]
        public bool CanDelete { get { return _canDelete; } set { if (this.CanDelete != value) { _canDelete = value; RaisePropertyChanged("CanDelete"); } } }

        private bool _isDeleted;
        [DataMember]
        public bool IsDeleted { get { return _isDeleted; } set { if (this.IsDeleted != value) { _isDeleted = value; RaisePropertyChanged("IsDeleted"); } } }

        private bool _isChanged;
        [DataMember]
        public bool IsChanged { get { return _isChanged; } set { if (this.IsChanged != value) { _isChanged = value; RaisePropertyChanged("IsChanged"); } } }

	}
}