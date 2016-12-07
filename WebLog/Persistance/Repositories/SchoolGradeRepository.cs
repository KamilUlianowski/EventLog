using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebLog.Core.Common;
using WebLog.Core.Models;
using WebLog.Core.Repositories;

namespace WebLog.Persistance.Repositories
{
    public class SchoolGradeRepository : Repository<SchoolGrade>, ISchoolGradeRepository
    {
        private readonly LogDbContext _context;

        public SchoolGradeRepository(LogDbContext context) : base(context)
        {
            _context = context;
        }

        public override IEnumerable<SchoolGrade> GetAll()
        {
            return _context.SchoolGrades.Include(x => x.Subject)
                .Include(x => x.Student)
                .Include(x => x.Teacher);

        }

        public IEnumerable<SchoolGrade> GetSchoolGrades(int subjectId, int schoolClassId)
        {
            return _context.SchoolGrades.Include(x => x.Subject)
                .Include(x => x.Student)
                .Where(x => x.Subject.Id == subjectId &&
                            x.Student.SchoolClass.Id == schoolClassId);
        }

        public IEnumerable<SchoolGrade> GetSchoolGrades(int studentId)
        {
            return _context.SchoolGrades.Include(x => x.Subject)
                .Include(x => x.Student)
                .Where(x => x.Student.Id == studentId)
                .OrderBy(x => x.Subject.Id);
        }

        public void AddGrade(Grade grade, int teacherId, int subjectId, int studentId)
        {
            var teacher = _context.Teachers.FirstOrDefault(x => x.Id == teacherId);
            var subject = _context.Subjects.FirstOrDefault(x => x.Id == subjectId);
            var student = _context.Students.Include(x => x.Parent).FirstOrDefault(x => x.Id == studentId);

            if (teacher == null || subject == null || student == null)
                return;
            var schoolGrade = new SchoolGrade(grade, teacher, student, subject);
            _context.SchoolGrades.Add(schoolGrade);
            schoolGrade.NotifyObserver();
        }

        public SchoolGrade GetGradeFromTest(User user, Test test)
        {
            return _context.SchoolGrades.Include(x => x.Subject)
                .Include(x => x.MyTest)
                .Include(x => x.Teacher)
                .Include(x => x.Student)
                .FirstOrDefault(x => x.Student.Id == user.Id && x.MyTest.Id == test.Id);
        }
    }
}