using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.EntityHelpers;
using Core.Interfaces;
using Core.Loops;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AgenciesController : BaseApiController
    {
        private readonly IAgencyServices _agencyServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHRManagerRepository _repoHR;
        private readonly IMapper _mapper;
        private readonly IRepositoryHelper _repositoryHelper;
        private readonly IGenericRepository<JobToRequest> _jobRequestRepository;

        public AgenciesController(IAgencyServices agencyServices, 
            UserManager<AppUser> userManager,
            IHRManagerRepository repoHR,
            IMapper mapper,
            IRepositoryHelper repositoryHelper,
            IGenericRepository<JobToRequest> jobRequestRepository)
        {
            _agencyServices = agencyServices;
            _userManager = userManager;
            _repoHR = repoHR;
            _mapper = mapper;
            _repositoryHelper = repositoryHelper;
            _jobRequestRepository = jobRequestRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<JobToRequestToReturnDto>>> GetAgencies([FromQuery] JobToRequesSpecParams specParams)
        {
            //AgencyWithJobLocationSpecification
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
            specParams.UserId = user.Id;

            var jobRequests = await _agencyServices.GetJobToRequests(specParams);
            var data = _mapper.Map<IReadOnlyList<JobToRequest>, IReadOnlyList<JobToRequestToReturnDto>>(jobRequests);
            return Ok(data);
        }

        [HttpGet("GetAgencyLoops")]
        public async Task<ActionResult<RetunAgencyLoopDto>> GetAgencyLoops()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var agency = await _agencyServices.GetAgencyByUserIdAsync(user.Id);
            var agencyLoops = await _agencyServices.GetRetunAgencyLoop(agency.Id);
            var data = _mapper.Map<RetunAgencyLoop, RetunAgencyLoopDto>(agencyLoops);
            return data;
        }

        [HttpPost("AddClientLocation")]
        public async Task<ActionResult<ClientLocationDto>> AddClientLocation(ClientLocationDto clientLocationDto)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var agency = await _agencyServices.GetAgencyByUserIdAsync(user.Id);
            clientLocationDto.AgencyId = agency.Id;
            var clientMap = _mapper.Map<ClientLocationDto, ClientLocation>(clientLocationDto);
            var clientToRetun = await _agencyServices.CreateClientLocationAsync(clientMap);
            return Ok(_mapper.Map<ClientLocation, ClientLocationDto>(clientToRetun));
        }
        [HttpGet("GetClientLocations")]
        public async Task<ActionResult<ClientLocationDto>> GetClientLocations()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var agency = await _agencyServices.GetAgencyByUserIdAsync(user.Id);

            var clientToRetun = await _agencyServices.GetClientLocationByAgency(agency.Id);
            return Ok(_mapper.Map<IReadOnlyList<ClientLocation>, IReadOnlyList<ClientLocationDto>>(clientToRetun));
        }
        [HttpDelete("ClientLocationDelete/{id}")]
        public async Task<ActionResult<ClientLocationDto>> ClientLocationDelete(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var agency = await _agencyServices.GetAgencyByUserIdAsync(user.Id);
            if (agency == null) return NotFound();

            var cl = await _agencyServices.DeleteClientLocationAsync(id);
            return Ok(_mapper.Map<ClientLocation, ClientLocationDto>(cl));
        }
        [HttpPut("ClientLocationUpdate")]
        public async Task<ActionResult<ClientLocationDto>> ClientLocationUpdate(ClientLocationDto clientLocationDto)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var agency = await _agencyServices.GetAgencyByUserIdAsync(user.Id);
            clientLocationDto.AgencyId = agency.Id;
            var clientMap = _mapper.Map<ClientLocationDto, ClientLocation>(clientLocationDto);
            var clientToRetun = await _agencyServices.UpdateClientLocationAsync(clientMap);
            return Ok(_mapper.Map<ClientLocation, ClientLocationDto>(clientToRetun));
        }
        [HttpGet("GetJobToRequests")]
        public async Task<ActionResult<Pagination<JobToRequestToReturnDto>>> GetJobToRequests([FromQuery] JobToRequesSpecParams specParams)
        {
            
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
            specParams.UserId = user.Id;

            var countSpec = new JobToRequestCounterSpec(specParams);
            var itemCounter = await _jobRequestRepository.CountAsync(countSpec);
            var jobRequests = await _agencyServices.GetJobToRequests(specParams);

            var data = _mapper.Map<IReadOnlyList<JobToRequest>, IReadOnlyList<JobToRequestToReturnDto>>(jobRequests);
            return Ok(new Pagination<JobToRequestToReturnDto>(specParams.PageIndex, specParams.PageSize, itemCounter, data));
       
        }
        [HttpGet("GetJobRequestByAgency")]
        public async Task<ActionResult<JobToRequestToReturnDto>> GetJobRequestByAgency()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var agency = await _agencyServices.GetAgencyByUserIdAsync(user.Id);
            //JobToRequestSpec
            var jobRequestToRetun = await _agencyServices.GetJobToRequestByAgency(agency.Id);
            return Ok(_mapper.Map<IReadOnlyList<JobToRequest>, IReadOnlyList<JobToRequestToReturnDto>>(jobRequestToRetun));
        }
        [HttpGet("GetJobToRequestById/{id}")]
        public async Task<ActionResult<JobToRequestToReturnDto>> GetJobToRequestById(int id)
        {
            var jobRequestToRetun = await _agencyServices.GetJobToRequestByIdAsync(id);
            return Ok(_mapper.Map<JobToRequest, JobToRequestToReturnDto>(jobRequestToRetun));
        }
        [HttpGet("GetJobRequestByUser")]
        public async Task<ActionResult<JobToRequestToReturnDto>> GetJobRequestByUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            //JobToRequestSpec
            var jobRequestToRetun = await _agencyServices.GetJobToRequestByUser(user.Id);
            return Ok(_mapper.Map<IReadOnlyList<JobToRequest>, IReadOnlyList<JobToRequestToReturnDto>>(jobRequestToRetun));
        }
        [HttpPost("CreateJobRequest")]
        public async Task<ActionResult<IReadOnlyList<JobToRequestToReturnDto>>> CreateJobRequest(JobToRequestForInsertDto jobToRequest)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var agency = await _agencyServices.GetAgencyByUserIdAsync(user.Id);
            jobToRequest.AgencyId = agency.Id;
            jobToRequest.AppUserId = user.Id;

            DateTime dateFrom = jobToRequest.DateRange[0];
            DateTime dateTo = jobToRequest.DateRange[1];
            var timeStart = await _agencyServices.GetTimeDetailAsync(jobToRequest.TimeDetailId);
            var timeEnd = await _agencyServices.GetTimeDetailAsync(jobToRequest.EndTimeDetailId);
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
                    var itemToRetun = await _agencyServices.CreateJobToRequestAsync(itemMap);
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
                var itemToRetun = await _agencyServices.CreateJobToRequestAsync(itemMap);
                var itemResult = _mapper.Map<JobToRequest, JobToRequestToReturnDto>(itemToRetun);
                returnAfterInsert.Add(itemResult);
            }

            return Ok(returnAfterInsert);
        }
        [HttpDelete("DeleteJobRequest/{id}")]
        public async Task<ActionResult<JobToRequestToReturnDto>> DeleteJobRequest(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var agency = await _agencyServices.GetAgencyByUserIdAsync(user.Id);
            if (agency == null) return NotFound();

            var itemToRetun = await _agencyServices.DeleteJobToRequestAsync(id);
            return Ok(_mapper.Map<JobToRequest, JobToRequestToReturnDto>(itemToRetun));
        }

        [HttpPut("UpdateJobRequest")]
        public async Task<ActionResult<JobToRequestToReturnDto>> UpdateJobRequest(JobToRequestInsertDto jobToRequestInsertDto)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var agency = await _agencyServices.GetAgencyByUserIdAsync(user.Id);
            jobToRequestInsertDto.AgencyId = agency.Id;
            var clientMap = _mapper.Map<JobToRequestInsertDto, JobToRequest>(jobToRequestInsertDto);
            var clientToRetun = await _agencyServices.UpdateJobToRequestAsync(clientMap);
            return Ok(_mapper.Map<JobToRequest, JobToRequestToReturnDto>(clientToRetun));
        }

        //InvitedCandidate
        [HttpGet("GetInvitedCandidateByAgency")]
        public async Task<ActionResult<IReadOnlyList<InvitedCandidateReturnDto>>> GetInvitedCandidateByAgency()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var agency = await _agencyServices.GetAgencyByUserIdAsync(user.Id);
            //JobToRequestSpec
            var jobRequestToRetun = await _agencyServices.GetInvitedCandidateByAgency(agency.Id);
            return Ok(_mapper.Map<IReadOnlyList<InvitedCandidate>, IReadOnlyList<InvitedCandidateReturnDto>>(jobRequestToRetun));
        }

        [HttpGet("GetInvitedCandidateByUser")]
        public async Task<ActionResult<IReadOnlyList<InvitedCandidateReturnDto>>> GetInvitedCandidateByUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var jobRequestToRetun = await _agencyServices.GetInvitedCandidateByUser(user.Id);
            return Ok(_mapper.Map<IReadOnlyList<InvitedCandidate>, IReadOnlyList<InvitedCandidateReturnDto>>(jobRequestToRetun));
        }
        [HttpGet("GetInvitedCandidateByJobId/{id}")]
        public async Task<ActionResult<IReadOnlyList<InvitedCandidateReturnDto>>> GetInvitedCandidateByJobId(int Id)
        {
            var jobRequestToRetun = await _agencyServices.GetInvitedCandidateByJobId(Id);
            return Ok(_mapper.Map<IReadOnlyList<InvitedCandidate>, IReadOnlyList<InvitedCandidateReturnDto>>(jobRequestToRetun));
        }
        [HttpPost("CreateInvitedCandidate")]
        public async Task<ActionResult<InvitedCandidateReturnDto>> CreateInvitedCandidate(InvitedCandidateInserOrUpdateDto invitedCandidate)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var agency = await _agencyServices.GetAgencyByUserIdAsync(user.Id);
            invitedCandidate.AgencyId = agency.Id;
            invitedCandidate.AppUserPostedId = user.Id;
            var itemMap = _mapper.Map<InvitedCandidateInserOrUpdateDto, InvitedCandidate>(invitedCandidate);
            var itemToRetun = await _agencyServices.CreateInvitedCandidateAsync(itemMap);

            return Ok(_mapper.Map<InvitedCandidate, InvitedCandidateReturnDto>(itemToRetun));
        }
        [HttpPost("InsertInvitedCandidate")]
        public async Task<ActionResult<InvitedCandidateReturnDto>> InsertInvitedCandidate(InvitedCandidateForInsertDto invitedCandidateForInsertDto)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var agency = await _agencyServices.GetAgencyByUserIdAsync(user.Id);
            if (agency == null) return NotFound();

            var itemToRetun = await _agencyServices.InsertInvitedCandidateAsync(invitedCandidateForInsertDto.CandidatesId, invitedCandidateForInsertDto.JobToRequestId);

            return Ok(_mapper.Map<IReadOnlyList<InvitedCandidate>, IReadOnlyList<InvitedCandidateReturnDto>>(itemToRetun));
        }
        [HttpDelete("DeleteInvitedCandidate/{id}")]
        public async Task<ActionResult<InvitedCandidateReturnDto>> DeleteInvitedCandidate(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var agency = await _agencyServices.GetAgencyByUserIdAsync(user.Id);
            if (agency == null) return NotFound();

            var itemToRetun = await _agencyServices.DeleteInvitedCandidateAsync(id);
            return Ok(_mapper.Map<InvitedCandidate, InvitedCandidateReturnDto>(itemToRetun));
        }

        [HttpPut("UpdateInvitedCandidate")]
        public async Task<ActionResult<JobToRequestToReturnDto>> UpdateInvitedCandidate(InvitedCandidateInserOrUpdateDto invitedCandidate)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var agency = await _agencyServices.GetAgencyByUserIdAsync(user.Id);
            invitedCandidate.AgencyId = agency.Id;
            var clientMap = _mapper.Map<InvitedCandidateInserOrUpdateDto, InvitedCandidate>(invitedCandidate);
            var clientToRetun = await _agencyServices.UpdateInvitedCandidateAsync(clientMap);
            return Ok(_mapper.Map<InvitedCandidate, InvitedCandidateReturnDto>(clientToRetun));
        }

        //JobConfirmed
        [HttpGet("GetJobConfirmedByAgency")]
        public async Task<ActionResult<IReadOnlyList<JobConfirmedToReturnDto>>> GetJobConfirmedByAgency()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var agency = await _agencyServices.GetAgencyByUserIdAsync(user.Id);

            var jobRequestToRetun = await _agencyServices.GetJobConfirmedByAgency(agency.Id);
            return Ok(_mapper.Map<IReadOnlyList<JobConfirmed>, IReadOnlyList<JobConfirmedToReturnDto>>(jobRequestToRetun));
        }
        [HttpGet("GetJobConfirmedByUser")]
        public async Task<ActionResult<IReadOnlyList<JobConfirmedToReturnDto>>> GetJobConfirmedByUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var jobRequestToRetun = await _agencyServices.GetJobConfirmedByUser(user.Id);
            return Ok(_mapper.Map<IReadOnlyList<JobConfirmed>, IReadOnlyList<JobConfirmedToReturnDto>>(jobRequestToRetun));
        }
        [HttpGet("GetJobConfirmedByJobId/{id}")]
        public async Task<ActionResult<IReadOnlyList<JobConfirmedToReturnDto>>> GetJobConfirmedByJobId(int Id)
        {
            var jobRequestToRetun = await _agencyServices.GetJobConfirmedByJobId(Id);
            return Ok(_mapper.Map<IReadOnlyList<JobConfirmed>, IReadOnlyList<JobConfirmedToReturnDto>>(jobRequestToRetun));
        }
        [HttpPost("CreateJobConfirmed")]
        public async Task<ActionResult<JobConfirmedToReturnDto>> CreateJobConfirmed(JobConfirmedInserOrUpdateDto jobConfirmed)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var agency = await _agencyServices.GetAgencyByUserIdAsync(user.Id);
            jobConfirmed.AgencyId = agency.Id;
            jobConfirmed.AppUserPostedId = user.Id;
            var itemMap = _mapper.Map<JobConfirmedInserOrUpdateDto, JobConfirmed>(jobConfirmed);
            var itemToRetun = await _agencyServices.CreateJobConfirmedAsync(itemMap);

            return Ok(_mapper.Map<JobConfirmed, JobConfirmedToReturnDto>(itemToRetun));
        }
        [HttpDelete("DeleteJobConfirmed/{id}")]
        public async Task<ActionResult<JobConfirmedToReturnDto>> DeleteJobConfirmed(int id)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var agency = await _agencyServices.GetAgencyByUserIdAsync(user.Id);
            if (agency == null) return NotFound();

            var itemToRetun = await _agencyServices.DeleteJobConfirmedAsync(id);
            return Ok(_mapper.Map<JobConfirmed, JobConfirmedToReturnDto>(itemToRetun));
        }

        [HttpPut("UpdateJobConfirmed")]
        public async Task<ActionResult<JobConfirmedToReturnDto>> UpdateJobConfirmed(JobConfirmedInserOrUpdateDto jobConfirmed)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var agency = await _agencyServices.GetAgencyByUserIdAsync(user.Id);
            jobConfirmed.AgencyId = agency.Id;
            var clientMap = _mapper.Map<JobConfirmedInserOrUpdateDto, JobConfirmed>(jobConfirmed);
            var clientToRetun = await _agencyServices.UpdateJobConfirmedAsync(clientMap);
            return Ok(_mapper.Map<JobConfirmed, JobConfirmedToReturnDto>(clientToRetun));
        }
        [HttpGet("GetCandidateForInvite/{id}")]
        public async Task<ActionResult<IReadOnlyList<CandidateForInvitationDto>>> GetCandidateForInvite(int Id)
        {
            var candidateForInviteToReturn = await _agencyServices.GetCandidateForInviteAsync(Id);
            return Ok(_mapper.Map<IReadOnlyList<Candidate>, IReadOnlyList<CandidateForInvitationDto>>(candidateForInviteToReturn));

        }
        [HttpGet("GetCandidateInProgress/{id}")]
        public async Task<ActionResult<IReadOnlyList<CandidateForInvitationDto>>> GetCandidateInProgress(int Id)
        {
            var candidateInProgressToReturn = await _agencyServices.GetCandidateInProgressAsync(Id);
            return Ok(_mapper.Map<IReadOnlyList<Candidate>, IReadOnlyList<CandidateForInvitationDto>>(candidateInProgressToReturn));
        }
        [HttpGet("GetCandidateResponded/{id}")]
        public async Task<ActionResult<IReadOnlyList<CandidateRespondedDto>>> GetCandidateResponded(int Id)
        {
            var candidateRespondedToReturn = await _agencyServices.GetCandidateRespondedAsync(Id);
            return Ok(_mapper.Map<IReadOnlyList<CandidateResponded>, IReadOnlyList<CandidateRespondedDto>>(candidateRespondedToReturn));
        }
        [HttpGet("GetCandidateBooked/{id}")]
        public async Task<ActionResult<IReadOnlyList<CandidateBookedDto>>> GetCandidateBooked(int Id)
        {
            var candidateRespondedToReturn = await _agencyServices.GetCandidateBookedAsync(Id);
            return Ok(_mapper.Map<IReadOnlyList<CandidateBooked>, IReadOnlyList<CandidateBookedDto>>(candidateRespondedToReturn));
        }
        [HttpPut("FinalizedJobConfirmed")]
        public async Task<ActionResult<JobConfirmedToReturnDto>> FinalizedJobConfirmedAsync(ConfirmeFinalDto confirmeFinal)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var agency = await _agencyServices.GetAgencyByUserIdAsync(user.Id);
            
            var clientMap = _mapper.Map<ConfirmeFinalDto, ConfirmeFinal>(confirmeFinal);
            var clientToRetun = await _agencyServices.FinalizedJobConfirmedAsync(clientMap);
            return Ok(_mapper.Map<JobConfirmed, JobConfirmedToReturnDto>(clientToRetun));
        }
        [HttpPost("AddAgency")]
        public async Task<ActionResult<AgencyDto>> AddAgencyAsync(AgencyDto agency)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var itemMap = _mapper.Map<AgencyDto, Agency>(agency);
            itemMap.AppUserId = user.Id;
            itemMap.AppUser = user;
            itemMap.LogoUrl = user.Avatar;
            var itemToRetun = await _agencyServices.AddAgencyAsync(itemMap);
            return Ok(_mapper.Map<Agency, AgencyDto>(itemToRetun));
        }
        [HttpGet("GetJobRequestData")]
        public async Task<ActionResult<JobRequestByAgencyDto>> GetJobRequestByAgency([FromQuery] JobRequestParams specParams)
        {

            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
            specParams.UserId = user.Id;
            var agency = await _agencyServices.GetAgencyByUserIdAsync(user.Id);
            specParams.AgencyId = agency.Id;
            var jobRequests = await _repoHR.GetJobToRequestByAgency(specParams.AgencyId, specParams);
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


    }
}