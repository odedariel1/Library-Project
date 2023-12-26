using LibraryProject.DataBase;
using LibraryProject.Model;
using LibraryProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class ManageAccountsPage : Page
    {
        public ObservableCollection<User> Members { get; } = new ObservableCollection<User>();
        public ObservableCollection<User> Labrarains { get; } = new ObservableCollection<User>();

        public ManageAccountsPage()
        {
            this.InitializeComponent();
            UserService userService = new UserService();
            foreach(Member user in userService.GetAllMembers())
            {
                Members.Add(user);
            }
            foreach (Labrarian user in userService.GetAllLabrarians())
            {
                Labrarains.Add(user);
            }
        }
        private void BackPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LabrarainMenuPage));
        }

        private void MemberAccountsList_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TextBlock Text = (TextBlock)sender;
            Member member = Text.DataContext as Member;
            UserService.ManagedAccount = member;
            this.Frame.Navigate(typeof(ManageMemberAccountPage));

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox text= (TextBox)sender;
            UserService userService = new UserService();
            foreach (Member user in userService.GetAllMembers())
            {
                Members.Remove(user);
            }
            foreach (Member user in userService.GetAllMembers())
            {
                if(user.Username.Contains(text.Text))
                {
                    Members.Add(user);
                }
            }
            foreach (Labrarian user in userService.GetAllLabrarians())
            {
                Labrarains.Remove(user);
            }
            foreach (Labrarian user in userService.GetAllLabrarians())
            {
                if (user.Username.Contains(text.Text))
                {
                    Labrarains.Add(user);
                }
            }
            
        }
        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
        }
    }
}
