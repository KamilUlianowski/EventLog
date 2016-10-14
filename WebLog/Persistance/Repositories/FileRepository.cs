using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebLog.Core.Models;
using WebLog.Core.Repositories;

namespace WebLog.Persistance.Repositories
{
    public class FileRepository : Repository<SubjectFile>, IFileRepository
    {
        private readonly LogDbContext _context;

        public FileRepository(LogDbContext context) : base(context)
        {
            _context = context;
        }

        public override SubjectFile Get(int id)
        {
            return _context.Files.Include(x => x.Subject)
                .FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<SubjectFile> GetAll()
        {
            return _context.Files.Include(x => x.Id);
        }
    }
}