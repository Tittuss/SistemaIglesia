using System.Net.Http.Json;
using Shared.DTOs;

namespace Client.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly HttpClient _http;
        public TeacherService(HttpClient http) => _http = http;

        public async Task<IEnumerable<TeacherDto>> GetAllTeachersAsync()
        {
            var response = await _http.GetFromJsonAsync<IEnumerable<TeacherDto>>("api/director/teachers");
            return response ?? new List<TeacherDto>();
        }

        public async Task<TeacherDto> GetTeacherByIdAsync(Guid id)
        {
            return await _http.GetFromJsonAsync<TeacherDto>($"api/director/teachers/{id}");
        }

        public async Task CreateTeacherAsync(CreateTeacherDto teacher)
        {
            await _http.PostAsJsonAsync("api/director/teachers", teacher);
        }

        public async Task UpdateTeacherAsync(UpdateTeacherDto teacher)
        {
            await _http.PutAsJsonAsync("api/director/teachers", teacher);
        }

        public async Task DeleteTeacherAsync(Guid id)
        {
            await _http.DeleteAsync($"api/director/teachers/{id}");
        }
    }
}
