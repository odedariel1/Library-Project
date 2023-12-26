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
    public sealed partial class MemberRentedItemHistoryPage : Page
    {
        public MemberRentedItemHistoryPage()
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
            LibraryService libraryService = new LibraryService();
            RentHistory rentHistory = libraryService.DateOfRent(LibraryService.Currentitem,UserService.CurrentUser.Id);
            DateTime dateTime = LibraryService.Currentitem.ItemHistory.Last().RentDay;
            RentDateField.Visibility = Visibility.Visible;
            ReturnDateField.Visibility = Visibility.Visible;
            RentDateField.Text = LibraryService.Currentitem.ItemHistory.Last().RentDay.ToString();
            if (LibraryService.Currentitem.ItemHistory.Last().DayOfReturn == DateTime.MinValue)
                ReturnDateField.Text = "Not Returned Yet";
            else
                ReturnDateField.Text = rentHistory.DayOfReturn.ToString();

        }
        private void ButtonBackPage_Click(object sender, RoutedEventArgs e)
        {
            LibraryService.Currentitem = null;
            this.Frame.Navigate(typeof(MyInventoryPage));
        }

    }
}
