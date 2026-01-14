using Shared.DTOs;
using System.Net.Http.Json;

namespace Client.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly HttpClient _http;
        public EnrollmentService(HttpClient http) => _http = http;

        public async Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync()
        {
            var response = await _http.GetFromJsonAsync<IEnumerable<EnrollmentDto>>("api/director/enrollments");
            return response ?? new List<EnrollmentDto>();
        }

        public async Task CreateEnrollmentAsync(CreateEnrollmentDto enrollment)
        {
            await _http.PostAsJsonAsync("api/director/enrollments", enrollment);
        }

        public async Task DeleteEnrollmentAsync(Guid id)
        {
            await _http.DeleteAsync($"api/director/enrollments/{id}");
        }
    }
}
