using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace WebLog.Core.Services
{
    public interface IMailService
    {
        void RemindOrChangePassword(string email, string password);
    }
}