using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace NavProject_Drawing.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login() => InitializeComponent();
        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
             
        }

        private void SignUpPassValidateTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (SignUpPassTextBox.Password != SignUpPassValidateTextBox.Password)
            {
                SignUpPassValidateTextBox.BorderBrush = Brushes.Red;
                SignUpPassValidateTextBox.BorderThickness = new Thickness(3);
                SignUpPassValidateTextBox.ToolTip = "Password doens't match";
            }
            else
            {
                SignUpPassValidateTextBox.BorderBrush = Brushes.Black;
                SignUpPassValidateTextBox.BorderThickness = new Thickness(1);
                SignUpPassValidateTextBox.ToolTip = "Ok";
            }
        }
    }
}
