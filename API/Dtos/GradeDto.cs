using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class GradeDto
    {
        public int Id { get; set; }
        public string GradeName { get; set; }
        public decimal HourlyRate { get; set; }
        
    }
}
