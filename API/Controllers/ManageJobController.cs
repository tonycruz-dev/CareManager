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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class ManageJobController : BaseApiController
    {
        private readonly IGenericRepository<Candidate> _candidateRepo;
        private readonly IGenericRepository<JobToRequest> _jobRecRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly CareManagerContext _context;
        private readonly IMapper _mapper;

        public ManageJobController(IGenericRepository<Candidate> candidateRepo,
            IGenericRepository<JobToRequest> jobRecRepo,
            UserManager<AppUser> userManager, 
            CareManagerContext context, IMapper mapper)
        {
            _candidateRepo = candidateRepo;
            _jobRecRepo = jobRecRepo;
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }
        [Authorize(Roles = "Client")]
        [HttpGet]
        public async Task<ActionResult> GetListJobToRequest([FromQuery]JobToRequesSpecParams jobToRequestParams)
        {
            //var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return Unauthorized();
            jobToRequestParams.UserId = user.Id;
            var spec = new JobToRequestPaginationSpec(jobToRequestParams);
            var jobs = await _jobRecRepo.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<JobToRequest>, IReadOnlyList<JobToRequestToReturnDto>>(jobs);
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetListJobToRequestById()
        {
            var spec = new JobToRequestSpec();
            var jobs = await _jobRecRepo.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<JobToRequest>, IReadOnlyList<JobToRequestToReturnDto>>(jobs);
            return Ok(data);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] JobToRequest jobToRequest)
        {

            try
            {
                _context.JobToRequests.Add(jobToRequest);
                await _context.SaveChangesAsync();

                var spec = new JobToRequestSpec(jobToRequest.Id);
                var result = await _jobRecRepo.GetEntityWithSpec(spec);

                return Ok(result);
            }
            catch (DbUpdateConcurrencyException)
            {

                return NotFound();
            }

            //return Ok();
        }
    }
}