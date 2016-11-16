﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace WebLog.Core.Models
{
    public class Advertisement
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Text { get; set; }
        public bool Visible { get; set; }
        public bool MainPage { get; set; }
        public bool OnlyForParents { get; set; }
        public DateTime Date { get; set; } // problemy z datą mogą być od nauczyciela ef
        public Teacher Teacher { get; set; }
        public List<SchoolClass> Classes { get; set; }

        public Advertisement()
        {
            Date = DateTime.Now;
        }

        public Advertisement(string text, bool mainPage = false)
        {
            Text = text;
            Visible = true;
            MainPage = mainPage;
            OnlyForParents = false;
            Date = DateTime.Now;
        }

        public Advertisement(string text, Teacher teacher, List<SchoolClass> classes, bool forParents, bool visible = false)
        {
            Text = "Wiadomość od : " + teacher.Name + " " + teacher.Surname + "\n\n" +text;
            Teacher = teacher;
            Date = DateTime.Now;
            Classes = classes;
            OnlyForParents = forParents;
            Visible = visible;
            MainPage = false;
        }
    }
}