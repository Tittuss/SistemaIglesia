using Shared.DTOs;

namespace Client.Services
{
    public interface IStudentPortalService
    {
        Task<IEnumerable<CourseGradeDto>> GetMyGradesAsync(Guid studentId);
    }
}
