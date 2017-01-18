using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WebLog.Core.Models
{
    public enum ClassProfile
    {
        LanguageProfile,
        MedicalProfile,
        MathematicProfile
    }
    public abstract class SchoolClass
    {
        public int Id { get; set; }
        public ClassProfile ClassProfile { get; set; }
        public string AdditionalInformation { get; set; }
        public int MaxNumberOfStudents { get; set; }

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
            Students = new List<Student>();
            Subjects = new List<Subject>();
            Initialize();
            Name = name;
        }

        public SchoolClass()
        {
            Students = new List<Student>();
            Subjects = new List<Subject>();
        }

        public abstract void AssignSubjects();
        public abstract void SetMaxNumberOfStudents();
        public abstract void SetAdditionalInformation();

        public void Initialize()
        {
            AssignSubjects();
            SetMaxNumberOfStudents();
            SetAdditionalInformation();
        }
    }
}