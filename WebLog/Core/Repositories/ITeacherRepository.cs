using WebLog.Core.Models;

namespace WebLog.Core.Repositories
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        Teacher GetTeacher(string mail);

        void UploadImage(int id,byte[] arr);

        Teacher GetTeacher(int id);

        Teacher GetTeacherWithImage(int id);

        ImageTeacher GetTeacherImage(int id);
    }
}