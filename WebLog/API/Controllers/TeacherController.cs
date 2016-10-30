using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebLog.API.ViewModels;
using WebLog.Core;

namespace WebLog.API.Controllers
{
    public class TeacherController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeacherController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
       
        [HttpGet]
        [Authorize]
        public IHttpActionResult GetSchoolGrades([FromBody] SchoolGradesViewModel vm)
        {
            var schoolGrades = _unitOfWork.SchoolGrades.GetSchoolGrades(vm.SubjectId, vm.SchoolClassId);

            return Ok(schoolGrades);
        }
    }
}
