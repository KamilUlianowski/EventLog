using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebLog.Core;
using WebLog.Core.Models;
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


        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/images/profile"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                    var user =  (Teacher)_unitOfWork.Users.GetUser(User.Identity.GetUserId());

                    _unitOfWork.Teachers.UploadImage(user.Id, array);
                }

            }
            // after successfully uploading redirect the user
            return RedirectToAction("Profile", "Edit");
        }

    }
}