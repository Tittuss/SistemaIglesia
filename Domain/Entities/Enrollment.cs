using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Enrollment : BaseEntity
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }

        public decimal? FinalGrade { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        public Student? Student { get; set; }
        public Course? Course { get; set; }
    }
}
