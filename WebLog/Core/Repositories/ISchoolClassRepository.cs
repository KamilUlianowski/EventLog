using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using WebLog.Core.Models;
using WebLog.Core.ViewModels;

namespace WebLog.Core.Repositories
{
    public interface ISchoolClassRepository : IRepository<SchoolClass>
    {
        void Add(SchoolClassViewModel classViewModel);
        SchoolClass AddStudent(int schoolClassId, int userId);
        SchoolClass AddTeacher(int schoolClassId, int userId);
        SchoolClass RemoveStudent(int schoolId, int studentId);
        SchoolClass RemoveTeacher(int schoolId);
        IEnumerable<SchoolClass> GetClasses(List<int> classesId);
        SchoolClass AddAdvertisement(int classId, Advertisement advertisement);
        //SchoolClass GetClass(int teacherId, int subjectId);
        IEnumerable<SchoolClass> GetTeacherClasses(Teacher teacher);
    }
}