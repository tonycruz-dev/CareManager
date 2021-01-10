using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class HRCandidateUpdatePhotoDto
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
    }
    public class HRAgencyUpdatePhotoDto
    {
        public int Id { get; set; }
        public int AgencyId { get; set; }
    }
}
