using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using WebLog.Core.Common;
using WebLog.Core.Models;

namespace WebLog.Core.ViewModels
{
    public class MessageViewModel
    {
        public string LoggedMail { get; set; }
        public Dictionary<string, List<string>> ViewMessages { get; set; }

        public MessageViewModel()
        {

        }

        public MessageViewModel(string loggedMail, List<Message> messages)
        {
            LoggedMail = loggedMail;
            ViewMessages = ConvertHelper.ConvertMessagesToShowInView(messages, loggedMail);
        }
    }
}