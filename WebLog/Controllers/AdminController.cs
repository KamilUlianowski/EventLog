using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLog.Core;
using WebLog.Core.Common;
using WebLog.Core.Models;
using WebLog.Core.ViewModels;

namespace WebLog.Controllers
{
    public class AdminController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Admin
        public ActionResult Manage()
        {
            return View();
        }

        public ActionResult Subjects()
        {
            return PartialView();
        }

        public ActionResult Classes()
        {
            return PartialView();
        }

        public ActionResult Teachers()
        {
            return PartialView();
        }

        public ActionResult Students()
        {
            return PartialView();
        }

        public ActionResult Advertisements()
        {
            return PartialView();
        }

        public ActionResult SchoolClasses()
        {
            return View(new ManageViewModel(
                        _unitOfWork.Classes.GetAll().ToList()));
        }

        [HttpGet]
        public ActionResult AddClass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddClass(SchoolClassViewModel schoolViewModel)
        {
            if (schoolViewModel == null)
                return RedirectToAction("Index", "Home");

            _unitOfWork.Classes.Add(schoolViewModel);
            _unitOfWork.Complete();
            return RedirectToAction("SchoolClasses", "Admin");
        }

        [HttpGet]
        public ActionResult AddSubject()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSubject(NewSubjectViewModel newSubjectViewModel)
        {
            _unitOfWork.Subjects.Add(new Subject(newSubjectViewModel.Name));
            _unitOfWork.Complete();
            return RedirectToAction("Subjects");

        }


        [HttpGet]
        public ActionResult SchoolClassDetail(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            return View(new SchoolClassViewModel(_unitOfWork.Classes.Get(id.Value),
                _unitOfWork.Students.GetAll().ToList(),
                _unitOfWork.Teachers.GetAll().ToList()));
        }

        [HttpGet]
        public ActionResult SubjectDetail(int? subjectId)
        {
            if (subjectId == null)
                return RedirectToAction("Index", "Home");

            var subject = _unitOfWork.Subjects.Get(subjectId.Value);
            var schoolClasses = _unitOfWork.Classes.GetAll().ToList();
            var teachers = _unitOfWork.Teachers.GetAll().ToList();

            if (subject == null)
                RedirectToAction("Index", "Home");

            return PartialView(new SubjectDetailViewModel(subject, schoolClasses, teachers));
        }

        [HttpPost]
        public ActionResult AddOrRemoveSubjectToClass(int subjectId, int schoolClassId)
        {

            _unitOfWork.Subjects.UpdateSubjectClasses(subjectId, schoolClassId);
            _unitOfWork.Complete();

            return RedirectToAction("Subjects");
        }

        [HttpPost]
        public ActionResult AddOrRemoveSubjectTeacher(int subjectId, int teacherId)
        {

            _unitOfWork.Subjects.UpdateSubjectTeachers(subjectId, teacherId);
            _unitOfWork.Complete();

            return RedirectToAction("Subjects");
        }

        [HttpPost]
        public ActionResult AddStudentToClass(SchoolClassViewModel schoolClassViewModel)
        {
            _unitOfWork.Classes.AddStudent(schoolClassViewModel.Id, schoolClassViewModel.SelectedStudentId);
            _unitOfWork.Complete();
            return RedirectToAction("SchoolClassDetail", "Admin", new { id = schoolClassViewModel.Id });
        }

        [HttpPost]
        public ActionResult AddTeacherToClass(SchoolClassViewModel schoolClassViewModel)
        {
            _unitOfWork.Classes.AddTeacher(schoolClassViewModel.Id, schoolClassViewModel.SelectedTeacherId);
            _unitOfWork.Complete();
            return RedirectToAction("SchoolClassDetail", "Admin", new { id = schoolClassViewModel.Id });
        }

        [HttpPost]
        public ActionResult DeleteStudent(SchoolClassViewModel schoolClassViewModel)
        {
            _unitOfWork.Classes.RemoveStudent(schoolClassViewModel.Id, schoolClassViewModel.SelectedStudentId);
            _unitOfWork.Complete();
            return RedirectToAction("SchoolClassDetail", "Admin", new { id = schoolClassViewModel.Id });
        }

        [HttpPost]
        public ActionResult DeleteTeacher(SchoolClassViewModel schoolClassViewModel)
        {
            _unitOfWork.Classes.RemoveTeacher(schoolClassViewModel.Id);
            _unitOfWork.Complete();
            return RedirectToAction("SchoolClassDetail", "Admin", new { id = schoolClassViewModel.Id });
        }



    }
}