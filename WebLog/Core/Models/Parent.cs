using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebLog.Core.ViewModels;
using WebLog.Core.ViewModels.AuthViewModels;

namespace WebLog.Core.Models
{
    [Table("Parent")]
    public class Parent : User
    {
        public ICollection<Student> MyChildrens { get; set; }

        public Parent()
        {
            //Students = new List<Student>();
        }

        public Parent(SignUpViewModel signUpViewModel) : base(signUpViewModel)
        {

        }

    }
}