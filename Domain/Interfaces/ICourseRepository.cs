using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        // Obtener cursos asignados a un docente
        Task<IEnumerable<Course>> GetCoursesByTeacherIdAsync(Guid teacherId);

        // Obtener detalle del curso
        Task<Course?> GetCourseWithDetailsAsync(Guid courseId);
        Task<IEnumerable<Course>> GetAllWithDetailsAsync();
    }
}
