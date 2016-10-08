using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebLog.Core;
using WebLog.Core.Models;
using WebLog.Core.Repositories;
using WebLog.Persistance.Repositories;

namespace WebLog.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LogDbContext _dbDbContext;
        public IUserRepository Users { get; }
        public ISchoolClassRepository Classes { get; }
        public IStudentRepository Students { get; }
        public ITeacherRepository Teachers { get; }
        public IParentRepository Parents { get; }
        public ISubjectRepository Subjects { get; }
        public ISchoolGradeRepository SchoolGrades { get; }
        public IMessageRepository Messages { get; }

        public UnitOfWork(LogDbContext dbContext)
        {
            _dbDbContext = dbContext;
            Classes = new SchoolClassRepository(dbContext);
            Users = new UserRepository(dbContext);
            Students = new StudentRepository(dbContext);
            Teachers = new TeacherRepository(dbContext);
            Parents = new ParentRepository(dbContext);
            Subjects = new SubjectRepository(dbContext);
            SchoolGrades = new SchoolGradeRepository(dbContext);
            Messages = new MessageRepository(dbContext);
        }

        public void Complete()
        {
            _dbDbContext.SaveChanges();
        }
    }
}