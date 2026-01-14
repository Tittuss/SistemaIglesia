using Shared.DTOs;
using AutoMapper;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Students
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseGradeDto>> GetMyGradesAsync(Guid studentId)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(studentId);
            
            if (student == null)
            {
                throw new KeyNotFoundException($"El estudiante con ID {studentId} no existe.");
            }
            
            if (!student.IsActive)
            {
                throw new UnauthorizedAccessException("El usuario está inactivo. Contacte a administración.");
            }
            
            var enrollments = await _unitOfWork.Enrollments.GetByStudentIdAsync(studentId);
            return _mapper.Map<IEnumerable<CourseGradeDto>>(enrollments);
        }
    }
}
