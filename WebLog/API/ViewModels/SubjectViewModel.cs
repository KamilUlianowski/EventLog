﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLog.API.ViewModels
{
    public class SubjectViewModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int SubjectId { get; set; }
        public int ClassId { get; set; }
        public int TeacherId { get; set; }
    }
}