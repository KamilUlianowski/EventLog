using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebLog.Core.Models;
using WebLog.Core.ViewModels.AuthViewModels;

namespace WebLog.Core.Factory
{
    public interface IUserFactory
    {
        User CreateUser(SignUpViewModel signUpViewModel);
    }
}