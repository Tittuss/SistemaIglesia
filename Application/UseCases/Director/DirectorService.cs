using Shared.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Director
{
    public class DirectorService : IDirectorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DirectorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Estudiante
        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _unitOfWork.Students.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }
        public async Task<StudentDto> CreateStudentAsync(CreateStudentDto dto)
        {
            var existingStudent = await _unitOfWork.Students.GetByEmailAsync(dto.Email);
            if (existingStudent != null)
            {
                throw new ArgumentException($"El correo {dto.Email} ya está registrado en el sistema.");
            }

            var studentEntity = _mapper.Map<Student>(dto);

            studentEntity.Id = Guid.NewGuid();
            studentEntity.CreatedAt = DateTime.UtcNow;
            studentEntity.IsActive = true;

            await _unitOfWork.Students.AddAsync(studentEntity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<StudentDto>(studentEntity);
        }

        public async Task UpdateStudentAsync(UpdateStudentDto dto)
        {
            var studentEntity = await _unitOfWork.Students.GetByIdAsync(dto.Id);
            if (studentEntity == null)
            {
                throw new KeyNotFoundException($"No se encontró al estudiante con ID {dto.Id}");
            }

            studentEntity.FirstName = dto.FirstName;
            studentEntity.LastName = dto.LastName;
            studentEntity.IsActive = dto.IsActive;

            _unitOfWork.Students.Update(studentEntity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteStudentAsync(Guid id)
        {
            var studentEntity = await _unitOfWork.Students.GetByIdAsync(id);
            if (studentEntity == null)
            {
                throw new KeyNotFoundException($"No se encontró al estudiante con ID {id}");
            }

            _unitOfWork.Students.Delete(studentEntity);

            await _unitOfWork.SaveAsync();
        }

        public async Task<StudentDto> GetStudentByIdAsync(Guid id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            if (student == null) throw new KeyNotFoundException("Estudiante no encontrado");
            return _mapper.Map<StudentDto>(student);
        }

        #endregion

        #region Docente
        public async Task<IEnumerable<TeacherDto>> GetAllTeachersAsync()
        {
            var teachers = await _unitOfWork.Teachers.GetAllAsync();
            return _mapper.Map<IEnumerable<TeacherDto>>(teachers);
        }
        public async Task<TeacherDto> CreateTeacherAsync(CreateTeacherDto dto)
        {
            var existingTeacher = await _unitOfWork.Students.GetByEmailAsync(dto.Email);
            if (existingTeacher != null)
            {
                throw new ArgumentException($"El correo {dto.Email} ya está registrado en el sistema.");
            }

            var teacher = _mapper.Map<Teacher>(dto);
            teacher.Id = Guid.NewGuid();

            await _unitOfWork.Teachers.AddAsync(teacher);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task UpdateTeacherAsync(UpdateTeacherDto dto)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(dto.Id);
            if (teacher == null) throw new KeyNotFoundException("Docente no encontrado");

            teacher.FirstName = dto.FirstName;
            teacher.LastName = dto.LastName;

            _unitOfWork.Teachers.Update(teacher);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteTeacherAsync(Guid id)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
            if (teacher == null) throw new KeyNotFoundException("Docente no encontrado");

            _unitOfWork.Teachers.Delete(teacher);
            await _unitOfWork.SaveAsync();
        }

        public async Task<TeacherDto> GetTeacherByIdAsync(Guid id)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
            if (teacher == null) throw new KeyNotFoundException("Docente no encontrado");
            return _mapper.Map<TeacherDto>(teacher);
        }
        #endregion

        #region Curso
        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _unitOfWork.Courses.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public async Task<CourseDto> CreateCourseAsync(CreateCourseDto dto)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(dto.TeacherId);
            if (teacher == null) throw new ArgumentException("El profesor asignado no existe");

            var course = _mapper.Map<Course>(dto);
            course.Id = Guid.NewGuid();

            course.AcademicPeriodId = dto.AcademicPeriodId;

            await _unitOfWork.Courses.AddAsync(course);
            await _unitOfWork.SaveAsync();

            course.Teacher = teacher;
            return _mapper.Map<CourseDto>(course);
        }

        public async Task UpdateCourseAsync(UpdateCourseDto dto)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(dto.Id);
            if (course == null) throw new KeyNotFoundException("Curso no encontrado");

            // Validar que el nuevo profesor exista
            if (course.TeacherId != dto.TeacherId)
            {
                var teacher = await _unitOfWork.Teachers.GetByIdAsync(dto.TeacherId);
                if (teacher == null) throw new ArgumentException("El nuevo profesor no existe");
                course.TeacherId = dto.TeacherId;
            }

            course.Name = dto.Name;
            course.Description = dto.Description;

            _unitOfWork.Courses.Update(course);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCourseAsync(Guid id)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);
            if (course == null) throw new KeyNotFoundException("Curso no encontrado");

            _unitOfWork.Courses.Delete(course);
            await _unitOfWork.SaveAsync();
        }

        public async Task<CourseDto> GetCourseByIdAsync(Guid id)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);
            if (course == null) throw new KeyNotFoundException("Curso no encontrado");

            if (course.Teacher == null)
                course.Teacher = await _unitOfWork.Teachers.GetByIdAsync(course.TeacherId);

            return _mapper.Map<CourseDto>(course);
        }
        #endregion

        #region Inscripciones
        public async Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentAsync()
        {
            var enrollments = await _unitOfWork.Enrollments.GetAllAsync();
            return _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
        }
        public async Task<EnrollmentDto> CreateEnrollmentAsync(CreateEnrollmentDto dto)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(dto.StudentId);
            if (student == null) throw new ArgumentException("El estudiante no existe.");

            var course = await _unitOfWork.Courses.GetByIdAsync(dto.CourseId);
            if (course == null) throw new ArgumentException("El curso no existe.");

            var existingEnrollments = await _unitOfWork.Enrollments.GetByCourseIdAsync(dto.CourseId);
            if (existingEnrollments.Any(e => e.StudentId == dto.StudentId))
            {
                throw new ArgumentException($"El estudiante ya está inscrito en el curso '{course.Name}'.");
            }

            var enrollment = new Enrollment
            {
                Id = Guid.NewGuid(),
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                FinalGrade = 0
            };

            await _unitOfWork.Enrollments.AddAsync(enrollment);
            await _unitOfWork.SaveAsync();

            enrollment.Student = student;
            enrollment.Course = course;

            return _mapper.Map<EnrollmentDto>(enrollment);
        }

        public async Task DeleteEnrollmentAsync(Guid id)
        {
            var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(id);
            if (enrollment == null) throw new KeyNotFoundException("Inscripción no encontrada.");

            _unitOfWork.Enrollments.Delete(enrollment);
            await _unitOfWork.SaveAsync();
        }

        public async Task<EnrollmentDto> GetEnrollmentByIdAsync(Guid id)
        {
            var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(id);
            if (enrollment == null) throw new KeyNotFoundException("Inscripción no encontrada.");

            if (enrollment.Student == null)
                enrollment.Student = await _unitOfWork.Students.GetByIdAsync(enrollment.StudentId);
            if (enrollment.Course == null)
                enrollment.Course = await _unitOfWork.Courses.GetByIdAsync(enrollment.CourseId);

            return _mapper.Map<EnrollmentDto>(enrollment);
        }
        #endregion
    }
}
