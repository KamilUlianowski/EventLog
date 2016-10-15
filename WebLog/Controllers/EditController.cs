using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebLog.Core;
using WebLog.Core.ViewModels;

namespace WebLog.Controllers
{
    public class EditController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Edit

        [Authorize]
        public ActionResult Profile()
        {
            var user = _unitOfWork.Users.GetUser(User.Identity.GetUserId());

            if (user == null)
                return RedirectToAction("Index", "Home");

            return View(new EditUserViewModel(user));
        }

        [HttpPost]
        [Authorize]
        public ActionResult Update(EditUserViewModel editUserViewModel)
        {
            _unitOfWork.Users.Update(editUserViewModel);
            _unitOfWork.Complete();
            return RedirectToAction("Profile", "Edit");
        }

    }
}