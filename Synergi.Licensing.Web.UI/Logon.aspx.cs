using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Resources;
using Synergi.Licensing.Common;
using Telerik.WebControls;
using System.Collections.Generic;
using System.Collections.Specialized;
using Synergi.Licensing.Business;
using Synergi.Licensing.Web.UI;

namespace Synergi.Licensing.Web.UI
{
    public partial class Logon : System.Web.UI.Page
    {
        #region Private Attributes
        private HtmlTable _loginTable;
        private RadComboBox _userNameCombo;
        private TextBox _userNameText;
        private RequiredFieldValidator _userValidator;
        private Button _loginButton;
        #endregion        

        #region Overrides

        protected override void OnInit(EventArgs e)
        {
            this.uiLogin.LoginError += new EventHandler(uiLogin_LoginError);
            base.OnInit(e);

            // Determine the configuration setting so we know which username control is visible and for validation
            bool enableAutoLoginComplete = SynergiLicensingSettings.IsEnableAutoLoginComplete();

            AssignLocalPointersToControls();

            // If an authentication timeout occurs, and the user performs an action that results in an AJAX callback
            // the browser hangs or errors.  To deal with this, we have to force the browser to properly reload the page
            // However, as we also have AJAX and postbacks on this page itself, we have to allow this and not force a redirect
            // Hence the check before setting redirect location.
            if (!_userNameCombo.IsCallBack && !IsPostBack)
                Response.RedirectLocation = Request.Url.OriginalString;

            _userNameCombo.Visible = enableAutoLoginComplete;
            _userNameText.Visible = !enableAutoLoginComplete;
            _userValidator.ControlToValidate = enableAutoLoginComplete ? "UserNameComboBox" : "UserName";

            if (enableAutoLoginComplete)
            {
                _userNameCombo.ItemsRequested += new RadComboBoxItemsRequestedEventHandler(UserNameCombo_ItemsRequested);
                uiLogin.LoggingIn += new LoginCancelEventHandler(uiLogin_LoggingIn);
            }
        }

        #endregion

        #region Private Methods

        private List<string> ListMatchingUserName(string startsWith)
        {
            // To avoid lots of SQL calls here, we try to make use of caching as best we can

            // 1. Have we got a cached Dictionary to check yet?
            Dictionary<string, List<string>> dictionary = Cache[Globals.CacheKeys.UserNameDictionaryCacheEntry] as Dictionary<string, List<string>>;

            if (null == dictionary) // Create one            
                dictionary = new Dictionary<string, List<string>>();

            // 2. Do we have an entry in the dictionary already or do we have to fetch and store?
            List<string> resultList = null;

            if (dictionary.ContainsKey(startsWith))
            {
                resultList = dictionary[startsWith];
            }
            else
            {
                resultList = SynergiMethods.ListUserName(Membership.ApplicationName, startsWith);
                
                dictionary.Add(startsWith, resultList);
                Cache[Globals.CacheKeys.UserNameDictionaryCacheEntry] = dictionary;
            }

            return resultList;
        }

        /// <summary>
        /// Gain access to our controls in the template control.
        /// </summary>
        private void AssignLocalPointersToControls()
        {
            _loginTable = uiLogin.FindControl("LoginTable") as HtmlTable;
            _userNameCombo = uiLogin.FindControl("UserNameComboBox") as RadComboBox;
            _userNameText = uiLogin.FindControl("UserName") as TextBox;
            _userValidator = uiLogin.FindControl("UserNameRequired") as RequiredFieldValidator;
            _loginButton = uiLogin.FindControl("LoginButton") as Button;
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            // Workaround for the stupid telerik "light" callback.
            // http://www.telerik.com/help/aspnet/combobox/combo_iscallback.html
            bool isComboCallBack = (_userNameCombo != null && _userNameCombo.IsCallBack);
            if (!IsPostBack && !isComboCallBack)
            {
                if (Request.QueryString["Logout"] != null)
                {
                    FormsAuthentication.SignOut();
                    Roles.DeleteCookie();
                    Cache[Globals.CacheKeys.ODS] = DateTime.Now.ToString();

                    Response.Redirect("Logon.aspx");
                }
                else
                {
                    Button loginButton = uiLogin.FindControl("LoginButton") as Button;
                    if (loginButton != null)
                    {
                        Page.Form.DefaultButton = loginButton.UniqueID;
                    }

                }
            }
        }

        private void UserNameCombo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox combo = (RadComboBox)o;
            if (combo != null)
            {
                if (combo.Items != null && combo.Items.Count > 0)
                {
                    combo.Items.Clear();
                }

                if (!string.IsNullOrEmpty(e.Text))
                {
                    List<string> userNames = ListMatchingUserName(e.Text.Replace("'", "''"));
                    foreach (string name in userNames)
                    {
                        RadComboBoxItem item = new RadComboBoxItem(name);
                        item.Value = name;
                        combo.Items.Add(item);
                    }
                }
            }
        }

        // If we're using the combo, put combo value into the original username textbox so normal processing can occur
        private void uiLogin_LoggingIn(object sender, LoginCancelEventArgs e)
        {
            uiLogin.UserName = _userNameCombo.AllowCustomText && _userNameCombo.Value == string.Empty ? _userNameCombo.Text : _userNameCombo.Value;
            MembershipUser userInfo = Membership.GetUser(uiLogin.UserName);
            if (userInfo == null)
            {
                e.Cancel = true;
            } 
        }

        private void uiLogin_LoginError(object sender, EventArgs e)
        {
            // There was a problem logging in the user
            // See if this user exists in the database
            MembershipUser userInfo = Membership.GetUser(uiLogin.UserName);

            if (null == userInfo)
                this.ExtraErrorInformation.Text = Synergi.Licensing.Web.UI.Properties.Resources.UserDoesNotExist + uiLogin.UserName;
            else
            {
                // See if the user is locked out or not approved
                if (!userInfo.IsApproved)
                    this.ExtraErrorInformation.Text = Synergi.Licensing.Web.UI.Properties.Resources.AccountNotApproved;
                else if (userInfo.IsLockedOut)
                    this.ExtraErrorInformation.Text = Synergi.Licensing.Web.UI.Properties.Resources.AccountLocked;
                else
                    this.ExtraErrorInformation.Text = string.Empty;
            }
        }

        #endregion
    }
}