using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebLog.API.ViewModels;
using WebLog.Core;
using WebLog.Core.Models;

namespace WebLog.API.Controllers
{
    public class ManageController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        [HttpPost]
        [Authorize]
        public IHttpActionResult AddSubject([FromBody] SubjectViewModel vm)
        {
            _unitOfWork.Subjects.Add(new Subject(vm.Name, vm.Url));
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult AddClass([FromUri] string name)
        {
            _unitOfWork.Classes.Add(new SchoolClass(name));
            _unitOfWork.Complete();

            return Ok();
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
    }
}
