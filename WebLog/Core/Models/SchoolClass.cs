using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebLog.Core.Models
{
    public class SchoolClass
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public List<Student> Students { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public Teacher Teacher { get; set; }
        public List<Advertisement> Advertisements { get; set; }

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