using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class JobRequestRepository : IJobRequestRepository
    {
        private readonly CareManagerContext _context;

        public JobRequestRepository(CareManagerContext context)
        {
            _context = context;
        }
        public async Task<JobToRequest> CreateJobToRequestAsync(JobToRequest jobToRequest)
        {
            //jobToRequest.
            _context.JobToRequests.Add(jobToRequest);
            await _context.SaveChangesAsync();
            var newJobRequest = await GetJobToRequestByIdAsync(jobToRequest.Id);
            return newJobRequest;
        }

        public async Task<JobToRequest> DeleteJobToRequestAsync(int Id)
        {
            var removeJR = _context.JobToRequests
                    .Include(x => x.TimeDetail)
                    .Include(x => x.ClientLocation)
                    .Include(x => x.AttributeDetail)
                    .Include(x => x.ShiftState)
                    .Include(x => x.JobType)
                    .Include(x => x.PaymentType)
                    .Include(x => x.Agency)
                    .Include(x => x.Grade)
                    .Include(x => x.Aria)
                    .Include(x => x.AppUser)
                .SingleOrDefault(c => c.Id == Id);
            _context.JobToRequests.Remove(removeJR);
            await _context.SaveChangesAsync();
            return removeJR;
        }

        public async Task<IReadOnlyList<JobToRequest>> GetJobToRequestByAgency(int agencyId)
        {
            return await _context.JobToRequests.Where(cl => cl.AgencyId == agencyId)
                    .Include(x => x.TimeDetail)
                    .Include(x => x.ClientLocation)
                    .Include(x => x.AttributeDetail)
                    .Include(x => x.ShiftState)
                    .Include(x => x.JobType)
                    .Include(x => x.PaymentType)
                    .Include(x => x.Agency)
                    .Include(x => x.Grade)
                    .Include(x => x.Aria)
                    .Include(x => x.AppUser)
                    .ToListAsync();
        }

        public async Task<IReadOnlyList<JobToRequest>> GetJobToRequestByUser(string userId)
        {
            return await _context.JobToRequests.Where(cl => cl.AppUserId == userId)
                    .Include(x => x.TimeDetail)
                    .Include(x => x.ClientLocation)
                    .Include(x => x.AttributeDetail)
                    .Include(x => x.ShiftState)
                    .Include(x => x.JobType)
                    .Include(x => x.PaymentType)
                    .Include(x => x.Agency)
                    .Include(x => x.Grade)
                    .Include(x => x.Aria)
                    .Include(x => x.AppUser)
                .ToListAsync();
        }

        public async Task<JobToRequest> UpdateJobToRequestAsync(JobToRequest jobToRequest)
        {
            var updateJR = await _context.JobToRequests.FirstOrDefaultAsync(cl => cl.Id == jobToRequest.Id);
            updateJR = jobToRequest;
            await _context.SaveChangesAsync();
            var newJobRequest = await GetJobToRequestByIdAsync(updateJR.Id);
            return newJobRequest;

        }
        public async Task<JobToRequest> GetJobToRequestByIdAsync(int Id)
        {
            var findRequest = await _context.JobToRequests
                     .Include(x => x.TimeDetail)
                    .Include(x => x.ClientLocation)
                    .Include(x => x.AttributeDetail)
                    .Include(x => x.ShiftState)
                    .Include(x => x.JobType)
                    .Include(x => x.PaymentType)
                    .Include(x => x.Agency)
                    .Include(x => x.Grade)
                    .Include(x => x.Aria)
                    .Include(x => x.AppUser)
                    .FirstOrDefaultAsync(cl => cl.Id == Id);
            return findRequest;
        }
    }
}
