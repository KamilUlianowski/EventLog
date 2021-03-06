﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebLog.API.ViewModels;
using WebLog.Core;
using WebLog.Core.Models;
using WebLog.Core.Proxy;
using WebLog.Core.Services;
using WebLog.Core.TemplateMethod;

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
        public IHttpActionResult GetTeacherWithImage([FromUri] int teacherId)
        {
            var teacher = _unitOfWork.Teachers.GetTeacher(teacherId);
            teacher.FaceImage.ShowImage(teacherId);
            RealImage realImage = ((ProxyImage) teacher.FaceImage).RealImage;
            return Ok(realImage.ImageTeacher);
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

            var advertisements = _unitOfWork.Advertisements.GetMainAdvertisements();
            if (advertisements != null)
                return Ok(advertisements);
            else
                return Ok();
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
        public IHttpActionResult AddClass([FromUri] string name, int profileId)
        {
            SchoolClass schoolClass = null;
            switch (profileId)
            {
                case 0:
                     schoolClass = new LanguageClass(name);
                    break;
                case 1:
                    schoolClass = new MedicalClass(name);
                    break;
                case 2:
                    schoolClass = new MathematicClass(name);
                    break;
            }

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
        public IHttpActionResult DeleteClass([FromUri] int num)
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
