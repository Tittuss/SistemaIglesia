using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateGradeDto
    {
        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
        public decimal NewGrade { get; set; }
    }
}
