using System;
using System.Collections.Generic;
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
        public string Text { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? SendDate { get; set; }
        public User UserFrom { get; set; }
        public User UserTo { get; set; }

        public Message()
        {
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