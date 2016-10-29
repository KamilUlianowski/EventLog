using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebLog.Core.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string Content { get; set; }

        public string ImagePath { get; set; }

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