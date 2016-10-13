using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using WebLog.Core.Common;
using WebLog.Core.Factory;
using WebLog.Core.Models;
using WebLog.Core.Repositories;
using WebLog.Core.ViewModels;
using WebLog.Core.ViewModels.AuthViewModels;
using WebLog.Persistance.Factory;

namespace WebLog.Persistance.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly LogDbContext _dbContext;
        private readonly IUserFactory _userFactory;

        public UserRepository(LogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _userFactory = new UserFactory();
        }

        public void AddUser(SignUpViewModel signUpViewModel)
        {
            _dbContext.Users.Add(_userFactory.CreateUser(signUpViewModel));
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