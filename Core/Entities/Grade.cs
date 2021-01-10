using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Grade : BaseEntity
    {
        public string GradeName { get; set; }
        public decimal HourlyRate { get; set; }

    }
}
