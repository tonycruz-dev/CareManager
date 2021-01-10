using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class HRDashboardDto
    {
        public int TotalJobRequest { get; set; }
        public int TotalCandidates { get; set; }
        public int TotalJobConfirmed { get; set; }
        public int TotalInvitesCandidates { get; set; }
        public List<AgencyDto> Agencies { get; set; }
        public List<CandidateDto> Candidates { get; set; }
    }
}
