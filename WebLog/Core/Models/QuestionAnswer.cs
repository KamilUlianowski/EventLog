using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLog.Core.Models
{
    public class QuestionAnswer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Question Question { get; set; }

        public QuestionAnswer()
        {
            
        }

        public QuestionAnswer(string text, Question question)
        {
            Text = text;
            Question = question;
        }
    }
}