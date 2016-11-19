using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebLog.Core;
using WebLog.Core.Models;
using WebLog.Core.ViewModels;

namespace WebLog.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult StudentAccount()
        {
            var user = _unitOfWork.Users.GetUser(User.Identity.GetUserId());

            var student = _unitOfWork.Students.Get(user.Id);
            return View(student);
        }

        [HttpGet]
        public ActionResult StudentGrades(int? studentId = null)
        {
            Student student = null;
            if (studentId == null)
            {
                var user = _unitOfWork.Users.GetUser(User.Identity.GetUserId());
                student = _unitOfWork.Students.Get(user.Id);
            }
            else
                student = _unitOfWork.Students.Get(studentId.Value);
            var schoolGrades = _unitOfWork.SchoolGrades.GetSchoolGrades(student.Id);
            var advertisements = new List<Advertisement>();
            if (student.SchoolClass != null)
                advertisements = _unitOfWork.Advertisements.GetAdvertisements(student.SchoolClass.Id).ToList();

            return PartialView(new StudentAccountViewModel(student, schoolGrades, advertisements));
        }

        [HttpGet]
        [Authorize]
        public ActionResult StudentSubjects()
        {
            return PartialView((Student)_unitOfWork.Students.GetStudent(User.Identity.GetUserId()));
        }

        [HttpGet]
        public ActionResult StudentAdvertisements(int classId)
        {
            return PartialView(_unitOfWork.Advertisements.GetAdvertisements(classId).ToList());
        }

        [HttpGet]
        public ActionResult StudentSubjectDetail(int subjectId)
        {
            var subject = _unitOfWork.Subjects.Get(subjectId);

            return PartialView(subject);
        }

    }
}