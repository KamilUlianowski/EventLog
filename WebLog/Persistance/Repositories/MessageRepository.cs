using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebLog.Core.Models;
using WebLog.Core.Repositories;

namespace WebLog.Persistance.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly LogDbContext _context;

        public MessageRepository(LogDbContext context) : base(context)
        {
            _context = context;
        }

        public override IEnumerable<Message> GetAll()
        {
            return _context.Messages.Include(x => x.UserFrom)
                                    .Include(x => x.UserTo);
        }

        public IEnumerable<Message> GetMessagesFrom(string email)
        {
            return _context.Messages.Include(x => x.UserFrom)
                .Include(x => x.UserTo)
                .Where(x => x.UserFrom.Email == email);
        }

        public IEnumerable<Message> GetMessagesTo(string email)
        {
            return _context.Messages.Include(x => x.UserFrom)
                .Include(x => x.UserTo)
                .Where(x => x.UserTo.Email == email);
        }

        public IEnumerable<Message> GetMessages(string email)
        {
            return _context.Messages.Include(x => x.UserFrom)
                .Include(x => x.UserTo)
                .Where(x => x.UserTo.Email == email || x.UserFrom.Email == email);
        }

        public override Message Get(int id)
        {
            return _context.Messages.Include(x => x.UserFrom)
                                    .Include(x => x.UserTo)
                                    .FirstOrDefault(x => x.Id == id);
        }
    }
}