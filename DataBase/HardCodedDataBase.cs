using LibraryProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.DataBase
{
    internal class HardCodedDataBase
    {
        public static List<LibraryItem> LibraryItems { get; set; }
        public static List<User> Users { get; set; }
        static HardCodedDataBase()
        {
            LibraryItems = new List<LibraryItem>();
            LibraryItems.Add(new Book()
            {
                Author = "J. R. R. Tolkien",
                Title = "The Lord of the Rings",
                RentPrice = 20,
                PublishedAt = new DateTime(1954, 7, 29),
                LibraryItemStatus = Enums.ItemStatus.Free,
                ItemGenre = Enums.Genre.Drama,
                ImageUrl = "LordOfTheRings.jpg",
                EnterLibraryDate = DateTime.Now,
                DaysUntilReturn = -14,
                QuanityId = 1,
            });
            LibraryItems.Add(new Book()
            {
                Author = "J.K. Rowling's",
                Title = "Harry Potter and the Sorcerer's Stone",
                RentPrice = 30,
                PublishedAt = new DateTime(1997, 6, 30),
                LibraryItemStatus = Enums.ItemStatus.Free,
                ItemGenre = Enums.Genre.Kids,
                ImageUrl = "BookExample.jpg",
                EnterLibraryDate = DateTime.Now,
                DaysUntilReturn = 12,
                QuanityId = 2,
            });
            LibraryItems.Add(new Book()
            {
                Author = "J.K. Rowling's",
                Title = "Harry Potter and the Philosopher's Stone",
                RentPrice = 30,
                PublishedAt = new DateTime(1997, 6, 30),
                LibraryItemStatus = Enums.ItemStatus.Free,
                ItemGenre = Enums.Genre.Kids,
                ImageUrl = "HarryPotterAndThePhilosopherStone.jpg",
                EnterLibraryDate = DateTime.Now,
                DaysUntilReturn = 12,
                QuanityId = 5,
            });
            LibraryItems.Add(new Book()
            {
                Author = "J.K. Rowling's",
                Title = "Harry Potter And The Order Of The Phoenix",
                RentPrice = 30,
                PublishedAt = new DateTime(2003, 6, 21),
                LibraryItemStatus = Enums.ItemStatus.Free,
                ItemGenre = Enums.Genre.Kids,
                ImageUrl = "HarryPotterAndTheOrderOfThePhoenix.jpg",
                EnterLibraryDate = DateTime.Now,
                DaysUntilReturn = 12,
                QuanityId = 6,
            });
            LibraryItems.Add(new Book()
            {
                Author = "Christopher Paolini",
                Title = "Eragon",
                RentPrice = 15,
                PublishedAt = new DateTime(1954, 7, 29),
                LibraryItemStatus = Enums.ItemStatus.Free,
                ItemGenre = Enums.Genre.Fantasy,
                ImageUrl = "Eragonbookcover.png",
                EnterLibraryDate = DateTime.Now,
                DaysUntilReturn = 7,
                QuanityId = 3,
            });
            LibraryItems.Add(new Book()
            {
                Author = "J. R. R. Tolkien",
                Title = "The Hobbit",
                RentPrice = 30,
                PublishedAt = new DateTime(2001, 5, 29),
                LibraryItemStatus = Enums.ItemStatus.Free,
                ItemGenre = Enums.Genre.Fantasy,
                ImageUrl = "TheHobbit.jpg",
                EnterLibraryDate = DateTime.Now,
                DaysUntilReturn = 19,
                QuanityId = 7,
            });
            LibraryItems.Add(new Journal()
            {
                CompanyName = "The Journal",
                Title = "Events marking 25th anniversary of Good Friday Agreement take place across island of Ireland",
                RentPrice = 9,
                PublishedAt = new DateTime(2023, 4, 7),
                LibraryItemStatus = Enums.ItemStatus.Free,
                ItemGenre = Enums.Genre.News,
                ImageUrl = "JournalImage.jpg",
                EnterLibraryDate = DateTime.Now,
                DaysUntilReturn = 3,
                QuanityId = 4,
            });
            LibraryItems.Add(new Journal()
            {
                CompanyName = "Marvel Sutdio's",
                Title = "Spiderman",
                RentPrice = 15,
                PublishedAt = new DateTime(2018, 6, 17),
                LibraryItemStatus = Enums.ItemStatus.Free,
                ItemGenre = Enums.Genre.Kids,
                ImageUrl = "SpiderMan.jpg",
                EnterLibraryDate = DateTime.Now,
                DaysUntilReturn = 3,
                QuanityId = 8,
            });
            LibraryItems.Add(new Journal()
            {
                CompanyName = "Marvel Sutdio's",
                Title = "The Avengers War Accross Time",
                RentPrice = 15,
                PublishedAt = new DateTime(2023, 1, 1),
                LibraryItemStatus = Enums.ItemStatus.Free,
                ItemGenre = Enums.Genre.Kids,
                ImageUrl = "TheAvengersComics.jpg",
                EnterLibraryDate = DateTime.Now,
                DaysUntilReturn = 3,
                QuanityId = 9,
            });
            LibraryItems.Add(new Journal()
            {
                CompanyName = "Marvel Sutdio's",
                Title = "Captian America Cold War",
                RentPrice = 15,
                PublishedAt = new DateTime(2022, 11, 16),
                LibraryItemStatus = Enums.ItemStatus.Free,
                ItemGenre = Enums.Genre.Kids,
                ImageUrl = "captainamericacoldwarComic.jpg",
                EnterLibraryDate = DateTime.Now,
                DaysUntilReturn = 3,
                QuanityId = 9,
            });
            Users = new List<User>();
            Users.Add(new Labrarian()
            {
                Username = "oded123",
                Name = "oded ariel",
                Password = "1234",
                PhoneNumber = "0524843851",
                Address = "Moshav Gea",
                BirthDay = new DateTime(2001, 10, 12)
            });
            Users.Add(new Member()
            {
                Username = "TrojanX",
                Name = "Moshe Lagoyski",
                Password = "1234",
                PhoneNumber = "0522249560",
                Address = "Rishon Lezion",
                BirthDay = new DateTime(1998, 10, 4)
            });
            Users.Add(new Member()
            {
                Username = "Member",
                Name = "Member",
                Password = "1234",
                PhoneNumber = "0521111111",
                Address = "israel",
                BirthDay = new DateTime(2001, 10, 12)
            });
        }

    }
}
