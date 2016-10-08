using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebLog.Core.ViewModels;
using WebLog.Core.ViewModels.AuthViewModels;

namespace WebLog.Core.Models
{
    [Table("Teacher")]
    public class Teacher : User
    {
        public ICollection<Subject> Subjects { get; set; }


        public Teacher()
        {
            Subjects = new List<Subject>();
        }

        public Teacher(SignUpViewModel signUpViewModel) : base(signUpViewModel)
        {
            Subjects = new List<Subject>();
        }
    }
}