using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;
using WebLog.Core.Common;
using WebLog.Core.Observer;
using WebLog.Core.ViewModels;
using WebLog.Core.ViewModels.AuthViewModels;

namespace WebLog.Core.Models
{
    [Table("Student")]
    public class Student : User, IStudent
    {
        public SchoolClass SchoolClass { get; set; }
        public Parent Parent { get; set; }
        public ICollection<SchoolGrade> SchoolGrades { get; set; }

        public Student()
        {

        }

        public Student(SignUpViewModel signUpViewModel) : base(signUpViewModel)
        {

        }

        public void SendNotice()
        {
            var lastSchoolGrade = SchoolGrades.Last();
            if (lastSchoolGrade.Grade == Grade.One && Parent != null)
            {
                var message = new StringBuilder();
                message.Append(Name + ' ' + Surname + ' ');
                message.Append("dostał właśnie 1 z przedmiotu " + lastSchoolGrade.Subject.Name);
                if (lastSchoolGrade.Teacher != null)
                    message.Append("\n Pozdrawiam" + lastSchoolGrade.Teacher.Name + ' ' + lastSchoolGrade.Teacher.Surname);
                Mail.Send(message.ToString(), Parent.Email, "1 z przedmiotu " + lastSchoolGrade.Subject.Name);
            }
        }
    }
}