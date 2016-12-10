using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebLog.Core;
using WebLog.Core.Models;
using WebLog.Core.Services;
using WebLog.Core.ViewModels;
using WebLog.Core.ViewModels.SubjectViewModels;
using WebLog.Persistance.Services;

namespace WebLog.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private INoticeService _noticeService;

        public TeacherController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult TeacherAccount()
        {
            var user = _unitOfWork.Users.GetUser(User.Identity.GetUserId());
            var teacher = _unitOfWork.Teachers.Get(user.Id);
            var schoolClasses = _unitOfWork.Classes.GetAll().ToList();
            return View(new TeacherAccountViewModel(teacher, schoolClasses));
        }

        [HttpGet]
        [Authorize]
        public ActionResult Subject(int id)
        {
            var subject = _unitOfWork.Subjects.Get(id);
            var tests = _unitOfWork.Tests.GetTestsFromSubject(id).ToList();
            var subjects = _unitOfWork.Subjects.GetAll().ToList();
            return PartialView(new SubjectSiteViewModel(subject, tests, subjects));
        }

        [HttpGet]
        [Authorize]
        public ActionResult SubjectName(string name)
        {
            var subject = _unitOfWork.Subjects.Get(name);
            var tests = _unitOfWork.Tests.GetTestsFromSubject(subject.Id).ToList();
            var subjects = _unitOfWork.Subjects.GetAll().ToList();
            return View("Subject", new SubjectSiteViewModel(subject, tests, subjects));
        }


        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public ActionResult TeacherClass(int? subjectId, int? schoolClassId)
        {
            var teacher = (Teacher)_unitOfWork.Users.GetUser(User.Identity.GetUserId());

            if ((subjectId == null) || (schoolClassId == null) || (teacher == null))
                return RedirectToAction("Index", "Home");

            var schoolClass = _unitOfWork.Classes.Get(schoolClassId.Value);
            var subject = _unitOfWork.Subjects.Get(subjectId.Value);
            var schoolGrades = _unitOfWork.SchoolGrades.GetSchoolGrades(subjectId.Value, schoolClassId.Value).ToList();

            if ((schoolClass == null) || (subject == null))
                return RedirectToAction("Index", "Home");

            return PartialView(new StudentGradesViewModel(subject, schoolClass, teacher, schoolGrades));
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public void AddGrade(StudentGradesViewModel vm, int selectedStudent)
        {
            _unitOfWork.SchoolGrades.AddGrade(vm.NewGrade, vm.Teacher.Id, vm.Subject.Id, selectedStudent);
            _unitOfWork.Complete();
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public void AddContent(SubjectSiteViewModel vm)
        {
            _unitOfWork.Subjects.UpdateContent(vm.Subject.Id, vm.Content);
            _unitOfWork.Complete();
            var subjects = _unitOfWork.Subjects.GetAll().ToList();
        }

        [HttpGet]
        public ActionResult TeacherAdvertisements()
        {
            var teacher = _unitOfWork.Teachers.GetTeacher(User.Identity.GetUserId());
            var schoolClasses = _unitOfWork.Classes.GetAll().ToList();
            if (teacher == null) return PartialView(new TeacherAccountViewModel());
            return PartialView(new TeacherAccountViewModel(teacher, schoolClasses));
        }

        [HttpPost]
        public ActionResult AddAdvertisement(TeacherAccountViewModel viewModel)
        {
            if(viewModel.SelectedClasses == null) return RedirectToAction("TeacherAdvertisements");
            var classes = _unitOfWork.Classes.GetClasses(viewModel.SelectedClasses.ToList()).ToList();
            viewModel.Teacher = _unitOfWork.Teachers.Get(viewModel.Teacher.Id);
            var advertisement = new Advertisement(viewModel.Text, viewModel.Teacher, classes, viewModel.OnlyForParents, viewModel.BySite);
            _unitOfWork.Advertisements.Add(advertisement);
            _unitOfWork.Complete();

            if (!viewModel.ByEmail) return RedirectToAction("TeacherAdvertisements");

            if(viewModel.OnlyForParents)
                _noticeService = new NoticeToParentsService();
            else
                _noticeService = new NoticeToAllService();
            _noticeService.SendNotice(advertisement);

            return RedirectToAction("TeacherAdvertisements");
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult AddFile(int subjectId, HttpPostedFileBase file)
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory + @"SubjectFiles\\";
            if ((file == null) || (file.ContentLength <= 0)) return View("~/Views/Shared/CustomMessage.cshtml", (object)"Wystąpił nieoczekiwany błąd"); ;

            var fileName = Path.GetFileName(file.FileName);

            if (fileName != null)
            {
                var finallyPath = Path.Combine(directory, fileName);
                file.SaveAs(finallyPath);
                var subject = _unitOfWork.Subjects.Get(subjectId);
                _unitOfWork.Files.Add(new SubjectFile(subject, finallyPath));
            }
            _unitOfWork.Complete();

            return View("~/Views/Shared/CustomMessage.cshtml", (object)"Plik wgrany na serwer");
        }
    }
}