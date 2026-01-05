using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICourseRepository Courses { get; }
        IEnrollmentRepository Enrollments { get; }
        IStudentRepository Students { get; }
        ITeacherRepository Teachers { get; }

        Task<int> SaveAsync();
    }
}
