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
        public DateTime Date { get; set; } // problemy z datą mogą być od nauczyciela ef
        public Teacher Teacher { get; set; }
        public List<SchoolClass> Classes { get; set; }

        public Advertisement()
        {
            Date = DateTime.Now;
        }

        public Advertisement(string text, Teacher teacher)
        {
            Text = text;
            Teacher = teacher;
            Date = DateTime.Now;
        }
    }
}