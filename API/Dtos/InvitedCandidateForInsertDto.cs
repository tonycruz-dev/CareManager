using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class InvitedCandidateForInsertDto
    {
        public int[] CandidatesId { get; set; }
        public int JobToRequestId { get; set; }

    }
}
