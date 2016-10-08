using WebLog.Core.Models;

namespace WebLog.Core.Repositories
{
    public interface IParentRepository : IRepository<Parent>
    {
        void AddChildren(int parentId, Student student);
    }
}