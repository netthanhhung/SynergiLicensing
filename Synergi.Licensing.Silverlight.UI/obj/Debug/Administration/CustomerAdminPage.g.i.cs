﻿#pragma checksum "D:\Working\Projects\Trunk\SynergiLicensing\Synergi.Licensing.Silverlight.UI\Administration\CustomerAdminPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2067DBDB91CD381AE79F69724EF4F785"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.237
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Synergi.Licensing.Silverlight.UI;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Synergi.Licensing.Silverlight.UI {
    
    
    public partial class CustomerAdminPage : System.Windows.Controls.Page {
        
        internal System.Windows.Controls.Grid LayoutRootOrgAdmin;
        
        internal System.Windows.Controls.TextBlock uiTitle;
        
        internal System.Windows.Controls.TextBlock uiParamsDisplay;
        
        internal System.Windows.Controls.ScrollViewer scrollViewerCustomer;
        
        internal Telerik.Windows.Controls.RadGridView gvwCustomers;
        
        internal Synergi.Licensing.Silverlight.UI.SingleClickButton btnSaveCustomer;
        
        internal System.Windows.Controls.Button btnCancelCustomer;
        
        internal System.Windows.Controls.TextBlock txtContactInfo;
        
        internal System.Windows.Controls.TextBlock txtContractInfo;
        
        internal System.Windows.Controls.Grid gridDetails;
        
        internal System.Windows.Controls.Grid gridContactInfo;
        
        internal System.Windows.Controls.TextBox txtAddress;
        
        internal System.Windows.Controls.TextBox txtCity;
        
        internal System.Windows.Controls.TextBox txtState;
        
        internal System.Windows.Controls.TextBox txtPostcode;
        
        internal Telerik.Windows.Controls.RadComboBox uiCountry;
        
        internal System.Windows.Controls.TextBox txtPhone;
        
        internal System.Windows.Controls.TextBox txtFax;
        
        internal Synergi.Licensing.Silverlight.UI.SingleClickButton btnSaveContact;
        
        internal Telerik.Windows.Controls.RadGridView gvwContracts;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Synergi.Licensing.Silverlight.UI;component/Administration/CustomerAdminPage.xaml" +
                        "", System.UriKind.Relative));
            this.LayoutRootOrgAdmin = ((System.Windows.Controls.Grid)(this.FindName("LayoutRootOrgAdmin")));
            this.uiTitle = ((System.Windows.Controls.TextBlock)(this.FindName("uiTitle")));
            this.uiParamsDisplay = ((System.Windows.Controls.TextBlock)(this.FindName("uiParamsDisplay")));
            this.scrollViewerCustomer = ((System.Windows.Controls.ScrollViewer)(this.FindName("scrollViewerCustomer")));
            this.gvwCustomers = ((Telerik.Windows.Controls.RadGridView)(this.FindName("gvwCustomers")));
            this.btnSaveCustomer = ((Synergi.Licensing.Silverlight.UI.SingleClickButton)(this.FindName("btnSaveCustomer")));
            this.btnCancelCustomer = ((System.Windows.Controls.Button)(this.FindName("btnCancelCustomer")));
            this.txtContactInfo = ((System.Windows.Controls.TextBlock)(this.FindName("txtContactInfo")));
            this.txtContractInfo = ((System.Windows.Controls.TextBlock)(this.FindName("txtContractInfo")));
            this.gridDetails = ((System.Windows.Controls.Grid)(this.FindName("gridDetails")));
            this.gridContactInfo = ((System.Windows.Controls.Grid)(this.FindName("gridContactInfo")));
            this.txtAddress = ((System.Windows.Controls.TextBox)(this.FindName("txtAddress")));
            this.txtCity = ((System.Windows.Controls.TextBox)(this.FindName("txtCity")));
            this.txtState = ((System.Windows.Controls.TextBox)(this.FindName("txtState")));
            this.txtPostcode = ((System.Windows.Controls.TextBox)(this.FindName("txtPostcode")));
            this.uiCountry = ((Telerik.Windows.Controls.RadComboBox)(this.FindName("uiCountry")));
            this.txtPhone = ((System.Windows.Controls.TextBox)(this.FindName("txtPhone")));
            this.txtFax = ((System.Windows.Controls.TextBox)(this.FindName("txtFax")));
            this.btnSaveContact = ((Synergi.Licensing.Silverlight.UI.SingleClickButton)(this.FindName("btnSaveContact")));
            this.gvwContracts = ((Telerik.Windows.Controls.RadGridView)(this.FindName("gvwContracts")));
        }
    }
}

