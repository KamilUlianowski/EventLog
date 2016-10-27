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
    public class TestRepository : Repository<Test>, ITestRepository
    {
        private readonly LogDbContext _context;

        public TestRepository(LogDbContext context) : base(context)
        {
            _context = context;
        }

        public override IEnumerable<Test> GetAll()
        {
            return _context.Tests.Include(x => x.Subject);
        }

        public IEnumerable<Test> GetTestsFromSubject(int subjectId)
        {
            return _context.Tests.Include(x => x.Subject)
                .Where(x => x.Subject.Id == subjectId);
        }

        public void Update(TestDetailViewModel testDetail)
        {
            var test = Get(testDetail.Id);

            if (test == null)
                return;

            test.Name = testDetail.Name;
            test.Time = testDetail.Time;
        }

        public override Test Get(int id)
        {
            return _context.Tests.Include(x => x.Subject)
                .Include(x => x.Questions)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}