using Models;

namespace Constracts.Interfaces
{
    public interface IConvertorService
    {
        List<Student> ConvertToStudents(string data);
    }
}
