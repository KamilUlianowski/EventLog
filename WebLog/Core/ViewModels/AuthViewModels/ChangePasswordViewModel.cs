using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Resources;

namespace WebLog.Core.ViewModels.AuthViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "Required")]
        [StringLength(15, MinimumLength = 6, ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "PasswordValidation")]
        public string Password { get; set; }

        public string Token { get; set; }

        public ChangePasswordViewModel(string token)
        {
            Token = token;
        }

        public ChangePasswordViewModel()
        {
            
        }
    }
}