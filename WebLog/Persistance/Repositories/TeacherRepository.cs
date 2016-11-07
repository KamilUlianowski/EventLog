using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebLog.Core.Models;
using WebLog.Core.Repositories;

namespace WebLog.Persistance.Repositories
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        private readonly LogDbContext _context;

        public TeacherRepository(LogDbContext context) : base(context)
        {
            _context = context;
        }

        public override IEnumerable<Teacher> GetAll()
        {
            return _context.Teachers.Include(x => x.Subjects)
                .Include(x => x.Advertisements)
                .Include(x => x.SchoolClasses);
        }

        public Teacher GetTeacher(string mail)
        {
            return _context.Teachers.Include(x => x.Subjects)
                .Include(x => x.Advertisements)
                .Include(x => x.SchoolClasses)
                .FirstOrDefault(x => x.Email.Equals(mail, StringComparison.CurrentCultureIgnoreCase));
        }

        public override Teacher Get(int id)
        {
            return _context.Teachers.Include(x => x.Subjects.Select(y => y.SchoolClasses))
                .Include(x => x.Advertisements)
                .Include(x => x.SchoolClasses)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}