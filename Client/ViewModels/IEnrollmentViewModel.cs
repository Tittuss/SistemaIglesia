using Shared.DTOs;

namespace Client.ViewModels
{
    public interface IEnrollmentViewModel
    {
        IEnumerable<EnrollmentDto> Enrollments { get; }

        IEnumerable<StudentDto> StudentsList { get; }
        IEnumerable<CourseDto> CoursesList { get; }

        CreateEnrollmentDto NewEnrollment { get; set; }

        bool IsLoading { get; }
        string Message { get; }

        Task LoadEnrollmentsAsync();
        Task InitializeCreateAsync();
        Task SaveAsync();
        Task DeleteAsync(Guid id);
    }
}
