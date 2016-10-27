using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLog.Core.ViewModels.TestsViewModels
{
    public class NewQuestionViewModel
    {
        public int TestId { get; set; }
        public string QuestionText { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public int CorrectAnswer { get; set; }
        public int Points { get; set; }

        public NewQuestionViewModel(int testId)
        {
            TestId = testId;
        }

        public NewQuestionViewModel()
        {
            
        }
    }
}