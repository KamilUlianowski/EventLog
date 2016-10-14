using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebLog.Core.Models;
using WebLog.Core.Repositories;
using WebLog.Core.ViewModels;

namespace WebLog.Persistance.Repositories
{
    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        private readonly LogDbContext _context;

        public SubjectRepository(LogDbContext context) : base(context)
        {
            _context = context;
        }

        public void UpdateSubject(int subjectId, int schoolClassId)
        {
            var subject = _context.Subjects.Include(x => x.SchoolClasses)
                                           .FirstOrDefault(x => x.Id == subjectId);

            var schoolClass = _context.SchoolClass.Include(x => x.Subjects )
                .FirstOrDefault(x => x.Id == schoolClassId);

            if (subject == null || schoolClass == null)
                return;

            if (schoolClass.Subjects.Contains(subject))
                schoolClass.Subjects.Remove(subject);
            else
                schoolClass.Subjects.Add(subject);
        }

        public void UpdateSubjectTeachers(int subjectId, int teacherId)
        {
            var subject = _context.Subjects.Include(x => x.Teachers)
                                          .FirstOrDefault(x => x.Id == subjectId);

            var teacher = _context.Teachers.Include(x => x.Subjects)
                .FirstOrDefault(x => x.Id == teacherId);

            if (subject == null || teacher == null)
                return;

            if (teacher.Subjects.Contains(subject))
                teacher.Subjects.Remove(subject);
            else
                teacher.Subjects.Add(subject);
        }

        public void UpdateContent(int subjectId, string content)
        {
            var subject = _context.Subjects.Include(x => x.SchoolClasses)
                .Include(x => x.Teachers)
                .FirstOrDefault(x => x.Id == subjectId);

            if (subject == null)
                return;

            subject.Content = content;
        }

        public Subject Get(string name)
        {
            return _context.Subjects.Include(x => x.SchoolClasses)
                .Include(x => x.Teachers)
                .FirstOrDefault(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}