using Shared.DTOs;
using System.Net.Http.Json;

namespace Client.Services
{
    public interface ITeacherPortalService
    {
        Task<IEnumerable<CourseSimpleDto>> GetMyCoursesAsync(Guid teacherId);
        Task<IEnumerable<StudentGradeDto>> GetStudentsByCourseAsync(Guid courseId);
        Task UpdateGradeAsync(UpdateGradeDto gradeDto);
    }

    public class TeacherPortalService : ITeacherPortalService
    {
        private readonly HttpClient _http;
        public TeacherPortalService(HttpClient http) => _http = http;

        public async Task<IEnumerable<CourseSimpleDto>> GetMyCoursesAsync(Guid teacherId)
        {
            return await _http.GetFromJsonAsync<IEnumerable<CourseSimpleDto>>($"api/teacher/{teacherId}/courses")
                   ?? new List<CourseSimpleDto>();
        }

        public async Task<IEnumerable<StudentGradeDto>> GetStudentsByCourseAsync(Guid courseId)
        {
            return await _http.GetFromJsonAsync<IEnumerable<StudentGradeDto>>($"api/teacher/course/{courseId}/students")
                   ?? new List<StudentGradeDto>();
        }

        public async Task UpdateGradeAsync(UpdateGradeDto gradeDto)
        {
            var response = await _http.PutAsJsonAsync("api/teacher/grade", gradeDto);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error al actualizar la nota.");
            }
        }
    }
}