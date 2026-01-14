using Shared.DTOs;
using System.Net.Http.Json;

namespace Client.Services
{
    public class CourseService : ICourseService
    {
        private readonly HttpClient _http;
        public CourseService(HttpClient http) => _http = http;

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var response = await _http.GetFromJsonAsync<IEnumerable<CourseDto>>("api/director/courses");
            return response ?? new List<CourseDto>();
        }

        public async Task<CourseDto> GetCourseByIdAsync(Guid id)
        {
            return await _http.GetFromJsonAsync<CourseDto>($"api/director/courses/{id}");
        }

        public async Task CreateCourseAsync(CreateCourseDto course)
        {
            await _http.PostAsJsonAsync("api/director/courses", course);
        }

        public async Task UpdateCourseAsync(UpdateCourseDto course)
        {
            await _http.PutAsJsonAsync("api/director/courses", course);
        }

        public async Task DeleteCourseAsync(Guid id)
        {
            await _http.DeleteAsync($"api/director/courses/{id}");
        }
    }
}
