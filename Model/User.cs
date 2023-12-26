using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Model
{
    public abstract class User
    {
        private static int _counter;
        public int Id { get; set;}
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        static User()
        {
            _counter = 100000000;
        }
        public User()
        {
            Id = _counter;
            _counter++;
            IsActive = true;
        }
    }
}
