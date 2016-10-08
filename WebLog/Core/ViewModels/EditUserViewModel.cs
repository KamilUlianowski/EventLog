using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Common;
using WebLog.Core.Models;

namespace WebLog.Core.ViewModels
{
    public class EditUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }
        public string Email { get; set; }
        public string Pesel { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public EditUserViewModel(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Surname = user.Surname;
            if (user.Birth.Year < 1850)
                user.Birth = DateTime.Now;
            Birth = user.Birth;
            Email = user.Email;
            Pesel = user.Pesel;
            Adress = user.Adress;
            Phone = user.Phone;
            Password = user.Password;
            
        }

        public EditUserViewModel()
        {
            
        }
    }
}