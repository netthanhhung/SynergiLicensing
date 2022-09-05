using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Synergi.Licensing.Common
{
    [DataContract]
	public partial class Country : Record
	{
        public class ColumnNames
        {
            public const string CountryId = "CountryId";
            public const string Name = "Name";
        }

		#region Public Constructors
		public Country() : base () {}
		#endregion

		#region Public Properties

        [DataMember]
        public int CountryId { get { return Utilities.ToInt(RecordId); } set { RecordId = value; RaisePropertyChanged("CountryId"); } }

        private string _name;
        [DataMember]
        public string Name { get { return _name; } set { if (!object.ReferenceEquals(this.Name, value)) { _name = value; RaisePropertyChanged("Name"); } } }

		#endregion
	}
}
