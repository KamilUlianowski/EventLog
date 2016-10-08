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
        public Student Student { get; set; }

        public Parent()
        {

        }

        public Parent(SignUpViewModel signUpViewModel) : base(signUpViewModel)
        {

        }

    }
}