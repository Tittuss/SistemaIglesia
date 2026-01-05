using Application.UseCases.Students;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("{studentId}/grades")]
        public async Task<IActionResult> GetMyGrades(Guid studentId)
        {
            var result = await _studentService.GetMyGradesAsync(studentId);
            return Ok(result);
        }
    }
}
