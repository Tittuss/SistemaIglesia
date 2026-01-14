using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Students
{
    public interface IStudentService
    {
        Task<IEnumerable<CourseGradeDto>> GetMyGradesAsync(Guid studentId);
    }
}
