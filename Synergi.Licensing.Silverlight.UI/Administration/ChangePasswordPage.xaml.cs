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
using System.Windows.Data;

namespace Synergi.Licensing.Silverlight.UI
{
    public partial class ChangePasswordPage : Page
    {
        public ChangePasswordPage()
        {
            InitializeComponent();

            uiSubmitButton.Click += new RoutedEventHandler(uiSubmitButton_Click);
        }

        void uiSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                DataServiceHelper.ChangePasswordAsync(uiPassword.Password, uiNewPassword.Password, ChangePasswordCompleted);
            }
        }

        private bool Validate()
        {
            UiHelper.Validate(uiPassword, PasswordBox.PasswordProperty);
            UiHelper.Validate(uiNewPassword, PasswordBox.PasswordProperty);
            UiHelper.Validate(uiConfirmPassword, PasswordBox.PasswordProperty);

            return UiHelper.CheckValidate(this.uiDataForm);
        }

        void ChangePasswordCompleted(int status)
        {
            if (status == 0)
                MessageBox.Show("Change Password Complete. Your password has been changed!");
            else
                MessageBox.Show("Password change failed. Please re-enter your values and try again!");
        }

    }
}
