using Shared.DTOs;

namespace Client.Services
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync();
        Task CreateEnrollmentAsync(CreateEnrollmentDto enrollment);
        Task DeleteEnrollmentAsync(Guid id);
    }
}
