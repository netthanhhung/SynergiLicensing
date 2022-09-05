using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Synergi.Licensing.Data;
using Synergi.Licensing.Common;
using System.Security.Cryptography;

namespace Synergi.Licensing.Business
{
    public static class SynergiMethods
    {
        #region Common
        /// <summary>
        /// Calls the data layer to save the record object.
        /// The record's RecordId is used to determine if the save operation is an insert or an update.
        /// </summary>
        /// <param name="record">The record object containing the data to be saved.</param>
        public static void SaveRecord(Record record)
        {
            new DataLayer().SaveRecord(record, (record.NullableRecordId == null) ? record.CreatedBy : record.UpdatedBy);
        }       

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public static void DeleteRecord(Record record)
        {
            DataLayer dl = new DataLayer();
            String type = record.GetType().ToString();
            dl.DeleteRecord(Utilities.ToLong(record.RecordId), type.Substring(type.LastIndexOf(".") + 1, (type.Length - (type.LastIndexOf(".") + 1))));
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public static void DeleteRecord(long id, Type recordType)
        {
            DataLayer temp = new DataLayer();
            String type = recordType.ToString();
            temp.DeleteRecord(id, type.Substring(type.LastIndexOf(".") + 1, (type.Length - (type.LastIndexOf(".") + 1))));
        }
       
        #endregion

        #region UserAccount
        public static List<AspUser> ListAspUser(Guid? userId)
        {
            return new DataLayer().ListAspUser(userId);
        }

        public static List<string> ListUserName(string applicationName, string startsWith)
        {
            return new DataLayer().ListUserName(applicationName, startsWith);
        }

        public static int? UpdateMembershipUserName(string applicationName, string userName, string newUserName)
        {
            return new DataLayer().UpdateMemberhipUserName(applicationName, userName, newUserName);
        }
        #endregion

        #region Contact Information
        public static List<Country> ListCountry(int? countryId)
        {
            return new DataLayer().ListCountry(countryId);
        }

        public static List<ContactInformation> ListContactInformation(int? contactInfoId)
        {            
            return new DataLayer().ListContactInformation(contactInfoId);
        }

        public static void SaveContactInformation(List<ContactInformation> saveList)
        {
            if (saveList != null)
            {
                foreach (ContactInformation item in saveList)
                {
                    if (item.IsDeleted && item.NullableRecordId != null)
                    {
                        DeleteRecord((Record)item);
                    }
                    else if (item.IsChanged)
                    {
                        SaveRecord((Record)item);
                    }
                }
            }
        }
        #endregion

        #region Customer
        public static List<Customer> ListCustomer(int? customerId, string name, bool includeLegacy)
        {
            DataLayer dl = new DataLayer();
            List<Customer> result = dl.ListCustomer(customerId, name, includeLegacy);
            if (result != null)
            {
                foreach (Customer cus in result)
                {
                    cus.ContactInformation = dl.ListContactInformation(cus.ContactInformationId).FirstOrDefault();
                    if(cus.ShippingContactInformationId.HasValue)
                        cus.ShippingContactInformation = dl.ListContactInformation(cus.ShippingContactInformationId).FirstOrDefault();
                    cus.Contracts = ListContract(null, cus.CustomerId, null, null, null, null, includeLegacy, false);
                }
            }
            return result;
        }

        public static void SaveCustomer(List<Customer> saveList)
        {
            if (saveList != null)
            {
                foreach (Customer item in saveList)
                {
                    if (item.IsDeleted && item.NullableRecordId != null)
                    {
                        if (item.ContactInformation != null)
                        {
                            DeleteRecord((Record)item.ContactInformation);
                        }
                        DeleteRecord((Record)item);
                    }
                    else if (item.IsChanged || (item.ContactInformation != null && item.ContactInformation.IsChanged))
                    {
                        if (item.ContactInformation != null && item.ContactInformation.IsChanged)
                        {
                            SaveRecord((Record)item.ContactInformation);
                            item.ContactInformationId = item.ContactInformation.ContactInformationId;
                        }
                        SaveRecord((Record)item);
                    }
                }
            }
        }
        #endregion

        #region Module
        public static List<Module> ListModule(int? moduleId)
        {
            return new DataLayer().ListModule(moduleId);
        }

        public static List<Module> ListModulesOfPackage(int packageId)
        {
            return new DataLayer().ListModulesOfPackage(packageId);
        }

        public static List<Module> ListAdditionalContractModules(int contractId)
        {
            return new DataLayer().ListAdditionalContractModules(contractId);
        }

        public static void SaveModule(List<Module> saveList)
        {
            if (saveList != null)
            {
                foreach (Module item in saveList)
                {
                    if (item.IsDeleted && item.NullableRecordId != null)
                    {
                        DeleteRecord((Record)item);
                    }
                    else if (item.IsChanged)
                    {
                        SaveRecord((Record)item);
                    }
                }
            }
        }
        #endregion

        #region Package
        public static List<Package> ListPackage(int? packageId, bool includeLegacy)
        {
            DataLayer dl = new DataLayer();
            List<Package> result = dl.ListPackage(packageId, includeLegacy);
            if (result != null)
            {
                foreach (Package pkg in result)
                    pkg.Modules = dl.ListModulesOfPackage(pkg.PackageId);
            }
            return result;
        }

        public static void SavePackage(List<Package> saveList)
        {
            DataLayer dataLayer = new DataLayer();
            if (saveList != null)
            {
                foreach (Package item in saveList)
                {
                    if (item.IsDeleted && item.NullableRecordId != null && item.CanDelete)
                    {
                        if (item.Modules != null && item.Modules.Count > 0)
                        {
                            foreach (Module mod in item.Modules)
                                dataLayer.DeleteModulePackage(item.PackageId, mod.ModuleId);
                        }
                        DeleteRecord((Record)item);
                    }
                    else if (item.IsChanged)
                    {
                        SaveRecord((Record)item);
                        List<Module> oldList = dataLayer.ListModulesOfPackage(item.PackageId);
                        //Delete if not found in new list
                        foreach (Module mod in oldList)
                        {
                            if (item.Modules == null || item.Modules.Count(i => i.ModuleId == mod.ModuleId) == 0)
                                dataLayer.DeleteModulePackage(item.PackageId, mod.ModuleId);
                        }
                        //Insert if not found in old list
                        if (item.Modules != null)
                        {
                            foreach (Module mod in item.Modules)
                            {
                                if (oldList.Count(i => i.ModuleId == mod.ModuleId) == 0)
                                    dataLayer.SaveModulePackage(item.PackageId, mod.ModuleId, item.UpdatedBy);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Contract
        public static List<Contract> ListContract(int? contractId, int? customerId, Guid? licenseKey, string domainName,
            string customerName, int? packageId, bool includeLegacy, bool listCustomer)
        {
            DataLayer dl = new DataLayer();
            List<Contract> result = dl.ListContract(contractId, customerId, licenseKey, domainName, customerName, packageId, includeLegacy);
            if (result != null)
            {
                foreach (Contract ct in result)
                {
                    if(listCustomer)
                        ct.Customer = dl.ListCustomer(ct.CustomerId, null, true).FirstOrDefault();
                    ct.Package = ListPackage(ct.PackageId, true).FirstOrDefault();
                    ct.AdditionalModules = dl.ListAdditionalContractModules(ct.ContractId);
                    ct.CompetitorSources = dl.ListContractCompetitorSources(ct.ContractId);
                    ct.EncryptedString = EncryptContractInfo(ct);
                }
            }
            return result;
        }

        public static void SaveContracts(List<Contract> saveList)
        {
            DataLayer dataLayer = new DataLayer();
            if (saveList != null)
            {
                foreach (Contract item in saveList)
                {
                    //Not allow to delete contract, just set IsLegacy = 0
                    if (item.IsChanged)
                    {
                        SaveRecord((Record)item);
                        //Module
                        List<Module> oldModuleList = dataLayer.ListAdditionalContractModules(item.ContractId);
                        //Delete if not found in new list
                        foreach (Module mod in oldModuleList)
                        {
                            if (item.AdditionalModules == null || item.AdditionalModules.Count(i => i.ModuleId == mod.ModuleId) == 0)
                                dataLayer.DeleteContractModule(item.ContractId, mod.ModuleId);
                        }
                        //Insert if not found in old list
                        if (item.AdditionalModules != null)
                        {
                            foreach (Module mod in item.AdditionalModules)
                            {
                                if (oldModuleList.Count(i => i.ModuleId == mod.ModuleId) == 0)
                                    dataLayer.SaveContractModule(item.ContractId, mod.ModuleId, item.UpdatedBy);
                            }
                        }

                        //CompetitorSource
                        List<CompetitorSource> oldCompSourceList = dataLayer.ListContractCompetitorSources(item.ContractId);
                        //Delete if not found in new list
                        foreach (CompetitorSource mod in oldCompSourceList)
                        {
                            if (item.CompetitorSources == null || item.CompetitorSources.Count(i => i.CompetitorSourceId == mod.CompetitorSourceId) == 0)
                                dataLayer.DeleteContractCompetitorSource(item.ContractId, mod.CompetitorSourceId);
                        }
                        //Insert if not found in old list
                        if (item.CompetitorSources != null)
                        {
                            foreach (CompetitorSource mod in item.CompetitorSources)
                            {
                                if (oldCompSourceList.Count(i => i.CompetitorSourceId == mod.CompetitorSourceId) == 0)
                                    dataLayer.SaveContractCompetitorSource(item.ContractId, mod.CompetitorSourceId, item.UpdatedBy);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region CompetitorSource
        public static List<CompetitorSource> ListCompetitorSource(int? competitorSourceId)
        {
            return new DataLayer().ListCompetitorSource(competitorSourceId);
        }

        public static List<CompetitorSource> ListContractCompetitorSources(int contractId)
        {
            return new DataLayer().ListContractCompetitorSources(contractId);
        }

        public static void SaveCompetitorSource(List<CompetitorSource> saveList)
        {
            if (saveList != null)
            {
                foreach (CompetitorSource item in saveList)
                {
                    if (item.IsDeleted && item.NullableRecordId != null)
                    {
                        DeleteRecord((Record)item);
                    }
                    else if (item.IsChanged)
                    {
                        SaveRecord((Record)item);
                    }
                }
            }
        }
        #endregion

        #region Encrypt/Decrypt        
        /// <summary>
        /// Encrypt the given string using the specified key.
        /// </summary>
        /// <param name="strToEncrypt">The string to be encrypted.</param>
        /// <param name="strKey">The encryption key.</param>
        /// <returns>The encrypted string.</returns>
        public static string Encrypt(string strToEncrypt, string strKey)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto =
                    new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = strKey;
                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = ASCIIEncoding.ASCII.GetBytes(strToEncrypt);
                return Convert.ToBase64String(objDESCrypto.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }

        /// <summary>
        /// Decrypt the given string using the specified key.
        /// </summary>
        /// <param name="strEncrypted">The string to be decrypted.</param>
        /// <param name="strKey">The decryption key.</param>
        /// <returns>The decrypted string.</returns>
        public static string Decrypt(string strEncrypted, string strKey)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = strKey;
                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = Convert.FromBase64String(strEncrypted);
                string strDecrypted = ASCIIEncoding.ASCII.GetString(
                    objDESCrypto.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
                objDESCrypto = null;
                return strDecrypted;
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }

        public static string EncryptContractInfo(Contract contract)
        {
            string contractInfo = "Domain={0}&SecondDomain={1}&ActivatedDate={2}&ExpiredDate={3}&RealEndDate={4}&NbrSites={5}&Modules={6}&CompetitorSources={7}";
            string result = string.Empty;
            if (contract != null)
            {
                string licenseKey = contract.LicenseKey.ToString();
                DateTime expiredDate = contract.ExpiredDate.HasValue ? contract.ExpiredDate.Value : DateTime.MaxValue;
                DateTime realEndDate = contract.RealEndDate.HasValue ? contract.RealEndDate.Value : DateTime.MaxValue;
                List<int> modules = new List<int>();
                if (contract.Package != null && contract.Package.Modules != null)
                    modules = (from m in contract.Package.Modules
                               select m.ModuleId).ToList();
                if (contract.AdditionalModules != null)
                {
                    foreach (Module mod in contract.AdditionalModules)
                    {
                        if (!modules.Contains(mod.ModuleId))
                            modules.Add(mod.ModuleId);
                    }
                }
                string moduleStr = string.Empty;
                foreach (int item in modules)
                {
                    moduleStr += item + ",";
                }
                if (!string.IsNullOrEmpty(moduleStr))
                {
                    moduleStr = moduleStr.Substring(0, moduleStr.Length - 1);
                }
                string competitorSourceStr = string.Empty;
                if (contract.CompetitorSources != null)
                {
                    foreach (CompetitorSource compSrc in contract.CompetitorSources)
                    {
                        competitorSourceStr += compSrc.CompetitorSourceId + ",";
                    }
                }
                if (!string.IsNullOrEmpty(competitorSourceStr))
                {
                    competitorSourceStr = competitorSourceStr.Substring(0, competitorSourceStr.Length - 1);
                }
                contractInfo = string.Format(contractInfo, contract.DomainName.Trim(), contract.SecondDomainName.Trim(),
                                        contract.ActivatedDate.ToString("dd/MM/yyyy"),
                                        expiredDate.ToString("dd/MM/yyyy"),
                                        realEndDate.ToString("dd/MM/yyyy"),
                                        contract.NbrSites,
                                        moduleStr,
                                        competitorSourceStr);

                //Start to encrypt the contrac info :
                result = Encrypt(contractInfo, licenseKey);
                string startExStr = Encrypt(DateTime.Now.ToString("dd/MM/yyyy hh:mm"), licenseKey);
                string endExStr = Encrypt(DateTime.Now.ToString("hh:mm dd/MM/yyyy"), licenseKey);
                startExStr = startExStr.Substring(0, 10);
                endExStr = endExStr.Substring(0, 10);
                result = startExStr + result + endExStr;

                //string decryptedStr = result.Substring(10, result.Length - 20);
                //decryptedStr = Decrypt(decryptedStr, licenseKey);
                //string[] parseStr = decryptedStr.Split('&');
            }
            return result;
        }

        public static string GetEncryptedLicenseInfo(Guid licenseKey)
        {
            List<Contract> contracts = ListContract(null, null, licenseKey, null, null, null, false, false);
            if (contracts.Count > 0)
                return contracts[0].EncryptedString;
            return string.Empty;
        }
        #endregion
    }
}
