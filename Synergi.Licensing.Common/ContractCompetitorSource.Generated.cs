using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Synergi.Licensing.Common
{
    [DataContract]
    public partial class ContractCompetitorSource : Record
    {
        #region Public Constructors

        public ContractCompetitorSource() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string ContractCompetitorSourceId = "ContractCompetitorSourceId";
            public const string CompetitorSourceId = "CompetitorSourceId";
            public const string ContractId = "ContractId";

        }

        #endregion

        #region Properties

        [DataMember]
        public int ContractCompetitorSourceId { get { return Utilities.ToInt(RecordId); } set { RecordId = value; RaisePropertyChanged("ContractCompetitorSourceId"); } }

        private int _competitorSourceId;
        [DataMember]
        public int CompetitorSourceId { get { return _competitorSourceId; } set { if (!object.ReferenceEquals(this.CompetitorSourceId, value)) { _competitorSourceId = value; RaisePropertyChanged("CompetitorSourceId"); } } }

        private int _contractId;
        [DataMember]
        public int ContractId { get { return _contractId; } set { if (!object.ReferenceEquals(this.ContractId, value)) { _contractId = value; RaisePropertyChanged("ContractId"); } } }
        #endregion

    }
}
