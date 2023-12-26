using LibraryProject.DataBase;
using LibraryProject.Model;
using LibraryProject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;

namespace LibraryProject.Services
{
    internal class LibraryService
    {
        public static LibraryItem Currentitem { get; set; }
        public void AddLibraryItem(LibraryItem item)
        {
            if (item != null)
            {
                HardCodedDataBase.LibraryItems.Add(item);
            }
        }
        public void RemoveLibraryItem(LibraryItem item)
        {
            foreach (LibraryItem libraryItem in HardCodedDataBase.LibraryItems)
            {
                if (item.Id == libraryItem.Id)
                {
                    libraryItem.IsAvaiable=false;
                }
            }
        }
        public void UnRemoveLibraryItem(LibraryItem item)
        {
            foreach (LibraryItem libraryItem in HardCodedDataBase.LibraryItems)
            {
                if (item.Id == libraryItem.Id)
                {
                    libraryItem.IsAvaiable = true;
                }
            }
        }
        public void ChangeLibraryItemDetails(LibraryItem item)
        {
            foreach (LibraryItem libraryItem in HardCodedDataBase.LibraryItems)
            {
                if (item.Id == libraryItem.Id)
                {
                    libraryItem.Title = item.Title;
                    libraryItem.RentPrice = item.RentPrice;
                    libraryItem.Discount = item.Discount;
                    libraryItem.PublishedAt = item.PublishedAt;
                    libraryItem.ItemGenre = item.ItemGenre;
                    libraryItem.ImageUrl = item.ImageUrl;
                    libraryItem.DaysUntilReturn = item.DaysUntilReturn;
                    libraryItem.QuanityId = item.QuanityId;
                    if (libraryItem is Book)
                    {
                        Book itembook = (Book)item;
                        Book book = (Book)libraryItem;
                        book.Author = itembook.Author;
                    }
                    if (libraryItem is Journal)
                    {
                        Journal itemjournal = (Journal)item;
                        Journal journal = (Journal)libraryItem;
                        journal.CompanyName = itemjournal.CompanyName;
                    }
                }
            }
        }
        public void RentLibraryItem(LibraryItem Item, int UserId)
        {
            foreach (LibraryItem libraryItem in HardCodedDataBase.LibraryItems)
            {
                if (Item.Id == libraryItem.Id)
                {
                    if (libraryItem.LibraryItemStatus == ItemStatus.Free)
                    {
                        libraryItem.LibraryItemStatus = ItemStatus.Rented;
                        RentHistory rentHistory = new RentHistory();
                        rentHistory.RentDay = DateTime.Now;
                        rentHistory.UserId = UserId;
                        libraryItem.ItemHistory.Add(rentHistory);
                    }
                }
            }
        }
        public void ReturnLibraryItem(LibraryItem Item)
        {
            foreach (LibraryItem libraryItem in HardCodedDataBase.LibraryItems)
            {
                if (Item.Id == libraryItem.Id)
                {
                    if (libraryItem.LibraryItemStatus == ItemStatus.Rented)
                    {
                        libraryItem.LibraryItemStatus = ItemStatus.Free;
                        libraryItem.ItemHistory.Last().DayOfReturn = DateTime.Now;
                      
                    }
                }
            }
        }
        

        public List<LibraryItem> ListOfRentedByUserId(int Userid)
        {
            List<LibraryItem> list = new List<LibraryItem>();
            foreach(LibraryItem libraryItem in HardCodedDataBase.LibraryItems)
            {
                if (libraryItem.LibraryItemStatus == ItemStatus.Rented && libraryItem.ItemHistory.Last().UserId==Userid)
                {
                        list.Add(libraryItem);
                }
            }
            return list;
        }
        public List<LibraryItem> ListOfAllTimeRentedByUserId(int Userid)
        {
            List<LibraryItem> list = new List<LibraryItem>();
            bool isExist = false;
            foreach (LibraryItem libraryItem in HardCodedDataBase.LibraryItems)
            {
                foreach(RentHistory history in libraryItem.ItemHistory)
                {
                    if (history.UserId == Userid)
                    {
                        if (!isExist)
                        {
                            isExist = true;
                            list.Add(libraryItem);
                        }
                    }
                }
                isExist = false;
            }
            return list;
        }
        public List<LibraryItem> ListOfAllRentedItems()
        {
            List<LibraryItem> list = new List<LibraryItem>();
            foreach (LibraryItem libraryItem in HardCodedDataBase.LibraryItems)
            {
                    if (libraryItem.LibraryItemStatus == ItemStatus.Rented)
                        list.Add(libraryItem);
            }
            return list;
        }
        public List<LibraryItem> ListOfAllLatedItems()
        {
            List<LibraryItem> list = new List<LibraryItem>();
            bool isExist = false;
            foreach (LibraryItem libraryItem in HardCodedDataBase.LibraryItems)
            {
                foreach (RentHistory history in libraryItem.ItemHistory)
                {
                    if (history.DayOfReturn == DateTime.MinValue && history.RentDay.AddDays(libraryItem.DaysUntilReturn) < DateTime.Now)
                    {
                        if (!isExist)
                        {
                            list.Add(libraryItem);
                            isExist = true;
                        }
                    }
                    else
                    {
                        if (history.RentDay.AddDays(libraryItem.DaysUntilReturn) < history.DayOfReturn)
                            if (!isExist)
                            {
                                list.Add(libraryItem);
                                isExist = true;
                            }
                    }
                }
                isExist = false;
            }
            return list;
        }
        public RentHistory DateOfRent(LibraryItem item,int userid)
        {
            foreach(RentHistory rented in item.ItemHistory)
            {
                if(rented.UserId == userid)
                {
                    return rented;
                }
            }
            return null;
        }
        public List<Book> GetAllFreeBooks()
        {
            List<Book> books = new List<Book>();
            foreach (LibraryItem libraryItem in HardCodedDataBase.LibraryItems)
            {
                if (libraryItem is Book)
                {
                    if (libraryItem.LibraryItemStatus == ItemStatus.Free && libraryItem.IsAvaiable)
                    {
                        Book book = (Book)libraryItem;
                        books.Add(book);
                    }
                }
            }
            return books;
        }
        public List<Journal> GetAllFreeJournals()
        {
            List<Journal> Journals = new List<Journal>();
            foreach (LibraryItem libraryItem in HardCodedDataBase.LibraryItems)
            {
                if (libraryItem is Journal)
                {
                    if (libraryItem.LibraryItemStatus == ItemStatus.Free && libraryItem.IsAvaiable)
                    {
                        Journal journal = (Journal)libraryItem;
                        Journals.Add(journal);
                    }
                }
            }
            return Journals;
        }
        public List<Book> GetAllBooks()
        {
            List<Book> Books = new List<Book>();


            foreach (LibraryItem libraryItem in HardCodedDataBase.LibraryItems)
            {
                if (libraryItem is Book)
                {
                    Book book = (Book)libraryItem;
                    Books.Add(book);
                }
            }
            return Books;

        }
        public List<Journal> GetAllJournals()
        {
            List<Journal> Journals = new List<Journal>();
            foreach (LibraryItem libraryItem in HardCodedDataBase.LibraryItems)
            {
                if (libraryItem is Journal)
                {
                    Journal journal = (Journal)libraryItem;
                    Journals.Add(journal);
                }

            }
            return Journals;

        }
        public LibraryItem GetItemById(int id)
        {
            foreach (LibraryItem libraryItem in HardCodedDataBase.LibraryItems)
            {
                if (libraryItem.Id == id)
                    return libraryItem;
            }
            return null;//exception
        }
    }
}
