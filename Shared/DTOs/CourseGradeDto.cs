using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class CourseGradeDto
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;
        public string Period { get; set; } = string.Empty;
        public decimal? FinalGrade { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
