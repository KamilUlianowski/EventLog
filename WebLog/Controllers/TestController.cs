using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using WebLog.Core;
using WebLog.Core.Common;
using WebLog.Core.Models;
using WebLog.Core.Services;
using WebLog.Core.ViewModels.SubjectViewModels;
using WebLog.Core.ViewModels.TestsViewModels;

namespace WebLog.Controllers
{
    public class TestController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataConversionService _dataConversionService;
        // GET: Test

        public TestController(IUnitOfWork unitOfWork, IDataConversionService dataConversionService)
        {
            _unitOfWork = unitOfWork;
            _dataConversionService = dataConversionService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddTest()
        {
            var subjects = _unitOfWork.Subjects.GetAll().ToList();
            return View(new NewTestViewModel(subjects));
        }

        [HttpPost]
        public ActionResult AddTest(SubjectSiteViewModel vm)
        {
            var subject = _unitOfWork.Subjects.Get(vm.SelectedSubject);

            if (subject == null)
                if (Request.UrlReferrer != null) Response.Redirect(Request.UrlReferrer.ToString());

            var test = new Test(vm.Name, subject, vm.Time);
            _unitOfWork.Tests.Add(test);
            _unitOfWork.Complete();

            return RedirectToAction("Subject", "Home", new { id = vm.Subject.Id });
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            if (User.IsInRole("Student"))
                return RedirectToAction("SolveTest", new { testId = id });

            var test = _unitOfWork.Tests.Get(id);

            if (test == null)
                if (Request.UrlReferrer != null) Response.Redirect(Request.UrlReferrer.ToString());

            return View(new TestDetailViewModel(test));
        }

        [HttpPost]
        public ActionResult Detail(TestDetailViewModel vm)
        {

            _unitOfWork.Tests.Update(vm);
            _unitOfWork.Complete();

            var test = _unitOfWork.Tests.Get(vm.Id);

            if (test == null)
                if (Request.UrlReferrer != null) Response.Redirect(Request.UrlReferrer.ToString());

            return View(new TestDetailViewModel(test));
        }

        [HttpGet]
        public ActionResult SolveTest(int testId)
        {
            var test = _unitOfWork.Tests.Get(testId);
            var user = _unitOfWork.Users.GetUser(User.Identity.GetUserId());

            var schoolGrade = _unitOfWork.SchoolGrades.GetGradeFromTest(user, test);

            if(schoolGrade == null)
                return PartialView(new SolveTestViewModel(test));

            return PartialView(new SolveTestViewModel(schoolGrade, test));

        }

        [HttpGet]
        public ActionResult AddQuestion(int id)
        {
            return View(new NewQuestionViewModel(id));
        }

        [HttpPost]
        public ActionResult AddQuestion(NewQuestionViewModel vm)
        {
            _unitOfWork.Questions.AddQuestion(vm);
            _unitOfWork.Complete();

            return RedirectToAction("Detail", new { id = vm.TestId });
        }

        [HttpPost]
        public void SaveTestResult(string result, int testId)
        {
            var test = _unitOfWork.Tests.Get(testId);
            var testResult = _dataConversionService.GetResult(_dataConversionService.ConvertToDict(result), testId);
            var grade = _dataConversionService.GetGrade(testResult, test.Questions);
            var user = _unitOfWork.Users.GetUser(User.Identity.GetUserId());
            _unitOfWork.SchoolGrades.Add( new SchoolGrade((Grade) grade, (Student)user, test.Subject, test));
            _unitOfWork.Complete();


        }
    }
}