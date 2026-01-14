using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class UpdateGradeDto
    {
        [Required]
        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
        public decimal NewGrade { get; set; }
    }
}
