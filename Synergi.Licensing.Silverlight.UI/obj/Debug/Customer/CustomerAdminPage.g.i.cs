﻿#pragma checksum "D:\Work\My-Documents\Synergi\synergilicensing\Synergi.Licensing.Silverlight.UI\Customer\CustomerAdminPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8E05DA2024DF645272FA7C43FA60769C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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
        
        internal System.Windows.Controls.TextBlock uiTitle;
        
        internal System.Windows.Controls.TextBlock uiParamsDisplay;
        
        internal System.Windows.Controls.Grid gridCustomer;
        
        internal System.Windows.Controls.TextBox uiCustomerFilter;
        
        internal System.Windows.Controls.CheckBox chkShowInactive;
        
        internal Telerik.Windows.Controls.RadGridView gvwCustomers;
        
        internal System.Windows.Controls.Grid gridContactInfo;
        
        internal System.Windows.Controls.TextBox txtAddress;
        
        internal System.Windows.Controls.TextBox txtCity;
        
        internal System.Windows.Controls.TextBox txtState;
        
        internal System.Windows.Controls.TextBox txtPostcode;
        
        internal Telerik.Windows.Controls.RadComboBox uiCountry;
        
        internal System.Windows.Controls.TextBox txtPhone;
        
        internal System.Windows.Controls.TextBox txtFax;
        
        internal System.Windows.Controls.Grid gridDetails;
        
        internal Telerik.Windows.Controls.RadGridView gvwContracts;
        
        internal Synergi.Licensing.Silverlight.UI.SingleClickButton btnNewContract;
        
        internal Synergi.Licensing.Silverlight.UI.SingleClickButton btnSaveCustomer;
        
        internal System.Windows.Controls.Button btnCancelCustomer;
        
        internal Telerik.Windows.Controls.RadWindow uiPopupContract;
        
        internal Synergi.Licensing.Silverlight.UI.ContractDetails ucContractDetails;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Synergi.Licensing.Silverlight.UI;component/Customer/CustomerAdminPage.xaml", System.UriKind.Relative));
            this.uiTitle = ((System.Windows.Controls.TextBlock)(this.FindName("uiTitle")));
            this.uiParamsDisplay = ((System.Windows.Controls.TextBlock)(this.FindName("uiParamsDisplay")));
            this.gridCustomer = ((System.Windows.Controls.Grid)(this.FindName("gridCustomer")));
            this.uiCustomerFilter = ((System.Windows.Controls.TextBox)(this.FindName("uiCustomerFilter")));
            this.chkShowInactive = ((System.Windows.Controls.CheckBox)(this.FindName("chkShowInactive")));
            this.gvwCustomers = ((Telerik.Windows.Controls.RadGridView)(this.FindName("gvwCustomers")));
            this.gridContactInfo = ((System.Windows.Controls.Grid)(this.FindName("gridContactInfo")));
            this.txtAddress = ((System.Windows.Controls.TextBox)(this.FindName("txtAddress")));
            this.txtCity = ((System.Windows.Controls.TextBox)(this.FindName("txtCity")));
            this.txtState = ((System.Windows.Controls.TextBox)(this.FindName("txtState")));
            this.txtPostcode = ((System.Windows.Controls.TextBox)(this.FindName("txtPostcode")));
            this.uiCountry = ((Telerik.Windows.Controls.RadComboBox)(this.FindName("uiCountry")));
            this.txtPhone = ((System.Windows.Controls.TextBox)(this.FindName("txtPhone")));
            this.txtFax = ((System.Windows.Controls.TextBox)(this.FindName("txtFax")));
            this.gridDetails = ((System.Windows.Controls.Grid)(this.FindName("gridDetails")));
            this.gvwContracts = ((Telerik.Windows.Controls.RadGridView)(this.FindName("gvwContracts")));
            this.btnNewContract = ((Synergi.Licensing.Silverlight.UI.SingleClickButton)(this.FindName("btnNewContract")));
            this.btnSaveCustomer = ((Synergi.Licensing.Silverlight.UI.SingleClickButton)(this.FindName("btnSaveCustomer")));
            this.btnCancelCustomer = ((System.Windows.Controls.Button)(this.FindName("btnCancelCustomer")));
            this.uiPopupContract = ((Telerik.Windows.Controls.RadWindow)(this.FindName("uiPopupContract")));
            this.ucContractDetails = ((Synergi.Licensing.Silverlight.UI.ContractDetails)(this.FindName("ucContractDetails")));
        }
    }
}

