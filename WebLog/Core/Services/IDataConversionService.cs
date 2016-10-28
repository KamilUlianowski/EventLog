using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Common;
using WebLog.Core.Models;

namespace WebLog.Core.Services
{
    public interface IDataConversionService
    {
        Dictionary<int, int> ConvertToDict(string result);
        int GetResult(Dictionary<int, int> dict, int testId);
        Dictionary<string, int> GetGradingScale(List<Question> listOfQuestions);
        int GetGrade(int result, List<Question> Questions );

    }
}