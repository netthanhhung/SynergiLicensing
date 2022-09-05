using System;
using System.Data.SqlClient;

namespace Synergi.Licensing.Common
{
    public partial class Customer
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.CustomerId, NullableRecordId)
				, Utilities.MakeInputParameter(ColumnNames.Name, Name)
				, Utilities.MakeInputParameter(ColumnNames.BusinessName, BusinessName)
				, Utilities.MakeInputParameter(ColumnNames.ContactInformationId, ContactInformationId)
				, Utilities.MakeInputParameter(ColumnNames.ShippingContactInformationId, ShippingContactInformationId)
				, Utilities.MakeInputParameter(ColumnNames.IsLegacy, IsLegacy)

			};
        }
    }
}
