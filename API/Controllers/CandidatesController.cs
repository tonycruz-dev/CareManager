using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class CandidatesController : BaseApiController
    {
        private readonly IGenericRepository<Candidate> _candidateRepo;
        private readonly ICandidateManageRepository _candidateManageRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly CareManagerContext _context;

        public CandidatesController(
            IGenericRepository<Candidate> candidateRepo,
            ICandidateManageRepository candidateManageRepository,
            UserManager<AppUser> userManager,
            IMapper mapper,
            CareManagerContext context)
        {
            _candidateRepo = candidateRepo;
            _candidateManageRepository = candidateManageRepository;
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Candidate>>> GetCandidates()
        {
            var spec = new CandidateWithGradeSpecification();
            return Ok(await _candidateRepo.ListAsync(spec));
        }
        [HttpGet("GetCandidateInvited")]
         public async Task<ActionResult<IReadOnlyList<InvitedCandidateFromCandidateDto>>> GetCandidateInvited()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var candidate = await _candidateManageRepository.GetCandidateByUserIdAsync(user.Id);
            var invites = await _candidateManageRepository.GetInvitesAsync(candidate.Id);
            var returnResults = _mapper.Map<List<InvitedCandidate>, List<InvitedCandidateFromCandidateDto>>(invites);
            Console.WriteLine(returnResults);
            return Ok(returnResults);
        }
        [HttpGet("GetJobConforme")]
        public async Task<ActionResult<IReadOnlyList<JobConfirmedToReturnDto>>> GetJobConforme()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var candidate = await _candidateManageRepository.GetCandidateByUserIdAsync(user.Id);
            var invites = await _candidateManageRepository.GetJobConformedAsync(candidate.Id);
            var returnResults = _mapper.Map<List<JobConfirmed>, List<JobConfirmedToReturnDto>>(invites);
            Console.WriteLine(returnResults);
            return Ok(returnResults);
        }
        [HttpGet("AcceptJob/{id}")]
        public async Task<ActionResult> AcceptJob(int id)
        {
            var candidate = await _candidateManageRepository.AcceptInvitesAsync(id);
            var jobAccepted = true;
            return Ok(jobAccepted);
        }
        [HttpGet("RejectJob/{id}")]
        public async Task<ActionResult> RejectJob(int id)
        {
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
                var createdCandidate = await  _candidateManageRepository.CreateCandidateAsync(itemToMap);
                return Ok(createdCandidate);
            }
            catch (DbUpdateConcurrencyException)
            {

                return NotFound();
            }

            //return Ok();
        }
        [HttpGet("GetJobFinish")]
        public async Task<ActionResult<IReadOnlyList<JobFinishToReturnDto>>> GetJobFinish()
        {
           var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var candidate = await _candidateManageRepository.GetCandidateByUserIdAsync(user.Id);
            var invites = await _candidateManageRepository.GetJobFinishAsync(candidate.Id);
            var returnResults = _mapper.Map<List<JobConfirmed>, List<JobFinishToReturnDto>>(invites);
            Console.WriteLine(returnResults);
            return Ok(returnResults);
        }
        
    }
}