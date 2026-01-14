using Client.Services;
using Microsoft.AspNetCore.Components;
using Shared.DTOs;

namespace Client.ViewModels
{
    public class CourseViewModel : ICourseViewModel
    {
        private readonly ICourseService _courseService;
        private readonly ITeacherService _teacherService;
        private readonly NavigationManager _navigation;
        private bool _isEditMode = false;

        public IEnumerable<CourseDto> Courses { get; private set; } = new List<CourseDto>();
        public IEnumerable<TeacherDto> Teachers { get; private set; } = new List<TeacherDto>();

        public CreateCourseDto NewCourse { get; set; } = new();
        public UpdateCourseDto EditCourse { get; set; } = new();

        public bool IsLoading { get; private set; }
        public string Message { get; private set; } = string.Empty;

        public CourseViewModel(ICourseService courseService, ITeacherService teacherService, NavigationManager navigation)
        {
            _courseService = courseService;
            _teacherService = teacherService;
            _navigation = navigation;
        }

        public async Task LoadCoursesAsync()
        {
            IsLoading = true;
            try
            {
                Courses = await _courseService.GetAllCoursesAsync();
            }
            catch (Exception ex) { Message = ex.Message; }
            finally { IsLoading = false; }
        }

        private async Task LoadTeachersForDropdown()
        {
            if (!Teachers.Any())
            {
                Teachers = await _teacherService.GetAllTeachersAsync();
            }
        }

        public async Task InitializeCreateAsync()
        {
            IsLoading = true;
            _isEditMode = false;
            NewCourse = new CreateCourseDto();
            NewCourse.AcademicPeriodId = Guid.Parse("11111111-1111-1111-1111-111111111111"); 

            await LoadTeachersForDropdown();
            IsLoading = false;
        }

        public async Task InitializeEditAsync(Guid id)
        {
            IsLoading = true;
            _isEditMode = true;
            try
            {
                await LoadTeachersForDropdown();

                var course = await _courseService.GetCourseByIdAsync(id);
                EditCourse = new UpdateCourseDto
                {
                    Id = course.Id,
                    Name = course.Name,
                    Description = course.Description,
                    TeacherId = course.TeacherId
                };
            }
            catch (Exception ex) { Message = ex.Message; }
            finally { IsLoading = false; }
        }

        public async Task SaveAsync()
        {
            IsLoading = true;
            try
            {
                if (_isEditMode)
                    await _courseService.UpdateCourseAsync(EditCourse);
                else
                    await _courseService.CreateCourseAsync(NewCourse);

                _navigation.NavigateTo("/courses");
            }
            catch (Exception ex) { Message = $"Error: {ex.Message}"; }
            finally { IsLoading = false; }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                await _courseService.DeleteCourseAsync(id);
                await LoadCoursesAsync();
            }
            catch (Exception ex) { Message = ex.Message; }
        }
    }
}
