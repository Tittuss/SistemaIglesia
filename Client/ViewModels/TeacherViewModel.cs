using Client.Services;
using Microsoft.AspNetCore.Components;
using Shared.DTOs;

namespace Client.ViewModels
{
    public class TeacherViewModel : ITeacherViewModel
    {
        private readonly ITeacherService _teacherService;
        private readonly NavigationManager _navigation;
        private bool _isEditMode = false;

        public IEnumerable<TeacherDto> Teachers { get; private set; } = new List<TeacherDto>();
        public CreateTeacherDto NewTeacher { get; set; } = new();
        public UpdateTeacherDto EditTeacher { get; set; } = new();
        public bool IsLoading { get; private set; }
        public string Message { get; private set; } = string.Empty;

        public TeacherViewModel(ITeacherService teacherService, NavigationManager navigation)
        {
            _teacherService = teacherService;
            _navigation = navigation;
        }

        public async Task LoadTeachersAsync()
        {
            IsLoading = true;
            try { Teachers = await _teacherService.GetAllTeachersAsync(); }
            catch (Exception ex) { Message = ex.Message; }
            finally { IsLoading = false; }
        }

        public Task InitializeCreateAsync()
        {
            _isEditMode = false;
            NewTeacher = new CreateTeacherDto();
            Message = string.Empty;
            return Task.CompletedTask;
        }

        public async Task InitializeEditAsync(Guid id)
        {
            IsLoading = true;
            _isEditMode = true;
            Message = string.Empty;
            try
            {
                var teacher = await _teacherService.GetTeacherByIdAsync(id);
                EditTeacher = new UpdateTeacherDto
                {
                    Id = teacher.Id,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName
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
                    await _teacherService.UpdateTeacherAsync(EditTeacher);
                else
                    await _teacherService.CreateTeacherAsync(NewTeacher);

                _navigation.NavigateTo("/teachers");
            }
            catch (Exception ex) { Message = $"Error: {ex.Message}"; }
            finally { IsLoading = false; }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                await _teacherService.DeleteTeacherAsync(id);
                await LoadTeachersAsync();
            }
            catch (Exception ex) { Message = ex.Message; }
        }
    }
}
