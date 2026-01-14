using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEnrollmentRepository : IGenericRepository<Enrollment>
    {
        // Estudiantes: Ver cursos Inscritos y notas
        Task<IEnumerable<Enrollment>> GetByStudentIdAsync(Guid studentId);
        
        // Docente: Listado de alumnos por cursos
        Task<IEnumerable<Enrollment>> GetByCourseIdAsync(Guid courseId);
        
        // Validar si ya existe una inscripción (Evitar duplicados)
        Task<bool> ExistsAsync(Guid studentId, Guid courseId);
        Task<IEnumerable<Enrollment>> GetAllWithDetailsAsync();
    }
}
