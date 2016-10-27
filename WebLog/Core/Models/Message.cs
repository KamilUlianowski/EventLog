using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebLog.Core.ViewModels.AuthViewModels;
using WebLog.Core.ViewModels.UserAccountsViewModels;

namespace WebLog.Core.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Text { get; set; }

        public DateTime SendDate { get; set; }
        public User UserFrom { get; set; }
        public User UserTo { get; set; }

        public Message()
        {
          SendDate = DateTime.Now;  
        }

        public Message(User userFrom, User userTo, string text)
        {
            UserFrom = userFrom;
            UserTo = userTo;
            Text = text;
            SendDate = DateTime.Now;
        }

        public Message(ParentAccountViewModel parentViewModel, Teacher teacher)
        {
            SendDate = DateTime.Now;
            Text = parentViewModel.Message;
            UserFrom = parentViewModel.Parent;
            UserTo = teacher;
  
            
        }
    }
}