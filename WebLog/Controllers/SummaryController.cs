using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLog.Core;
using WebLog.Core.Common;
using WebLog.Core.ViewModels.SummaryViewModels;

namespace WebLog.Controllers
{
    public class SummaryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SummaryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult Index()
        {

            return View(new SummaryViewModel(
                _unitOfWork.Students.GetAll().ToList(),           
               _unitOfWork.Subjects.GetAll().ToList()
                ));
        }

        public ActionResult Search(SummaryViewModel summaryViewModel)
        {
            var schoolGrades = _unitOfWork.SchoolGrades.GetAll();
            if (summaryViewModel.SelectedStudent != null)
                schoolGrades = schoolGrades.Where(x => x.Student.Id == summaryViewModel.SelectedStudent);
            if (summaryViewModel.SelectedSubject != null)
                schoolGrades = schoolGrades.Where(x => x.Subject.Id == summaryViewModel.SelectedSubject);
            if (summaryViewModel.StartDate.Year > 2000)
                schoolGrades = schoolGrades.Where(x => x.Date > summaryViewModel.StartDate);
            if (summaryViewModel.EndDate.Year > 2000)
                schoolGrades = schoolGrades.Where(x => x.Date < summaryViewModel.EndDate);

            return PartialView("SummaryPartial", ConvertHelper.SummaryGrades(schoolGrades.ToList()));
        }

    }
}