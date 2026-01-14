using Shared.DTOs;

namespace Client.ViewModels
{
    public interface ITeacherViewModel
    {
        IEnumerable<TeacherDto> Teachers { get; }
        CreateTeacherDto NewTeacher { get; set; }
        UpdateTeacherDto EditTeacher { get; set; }
        bool IsLoading { get; }
        string Message { get; }

        Task LoadTeachersAsync();
        Task InitializeCreateAsync();
        Task InitializeEditAsync(Guid id);
        Task SaveAsync();
        Task DeleteAsync(Guid id);
    }
}
