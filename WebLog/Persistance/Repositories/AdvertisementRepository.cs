using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebLog.Core.Models;
using WebLog.Core.Repositories;

namespace WebLog.Persistance.Repositories
{
    public class AdvertisementRepository : Repository<Advertisement>, IAdvertisementRepository
    {
        private readonly LogDbContext _context;

        public AdvertisementRepository(LogDbContext context) : base(context)
        {
            _context = context;
        }

        public override IEnumerable<Advertisement> GetAll()
        {
            return _context.Advertisements.Include(x => x.Teacher);
        }
    }
}