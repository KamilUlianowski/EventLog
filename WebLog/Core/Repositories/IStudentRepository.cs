using WebLog.Core.Models;
using WebLog.Core.ViewModels.AuthViewModels;

namespace WebLog.Core.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        User GetStudent(SignUpParentViewModel parentViewModel);
    }
}