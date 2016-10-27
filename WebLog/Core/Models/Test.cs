using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.ViewModels.TestsViewModels;

namespace WebLog.Core.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Question> Questions { get; set; }
        public Subject Subject { get; set; }
        public int Time { get; set; }

        public Test()
        {
            
        }

        public Test(string name, Subject subject, int time)
        {
            Name = name;
            Subject = subject;
            Time = time;
        }
    }
}