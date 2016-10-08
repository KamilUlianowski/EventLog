using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Models;

namespace WebLog.Core.Repositories
{
    public interface ISchoolGradeRepository : IRepository<SchoolGrade>
    {
        IEnumerable<SchoolGrade> GetSchoolGrades(int subjectId, int schoolClassId);
        IEnumerable<SchoolGrade> GetSchoolGrades(int studentId);
        void AddGrade(int grade, int teacherId, int subjectId, int studentId);
    }
}