using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class JobRequestByAgencyDto
    {
        public IReadOnlyList<JobToRequestToReturnDto> Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public int Booked { get; set; }
        public int Canceled { get; set; }
        public int Finish { get; set; }
        public int InProgress { get; set; }
        public int Pending { get; set; }
    }
}
