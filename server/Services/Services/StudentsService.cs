using Constracts.Interfaces;
using DAL;
using Microsoft.Extensions.Configuration;
using Models;
using System.Linq;

namespace Services.Services
{
    public class StudentsService : IStudentService
    {
        private readonly IDalService _dalService;
        private readonly IConvertorService _convertorService;
        public StudentsService(IConfiguration configuration, IDalService dalService, IConvertorService convertorService)
        {
            _dalService = dalService;
            _convertorService = convertorService;
        }


        public async Task<bool> Register(Student student)
        {
            if (!IsraelValidId(student.Id)) return false;
            if (student.BirthDate > DateTime.Now) return false;
            if (student.ImmigrationDate > DateTime.Now) return false;

            return await _dalService.AddStudent(student);

        }
        public async Task<IEnumerable<Student>> RegisterByCsv(string data)
        {
            var newStudents = _convertorService.ConvertToStudents(data);
            var existStudentsIds = (await _dalService.GetAll()).Select(s => s.Id);
            var onlyNew = newStudents.Where(s => !existStudentsIds.Contains(s.Id));
            await _dalService.AddStudents(onlyNew);
            var exist = newStudents.Where(s => existStudentsIds.Contains(s.Id));
            return exist;

        }
        public Task<bool> RegisterMany(List<Student> students)
        {
            return _dalService.AddStudents(students);
        }
        public bool IsraelValidId(int idNumber)
        {
            char[] digits = idNumber.ToString().PadLeft(9, '0').ToCharArray();
            int[] multiply = new int[9];
            int[] oneDigit = new int[9];
            for (int i = 0; i < 9; i++)
                multiply[i] = Convert.ToInt32(digits[i].ToString()) * (i % 2 == 0 ? 1 : 2);
            for (int i = 0; i < 9; i++)
                oneDigit[i] = (int)(multiply[i] / 10) + multiply[i] % 10;
            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += oneDigit[i];
            return sum % 10 == 0;
        }
        public List<Student> RegisterFromCsv()
        {
            List<Student> students = File.ReadLines("")
                .Skip(1)
                .Select(v => ConverToStudent(v))
                .ToList();
            return students;
        }

        private Student ConverToStudent(string line)
        {
            Student retVal = new Student();
            string[] values = line.Split(',');
            return retVal;
        }
    }
}
