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
    public interface IObservable
    {
        void AddObserver(Parent parent);
        void DeleteObserver();
        void Notify();
    }

    [Table("Student")]
    public class Student : User, IObservable
    {
        private IObserver observer;
        public SchoolClass SchoolClass { get; set; }

        public Parent Parent { get; set; }
        public ICollection<SchoolGrade> SchoolGrades { get; set; }

        public Student()
        {
        }

        public Student(SignUpViewModel signUpViewModel) : base(signUpViewModel)
        {

        }

        public Student(string name, string surname, string email, string password, string pesel) : base(name,surname,email,password,pesel)
        {

        }

        public void AddObserver(Parent parent)
        {
            observer = parent;
        }

        public void DeleteObserver()
        {
            observer = null;
        }

        public void Notify()
        {
            observer.SendNotice();
        }
    }
}