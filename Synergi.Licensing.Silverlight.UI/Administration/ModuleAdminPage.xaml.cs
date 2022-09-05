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
    public partial class ModuleAdminPage : Page
    {        
        private int _seletedPackageId;
        
        public ModuleAdminPage()
        {
            InitializeComponent();

            btnSave.Click += new RoutedEventHandler(btnSave_Click);
            btnCancel.Click += new RoutedEventHandler(btnCancel_Click);
            BeginRebind();            
        }

        private void BeginRebind()
        {
            DataServiceHelper.ListModuleAsync(null, ListModulesCompleted);   
        }             

        private void ListModulesCompleted(List<Module> moduleList)
        {
            foreach (Module m in moduleList)
            {
                m.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(m_PropertyChanged);
            }
            gvwModules.ItemsSource = moduleList;
        }

        void m_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Module item = sender as Module;
            if (item != null && e.PropertyName != "IsChanged")
            {
                item.IsChanged = true;
                item.UpdatedBy = Globals.UserLogin.UserName;
            }
        }

        void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            BeginRebind();
        }

        void btnSave_Click(object sender, RoutedEventArgs e)
        {
            List<Module> itemSource = gvwModules.ItemsSource as List<Module>;
            List<Module> saveList = itemSource.Where(i => i.IsChanged).ToList();
            Globals.IsBusy = true;
            DataServiceHelper.SaveModuleAsync(saveList, SaveModulesCompleted);
        }

        void SaveModulesCompleted()
        {
            Globals.IsBusy = false;
            BeginRebind();
        }
    }
}
