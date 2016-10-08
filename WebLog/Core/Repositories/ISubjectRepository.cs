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
        void UpdateSubject(int subjectId, int schoolClassId);
        void UpdateSubjectTeachers(int subjectId, int teacherId);

    }
}