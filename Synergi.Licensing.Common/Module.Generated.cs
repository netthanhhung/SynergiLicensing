using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Synergi.Licensing.Common
{
    [DataContract]
    public partial class Module : Record
    {
        #region Public Constructors

        public Module() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string ModuleId = "ModuleId";
            public const string Name = "Name";
            public const string Description = "Description";
            public const string Price = "Price";
        }

        #endregion

        #region Properties

        [DataMember]
        public int ModuleId { get { return Utilities.ToInt(RecordId); } set { RecordId = value; RaisePropertyChanged("ModuleId"); } }

        private string _name;
        [DataMember]
        public string Name { get { return _name; } set { if (!object.ReferenceEquals(this.Name, value)) { _name = value; RaisePropertyChanged("Name"); } } }

        private string _description;
        [DataMember]
        public string Description { get { return _description; } set { if (!object.ReferenceEquals(this.Description, value)) { _description = value; RaisePropertyChanged("Description"); } } }
        
        private decimal _price;
        [DataMember]
        public decimal Price { get { return _price; } set { if (!object.ReferenceEquals(this.Price, value)) { _price = value; RaisePropertyChanged("Price"); } } }
        #endregion

    }
}
