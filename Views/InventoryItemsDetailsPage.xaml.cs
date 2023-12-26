using LibraryProject.Enums;
using LibraryProject.Model;
using LibraryProject.Services;
using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibraryProject.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InventoryItemsDetailsPage : Page
    {
        public InventoryItemsDetailsPage()
        {
            this.InitializeComponent();
            TitleField.Text = LibraryService.Currentitem.Title;
            if (LibraryService.Currentitem is Book)
            {
                Book item = LibraryService.Currentitem as Book;
                AuthorAndCompanyField.Text = item.Author;
            }
            if (LibraryService.Currentitem is Journal)
            {
                Journal item = LibraryService.Currentitem as Journal;
                AuthorAndCompanyField.Text = item.CompanyName;
            }
            PriceField.Text = LibraryService.Currentitem.RentPrice.ToString();
            PublishedField.Text = LibraryService.Currentitem.PublishedAt.Date.ToString();
            GenreField.Text = LibraryService.Currentitem.ItemGenre.ToString();
            DaysOfRentField.Text = LibraryService.Currentitem.DaysUntilReturn.ToString();
            QuantityField.Text = LibraryService.Currentitem.QuanityId.ToString();
            BitmapImage image = new BitmapImage(new Uri($"ms-appx:///Assets/{LibraryService.Currentitem.ImageUrl}"));
            ImageField.Source = image;
            StatusField.Text = LibraryService.Currentitem.LibraryItemStatus.ToString();
            if (LibraryService.Currentitem.LibraryItemStatus == ItemStatus.Rented)
            {
                DateTime dateTime = LibraryService.Currentitem.ItemHistory.Last().RentDay;
                RentDateField.Visibility = Visibility.Visible;
                ReturnDateField.Visibility = Visibility.Visible;
                RentDateField.Text = LibraryService.Currentitem.ItemHistory.Last().RentDay.ToString();
                ReturnDateField.Text = dateTime.AddDays(LibraryService.Currentitem.DaysUntilReturn).ToString();
                if(dateTime.AddDays(LibraryService.Currentitem.DaysUntilReturn) <= DateTime.Now)
                {
                    StatusField.Text = $"{StatusField.Text} And Late";
                    StatusField.Foreground = new SolidColorBrush(Colors.Red);
                    ReturnDateField.Foreground= new SolidColorBrush(Colors.Red);
                }
            }
            else
            {
                RentDateField.Visibility = Visibility.Collapsed;
                ReturnDateField.Visibility = Visibility.Collapsed;
            }
        }
        private void ButtonBackPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MyInventoryPage));
        }

        private void ReturnItem_Click(object sender, RoutedEventArgs e)
        {
            LibraryService libraryService = new LibraryService();
            libraryService.ReturnLibraryItem(LibraryService.Currentitem);
            MessageConfirmButton.Visibility = Visibility.Visible;
            MessageContent.Visibility = Visibility.Visible;
            MessageBackGround.Visibility = Visibility.Visible;
        }
        private void MessageConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            MessageConfirmButton.Visibility = Visibility.Collapsed;
            MessageContent.Visibility = Visibility.Collapsed;
            MessageBackGround.Visibility = Visibility.Collapsed;
            LibraryService.Currentitem = null;
            this.Frame.Navigate(typeof(MyInventoryPage));
        }
    } 
}
