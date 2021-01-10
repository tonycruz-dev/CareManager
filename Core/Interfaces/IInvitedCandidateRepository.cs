using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
   public interface IInvitedCandidateRepository
   {
        Task<IReadOnlyList<InvitedCandidate>> GetInvitedCandidateByAgency(int agencyId);
        Task<IReadOnlyList<InvitedCandidate>> GetInvitedCandidateByUser(string userId);
        Task<IReadOnlyList<InvitedCandidate>> GetInvitedCandidateByJobId(int jobId);
        Task<InvitedCandidate> CreateInvitedCandidateAsync(InvitedCandidate invitedCandidates);
        Task<InvitedCandidate> DeleteInvitedCandidateAsync(int Id);
        Task<InvitedCandidate> UpdateInvitedCandidateAsync(InvitedCandidate invitedCandidates);

        Task<IReadOnlyList<InvitedCandidate>> InsertInvitedCandidateAsync(int[] candidateId, int jobToRequestId);
        Task<IReadOnlyList<InvitedCandidate>> GetCandidatesInprogress(int jobToRequestId);

    }
}
