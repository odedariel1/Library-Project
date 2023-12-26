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
    public sealed partial class ManageLibraryItemsPage : Page
    {
        public ObservableCollection<LibraryItem> Books = new ObservableCollection<LibraryItem>();
        public ObservableCollection<LibraryItem> Journals = new ObservableCollection<LibraryItem>();
        public ManageLibraryItemsPage()
        {
            this.InitializeComponent();
            LibraryService libraryService = new LibraryService();
            foreach (Book book in libraryService.GetAllBooks())
            {
                Books.Add(book);
            }
            foreach (Journal journal in libraryService.GetAllJournals())
            {
                Journals.Add(journal);
            }
        }
        private void BackPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LabrarainMenuPage));
        }

        private void myItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
            TextBlock Text = (TextBlock)sender;
            if(Text.DataContext is Book)
            {
                Book item = Text.DataContext as Book;
                LibraryService.Currentitem = item;
                this.Frame.Navigate(typeof(ManageItemDetailsPage));
            }
            if(Text.DataContext is Journal)
            {
                Journal item = Text.DataContext as Journal;
                LibraryService.Currentitem = item;
                this.Frame.Navigate(typeof(ManageItemDetailsPage));
            }
                
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddItemPage));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox text = (TextBox)sender;
            LibraryService libraryService = new LibraryService();
            foreach (Book book in libraryService.GetAllFreeBooks())
            {
                Books.Remove(book);
            }
            foreach (Journal journal in libraryService.GetAllJournals())
            {
                Journals.Remove(journal);
            }
            foreach (Book book in libraryService.GetAllFreeBooks())
            {
                if (book.Title.Contains(text.Text))
                {
                    Books.Add(book);
                }
            }
            foreach (Journal journal in libraryService.GetAllJournals())
            {
                if (journal.Title.Contains(text.Text))
                {
                    Journals.Add(journal);
                }
            }
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
        }
    }
}
