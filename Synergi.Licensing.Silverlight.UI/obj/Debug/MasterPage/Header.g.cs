﻿#pragma checksum "D:\Working\My-Documents\Synergi\synergilicensing\Synergi.Licensing.Silverlight.UI\MasterPage\Header.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E8099B4146CE08C7564E9159CCF0BC9E"
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
using Telerik.Windows.Controls;


namespace Synergi.Licensing.Silverlight.UI {
    
    
    public partial class Header : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Button uiBtnLogo;
        
        internal System.Windows.Controls.Image uiLogo;
        
        internal Telerik.Windows.Controls.RadContextMenu uiContextMenu;
        
        internal Synergi.Licensing.Silverlight.UI.HeaderMenuButton uiCustomer;
        
        internal System.Windows.Controls.TextBlock uiDeploymentInfo;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Synergi.Licensing.Silverlight.UI;component/MasterPage/Header.xaml", System.UriKind.Relative));
            this.uiBtnLogo = ((System.Windows.Controls.Button)(this.FindName("uiBtnLogo")));
            this.uiLogo = ((System.Windows.Controls.Image)(this.FindName("uiLogo")));
            this.uiContextMenu = ((Telerik.Windows.Controls.RadContextMenu)(this.FindName("uiContextMenu")));
            this.uiCustomer = ((Synergi.Licensing.Silverlight.UI.HeaderMenuButton)(this.FindName("uiCustomer")));
            this.uiDeploymentInfo = ((System.Windows.Controls.TextBlock)(this.FindName("uiDeploymentInfo")));
        }
    }
}

