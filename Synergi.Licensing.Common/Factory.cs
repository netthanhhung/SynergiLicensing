using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synergi.Licensing.Common
{
    public static partial class Factory
    {
        private static void PopulateRecord(Record record, System.Data.IDataReader reader)
        {
            record.Concurrency = Utilities.ToByteArray(reader["Concurrency"]);
            record.DateCreated = Utilities.ToNDateTime(reader["DateCreated"]);
            record.DateUpdated = Utilities.ToNDateTime(reader["DateUpdated"]);
            record.CreatedBy = Utilities.ToString(reader["CreatedBy"]);
            record.UpdatedBy = Utilities.ToString(reader["UpdatedBy"]);
        }

        #region
        public static AspUser AspUser(System.Data.IDataReader reader)
        {
            AspUser result = null;

            if (null != reader && reader.Read())
            {
                result = new AspUser();
                PopulateAspUser(result, reader);
            }

            return result;
        }

        public static void PopulateAspUser(AspUser input, System.Data.IDataReader reader)
        {
            input.ApplicationId = Utilities.ToGuid(reader[Synergi.Licensing.Common.AspUser.ColumnNames.ApplicationId]);
            input.UserId = Utilities.ToGuid(reader[Synergi.Licensing.Common.AspUser.ColumnNames.UserId]);
            input.Password = Utilities.ToString(reader[Synergi.Licensing.Common.AspUser.ColumnNames.Password]);
            input.PasswordFormat = Utilities.ToInt(reader[Synergi.Licensing.Common.AspUser.ColumnNames.PasswordFormat]);
            input.PasswordSalt = Utilities.ToString(reader[Synergi.Licensing.Common.AspUser.ColumnNames.PasswordSalt]);
            input.MobilePIN = Utilities.ToString(reader[Synergi.Licensing.Common.AspUser.ColumnNames.MobilePIN]);
            input.Email = Utilities.ToString(reader[Synergi.Licensing.Common.AspUser.ColumnNames.Email]);

            input.Email = Utilities.ToString(reader[Synergi.Licensing.Common.AspUser.ColumnNames.Email]);
            input.LoweredEmail = Utilities.ToString(reader[Synergi.Licensing.Common.AspUser.ColumnNames.LoweredEmail]);
            input.PasswordQuestion = Utilities.ToString(reader[Synergi.Licensing.Common.AspUser.ColumnNames.PasswordQuestion]);
            input.PasswordAnswer = Utilities.ToString(reader[Synergi.Licensing.Common.AspUser.ColumnNames.PasswordAnswer]);
            input.IsApproved = Utilities.ToBool(reader[Synergi.Licensing.Common.AspUser.ColumnNames.IsApproved]);
            input.IsLockedOut = Utilities.ToBool(reader[Synergi.Licensing.Common.AspUser.ColumnNames.IsLockedOut]);
            input.CreationDate = Utilities.ToDateTime(reader[Synergi.Licensing.Common.AspUser.ColumnNames.CreateDate]);
            input.LastLoginDate = Utilities.ToDateTime(reader[Synergi.Licensing.Common.AspUser.ColumnNames.LastLoginDate]);
            input.LastPasswordChangedDate = Utilities.ToDateTime(reader[Synergi.Licensing.Common.AspUser.ColumnNames.LastPasswordChangedDate]);
            input.LastLockoutDate = Utilities.ToDateTime(reader[Synergi.Licensing.Common.AspUser.ColumnNames.LastLockoutDate]);

            input.FailedPasswordAttemptCount = Utilities.ToInt(reader[Synergi.Licensing.Common.AspUser.ColumnNames.FailedPasswordAttemptCount]);
            input.FailedPasswordAttemptWindowStart = Utilities.ToDateTime(reader[Synergi.Licensing.Common.AspUser.ColumnNames.FailedPasswordAttemptWindowStart]);
            input.FailedPasswordAnswerAttemptCount = Utilities.ToInt(reader[Synergi.Licensing.Common.AspUser.ColumnNames.FailedPasswordAnswerAttemptCount]);
            input.FailedPasswordAnswerAttemptWindowStart = Utilities.ToDateTime(reader[Synergi.Licensing.Common.AspUser.ColumnNames.FailedPasswordAnswerAttemptWindowStart]);
            input.Comment = Utilities.ToString(reader[Synergi.Licensing.Common.AspUser.ColumnNames.Comment]);
            input.UserName = Utilities.ToString(reader[Synergi.Licensing.Common.AspUser.ColumnNames.UserName]);
            input.LoweredUserName = Utilities.ToString(reader[Synergi.Licensing.Common.AspUser.ColumnNames.LoweredUserName]);
            input.MobileAlias = Utilities.ToString(reader[Synergi.Licensing.Common.AspUser.ColumnNames.MobileAlias]);

            input.IsAnonymous = Utilities.ToBool(reader[Synergi.Licensing.Common.AspUser.ColumnNames.IsAnonymous]);
            input.LastActivityDate = Utilities.ToDateTime(reader[Synergi.Licensing.Common.AspUser.ColumnNames.LastActivityDate]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillAspUserList(List<AspUser> list, System.Data.IDataReader reader)
        {
            list.Clear();
            AspUser item;
            while (true)
            {
                item = Factory.AspUser(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion
        #region ContactInformation
        public static Country Country(System.Data.IDataReader reader)
        {
            Country result = null;

            if (null != reader && reader.Read())
            {
                result = new Country();
                PopulateCountry(result, reader);
            }

            return result;
        }

        public static void PopulateCountry(Country input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.CountryId = Utilities.ToInt(reader[Synergi.Licensing.Common.Country.ColumnNames.CountryId]);
            input.Name = Utilities.ToString(reader[Synergi.Licensing.Common.Country.ColumnNames.Name]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillCountryList(List<Country> list, System.Data.IDataReader reader)
        {
            list.Clear();
            Country item;
            while (true)
            {
                item = Factory.Country(reader);
                if (null == item) break;
                list.Add(item);
            }
        }

        public static ContactInformation ContactInformation(System.Data.IDataReader reader)
        {
            ContactInformation result = null;

            if (null != reader && reader.Read())
            {
                result = new ContactInformation();
                PopulateContactInformation(result, reader);
            }

            return result;
        }

        public static void PopulateContactInformation(ContactInformation input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = Utilities.ToInt(reader[Synergi.Licensing.Common.ContactInformation.ColumnNames.ContactInformationId]);
            input.Address = Utilities.ToString(reader[Synergi.Licensing.Common.ContactInformation.ColumnNames.Address]);
            input.City = Utilities.ToString(reader[Synergi.Licensing.Common.ContactInformation.ColumnNames.City]);
            input.State = Utilities.ToString(reader[Synergi.Licensing.Common.ContactInformation.ColumnNames.State]);
            input.Postcode = Utilities.ToString(reader[Synergi.Licensing.Common.ContactInformation.ColumnNames.Postcode]);
            input.CountryId = Utilities.ToInt(reader[Synergi.Licensing.Common.ContactInformation.ColumnNames.CountryId]);
            input.PhoneNumber = Utilities.ToString(reader[Synergi.Licensing.Common.ContactInformation.ColumnNames.PhoneNumber]);
            input.FaxNumber = Utilities.ToString(reader[Synergi.Licensing.Common.ContactInformation.ColumnNames.FaxNumber]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillContactInformationList(List<ContactInformation> list, System.Data.IDataReader reader)
        {
            list.Clear();
            ContactInformation item;
            while (true)
            {
                item = Factory.ContactInformation(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region Customer
        public static Customer Customer(System.Data.IDataReader reader)
        {
            Customer result = null;

            if (null != reader && reader.Read())
            {
                result = new Customer();
                PopulateCustomer(result, reader);
            }

            return result;
        }

        public static void PopulateCustomer(Customer input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = Utilities.ToInt(reader[Synergi.Licensing.Common.Customer.ColumnNames.CustomerId]);
            input.Name = Utilities.ToString(reader[Synergi.Licensing.Common.Customer.ColumnNames.Name]);
            input.BusinessName = Utilities.ToString(reader[Synergi.Licensing.Common.Customer.ColumnNames.BusinessName]);
            input.ContactInformationId = Utilities.ToInt(reader[Synergi.Licensing.Common.Customer.ColumnNames.ContactInformationId]);
            input.ShippingContactInformationId = Utilities.ToNInt(reader[Synergi.Licensing.Common.Customer.ColumnNames.ShippingContactInformationId]);
            input.IsLegacy = Utilities.ToBool(reader[Synergi.Licensing.Common.Customer.ColumnNames.IsLegacy]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillCustomerList(List<Customer> list, System.Data.IDataReader reader)
        {
            list.Clear();
            Customer item;
            while (true)
            {
                item = Factory.Customer(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region Module
        public static Module Module(System.Data.IDataReader reader)
        {
            Module result = null;

            if (null != reader && reader.Read())
            {
                result = new Module();
                PopulateModule(result, reader);
            }

            return result;
        }

        public static void PopulateModule(Module input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = Utilities.ToInt(reader[Synergi.Licensing.Common.Module.ColumnNames.ModuleId]);
            input.Name = Utilities.ToString(reader[Synergi.Licensing.Common.Module.ColumnNames.Name]);
            input.Description = Utilities.ToString(reader[Synergi.Licensing.Common.Module.ColumnNames.Description]);
            input.Price = Utilities.ToDecimal(reader[Synergi.Licensing.Common.Module.ColumnNames.Price]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillModuleList(List<Module> list, System.Data.IDataReader reader)
        {
            list.Clear();
            Module item;
            while (true)
            {
                item = Factory.Module(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region Package
        public static Package Package(System.Data.IDataReader reader)
        {
            Package result = null;

            if (null != reader && reader.Read())
            {
                result = new Package();
                PopulatePackage(result, reader);
            }

            return result;
        }

        public static void PopulatePackage(Package input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = Utilities.ToInt(reader[Synergi.Licensing.Common.Package.ColumnNames.PackageId]);
            input.Name = Utilities.ToString(reader[Synergi.Licensing.Common.Package.ColumnNames.Name]);
            input.Description = Utilities.ToString(reader[Synergi.Licensing.Common.Package.ColumnNames.Description]);
            input.Price = Utilities.ToDecimal(reader[Synergi.Licensing.Common.Package.ColumnNames.Price]);
            input.IsLegacy = Utilities.ToBool(reader[Synergi.Licensing.Common.Package.ColumnNames.IsLegacy]);
            input.CanDelete = Utilities.ToBool(reader["CanDelete"]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillPackageList(List<Package> list, System.Data.IDataReader reader)
        {
            list.Clear();
            Package item;
            while (true)
            {
                item = Factory.Package(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region Contract
        public static Contract Contract(System.Data.IDataReader reader)
        {
            Contract result = null;

            if (null != reader && reader.Read())
            {
                result = new Contract();
                PopulateContract(result, reader);
            }

            return result;
        }

        public static void PopulateContract(Contract input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = Utilities.ToInt(reader[Synergi.Licensing.Common.Contract.ColumnNames.ContractId]);
            input.CustomerId = Utilities.ToInt(reader[Synergi.Licensing.Common.Contract.ColumnNames.CustomerId]);
            input.LicenseKey = Utilities.ToGuid(reader[Synergi.Licensing.Common.Contract.ColumnNames.LicenseKey]);
            input.Date = Utilities.ToDateTime(reader[Synergi.Licensing.Common.Contract.ColumnNames.Date]);
            input.DomainName = Utilities.ToString(reader[Synergi.Licensing.Common.Contract.ColumnNames.DomainName]);
            input.SecondDomainName = Utilities.ToString(reader[Synergi.Licensing.Common.Contract.ColumnNames.SecondDomainName]);
            input.ActivatedDate = Utilities.ToDateTime(reader[Synergi.Licensing.Common.Contract.ColumnNames.ActivatedDate]);
            input.ExpiredDate = Utilities.ToNDateTime(reader[Synergi.Licensing.Common.Contract.ColumnNames.ExpiredDate]);
            input.RealEndDate = Utilities.ToNDateTime(reader[Synergi.Licensing.Common.Contract.ColumnNames.RealEndDate]);
            input.NbrSites = Utilities.ToInt(reader[Synergi.Licensing.Common.Contract.ColumnNames.NbrSites]);
            input.Description = Utilities.ToString(reader[Synergi.Licensing.Common.Contract.ColumnNames.Description]);
            input.PackageId = Utilities.ToInt(reader[Synergi.Licensing.Common.Contract.ColumnNames.PackageId]);
            input.TotalPrice = Utilities.ToDecimal(reader[Synergi.Licensing.Common.Contract.ColumnNames.TotalPrice]);
            input.IsLegacy = Utilities.ToBool(reader[Synergi.Licensing.Common.Contract.ColumnNames.IsLegacy]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillContractList(List<Contract> list, System.Data.IDataReader reader)
        {
            list.Clear();
            Contract item;
            while (true)
            {
                item = Factory.Contract(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region CompetitorSource
        public static CompetitorSource CompetitorSource(System.Data.IDataReader reader)
        {
            CompetitorSource result = null;

            if (null != reader && reader.Read())
            {
                result = new CompetitorSource();
                PopulateCompetitorSource(result, reader);
            }

            return result;
        }

        public static void PopulateCompetitorSource(CompetitorSource input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = Utilities.ToInt(reader[Synergi.Licensing.Common.CompetitorSource.ColumnNames.CompetitorSourceId]);
            input.Name = Utilities.ToString(reader[Synergi.Licensing.Common.CompetitorSource.ColumnNames.Name]);
            input.Description = Utilities.ToString(reader[Synergi.Licensing.Common.CompetitorSource.ColumnNames.Description]);
            input.Price = Utilities.ToDecimal(reader[Synergi.Licensing.Common.CompetitorSource.ColumnNames.Price]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillCompetitorSourceList(List<CompetitorSource> list, System.Data.IDataReader reader)
        {
            list.Clear();
            CompetitorSource item;
            while (true)
            {
                item = Factory.CompetitorSource(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion


    }
}
