using Shared.DTOs;

namespace Client.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto> GetCourseByIdAsync(Guid id);
        Task CreateCourseAsync(CreateCourseDto course);
        Task UpdateCourseAsync(UpdateCourseDto course);
        Task DeleteCourseAsync(Guid id);
    }
}
