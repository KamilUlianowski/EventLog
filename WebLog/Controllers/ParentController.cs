using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebLog.Core;
using WebLog.Core.ViewModels.UserAccountsViewModels;

namespace WebLog.Controllers
{
    public class ParentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ParentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult ParentAccount()
        {
            var user = _unitOfWork.Users.GetUser(User.Identity.GetUserId());

            var parent = _unitOfWork.Parents.Get(user.Id);
            var teachers = _unitOfWork.Teachers.GetAll().ToList();
            return View("~/Views/Parent/ParentAccount.cshtml", new ParentAccountViewModel(parent, teachers));
        }
    }
}