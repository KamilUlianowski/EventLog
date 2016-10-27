using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebLog.Core.Models;
using WebLog.Core.Repositories;

namespace WebLog.Persistance.Repositories
{
    public class QuestionAnswerRepository : Repository<QuestionAnswer>, IQuestionAnswerRepository
    {
        private readonly LogDbContext _context;

        public QuestionAnswerRepository(LogDbContext context) : base(context)
        {
            _context = context;
        }
    }
}