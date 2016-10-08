using System;
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

        public User GetStudent(SignUpParentViewModel parentViewModel)
        {
            var student = _context.Students.FirstOrDefault(x => x.Name.Equals(parentViewModel.Name, StringComparison.OrdinalIgnoreCase) &&
                                                                x.Surname.Equals(parentViewModel.Surname, StringComparison.OrdinalIgnoreCase) && 
                                                                x.Pesel == parentViewModel.Pesel);

            return student;
        }
    }
}