using System;
using System.Data.SqlClient;

namespace Synergi.Licensing.Common
{
    public partial class ContractCompetitorSource
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.ContractCompetitorSourceId, NullableRecordId)
				, Utilities.MakeInputParameter(ColumnNames.CompetitorSourceId, CompetitorSourceId)
				, Utilities.MakeInputParameter(ColumnNames.ContractId, ContractId)

			};
        }
    }
}
