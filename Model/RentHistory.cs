using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Model
{
    public class RentHistory
    {
        public DateTime RentDay { get; set; }
        public DateTime DayOfReturn { get; set; }
        public int UserId { get; set; }
    }
}

