using Core.Entities;
using Core.EntityHelpers;
using Core.Loops;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
   public interface IAgencyServices
    {

        Task<Agency> AddAgencyAsync(Agency agency);
        Task<Agency> UpdateAgencyAsync(Agency agency);
        Task<Agency> DeleteAgencyAsync(Agency agency);
        Task<IReadOnlyList<Agency>> GetAgencies();

        Task<IReadOnlyList<ClientLocation>> GetClientLocationAsync(string userId);
        Task<ClientLocation> CreateClientLocationAsync(ClientLocation clientLocation);
        Task<ClientLocation> UpdateClientLocationAsync(ClientLocation clientLocation);
        Task<ClientLocation> DeleteClientLocationAsync(int Id);
        Task<IReadOnlyList<ClientLocation>> GetClientLocationByAgency(int agencyId);


        Task<RetunAgencyLoop> GetRetunAgencyLoop(int agencyId);
        Task<InvitedCandidate> AddInviteAsync(InvitedCandidate invitedCandidate);
        Task<IReadOnlyList<JobToRequest>> GetJobToRequests(JobToRequesSpecParams jobToRequestPaginationSpec);


        Task<IReadOnlyList<JobToRequest>> GetJobToRequestByAgency(int agencyId);
        Task<IReadOnlyList<JobToRequest>> GetJobToRequestByUser(string userId);
        Task<JobToRequest> CreateJobToRequestAsync(JobToRequest jobToRequest);
        Task<JobToRequest> DeleteJobToRequestAsync(int Id);
        Task<JobToRequest> UpdateJobToRequestAsync(JobToRequest jobToRequest);



        Task<IReadOnlyList<InvitedCandidate>> GetInvitedCandidateByAgency(int agencyId);
        Task<IReadOnlyList<InvitedCandidate>> GetInvitedCandidateByUser(string userId);
        Task<InvitedCandidate> CreateInvitedCandidateAsync(InvitedCandidate invitedCandidates);
        Task<InvitedCandidate> DeleteInvitedCandidateAsync(int Id);
        Task<InvitedCandidate> UpdateInvitedCandidateAsync(InvitedCandidate invitedCandidates);
        Task<IReadOnlyList<InvitedCandidate>> GetInvitedCandidateByJobId(int jobId);

        Task<Agency> GetAgencyByUserIdAsync(string userId);

        Task<IReadOnlyList<JobConfirmed>> GetJobConfirmedByAgency(int agencyId);
        Task<IReadOnlyList<JobConfirmed>> GetJobConfirmedByUser(string userId);
        Task<IReadOnlyList<JobConfirmed>> GetJobConfirmedByJobId(int jobId);
        Task<JobConfirmed> CreateJobConfirmedAsync(JobConfirmed jobConfirmed);
        Task<JobConfirmed> DeleteJobConfirmedAsync(int Id);
        Task<JobConfirmed> UpdateJobConfirmedAsync(JobConfirmed jobConfirmed);

        Task<TimeDetail> GetTimeDetailAsync(int Id);
        Task<IReadOnlyList<TimeDetail>> GetTimeDetailsAsync();

        Task<IReadOnlyList<Candidate>> GetCandidateForInviteAsync(int gradeId);
        Task<JobToRequest> GetJobToRequestByIdAsync(int Id);
        Task<IReadOnlyList<InvitedCandidate>> InsertInvitedCandidateAsync(int[] candidateId, int jobToRequestId);



        Task<IReadOnlyList<Candidate>> GetCandidateInvitedAsync(int jobRequestId);
        Task<IReadOnlyList<Candidate>> GetCandidateInProgressAsync(int jobRequestId);
        Task<IReadOnlyList<CandidateResponded>> GetCandidateRespondedAsync(int jobRequestId);
        Task<IReadOnlyList<CandidateBooked>> GetCandidateBookedAsync(int jobRequestId);
        Task<JobConfirmed> FinalizedJobConfirmedAsync(ConfirmeFinal confirmeFinal);
    }
}
