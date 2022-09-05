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
    public partial class PackageAdminPage : Page
    {        
        private int _seletedPackageId;
        private List<Package> _originalItemSource = new List<Package>();
                
        private static class UserMessages
        {
            internal const string NameErrorMessage2 = "The string may not exceed 128 characters in length.";
            internal const string ModuleExist = "This module is already added";
            //internal const string ContractInfoFor = "Contract information for {0}";     
        }

        public PackageAdminPage()
        {
            InitializeComponent();

            DataServiceHelper.ListModuleAsync(null, ListModulesCompleted);
            
            btnSavePackage.Click += new RoutedEventHandler(btnSavePackage_Click);
            btnCancelPackage.Click += new RoutedEventHandler(btnCancelPackage_Click);
            gvwPackages.SelectionChanged += new EventHandler<Telerik.Windows.Controls.SelectionChangeEventArgs>(gvwPackages_SelectionChanged);
            gvwPackages.AddingNewDataItem += new EventHandler<Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs>(gvwPackages_AddingNewDataItem);
            gvwPackages.CellValidating += new EventHandler<Telerik.Windows.Controls.GridViewCellValidatingEventArgs>(gvwPackages_CellValidating);
            gvwPackages.RowEditEnded += new EventHandler<GridViewRowEditEndedEventArgs>(gvwPackages_RowEditEnded);

            btnAddModule.Click += new RoutedEventHandler(btnAddModule_Click);
            gvwModules.Deleting += new EventHandler<GridViewDeletingEventArgs>(gvwModules_Deleting);
            //UiHelper.ApplyMouseWheelScrollViewer(scrollViewerPackage);
        }      

        private void ListModulesCompleted(List<Module> moduleList)
        {
            uiModuleList.ItemsSource = moduleList;
            BeginRebindPackage();
        }

        private void BeginRebindPackage()
        {
            Globals.IsBusy = true;
            DataServiceHelper.ListPackageAsync(null, true, ListPackageCompleted);
        }

        void ListPackageCompleted(List<Package> packageList)
        {
            Globals.IsBusy = false;
            _originalItemSource.Clear();
            foreach (Package item in packageList)
            {
                item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(PackageItem_PropertyChanged);
                _originalItemSource.Add(item);
            }
            gvwPackages.ItemsSource = packageList;
            if (_seletedPackageId > 0 && packageList.Count(i => i.PackageId == _seletedPackageId) > 0)
            {
                gvwPackages.SelectedItem = packageList.First(i => i.PackageId == _seletedPackageId);
            }
            else if (packageList.Count > 0)
            {
                gvwPackages.SelectedItem = packageList[0];
            }
        }

        #region Package GridView
        void gvwPackages_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            gvwPackages_SelectionChanged(null, null);
        }

        private void gvwPackages_CellValidating(object sender, Telerik.Windows.Controls.GridViewCellValidatingEventArgs e)
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
        }


        void gvwPackages_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            Package newItem = new Package();
            newItem.RecordId = null;
            newItem.CreatedBy = Globals.UserLogin.UserName;
            newItem.DateCreated = Globals.Now;
            newItem.Modules = new List<Module>();
            newItem.IsChanged = true;
            e.NewObject = newItem;

        }

        void btnCancelPackage_Click(object sender, RoutedEventArgs e)
        {
            if (gvwPackages.SelectedItem != null)
                _seletedPackageId = ((Package)gvwPackages.SelectedItem).PackageId;
            BeginRebindPackage();
        }

        void btnSavePackage_Click(object sender, RoutedEventArgs e)
        {
            if (gvwPackages.SelectedItem != null)
                _seletedPackageId = ((Package)gvwPackages.SelectedItem).PackageId;
            List<Package> oldList = (List<Package>)gvwPackages.ItemsSource;
			List<Package> saveList = oldList.Where(d => (d.IsChanged || d.NullableRecordId == null)).ToList();
            Globals.IsBusy = true;
            DataServiceHelper.SavePackageAsync(saveList, SavePackageCompleted);
        }

        private void SavePackageCompleted()
        {
            Globals.IsBusy = false;
            //Reload data
            BeginRebindPackage();
            MessageBox.Show(Globals.UserMessages.RecordsSaved);
        }

        void gvwPackages_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            if (gvwPackages.SelectedItem != null)
            {
                Package selectedPackage = gvwPackages.SelectedItem as Package;
                gridDetails.Visibility = System.Windows.Visibility.Collapsed;
                if (selectedPackage != null)
                {
                    _seletedPackageId = selectedPackage.PackageId;
                    if (selectedPackage.Modules == null)
                    {
                        selectedPackage.Modules = new List<Module>();
                    }
                    gvwModules.ItemsSource = selectedPackage.Modules;
                    gridDetails.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        void PackageItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Package item = sender as Package;
            if (item != null && e.PropertyName != "IsChanged")
            {
                item.IsChanged = true;
                item.UpdatedBy = Globals.UserLogin.UserName;
            }
        }

        #endregion

        #region Modules
        void btnAddModule_Click(object sender, RoutedEventArgs e)
        {
            Module selectedModule = uiModuleList.SelectedItem as Module;
            List<Module> moduleList = gvwModules.ItemsSource as List<Module>;
            if (moduleList == null)
                moduleList = new List<Module>();
            if (selectedModule != null)
            {
                if (moduleList.Count(i => i.ModuleId == selectedModule.ModuleId) > 0)
                {
                    MessageBox.Show(UserMessages.ModuleExist, Globals.UserMessages.ValidationError, MessageBoxButton.OK);
                }
                else
                {
                    moduleList.Add(selectedModule);
                    if (gvwPackages.SelectedItem != null)
                    {
                        Package selectedPackage = gvwPackages.SelectedItem as Package;                     
                        if (selectedPackage != null)
                        {
                            selectedPackage.IsChanged = true;
                            selectedPackage.UpdatedBy = Globals.UserLogin.UserName;
                        }
                    }
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
                if (gvwPackages.SelectedItem != null)
                {
                    Package selectedPackage = gvwPackages.SelectedItem as Package;
                    if (selectedPackage != null)
                    {
                        selectedPackage.IsChanged = true;
                        selectedPackage.UpdatedBy = Globals.UserLogin.UserName;
                    }
                }
            }
        } 

        #endregion

    }
}
