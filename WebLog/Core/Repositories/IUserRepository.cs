using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Common;
using WebLog.Core.Models;
using WebLog.Core.ViewModels;
using WebLog.Core.ViewModels.AuthViewModels;
using ChangePasswordViewModel = WebLog.Core.ViewModels.AuthViewModels.ChangePasswordViewModel;

namespace WebLog.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        void AddUser(SignUpViewModel signUpViewModel);
        bool Login(SignInViewModel signInViewModel);
        bool CheckToken(string token);
        void Update(EditUserViewModel editUserViewModel);
        void UpdateToken(string email);
        void UpdatePassword(ChangePasswordViewModel vm);
        User GetUser(string mail);  
    }
}