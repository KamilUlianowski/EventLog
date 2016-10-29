using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebLog.Core;

namespace WebLog.API.Controllers
{
    public class ManageController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        [Authorize]
        public IHttpActionResult GetSubjects()
        {
            var subjects = _unitOfWork.Subjects.GetAll();

            return Ok(subjects);
        }
    }
}
