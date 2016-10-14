using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Models;
using System.Data.Entity;
using WebLog.Core.Repositories;
using WebLog.Core.ViewModels;

namespace WebLog.Persistance.Repositories
{
    public class SchoolClassRepository : Repository<SchoolClass>, ISchoolClassRepository
    {
        private readonly LogDbContext _context;

        public SchoolClassRepository(LogDbContext context) : base(context)
        {
            _context = context;
        }

        public override IEnumerable<SchoolClass> GetAll()
        {
            return _context.SchoolClass.Include(x => x.Students)
                                       .Include(x => x.Teacher)
                                       .Include(x => x.Subjects).ToList();
        }

        public override SchoolClass Get(int id)
        {
            return _context.SchoolClass.Include(x => x.Teacher)
                                       .Include(x => x.Students)
                                       .Include(x => x.Advertisements)
                                       .FirstOrDefault(x => x.Id == id);
        }

        public void Delete(int id)
        {
            var schoolClass = _context.SchoolClass.FirstOrDefault(x => x.Id == id);

            if (schoolClass != null)
                _context.SchoolClass.Remove(schoolClass);
        }

        public void Add(SchoolClassViewModel classViewModel)
        {
            _context.SchoolClass.Add(new SchoolClass(classViewModel.Name));
        }

        public void AddStudent(int schoolClassId, int studentId)
        {
            var schoolClass = _context.SchoolClass.FirstOrDefault(x => x.Id == schoolClassId);
            var student = _context.Students.FirstOrDefault(x => x.Id == studentId);

            if (schoolClass != null && student != null)
                schoolClass.Students.Add(student);

        }

        public void AddTeacher(int schoolClassId, int teacherId)
        {
            var schoolClass = _context.SchoolClass.FirstOrDefault(x => x.Id == schoolClassId);
            var teacher = _context.Teachers.FirstOrDefault(x => x.Id == teacherId);

            if (schoolClass != null && teacher != null)
                schoolClass.Teacher = teacher;
        }

        public void RemoveStudent(int schooClassId, int studentId)
        {
            var schoolClass = _context.SchoolClass.FirstOrDefault(x => x.Id == schooClassId);
            var student = _context.Students.FirstOrDefault(x => x.Id == studentId);

            if (schoolClass != null && student != null)
                schoolClass.Students.Remove(student);

        }

        public void RemoveTeacher(int schooClassId)
        {
            var schoolClass = _context.SchoolClass.Include(x => x.Teacher).FirstOrDefault(x => x.Id == schooClassId);

            if (schoolClass != null)
                schoolClass.Teacher = null;

        }

        public IEnumerable<SchoolClass> GetClasses(List<int> classesId)
        {
           return _context.SchoolClass.Include(x => x.Teacher)
                                .Where(x => classesId.Contains(x.Id));
        }

        public void AddAdvertisement(int classId, Advertisement advertisement)
        {
            var schoolClass = Get(classId);

            schoolClass?.Advertisements.Add(advertisement);
        }

        //public SchoolClass GetClass(int teacherId, int subjectId)
        //{
        //    return _context.SchoolClass.Include(x => x.Subjects)
        //                                .Include(x => x.Teacher)
        //                                .FirstOrDefault(x => x.Teacher.Id == teacherId &&
        //                                                x.Subjects.Contains())
        //}

        public IEnumerable<SchoolClass> GetTeacherClasses(Teacher teacher)
        {
            return _context.SchoolClass.Include(x => x.Teacher)
                .Where(x => x.Teacher.Id == teacher.Id);
        }
    }
}