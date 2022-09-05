using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Linq;
using Synergi.Licensing.Common;

using System.IO;
using Synergi.Licensing.Silverlight.UI.SynergiLicensingService;

namespace Synergi.Licensing.Silverlight.UI
{
	public static class DataServiceHelper
	{
		/*  ======================================================================            
		 *      EVENTS AND DELEGATES
		 *  ====================================================================== */
		internal delegate void EmptyCallback();
		internal delegate void DataServiceCallback<T>(T returnValue);
		internal delegate void DataServiceIEnumerableCallback<T>(IEnumerable<T> itemsSource);
		internal delegate void DataServiceListCallback<T>(List<T> itemsSource);


		/*  ======================================================================            
		 *      PAGE MEMBERS
		 *  ====================================================================== */
		private static Dictionary<Guid, Delegate> _callbacks = new Dictionary<Guid, Delegate>();
		private static Dictionary<Guid, Object[]> _params = new Dictionary<Guid, Object[]>();

		/*  ======================================================================            
		 *      EVENT HANDLERS
		 *  ====================================================================== */
        private static SynergiLicensingServiceClient GetProxy()
		{
			return GetProxy(Guid.Empty, null);
		}

        private static SynergiLicensingServiceClient GetProxy(Guid callerKey, Delegate callback)
		{
            SynergiLicensingServiceClient result = new SynergiLicensingServiceClient();
			// Rely on the endpoint addresses being setup within ServiceReferences.ClientConfig instead.
			// result.Endpoint.Address = new EndpointAddress(Globals.EndPointAddresses.DataService);            
			if (callback != null)
			{
				_callbacks.Add(callerKey, callback);
			}

			return result;
		}

		
		/*  ======================================================================            
		 *      EVENT HANDLERS
		 *  ====================================================================== */
		static void proxy_VoidMethodCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			Guid callerKey = (Guid)e.UserState;
			if (_callbacks.ContainsKey(callerKey))
			{
				((EmptyCallback)_callbacks[callerKey]).Invoke();

				_callbacks.Remove(callerKey);
			}
		}

        /*  ======================================================================            
		 *      Methods
		 *  ====================================================================== */
        internal static void SelectSessionId()
        {
            SynergiLicensingServiceClient proxy = GetProxy();
            proxy.SelectSessionIdAsync();
        }

        //GetGlobalVariableAsync
        internal delegate void GetGlobalVariableCallBack(GlobalVariable itemSource);
        internal static void GetGlobalVariableAsync(GetGlobalVariableCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();
            SynergiLicensingServiceClient proxy = GetProxy(callerKey, callback);
            proxy.GetGlobalVariableCompleted += new EventHandler<GetGlobalVariableCompletedEventArgs>(proxy_GetGlobalVariableCompleted);
            proxy.GetGlobalVariableAsync(callerKey);
        }

        static void proxy_GetGlobalVariableCompleted(object sender, GetGlobalVariableCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                GlobalVariable itemSource = e.Result;
                ((GetGlobalVariableCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        // ChangePasswordAsync
        internal delegate void ChangePasswordCallback(int status);
        internal static void ChangePasswordAsync(string oldPassword, string newPassword, ChangePasswordCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            SynergiLicensingServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ChangePasswordCompleted += new EventHandler<ChangePasswordCompletedEventArgs>(proxy_ChangePasswordCompleted);
            proxy.ChangePasswordAsync(oldPassword, newPassword, callerKey);
        }

        static void proxy_ChangePasswordCompleted(object sender, ChangePasswordCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                int status = e.Result;

                ((ChangePasswordCallback)_callbacks[callerKey]).Invoke(status);

                _callbacks.Remove(callerKey);
            }
        }

        // ListAspUser
        internal delegate void ListAspUserCallback(List<AspUser> itemSource);
        internal static void ListAspUserAsync(Guid? userId, ListAspUserCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            SynergiLicensingServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListAspUserCompleted += new EventHandler<ListAspUserCompletedEventArgs>(proxy_ListAspUserCompleted);
            proxy.ListAspUserAsync(userId, callerKey);
        }

        static void proxy_ListAspUserCompleted(object sender, ListAspUserCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<AspUser> itemSource = e.Result;
                ((ListAspUserCallback)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        // UnlockAspUser
        internal delegate void UnlockAspUserCallBack(AspUser oldUser);
        internal static void UnlockAspUserAsync(AspUser oldUser, UnlockAspUserCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            SynergiLicensingServiceClient proxy = GetProxy(callerKey, callback);
            proxy.UnlockAspUserCompleted += new EventHandler<UnlockAspUserCompletedEventArgs>(proxy_UnlockAspUserCompleted);
            proxy.UnlockAspUserAsync(oldUser, callerKey);
        }

        static void proxy_UnlockAspUserCompleted(object sender, UnlockAspUserCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                AspUser itemSource = e.Result;
                ((UnlockAspUserCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //Save AspUser
        internal delegate void SaveAspUserCallBack(AspUser result);
        internal static void SaveAspUserAsync(AspUser saveUser, SaveAspUserCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();
            SynergiLicensingServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveAspUserCompleted += new EventHandler<SaveAspUserCompletedEventArgs>(proxy_SaveAspUserCompleted);
            proxy.SaveAspUserAsync(saveUser, callerKey);
        }

        static void proxy_SaveAspUserCompleted(object sender, SaveAspUserCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                AspUser result = e.Result;
                ((SaveAspUserCallBack)_callbacks[callerKey]).Invoke(result);

                _callbacks.Remove(callerKey);
            }
        }

        // ListCountry
        internal delegate void ListCountryCallback(List<Country> itemSource);
        internal static void ListCountryAsync(int? countryId, ListCountryCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            SynergiLicensingServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListCountryCompleted += new EventHandler<ListCountryCompletedEventArgs>(proxy_ListCountryCompleted);
            proxy.ListCountryAsync(countryId, callerKey);
        }

        static void proxy_ListCountryCompleted(object sender, ListCountryCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<Country> itemSource = e.Result;
                ((ListCountryCallback)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        // ListContactInformation
        internal delegate void ListContactInformationCallback(List<ContactInformation> itemSource);
        internal static void ListContactInformationAsync(int? contactInformationId, ListContactInformationCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            SynergiLicensingServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListContactInformationCompleted += new EventHandler<ListContactInformationCompletedEventArgs>(proxy_ListContactInformationCompleted);
            proxy.ListContactInformationAsync(contactInformationId, callerKey);
        }

        static void proxy_ListContactInformationCompleted(object sender, ListContactInformationCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<ContactInformation> itemSource = e.Result;
                ((ListContactInformationCallback)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //SaveContactInformation
        internal static void SaveContactInformationAsync(List<ContactInformation> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            SynergiLicensingServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveContactInformationCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveContactInformationAsync(saveList, callerKey);
        }

        // ListCustomer
        internal delegate void ListCustomerCallback(List<Customer> itemSource);
        internal static void ListCustomerAsync(int? customerId, string name, bool includeLegacy, ListCustomerCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            SynergiLicensingServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListCustomerCompleted += new EventHandler<ListCustomerCompletedEventArgs>(proxy_ListCustomerCompleted);
            proxy.ListCustomerAsync(customerId, name, includeLegacy, callerKey);
        }

        static void proxy_ListCustomerCompleted(object sender, ListCustomerCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<Customer> itemSource = e.Result;
                ((ListCustomerCallback)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //SaveCustomer
        internal static void SaveCustomerAsync(List<Customer> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            SynergiLicensingServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveCustomerCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveCustomerAsync(saveList, callerKey);
        }

        // ListModule
        internal delegate void ListModuleCallback(List<Module> itemSource);
        internal static void ListModuleAsync(int? moduleId, ListModuleCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            SynergiLicensingServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListModuleCompleted += new EventHandler<ListModuleCompletedEventArgs>(proxy_ListModuleCompleted);
            proxy.ListModuleAsync(moduleId, callerKey);
        }

        static void proxy_ListModuleCompleted(object sender, ListModuleCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<Module> itemSource = e.Result;
                ((ListModuleCallback)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //SaveModule
        internal static void SaveModuleAsync(List<Module> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            SynergiLicensingServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveModuleCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveModuleAsync(saveList, callerKey);
        }

        // ListModulesOfPackage
        internal delegate void ListModulesOfPackageCallback(List<Module> itemSource);
        internal static void ListModulesOfPackageAsync(int packageId, ListModulesOfPackageCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            SynergiLicensingServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListModulesOfPackageCompleted += new EventHandler<ListModulesOfPackageCompletedEventArgs>(proxy_ListModulesOfPackageCompleted);
            proxy.ListModulesOfPackageAsync(packageId, callerKey);
        }

        static void proxy_ListModulesOfPackageCompleted(object sender, ListModulesOfPackageCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<Module> itemSource = e.Result;
                ((ListModulesOfPackageCallback)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        // ListAdditionalContractModules
        internal delegate void ListAdditionalContractModulesCallback(List<Module> itemSource);
        internal static void ListAdditionalContractModulesAsync(int contractId, ListAdditionalContractModulesCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            SynergiLicensingServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListAdditionalContractModulesCompleted += new EventHandler<ListAdditionalContractModulesCompletedEventArgs>(proxy_ListAdditionalContractModulesCompleted);
            proxy.ListAdditionalContractModulesAsync(contractId, callerKey);
        }

        static void proxy_ListAdditionalContractModulesCompleted(object sender, ListAdditionalContractModulesCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<Module> itemSource = e.Result;
                ((ListAdditionalContractModulesCallback)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        // ListPackage
        internal delegate void ListPackageCallback(List<Package> itemSource);
        internal static void ListPackageAsync(int? packageId, bool includeLegacy, ListPackageCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            SynergiLicensingServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListPackageCompleted += new EventHandler<ListPackageCompletedEventArgs>(proxy_ListPackageCompleted);
            proxy.ListPackageAsync(packageId, includeLegacy, callerKey);
        }

        static void proxy_ListPackageCompleted(object sender, ListPackageCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<Package> itemSource = e.Result;
                ((ListPackageCallback)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //SavePackage
        internal static void SavePackageAsync(List<Package> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            SynergiLicensingServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SavePackageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SavePackageAsync(saveList, callerKey);
        }

        // ListCompetitorSource
        internal delegate void ListCompetitorSourceCallback(List<CompetitorSource> itemSource);
        internal static void ListCompetitorSourceAsync(int? moduleId, ListCompetitorSourceCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            SynergiLicensingServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListCompetitorSourceCompleted += new EventHandler<ListCompetitorSourceCompletedEventArgs>(proxy_ListCompetitorSourceCompleted);
            proxy.ListCompetitorSourceAsync(moduleId, callerKey);
        }

        static void proxy_ListCompetitorSourceCompleted(object sender, ListCompetitorSourceCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<CompetitorSource> itemSource = e.Result;
                ((ListCompetitorSourceCallback)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //SaveCompetitorSource
        internal static void SaveCompetitorSourceAsync(List<CompetitorSource> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            SynergiLicensingServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveCompetitorSourceCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveCompetitorSourceAsync(saveList, callerKey);
        }

        // ListContractCompetitorSources
        internal delegate void ListContractCompetitorSourcesCallback(List<CompetitorSource> itemSource);
        internal static void ListContractCompetitorSourcesAsync(int contractId, ListContractCompetitorSourcesCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            SynergiLicensingServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListContractCompetitorSourcesCompleted += new EventHandler<ListContractCompetitorSourcesCompletedEventArgs>(proxy_ListContractCompetitorSourcesCompleted);
            proxy.ListContractCompetitorSourcesAsync(contractId, callerKey);
        }

        static void proxy_ListContractCompetitorSourcesCompleted(object sender, ListContractCompetitorSourcesCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<CompetitorSource> itemSource = e.Result;
                ((ListContractCompetitorSourcesCallback)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        // ListContract
        internal delegate void ListContractCallback(List<Contract> itemSource);
        internal static void ListContractsAsync(int? contractId, int? customerId, Guid? licenseKey, string domainName,
            string customerName, int? packageId, bool includeLegacy, bool listCustomer, ListContractCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            SynergiLicensingServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListContractCompleted += new EventHandler<ListContractCompletedEventArgs>(proxy_ListContractCompleted);
            proxy.ListContractAsync(contractId, customerId, licenseKey, domainName, customerName, packageId, 
                includeLegacy, listCustomer, callerKey);
        }

        static void proxy_ListContractCompleted(object sender, ListContractCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<Contract> itemSource = e.Result;
                ((ListContractCallback)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //SaveContracts
        internal static void SaveContracstAsync(List<Contract> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            SynergiLicensingServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveContractsCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveContractsAsync(saveList, callerKey);
        }

        // SaveContract
        internal delegate void SaveContractCallback(Contract savedContract);
        internal static void SaveContractsAsync(Contract savedContract, SaveContractCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            SynergiLicensingServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveContractCompleted += new EventHandler<SaveContractCompletedEventArgs>(proxy_SaveContractCompleted);
            proxy.SaveContractAsync(savedContract, callerKey);
        }

        static void proxy_SaveContractCompleted(object sender, SaveContractCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                Contract savedContract = e.Result;
                ((SaveContractCallback)_callbacks[callerKey]).Invoke(savedContract);

                _callbacks.Remove(callerKey);
            }
        }
	}
}
