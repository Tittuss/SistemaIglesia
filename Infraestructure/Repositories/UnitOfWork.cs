using Domain.Interfaces;
using Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IEnrollmentRepository? _enrollmentRepository;
        private ICourseRepository? _courseRepository;
        private IStudentRepository? _studentRepository;
        private ITeacherRepository? _teacherRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICourseRepository Courses => _courseRepository ??= new CourseRepository(_context);

        public IEnrollmentRepository Enrollments => _enrollmentRepository ??= new EnrollmentRepository(_context);

        public IStudentRepository Students => _studentRepository ??= new StudentRepository(_context);

        public ITeacherRepository Teachers => _teacherRepository ??= new TeacherRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
