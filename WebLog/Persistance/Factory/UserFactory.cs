using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Common;
using WebLog.Core.Factory;
using WebLog.Core.Models;
using WebLog.Core.ViewModels.AuthViewModels;

namespace WebLog.Persistance.Factory
{
    public class UserFactory : IUserFactory
    {
        public User CreateUser(SignUpViewModel signUpViewModel)
        {
            switch (signUpViewModel.TypeOfUser)
            {
                case TypeOfUser.Student:
                    return new Student(signUpViewModel);
                case TypeOfUser.Parent:
                    return new Parent(signUpViewModel);
                case TypeOfUser.Teacher:
                   return new Teacher(signUpViewModel);
                default:
                    return null;
                //return new User();
            }
        }
    }
}