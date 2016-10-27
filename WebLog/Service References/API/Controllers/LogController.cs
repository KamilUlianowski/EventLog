using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using WebLog.Core;
using WebLog.Core.Models;
using WebLog.Core.ViewModels;
using ChangePasswordViewModel = WebLog.Core.ViewModels.AuthViewModels.ChangePasswordViewModel;

namespace WebLog.API.Controllers
{
    public class LogController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public LogController()
        {

        }


        [HttpPost]
        public IHttpActionResult AddAdvertisement(TeacherAccountViewModel viewModel)
        {
            var classes = _unitOfWork.Classes.GetClasses(viewModel.SelectedClasses.ToList()).ToList();
            viewModel.Teacher = _unitOfWork.Teachers.Get(viewModel.Teacher.Id);

            foreach (var schoolClass in classes)
                _unitOfWork.Classes.AddAdvertisement(schoolClass.Id, new Advertisement(viewModel.Text, viewModel.Teacher));

            _unitOfWork.Complete();

            return Ok();
        }


    }
}
