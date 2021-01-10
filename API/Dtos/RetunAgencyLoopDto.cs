using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class RetunAgencyLoopDto
    {
        public IReadOnlyList<TimeDetailDto> TimeDetails { get; set; }
        public IReadOnlyList<JobTypeDto> JobTypes { get; set; }
        public IReadOnlyList<GradeDto> Grades { get; set; }

        public IReadOnlyList<ClientLocationDto> ClientLocations { get; set; }
        public IReadOnlyList<AttributeDetailDto> AttributeDetails { get; set; }

        public IReadOnlyList<ShiftStateDto> ShiftStates { get; set; }
        public IReadOnlyList<PaymentTypeDto> PaymentTypes { get; set; }
        public IReadOnlyList<AriaDto> Arias { get; set; }
    }
}
