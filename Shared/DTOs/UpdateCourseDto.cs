using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class UpdateCourseDto
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid TeacherId { get; set; } // Permitimos cambiar al profesor
    }
}
