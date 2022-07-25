using Models;

namespace Constracts.Interfaces
{
    public interface IStudentService
    {
        Task<bool> Register(Student student);
        Task<IEnumerable<Student>> RegisterByCsv(string data);
        Task<bool> RegisterMany(List<Student> students);
    }
}
