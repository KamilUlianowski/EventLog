using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Models;

namespace WebLog.Core.Repositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        IEnumerable<Message> GetMessagesFrom(string email);
        IEnumerable<Message> GetMessagesTo(string email);
        IEnumerable<Message> GetMessages(string email);
    }
}