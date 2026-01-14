using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class CourseSimpleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Period { get; set; } = string.Empty;
        public int EnrolledStudentsCount { get; set; }
    }
}
