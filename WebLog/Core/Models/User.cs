using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebLog.Core.Common;
using WebLog.Core.ViewModels;
using WebLog.Core.ViewModels.AuthViewModels;

namespace WebLog.Core.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Surname { get; set; }

        [Required]
        public DateTime Birth { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(30)]
        public string Email { get; set; }

        [Required]
        public string Pesel { get; set; }

        [Required]
        public string Adress { get; set; }

        public string Phone { get; set; }

        [Required]
        [MaxLength(30)]
        public string Password { get; set; }

        public string Token { get; set; }

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
            Phone = signUpViewModel.Phone;
        }

        public User()
        {
            
        }
    }
}