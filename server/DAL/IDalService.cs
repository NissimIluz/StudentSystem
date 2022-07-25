using Models;

namespace DAL
{
    public interface IDalService
    {
        Task<bool> AddStudent(Student student);
        Task<List<Student>> GetAll();
        Task<bool> AddStudents(IEnumerable<Student> onlyNew);
    }
}
