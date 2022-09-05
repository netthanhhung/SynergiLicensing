using System;
using System.Data.SqlClient;

namespace Synergi.Licensing.Common
{
    public partial class Package
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.PackageId, NullableRecordId)
				, Utilities.MakeInputParameter(ColumnNames.Name, Name)
				, Utilities.MakeInputParameter(ColumnNames.Description, Description)
                , Utilities.MakeInputParameter(ColumnNames.Price, Price)
                , Utilities.MakeInputParameter(ColumnNames.IsLegacy, IsLegacy)
			};
        }
    }
}
