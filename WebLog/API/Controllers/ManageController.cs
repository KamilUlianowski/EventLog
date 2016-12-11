using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebLog.API.ViewModels;
using WebLog.Core;
using WebLog.Core.Models;
using WebLog.Core.Services;

namespace WebLog.API.Controllers
{
    public class ManageController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;

        public ManageController(IUnitOfWork unitOfWork, IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult GetSubjects()
        {
            var subjects = _unitOfWork.Subjects.GetAll();

            return Ok(subjects);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult GetClasses()
        {
            var classes = _unitOfWork.Classes.GetAll();

            return Ok(classes);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult GetStudents()
        {
            var students = _unitOfWork.Students.GetAll();

            return Ok(students);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult GetTeachers()
        {
            var teachers = _unitOfWork.Teachers.GetAll();

            return Ok(teachers);
        }

        [HttpGet]
        public IHttpActionResult GetMainAdvertisements()
        {
            var advertisements = _unitOfWork.Advertisements.GetMainAdvertisements().ToList();

            return Ok(advertisements);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult AddMainAdvertisement([FromBody] NewMessage vm)
        {
            var advertisement = new Advertisement(vm.Text,vm.Title, true);
            _unitOfWork.Advertisements.Add(advertisement);
            _unitOfWork.Complete();

            return Ok(advertisement);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult SubjectDetail([FromUri] int subjectId)
        {
            var subject = _unitOfWork.Subjects.Get(subjectId);

            if (subject == null)
                return NotFound();

            return Ok(subject);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult ClassDetail([FromUri] int classId)
        {
            var schoolClass = _unitOfWork.Classes.Get(classId);

            if (schoolClass == null)
                return NotFound();

            return Ok(schoolClass);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult AddSubject([FromBody] SubjectViewModel vm)
        {
            var subject = new Subject(vm.Name, vm.Url);
            _unitOfWork.Subjects.Add(subject);
            _unitOfWork.Complete();

            return Ok(subject);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult AddClass([FromUri] string name)
        {
            var schoolClass = new SchoolClass(name);
            _unitOfWork.Classes.Add(schoolClass);
            _unitOfWork.Complete();

            return Ok(schoolClass);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult DeleteSubject([FromBody] int num)
        {
            var subject = _unitOfWork.Subjects.Get(num);

            if (subject == null)
                return NotFound();

            _unitOfWork.Subjects.Remove(subject);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult DeleteClass([FromBody] int num)
        {
            var selectedClass = _unitOfWork.Classes.Get(num);

            if (selectedClass == null)
                return NotFound();

            _unitOfWork.Classes.Remove(selectedClass);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult DeleteStudentFromClass([FromBody] ClassViewModel vm)
        {
           var student =  _unitOfWork.Classes.RemoveStudent(vm.ClassId, vm.StudentId);
            _unitOfWork.Complete();

            return Ok(student);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult DeleteTeacherFromClass([FromBody] ClassViewModel vm)
        {
           var schoolClass =  _unitOfWork.Classes.RemoveTeacher(vm.ClassId);
            _unitOfWork.Complete();

            return Ok(schoolClass);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult AddStudentToClass([FromBody] ClassViewModel vm)
        {
            var schoolClass = _unitOfWork.Classes.AddStudent(vm.ClassId, vm.StudentId);
            _unitOfWork.Complete();

            return Ok(schoolClass);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult AddTeacherToClass([FromBody] ClassViewModel vm)
        {
           var schoolClass = _unitOfWork.Classes.AddTeacher(vm.ClassId, vm.TeacherId);
            _unitOfWork.Complete();

            return Ok(schoolClass);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult UpdateClassesSubject([FromBody] SubjectViewModel vm)
        {
           var subject = _unitOfWork.Subjects.UpdateSubjectClasses(vm.SubjectId, vm.ClassId);
            _unitOfWork.Complete();

            return Ok(subject);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult UpdateTeacherSubject([FromBody] SubjectViewModel vm)
        {

            var subject =_unitOfWork.Subjects.UpdateSubjectTeachers(vm.SubjectId, vm.TeacherId);
            _unitOfWork.Complete();
            return Ok(subject);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult SendRatingSummary()
        {
            var students = _unitOfWork.Students.GetAll().ToList();
            _mailService.SendRatingSummary(students);

            return Ok();
        }
    }
}
