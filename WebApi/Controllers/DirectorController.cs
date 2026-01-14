using Application.DTOs;
using Application.UseCases.Director;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IDirectorService _directorService;
        public DirectorController(IDirectorService directorService)
        {
            _directorService = directorService;
        }

        [HttpPost("students")]
        public async Task<ActionResult<StudentDto>> CreateStudent([FromBody] CreateStudentDto dto)
        {
            try
            {
                var createdStudent = await _directorService.CreateStudentAsync(dto);
                return CreatedAtAction(nameof(GetStudentById), new { id = createdStudent.Id }, createdStudent);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("students")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentDto dto)
        {
            try
            {
                await _directorService.UpdateStudentAsync(dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("students/{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            try
            {
                await _directorService.DeleteStudentAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("students/{id}")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var student = await _directorService.GetStudentByIdAsync(id);
            return Ok(student);
        }


        [HttpPost("teachers")]
        public async Task<ActionResult<TeacherDto>> CreateTeacher(CreateTeacherDto dto)
        {
            var result = await _directorService.CreateTeacherAsync(dto);
            return CreatedAtAction(nameof(GetTeacherById), new { id = result.Id }, result);
        }

        [HttpGet("teachers/{id}")]
        public async Task<IActionResult> GetTeacherById(Guid id)
        {
            var result = await _directorService.GetTeacherByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("teachers")]
        public async Task<IActionResult> UpdateTeacher(UpdateTeacherDto dto)
        {
            await _directorService.UpdateTeacherAsync(dto);
            return NoContent();
        }

        [HttpDelete("teachers/{id}")]
        public async Task<IActionResult> DeleteTeacher(Guid id)
        {
            await _directorService.DeleteTeacherAsync(id);
            return NoContent();
        }

        [HttpPost("courses")]
        public async Task<ActionResult<CourseDto>> CreateCourse(CreateCourseDto dto)
        {
            try
            {
                var result = await _directorService.CreateCourseAsync(dto);
                return CreatedAtAction(nameof(GetCourseById), new { id = result.Id }, result);
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("courses/{id}")]
        public async Task<IActionResult> GetCourseById(Guid id)
        {
            var result = await _directorService.GetCourseByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("courses")]
        public async Task<IActionResult> UpdateCourse(UpdateCourseDto dto)
        {
            try
            {
                await _directorService.UpdateCourseAsync(dto);
                return NoContent();
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("courses/{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            await _directorService.DeleteCourseAsync(id);
            return NoContent();
        }
    }
}
