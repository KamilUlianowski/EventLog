﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using WebLog.Core.Models;

namespace WebLog.Core.Common
{
    public static class ConvertHelper
    {
        public static Dictionary<string, List<SchoolGrade>> StudentSchoolGrades(IEnumerable<SchoolGrade> schoolGrades)
        {
            var dictGrades = new Dictionary<string, List<SchoolGrade>>();
            if(schoolGrades == null) return dictGrades;
            foreach (var schoolGrade in schoolGrades)
            {
                if (dictGrades.ContainsKey(schoolGrade.Subject.Name))
                    dictGrades[schoolGrade.Subject.Name].Add(schoolGrade);
                else
                    dictGrades.Add(schoolGrade.Subject.Name, new List<SchoolGrade>() { schoolGrade });
            }

            return dictGrades;
        }

        public static Dictionary<string, Dictionary<string, List<int>>> SummaryGrades(List<SchoolGrade> schoolGrades)
        {
            Dictionary<string, Dictionary<string, List<int>>> dict =
                new Dictionary<string, Dictionary<string, List<int>>>();

            foreach (var schoolGrade in schoolGrades)
            {
                if (dict.ContainsKey(schoolGrade.Subject.Name))
                {
                    if (
                        dict[schoolGrade.Subject.Name].ContainsKey(schoolGrade.Student.Name + " " +
                                                                   schoolGrade.Student.Surname))
                        dict[schoolGrade.Subject.Name][schoolGrade.Student.Name + " " + schoolGrade.Student.Surname].Add
                            ( (int)(schoolGrade.Grade));
                    else
                        dict[schoolGrade.Subject.Name].Add(
                            schoolGrade.Student.Name + " " + schoolGrade.Student.Surname,
                            new List<int>() { (int)(schoolGrade.Grade) });
                }
                else
                {
                    dict.Add(schoolGrade.Subject.Name, new Dictionary<string, List<int>>());
                    dict[schoolGrade.Subject.Name].Add(
                           schoolGrade.Student.Name + " " + schoolGrade.Student.Surname,
                           new List<int>() { (int)(schoolGrade.Grade) });
                }

            }

            return dict;
        }

        //email od kogo - w tuplu kto wysłał (ty czy ktoś inny) i treść
        public static Dictionary<Tuple<string,string>, List<string>> ConvertMessagesToShowInView(List<Message> messages, string email)
        {
            var viewMessages = new Dictionary<Tuple<string,string>, List<string>>();

            foreach (var message in messages)
            {
                var userFrom = new Tuple<string, string>(message.UserFrom.Email,message.UserFrom.Name + "_" + message.UserFrom.Surname);
                var userTo = new Tuple<string, string>(message.UserTo.Email, message.UserTo.Name + "_" + message.UserTo.Surname);

                if (!viewMessages.ContainsKey(userFrom) && message.UserFrom.Email != email)
                    viewMessages.Add(userFrom, new List<string>() { message.Text });

                else if (!viewMessages.ContainsKey(userTo) && message.UserTo.Email != email)
                    viewMessages.Add(userTo, new List<string>() { message.Text });

                else if (viewMessages.ContainsKey(userFrom) && message.UserFrom.Email != email)
                    viewMessages[userFrom].Add(message.Text);

                else if (viewMessages.ContainsKey(userTo) && message.UserTo.Email != email)
                     viewMessages[userTo].Add(message.Text);

            }

            return viewMessages;
        }

        public static List<Tuple<string, string, List<Tuple<int,string, bool, DateTime, string>>>> GetConvertedMessages(List<Message> messages, string mail)
        {
            var conMessages = new List<Tuple<string, string, List<Tuple<int, string, bool, DateTime, string>>>>();
            foreach (var message in messages)
            {
                var userFrom = message.UserFrom.Name + " " + message.UserFrom.Surname;
                var userTo = message.UserTo.Name + " " + message.UserTo.Surname;

                if (message.UserFrom.Email.ToLower() != mail.ToLower())
                {
                    if (conMessages.Any(x => x.Item1 == userFrom))
                        conMessages.FirstOrDefault(x => x.Item1 == userFrom).Item3.Add(new Tuple<int, string, bool, DateTime, string>
                            (message.Id,message.Text, false, message.SendDate, userFrom));
                    else
                        conMessages.Add(new Tuple<string,string, List<Tuple<int,string, bool, DateTime, string>>>
                            (userFrom, message.UserFrom.Email, new List<Tuple<int, string, bool, DateTime, string>>
                            { new Tuple<int, string, bool, DateTime, string>(message.Id, message.Text, false, message.SendDate, userFrom) }));
                }
                else if (message.UserTo.Email.ToLower() != mail.ToLower())
                {
                    if (conMessages.Any(x => x.Item1 == userTo))
                        conMessages.FirstOrDefault(x => x.Item1 == userTo).Item3.Add(new Tuple<int, string, bool, DateTime, string>
                            (message.Id, message.Text, true, message.SendDate, userFrom));
                    else
                        conMessages.Add(new Tuple<string, string, List<Tuple<int,string, bool, DateTime, string>>>
                            (userTo, message.UserTo.Email, new List<Tuple<int,string, bool, DateTime, string>>
                            { new Tuple<int, string, bool, DateTime, string>(message.Id, message.Text, true, message.SendDate, userFrom)}));

                }
            }
            
            return conMessages;   
        }
    }
}