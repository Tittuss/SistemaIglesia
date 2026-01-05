using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Teachers
{
    public interface ITeacherService
    {
        Task<IEnumerable<CourseSimpleDto>> GetAssignedCoursesAsync(Guid teacherId);
        Task<IEnumerable<StudentGradeDto>> GetStudentsByCourseAsync(Guid courseId);
        Task UpdateStudentGradeAsync(UpdateGradeDto dto);
    }
}
