using Shared.DTOs;

namespace Client.Services
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetAllTeachersAsync();
        Task<TeacherDto> GetTeacherByIdAsync(Guid id);
        Task CreateTeacherAsync(CreateTeacherDto teacher);
        Task UpdateTeacherAsync(UpdateTeacherDto teacher);
        Task DeleteTeacherAsync(Guid id);
    }
}
