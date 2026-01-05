using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Credits { get; set; }

        // Foreign Keys and Navigation Properties
        public Guid TeacherId { get; set; }
        public Teacher? Teacher { get; set; } = null!;

        public Guid AcademicPeriodId { get; set; }
        public AcademicPeriod? AcademicPeriod { get; set; } = null!;

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
