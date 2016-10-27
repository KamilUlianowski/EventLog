using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.ViewModels.TestsViewModels;

namespace WebLog.Core.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<QuestionAnswer> Answers { get; set; }
        public int CorrectAnswer { get; set; }
        public int Points { get; set; }
        public Test Test { get; set; }

        public Question()
        {
            
        }

        public Question(NewQuestionViewModel vm, Test test)
        {
            Text = vm.QuestionText;
            CorrectAnswer = vm.CorrectAnswer;
            Points = vm.Points;
            Test = test;
        }
    }
}