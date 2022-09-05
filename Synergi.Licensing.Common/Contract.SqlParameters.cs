using System;
using System.Data.SqlClient;

namespace Synergi.Licensing.Common
{
    public partial class Contract
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.ContractId, NullableRecordId)
				, Utilities.MakeInputParameter(ColumnNames.CustomerId, CustomerId)
				, Utilities.MakeInputParameter(ColumnNames.LicenseKey, LicenseKey)
				, Utilities.MakeInputParameter(ColumnNames.Date, Date)
				, Utilities.MakeInputParameter(ColumnNames.DomainName, DomainName)
                , Utilities.MakeInputParameter(ColumnNames.SecondDomainName, SecondDomainName)
				, Utilities.MakeInputParameter(ColumnNames.ActivatedDate, ActivatedDate)
                , Utilities.MakeInputParameter(ColumnNames.ExpiredDate, ExpiredDate)
                , Utilities.MakeInputParameter(ColumnNames.RealEndDate, RealEndDate)
                , Utilities.MakeInputParameter(ColumnNames.NbrSites, NbrSites)
                , Utilities.MakeInputParameter(ColumnNames.Description, Description)
                , Utilities.MakeInputParameter(ColumnNames.PackageId, PackageId)
                , Utilities.MakeInputParameter(ColumnNames.TotalPrice, TotalPrice)
                , Utilities.MakeInputParameter(ColumnNames.IsLegacy, IsLegacy)
			};
        }
    }
}
