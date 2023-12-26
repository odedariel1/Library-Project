using LibraryProject.DataBase;
using LibraryProject.Model;
using LibraryProject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Services
{
    internal class UserService
    {
        public static User CurrentUser { get; set; }
        public static User ManagedAccount { get; set; }
        public void SignUpUser(User user)
        {
            foreach (var item in HardCodedDataBase.Users)
            {
                if (user.Username == item.Username || user.Id == item.Id)
                    throw new ArgumentException("This Account Already Exist");
            }
            HardCodedDataBase.Users.Add(user);
        }
        public User LogInUser(string username, string password)
        {
            foreach (User user in HardCodedDataBase.Users)
            {
                if (user.Username == username && user.Password == password && user.IsActive == true)
                {
                    return user;
                }
            }
            return null; 
        }
        public void ChangeDetails(User user)
        {
            foreach (User item in HardCodedDataBase.Users)
            {
                if (user.Id == item.Id)
                {
                    item.Username = user.Username;
                    item.Name = user.Name;
                    item.Password = user.Password;
                    item.BirthDay = user.BirthDay;
                    item.Address = user.Address;
                    item.PhoneNumber = user.PhoneNumber;
                }
            }
        }
        public void UnActiveUser(User user)
        {
            foreach (User item in HardCodedDataBase.Users)
            {
                if (user.Id == item.Id)
                {
                    user.IsActive = false;
                }
            }
        }
        public void ActiveUser(User user)
        {
            foreach (User item in HardCodedDataBase.Users)
            {
                if (user.Id == item.Id)
                {
                    user.IsActive = true;
                }
            }
        }

        public User GetUserById(int id)
        {
            foreach (User user in HardCodedDataBase.Users)
            {
                if (user.Id == id)
                {
                    return user;
                }
            }
            return null;
        }
        public List<Member> GetAllMembers()
        {
            List<Member> list = new List<Member>();
            foreach (User user in HardCodedDataBase.Users)
            {
                if (user is Member)
                {
                    Member member = (Member)user;
                    list.Add(member);
                }
            }
            return list;
        }
        public List<Labrarian> GetAllLabrarians()
        {
            List<Labrarian> list = new List<Labrarian>();
            foreach (User user in HardCodedDataBase.Users)
            {
                if (user is Labrarian)
                {
                    Labrarian labrarian = (Labrarian)user;
                    list.Add(labrarian);
                }
            }
            return list;
        }
    }
}
