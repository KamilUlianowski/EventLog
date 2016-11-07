using WebLog.Core.Models;

namespace WebLog.Core.Repositories
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        Teacher GetTeacher(string mail);
    }
}