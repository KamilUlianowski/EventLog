using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.ViewModels;
using WebLog.Core.ViewModels.AuthViewModels;

namespace WebLog.Core.Services
{
    public interface IAuthService
    {
        void Login(SignInViewModel signInViewModel, HttpContext httpContext);
    }
}