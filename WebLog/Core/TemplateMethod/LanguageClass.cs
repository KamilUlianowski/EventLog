using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebLog.Core.Models;
using WebLog.Persistance;

namespace WebLog.Core.TemplateMethod
{
    [Table("LanguageClass")]
    public class LanguageClass : SchoolClass
    {
        public LanguageClass()
        {
            
        }

        public LanguageClass(string name) : base(name)
        {

        }
        public override void AssignSubjects()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new LogDbContext());
            var subjects = unitOfWork.Subjects.GetAll().ToList();
            Subjects.Add(subjects[1]);
            Subjects.Add(subjects[3]);
        }


        public override void SetMaxNumberOfStudents()
        {
            MaxNumberOfStudents = 25;
        }

        public override void SetAdditionalInformation()
        {
            AdditionalInformation = "To jest klasa językowa";
        }
    }
}