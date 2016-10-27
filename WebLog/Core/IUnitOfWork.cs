using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Repositories;

namespace WebLog.Core
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }  
        ISchoolClassRepository Classes { get; }
        IStudentRepository Students { get; }
        ITeacherRepository Teachers { get; }
        IParentRepository Parents { get; }
        ISubjectRepository Subjects { get; }
        ISchoolGradeRepository SchoolGrades { get; }
        IMessageRepository Messages { get; }
        IAdvertisementRepository Advertisements { get; }
        IFileRepository Files { get; }
        IQuestionRepository Questions { get; }
        IQuestionAnswerRepository QuestionAnswers { get; }
        ITestRepository Tests { get; set; }

        void Complete();
    }
}