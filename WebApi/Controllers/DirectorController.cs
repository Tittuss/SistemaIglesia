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
        public IActionResult GetStudentById(Guid id)
        {
            return Ok();
        }
    }
}
