﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;
using WebLog.Core.Proxy;
using WebLog.Core.ViewModels;
using WebLog.Core.ViewModels.AuthViewModels;

namespace WebLog.Core.Models
{
    [Table("Teacher")]
    public class Teacher : User
    {
        public ImageTeacher Pitcure { get; set; }
        public IFacePicture FaceImage { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public ICollection<SchoolClass> SchoolClasses { get; set; }
        public ICollection<Advertisement> Advertisements { get; set; }

        public Teacher(string name, string surname, string email, string password, string pesel) : base(name,surname,email,password,pesel)
        {

        }

        public Teacher()
        {
            Subjects = new List<Subject>();
            SchoolClasses = new List<SchoolClass>();
            Advertisements= new List<Advertisement>();
            FaceImage = new ProxyImage();

        }

        public Teacher(SignUpViewModel signUpViewModel) : base(signUpViewModel)
        {
            Subjects = new List<Subject>();
        }
    }
}