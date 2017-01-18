using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Common;
using WebLog.Core.Models;
using WebLog.Core.Services;

namespace WebLog.Core.Strategy
{
    public class NoticeToStudentService :INoticeService
    {
        public void SendNotice(Advertisement advertisement)
        {
            foreach (var schoolClass in advertisement.Classes)
            {
                foreach (var student in schoolClass.Students)
                {
                    Mail.Send(advertisement.Text, student.Email, "Ogłoszenie");
                }
            }
        }
    }
}