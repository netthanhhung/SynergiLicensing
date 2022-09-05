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
    public partial class CompetitorSourceAdminPage : Page
    {        
        private int _seletedPackageId;
        
        public CompetitorSourceAdminPage()
        {
            InitializeComponent();

            btnSave.Click += new RoutedEventHandler(btnSave_Click);
            btnCancel.Click += new RoutedEventHandler(btnCancel_Click);
            BeginRebind();            
        }

        private void BeginRebind()
        {
            DataServiceHelper.ListCompetitorSourceAsync(null, ListCompetitorSourcesCompleted);   
        }             

        private void ListCompetitorSourcesCompleted(List<CompetitorSource> compSrcList)
        {
            foreach (CompetitorSource m in compSrcList)
            {
                m.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(m_PropertyChanged);
            }
            gvwCompetitorSources.ItemsSource = compSrcList;
        }

        void m_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CompetitorSource item = sender as CompetitorSource;
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
            List<CompetitorSource> itemSource = gvwCompetitorSources.ItemsSource as List<CompetitorSource>;
            List<CompetitorSource> saveList = itemSource.Where(i => i.IsChanged).ToList();
            Globals.IsBusy = true;
            DataServiceHelper.SaveCompetitorSourceAsync(saveList, SaveCompetitorSourcesCompleted);
        }

        void SaveCompetitorSourcesCompleted()
        {
            Globals.IsBusy = false;
            BeginRebind();
        }
    }
}
