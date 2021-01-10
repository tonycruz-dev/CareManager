using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Core.EntityHelpers;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    public class HRManagerController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJobRequestRepository _jobRequestRepo;
        private readonly IClientLocation _repoClientLocation;
        private readonly IInvitedCandidateRepository _repoInvCandidate;
        private readonly IJobConfirmedRepository _repoJobConfi;
        private readonly IAgencyRepository _agencyRepository;
        private readonly ICandidateManageRepository _candidateManageRepository;
        private readonly IMapper _mapper;
        private readonly IHRManagerRepository _repo;
        private readonly IRepositoryHelper _repositoryHelper;

        public HRManagerController(IHRManagerRepository repo, 
            IRepositoryHelper repositoryHelper, 
            UserManager<AppUser> userManager,
            IJobRequestRepository jobRequestRepo,
            IClientLocation repoClientLocation,
            IInvitedCandidateRepository repoInvCandidate,
            IJobConfirmedRepository repoJobConfi,
            IAgencyRepository agencyRepository,
            ICandidateManageRepository CandidateManageRepository,
            IMapper mapper)
        {
            _repo = repo;
            _repositoryHelper = repositoryHelper;
            _userManager = userManager;
            _jobRequestRepo = jobRequestRepo;
            _repoClientLocation = repoClientLocation;
            _repoInvCandidate = repoInvCandidate;
            _repoJobConfi = repoJobConfi;
            _agencyRepository = agencyRepository;
            _candidateManageRepository = CandidateManageRepository;
            _mapper = mapper;
        }
        // GET: api/<HRManagerController>
        [HttpGet("GetHRDashboard")]
        public async Task<ActionResult<HRDashboardDto>> GetHRDashboard()
        {

            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
            var agenciesRequest = await _repositoryHelper.GetHRDashboard(); // .GetAgencies();
            var resultAgencies = _mapper.Map<HRDashboard, HRDashboardDto>(agenciesRequest);
            return Ok(resultAgencies);
        }
        [HttpGet("GetAgeny/{id}")]
        public async Task<ActionResult<AgencyDto>> GetAgency(int id)
        {

            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
            var agencyRequest = await _agencyRepository.GetAgencyAsync(id);
            var resultAgency = _mapper.Map<Agency, AgencyDto>(agencyRequest);
            return Ok(resultAgency);
        }
        [HttpGet("GetAgencis")]
        public async Task<ActionResult<IReadOnlyList<AgencyDto>>> GetAgencis()
        {

            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
            var agenciesRequest = await _agencyRepository.GetAgencies();
            var resultAgencies = _mapper.Map<IReadOnlyList<Agency>, IReadOnlyList<AgencyDto>>(agenciesRequest);
            return Ok(resultAgencies);
        }
        [HttpGet("GetCandidates")]
        public async Task<ActionResult<IReadOnlyList<CandidateDto>>> GetCandidates()
        {

            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var candidatesRequest = await _candidateManageRepository.GetCandidatesAsync();
            var resultCandidates = _mapper.Map<IReadOnlyList<Candidate>, IReadOnlyList<CandidateDto>>(candidatesRequest);
            return Ok(resultCandidates);
        }
        [HttpGet("GetCandidate/{id}")]
        public async Task<ActionResult<CandidateDto>> GetCandidate(int id)
        {

            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var candidateRequest = await _candidateManageRepository.GetCandidateAsync(id);
            var resultCandidate = _mapper.Map<Candidate, CandidateDto>(candidateRequest);
            return Ok(resultCandidate);
        }
        [HttpGet("GetJobRequestByAgency")]
        public async Task<ActionResult<JobRequestByAgencyDto>> GetJobRequestByAgency([FromQuery] JobRequestParams specParams)
        {

            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
            specParams.UserId = user.Id;

            var jobRequests = await _repo.GetJobToRequestByAgency(specParams.AgencyId, specParams);
            var resultJobstatus = await _repositoryHelper.GetAgencyJobStatesAsync(specParams.AgencyId);
            var data = _mapper.Map<IReadOnlyList<JobToRequest>, IReadOnlyList<JobToRequestToReturnDto>>(jobRequests);

            var resultJobRequest = new JobRequestByAgencyDto
            {
                Data = data,
                CurrentPage = jobRequests.CurrentPage,
                PageSize = jobRequests.PageSize,
                TotalCount = jobRequests.TotalCount,
                TotalPages = jobRequests.TotalPages,
                Pending = resultJobstatus.Pending,
                InProgress = resultJobstatus.InProgress,
                Booked = resultJobstatus.Booked,
                Finish = resultJobstatus.Finish,
                Canceled = resultJobstatus.Canceled,

            };

           /// Response.AddPagination(jobRequests.CurrentPage, jobRequests.PageSize, jobRequests.TotalCount, jobRequests.TotalPages);
            return Ok(resultJobRequest);
        }
        [HttpGet("GetJobToRequestById/{id}")]
        public async Task<ActionResult<JobToRequestToReturnDto>> GetJobToRequestById(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var jobRequestToRetun = await _jobRequestRepo.GetJobToRequestByIdAsync(id);
            return Ok(_mapper.Map<JobToRequest, JobToRequestToReturnDto>(jobRequestToRetun));
        }
        [HttpGet("GetHRAgencyLoops/{id}")]
        public async Task<ActionResult<RetunAgencyLoopDto>> GetAgencyLoops(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var agencyLoops = await _repositoryHelper.GetAgencyLoop(id); 
            var data = _mapper.Map<AgencyListLoop, RetunAgencyLoopDto>(agencyLoops);
            return data;
        }
        [HttpPost("AddClientLocation")]
        public async Task<ActionResult<ClientLocationDto>> AddClientLocation(ClientLocationDto clientLocation)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            clientLocation.AgencyId = clientLocation.AgencyId;
            var clientMap = _mapper.Map<ClientLocationDto, ClientLocation>(clientLocation);
            var clientToRetun = await _repoClientLocation.CreateClientLocationAsync(clientMap);
            return Ok(_mapper.Map<ClientLocation, ClientLocationDto>(clientToRetun));
        }
        [HttpGet("GetClientLocations/{id}")]
        public async Task<ActionResult<ClientLocationDto>> GetClientLocations(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var clientToRetun = await _repoClientLocation.GetClientLocationByAgency(id);
            return Ok(_mapper.Map<IReadOnlyList<ClientLocation>, IReadOnlyList<ClientLocationDto>>(clientToRetun));
        }
        [HttpDelete("ClientLocationDelete/{id}")]
        public async Task<ActionResult<ClientLocationDto>> ClientLocationDelete(int id)
        {
           var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var cl = await _repoClientLocation.DeleteClientLocationAsync(id);
            return Ok(_mapper.Map<ClientLocation, ClientLocationDto>(cl));
        }
        [HttpPut("ClientLocationUpdate")]
        public async Task<ActionResult<ClientLocationDto>> ClientLocationUpdate(ClientLocationDto clientLocationDto)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
 
            var clientMap = _mapper.Map<ClientLocationDto, ClientLocation>(clientLocationDto);
            var clientToRetun = await _repoClientLocation.UpdateClientLocationAsync(clientMap);
            return Ok(_mapper.Map<ClientLocation, ClientLocationDto>(clientToRetun));
        }
        [HttpPost("CreateJobRequest")]
        public async Task<ActionResult<IReadOnlyList<JobToRequestToReturnDto>>> CreateJobRequest(JobToRequestForInsertDto jobToRequest)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            // var agency = await _agencyServices.GetAgencyByUserIdAsync(user.Id);
            // jobToRequest.AgencyId = agency.Id;
            jobToRequest.AppUserId = user.Id;

            DateTime dateFrom = jobToRequest.DateRange[0];
            DateTime dateTo = jobToRequest.DateRange[1];
            var timeStart = await _repositoryHelper.GetTimeDetailAsync(jobToRequest.TimeDetailId);
            var timeEnd = await _repositoryHelper.GetTimeDetailAsync(jobToRequest.EndTimeDetailId);
            var results = dateTo.Subtract(dateFrom).Days;
            var returnAfterInsert = new List<JobToRequestToReturnDto>();
            if (results > 0)
            {
                for (int i = 0; i <= results; i++)
                {
                    var addedDate = dateFrom.AddDays(i);
                    var jtrid = new JobToRequestInsertDto
                    {
                        AgencyId = jobToRequest.AgencyId,
                        AppUserId = jobToRequest.AppUserId,
                        AriaId = jobToRequest.AriaId,
                        NumberCandidate = jobToRequest.NumberCandidate,
                        JobDateStart = addedDate,
                        JobDateEnd = addedDate,
                        StartTime = timeStart.Hour,
                        EndTime = timeEnd.Hour,
                        NumberApplied = 0,
                        TimeDetailId = jobToRequest.TimeDetailId,
                        JobTypeId = jobToRequest.JobTypeId,
                        PaymentTypeId = jobToRequest.PaymentTypeId,
                        GradeId = jobToRequest.GradeId,
                        ClientLocationId = jobToRequest.ClientLocationId,
                        AttributeDetailId = jobToRequest.AttributeDetailId,
                        ShiftStateId = 1,
                    };
                    var itemMap = _mapper.Map<JobToRequestInsertDto, JobToRequest>(jtrid);
                    var itemToRetun = await _jobRequestRepo.CreateJobToRequestAsync(itemMap);
                    var itemResult = _mapper.Map<JobToRequest, JobToRequestToReturnDto>(itemToRetun);
                    returnAfterInsert.Add(itemResult);

                }
            }
            else
            {
                var jtrid = new JobToRequestInsertDto
                {
                    AgencyId = jobToRequest.AgencyId,
                    AppUserId = jobToRequest.AppUserId,
                    AriaId = jobToRequest.AriaId,
                    NumberCandidate = jobToRequest.NumberCandidate,
                    JobDateStart = dateFrom,
                    JobDateEnd = dateFrom,
                    StartTime = timeStart.Hour,
                    EndTime = timeEnd.Hour,
                    NumberApplied = 0,
                    TimeDetailId = jobToRequest.TimeDetailId,
                    JobTypeId = jobToRequest.JobTypeId,
                    PaymentTypeId = jobToRequest.PaymentTypeId,
                    GradeId = jobToRequest.GradeId,
                    ClientLocationId = jobToRequest.ClientLocationId,
                    AttributeDetailId = jobToRequest.AttributeDetailId,
                    ShiftStateId = 1,
                };
                var itemMap = _mapper.Map<JobToRequestInsertDto, JobToRequest>(jtrid);
                var itemToRetun = await _jobRequestRepo.CreateJobToRequestAsync(itemMap);
                var itemResult = _mapper.Map<JobToRequest, JobToRequestToReturnDto>(itemToRetun);
                returnAfterInsert.Add(itemResult);
            }

            return Ok(returnAfterInsert);
        }
        [HttpDelete("DeleteJobRequest/{id}")]
        public async Task<ActionResult<JobToRequestToReturnDto>> DeleteJobRequest(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var itemToRetun = await _jobRequestRepo.DeleteJobToRequestAsync(id);
            return Ok(_mapper.Map<JobToRequest, JobToRequestToReturnDto>(itemToRetun));
        }

        [HttpPut("UpdateJobRequest")]
        public async Task<ActionResult<JobToRequestToReturnDto>> UpdateJobRequest(JobToRequestInsertDto jobToRequestInsertDto)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var clientMap = _mapper.Map<JobToRequestInsertDto, JobToRequest>(jobToRequestInsertDto);
            var clientToRetun = await _jobRequestRepo.UpdateJobToRequestAsync(clientMap);
            return Ok(_mapper.Map<JobToRequest, JobToRequestToReturnDto>(clientToRetun));
        }
        //InvitedCandidate
        [HttpGet("GetInvitedCandidateByAgency/{id}")]
        public async Task<ActionResult<IReadOnlyList<InvitedCandidateReturnDto>>> GetInvitedCandidateByAgency(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
            
            var jobRequestToRetun = await _repoInvCandidate.GetInvitedCandidateByAgency(id);
            return Ok(_mapper.Map<IReadOnlyList<InvitedCandidate>, IReadOnlyList<InvitedCandidateReturnDto>>(jobRequestToRetun));
        }

       
        [HttpGet("GetInvitedCandidateByJobId/{id}")]
        public async Task<ActionResult<IReadOnlyList<InvitedCandidateReturnDto>>> GetInvitedCandidateByJobId(int Id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
            var jobRequestToRetun = await _repoInvCandidate.GetInvitedCandidateByJobId(Id);
            return Ok(_mapper.Map<IReadOnlyList<InvitedCandidate>, IReadOnlyList<InvitedCandidateReturnDto>>(jobRequestToRetun));
        }
        [HttpPost("CreateInvitedCandidate")]
        public async Task<ActionResult<InvitedCandidateReturnDto>> CreateInvitedCandidate(InvitedCandidateInserOrUpdateDto invitedCandidate)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            invitedCandidate.AppUserPostedId = user.Id;
            var itemMap = _mapper.Map<InvitedCandidateInserOrUpdateDto, InvitedCandidate>(invitedCandidate);
            var itemToRetun = await _repoInvCandidate.CreateInvitedCandidateAsync(itemMap);

            return Ok(_mapper.Map<InvitedCandidate, InvitedCandidateReturnDto>(itemToRetun));
        }
        [HttpPost("InsertInvitedCandidate")]
        public async Task<ActionResult<InvitedCandidateReturnDto>> InsertInvitedCandidate(InvitedCandidateForInsertDto invitedCandidateForInsertDto)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
          

            var itemToRetun = await _repoInvCandidate.InsertInvitedCandidateAsync(invitedCandidateForInsertDto.CandidatesId, invitedCandidateForInsertDto.JobToRequestId);

            return Ok(_mapper.Map<IReadOnlyList<InvitedCandidate>, IReadOnlyList<InvitedCandidateReturnDto>>(itemToRetun));
        }
        [HttpDelete("DeleteInvitedCandidate/{id}")]
        public async Task<ActionResult<InvitedCandidateReturnDto>> DeleteInvitedCandidate(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var itemToRetun = await _repoInvCandidate.DeleteInvitedCandidateAsync(id);
            return Ok(_mapper.Map<InvitedCandidate, InvitedCandidateReturnDto>(itemToRetun));
        }

        [HttpPut("UpdateInvitedCandidate")]
        public async Task<ActionResult<JobToRequestToReturnDto>> UpdateInvitedCandidate(InvitedCandidateInserOrUpdateDto invitedCandidate)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var clientMap = _mapper.Map<InvitedCandidateInserOrUpdateDto, InvitedCandidate>(invitedCandidate);
            var clientToRetun = await _repoInvCandidate.UpdateInvitedCandidateAsync(clientMap);
            return Ok(_mapper.Map<InvitedCandidate, InvitedCandidateReturnDto>(clientToRetun));
        }
        //JobConfirmed
        [HttpGet("GetJobConfirmedByAgency/{id}")]
        public async Task<ActionResult<IReadOnlyList<JobConfirmedToReturnDto>>> GetJobConfirmedByAgency(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var jobRequestToRetun = await _repoJobConfi.GetJobConfirmedByAgency(id);
            return Ok(_mapper.Map<IReadOnlyList<JobConfirmed>, IReadOnlyList<JobConfirmedToReturnDto>>(jobRequestToRetun));
        }
        
        [HttpGet("GetJobConfirmedByJobId/{id}")]
        public async Task<ActionResult<IReadOnlyList<JobConfirmedToReturnDto>>> GetJobConfirmedByJobId(int Id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var jobRequestToRetun = await _repoJobConfi.GetJobConfirmedByJobId(Id);
            return Ok(_mapper.Map<IReadOnlyList<JobConfirmed>, IReadOnlyList<JobConfirmedToReturnDto>>(jobRequestToRetun));
        }
        [HttpPost("CreateJobConfirmed")]
        public async Task<ActionResult<JobConfirmedToReturnDto>> CreateJobConfirmed(JobConfirmedInserOrUpdateDto jobConfirmed)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            jobConfirmed.AppUserPostedId = user.Id;
            var itemMap = _mapper.Map<JobConfirmedInserOrUpdateDto, JobConfirmed>(jobConfirmed);
            var itemToRetun = await _repoJobConfi.CreateJobConfirmedAsync(itemMap);

            return Ok(_mapper.Map<JobConfirmed, JobConfirmedToReturnDto>(itemToRetun));
        }
        [HttpDelete("DeleteJobConfirmed/{id}")]
        public async Task<ActionResult<JobConfirmedToReturnDto>> DeleteJobConfirmed(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var itemToRetun = await _repoJobConfi.DeleteJobConfirmedAsync(id);
            return Ok(_mapper.Map<JobConfirmed, JobConfirmedToReturnDto>(itemToRetun));
        }

        [HttpPut("UpdateJobConfirmed")]
        public async Task<ActionResult<JobConfirmedToReturnDto>> UpdateJobConfirmed(JobConfirmedInserOrUpdateDto jobConfirmed)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var clientMap = _mapper.Map<JobConfirmedInserOrUpdateDto, JobConfirmed>(jobConfirmed);
            var clientToRetun = await _repoJobConfi.UpdateJobConfirmedAsync(clientMap);
            return Ok(_mapper.Map<JobConfirmed, JobConfirmedToReturnDto>(clientToRetun));
        }
        [HttpGet("GetCandidateForInvite/{id}")]
        public async Task<ActionResult<IReadOnlyList<CandidateForInvitationDto>>> GetCandidateForInvite(int Id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var candidateForInviteToReturn = await _repositoryHelper.GetCandidateForInviteAsync(Id);
            return Ok(_mapper.Map<IReadOnlyList<Candidate>, IReadOnlyList<CandidateForInvitationDto>>(candidateForInviteToReturn));

        }
        [HttpGet("GetCandidateInProgress/{id}")]
        public async Task<ActionResult<IReadOnlyList<CandidateForInvitationDto>>> GetCandidateInProgress(int Id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
            
            var candidateInProgressToReturn = await _repositoryHelper.GetCandidateInProgressAsync(Id);
            return Ok(_mapper.Map<IReadOnlyList<Candidate>, IReadOnlyList<CandidateForInvitationDto>>(candidateInProgressToReturn));
        }
        [HttpGet("GetCandidateResponded/{id}")]
        public async Task<ActionResult<IReadOnlyList<CandidateRespondedDto>>> GetCandidateResponded(int Id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var candidateRespondedToReturn = await _repositoryHelper.GetCandidateRespondedAsync(Id);
            return Ok(_mapper.Map<IReadOnlyList<CandidateResponded>, IReadOnlyList<CandidateRespondedDto>>(candidateRespondedToReturn));
        }
        [HttpGet("GetCandidateBooked/{id}")]
        public async Task<ActionResult<IReadOnlyList<CandidateBookedDto>>> GetCandidateBooked(int Id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var candidateRespondedToReturn = await _repositoryHelper.GetCandidateBookedAsync(Id);
            return Ok(_mapper.Map<IReadOnlyList<CandidateBooked>, IReadOnlyList<CandidateBookedDto>>(candidateRespondedToReturn));
        }
        [HttpPut("FinalizedJobConfirmed")]
        public async Task<ActionResult<JobConfirmedToReturnDto>> FinalizedJobConfirmedAsync(ConfirmeFinalDto confirmeFinal)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();


            var clientMap = _mapper.Map<ConfirmeFinalDto, ConfirmeFinal>(confirmeFinal);
            var clientToRetun = await _repoJobConfi.FinalizedJobConfirmedAsync(clientMap);
            return Ok(_mapper.Map<JobConfirmed, JobConfirmedToReturnDto>(clientToRetun));
        }
        // Candidates
        [HttpGet("GetCandidateInvited/{id}")]
        public async Task<ActionResult<IReadOnlyList<InvitedCandidateFromCandidateDto>>> GetCandidateInvited(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var invites = await _candidateManageRepository.GetInvitesAsync(id);
            var returnResults = _mapper.Map<List<InvitedCandidate>, List<InvitedCandidateFromCandidateDto>>(invites);
            Console.WriteLine(returnResults);
            return Ok(returnResults);
        }
        [HttpGet("GetJobConforme/{id}")]
        public async Task<ActionResult<IReadOnlyList<JobConfirmedToReturnDto>>> GetJobConforme(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var invites = await _candidateManageRepository.GetJobConformedAsync(id);
            var returnResults = _mapper.Map<List<JobConfirmed>, List<JobConfirmedToReturnDto>>(invites);
            Console.WriteLine(returnResults);
            return Ok(returnResults);
        }
        [HttpGet("AcceptJob/{id}")]
        public async Task<ActionResult> AcceptJob(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var candidate = await _candidateManageRepository.AcceptInvitesAsync(id);
            var jobAccepted = true;
            return Ok(jobAccepted);
        }
        [HttpGet("RejectJob/{id}")]
        public async Task<ActionResult> RejectJob(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var candidate = await _candidateManageRepository.RejectInvitesAsync(id);
            var jobRejected = true;
            return Ok(jobRejected);

        }
        [HttpPost("AddCandidate")]
        public async Task<ActionResult> PostAddCandidate([FromBody] CandidateDto candidate)
        {

            try
            {
                var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
                var itemToMap = _mapper.Map<CandidateDto, Candidate>(candidate);
                itemToMap.AppUserId = user.Id;
                itemToMap.AppUser = user;
                itemToMap.PhotoUrl = user.Avatar;
                var createdCandidate = await _candidateManageRepository.CreateCandidateAsync(itemToMap);
                return Ok(createdCandidate);
            }
            catch (DbUpdateConcurrencyException)
            {

                return NotFound();
            }

            //return Ok();
        }
        [HttpGet("GetJobFinish/{id}")]
        public async Task<ActionResult<IReadOnlyList<JobFinishToReturnDto>>> GetJobFinish(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();

            var invites = await _candidateManageRepository.GetJobFinishAsync(id);
            var returnResults = _mapper.Map<List<JobConfirmed>, List<JobFinishToReturnDto>>(invites);
            Console.WriteLine(returnResults);
            return Ok(returnResults);
        }
    }
}
