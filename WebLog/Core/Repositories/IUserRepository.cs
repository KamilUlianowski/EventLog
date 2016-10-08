using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Common;
using WebLog.Core.Models;
using WebLog.Core.ViewModels;
using WebLog.Core.ViewModels.AuthViewModels;

namespace WebLog.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        void AddUser(SignUpViewModel signUpViewModel);
        bool Login(SignInViewModel signInViewModel);
        void Update(EditUserViewModel editUserViewModel);
        User GetUser(string mail);  
    }
}