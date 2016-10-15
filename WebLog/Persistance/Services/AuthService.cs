using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using WebLog.Core;
using WebLog.Core.Services;
using WebLog.Core.ViewModels;
using WebLog.Core.ViewModels.AuthViewModels;

namespace WebLog.Persistance.Services
{
    public class AuthService : IAuthService
    {
        private IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Login(SignInViewModel signInViewModel, HttpContext httpContext)
        {
            if (_unitOfWork.Users.Login(signInViewModel))
            {
                var typeOfUser = _unitOfWork.Users.GetUser(signInViewModel.Email).GetType();
                var ident = new ClaimsIdentity(
                  new[] { 
              // adding following 2 claim just for supporting default antiforgery provider
              new Claim(ClaimTypes.NameIdentifier, signInViewModel.Email),
              new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

              new Claim(ClaimTypes.Email,signInViewModel.Email),
              new Claim(ClaimTypes.Role, typeOfUser.Name),



                  },
                  DefaultAuthenticationTypes.ApplicationCookie);

                httpContext.GetOwinContext().Authentication.SignIn(
                   new AuthenticationProperties { IsPersistent = false }, ident);

            }
        }
    }
}