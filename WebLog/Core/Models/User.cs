using System;
using System.Collections.Generic;
using WebLog.Core.Common;
using WebLog.Core.ViewModels;
using WebLog.Core.ViewModels.AuthViewModels;

namespace WebLog.Core.Models
{
    public class User
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

        public User(SignUpViewModel signUpViewModel)
        {
            Name = signUpViewModel.Name;
            Surname = signUpViewModel.Surname;
            if (signUpViewModel.Birth.Year < 1850)
                signUpViewModel.Birth = DateTime.Now;
            Birth = signUpViewModel.Birth;
            Email = signUpViewModel.Email;
            Password = signUpViewModel.Password;
            Pesel = signUpViewModel.Pesel;
            Adress = signUpViewModel.Adress;
            Password = signUpViewModel.Phone;
        }

        public User()
        {
            
        }
    }
}