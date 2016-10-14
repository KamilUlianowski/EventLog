using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLog.Core.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public ICollection<SchoolClass> SchoolClasses { get; set; }
        public ICollection<Teacher> Teachers { get; set; }

        public Subject()
        {
            SchoolClasses = new List<SchoolClass>();
            Teachers = new List<Teacher>();
        }

        public Subject(string name)
        {
            Name = name;
            SchoolClasses = new List<SchoolClass>();
            Teachers = new List<Teacher>();
        }
    }
}