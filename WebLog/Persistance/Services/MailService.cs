using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using WebLog.Core.Services;

namespace WebLog.Persistance.Services
{
    public class MailService : IMailService
    {
        public void RemindOrChangePassword(string email, string password)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("kamilulianowski@gmail.com", "KUDR17131d");

                MailMessage mm = new MailMessage("kamilulianowski@gmail.com", email, "Przypomnienie Hasła",
                    "Twoje hasło to:" + password);
                mm.BodyEncoding = Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);
            }
            catch (Exception ex)
            {
                // ignored
            }
        }
    }
}