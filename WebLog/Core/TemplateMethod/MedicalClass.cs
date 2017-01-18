using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebLog.Core.Models;
using WebLog.Core.Repositories;
using WebLog.Persistance;
using WebLog.Persistance.Repositories;

namespace WebLog.Core.TemplateMethod
{

    [Table("MedicalClass")]
    public class MedicalClass : SchoolClass
    {
        public MedicalClass(string name) : base(name)
        {
            
        }

        public MedicalClass()
        {
            
        }
        public override void AssignSubjects()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new LogDbContext());
            var subjects = unitOfWork.Subjects.GetAll().ToList();
            this.Subjects.Add(subjects[0]);
            Subjects.Add(subjects[1]);
            Subjects.Add(subjects[2]);
        }


        public override void SetMaxNumberOfStudents()
        {
            MaxNumberOfStudents = 15;
        }

        public override void SetAdditionalInformation()
        {
            AdditionalInformation = "To jest klasa medyczna";
        }
    }
}