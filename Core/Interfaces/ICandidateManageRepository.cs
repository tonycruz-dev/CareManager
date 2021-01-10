using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICandidateManageRepository
    {
        Task<Candidate> GetCandidateAsync(int id);
        Task<IReadOnlyList<Candidate>> GetCandidatesAsync();
        Task<IReadOnlyList<Candidate>> GetTopCandidatesAsync(int records);
        Task<List<InvitedCandidate>> GetInvitesAsync(int jobRequestId);
        Task<List<JobConfirmed>> GetJobConformedAsync(int jobRequestId);
        Task<List<JobConfirmed>> GetJobFinishAsync(int id);
        Task<Candidate> GetCandidateByUserIdAsync(string userId);

        Task<InvitedCandidate> AcceptInvitesAsync(int inviteId);
        Task<InvitedCandidate> RejectInvitesAsync(int inviteId);

        Task<Candidate> CreateCandidateAsync(Candidate candidate);
        Task<Candidate> DeleteCandidateAsync(int Id);
        Task<Candidate> UpdateCandidateAsync(Candidate candidate);
    }
}
