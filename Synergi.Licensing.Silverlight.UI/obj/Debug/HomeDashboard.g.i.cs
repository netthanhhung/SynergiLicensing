﻿#pragma checksum "D:\Working\Projects\Trunk\SynergiLicensing\Synergi.Licensing.Silverlight.UI\HomeDashboard.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C93DEA48271F4EF105AF612BA40B13FB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.237
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Rockford.TimeClock.Silverlight.UI;
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


namespace Rockford.TimeClock.Silverlight.UI {
    
    
    public partial class HomeDashboard : System.Windows.Controls.Page {
        
        internal System.Windows.Controls.Grid uiLayoutRoot;
        
        internal Rockford.TimeClock.Silverlight.UI.DashboardViewer ucDashboardViewer;
        
        internal Telerik.Windows.Controls.RadWindow uiPopupParams;
        
        internal Rockford.TimeClock.Silverlight.UI.EmployeeRatingParams ucEmployeeRatingParams;
        
        internal Telerik.Windows.Controls.RadButton uiPopupParamsOk;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Synergi.Licensing.Silverlight.UI;component/HomeDashboard.xaml", System.UriKind.Relative));
            this.uiLayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("uiLayoutRoot")));
            this.ucDashboardViewer = ((Rockford.TimeClock.Silverlight.UI.DashboardViewer)(this.FindName("ucDashboardViewer")));
            this.uiPopupParams = ((Telerik.Windows.Controls.RadWindow)(this.FindName("uiPopupParams")));
            this.ucEmployeeRatingParams = ((Rockford.TimeClock.Silverlight.UI.EmployeeRatingParams)(this.FindName("ucEmployeeRatingParams")));
            this.uiPopupParamsOk = ((Telerik.Windows.Controls.RadButton)(this.FindName("uiPopupParamsOk")));
        }
    }
}

