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
            var user = _unitOfWork.Users.GetUser(User.Identity.GetUserId());

            if (user == null)
                return RedirectToAction("Index");

            if (user.GetType() == typeof(Admin))
                return RedirectToAction("Manage", "Admin");

            if (user.GetType() == typeof(Student))
                return RedirectToAction("StudentAccount", "Student");

            if (user.GetType() == typeof(Teacher))
                return RedirectToAction("TeacherAccount", "Teacher");

            if (user.GetType() == typeof(Parent))
                return RedirectToAction("ParentAccount", "Parent");

            return RedirectToAction("Index");
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

        [HttpPost]
        public ActionResult RemindPassword(string email)
        {
            _unitOfWork.Users.UpdateToken(email);
            _unitOfWork.Complete();
            _iMailService.RemindOrChangePassword(email, _unitOfWork.Users.GetUser(email).Token);
            return RedirectToAction("Confirm", new { message = "Wysłano maila" });
        }

        [HttpGet]
        public ActionResult Confirm(string message)
        {
            return View((object)message);
        }
    }
}