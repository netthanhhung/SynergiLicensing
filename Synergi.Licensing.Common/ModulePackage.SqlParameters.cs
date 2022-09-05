using System;
using System.Data.SqlClient;

namespace Synergi.Licensing.Common
{
    public partial class ModulePackage
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.ModulePackageId, NullableRecordId)
				, Utilities.MakeInputParameter(ColumnNames.ModuleId, ModuleId)
				, Utilities.MakeInputParameter(ColumnNames.PackageId, PackageId)

			};
        }
    }
}
