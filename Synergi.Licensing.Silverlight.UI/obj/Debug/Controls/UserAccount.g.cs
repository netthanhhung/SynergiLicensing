﻿#pragma checksum "D:\Working\My-Documents\Synergi\synergilicensing\Synergi.Licensing.Silverlight.UI\Controls\UserAccount.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9BD1890665906DB534ED7807C8940CA0"
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
    
    
    public partial class UserAccount : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.StackPanel panelUserAccount;
        
        internal Telerik.Windows.Controls.RadComboBox uiUsers;
        
        internal System.Windows.Controls.PasswordBox txtPassword;
        
        internal System.Windows.Controls.TextBlock txtEmailLabel;
        
        internal System.Windows.Controls.TextBox txtEmail;
        
        internal System.Windows.Controls.Grid gridExtraInfo;
        
        internal System.Windows.Controls.CheckBox chkAccountApproved;
        
        internal System.Windows.Controls.CheckBox chkResetPassword;
        
        internal System.Windows.Controls.CheckBox chkChangePasswordQuestionAnswer;
        
        internal System.Windows.Controls.TextBox txtResetPasswordInfo;
        
        internal System.Windows.Controls.TextBlock txtPasswordQuestionLabel;
        
        internal System.Windows.Controls.TextBox txtPasswordQuestion;
        
        internal System.Windows.Controls.TextBlock txtPasswordAnswerLabel;
        
        internal System.Windows.Controls.TextBox txtPasswordAnswer;
        
        internal System.Windows.Controls.TextBlock txtInputPasswordLbl;
        
        internal System.Windows.Controls.PasswordBox txtInputPassword;
        
        internal Synergi.Licensing.Silverlight.UI.Information ucInformation;
        
        internal System.Windows.Controls.Image uiImageOnline;
        
        internal Synergi.Licensing.Silverlight.UI.SingleClickButton btnUnlock;
        
        internal Synergi.Licensing.Silverlight.UI.SingleClickButton btnSave;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Synergi.Licensing.Silverlight.UI;component/Controls/UserAccount.xaml", System.UriKind.Relative));
            this.panelUserAccount = ((System.Windows.Controls.StackPanel)(this.FindName("panelUserAccount")));
            this.uiUsers = ((Telerik.Windows.Controls.RadComboBox)(this.FindName("uiUsers")));
            this.txtPassword = ((System.Windows.Controls.PasswordBox)(this.FindName("txtPassword")));
            this.txtEmailLabel = ((System.Windows.Controls.TextBlock)(this.FindName("txtEmailLabel")));
            this.txtEmail = ((System.Windows.Controls.TextBox)(this.FindName("txtEmail")));
            this.gridExtraInfo = ((System.Windows.Controls.Grid)(this.FindName("gridExtraInfo")));
            this.chkAccountApproved = ((System.Windows.Controls.CheckBox)(this.FindName("chkAccountApproved")));
            this.chkResetPassword = ((System.Windows.Controls.CheckBox)(this.FindName("chkResetPassword")));
            this.chkChangePasswordQuestionAnswer = ((System.Windows.Controls.CheckBox)(this.FindName("chkChangePasswordQuestionAnswer")));
            this.txtResetPasswordInfo = ((System.Windows.Controls.TextBox)(this.FindName("txtResetPasswordInfo")));
            this.txtPasswordQuestionLabel = ((System.Windows.Controls.TextBlock)(this.FindName("txtPasswordQuestionLabel")));
            this.txtPasswordQuestion = ((System.Windows.Controls.TextBox)(this.FindName("txtPasswordQuestion")));
            this.txtPasswordAnswerLabel = ((System.Windows.Controls.TextBlock)(this.FindName("txtPasswordAnswerLabel")));
            this.txtPasswordAnswer = ((System.Windows.Controls.TextBox)(this.FindName("txtPasswordAnswer")));
            this.txtInputPasswordLbl = ((System.Windows.Controls.TextBlock)(this.FindName("txtInputPasswordLbl")));
            this.txtInputPassword = ((System.Windows.Controls.PasswordBox)(this.FindName("txtInputPassword")));
            this.ucInformation = ((Synergi.Licensing.Silverlight.UI.Information)(this.FindName("ucInformation")));
            this.uiImageOnline = ((System.Windows.Controls.Image)(this.FindName("uiImageOnline")));
            this.btnUnlock = ((Synergi.Licensing.Silverlight.UI.SingleClickButton)(this.FindName("btnUnlock")));
            this.btnSave = ((Synergi.Licensing.Silverlight.UI.SingleClickButton)(this.FindName("btnSave")));
        }
    }
}

