using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Common;
using WebLog.Core.Models;
using WebLog.Core.Services;

namespace WebLog.Persistance.Services
{
    public class NoticeToParentsService : INoticeService
    {
        public void SendNotice(Advertisement advertisement)
        {
            foreach (var schoolClass in advertisement.Classes)
            {
                foreach (var student in schoolClass.Students)
                {
                    if (student.Parent != null)
                    {
                        Mail.Send(advertisement.Text, student.Parent.Email, "Ogłoszenie");
                    }
                }
            }
        }
    }
}