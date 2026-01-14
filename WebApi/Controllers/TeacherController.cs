using Shared.DTOs;
using Application.UseCases.Teachers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet("{teacherId}/courses")]
        public async Task<IActionResult> GetAssignedCourses(Guid teacherId)
        {
            var courses = await _teacherService.GetAssignedCoursesAsync(teacherId);
            return Ok(courses);
        }

        [HttpGet("course/{courseId}/students")]
        public async Task<IActionResult> GetStudentsByCourse(Guid courseId)
        {
            var students = await _teacherService.GetStudentsByCourseAsync(courseId);
            return Ok(students);
        }

        [HttpPut("grade")]
        public async Task<IActionResult> UpdateGrade([FromBody] UpdateGradeDto gradeDto)
        {
            try
            {
                await _teacherService.UpdateStudentGradeAsync(gradeDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
