﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Resources;
using WebLog.Core.Common;
using WebLog.Core.Models;

namespace WebLog.Core.ViewModels
{
    public class EditUserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "NameValidation")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "SurnameValidation")]
        public string Surname { get; set; }

        [Required(ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "Required")]
        [DataType(DataType.DateTime, ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "BirthValidation")]
        public DateTime Birth { get; set; }

        [Required(ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "Required")]
        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "EmailValidation")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "Required")]
        [StringLength(15, MinimumLength = 6, ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "PasswordValidation")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "Required")]
        [StringLength(11, MinimumLength = 11, ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "PeselValidationLength")]
        [RegularExpression(@"^[0-9]*$", ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "PeselValidationNumbers")]
        public string Pesel { get; set; }

        [Required(ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "Required")]
        public string Adress { get; set; }

        [Required(ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "Required")]
        [RegularExpression(@"^[0-9]*$", ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "SignUpViewModel_Phone_Phone_consists_only_of_numbers")]
        public string Phone { get; set; }

        public EditUserViewModel(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Surname = user.Surname;
            Email = user.Email;
            Pesel = user.Pesel;
            Password = user.Password;
            
        }

        public EditUserViewModel()
        {
            
        }
    }
}