using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using WebLog.Core;
using WebLog.Core.Common;
using WebLog.Core.Models;
using WebLog.Core.Services;
using WebLog.Core.ViewModels;
using WebLog.Core.ViewModels.AuthViewModels;
using WebLog.Core.ViewModels.SubjectViewModels;
using WebLog.Core.ViewModels.UserAccountsViewModels;
using ChangePasswordViewModel = WebLog.Core.ViewModels.AuthViewModels.ChangePasswordViewModel;

namespace WebLog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly IMailService _iMailService;

        public HomeController(IUnitOfWork unitOfWork, IAuthService authService, IMailService iMailService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
            _iMailService = iMailService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MySchool() // brzydka metoda, może do poprawy
        {
            int a = 5;
            var user = _unitOfWork.Users.GetUser(User.Identity.GetUserId());

            if (user == null)
                return RedirectToAction("Index");

            if (user.GetType() == typeof(Student))
            {
                var student = _unitOfWork.Students.Get(user.Id);
                var schoolGrades = _unitOfWork.SchoolGrades.GetSchoolGrades(student.Id);
                var advertisements = _unitOfWork.Advertisements.GetAdvertisements(student.SchoolClass.Id).ToList();
                return View("StudentAccount", new StudentAccountViewModel(student, schoolGrades, advertisements));
            }

            else if (user.GetType() == typeof(Teacher))
            {
                var teacher = _unitOfWork.Teachers.Get(user.Id);
                var schoolClasses = _unitOfWork.Classes.GetAll().ToList();
                return View("TeacherAccount", new TeacherAccountViewModel(teacher, schoolClasses));
            }

            else if (user.GetType() == typeof(Parent))
            {
                var parent = _unitOfWork.Parents.Get(user.Id);
                var schoolGrades = _unitOfWork.SchoolGrades.GetSchoolGrades(parent.Student.Id);
                var teachers = _unitOfWork.Teachers.GetAll().ToList();
                return View("ParentAccount", new ParentAccountViewModel(parent, schoolGrades, teachers));
            }


            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult TeacherClass(int? subjectId, int? schoolClassId)
        {
            var teacher = (Teacher)_unitOfWork.Users.GetUser(User.Identity.GetUserId());

            if (subjectId == null || schoolClassId == null || teacher == null)
                return RedirectToAction("Index");

            var schoolClass = _unitOfWork.Classes.Get(schoolClassId.Value);
            var subject = _unitOfWork.Subjects.Get(subjectId.Value);
            var schoolGrades = _unitOfWork.SchoolGrades.GetSchoolGrades(subjectId.Value, schoolClassId.Value).ToList();

            if (schoolClass == null || subject == null)
                return RedirectToAction("Index");

            return View(new StudentGradesViewModel(subject, schoolClass, teacher, schoolGrades));

        }

        [HttpPost]
        public void AddGrade(int grade, int teacherId, int subjectId, int studentId)
        {
            _unitOfWork.SchoolGrades.AddGrade(grade, teacherId, subjectId, studentId);
            _unitOfWork.Complete();
        }

        [HttpPost]
        public ActionResult SignIn(SignInViewModel signInViewModel)
        {
            _authService.Login(signInViewModel, System.Web.HttpContext.Current);

            return RedirectToAction("MySchool", "Home");


        }

        [HttpPost]
        public void SendAdvertisement()
        {

        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(SignUpViewModel signUpViewModel)
        {
            _unitOfWork.Users.AddUser(signUpViewModel);
            _unitOfWork.Complete();

            if (signUpViewModel.TypeOfUser == TypeOfUser.Parent)
            {
                var user = _unitOfWork.Users.GetUser(signUpViewModel.Email);
                if (user == null)
                    return RedirectToAction("Index", "Error", "Wystapil nieoczekiwany blad");

                return RedirectToAction("SignUpParent", "Home", new { parentId = user.Id });
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult SignUpParent(int parentId)
        {
            var parentViewModel = new SignUpParentViewModel(parentId);
            return View(parentViewModel);
        }

        [HttpPost]
        public ActionResult SignUpParent(SignUpParentViewModel parentViewModel)
        {
            var student = _unitOfWork.Students.GetStudent(parentViewModel);

            if (student != null)
            {
                _unitOfWork.Parents.AddChildren(parentViewModel.ParentId, (Student)student);
                _unitOfWork.Complete();
            }
            else
                return RedirectToAction("Index", "Error", new { message = "Coś poszło nie tak" });


            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult RequestToTeacher(ParentAccountViewModel parentViewModel)
        {
            parentViewModel.Parent = _unitOfWork.Parents.Get(parentViewModel.Parent.Id);
            _unitOfWork.Messages.Add(new Message(parentViewModel,
                _unitOfWork.Teachers.Get(parentViewModel.SelectedTeacher)));
            _unitOfWork.Complete();

            return RedirectToAction("MySchool");
        }

        [HttpGet]
        public ActionResult SignUp(UserViewModel userViewModel)
        {
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            string[] myCookies = Request.Cookies.AllKeys;
            foreach (string cookie in myCookies)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult RemindPassword()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ChangePassword(string userToken)
        {
            return _unitOfWork.Users.CheckToken(userToken) ? View(new ChangePasswordViewModel(userToken)) : View("Index");
        }

        [HttpPost]
        public ActionResult RemindPassword(string email)
        {

            _unitOfWork.Users.UpdateToken(email);
            _unitOfWork.Complete();
            _iMailService.RemindOrChangePassword(email, _unitOfWork.Users.GetUser(email).Token);
            return RedirectToAction("Confirm", new { message = "Wysłano maila" });
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel vm)
        {
            _unitOfWork.Users.UpdatePassword(vm);
            _unitOfWork.Complete();
            return View("Index");
        }

        [HttpGet]
        public ActionResult Confirm(string message)
        {
            return View((object)message);
        }

        [HttpGet]
        public ActionResult Messages()
        {
            var messages = _unitOfWork.Messages.GetMessages(User.Identity.GetUserId()).ToList();
            return View(new MessageViewModel(User.Identity.GetUserId(), messages));
        }

        [HttpGet]
        public ActionResult Subject(int id)
        {
            var subject = _unitOfWork.Subjects.Get(id);

            return View(new SubjectSiteViewModel(subject));
        }

        [HttpGet]
        public ActionResult SubjectName(string name)
        {
            var subject = _unitOfWork.Subjects.Get(name);

            return View("Subject", new SubjectSiteViewModel(subject));
        }

        [HttpPost]
        public ActionResult AddContent(SubjectSiteViewModel vm)
        {
            _unitOfWork.Subjects.UpdateContent(vm.Subject.Id, vm.Content);
            _unitOfWork.Complete();
            return View("Subject", new SubjectSiteViewModel(_unitOfWork.Subjects.Get(vm.Subject.Id)));
        }

        [HttpPost]
        public ActionResult AddFile(int subjectId, HttpPostedFileBase file)
        {

            var directory = AppDomain.CurrentDomain.BaseDirectory + @"SubjectFiles\\";
            if (file == null || file.ContentLength <= 0) return View("Index");

            var fileName = Path.GetFileName(file.FileName);

            if (fileName != null)
            {
                var finallyPath = Path.Combine(directory, fileName);
                file.SaveAs(finallyPath);
                var subject = _unitOfWork.Subjects.Get(subjectId);
                _unitOfWork.Files.Add(new SubjectFile(subject, finallyPath));
            }
            _unitOfWork.Complete();
            return View("Subject", new SubjectSiteViewModel(_unitOfWork.Subjects.Get(subjectId)));
        }

        [HttpGet]
        public void ChangeLanguage(string lang)
        {
            Session["lang"] = lang;
        }
    }
}
