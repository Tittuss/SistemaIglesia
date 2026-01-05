using AutoMapper;
using Application.DTOs;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Teachers
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TeacherService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseSimpleDto>> GetAssignedCoursesAsync(Guid teacherId)
        {
            var courses = await _unitOfWork.Courses.GetCoursesByTeacherIdAsync(teacherId);
            return _mapper.Map<IEnumerable<CourseSimpleDto>>(courses);
        }

        public async Task<IEnumerable<StudentGradeDto>> GetStudentsByCourseAsync(Guid courseId)
        {
            var enrollments = await _unitOfWork.Enrollments.GetByCourseIdAsync(courseId);
            return _mapper.Map<IEnumerable<StudentGradeDto>>(enrollments);
        }

        public async Task UpdateStudentGradeAsync(UpdateGradeDto dto)
        {
            var enrollments = await _unitOfWork.Enrollments.GetByCourseIdAsync(dto.CourseId);
            var targetEnrollment = enrollments.FirstOrDefault(e => e.StudentId == dto.StudentId);

            if (targetEnrollment == null)
                throw new Exception("El estudiante no está inscrito en este curso.");

            targetEnrollment.FinalGrade = dto.NewGrade;

            _unitOfWork.Enrollments.Update(targetEnrollment);
            await _unitOfWork.SaveAsync();
        }
    }
}
