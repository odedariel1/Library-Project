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
using Windows.System;
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
    public sealed partial class ManageMemberAccountPage : Page
    {
        public ObservableCollection<LibraryItem> Books = new ObservableCollection<LibraryItem>();
        public ObservableCollection<LibraryItem> Journals = new ObservableCollection<LibraryItem>();
        public ManageMemberAccountPage()
        {
            this.InitializeComponent();
            Username.Text = UserService.ManagedAccount.Username;
            Name.Text = UserService.ManagedAccount.Name;
            Address.Text = UserService.ManagedAccount.Address;
            BirthDay.Text = UserService.ManagedAccount.BirthDay.ToString();
            PhoneNumber.Text = UserService.ManagedAccount.PhoneNumber.ToString();
            IsActive.Text = UserService.ManagedAccount.IsActive.ToString();
            LibraryService libraryService = new LibraryService();
            foreach (LibraryItem item in libraryService.ListOfAllTimeRentedByUserId(UserService.ManagedAccount.Id))
            {
                if (item != null)
                {
                    foreach (RentHistory history in item.ItemHistory)
                    {
                        if (item is Book)
                        {
                            Book book = new Book();
                            book.Id = item.Id;
                            book.Title = item.Title;
                            book.Author = (item as Book).Author;
                            book.Discount = item.Discount;
                            book.DaysUntilReturn = item.DaysUntilReturn;
                            book.EnterLibraryDate = item.EnterLibraryDate;
                            book.ImageUrl = item.ImageUrl;
                            book.IsAvaiable = item.IsAvaiable;
                            book.LibraryItemStatus = item.LibraryItemStatus;
                            book.PublishedAt = item.PublishedAt;
                            book.QuanityId = item.QuanityId;
                            book.RentPrice = item.RentPrice;
                            book.ItemHistory.Add(history);
                            Books.Add(book);
                        }
                        if (item is Journal)
                        {
                            Journal journal = new Journal();
                            journal.Id = item.Id;
                            journal.Title = item.Title;
                            journal.CompanyName = (item as Journal).CompanyName;
                            journal.Discount = item.Discount;
                            journal.DaysUntilReturn = item.DaysUntilReturn;
                            journal.EnterLibraryDate = item.EnterLibraryDate;
                            journal.ImageUrl = item.ImageUrl;
                            journal.IsAvaiable = item.IsAvaiable;
                            journal.LibraryItemStatus = item.LibraryItemStatus;
                            journal.PublishedAt = item.PublishedAt;
                            journal.QuanityId = item.QuanityId;
                            journal.RentPrice = item.RentPrice;
                            journal.ItemHistory.Add(history);
                            Journals.Add(journal);
                        }
                    }


                }
            }
        }

        private void BackPageButton_Click(object sender, RoutedEventArgs e)
        {
            UserService.ManagedAccount = null;
            this.Frame.Navigate(typeof(ManageAccountsPage));
        }

        private void myItem_Tapped(object sender, TappedRoutedEventArgs e)
        {

            TextBlock Text = (TextBlock)sender;
            if (Text.DataContext is Book)
            {
                Book item = Text.DataContext as Book;
                LibraryService.Currentitem = item;
                this.Frame.Navigate(typeof(RentedItemsHistoryPage));
            }
            if (Text.DataContext is Journal)
            {
                Journal item = Text.DataContext as Journal;
                LibraryService.Currentitem = item;
                this.Frame.Navigate(typeof(RentedItemsHistoryPage));
            }

        }

        private void ChangeStatusButton_Click(object sender, RoutedEventArgs e)
        {
            UserService userService = new UserService();
            if(UserService.ManagedAccount.IsActive)
            {
                userService.UnActiveUser(UserService.ManagedAccount);
                IsActive.Text = UserService.ManagedAccount.IsActive.ToString();

            }
            else
            {

                userService.ActiveUser(UserService.ManagedAccount);
                IsActive.Text = UserService.ManagedAccount.IsActive.ToString();
            }
        }
    }
}
