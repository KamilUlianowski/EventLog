using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Models;
using WebLog.Persistance.Services;

namespace WebLog.Core.ViewModels.TestsViewModels
{
    public class SolveTestViewModel
    {
        public List<Question> ListOfQuestions { get; set; }
        public int TestId { get; set; }
        public string Name { get; set; }
        public Dictionary<string, int> GradingScale { get; set; }
        public SchoolGrade Grade { get; set; }


        public SolveTestViewModel()
        {
            
        }

        public SolveTestViewModel(Test test)
        {
            ListOfQuestions = test.Questions;
            TestId = test.Id;
            Name = test.Name;
            GradingScale = new DataConversionService().GetGradingScale(test.Questions);
        }

        public SolveTestViewModel( SchoolGrade grade)
        {
            Grade = grade;
        }
    }
}