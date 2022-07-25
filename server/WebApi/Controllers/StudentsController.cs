using Constracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger _looger;

        public StudentsController(IStudentService studentService, ILogger<StudentsController> looger)
        {
            _studentService = studentService;
            _looger = looger;
        }
        [HttpPost("Register")]
        public async Task<ObjectResult> RegisterAsync(Student students)
        {
            try
            {
                bool retVal = await _studentService.Register(students);
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _looger.LogError(ex.Message);
                return Problem();
            }
        }
        [HttpPost("RegisterMany")]
        public async Task<ObjectResult> RegisterMany(List<Student> students)
        {
            try
            {
                var retVal = await _studentService.RegisterMany(students);
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _looger.LogError(ex.Message);
                return Problem();
            }
        }
        [HttpPost("RegisterByCsv")]
        public async Task<ObjectResult> RegisterByCsvAsync(IFormFile file)
        {
            try
            {
                if (file == null) return Problem();

                var data = new StringBuilder();
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        data.AppendLine(reader.ReadLine());
                }
                var retVal = await _studentService.RegisterByCsv(data.ToString());
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _looger.LogError(ex.Message);
                return Problem();
            }
        }
    }
}