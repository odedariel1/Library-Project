using LibraryProject.Model;
using LibraryProject.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibraryProject.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            this.InitializeComponent();
        }

        private void BackToLoginPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            if (SignInUserNameField.Text != "Write Here" && SignInUserNameField.Text.Length > 0 &&
                SignInPasswordField.Text != "Write Here" && SignInPasswordField.Text.Length > 0 &&
                SignInNameField.Text != "Write Here" && SignInNameField.Text.Length > 0 &&
                SignInPhoneField.Text != "Write Here" && SignInPhoneField.Text.Length == 10 &&
                SignInAddressField.Text != "Write Here" && SignInAddressField.Text.Length > 0 &&
                SignInBirthDayField.Date.Year>=1923 && SignInBirthDayField.Date.Month>0 && SignInBirthDayField.Date.Day>0)
            {
                Member member = new Member();
                member.Username = SignInUserNameField.Text;
                member.Password = SignInPasswordField.Text;
                member.Name = SignInNameField.Text;
                member.Address = SignInAddressField.Text;
                member.PhoneNumber = SignInPhoneField.Text;
                member.BirthDay = new DateTime(SignInBirthDayField.Date.Year, SignInBirthDayField.Date.Month, SignInBirthDayField.Date.Day);
                bool isCatch = false;
                try
                {
                    UserService userService = new UserService();
                    userService.SignUpUser(member);
                }
                catch(Exception ex)
                {
                    isCatch = true;
                    MessageContent.Text = ex.Message;
                }
                if (!isCatch)
                MessageContent.Text = "Your Account Have Been Created!";
                MessageBackGround.Fill = new SolidColorBrush(Colors.LightGreen);
                MessageConfirmButton.Visibility = Visibility.Visible;
                MessageContent.Visibility = Visibility.Visible;
                MessageBackGround.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBackGround.Fill = new SolidColorBrush(Colors.DarkRed);
                MessageContent.Text = "Something Went Wrong. Check Details";
                MessageConfirmButton.Visibility = Visibility.Visible;
                MessageContent.Visibility = Visibility.Visible;
                MessageBackGround.Visibility = Visibility.Visible;
            }
        }
        private void MessageConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            MessageConfirmButton.Visibility = Visibility.Collapsed;
            MessageContent.Visibility = Visibility.Collapsed;
            MessageBackGround.Visibility = Visibility.Collapsed;
        }
        private void TextBox_OnBeforeTextChanging(TextBox sender,TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
        private void TextBox_OnTextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            //Save the position of the selection, to prevent the cursor to jump to the start
            int pos = sender.SelectionStart;
            sender.Text = new String(sender.Text.Where(char.IsDigit).ToArray());
            sender.SelectionStart = pos;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox text = (TextBox) sender;
            if(text.Text == "Write Here")
            text.Text = "";
        }
    }
}
