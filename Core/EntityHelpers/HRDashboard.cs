using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.EntityHelpers
{
    public class HRDashboard
    {
        public int TotalJobRequest { get; set; }
        public int TotalCandidates { get; set; }
        public int TotalJobConfirmed { get; set; }
        public int TotalInvitesCandidates { get; set; }
        public IReadOnlyList<Agency> Agencies { get; set; }
        public IReadOnlyList<Candidate> Candidates { get; set; }

    }
}
