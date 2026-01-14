using Shared.DTOs;
namespace Client.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
        Task<StudentDto> GetStudentByIdAsync(Guid id);
        Task CreateStudentAsync(CreateStudentDto student);
        Task UpdateStudentAsync(UpdateStudentDto student);
        Task DeleteStudentAsync(Guid id);
    }
}
