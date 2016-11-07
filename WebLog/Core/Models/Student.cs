using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebLog.Core.ViewModels;
using WebLog.Core.ViewModels.AuthViewModels;

namespace WebLog.Core.Models
{
    [Table("Student")]
    public class Student : User
    {
        public SchoolClass SchoolClass { get; set; }
        public Parent Parent { get; set; }

        public Student()
        {
            
        }

        public Student(SignUpViewModel signUpViewModel) : base(signUpViewModel)
        {
            
        }
    }
}