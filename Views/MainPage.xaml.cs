using LibraryProject.DataBase;
using LibraryProject.Services;
using LibraryProject.Views;
using LibraryProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Gaming.Input;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LibraryProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private void MainPageSignInButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SignUpPage));
        }

        private void MainPageLogInButton_Click(object sender, RoutedEventArgs e)
        {
            UserService userService = new UserService();
            User user = userService.LogInUser(MainPageLogInUserName.Text, MainPageLogInPassword.Text);
            if (user != null)
            {
                UserService.CurrentUser = user;
                if (user is Labrarian)
                {
                    this.Frame.Navigate(typeof(LabrarainMenuPage));
                }
                else if (user is Member)
                {
                    this.Frame.Navigate(typeof(MemberMenuPage));
                }
            }
            else
            {
                MessageBackGround.Fill = new SolidColorBrush(Colors.DarkRed);
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

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox text = (TextBox)sender;
            if (text.Text == "Write Here")
                text.Text = "";
        }
    }
}
