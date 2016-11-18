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

    }
}