using Application.DTOs;
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
            var enrollments = await _unitOfWork.Enrollments.GetByStudentIdAsync(studentId);
            return _mapper.Map<IEnumerable<CourseGradeDto>>(enrollments);
        }
    }
}
