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
    public sealed partial class AddItemPage : Page
    {
        public AddItemPage()
        {
            this.InitializeComponent();
        }

        private void ButtonBackPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ManageLibraryItemsPage));
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (TitleField.Text != "" && AuthorAndCompanyField.Text != "" && PriceField.Text != "" && PublishedField.Date != null && TypeField.SelectedItem != null && GenreField.SelectedItem != null && DaysOfRentField.Text != "" && QuantityField.Text != "" && ImageFilePath.Text != "" && ImageFilePath.Text != "Operation cancelled.")
            {
                ComboBoxItem comboboxtype = TypeField.SelectedItem as ComboBoxItem;
                if (comboboxtype.Content.ToString() == "Book")
                {
                    Book item = new Book();
                    item.Title = TitleField.Text;
                    item.Author = AuthorAndCompanyField.Text;
                    item.RentPrice = int.Parse(PriceField.Text);
                    item.PublishedAt = new DateTime(PublishedField.Date.Year, PublishedField.Date.Month, PublishedField.Date.Day);
                    ComboBoxItem combox = new ComboBoxItem();
                    combox = GenreField.SelectedItem as ComboBoxItem;
                    switch (combox.Content)
                    {
                        case "Action":
                            {
                                item.ItemGenre = Genre.Action;
                                break;
                            }
                        case "Comedy":
                            {
                                item.ItemGenre = Genre.Comedy;
                                break;
                            }
                        case "Drama":
                            {
                                item.ItemGenre = Genre.Drama;
                                break;
                            }
                        case "Fantasy":
                            {
                                item.ItemGenre = Genre.Fantasy;
                                break;
                            }
                        case "Kids":
                            {
                                item.ItemGenre = Genre.Kids;
                                break;
                            }
                        case "News":
                            {
                                item.ItemGenre = Genre.News;
                                break;
                            }
                        case "Romantic":
                            {
                                item.ItemGenre = Genre.Romantic;
                                break;
                            }
                        default: break;
                    }
                    item.DaysUntilReturn = int.Parse(DaysOfRentField.Text);
                    item.QuanityId = int.Parse(QuantityField.Text);
                    item.ImageUrl = ImageFilePath.Text;
                    LibraryService libraryService = new LibraryService();
                    libraryService.AddLibraryItem(item);
                    MessageContent.Text = "The Item Have Been Added!";
                    MessageBackGround.Fill = new SolidColorBrush(Colors.LightGreen);
                }
                if (comboboxtype.Content.ToString() == "Journal")
                {
                    Journal item = new Journal();
                    item.Title = TitleField.Text;
                    item.CompanyName = AuthorAndCompanyField.Text;
                    item.RentPrice = int.Parse(PriceField.Text);
                    item.PublishedAt = new DateTime(PublishedField.Date.Year, PublishedField.Date.Month, PublishedField.Date.Day);
                    ComboBoxItem combox = new ComboBoxItem();
                    combox = GenreField.SelectedItem as ComboBoxItem;
                    switch (combox.Content.ToString())
                    {
                        case "Action":
                            {
                                item.ItemGenre = Genre.Action;
                                break;
                            }
                        case "Comedy":
                            {
                                item.ItemGenre = Genre.Comedy;
                                break;
                            }
                        case "Drama":
                            {
                                item.ItemGenre = Genre.Drama;
                                break;
                            }
                        case "Fantasy":
                            {
                                item.ItemGenre = Genre.Fantasy;
                                break;
                            }
                        case "Kids":
                            {
                                item.ItemGenre = Genre.Kids;
                                break;
                            }
                        case "News":
                            {
                                item.ItemGenre = Genre.News;
                                break;
                            }
                        case "Romantic":
                            {
                                item.ItemGenre = Genre.Romantic;
                                break;
                            }
                        default: break;
                    }
                    item.DaysUntilReturn = int.Parse(DaysOfRentField.Text);
                    item.QuanityId = int.Parse(QuantityField.Text);
                    item.ImageUrl = ImageFilePath.Text;
                    LibraryService libraryService = new LibraryService();
                    libraryService.AddLibraryItem(item);
                    MessageContent.Text = "The Item Have Been Added!";
                    MessageBackGround.Fill = new SolidColorBrush(Colors.LightGreen);
                }
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
        private void MessageConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            MessageConfirmButton.Visibility = Visibility.Collapsed;
            MessageContent.Visibility = Visibility.Collapsed;
            MessageBackGround.Visibility = Visibility.Collapsed;
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
