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
        void AddStudent(int schoolClassId, int userId);
        void AddTeacher(int schoolClassId, int userId);
        void RemoveStudent(int schoolId, int studentId);
        void RemoveTeacher(int schoolId);
        IEnumerable<SchoolClass> GetClasses(List<int> classesId);
        void AddAdvertisement(int classId, Advertisement advertisement);
        //SchoolClass GetClass(int teacherId, int subjectId);
        IEnumerable<SchoolClass> GetTeacherClasses(Teacher teacher);
    }
}