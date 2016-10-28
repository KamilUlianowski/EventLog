using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebLog.Core.Models;
using WebLog.Core.Repositories;
using WebLog.Core.ViewModels.TestsViewModels;

namespace WebLog.Persistance.Repositories
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        private readonly LogDbContext _context;

        public QuestionRepository(LogDbContext context) : base(context)
        {
            _context = context;
        }

        public void AddQuestion(NewQuestionViewModel vm)
        {
            var test = _context.Tests.Include(x => x.Subject)
                .Include(x => x.Questions)
                .FirstOrDefault(x => x.Id == vm.TestId);

            if (test == null)
                return;

            var question = new Question(vm, test);
            _context.Questions.Add(question);

            _context.QuestionAnswers.Add(new QuestionAnswer(vm.Answer1, question));
            _context.QuestionAnswers.Add(new QuestionAnswer(vm.Answer2, question));
            _context.QuestionAnswers.Add(new QuestionAnswer(vm.Answer3, question));

        }

        public override IEnumerable<Question> GetAll()
        {
            return _context.Questions.Include(x => x.Answers);
        }
    }
}