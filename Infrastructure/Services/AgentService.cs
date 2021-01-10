using Core.Entities;
using Core.EntityHelpers;
using Core.Interfaces;
using Core.Loops;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
   public class AgentService: IAgencyServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAgencyRepository  _agencyRepository;
        private readonly IClientLocation _clientLocation;
        private readonly IJobRequestRepository _jobRequestRepo;
        private readonly IInvitedCandidateRepository _invitedCandidateRepo;
        private readonly IJobConfirmedRepository _confirmedRepo;
        private readonly IRepositoryHelper _repositoryHelper;

        public AgentService(IUnitOfWork unitOfWork, 
            IClientLocation clientLocation, 
            IJobRequestRepository jobRequestRepo, 
            IInvitedCandidateRepository invitedCandidateRepo,
            IJobConfirmedRepository ConfirmedRepo,
            IRepositoryHelper repositoryHelper,
            IAgencyRepository agencyRepository)
        {
            _unitOfWork = unitOfWork;
            _clientLocation = clientLocation;
            _jobRequestRepo = jobRequestRepo;
            _invitedCandidateRepo = invitedCandidateRepo;
            _confirmedRepo = ConfirmedRepo;
            _repositoryHelper = repositoryHelper;
            _agencyRepository = agencyRepository;
        }

        public Task<InvitedCandidate> AddInviteAsync(InvitedCandidate invitedCandidate)
        {
            throw new NotImplementedException();
        }

        public async Task<ClientLocation> CreateClientLocationAsync(ClientLocation clientLocation)
        {
            return await _clientLocation.CreateClientLocationAsync(clientLocation);
        }
        public async Task<IReadOnlyList<ClientLocation>> GetClientLocationByAgency(int agencyId)
        {
            return await _clientLocation.GetClientLocationByAgency(agencyId);
        }
        public async Task<ClientLocation> DeleteClientLocationAsync(int Id)
        {
            return await _clientLocation.DeleteClientLocationAsync(Id);
        }
        public async Task<IReadOnlyList<ClientLocation>> GetClientLocationAsync(string userId)
        {
            return await _clientLocation.GetClientLocationByUser(userId);
        }
        public async Task<ClientLocation> UpdateClientLocationAsync(ClientLocation clientLocation)
        {
            return await _clientLocation.UpdateClientLocationAsync(clientLocation);
        }
        // Job To Request
        public async Task<JobToRequest> CreateJobToRequestAsync(JobToRequest jobToRequest)
        {
            return await _jobRequestRepo.CreateJobToRequestAsync(jobToRequest);
        }

        public async Task<JobToRequest> DeleteJobToRequestAsync(int Id)
        {
            return await _jobRequestRepo.DeleteJobToRequestAsync(Id);
        }

        public async Task<IReadOnlyList<JobToRequest>> GetJobToRequestByAgency(int agencyId)
        {
            return await _jobRequestRepo.GetJobToRequestByAgency(agencyId);
        }

        public async Task<IReadOnlyList<JobToRequest>> GetJobToRequestByUser(string userId)
        {
            return await _jobRequestRepo.GetJobToRequestByUser(userId);
        }

        public async Task<IReadOnlyList<JobToRequest>> GetJobToRequests(JobToRequesSpecParams specParam)
        {
            var spec = new JobToRequestPaginationSpec(specParam);
            return await _unitOfWork.Repository<JobToRequest>().ListAsync(spec);
        }
        public async Task<JobToRequest> UpdateJobToRequestAsync(JobToRequest jobToRequest)
        {
            return await _jobRequestRepo.UpdateJobToRequestAsync(jobToRequest);
        }
        //
        public async Task<RetunAgencyLoop> GetRetunAgencyLoop(int agencyId)
        {
            var loopResults = new RetunAgencyLoop
            {
                ClientLocations = await _clientLocation.GetClientLocationByAgency(agencyId),
                Arias = await _unitOfWork.Repository<Aria>().ListAllAsync(),
                PaymentTypes = await _unitOfWork.Repository<PaymentType>().ListAllAsync(),
                JobTypes = await _unitOfWork.Repository<JobType>().ListAllAsync(),
                Grades = await _unitOfWork.Repository<Grade>().ListAllAsync(),
                AttributeDetails = await _unitOfWork.Repository<AttributeDetail>().ListAllAsync(),
                TimeDetails = await _unitOfWork.Repository<TimeDetail>().ListAllAsync()
            };

            return loopResults;
        }

        public async Task<IReadOnlyList<InvitedCandidate>> GetInvitedCandidateByUser(string userId)
        {
            return await _invitedCandidateRepo.GetInvitedCandidateByUser(userId);
        }

        public async Task<InvitedCandidate> CreateInvitedCandidateAsync(InvitedCandidate invitedCandidates)
        {
            return await _invitedCandidateRepo.CreateInvitedCandidateAsync(invitedCandidates);
        }

        public async Task<InvitedCandidate> DeleteInvitedCandidateAsync(int Id)
        {
            return await _invitedCandidateRepo.DeleteInvitedCandidateAsync(Id);
        }
        public async Task<IReadOnlyList<InvitedCandidate>> GetInvitedCandidateByAgency(int agencyId)
        {
            return await _invitedCandidateRepo.GetInvitedCandidateByAgency(agencyId);
        }

        public async Task<InvitedCandidate> UpdateInvitedCandidateAsync(InvitedCandidate invitedCandidates)
        {
            return await _invitedCandidateRepo.UpdateInvitedCandidateAsync(invitedCandidates);
        }

        public async Task<Agency> GetAgencyByUserIdAsync(string userId)
        {
            return await _repositoryHelper.GetAgencyByUserIdAsync(userId);
        }

        public async Task<IReadOnlyList<InvitedCandidate>> GetInvitedCandidateByJobId(int jobId)
        {
            return await _invitedCandidateRepo.GetInvitedCandidateByJobId(jobId);
        }

        public async Task<IReadOnlyList<JobConfirmed>> GetJobConfirmedByAgency(int agencyId)
        {
            return await _confirmedRepo.GetJobConfirmedByAgency(agencyId);
        }

        public async Task<IReadOnlyList<JobConfirmed>> GetJobConfirmedByUser(string userId)
        {
            return await _confirmedRepo.GetJobConfirmedByUser(userId);
        }

        public async Task<IReadOnlyList<JobConfirmed>> GetJobConfirmedByJobId(int jobId)
        {
            return await _confirmedRepo.GetJobConfirmedByJobId(jobId);
        }

        public async Task<JobConfirmed> CreateJobConfirmedAsync(JobConfirmed jobConfirmed)
        {
            return await _confirmedRepo.CreateJobConfirmedAsync(jobConfirmed);
        }

        public async Task<JobConfirmed> DeleteJobConfirmedAsync(int Id)
        {
            return await _confirmedRepo.DeleteJobConfirmedAsync(Id);
        }

        public async Task<JobConfirmed> UpdateJobConfirmedAsync(JobConfirmed jobConfirmed)
        {
            return await _confirmedRepo.UpdateJobConfirmedAsync(jobConfirmed);
        }

        public async Task<TimeDetail> GetTimeDetailAsync(int Id)
        {
           return await _repositoryHelper.GetTimeDetailAsync(Id);
        }

        public async Task<IReadOnlyList<TimeDetail>> GetTimeDetailsAsync()
        {
            return await _repositoryHelper.GetTimeDetailsAsync();
        }

       

        public async Task<JobToRequest> GetJobToRequestByIdAsync(int Id)
        {
            return await _jobRequestRepo.GetJobToRequestByIdAsync(Id);
        }

        public async Task<IReadOnlyList<InvitedCandidate>> InsertInvitedCandidateAsync(int[] candidateId, int jobToRequestId)
        {
            return await _invitedCandidateRepo.InsertInvitedCandidateAsync(candidateId, jobToRequestId);
        }

        public async Task<IReadOnlyList<Candidate>> GetCandidateForInviteAsync(int gradeId)
        {
            return await _repositoryHelper.GetCandidateForInviteAsync(gradeId);
        }

        public async Task<IReadOnlyList<Candidate>> GetCandidateInvitedAsync(int jobRequestId)
        {
            return await _repositoryHelper.GetCandidateInvitedAsync(jobRequestId);
        }

        public async Task<IReadOnlyList<Candidate>> GetCandidateInProgressAsync(int jobRequestId)
        {
            return await _repositoryHelper.GetCandidateInProgressAsync(jobRequestId);
        }

        public async Task<IReadOnlyList<CandidateResponded>> GetCandidateRespondedAsync(int jobRequestId)
        {
            return await _repositoryHelper.GetCandidateRespondedAsync(jobRequestId);
        }

        public async Task<IReadOnlyList<CandidateBooked>> GetCandidateBookedAsync(int jobRequestId)
        {
            return await _repositoryHelper.GetCandidateBookedAsync(jobRequestId);
        }

        public async Task<JobConfirmed> FinalizedJobConfirmedAsync(ConfirmeFinal confirmeFinal)
        {
            return await _confirmedRepo.FinalizedJobConfirmedAsync(confirmeFinal);
        }

        public async Task<Agency> AddAgencyAsync(Agency agency)
        {
            return await _agencyRepository.AddAgencyAsync(agency);
        }

        public async Task<Agency> UpdateAgencyAsync(Agency agency)
        {
            return await _agencyRepository.UpdateAgencyAsync(agency);
        }

        public async Task<Agency> DeleteAgencyAsync(Agency agency)
        {
            return await _agencyRepository.DeleteAgencyAsync(agency);
        }

        public async Task<IReadOnlyList<Agency>> GetAgencies()
        {
            return await _agencyRepository.GetAgencies();
        }
    }
}
