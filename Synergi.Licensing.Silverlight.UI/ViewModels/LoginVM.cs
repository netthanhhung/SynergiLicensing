using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Synergi.Licensing.Silverlight.UI
{
    public class LoginVM
    {
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Password is required.");
                }

                password = value;
            }
        }

        private string newPassword;
        public string NewPassword
        {
            get { return newPassword; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("New Password is required.");
                }

                newPassword = value;
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Confirm New Password is required.");
                }

                if (!value.Equals(newPassword, StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("The Confirm New Password must match the New Password entry.");
                }

                confirmPassword = value;
            }
        }
    }
}
