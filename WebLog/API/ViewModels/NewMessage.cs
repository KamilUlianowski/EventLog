using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLog.API.ViewModels
{
    public class NewMessage
    {
        public string Email { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public int Id { get; set; }
    }
}