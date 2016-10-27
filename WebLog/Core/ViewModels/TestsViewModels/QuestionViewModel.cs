using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Models;

namespace WebLog.Core.ViewModels.TestsViewModels
{
    public class QuestionViewModel
    {
        public string Text { get; set; }
        public List<QuestionAnswer> Answers { get; set; }
        public int CorrectAnswer { get; set; }
        public int Points { get; set; }
    }
}