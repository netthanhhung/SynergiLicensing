using System;
using System.Data.SqlClient;

namespace Synergi.Licensing.Common
{
    public partial class ContactInformation
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.ContactInformationId, NullableRecordId),
				Utilities.MakeInputParameter(ColumnNames.Address, _address),
				Utilities.MakeInputParameter(ColumnNames.City, _city),
				Utilities.MakeInputParameter(ColumnNames.State, _state),
				Utilities.MakeInputParameter(ColumnNames.Postcode, _postcode),
				Utilities.MakeInputParameter(ColumnNames.CountryId, _countryId),
				Utilities.MakeInputParameter(ColumnNames.PhoneNumber, _phoneNumber),
				Utilities.MakeInputParameter(ColumnNames.FaxNumber, _faxNumber)
			};
        }
    }
}
