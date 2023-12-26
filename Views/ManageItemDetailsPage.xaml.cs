using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LibraryProject.DataBase;
using LibraryProject.Model;
using LibraryProject.Services;
using LibraryProject.Enums;
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
using System.Drawing;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibraryProject.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ManageItemDetailsPage : Page
    {
        public ManageItemDetailsPage()
        {
            this.InitializeComponent();
            TitleField.Text = LibraryService.Currentitem.Title;
            if(LibraryService.Currentitem is Book)
            {
               Book item = LibraryService.Currentitem as Book;
               AuthorAndCompanyField.Text = item.Author;
            }
            if (LibraryService.Currentitem is Journal)
            {
               Journal item = LibraryService.Currentitem as Journal;
               AuthorAndCompanyField.Text = item.CompanyName;
            }
            PriceField.Text= LibraryService.Currentitem.RentPrice.ToString();
            PublishedField.Date= LibraryService.Currentitem.PublishedAt.Date;
            foreach (ComboBoxItem cbi in GenreField.Items)
            {
                if (cbi.Content as String == LibraryService.Currentitem.ItemGenre.ToString())
                {
                    GenreField.SelectedItem = cbi;
                    break;
                }
            }
            DaysOfRentField.Text = LibraryService.Currentitem.DaysUntilReturn.ToString();
            QuantityField.Text = LibraryService.Currentitem.QuanityId.ToString();
            ImageFilePath.Text = LibraryService.Currentitem.ImageUrl;
            BitmapImage image = new BitmapImage(new Uri($"ms-appx:///Assets/{LibraryService.Currentitem.ImageUrl}"));
            ImageProduce.Source = image;
            StatusField.Text = LibraryService.Currentitem.LibraryItemStatus.ToString();
            if (LibraryService.Currentitem.LibraryItemStatus==ItemStatus.Rented) 
            {
                DateTime dateTime = LibraryService.Currentitem.ItemHistory.Last().RentDay;
                RentDateField.Visibility = Visibility.Visible;
                ReturnDateField.Visibility = Visibility.Visible;
                RentDateField.Text = LibraryService.Currentitem.ItemHistory.Last().RentDay.ToString();
                ReturnDateField.Text = dateTime.AddDays(LibraryService.Currentitem.DaysUntilReturn).ToString();
            }
            else
            {
                RentDateField.Visibility = Visibility.Collapsed;
                ReturnDateField.Visibility = Visibility.Collapsed;
            }
            IsAvailableField.Text = LibraryService.Currentitem.IsAvaiable.ToString();
            if(LibraryService.Currentitem.IsAvaiable)
            {
                DeleteButton.Content = "Delete Item";
            }
            else
            {
                DeleteButton.Content = "UnDelete Item";
            }
        }
        private void ButtonBackPage_Click(object sender, RoutedEventArgs e)
        {
            LibraryService.Currentitem = null;
            this.Frame.Navigate(typeof(ManageLibraryItemsPage));
        }
        private async void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                ImageFilePath.Text = file.Name;
                ImageProduce.Source = new BitmapImage(new Uri($"ms-appx:///Assets/{file.Name}"));
            }
            else
            {
                ImageFilePath.Text = "Operation cancelled.";
            }
        }

        private void MessageConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            MessageConfirmButton.Visibility = Visibility.Collapsed;
            MessageContent.Visibility = Visibility.Collapsed;
            MessageBackGround.Visibility = Visibility.Collapsed;
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (LibraryService.Currentitem.IsAvaiable)
            {
                LibraryService.Currentitem.IsAvaiable = false;
                DeleteButton.Content = "UnDelete Item";
            }
            else
            {
                LibraryService.Currentitem.IsAvaiable = true;
                DeleteButton.Content = "Delete Item";
            }
            IsAvailableField.Text = LibraryService.Currentitem.IsAvaiable.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (TitleField.Text != "" && AuthorAndCompanyField.Text != "" && PriceField.Text != "" && PublishedField.Date != null && GenreField.SelectedItem != null && DaysOfRentField.Text != "" && QuantityField.Text != "" && ImageFilePath.Text != "")
            {
                LibraryService.Currentitem.Title = TitleField.Text;
                if (LibraryService.Currentitem is Book)
                {
                    Book item = LibraryService.Currentitem as Book;
                    item.Author = AuthorAndCompanyField.Text;
                }
                if (LibraryService.Currentitem is Journal)
                {
                    Journal item = LibraryService.Currentitem as Journal;
                    item.CompanyName = AuthorAndCompanyField.Text;
                }
                LibraryService.Currentitem.RentPrice = int.Parse(PriceField.Text);
                LibraryService.Currentitem.PublishedAt = new DateTime(PublishedField.Date.Year, PublishedField.Date.Month, PublishedField.Date.Day);
                ComboBoxItem combox = new ComboBoxItem();
                combox = GenreField.SelectedItem as ComboBoxItem;
                switch (combox.Content)
                {
                    case "Action":
                        {
                            LibraryService.Currentitem.ItemGenre = Genre.Action;
                            break;
                        }
                    case "Comedy":
                        {
                            LibraryService.Currentitem.ItemGenre = Genre.Comedy;
                            break;
                        }
                    case "Drama":
                        {
                            LibraryService.Currentitem.ItemGenre = Genre.Drama;
                            break;
                        }
                    case "Fantasy":
                        {
                            LibraryService.Currentitem.ItemGenre = Genre.Fantasy;
                            break;
                        }
                    case "Kids":
                        {
                            LibraryService.Currentitem.ItemGenre = Genre.Kids;
                            break;
                        }
                    case "News":
                        {
                            LibraryService.Currentitem.ItemGenre = Genre.News;
                            break;
                        }
                    case "Romantic":
                        {
                            LibraryService.Currentitem.ItemGenre = Genre.Romantic;
                            break;
                        }
                    default: break;
                }
                LibraryService.Currentitem.DaysUntilReturn = int.Parse(DaysOfRentField.Text);
                LibraryService.Currentitem.QuanityId = int.Parse(QuantityField.Text);
                LibraryService.Currentitem.ImageUrl = ImageFilePath.Text;
                LibraryService libraryService = new LibraryService();
                libraryService.ChangeLibraryItemDetails(LibraryService.Currentitem);
                MessageContent.Text = "The Details Have Been Saved!";
            }
            else
            {
                MessageContent.Text = "Please Fill All The Fields!";
                MessageBackGround.Fill = new SolidColorBrush(Colors.DarkRed);
            }
            MessageConfirmButton.Visibility = Visibility.Visible;
            MessageContent.Visibility = Visibility.Visible;
            MessageBackGround.Visibility = Visibility.Visible;
        }

        private void NumbersOnly_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void NumbersOnly_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            //Save the position of the selection, to prevent the cursor to jump to the start
            int pos = sender.SelectionStart;
            sender.Text = new String(sender.Text.Where(char.IsDigit).ToArray());
            sender.SelectionStart = pos;
        }
    }
}
