using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
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
        private readonly IAuthService _authService;
        private readonly IMailService _iMailService;
        private readonly IUnitOfWork _unitOfWork;

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
        [Authorize]
        public ActionResult MySchool() // brzydka metoda, może do poprawy
        {
            var a = 5;
            var user = _unitOfWork.Users.GetUser(User.Identity.GetUserId());

            if (user == null)
                return RedirectToAction("Index");

            if (user.GetType() == typeof(Admin))
            {
                return RedirectToAction("Manage", "Admin");
            }

            if (user.GetType() == typeof(Student))
            {
                var student = _unitOfWork.Students.Get(user.Id);
                return View("StudentAccount", student);
            }

            if (user.GetType() == typeof(Teacher))
            {
                var teacher = _unitOfWork.Teachers.Get(user.Id);
                var schoolClasses = _unitOfWork.Classes.GetAll().ToList();
                return View("TeacherAccount", new TeacherAccountViewModel(teacher, schoolClasses));
            }

            if (user.GetType() == typeof(Parent))
            {
                var parent = _unitOfWork.Parents.Get(user.Id);
                var teachers = _unitOfWork.Teachers.GetAll().ToList();
                return View("ParentAccount", new ParentAccountViewModel(parent, teachers));
            }


            return RedirectToAction("Index");
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
        [Authorize(Roles = "Teacher")]
        public ActionResult TeacherClass(int? subjectId, int? schoolClassId)
        {
            var teacher = (Teacher)_unitOfWork.Users.GetUser(User.Identity.GetUserId());

            if ((subjectId == null) || (schoolClassId == null) || (teacher == null))
                return RedirectToAction("Index");

            var schoolClass = _unitOfWork.Classes.Get(schoolClassId.Value);
            var subject = _unitOfWork.Subjects.Get(subjectId.Value);
            var schoolGrades = _unitOfWork.SchoolGrades.GetSchoolGrades(subjectId.Value, schoolClassId.Value).ToList();

            if ((schoolClass == null) || (subject == null))
                return RedirectToAction("Index");

            return PartialView(new StudentGradesViewModel(subject, schoolClass, teacher, schoolGrades));
        }

        [HttpPost]
        [Authorize]
        public void SendMessage(MessageViewModel vm, string userToEmail)
        {
            var userFrom = _unitOfWork.Users.GetUser(User.Identity.GetUserId());
            var userTo = _unitOfWork.Users.GetUser(userToEmail);

            _unitOfWork.Messages.Add(new Message(userFrom, userTo, vm.Message));
            _unitOfWork.Complete();
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public void AddGrade(StudentGradesViewModel vm, int selectedStudent)
        {
            _unitOfWork.SchoolGrades.AddGrade(vm.NewGrade, vm.Teacher.Id, vm.Subject.Id, selectedStudent);
            _unitOfWork.Complete();
        }

        [HttpPost]
        [Authorize]
        public ActionResult RequestToTeacher(ParentAccountViewModel parentViewModel)
        {
            parentViewModel.Parent = _unitOfWork.Parents.Get(parentViewModel.Parent.Id);
            _unitOfWork.Messages.Add(new Message(parentViewModel,
                _unitOfWork.Teachers.Get(parentViewModel.SelectedTeacher)));
            _unitOfWork.Complete();

            return RedirectToAction("MySchool");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Messages()
        {
            return View(new MessageViewModel(User.Identity.GetUserId(),
                _unitOfWork.Messages.GetMessages(User.Identity.GetUserId()).ToList()));
        }

        [HttpGet]
        [Authorize]
        public ActionResult StudentSubjects()
        {
            return PartialView((Student)_unitOfWork.Students.GetStudent(User.Identity.GetUserId()));
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

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult AddContent(SubjectSiteViewModel vm)
        {
            _unitOfWork.Subjects.UpdateContent(vm.Subject.Id, vm.Content);
            _unitOfWork.Complete();
            var subjects = _unitOfWork.Subjects.GetAll().ToList();
            return View("Subject", new SubjectSiteViewModel(_unitOfWork.Subjects.Get(vm.Subject.Id), subjects));
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult AddFile(int subjectId, HttpPostedFileBase file)
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory + @"SubjectFiles\\";
            if ((file == null) || (file.ContentLength <= 0)) return View("Index");

            var fileName = Path.GetFileName(file.FileName);

            if (fileName != null)
            {
                var finallyPath = Path.Combine(directory, fileName);
                file.SaveAs(finallyPath);
                var subject = _unitOfWork.Subjects.Get(subjectId);
                _unitOfWork.Files.Add(new SubjectFile(subject, finallyPath));
            }
            _unitOfWork.Complete();
            var subjects = _unitOfWork.Subjects.GetAll().ToList();
            return View("Subject", new SubjectSiteViewModel(_unitOfWork.Subjects.Get(subjectId), subjects));
        }

        [HttpGet]
        public void ChangeLanguage(string lang)
        {
            Session["lang"] = lang;
        }


        [HttpPost]
        public ActionResult SignIn(SignInViewModel signInViewModel)
        {
            _authService.Login(signInViewModel, System.Web.HttpContext.Current);

            return RedirectToAction("MySchool", "Home");
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

        [HttpGet]
        public ActionResult SignUp(UserViewModel userViewModel)
        {
            return View();
        }


        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel vm)
        {
            _unitOfWork.Users.UpdatePassword(vm);
            _unitOfWork.Complete();
            return View("Index");
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

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            var myCookies = Request.Cookies.AllKeys;
            foreach (var cookie in myCookies)
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);

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
            return _unitOfWork.Users.CheckToken(userToken)
                ? View(new ChangePasswordViewModel(userToken))
                : View("Index");
        }

        [HttpGet]
        public ActionResult StudentSubjectDetail(int subjectId)
        {
            var subject = _unitOfWork.Subjects.Get(subjectId);

            return PartialView(subject);
        }

        [HttpPost]
        public ActionResult RemindPassword(string email)
        {
            _unitOfWork.Users.UpdateToken(email);
            _unitOfWork.Complete();
            _iMailService.RemindOrChangePassword(email, _unitOfWork.Users.GetUser(email).Token);
            return RedirectToAction("Confirm", new { message = "Wysłano maila" });
        }

        [HttpGet]
        public ActionResult StudentAdvertisements(int classId)
        {
            return PartialView(_unitOfWork.Advertisements.GetAdvertisements(classId).ToList());
        }

        [HttpGet]
        public ActionResult TeacherAdvertisements()
        {
            var teacher = _unitOfWork.Teachers.GetTeacher(User.Identity.GetUserId());
            if (teacher == null) return PartialView(new TeacherAccountViewModel());
            return PartialView(new TeacherAccountViewModel(teacher));
        }

        [HttpPost]
        public ActionResult AddAdvertisement(TeacherAccountViewModel viewModel)
        {
            var classes = _unitOfWork.Classes.GetClasses(viewModel.SelectedClasses.ToList()).ToList();
            viewModel.Teacher = _unitOfWork.Teachers.Get(viewModel.Teacher.Id);
            var advertisement = new Advertisement(viewModel.Text, viewModel.Teacher, classes, viewModel.OnlyForParents, viewModel.BySite);
            _unitOfWork.Advertisements.Add(advertisement);

            _unitOfWork.Complete();

            if (viewModel.ByEmail)
                _iMailService.SendAdvertisement(advertisement, viewModel.OnlyForParents);

            return RedirectToAction("TeacherAdvertisements");
        }

        [HttpGet]
        public ActionResult Confirm(string message)
        {
            return View((object)message);
        }
    }
}