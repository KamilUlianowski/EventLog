using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebLog.Core.Models;
using WebLog.Persistance;

namespace WebLog.Core.TemplateMethod
{
    [Table("MathematicClass")]
    public class MathematicClass : SchoolClass
    {
        public MathematicClass(string name) : base(name)
        {

        }

        public MathematicClass()
        {

        }
        public override void AssignSubjects()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new LogDbContext());
            var subjects = unitOfWork.Subjects.GetAll().ToList();
            Subjects.Add(subjects[2]);
            Subjects.Add(subjects[4]);
            Subjects.Add(subjects[5]);
        }


        public override void SetMaxNumberOfStudents()
        {
            MaxNumberOfStudents = 30;
        }

        public override void SetAdditionalInformation()
        {
            AdditionalInformation = "To jest klasa matematyczna";
        }
    }
}