using System.Net.Http.Json;
using Shared.DTOs;

namespace Client.Services
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient _http;


        public StudentService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var response = await _http.GetFromJsonAsync<IEnumerable<StudentDto>>("api/director/students");
            return response ?? new List<StudentDto>();
        }
        public async Task<StudentDto> GetStudentByIdAsync(Guid id)
        {
            return await _http.GetFromJsonAsync<StudentDto>($"api/director/students/{id}");
        }

        public async Task CreateStudentAsync(CreateStudentDto student)
        {
            await _http.PostAsJsonAsync("api/director/students", student);
        }

        public async Task UpdateStudentAsync(UpdateStudentDto student)
        {
            await _http.PutAsJsonAsync("api/director/students", student);
        }

        public async Task DeleteStudentAsync(Guid id)
        {
            await _http.DeleteAsync($"api/director/students/{id}");
        }
    }
}
