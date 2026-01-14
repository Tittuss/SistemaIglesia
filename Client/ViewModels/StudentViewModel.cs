using Client.Services;
using Microsoft.AspNetCore.Components;
using Shared.DTOs;

namespace Client.ViewModels
{
    public class StudentViewModel : IStudentViewModel
    {
        private readonly IStudentService _studentService;
        private readonly NavigationManager _navigation;

        public IEnumerable<StudentDto> Students { get; private set; } = new List<StudentDto>();
        public CreateStudentDto NewStudent { get; set; } = new ();
        public UpdateStudentDto EditStudent { get; set; } = new();
        
        public bool IsLoading { get; private set; } = true;
        public string Message { get; private set; } = string.Empty;

        private bool _isEditMode = false;

        public StudentViewModel(IStudentService studentService, NavigationManager navigation)
        {
            _studentService = studentService;
            _navigation = navigation;
        }

        public async Task LoadStudentsAsync()
        {
            IsLoading = true;
            Message = "Cargando estudiantes...";

            try
            {
                Students = await _studentService.GetAllStudentsAsync();
                Message = string.Empty;
            }
            catch (Exception ex)
            {
                Message = $"Error al cargar: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        public Task InitializeCreateAsync()
        {
            _isEditMode = false;
            NewStudent = new CreateStudentDto();
            return Task.CompletedTask;
        }

        public async Task InitializeEditAsync(Guid id)
        {
            IsLoading = true;
            _isEditMode = true;
            try 
            {
                var student = await _studentService.GetStudentByIdAsync(id);
                // Mapear al DTO de edición
                EditStudent = new UpdateStudentDto 
                { 
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    IsActive = student.IsActive
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
                {
                    await _studentService.UpdateStudentAsync(EditStudent);
                }
                else
                {
                    await _studentService.CreateStudentAsync(NewStudent);
                }
                _navigation.NavigateTo("/students");
            }
            catch (Exception ex) { Message = $"Error: {ex.Message}"; }
            finally { IsLoading = false; }
        }

        public async Task DeleteAsync(Guid id)
        {
            if(!await ConfirmDelete()) return;

            try 
            {
                await _studentService.DeleteStudentAsync(id);
                await LoadStudentsAsync();
            }
            catch (Exception ex) { Message = ex.Message; }
        }

        private Task<bool> ConfirmDelete() => Task.FromResult(true);
    }
}
