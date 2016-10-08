using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using WebLog.Core.Common;
using WebLog.Core.Models;
using WebLog.Core.Repositories;
using WebLog.Core.ViewModels;
using WebLog.Core.ViewModels.AuthViewModels;

namespace WebLog.Persistance.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private LogDbContext _dbContext;

        public UserRepository(LogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUser(SignUpViewModel signUpViewModel)
        {
            switch (signUpViewModel.TypeOfUser)
            {
                case TypeOfUser.Student:
                    _dbContext.Students.Add(new Student(signUpViewModel));
                    break;
                case TypeOfUser.Parent:
                    _dbContext.Parents.Add(new Parent(signUpViewModel));
                    break;
                case TypeOfUser.Teacher:
                    _dbContext.Teachers.Add(new Teacher(signUpViewModel));
                    break;
            }
        }

        public bool Login(SignInViewModel signInViewModel)
        {
            return _dbContext.Users.Any(x => x.Email == signInViewModel.Email &&
                                             x.Password == signInViewModel.Password);
        }

        public User GetUser(string mail)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Email == mail);
        }

        public void Update(EditUserViewModel editUserViewModel)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == editUserViewModel.Email);

            if (user == null) return;

            user.Name = editUserViewModel.Name;
            user.Surname = editUserViewModel.Surname;
            user.Adress = editUserViewModel.Adress;
            user.Pesel = editUserViewModel.Pesel;
            user.Phone = editUserViewModel.Phone;
            user.Birth = editUserViewModel.Birth;
        }

    }
}