using AutoMapper;
using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeo: Inscripción->Vista Estudiante(Curso + Nota)
            CreateMap<Enrollment, CourseGradeDto>()
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course!.Name))
                .ForMember(dest => dest.Period, opt => opt.MapFrom(src => src.Course!.AcademicPeriod!.Name))
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src =>
                    $"{src.Course!.Teacher!.FirstName} {src.Course!.Teacher!.LastName}"));

            // Mapeo: Curso -> Vista Docente (Lista de Cursos)
            CreateMap<Course, CourseSimpleDto>()
                .ForMember(dest => dest.Period, opt => opt.MapFrom(src => src.AcademicPeriod!.Name))
                .ForMember(dest => dest.EnrolledStudentsCount, opt => opt.MapFrom(src => src.Enrollments.Count));

            // Mapeo: Inscripción -> Vista Docente (Alumno + Nota)
            CreateMap<Enrollment, StudentGradeDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src =>
                    $"{src.Student!.FirstName} {src.Student!.LastName}"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Student!.Email))
                .ForMember(dest => dest.CurrentGrade, opt => opt.MapFrom(src => src.FinalGrade));
            
            // Estudiante
            CreateMap<CreateStudentDto, Student>();
            CreateMap<Student, StudentDto>();
            CreateMap<UpdateStudentDto, Student>();
        }
    }
}
