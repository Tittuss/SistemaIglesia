using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Director
{
    public interface IDirectorService
    {
        // Estudiante
        Task<StudentDto> CreateStudentAsync(CreateStudentDto dto);
        Task UpdateStudentAsync(UpdateStudentDto dto);
        Task DeleteStudentAsync(Guid id);
        Task<StudentDto> GetStudentByIdAsync(Guid id);

        // Docente
        Task<TeacherDto> CreateTeacherAsync(CreateTeacherDto dto);
        Task UpdateTeacherAsync(UpdateTeacherDto dto);
        Task DeleteTeacherAsync(Guid id);
        Task<TeacherDto> GetTeacherByIdAsync(Guid id);

        // Curso
        Task<CourseDto> CreateCourseAsync(CreateCourseDto dto);
        Task UpdateCourseAsync(UpdateCourseDto dto);
        Task DeleteCourseAsync(Guid id);
        Task<CourseDto> GetCourseByIdAsync(Guid id);
    }
}
