using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLog.Core.Models
{
    public class SubjectFile
    {
        public int Id { get; set; }
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