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
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;
using System.Security.Cryptography;
using nsTooltips = Silverlight.Controls.ToolTips;
using Synergi.Licensing.Common;

namespace Synergi.Licensing.Silverlight.UI
{
    public partial class UserAccount : UserControl
    {
        private static class UserMessages
        {
            internal const string DefaultPasswordQuestion = "Not set yet";
            internal const string DefaultPasswordAnswer = "Not set yet";
            internal const string NewRecord = "New Record";
            internal const string CreatedInfo = "Created {0}, last updated {1}.";
            internal const string LockedOutMessage = "Account was locked out on {0}. Please uncheck box, and press Save to unlock account.";
            internal const string OfflineTooltip = "User is currently offline";
            internal const string OnlineTooltip = "User is currently online.";
            internal const string UserUnlocked = "User has been unlocked.";
            internal const string UserPasswordEmpty = "Username and Password should not be empty.";
            internal const string NewGenPassword = "New password for user is '{0}', please advise user";
            internal const string QuestionPasswordEmpty = "Please input password question and answer.";
            internal const string InputPasswordEmpty = "Please input password to change password question and answer.";
            internal const string InputPasswordIncorrect = "Cannot change password question and answer because the password is incorrect";
        }
        /*  ======================================================================            
         *      EVENTS AND DELEGATES
         *  ====================================================================== */
        internal event EventHandler SaveUserAccountComplete;

        #region Properties
        //private const string DisplayPassword = "display";
        private const string OnlineImage = "/Synergi.Licensing.Silverlight.UI;component/Images/online.png";
        private const string OfflineImage = "/Synergi.Licensing.Silverlight.UI;component/Images/offline.png";
        private const string TransparentImage = "/Synergi.Licensing.Silverlight.UI;component/Images/Transparent.png";

        private List<AspUser> _aspUsers = new List<AspUser>();        

        public Brush BackgroundColour {
            get { return panelUserAccount.Background; }
            set { panelUserAccount.Background = value; } 
        }
        public bool IsEditable { get; set; }
        public AspUser SavedAspUser { get; set; }
        
        #endregion
        
        #region Constructors
        public UserAccount()
        {
            InitializeComponent();

            uiUsers.IsFilteringEnabled = true;
            uiUsers.KeyUp += new KeyEventHandler(uiUsers_KeyUp);
            uiUsers.OpenDropDownOnFocus = true;
            uiUsers.TextSearchMode = TextSearchMode.Contains;
            uiUsers.LostFocus += new RoutedEventHandler(uiUsers_LostFocus);

            btnSave.Click += new RoutedEventHandler(btnSave_Click);
            btnUnlock.Click += new RoutedEventHandler(btnUnlock_Click);

            chkChangePasswordQuestionAnswer.Checked += new RoutedEventHandler(chkChangePasswordQuestionAnswer_Checked);
            chkChangePasswordQuestionAnswer.Unchecked += new RoutedEventHandler(chkChangePasswordQuestionAnswer_Checked);
        }
        
        void chkChangePasswordQuestionAnswer_Checked(object sender, RoutedEventArgs e)
        {
            txtPasswordQuestion.Visibility = txtPasswordQuestionLabel.Visibility
                = txtPasswordAnswer.Visibility = txtPasswordAnswerLabel.Visibility                
                = chkChangePasswordQuestionAnswer.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            txtInputPasswordLbl.Visibility = txtInputPassword.Visibility = System.Windows.Visibility.Collapsed;
            if (uiUsers.SelectedValue != null && (Guid)uiUsers.SelectedValue != Guid.Empty)
            {
                Guid userId = (Guid)uiUsers.SelectedValue;
                AspUser aspUser = _aspUsers.FirstOrDefault(i => i.UserId == userId);
                if (aspUser != null)
                {
                    txtInputPasswordLbl.Visibility = txtInputPassword.Visibility
                        = chkChangePasswordQuestionAnswer.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        public void ResetControlStatus()
        {
            uiImageOnline.Visibility = chkResetPassword.Visibility = txtResetPasswordInfo.Visibility = System.Windows.Visibility.Collapsed;
            btnSave.IsEnabled = this.IsEditable;
            btnUnlock.IsEnabled = false;
            txtPassword.Password = string.Empty;
            txtPassword.IsEnabled = true;
            chkChangePasswordQuestionAnswer.IsChecked = false;
            //chkChangePasswordQuestionAnswer.Visibility = System.Windows.Visibility.Collapsed;
            txtPasswordQuestion.Text = UserMessages.DefaultPasswordQuestion;
            txtPasswordAnswer.Text = UserMessages.DefaultPasswordAnswer;
            txtInputPassword.Password = string.Empty;
            txtEmail.Text = string.Empty;
            chkChangePasswordQuestionAnswer_Checked(null, null);
            txtResetPasswordInfo.Text = string.Empty;
            ucInformation.InfoMessage = UserMessages.NewRecord;            
        }

        public void RebindData()
        {
            Globals.IsBusy = true;
            ResetControlStatus();
            ucInformation.InfoMessage = UserMessages.NewRecord;
            DataServiceHelper.ListAspUserAsync(null, ListAllAspUserCompleted);
        }
       
        void ListAllAspUserCompleted(List<AspUser> userList)
        {
            _aspUsers = userList;
            FillUserNameCombobox();
            Globals.IsBusy = false;
        }

        void FillUserNameCombobox()
        {
            Dictionary<Guid, string> userItemSource = new Dictionary<Guid, string>();
            foreach (AspUser user in _aspUsers)
            {
                if (!userItemSource.ContainsKey(user.UserId))
                {
                    userItemSource.Add(user.UserId, user.UserName);
                }
            }
            uiUsers.ItemsSource = userItemSource;
            RebindUserAccountData();
        }


        void uiUsers_KeyUp(object sender, KeyEventArgs e)
        {
            Key keyCode = e.Key;
            if (keyCode == Key.Tab || keyCode == Key.Enter)
            {
                if (keyCode == Key.Enter)
                {
                    btnSave.Focus();
                }
                //RebindUserRoleAuthList();

            }
        }

        void uiUsers_LostFocus(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(uiUsers.Text))
                RebindUserAccountData();
        }

        public void RebindUserAccountData()
        {
            bool exist = false;
            txtInputPassword.Password = string.Empty;
            if (uiUsers.SelectedValue != null && (Guid)uiUsers.SelectedValue != Guid.Empty)
            {
                Guid userId = (Guid)uiUsers.SelectedValue;
                AspUser aspUser = _aspUsers.FirstOrDefault(i => i.UserId == userId);
                if (aspUser != null)
                {
                    exist = true;
                    uiImageOnline.Visibility = chkResetPassword.Visibility = System.Windows.Visibility.Visible;
                    txtPassword.Password = aspUser.Password;
                    txtPassword.IsEnabled = false;
                    txtEmail.Text = string.Empty;

                    if (!string.IsNullOrEmpty(aspUser.Email))
                    {
                        txtEmail.Text = aspUser.Email;
                    }
                    //uiEmployees.IsEnabled = false;
                    chkAccountApproved.IsChecked = aspUser.IsApproved;
                    chkResetPassword.IsChecked = false;
                    txtResetPasswordInfo.Text = string.Empty;
                    if (aspUser.IsLockedOut)
                    {
                        chkResetPassword.IsEnabled = false;
                        chkAccountApproved.IsEnabled = false;
                        btnUnlock.IsEnabled = this.IsEditable;
                        ucInformation.InfoMessage = string.Format(UserMessages.LockedOutMessage, aspUser.LastLockoutDate);
                    }
                    else
                    {
                        btnUnlock.IsEnabled = false;
                        chkAccountApproved.IsEnabled = true;
                        chkResetPassword.IsEnabled = true;
                        ucInformation.InfoMessage = string.Format(UserMessages.CreatedInfo, aspUser.CreationDate.ToString(), aspUser.LastActivityDate.ToString());
                    }
                    if(!string.IsNullOrEmpty(aspUser.PasswordQuestion))
                        txtPasswordQuestion.Text = aspUser.PasswordQuestion;
                    if (!string.IsNullOrEmpty(aspUser.PasswordAnswer))
                    txtPasswordAnswer.Text = aspUser.PasswordAnswer;

                    chkChangePasswordQuestionAnswer.IsChecked = false;

                    if (aspUser.IsOnline)  /*This displays after adding new Admins incorrectly, as the UserID reset triggers this method, ie after saving a new siteAdmin as Org admin.*/
                    {                   /*Dont know where else it is used though, so shall leave for now*/
                        uiImageOnline.Source = new BitmapImage(new Uri(OnlineImage, UriKind.Relative));
                        nsTooltips.ToolTip tooltip = new nsTooltips.ToolTip()
                        {
                            DisplayTime = new Duration(TimeSpan.FromSeconds(10)),
                            InitialDelay = new Duration(TimeSpan.FromMilliseconds(0)),
                            Content = UserMessages.OnlineTooltip
                        };
                        nsTooltips.ToolTipService.SetToolTip(uiImageOnline, tooltip);
                    }
                    else
                    {
                        uiImageOnline.Source = new BitmapImage(new Uri(OfflineImage, UriKind.Relative));
                        nsTooltips.ToolTip tooltip = new nsTooltips.ToolTip()
                        {
                            DisplayTime = new Duration(TimeSpan.FromSeconds(10)),
                            InitialDelay = new Duration(TimeSpan.FromMilliseconds(0)),
                            Content = UserMessages.OfflineTooltip
                        };
                        nsTooltips.ToolTipService.SetToolTip(uiImageOnline, tooltip);
                    }
                }
            }

            if (!exist)
            {
                ResetControlStatus();                
            }
        }

        void btnUnlock_Click(object sender, RoutedEventArgs e)
        {
            if (uiUsers.SelectedValue != null && (Guid)uiUsers.SelectedValue != Guid.Empty)
            {
                Guid userId = (Guid)uiUsers.SelectedValue;
                AspUser aspUser = _aspUsers.FirstOrDefault(i => i.UserId == userId);
                if (aspUser != null)
                {
                    Globals.IsBusy = true;
                    DataServiceHelper.UnlockAspUserAsync(aspUser, UnlockAspUserCompleted);
                }
            }
        }

        void UnlockAspUserCompleted(AspUser aspUser)
        {
            for (int i = 0; i < _aspUsers.Count; i++)
            {
                if (_aspUsers[i].UserId == aspUser.UserId)
                {
                    _aspUsers[i] = aspUser;
                    RebindUserAccountData();
                    MessageBox.Show(UserMessages.UserUnlocked);
                    break;
                }
            }
            Globals.IsBusy = true;
        }

        void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (chkChangePasswordQuestionAnswer.IsChecked == true
                 && (string.IsNullOrEmpty(txtPasswordQuestion.Text) || string.IsNullOrEmpty(txtPasswordAnswer.Text)))
            {
                MessageBox.Show(UserMessages.QuestionPasswordEmpty, Globals.UserMessages.ValidationError, MessageBoxButton.OK);
                return;
            }

            if (uiUsers.SelectedValue != null && (Guid)uiUsers.SelectedValue != Guid.Empty)//Means update user
            {
                Guid userId = (Guid)uiUsers.SelectedValue;
                AspUser aspUser = _aspUsers.FirstOrDefault(i => i.UserId == userId);
                if (aspUser != null)
                {
                    Globals.IsBusy = true;
                    aspUser = GetSaveAspUser(aspUser);
                    DataServiceHelper.SaveAspUserAsync(aspUser, SaveAspUserCompleted);
                }
                if (chkChangePasswordQuestionAnswer.IsChecked == true
                    && string.IsNullOrEmpty(txtInputPassword.Password))
                {
                    MessageBox.Show(UserMessages.InputPasswordEmpty, Globals.UserMessages.ValidationError, MessageBoxButton.OK);
                    return;
                }
            }
            else//means create new user
            {
                if (string.IsNullOrEmpty(uiUsers.Text) || string.IsNullOrEmpty(txtPassword.Password))
                {
                    MessageBox.Show(UserMessages.UserPasswordEmpty, Globals.UserMessages.ValidationError, MessageBoxButton.OK);
                    return;
                }
                AspUser newUser = new AspUser();
                newUser.UserName = uiUsers.Text;
                newUser.Password = txtPassword.Password;
                newUser = GetSaveAspUser(newUser);
                if (string.IsNullOrEmpty(newUser.PasswordQuestion))
                    newUser.PasswordQuestion = UserMessages.DefaultPasswordQuestion;
                if (string.IsNullOrEmpty(newUser.PasswordAnswer))
                    newUser.PasswordAnswer = UserMessages.DefaultPasswordAnswer;
                Globals.IsBusy = true;
                DataServiceHelper.SaveAspUserAsync(newUser, CreateAspUserCompleted);
            }
        }

        private AspUser GetSaveAspUser(AspUser aspUser)
        {
            aspUser.IsResetPassword = chkResetPassword.IsChecked == true;
            aspUser.IsApproved = chkAccountApproved.IsChecked == true;
            aspUser.Email = txtEmail.Text;
            if (chkChangePasswordQuestionAnswer.IsChecked == true)
            {
                aspUser.PasswordQuestion = txtPasswordQuestion.Text;
                aspUser.PasswordAnswer = txtPasswordAnswer.Text;
                if (string.IsNullOrEmpty(aspUser.PasswordQuestion))
                    aspUser.PasswordQuestion = UserMessages.DefaultPasswordQuestion;
                if (string.IsNullOrEmpty(aspUser.PasswordAnswer))
                    aspUser.PasswordAnswer = UserMessages.DefaultPasswordAnswer;
                aspUser.InputPassword = txtInputPassword.Password;
            }
            else
            {
                aspUser.PasswordQuestion = string.Empty;
                aspUser.PasswordAnswer = string.Empty;
            }
            
            aspUser.ErrorMessage = string.Empty;
            return aspUser;
        }

        void SaveAspUserCompleted(AspUser aspUser)
        {
            for (int i = 0; i < _aspUsers.Count; i++)
            {
                if (_aspUsers[i].UserId == aspUser.UserId)
                {
                    if (aspUser.IsSavedQAError)
                    {
                        MessageBox.Show(UserMessages.InputPasswordIncorrect);
                    }
                    else if (!string.IsNullOrEmpty(aspUser.ErrorMessage))
                    {
                        MessageBox.Show(aspUser.ErrorMessage);
                    }
                    else
                    {
                        _aspUsers[i] = aspUser;
                        SavedAspUser = aspUser;
                        if (SaveUserAccountComplete != null)
                        {
                            SaveUserAccountComplete(this, null);
                        }
                        RebindUserAccountData();
                        if (!string.IsNullOrEmpty(aspUser.NewGenPassword))
                        {
                            txtResetPasswordInfo.Text = string.Format(UserMessages.NewGenPassword, aspUser.NewGenPassword);
                            txtResetPasswordInfo.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            txtResetPasswordInfo.Visibility = System.Windows.Visibility.Collapsed;
                        }
                        
                        MessageBox.Show(Globals.UserMessages.RecordsSaved);
                    }

                    break;
                }
            }
            Globals.IsBusy = false;
        }

        void CreateAspUserCompleted(AspUser aspUser)
        {
            if (!string.IsNullOrEmpty(aspUser.ErrorMessage))
            {
                Globals.IsBusy = false;
                MessageBox.Show(aspUser.ErrorMessage);
                return;
            }
            _aspUsers.Add(aspUser);
            SavedAspUser = aspUser;
            if (SaveUserAccountComplete != null)
            {
                SaveUserAccountComplete(this, null);
            }
            Dictionary<Guid, string> userItemSource = uiUsers.ItemsSource as Dictionary<Guid, string>;
            userItemSource.Add(aspUser.UserId, aspUser.UserName);
            userItemSource.OrderBy(i => i.Value);
            uiUsers.ItemsSource = null;
            uiUsers.ItemsSource = userItemSource;
            uiUsers.SelectedValue = aspUser.UserId;
            RebindUserAccountData();
            Globals.IsBusy = false;
            MessageBox.Show(Globals.UserMessages.RecordsSaved);
        }     
        #endregion

    }
}
