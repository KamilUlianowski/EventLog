using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLog.Core.Models
{
    public class SchoolClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public Teacher Teacher { get; set; }

        public SchoolClass(string name)
        {
            Name = name;
        }

        public SchoolClass()
        {
            Students = new List<Student>();
        }
    }
}