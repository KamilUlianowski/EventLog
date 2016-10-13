using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebLog.Core;

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


        [HttpGet]
        [ActionName("test")]
        public IHttpActionResult AddAdvertisement()
        {
            return Ok();
        }
    }
}
