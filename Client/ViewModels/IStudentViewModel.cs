using Shared.DTOs;

namespace Client.ViewModels
{
    public interface IStudentViewModel
    {
        IEnumerable<StudentDto> Students { get; }
        CreateStudentDto NewStudent { get; set; }
        UpdateStudentDto EditStudent { get; set; }

        bool IsLoading { get; }
        string Message { get; }

        Task LoadStudentsAsync();
        Task InitializeCreateAsync();
        Task InitializeEditAsync(Guid id);
        Task SaveAsync();
        Task DeleteAsync(Guid id);
    }
}
