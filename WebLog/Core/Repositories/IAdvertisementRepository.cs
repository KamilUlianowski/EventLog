using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using WebLog.Core.Models;

namespace WebLog.Core.Repositories
{
    public interface IAdvertisementRepository : IRepository<Advertisement>
    {
        IEnumerable<Advertisement> GetAdvertisements(int schoolClassId);
        IEnumerable<Advertisement> GetAdvertisements(Teacher teacher);
        IEnumerable<Advertisement> GetMainAdvertisements();
    }
}