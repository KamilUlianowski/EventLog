using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core;
using WebLog.Core.Common;
using WebLog.Core.Models;
using WebLog.Core.Services;

namespace WebLog.Persistance.Services
{
    public class DataConversionService : IDataConversionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DataConversionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public DataConversionService()
        {

        }

        public Dictionary<int, int> ConvertToDict(string result)
        {
            
            result = result.Remove(result.Length - 1);
            var numbers = result.Split(',').Select(Int32.Parse).ToList();
            var dict = new Dictionary<int, int>();

            try
            {
                for (int i = 0; i < numbers.Count; i += 2)
                    dict.Add(numbers[i], numbers[i + 1]);
            }
            catch
            {
                // ignored
            }
            return dict;
        }

        public int GetResult(Dictionary<int, int> dict, int testId)
        {
            var test = _unitOfWork.Tests.Get(testId);

            if (test == null)
                return 0;

            int testResult = 0;

            foreach (var item in dict)
            {
                var question = test.Questions.FirstOrDefault(x => x.Id == item.Key);

                if (question != null && question.CorrectAnswer == item.Value)
                    testResult += question.Points;
            }

            return testResult;
        }

        public Dictionary<string, int> GetGradingScale(List<Question> listOfQuestions)
        {
            var maxPoints = listOfQuestions.Sum(x => x.Points);
            var gradingScale = new Dictionary<string, int>();
            if (maxPoints == 0) return gradingScale;
            gradingScale.Add(0 + " - " + 0.3*maxPoints + "p.", 1);
            gradingScale.Add(0.3*maxPoints + " - " + 0.5*maxPoints + "p.", 2);
            gradingScale.Add(0.5*maxPoints + " - " + 0.65*maxPoints + "p.", 3);
            gradingScale.Add(0.65*maxPoints + " - " + 0.8*maxPoints + "p.", 4);
            gradingScale.Add(0.8*maxPoints + " - " + 0.9*maxPoints + "p.", 5);
            gradingScale.Add(0.91*maxPoints + " - " + maxPoints + "p.", 6);
            return gradingScale;

        }

        public int GetGrade(int result, List<Question> questions )
        {
            var maxPoints = questions.Sum(x => x.Points);

            var procentPoints = (result / maxPoints) * 100;

            if (procentPoints < 30)
                return 1;
            else if (procentPoints >= 30 && procentPoints < 50)
                return 2;
            else if (procentPoints >= 50 && procentPoints < 65)
                return 3;
            else if (procentPoints >= 65 && procentPoints < 80)
                return 4;
            else if (procentPoints >= 80 && procentPoints < 90)
                return 5;
            else if (procentPoints >= 90)
                return 6;

            return 1;
        }
    }
}