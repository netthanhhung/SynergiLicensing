using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Synergi.Licensing.Common
{
    [DataContract]
    public partial class ContractModule : Record
    {
        #region Public Constructors

        public ContractModule() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string ContractModuleId = "ContractModuleId";
            public const string ModuleId = "ModuleId";
            public const string ContractId = "ContractId";

        }

        #endregion

        #region Properties

        [DataMember]
        public int ContractModuleId { get { return Utilities.ToInt(RecordId); } set { RecordId = value; RaisePropertyChanged("ContractModuleId"); } }

        private int _moduleId;
        [DataMember]
        public int ModuleId { get { return _moduleId; } set { if (!object.ReferenceEquals(this.ModuleId, value)) { _moduleId = value; RaisePropertyChanged("ModuleId"); } } }

        private int _contractId;
        [DataMember]
        public int ContractId { get { return _contractId; } set { if (!object.ReferenceEquals(this.ContractId, value)) { _contractId = value; RaisePropertyChanged("ContractId"); } } }
        #endregion

    }
}
