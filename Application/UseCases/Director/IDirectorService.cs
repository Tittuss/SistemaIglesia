using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Director
{
    public interface IDirectorService
    {
        Task<StudentDto> CreateStudentAsync(CreateStudentDto dto);
        Task UpdateStudentAsync(UpdateStudentDto dto);
        Task DeleteStudentAsync(Guid id);
    }
}
