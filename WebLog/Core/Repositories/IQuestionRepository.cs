using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Models;
using WebLog.Core.ViewModels.TestsViewModels;

namespace WebLog.Core.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
        void AddQuestion(NewQuestionViewModel newQuestionVm);
    }
}