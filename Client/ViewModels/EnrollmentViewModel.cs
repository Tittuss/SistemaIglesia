using Client.Services;
using Microsoft.AspNetCore.Components;
using Shared.DTOs;

namespace Client.ViewModels
{
    public class EnrollmentViewModel : IEnrollmentViewModel
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        private readonly NavigationManager _navigation;

        public IEnumerable<EnrollmentDto> Enrollments { get; private set; } = new List<EnrollmentDto>();
        public IEnumerable<StudentDto> StudentsList { get; private set; } = new List<StudentDto>();
        public IEnumerable<CourseDto> CoursesList { get; private set; } = new List<CourseDto>();

        public CreateEnrollmentDto NewEnrollment { get; set; } = new();

        public bool IsLoading { get; private set; }
        public string Message { get; private set; } = string.Empty;

        public EnrollmentViewModel(
            IEnrollmentService enrollmentService,
            IStudentService studentService,
            ICourseService courseService,
            NavigationManager navigation)
        {
            _enrollmentService = enrollmentService;
            _studentService = studentService;
            _courseService = courseService;
            _navigation = navigation;
        }

        public async Task LoadEnrollmentsAsync()
        {
            IsLoading = true;
            try { Enrollments = await _enrollmentService.GetAllEnrollmentsAsync(); }
            catch (Exception ex) { Message = ex.Message; }
            finally { IsLoading = false; }
        }

        public async Task InitializeCreateAsync()
        {
            IsLoading = true;
            NewEnrollment = new CreateEnrollmentDto();
            try
            {
                var t1 = _studentService.GetAllStudentsAsync();
                var t2 = _courseService.GetAllCoursesAsync();

                await Task.WhenAll(t1, t2);

                StudentsList = t1.Result;
                CoursesList = t2.Result;
            }
            catch (Exception ex) { Message = "Error cargando listas: " + ex.Message; }
            finally { IsLoading = false; }
        }

        public async Task SaveAsync()
        {
            IsLoading = true;
            try
            {
                await _enrollmentService.CreateEnrollmentAsync(NewEnrollment);
                _navigation.NavigateTo("/enrollments");
            }
            catch (Exception ex) { Message = $"Error: {ex.Message}"; }
            finally { IsLoading = false; }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                await _enrollmentService.DeleteEnrollmentAsync(id);
                await LoadEnrollmentsAsync();
            }
            catch (Exception ex) { Message = ex.Message; }
        }
    }
}
