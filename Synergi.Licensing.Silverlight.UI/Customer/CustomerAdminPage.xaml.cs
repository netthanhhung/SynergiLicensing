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
    public partial class CustomerAdminPage : Page
    {
        private const int _defaultCountryId = 13;    

        private int _seletedCustomerId;
        private List<Customer> _originalItemSource = new List<Customer>();
                
        private static class UserMessages
        {
            internal const string NameErrorMessage2 = "The string may not exceed 128 characters in length.";
            
        }

        public CustomerAdminPage()
        {
            InitializeComponent();            

            DataServiceHelper.ListCountryAsync(null, ListCountryCompleted);
            BeginRebindCustomer();
            
            btnSaveCustomer.Click += new RoutedEventHandler(btnSaveCustomer_Click);
            btnCancelCustomer.Click += new RoutedEventHandler(btnCancelCustomer_Click);
            gvwCustomers.SelectionChanged += new EventHandler<Telerik.Windows.Controls.SelectionChangeEventArgs>(gvwCustomers_SelectionChanged);
            gvwCustomers.AddingNewDataItem += new EventHandler<Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs>(gvwCustomers_AddingNewDataItem);            
            gvwCustomers.CellValidating += new EventHandler<Telerik.Windows.Controls.GridViewCellValidatingEventArgs>(gvwCustomers_CellValidating);
            gvwCustomers.RowEditEnded += new EventHandler<GridViewRowEditEndedEventArgs>(gvwCustomers_RowEditEnded);

            uiCustomerFilter.TextChanged += new TextChangedEventHandler(uiCustomerFilter_TextChanged);
            btnNewContract.Click += new RoutedEventHandler(btnNewContract_Click);

            chkShowInactive.Checked += new RoutedEventHandler(chkShowInactive_CheckChanged);
            chkShowInactive.Unchecked += new RoutedEventHandler(chkShowInactive_CheckChanged);

            ucContractDetails.SaveContractCompleted += new EventHandler(ucContractDetails_SaveContractCompleted);
            ucContractDetails.ButtonCancelClicked += new EventHandler(ucContractDetails_ButtonCancelClicked);
            //UiHelper.ApplyMouseWheelScrollViewer(scrollViewerCustomer);
            this.Loaded += new RoutedEventHandler(CustomerAdminPage_Loaded);
        }     

        void chkShowInactive_CheckChanged(object sender, RoutedEventArgs e)
        {
            BeginRebindCustomer();
        }

        void CustomerAdminPage_Loaded(object sender, RoutedEventArgs e)
        {
            gvwCustomers.Height = gridCustomer.ActualHeight - 30;
            gvwContracts.Height = gridDetails.ActualHeight - 50;
        }

        void ListCountryCompleted(List<Country> countryList)
        {
            uiCountry.ItemsSource = countryList;
        }

        void uiCustomerFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            BeginRebindCustomer();
            //string textFilter = uiCustomerFilter.Text.Trim();
            //if (!string.IsNullOrEmpty(textFilter) && textFilter.Length > 2)
            //{
            //    textFilter = textFilter.ToLower();
            //    IEnumerable<Customer> filteredList = _originalItemSource.Where(
            //        i => (!string.IsNullOrEmpty(i.Name) && i.Name.ToLower().Contains(textFilter))
            //              || (!string.IsNullOrEmpty(i.BusinessName) && i.BusinessName.ToLower().Contains(textFilter)));
            //    if (filteredList != null)
            //    {
            //        gvwCustomers.ItemsSource = filteredList.ToList();
            //    }
            //}
            //else if (string.IsNullOrEmpty(textFilter))
            //{
            //    List<Customer> newItemsource = new List<Customer>();
            //    foreach (Customer item in _originalItemSource)
            //    {
            //        newItemsource.Add(item);
            //    }
            //    gvwCustomers.ItemsSource = newItemsource;
            //}
        }

        private void BeginRebindCustomer()
        {
            Globals.IsBusy = true;
            DataServiceHelper.ListCustomerAsync(null, uiCustomerFilter.Text.Trim(), chkShowInactive.IsChecked == true, ListCustomerCompleted);
        }

        void ListCustomerCompleted(List<Customer> customerList)
        {
            Globals.IsBusy = false;
            _originalItemSource.Clear();
            foreach (Customer item in customerList)
            {
                item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(CustomerItem_PropertyChanged);
                item.ContactInformation.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ContactInformation_PropertyChanged);
                _originalItemSource.Add(item);
            }
            gvwCustomers.ItemsSource = customerList;
            if (_seletedCustomerId > 0 && customerList.Count(i => i.CustomerId == _seletedCustomerId) > 0)
            {
                gvwCustomers.SelectedItem = customerList.First(i => i.CustomerId == _seletedCustomerId);
            }
            else if (customerList.Count > 0)
            {
                gvwCustomers.SelectedItem = customerList[0];
            }
        }

        #region Customer GridView
        void gvwCustomers_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            gvwCustomers_SelectionChanged(null, null);
        }

        private void gvwCustomers_CellValidating(object sender, Telerik.Windows.Controls.GridViewCellValidatingEventArgs e)
        {
            if (e.Cell.Column.UniqueName == "Name")
            {
                if (e.NewValue.ToString().Length < 1)
                {
                    e.IsValid = false;
                    e.ErrorMessage = Globals.UserMessages.RequiredFieldGeneric;
                }
                else if (e.NewValue.ToString().Length > 128)
                {
                    e.IsValid = false;
                    e.ErrorMessage = UserMessages.NameErrorMessage2;
                }
            }
            //else if (e.Cell.Column.UniqueName == "DisplayIndex")
            //{
            //    int displayindex;
            //     if (!int.TryParse(e.NewValue.ToString(), out displayindex))
            //    {
            //        e.IsValid = false;
            //        e.ErrorMessage = Globals.UserMessages.RequiredFieldGeneric;
            //    }                
            //}
        }

        //void gvwCustomers_BeginningEdit(object sender, Telerik.Windows.Controls.GridViewBeginningEditRoutedEventArgs e)
        //{
        //    if (e.Cell.Column.UniqueName == "NumberOfRooms")
        //    {
        //        e.Cancel = true;
        //    }
        //}

        void gvwCustomers_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            Customer newItem = new Customer();
            newItem.RecordId = null;
            newItem.CreatedBy = Globals.UserLogin.UserName;
            newItem.DateCreated = Globals.Now;
            newItem.ContactInformation = new ContactInformation();
            newItem.ContactInformation.CountryId = _defaultCountryId;
            newItem.IsChanged = newItem.ContactInformation.IsChanged = true;
            e.NewObject = newItem;

            gridDetails.Visibility = gridContactInfo.Visibility = System.Windows.Visibility.Collapsed;
        }

        void btnCancelCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (gvwCustomers.SelectedItem != null)
                _seletedCustomerId = ((Customer)gvwCustomers.SelectedItem).CustomerId;
            BeginRebindCustomer();
        }

        void btnSaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (gvwCustomers.SelectedItem != null)
                _seletedCustomerId = ((Customer)gvwCustomers.SelectedItem).CustomerId;
            List<Customer> oldList = (List<Customer>)gvwCustomers.ItemsSource;
			List<Customer> saveList = oldList.Where(d => (d.IsChanged || d.NullableRecordId == null || d.ContactInformation.IsChanged)).ToList();
            Globals.IsBusy = true;
            DataServiceHelper.SaveCustomerAsync(saveList, SaveCustomerCompleted);
        }

        private void SaveCustomerCompleted()
        {
            Globals.IsBusy = false;
            //Reload data
            BeginRebindCustomer();
            MessageBox.Show(Globals.UserMessages.RecordsSaved);
        }

        void gvwCustomers_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            if (gvwCustomers.SelectedItem != null)
            {
                Customer selectedCustomer = gvwCustomers.SelectedItem as Customer;
                gridDetails.Visibility = gridContactInfo.Visibility = System.Windows.Visibility.Collapsed;
                
                if (selectedCustomer != null)
                {
                    _seletedCustomerId = selectedCustomer.CustomerId;
                    if (selectedCustomer.ContactInformation == null)
                    {
                        selectedCustomer.ContactInformation = new ContactInformation();
                        selectedCustomer.ContactInformation.IsChanged = true;
                        selectedCustomer.ContactInformation.CountryId = _defaultCountryId;
                    }
                    gridContactInfo.DataContext = selectedCustomer.ContactInformation;
                    gvwContracts.ItemsSource = selectedCustomer.Contracts;
                    gridDetails.Visibility = gridContactInfo.Visibility = System.Windows.Visibility.Visible;
                    btnNewContract.Visibility = (gridDetails.Visibility == Visibility.Visible
                        && selectedCustomer.CustomerId > 0) ? Visibility.Visible : Visibility.Collapsed;
                    //txtContactInfo.Text = string.Format(UserMessages.ContactInfoFor, selectedCustomer.Name);
                    //txtContractInfo.Text = string.Format(UserMessages.ContractInfoFor, selectedCustomer.Name);
                }
            }
        }

        void CustomerItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Customer item = sender as Customer;
            if (item != null && e.PropertyName != "IsChanged")
            {
                item.IsChanged = true;
                item.UpdatedBy = Globals.UserLogin.UserName;
            }
        }

        void ContactInformation_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ContactInformation item = sender as ContactInformation;
            if (item != null && e.PropertyName != "IsChanged")
            {
                item.IsChanged = true;
                item.UpdatedBy = Globals.UserLogin.UserName;
            }
        }
        #endregion

        void ucContractDetails_ButtonCancelClicked(object sender, EventArgs e)
        {
            uiPopupContract.Close();
        }

        void ucContractDetails_SaveContractCompleted(object sender, EventArgs e)
        {
            uiPopupContract.Close();
            DataServiceHelper.ListContractsAsync(null, ucContractDetails.Customer.CustomerId, null, null, null, null, true, false,
                ListContractsCompleted);
        }

        private void ListContractsCompleted(List<Common.Contract> contracts)
        {
            Customer selectedCustomer = gvwCustomers.SelectedItem as Customer;
            if (selectedCustomer != null)
            {
                selectedCustomer.Contracts = contracts;
                gvwCustomers_SelectionChanged(null, null);
            }
        }

        private void btnSelectContract_Click(object sender, RoutedEventArgs e)
        {
            Button selectButton = ((Button)sender);
            Customer selectedCustomer = gvwCustomers.SelectedItem as Customer;
            if (selectedCustomer != null)
            {
                int contractId = Convert.ToInt32(selectButton.Tag.ToString());
                ucContractDetails.ContractId = contractId;
                ucContractDetails.Customer = selectedCustomer;
                ucContractDetails.BeginRebind();
                uiPopupContract.ShowDialog();
            }
        }

        void btnNewContract_Click(object sender, RoutedEventArgs e)
        {
            Customer selectedCustomer = gvwCustomers.SelectedItem as Customer;
            if (selectedCustomer != null)
            {
                ucContractDetails.ContractId = null;
                ucContractDetails.Customer = selectedCustomer;
                ucContractDetails.BeginRebind();
                uiPopupContract.ShowDialog();
            }
        }
    }
}
