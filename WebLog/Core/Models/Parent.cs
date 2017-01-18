using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;
using WebLog.Core.Common;
using WebLog.Core.ViewModels;
using WebLog.Core.ViewModels.AuthViewModels;

namespace WebLog.Core.Models
{
    public interface IObserver
    {
        void SendNotice();
    }

    [Table("Parent")]
    public class Parent : User, IObserver
    {
        public ICollection<Student> MyChildrens { get; set; }

        public Parent()
        {
            //Students = new List<Student>();
        }

        public Parent(SignUpViewModel signUpViewModel) : base(signUpViewModel)
        {

        }

        public Parent(string name, string surname, string email, string password, string pesel) : base(name,surname,email,password,pesel)
        {
            MyChildrens = new List<Student>();
        }

        public void SendNotice()
        {
            foreach (var Child in MyChildrens)
            {
                var lastSchoolGrade = Child.SchoolGrades.Last();
                if (lastSchoolGrade.Grade == Grade.One && DateTime.Now < lastSchoolGrade.Date.AddMinutes(10))
                {
                    var message = new StringBuilder();
                    message.Append(Name + ' ' + Surname + ' ');
                    message.Append("dostał właśnie 1 z przedmiotu " + lastSchoolGrade.Subject.Name);
                    if (lastSchoolGrade.Teacher != null)
                        message.Append("\n Pozdrawiam " + lastSchoolGrade.Teacher.Name + ' ' +
                                       lastSchoolGrade.Teacher.Surname);
                    Mail.Send(message.ToString(), Child.Email, "1 z przedmiotu " + lastSchoolGrade.Subject.Name);
                }
            }
        }
    }
}