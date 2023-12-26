using LibraryProject.Model;
using LibraryProject.Services;
using LibraryProject.Enums;
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
    public sealed partial class MyInventoryPage : Page
    {
        public ObservableCollection<LibraryItem> Books = new ObservableCollection<LibraryItem>();
        public ObservableCollection<LibraryItem> Journals = new ObservableCollection<LibraryItem>();
        public ObservableCollection<LibraryItem> rentedItems = new ObservableCollection<LibraryItem>();
        public MyInventoryPage()
        {
            this.InitializeComponent();
            LibraryService libraryService = new LibraryService();
            foreach (LibraryItem item in libraryService.ListOfRentedByUserId(UserService.CurrentUser.Id))
            {
                if(item is Book)
                {
                    Books.Add(item);
                }
                if(item is Journal)
                {
                    Journals.Add(item);
                }
            }
            foreach (LibraryItem item in libraryService.ListOfAllTimeRentedByUserId(UserService.CurrentUser.Id))
            {
                if(item!=null)
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
                            rentedItems.Add(book);
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
                            rentedItems.Add(journal);
                        }
                    }
                }
            }
        }

        private void BackPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MemberMenuPage));
        }


        private void myItem_Tapped(object sender, TappedRoutedEventArgs e)
        {

            TextBlock Text = (TextBlock)sender;
            if (Text.DataContext is Book)
            {
                Book item = Text.DataContext as Book;
                LibraryService.Currentitem = item;
                this.Frame.Navigate(typeof(InventoryItemsDetailsPage));
            }
            if (Text.DataContext is Journal)
            {
                Journal item = Text.DataContext as Journal;
                LibraryService.Currentitem = item;
                this.Frame.Navigate(typeof(InventoryItemsDetailsPage));
            }

        }
        private void myHistory_Tapped(object sender, TappedRoutedEventArgs e)
        {

            TextBlock Text = (TextBlock)sender;
            if (Text.DataContext is Book)
            {
                Book item = Text.DataContext as Book;
                LibraryService.Currentitem = item;
                this.Frame.Navigate(typeof(MemberRentedItemHistoryPage));
            }
            if (Text.DataContext is Journal)
            {
                Journal item = Text.DataContext as Journal;
                LibraryService.Currentitem = item;
                this.Frame.Navigate(typeof(MemberRentedItemHistoryPage));
            }

        }
        private void PersonalPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PersonalDataPage));
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox text = (TextBox)sender;
            LibraryService libraryService = new LibraryService();
            foreach (LibraryItem book in libraryService.ListOfRentedByUserId(UserService.CurrentUser.Id))
            {
                if (book is Book)
                { 
                    Books.Remove(book);
                }
            }
            foreach (LibraryItem journal in libraryService.ListOfRentedByUserId(UserService.CurrentUser.Id))
            {
                if(journal is Journal)
                { 
                    Journals.Remove(journal);
                }
            }
            foreach (LibraryItem item in libraryService.ListOfAllTimeRentedByUserId(UserService.CurrentUser.Id))
            {
                rentedItems.Remove(item);
            }
                foreach (LibraryItem book in libraryService.ListOfRentedByUserId(UserService.CurrentUser.Id))
            {
                if (book.Title.Contains(text.Text) && (book is Book))
                {
                    Books.Add(book);
                }
            }
            foreach (LibraryItem journal in libraryService.ListOfRentedByUserId(UserService.CurrentUser.Id))
            {
                if (journal.Title.Contains(text.Text)&&(journal is Journal))
                {
                    Journals.Add(journal);
                }
            }
            foreach (LibraryItem item in libraryService.ListOfAllTimeRentedByUserId(UserService.CurrentUser.Id))
            {
                if (item.Title.Contains(text.Text))
                {
                    rentedItems.Add(item);
                }
            }
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
        }
    }
}
