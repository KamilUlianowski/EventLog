using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Models;
using WebLog.Core.ViewModels;

namespace WebLog.Core.Repositories
{
    public interface ISubjectRepository : IRepository<Subject>
    {
        Subject UpdateSubjectClasses(int subjectId, int schoolClassId);
        Subject UpdateSubjectTeachers(int subjectId, int teacherId);
        void UpdateContent(int subjectId, string content);
        Subject Get(string name);
        

    }
}