﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using WebLog.Core.Models;
using WebLog.Core.Services;

namespace WebLog.Persistance.Services
{
    public class MailService : IMailService
    {
        private static void Send(string message, string email, string subject)
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
                client.Credentials = new System.Net.NetworkCredential("czesio476@gmail.com", "KU6c2114Ppk");

                MailMessage mm = new MailMessage("czesio476@gmail.com", email, subject,
                    message);
                mm.BodyEncoding = Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        public void RemindOrChangePassword(string email, string token)
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
                client.Credentials = new System.Net.NetworkCredential("czesio476@gmail.com", "KU6c2114Ppk");

                MailMessage mm = new MailMessage("czesio476@gmail.com", email, "Przypomnienie Hasła",
                    "Link do zmiany asla:" + "http://localhost:51086/Home/ChangePassword?userToken=" + token);
                mm.BodyEncoding = Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);
            }
            catch (Exception ex)
            {
                // ignored
            }
        }


        public void SendAdvertisement(Advertisement adv, bool onlyForParents = false)
        {
            foreach (var schoolClass in adv.Classes)
            {
                foreach (var student in schoolClass.Students)
                {
                    Send(adv.Text, student.Parent.Email, "Ogłoszenie");

                    if (!onlyForParents)
                    {
                        Send(adv.Text, student.Email, "Ogłoszenie");
                    }
                }
            }
        }
    }
}