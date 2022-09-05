using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Synergi.Licensing.Common;

namespace Synergi.Licensing.Silverlight.UI
{
    public partial class ContractDetails : UserControl
    {
        public event EventHandler SaveContractCompleted;
        public event EventHandler ButtonCancelClicked;
        public int? ContractId { get; set; }
        public Customer Customer { get; set; }

        private Contract _currentContract;
        //private Customer _currentCustomer = new Customer();
        private static class UserMessages
        {
            internal const string DomainNameMissing = "Please input Domain Name";
            internal const string DateMissing = "Please input Date";
            internal const string ActivatedDateMissing = "Please input Activated Date";
            internal const string PackageMissing = "Please select a package";
            internal const string ModuleExist = "This module is already added or exists in package";
            internal const string CompetitorSourceExist = "This competitor source is already added";
            internal const string NbrSitesMissing = "Please input a number of Sites";
            internal const string ExpiredDateError = "Expired Date should be greater than Activated Date";
            internal const string RealEndDateError = "Real End Date should be greater than Expired Date";
        }

        public ContractDetails()
        {
            InitializeComponent();

            DataServiceHelper.ListPackageAsync(null, false, ListPackageCompleted);
            DataServiceHelper.ListModuleAsync(null, ListModuleCompleted);
            DataServiceHelper.ListCompetitorSourceAsync(null, ListCompetitorSourceCompleted);

            btnSaveContract.Click += new RoutedEventHandler(btnSaveContract_Click);
            btnCancelContract.Click += new RoutedEventHandler(btnCancelContract_Click);

            gvwModules.Deleting +=new EventHandler<GridViewDeletingEventArgs>(gvwModules_Deleting);
            btnAddModule.Click += new RoutedEventHandler(btnAddModule_Click);
            gvwCompetitorSources.Deleting += new EventHandler<GridViewDeletingEventArgs>(gvwCompetitorSources_Deleting);
            btnAddCompetitorSource.Click += new RoutedEventHandler(btnAddCompetitorSource_Click);
            uiExpiredDate.SelectionChanged += new Telerik.Windows.Controls.SelectionChangedEventHandler(uiExpiredDate_SelectionChanged);
        }


        void ListPackageCompleted(List<Package> packages)
        {
            uiPackages.ItemsSource = packages;
        }

        void ListModuleCompleted(List<Module> modules)
        {
            uiModuleList.ItemsSource = modules;
        }

        void ListCompetitorSourceCompleted(List<CompetitorSource> modules)
        {
            uiCompetitorSourceList.ItemsSource = modules;
        }

        public void BeginRebind()
        {
            if (this.ContractId > 0)
            {
                Globals.IsBusy = true;
                DataServiceHelper.ListContractsAsync(this.ContractId.Value, null, null, null, null, null, true, true, ListContractsCompleted);
            }
            else
            {
                Globals.IsBusy = true;
                DataServiceHelper.ListCustomerAsync(this.Customer.CustomerId, null, true, ListCustomerCompleted);
            }
        }

        //private void BeginRebindContract(int contractId)
        //{
        //    Globals.IsBusy = true;
        //    DataServiceHelper.ListContractsAsync(contractId, null, null, null, null, null, true, true, ListContractsCompleted);
        //}

        private void ListContractsCompleted(List<Contract> list)
        {
            Globals.IsBusy = false;
            if (list.Count > 0)
            {
                _currentContract = list[0];
                _currentContract.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_currentContract_PropertyChanged);
            }
            else
            {
                CreateNewContract();
            }
            gridContractDetails.DataContext = _currentContract;
            gvwModules.ItemsSource = _currentContract.AdditionalModules;
            gvwCompetitorSources.ItemsSource = _currentContract.CompetitorSources;
        }

        void _currentContract_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (_currentContract != null && e.PropertyName != "IsChanged")
            {
                _currentContract.IsChanged = true;
                _currentContract.UpdatedBy = Globals.UserLogin.UserName;
            }
        }

        private void CreateNewContract()
        {
            _currentContract = new Contract();
            _currentContract.Customer = this.Customer;
            _currentContract.CustomerId = this.Customer.CustomerId;            
            _currentContract.Date = _currentContract.ActivatedDate = Globals.Today;
            _currentContract.AdditionalModules = new List<Module>();
            _currentContract.CompetitorSources = new List<CompetitorSource>();
            _currentContract.IsChanged = true;
            _currentContract.CreatedBy = _currentContract.UpdatedBy = Globals.UserLogin.UserName;

        }
        private void ListCustomerCompleted(List<Customer> list)
        {
            Globals.IsBusy = false;
            CreateNewContract();
            if (list.Count > 0)
            {
                _currentContract.Customer = list[0];
                _currentContract.CustomerId = list[0].CustomerId;
            }
            gridContractDetails.DataContext = _currentContract;
            gvwModules.ItemsSource = _currentContract.AdditionalModules;
            gvwCompetitorSources.ItemsSource = _currentContract.CompetitorSources;
        }

        void btnCancelContract_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonCancelClicked != null)
            {
                ButtonCancelClicked(this, null);
            }
        }

        void btnSaveContract_Click(object sender, RoutedEventArgs e)
        {
            if (_currentContract.CustomerId <= 0)
                return;
            if (!uiDate.SelectedDate.HasValue)
            {
                MessageBox.Show(UserMessages.DateMissing, Globals.UserMessages.ValidationError, MessageBoxButton.OK);
                return;
            }
            if (!uiActivatedDate.SelectedDate.HasValue)
            {
                MessageBox.Show(UserMessages.DateMissing, Globals.UserMessages.ValidationError, MessageBoxButton.OK);
                return;
            }

            if (uiPackages.SelectedValue == null)
            {
                MessageBox.Show(UserMessages.PackageMissing, Globals.UserMessages.ValidationError, MessageBoxButton.OK);
                return;
            }
            if (string.IsNullOrEmpty(txtDomainName.Text))
            {
                MessageBox.Show(UserMessages.DomainNameMissing, Globals.UserMessages.ValidationError, MessageBoxButton.OK);
                return;
            }
            if (uiNbrSites.Value <= 0)
            {
                MessageBox.Show(UserMessages.NbrSitesMissing, Globals.UserMessages.ValidationError, MessageBoxButton.OK);
                return;
            }
            if (uiExpiredDate.SelectedDate.HasValue && uiExpiredDate.SelectedDate.Value <= uiActivatedDate.SelectedDate)
            {
                MessageBox.Show(UserMessages.ExpiredDateError, Globals.UserMessages.ValidationError, MessageBoxButton.OK);
                return;
            }
            if (uiRealEndDate.SelectedDate.HasValue && uiExpiredDate.SelectedDate.HasValue
                && uiRealEndDate.SelectedDate <= uiExpiredDate.SelectedDate
                || uiExpiredDate.SelectedDate.HasValue && !uiRealEndDate.SelectedDate.HasValue)
            {
                MessageBox.Show(UserMessages.RealEndDateError, Globals.UserMessages.ValidationError, MessageBoxButton.OK);
                return;
            }

            Globals.IsBusy = true;
            DataServiceHelper.SaveContractsAsync(_currentContract, SaveContracCompleted);
        }

        void SaveContracCompleted(Contract saveContract)
        {
            Globals.IsBusy = false;
            MessageBox.Show(Globals.UserMessages.RecordsSaved);           
            if (SaveContractCompleted != null)
            {
                SaveContractCompleted(this, null);
            }
        }

        void uiExpiredDate_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (uiExpiredDate.SelectedDate.HasValue)
                uiRealEndDate.SelectedDate = uiExpiredDate.SelectedDate.Value.AddMonths(1);
        }

        #region Modules
        void btnAddModule_Click(object sender, RoutedEventArgs e)
        {
            Module selectedModule = uiModuleList.SelectedItem as Module;
            List<Module> moduleList = gvwModules.ItemsSource as List<Module>;
            if (moduleList == null)
                moduleList = new List<Module>();
            if (selectedModule != null)
            {
                if (moduleList.Count(i => i.ModuleId == selectedModule.ModuleId) > 0
                    || (uiPackages.SelectedItem != null && ((Package)uiPackages.SelectedItem).Modules != null
                        && ((Package)uiPackages.SelectedItem).Modules.Count(i => i.ModuleId == selectedModule.ModuleId) > 0))
                {
                    MessageBox.Show(UserMessages.ModuleExist, Globals.UserMessages.ValidationError, MessageBoxButton.OK);
                }
                else
                {
                    moduleList.Add(selectedModule);
                    _currentContract.IsChanged = true;
                    _currentContract.UpdatedBy = Globals.UserLogin.UserName;
                     
                }
                gvwModules.ItemsSource = null;
                gvwModules.ItemsSource = moduleList;
            }
        }

        void gvwModules_Deleting(object sender, GridViewDeletingEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(Globals.UserMessages.ConfirmDeleteNoParam, Globals.UserMessages.ConfirmationRequired, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                _currentContract.IsChanged = true;
                _currentContract.UpdatedBy = Globals.UserLogin.UserName;
            }
        }

        #endregion

        #region CompetitorSources
        void btnAddCompetitorSource_Click(object sender, RoutedEventArgs e)
        {
            CompetitorSource selectedCompetitorSource = uiCompetitorSourceList.SelectedItem as CompetitorSource;
            List<CompetitorSource> competitorSourceList = gvwCompetitorSources.ItemsSource as List<CompetitorSource>;
            if (competitorSourceList == null)
                competitorSourceList = new List<CompetitorSource>();
            if (selectedCompetitorSource != null)
            {
                if (competitorSourceList.Count(i => i.CompetitorSourceId == selectedCompetitorSource.CompetitorSourceId) > 0)
                {
                    MessageBox.Show(UserMessages.CompetitorSourceExist, Globals.UserMessages.ValidationError, MessageBoxButton.OK);
                }
                else
                {
                    competitorSourceList.Add(selectedCompetitorSource);
                    _currentContract.IsChanged = true;
                    _currentContract.UpdatedBy = Globals.UserLogin.UserName;

                }
                gvwCompetitorSources.ItemsSource = null;
                gvwCompetitorSources.ItemsSource = competitorSourceList;
            }
        }

        void gvwCompetitorSources_Deleting(object sender, GridViewDeletingEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(Globals.UserMessages.ConfirmDeleteNoParam, Globals.UserMessages.ConfirmationRequired, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                _currentContract.IsChanged = true;
                _currentContract.UpdatedBy = Globals.UserLogin.UserName;
            }
        }

        #endregion
    }
}
