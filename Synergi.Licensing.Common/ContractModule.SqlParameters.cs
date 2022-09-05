using System;
using System.Data.SqlClient;

namespace Synergi.Licensing.Common
{
    public partial class ContractModule
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.ContractModuleId, NullableRecordId)
				, Utilities.MakeInputParameter(ColumnNames.ModuleId, ModuleId)
				, Utilities.MakeInputParameter(ColumnNames.ContractId, ContractId)

			};
        }
    }
}
