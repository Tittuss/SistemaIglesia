using Shared.DTOs;
using System.Net.Http.Json;

namespace Client.Services
{
    public class StudentPortalService : IStudentPortalService
    {
        private readonly HttpClient _http;

        public StudentPortalService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<CourseGradeDto>> GetMyGradesAsync(Guid studentId)
        {
            return await _http.GetFromJsonAsync<IEnumerable<CourseGradeDto>>($"api/student/{studentId}/grades")
                   ?? new List<CourseGradeDto>();
        }
    }
}
