using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using Synergi.Licensing.Common;
using System.Data;
using System.Data.Common;

namespace Synergi.Licensing.Data
{
    public sealed partial class DataLayer
    {
        #region Private Attributes
        private Database _db;
        private const int _extendedTimeout = 5400;

        #endregion

        #region Public Constructors
        public DataLayer()
        {
            //_db = DatabaseFactory.CreateDatabase();
            _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("SynergiLicensing");
        }
        #endregion

        #region Common
        private void AddParameters(System.Data.Common.DbCommand command, SqlParameter[] sqlParams)
        {
            foreach (SqlParameter param in sqlParams)
            {
                _db.AddParameter(command, param.ParameterName, param.DbType, param.Size, param.Direction, param.IsNullable, param.Precision, param.Scale, param.SourceColumn, param.SourceVersion, param.Value);
            }
        }

        public void SaveRecord(Record record)
        {
            SqlParameter[] sqlParams = record.SqlParameters();
            System.Data.Common.DbCommand cmd = _db.GetStoredProcCommand("procSave" + record.TypeName);
            AddParameters(cmd, sqlParams);
            record.Concurrency = Utilities.ToByteArray(_db.ExecuteScalar(cmd));
            record.RecordId = (long)_db.GetParameterValue(cmd, record.TypeName + "ID");
        }

        public void SaveRecord(Record record, string currentUser)
        {
            SqlParameter[] sqlParams = record.SqlParameters();
            System.Data.Common.DbCommand cmd = _db.GetStoredProcCommand("procSave" + record.TypeName);
            AddParameters(cmd, sqlParams);
            AddParameters(cmd, new SqlParameter[] { new SqlParameter("CurrentUser", currentUser) });
            record.Concurrency = Utilities.ToByteArray(_db.ExecuteScalar(cmd));
            record.RecordId = (long)_db.GetParameterValue(cmd, record.TypeName + "ID");
        }

        public void SaveRecord(Record record, string currentUser, string procName)
        {
            SqlParameter[] sqlParams = record.SqlParameters();
            System.Data.Common.DbCommand cmd = _db.GetStoredProcCommand(procName);
            AddParameters(cmd, sqlParams);
            AddParameters(cmd, new SqlParameter[] { new SqlParameter("CurrentUser", currentUser) });
            record.Concurrency = Utilities.ToByteArray(_db.ExecuteScalar(cmd));
            record.RecordId = (long)_db.GetParameterValue(cmd, record.TypeName + "ID");
        }

        public void SaveRecordExtended(Record record, string procName)
        {
            SqlParameter[] sqlParams = record.SqlParameters();
            System.Data.Common.DbCommand cmd = _db.GetStoredProcCommand(procName);
            AddParameters(cmd, sqlParams);
            _db.ExecuteScalar(cmd);
        }
        
        private DbCommand GetDbCommandWithExtendedTimeout(string storedProcedureName, params object[] parameterValues)
        {
            DbCommand result = this._db.GetStoredProcCommand(storedProcedureName, parameterValues);
            result.CommandTimeout = _extendedTimeout;

            return result;
        }

        private int ExecuteNonQueryWithExtendedTimeout(string storedProcedureName, params object[] parameterValues)
        {
            int result = 0;

            using (DbCommand cmd = this._db.GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                cmd.CommandTimeout = _extendedTimeout;
                result = _db.ExecuteNonQuery(cmd);
            }

            return result;
        }

        public void DeleteRecord(long recordId, string recordType)
        {
            _db.ExecuteNonQuery("procDelete", recordId, recordType);
        }
        #endregion

        #region UserAccount
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<string> ListUserName(string applicationName, string startsWith)
        {
            List<string> result = new List<string>();

            using (IDataReader reader = _db.ExecuteReader("procListUserName", applicationName, startsWith))
            {
                result.Clear();

                while (true)
                {
                    string userName = null;

                    if (null != reader && reader.Read())
                    {
                        userName = Utilities.ToString(reader["UserName"]);
                    }

                    if (string.IsNullOrEmpty(userName))
                    {
                        break;
                    }

                    result.Add(userName);
                }
            }

            return result;
        }

        public int? UpdateMemberhipUserName(string applicationName, string userName, string newUserName)
        {
            int? result = null;

            SqlParameter[] sqlParams = new SqlParameter[]
			{ 
				Utilities.MakeInputParameter("ApplicationName", applicationName),
				Utilities.MakeInputParameter("UserName", userName),
				Utilities.MakeInputParameter("NewUserName", newUserName)
			};

            System.Data.Common.DbCommand cmd = _db.GetStoredProcCommand("aspnet_Membership_UpdateUserName_Custom");
            AddParameters(cmd, sqlParams);

            result = Utilities.ToNInt(_db.ExecuteScalar(cmd));

            return result;
        }
        #endregion

        #region AspUser

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<AspUser> ListAspUser(Guid? userId)
        {
            List<AspUser> result = new List<AspUser>();
            using (IDataReader reader = _db.ExecuteReader("procListAspUser", userId))
            {
                Factory.FillAspUserList(result, reader);
            }
            return result;
        }        
        #endregion
        

        #region Contact Information
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Country> ListCountry(int? countryId)
        {
            List<Country> result = new List<Country>();

            using (IDataReader reader = _db.ExecuteReader("procListCountry", countryId))
            {
                Factory.FillCountryList(result, reader);
            }

            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<ContactInformation> ListContactInformation(int? contactInfoId)
        {
            List<ContactInformation> result = new List<ContactInformation>();

            using (IDataReader reader = _db.ExecuteReader("procListContactInformation", contactInfoId))
            {
                Factory.FillContactInformationList(result, reader);
            }

            return result;
        }
        #endregion

        #region Customer
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Customer> ListCustomer(int? customerId, string name, bool includeLegacy)
        {
            List<Customer> result = new List<Customer>();

            using (IDataReader reader = _db.ExecuteReader("procListCustomer", customerId, name, includeLegacy))
            {
                Factory.FillCustomerList(result, reader);
            }

            return result;
        }
        #endregion

        #region Module
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Module> ListModule(int? moduleId)
        {
            List<Module> result = new List<Module>();

            using (IDataReader reader = _db.ExecuteReader("procListModule", moduleId))
            {
                Factory.FillModuleList(result, reader);
            }

            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Module> ListModulesOfPackage(int packageId)
        {
            List<Module> result = new List<Module>();

            using (IDataReader reader = _db.ExecuteReader("procListModulesOfPackage", packageId))
            {
                Factory.FillModuleList(result, reader);
            }

            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Module> ListAdditionalContractModules(int contractId)
        {
            List<Module> result = new List<Module>();

            using (IDataReader reader = _db.ExecuteReader("procListAdditionalContractModules", contractId))
            {
                Factory.FillModuleList(result, reader);
            }

            return result;
        }        
        #endregion

        #region Package
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Package> ListPackage(int? packageId, bool includeLegacy)
        {
            List<Package> result = new List<Package>();

            using (IDataReader reader = _db.ExecuteReader("procListPackage", packageId, includeLegacy))
            {
                Factory.FillPackageList(result, reader);
            }

            return result;
        }
        #endregion

        #region ModuldePackage
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public void DeleteModulePackage(int packageId, int moduleId)
        {
            _db.ExecuteNonQuery("procDeleteModulePackage", packageId, moduleId);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public void SaveModulePackage(int packageId, int moduleId, string currentUser)
        {
			System.Data.Common.DbCommand cmd = _db.GetStoredProcCommand("procSaveModulePackage");
			AddParameters(cmd, new SqlParameter[] { new SqlParameter("PackageId", packageId) });
            AddParameters(cmd, new SqlParameter[] { new SqlParameter("ModuleId", moduleId) });
			AddParameters(cmd, new SqlParameter[] { new SqlParameter("CurrentUser", currentUser) });
			_db.ExecuteScalar(cmd);
        }
        #endregion

        #region Contract
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Contract> ListContract(int? contractId, int? customerId, Guid? licenseKey, string domainName, 
            string customerName, int? packageId, bool includeLegacy)
        {
            List<Contract> result = new List<Contract>();

            using (IDataReader reader = _db.ExecuteReader("procListContract", contractId, customerId, licenseKey,
                domainName, customerName, packageId, includeLegacy))
            {
                Factory.FillContractList(result, reader);
            }

            return result;
        }
        #endregion

        #region ContractModulde
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public void DeleteContractModule(int contractId, int moduleId)
        {
            _db.ExecuteNonQuery("procDeleteContractModule", contractId, moduleId);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public void SaveContractModule(int contractId, int moduleId, string currentUser)
        {
            System.Data.Common.DbCommand cmd = _db.GetStoredProcCommand("procSaveContractModule");
            AddParameters(cmd, new SqlParameter[] { new SqlParameter("ContractId", contractId) });
            AddParameters(cmd, new SqlParameter[] { new SqlParameter("ModuleId", moduleId) });
            AddParameters(cmd, new SqlParameter[] { new SqlParameter("CurrentUser", currentUser) });
            _db.ExecuteScalar(cmd);
        }
        #endregion

        #region CompetitorSource
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<CompetitorSource> ListCompetitorSource(int? competitorSourceId)
        {
            List<CompetitorSource> result = new List<CompetitorSource>();

            using (IDataReader reader = _db.ExecuteReader("procListCompetitorSource", competitorSourceId))
            {
                Factory.FillCompetitorSourceList(result, reader);
            }

            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<CompetitorSource> ListContractCompetitorSources(int contractId)
        {
            List<CompetitorSource> result = new List<CompetitorSource>();

            using (IDataReader reader = _db.ExecuteReader("procListContractCompetitorSources", contractId))
            {
                Factory.FillCompetitorSourceList(result, reader);
            }

            return result;
        }
        #endregion

        #region ContractModulde
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public void DeleteContractCompetitorSource(int contractId, int moduleId)
        {
            _db.ExecuteNonQuery("procDeleteContractCompetitorSource", contractId, moduleId);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public void SaveContractCompetitorSource(int contractId, int moduleId, string currentUser)
        {
            System.Data.Common.DbCommand cmd = _db.GetStoredProcCommand("procSaveContractCompetitorSource");
            AddParameters(cmd, new SqlParameter[] { new SqlParameter("ContractId", contractId) });
            AddParameters(cmd, new SqlParameter[] { new SqlParameter("CompetitorSourceId", moduleId) });
            AddParameters(cmd, new SqlParameter[] { new SqlParameter("CurrentUser", currentUser) });
            _db.ExecuteScalar(cmd);
        }
        #endregion
    }
}
