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
using ChangePasswordViewModel = WebLog.Core.ViewModels.AuthViewModels.ChangePasswordViewModel;

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
            User zz = _userFactory.CreateUser(signUpViewModel);
            _dbContext.Users.Add(_userFactory.CreateUser(signUpViewModel));
        }

        public bool Login(SignInViewModel signInViewModel)
        {
            return _dbContext.Users.Any(x => x.Email == signInViewModel.Email &&
                                             x.Password == signInViewModel.Password);
        }

        public bool CheckToken(string token)
        {
            return token != null && _dbContext.Users.Any(x => x.Token == token);
        }

        public void UpdateToken(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (user == null)
                return;

            user.Token = Guid.NewGuid().ToString();
        }

        public void UpdatePassword(ChangePasswordViewModel vm)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Token == vm.Token);
            if (user != null)
            {
                user.Password = vm.Password;
                user.Token = null;
            }

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
            user.Pesel = editUserViewModel.Pesel;
        }

    }
}