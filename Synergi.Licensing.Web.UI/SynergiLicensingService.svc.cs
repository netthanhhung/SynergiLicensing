using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.Web.Security;
using System.Web;
using Synergi.Licensing.Common;
using Synergi.Licensing.Business;

namespace Synergi.Licensing.Web.UI
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SynergiLicensingService
    {
        #region Common
        [OperationContract]
        public string SelectSessionId()
        {
            MembershipUser user = Membership.GetUser();
            string result = (user == null ? string.Empty : HttpContext.Current.Session.SessionID);

            return result;
        }

        [OperationContract]
        public GlobalVariable GetGlobalVariable()
        {
            GlobalVariable result = new GlobalVariable();

            result.AppSettings = GetAppSettings();
            result.UserLogin = GetUserLogin();
            result.ApplicationCurrentDateTime = DateTime.Now;

            return result;
        }

        [OperationContract]
        public AppSettings GetAppSettings()
        {
            AppSettings appSettings = new AppSettings();

            return appSettings;
        }

        [OperationContract]
        public UserLogin GetUserLogin()
        {
            MembershipUser loginUser = Membership.GetUser();
            UserLogin settings = new UserLogin();
            settings.UserName = loginUser.UserName;
            settings.UserUserId = Utilities.ToGuid(loginUser.ProviderUserKey);
            
            //settings.ActiveModules = Role.ListActiveModules();
            settings.AspUser = GetAspUser(settings.UserUserId);
            settings.UserCustomerId = 123; //TODO :

            return settings;
        }

        [OperationContract]
        public AspUser GetAspUser(Guid userId)
        {
            List<AspUser> result = SynergiMethods.ListAspUser(userId);
            if (result != null && result.Count > 0)
                return result[0];
            return null;
        }

        [OperationContract]
        public List<AspUser> ListAspUser(Guid? userId)
        {            
            return SynergiMethods.ListAspUser(userId);
        }

        [OperationContract]
        public AspUser UnlockAspUser(AspUser oldUser)
        {
            if (oldUser != null)
            {
                MembershipUser memberShipUser = Membership.GetUser(oldUser.UserId);
                memberShipUser.UnlockUser();
                oldUser = GetAspUser(oldUser.UserId);
            }
            return oldUser;
        }

        [OperationContract]
        public AspUser SaveAspUser(AspUser saveUser)
        {
            if (saveUser != null)
            {
                MembershipProvider simpleProvider = Membership.Providers["SimpleProvider"];

                if (saveUser.UserId == Guid.Empty) //means this is new user : create user
                {
                    // Insert New Membership Account
                    MembershipCreateStatus status;
                    MembershipUser newUser = Membership.CreateUser(saveUser.UserName, saveUser.Password, saveUser.Email,
                            saveUser.PasswordQuestion, saveUser.PasswordAnswer, saveUser.IsApproved, out status);

                    if (status == MembershipCreateStatus.Success)
                    {
                        Guid newUserId = Utilities.ToGuid(newUser.ProviderUserKey);
                        saveUser = GetAspUser(newUserId);
                    }
                    else
                    {
                        switch (status)
                        {
                            case MembershipCreateStatus.DuplicateEmail:
                                saveUser.ErrorMessage = "The e-mail address already exists in the database for the application."; break;
                            case MembershipCreateStatus.DuplicateProviderUserKey:
                                saveUser.ErrorMessage = "The provider user key already exists in the database for the application."; break;
                            case MembershipCreateStatus.DuplicateUserName:
                                saveUser.ErrorMessage = "The user name already exists in the database for the application."; break;
                            case MembershipCreateStatus.InvalidAnswer:
                                saveUser.ErrorMessage = "The password answer is not formatted correctly."; break;
                            case MembershipCreateStatus.InvalidEmail:
                                saveUser.ErrorMessage = "The e-mail address is not formatted correctly."; break;
                            case MembershipCreateStatus.InvalidProviderUserKey:
                                saveUser.ErrorMessage = "The provider user key is of an invalid type or format."; break;
                            case MembershipCreateStatus.InvalidQuestion:
                                saveUser.ErrorMessage = "The password question is not formatted correctly."; break;
                            case MembershipCreateStatus.InvalidUserName:
                                saveUser.ErrorMessage = "The user name was not found in the database."; break;
                            case MembershipCreateStatus.InvalidPassword:
                                saveUser.ErrorMessage = "The password is not formatted correctly."; break;
                            default:
                                saveUser.ErrorMessage = "Fail to create new user";
                                break;

                        }

                    }
                }
                else
                {
                    MembershipUser memberShipUser = Membership.GetUser(saveUser.UserId);
                    int? updateCode = null;
                    if (memberShipUser.UserName != saveUser.UserName)
                    {
                        updateCode = SynergiMethods.UpdateMembershipUserName(Membership.ApplicationName, memberShipUser.UserName, saveUser.UserName);
                        memberShipUser = Membership.GetUser(saveUser.UserId);
                    }

                    string newGenPassword = string.Empty;
                    bool saveQAerror = false;
                    if (updateCode == null || updateCode == 0)
                    {
                        memberShipUser.Email = saveUser.Email;
                        memberShipUser.IsApproved = saveUser.IsApproved;
                        Membership.UpdateUser(memberShipUser);

                        if (!string.IsNullOrEmpty(saveUser.PasswordQuestion) && !string.IsNullOrEmpty(saveUser.PasswordAnswer))
                        {
                            saveQAerror = !memberShipUser.ChangePasswordQuestionAndAnswer(saveUser.InputPassword, saveUser.PasswordQuestion, saveUser.PasswordAnswer);
                        }

                        if (saveUser.IsResetPassword)
                        {
                            if (simpleProvider != null)
                            {
                                MembershipUser simpleUser = simpleProvider.GetUser(saveUser.UserId, false);
                                if (simpleUser != null)
                                {
                                    if (saveUser.IsResetPassword)
                                    {
                                        newGenPassword = simpleUser.ResetPassword();
                                    }
                                }
                            }
                        }
                    }
                    saveUser = GetAspUser(saveUser.UserId);
                    saveUser.NewGenPassword = newGenPassword;
                    saveUser.IsSavedQAError = saveQAerror;
                }
            }

            return saveUser;
        }
        
        private AspUser ConvertUser(MembershipUser membership)
        {
            AspUser user = new AspUser();
            if (membership == null)
                return null;
            user.UserId = Utilities.ToGuid(membership.ProviderUserKey);
            user.UserName = membership.UserName;
            user.IsApproved = membership.IsApproved;
            user.IsLockedOut = membership.IsLockedOut;
            user.IsOnline = membership.IsOnline;
            user.Comment = membership.Comment;
            user.CreationDate = membership.CreationDate;
            user.Email = membership.Email;
            user.LastActivityDate = membership.LastActivityDate;
            user.LastLockoutDate = membership.LastLockoutDate;
            user.LastLoginDate = membership.LastLoginDate;
            user.LastPasswordChangedDate = membership.LastPasswordChangedDate;
            user.PasswordQuestion = membership.PasswordQuestion;
            user.ProviderName = membership.ProviderName;
            user.ProviderUserKey = membership.ProviderUserKey;
            user.Password = "nothing";
            return user;
        }

        [OperationContract]
        public int ChangePassword(string oldPassword, string newPassword)
        {
            MembershipUser user = Membership.GetUser(HttpContext.Current.User.Identity.Name);

            try
            {
                if (user.ChangePassword(oldPassword, newPassword))
                    return 0; // Password changed
                else
                    return 1; // Password change failed. Please re-enter your values and try again
            }
            catch
            {
                return 3; // An exception occurred. Please re-enter your values and try again
            }
        }
        #endregion

        #region Contact Information
        [OperationContract]
        public List<ContactInformation> ListContactInformation(int? contactInfoId)
        {
            return SynergiMethods.ListContactInformation(contactInfoId);
        }

        [OperationContract]
        public void SaveContactInformation(List<ContactInformation> saveList)
        {
            SynergiMethods.SaveContactInformation(saveList);
        }
        #endregion

        #region Customer
        [OperationContract]
        public List<Customer> ListCustomer(int? customerId, string name, bool includeLegacy)
        {
            return SynergiMethods.ListCustomer(customerId, name, includeLegacy);
        }

        [OperationContract]
        public void SaveCustomer(List<Customer> saveList)
        {
            SynergiMethods.SaveCustomer(saveList);
        }
        #endregion

        #region Module
        [OperationContract]
        public List<Module> ListModule(int? moduleId)
        {
            return SynergiMethods.ListModule(moduleId);
        }

        [OperationContract]
        public List<Module> ListModulesOfPackage(int packageId)
        {
            return SynergiMethods.ListModulesOfPackage(packageId);
        }

        [OperationContract]
        public List<Module> ListAdditionalContractModules(int contractId)
        {
            return SynergiMethods.ListAdditionalContractModules(contractId);
        }

        [OperationContract]
        public void SaveModule(List<Module> saveList)
        {
            SynergiMethods.SaveModule(saveList);
        }
        #endregion

        #region Package
        [OperationContract]
        public List<Package> ListPackage(int? packageId, bool includeLegacy)
        {
            return SynergiMethods.ListPackage(packageId, includeLegacy);
        }

        [OperationContract]
        public void SavePackage(List<Package> saveList)
        {
            SynergiMethods.SavePackage(saveList);
        }
        #endregion

        #region CompetitorSource
        [OperationContract]
        public List<CompetitorSource> ListCompetitorSource(int? competitorSourceId)
        {
            return SynergiMethods.ListCompetitorSource(competitorSourceId);
        }

        [OperationContract]
        public List<CompetitorSource> ListContractCompetitorSources(int contractId)
        {
            return SynergiMethods.ListContractCompetitorSources(contractId);
        }

        [OperationContract]
        public void SaveCompetitorSource(List<CompetitorSource> saveList)
        {
            SynergiMethods.SaveCompetitorSource(saveList);
        }
        #endregion

        #region Contract
        [OperationContract]
        public List<Country> ListCountry(int? countryId)
        {
            return SynergiMethods.ListCountry(countryId);
        }

        [OperationContract]
        public List<Contract> ListContract(int? contractId, int? customerId, Guid? licenseKey, string domainName,
            string customerName, int? packageId, bool includeLegacy, bool listCustomer)
        {
            return SynergiMethods.ListContract(contractId, customerId, licenseKey, domainName, customerName, packageId, includeLegacy, listCustomer);
        }

        [OperationContract]
        public void SaveContracts(List<Contract> saveList)
        {
            SynergiMethods.SaveContracts(saveList);
        }

        [OperationContract]
        public Contract SaveContract(Contract savedContract)
        {
            List<Contract> saveList = new List<Contract>();
            saveList.Add(savedContract);
            SynergiMethods.SaveContracts(saveList);
            saveList = ListContract(savedContract.ContractId, null, null, null, null, null, true, true);
            if (saveList.Count > 0)
                return saveList[0];
            return savedContract;
        }        
        #endregion
    }

    /// <summary>
    /// Entity object class for storing the setting of TimeClock Application. It will consume by 
    /// the Silverlight pages
    /// </summary>
    [DataContract]
    public class UserLogin
    {        
        [DataMember]
        public int? UserCustomerId { set; get; }

        [DataMember]
        public string UserName { set; get; }

        [DataMember]
        public bool IsUserCustomer { set; get; }
        
        [DataMember]
        public bool IsUserAdministrator { set; get; }

        [DataMember]
        public Guid UserUserId { get; set; }

        [DataMember]
        public string UserOrganisationWeekday { set; get; }

        [DataMember]
        public AspUser AspUser { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class AppSettings
    {
        [DataMember]
        public string ReportServerUrl { set; get; }

        [DataMember]
        public string ReportLocalization { set; get; }
    }

    [DataContract]
    public class GlobalVariable
    {
        [DataMember]
        public AppSettings AppSettings { set; get; }

        [DataMember]
        public UserLogin UserLogin { set; get; }

        [DataMember]
        public DateTime? ApplicationCurrentDateTime { set; get; }
    }
}
