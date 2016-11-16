using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebLog.Core.Models;
using WebLog.Core.Repositories;
using WebLog.Core.ViewModels.AuthViewModels;

namespace WebLog.Persistance.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly LogDbContext _context;

        public StudentRepository(LogDbContext context) : base(context)
        {
            _context = context;
        }

        public override Student Get(int id)
        {
            return _context.Students.Include(x => x.SchoolClass)
                                 .Include(x => x.SchoolGrades)
                                 .Include(x => x.Parent)
                                .FirstOrDefault(x => x.Id == id);
        }

        public User GetStudent(SignUpParentViewModel parentViewModel)
        {
            var student = _context.Students.FirstOrDefault(x => x.Name.Equals(parentViewModel.Name, StringComparison.OrdinalIgnoreCase) &&
                                                                x.Surname.Equals(parentViewModel.Surname, StringComparison.OrdinalIgnoreCase) &&
                                                                x.Pesel == parentViewModel.Pesel);

            return student;
        }

        public Student GetStudent(string mail)
        {
            return _context.Students.Include(x => x.SchoolClass.Subjects)
                                                 .Include(x => x.SchoolGrades)
                                                 .Include(x => x.Parent)
                .FirstOrDefault(x => x.Email.Equals(mail, StringComparison.OrdinalIgnoreCase));
        }

        public override IEnumerable<Student> GetAll()
        {
            return _context.Students.Include(x => x.SchoolClass)
                .Include(x => x.Parent)
                .Include(x => x.SchoolGrades.Select(y => y.Subject));
        }
    }
}