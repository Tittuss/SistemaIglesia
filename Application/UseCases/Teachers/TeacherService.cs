using AutoMapper;
using Shared.DTOs;
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
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(teacherId);
            if (teacher == null)
            {
                throw new KeyNotFoundException($"El docente con ID {teacherId} no existe.");
            }
            var courses = await _unitOfWork.Courses.GetCoursesByTeacherIdAsync(teacherId);
            return _mapper.Map<IEnumerable<CourseSimpleDto>>(courses);
        }

        public async Task<IEnumerable<StudentGradeDto>> GetStudentsByCourseAsync(Guid courseId)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(courseId);
            if (course == null) throw new KeyNotFoundException("El curso solicitado no existe."); var enrollments = await _unitOfWork.Enrollments.GetByCourseIdAsync(courseId);
            return _mapper.Map<IEnumerable<StudentGradeDto>>(enrollments);
        }

        public async Task UpdateStudentGradeAsync(UpdateGradeDto dto)
        {
            if (dto.NewGrade < 0 || dto.NewGrade > 100)
            {
                throw new ArgumentException("La nota debe estar entre 0 y 100.");
            }

            var course = await _unitOfWork.Courses.GetByIdAsync(dto.CourseId);
            if (course == null) throw new KeyNotFoundException("Curso no encontrado.");

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
