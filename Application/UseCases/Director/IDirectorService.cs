using Shared.DTOs;
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
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
        Task<StudentDto> CreateStudentAsync(CreateStudentDto dto);
        Task UpdateStudentAsync(UpdateStudentDto dto);
        Task DeleteStudentAsync(Guid id);
        Task<StudentDto> GetStudentByIdAsync(Guid id);

        // Docente
        Task<IEnumerable<TeacherDto>> GetAllTeachersAsync();
        Task<TeacherDto> CreateTeacherAsync(CreateTeacherDto dto);
        Task UpdateTeacherAsync(UpdateTeacherDto dto);
        Task DeleteTeacherAsync(Guid id);
        Task<TeacherDto> GetTeacherByIdAsync(Guid id);

        // Curso
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto> CreateCourseAsync(CreateCourseDto dto);
        Task UpdateCourseAsync(UpdateCourseDto dto);
        Task DeleteCourseAsync(Guid id);
        Task<CourseDto> GetCourseByIdAsync(Guid id);

        // Inscripciones
        Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentAsync();
        Task<EnrollmentDto> CreateEnrollmentAsync(CreateEnrollmentDto dto);
        Task DeleteEnrollmentAsync(Guid id);
        Task<EnrollmentDto> GetEnrollmentByIdAsync(Guid id);
    }
}
