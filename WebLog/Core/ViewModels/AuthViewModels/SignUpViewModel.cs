using System;
using WebLog.Core.Common;

namespace WebLog.Core.ViewModels.AuthViewModels
{
    public class SignUpViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime Birth { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Pesel { get; set; }

        public string Adress { get; set; }

        public string Phone { get; set; }

        public TypeOfUser TypeOfUser { get; set; }
    }
}