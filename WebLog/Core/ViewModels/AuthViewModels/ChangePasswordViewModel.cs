using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLog.Core.ViewModels.AuthViewModels
{
    public class ChangePasswordViewModel
    {
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