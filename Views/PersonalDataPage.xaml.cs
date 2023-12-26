using LibraryProject.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class PersonalDataPage : Page
    {
        public PersonalDataPage()
        {
            this.InitializeComponent();
            Username.Text = UserService.CurrentUser.Username;
            Name.Text = UserService.CurrentUser.Name;
            Address.Text = UserService.CurrentUser.Address;
            BirthDayField.Date = UserService.CurrentUser.BirthDay;
            PhoneNumber.Text = UserService.CurrentUser.PhoneNumber;
            Password.Text = UserService.CurrentUser.Password;
        }

        private void BackPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MyInventoryPage));
        }

        private void ChangeDetails_Click(object sender, RoutedEventArgs e)
        {
            if (Username.Text.Length>1 && Name.Text.Length > 1 && Address.Text.Length >1 
                && BirthDayField.Date < DateTime.Now.AddYears(-3) && PhoneNumber.Text.Length == 10 && Password.Text.Length>0)
            {
                UserService.CurrentUser.Username = Username.Text;
                UserService.CurrentUser.Name = Name.Text;
                UserService.CurrentUser.Address = Address.Text;
                UserService.CurrentUser.BirthDay = new DateTime(BirthDayField.Date.Year, BirthDayField.Date.Month, BirthDayField.Date.Day);
                UserService.CurrentUser.PhoneNumber = PhoneNumber.Text;
                UserService.CurrentUser.Password = Password.Text;
                UserService userService = new UserService();
                userService.ChangeDetails(UserService.CurrentUser);
                MessageContent.Text = "Your Data Have Been Updated!";
                MessageConfirmButton.Visibility = Visibility.Visible;
                MessageContent.Visibility = Visibility.Visible;
                MessageBackGround.Visibility = Visibility.Visible;
                MessageBackGround.Fill = new SolidColorBrush(Colors.LightGreen);
            }
            else
            {
                MessageContent.Text = "Please Fill All The Fields!";
                MessageBackGround.Fill = new SolidColorBrush(Colors.DarkRed);
                MessageConfirmButton.Visibility = Visibility.Visible;
                MessageContent.Visibility = Visibility.Visible;
                MessageBackGround.Visibility = Visibility.Visible;
            }
        }
        private void TextBox_OnBeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
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

        private void MessageConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            MessageConfirmButton.Visibility = Visibility.Collapsed;
            MessageContent.Visibility = Visibility.Collapsed;
            MessageBackGround.Visibility = Visibility.Collapsed;
        }
    }
}
