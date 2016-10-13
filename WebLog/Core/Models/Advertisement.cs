using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace WebLog.Core.Models
{
    public class Advertisement
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Teacher Teacher { get; set; }

        public Advertisement()
        {
            
        }
    }
}