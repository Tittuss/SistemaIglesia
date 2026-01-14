using Shared.DTOs;

namespace Client.ViewModels
{
    public interface ICourseViewModel
    {
        IEnumerable<CourseDto> Courses { get; }
        IEnumerable<TeacherDto> Teachers { get; }

        CreateCourseDto NewCourse { get; set; }
        UpdateCourseDto EditCourse { get; set; }

        bool IsLoading { get; }
        string Message { get; }

        Task LoadCoursesAsync();
        Task InitializeCreateAsync();
        Task InitializeEditAsync(Guid id);
        Task SaveAsync();
        Task DeleteAsync(Guid id);
    }
}
