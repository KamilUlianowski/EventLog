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

        public override Advertisement Get(int id)
        {
            return _context.Advertisements.Include(x => x.Teacher)
                .Include(x => x.Classes)
                .FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<Advertisement> GetAll()
        {
            return _context.Advertisements.Include(x => x.Teacher)
                                          .Include(x => x.Classes);
        }

        public IEnumerable<Advertisement> GetAdvertisements(int id)
        {

            return _context.Advertisements.Include(x => x.Teacher)
                .Include(x => x.Classes)
                .Where(x => x.Classes.Any(c => c.Id == id) && x.Visible);
        }

        public IEnumerable<Advertisement> GetAdvertisements(Teacher teacher)
        {
            return _context.Advertisements.Include(x => x.Teacher)
                .Include(x => x.Classes)
                .Where(x => x.Teacher.Id == teacher.Id);
        }

        public IEnumerable<Advertisement> GetMainAdvertisements()
        {
            try
            {
                return _context.Advertisements.Where(x => x.MainPage);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<Advertisement> GetAdvertisements(List<int> advertisementsId)
        {
            return _context.Advertisements.Include(x => x.Teacher)
                                          .Include(x => x.Classes)
                                          .Where(x => advertisementsId.Contains(x.Id));
        }
    }
}