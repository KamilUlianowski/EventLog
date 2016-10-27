using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebLog.Core;
using WebLog.Core.Models;
using WebLog.Core.ViewModels.TestsViewModels;

namespace WebLog.Controllers
{
    public class TestController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        // GET: Test

        public TestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
        public ActionResult AddTest(NewTestViewModel vm)
        {
            var subject = _unitOfWork.Subjects.Get(vm.SelectedSubject);

            if(subject == null)
                if (Request.UrlReferrer != null) Response.Redirect(Request.UrlReferrer.ToString());

            _unitOfWork.Tests.Add(new Test(vm.Name, subject, vm.Time));
            _unitOfWork.Complete();

            return RedirectToAction("Subject", "Home", new { id = vm.SelectedSubject });
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var test = _unitOfWork.Tests.Get(id);

            if(test == null)
                if (Request.UrlReferrer != null) Response.Redirect(Request.UrlReferrer.ToString());

            return View(new TestDetailViewModel(test));
        }

        [HttpPost]
        public ActionResult Detail(TestDetailViewModel vm)
        {
            _unitOfWork.Tests.Update(vm);
            _unitOfWork.Complete();

            var test = _unitOfWork.Tests.Get(vm.Id);

            if(test == null)
                if (Request.UrlReferrer != null) Response.Redirect(Request.UrlReferrer.ToString());

            return View(new TestDetailViewModel(test));
        }

        [HttpGet]
        public ActionResult AddQuestion(int testId)
        {
            return View(new NewQuestionViewModel(testId));
        }

        [HttpPost]
        public ActionResult AddQuestion(NewQuestionViewModel vm)
        {
            _unitOfWork.Questions.AddQuestion(vm);
            _unitOfWork.Complete();

            return RedirectToAction("Detail", new {id = vm.TestId});
        }
    }
}