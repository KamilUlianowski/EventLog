using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebLog.Core.Models
{
    public class SubjectFile
    {
        public int Id { get; set; }

        [Required]
        public string Path { get; set; }

        public Subject Subject { get; set; }

        public SubjectFile()
        {
            
        }

        public SubjectFile(Subject subject, string path)
        {
            Subject = subject;
            Path = path;
        }
    }
}