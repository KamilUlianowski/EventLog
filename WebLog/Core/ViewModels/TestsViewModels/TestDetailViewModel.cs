using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Models;

namespace WebLog.Core.ViewModels.TestsViewModels
{
    public class TestDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Time { get; set; }
        public List<Question> Questions { get; set; }
        public Subject Subject { get; set; }

        public TestDetailViewModel()
        {
            
        }

        public TestDetailViewModel(Test test)
        {
            Id = test.Id;
            Name = test.Name;
            Time = test.Time;
            Questions = test.Questions;
            Subject = test.Subject;
        }
    }
}