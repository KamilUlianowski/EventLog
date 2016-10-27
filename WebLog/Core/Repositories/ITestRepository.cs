using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using WebLog.Core.Models;
using WebLog.Core.ViewModels.TestsViewModels;

namespace WebLog.Core.Repositories
{
    public interface ITestRepository : IRepository<Test>
    {
        IEnumerable<Test> GetTestsFromSubject(int subjectId);
        void Update(TestDetailViewModel testDetail);
    }
}