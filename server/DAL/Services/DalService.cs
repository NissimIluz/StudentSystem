using Constracts.Interfaces;
using Microsoft.Extensions.Configuration;
using Models;
using System.ComponentModel;

namespace DAL.Services
{
    public class Dal : IDalService
    {
        private readonly IConfiguration _configuration;
        private readonly IConvertorService _convertorService;
        public Dal(IConfiguration configuration, IConvertorService convertorService)
        {
            _configuration = configuration;
            _convertorService = convertorService;
        }

        public async Task<bool> AddStudent(Student student)
        {
            try
            {
                await SaveToCsv<Student>(new List<Student>() { student });
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> AddStudents(IEnumerable<Student> onlyNew)
        {
            try
            {
                await SaveToCsv<Student>(onlyNew);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<Student>> GetAll()
        {
            try
            {
                List<Student> students = new List<Student>();
                string data = await File.ReadAllTextAsync(_configuration["CsvFilePath"]);;
                return _convertorService.ConvertToStudents(data);
                
            }
            catch
            {
                return null;
            }
        }
        private async Task SaveToCsv<T>(IEnumerable<T> reportData, string path = null)
        {
            path = path == null ? _configuration["CsvFilePath"] : path; 
            var lines = new List<string>();
            IEnumerable<PropertyDescriptor> props = TypeDescriptor.GetProperties(typeof(T)).OfType<PropertyDescriptor>();
            var header = string.Join(",", props.ToList().Select(x => x.Name));
            if (!File.Exists(Path.GetFullPath(path)))
            {
                lines.Add(header);
            }
            var valueLines = reportData.Select(row => string.Join(",", header.Split(',').Select(a => row.GetType().GetProperty(a).GetValue(row, null))));
            lines.AddRange(valueLines);
            await File.AppendAllLinesAsync(path, lines.ToArray());
        }
    }
}
