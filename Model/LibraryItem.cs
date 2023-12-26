using LibraryProject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Model
{
    public abstract class LibraryItem
    {
        private static int _counter;
        public int Id { get; set; }
        public string Title { get; set; }
        public double RentPrice { get; set; }
        public double Discount { get; set; }
        public DateTime PublishedAt { get; set; }
        public ItemStatus LibraryItemStatus { get; set; }
        public Genre ItemGenre { get; set; }
        public bool IsAvaiable { get; set; }
        public string ImageUrl { get; set; }
        public DateTime EnterLibraryDate { get; set; }
        public int DaysUntilReturn { get; set; }
        public int QuanityId { get; set; }
        public List<RentHistory> ItemHistory { get; set; }

        static LibraryItem()
        {
            _counter = 0;
        }
        public LibraryItem()
        {
            ItemHistory = new List<RentHistory>();
            Id = _counter++;
            IsAvaiable = true;
        }

    }
}
