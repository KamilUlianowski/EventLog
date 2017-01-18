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
        private readonly LogDbContext _dbContext;
        public IUserRepository Users { get; }
        public ISchoolClassRepository Classes { get; }
        public IStudentRepository Students { get; }
        public ITeacherRepository Teachers { get; }
        public IParentRepository Parents { get; }
        public ISubjectRepository Subjects { get; }
        public ISchoolGradeRepository SchoolGrades { get; }
        public IMessageRepository Messages { get; }
        public IAdvertisementRepository Advertisements { get; }
        public IFileRepository Files { get; }
        public IQuestionRepository Questions { get; }
        public IQuestionAnswerRepository QuestionAnswers { get; }
        public ITestRepository Tests { get; set; }

        public UnitOfWork(LogDbContext dbContext)
        {
            _dbContext = dbContext;
            Classes = new SchoolClassRepository(dbContext);
            Users = new UserRepository(dbContext);
            Students = new StudentRepository(dbContext);
            Teachers = new TeacherRepository(dbContext);
            Parents = new ParentRepository(dbContext);
            Subjects = new SubjectRepository(dbContext);
            SchoolGrades = new SchoolGradeRepository(dbContext);
            Messages = new MessageRepository(dbContext);
            Advertisements = new AdvertisementRepository(dbContext);
            Files = new FileRepository(dbContext);
            Questions = new QuestionRepository(dbContext);
            QuestionAnswers = new QuestionAnswerRepository(dbContext);
            Tests = new TestRepository(dbContext);
        }

        public void Complete()
        {
            _dbContext.SaveChanges();
        }
    }
}