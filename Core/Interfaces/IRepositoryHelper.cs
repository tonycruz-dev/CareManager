using Core.Entities;
using Core.EntityHelpers;
using Core.Loops;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepositoryHelper
    {
        Task<Agency> GetAgencyByUserIdAsync(string userId);
        Task<TimeDetail> GetTimeDetailAsync(int Id);
        Task<IReadOnlyList<TimeDetail>> GetTimeDetailsAsync();
        Task<IReadOnlyList<Candidate>> GetCandidateForInviteAsync(int gradeId);
        Task<IReadOnlyList<Candidate>> GetCandidateInvitedAsync(int jobRequestId);
        Task<IReadOnlyList<Candidate>> GetCandidateInProgressAsync(int jobRequestId);
        Task<IReadOnlyList<CandidateResponded>> GetCandidateRespondedAsync(int jobRequestId);

        Task<IReadOnlyList<CandidateBooked>> GetCandidateBookedAsync(int jobRequestId);
        Task<AppUser> UpdateUserPhotoAsync(AppUser appUser);
        Task<AgencyJobStatus> GetAgencyJobStatesAsync(int Id);
        Task<AgencyListLoop> GetAgencyLoop(int agencyId);
        Task<HRDashboard> GetHRDashboard();

    }
}
