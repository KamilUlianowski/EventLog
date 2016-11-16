using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using WebLog.Core.Models;

namespace WebLog.Core.Services
{
    public interface IMailService
    {
        void RemindOrChangePassword(string email, string password);
        void SendAdvertisement(Advertisement advertisement, bool onlyForParents = false);
        void SendRatingSummary(List<Student> students);
    }
}