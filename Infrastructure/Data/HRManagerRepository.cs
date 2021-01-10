using Core.Entities;
using Core.EntityHelpers;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class HRManagerRepository : IHRManagerRepository
    {
        private readonly CareManagerContext _context;
        public HRManagerRepository(CareManagerContext context)
        {
            _context = context;
        }
        public  void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public async Task<JobToRequest> CreateJobToRequestAsync(JobToRequest jobToRequest)
        {
            _context.JobToRequests.Add(jobToRequest);
            await _context.SaveChangesAsync();
            var newJobRequest = await GetJobToRequestByIdAsync(jobToRequest.Id);
            return newJobRequest;
        }

        public  void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<JobToRequest> DeleteJobToRequestAsync(int Id)
        {
            var removeJR = _context.JobToRequests
                .SingleOrDefault(c => c.Id == Id);
            _context.JobToRequests.Remove(removeJR);
            await _context.SaveChangesAsync();
            return removeJR;
        }

        public async Task<PagedList<JobToRequest>> GetJobToRequestByAgency(int agencyId, JobRequestParams jobRequestParams)
        {
            var jobRequest = _context.JobToRequests
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
                      .OrderByDescending(u => u.JobDateStart).AsQueryable();


            jobRequest = jobRequest.Where(u => u.AgencyId == agencyId);





            if (!string.IsNullOrEmpty(jobRequestParams.Search))
            {
                jobRequest = jobRequest
                   .Where(jr => jr.ClientLocation.Address1.Contains(jobRequestParams.Search) ||
                   jr.ShiftState.ShiftDetails.ToLower().Contains(jobRequestParams.Search));
            }

            if (!string.IsNullOrEmpty(jobRequestParams.DateFrom))
            {
                //var f = jobRequestParams.DateFrom;
                //var dateFromRemoveLastCarecter = jobRequestParams.DateFrom.Substring(0, jobRequestParams.DateFrom.Length - 3);
                //var dateFromRemoveFirstCarecters = dateFromRemoveLastCarecter.Remove(0, 1);
                //DateTime Dfrom = DateTime.Parse(dateFromRemoveFirstCarecters);
                var f = jobRequestParams.DateFrom;
                //var dateFromRemoveLastCarecter = jobRequestParams.DateFrom.Substring(0, jobRequestParams.DateFrom.Length - 3);
                //var dateFromRemoveFirstCarecters = dateFromRemoveLastCarecter.Remove(0, 1);
                DateTime Dfrom = DateTime.Parse(jobRequestParams.DateFrom);

                //var dateToRemoveLastCarecter = jobRequestParams.DateTo.Substring(0, jobRequestParams.DateTo.Length - 3);
                //var dateToRemoveFirstCarecters = dateToRemoveLastCarecter.Remove(0, 1);

                DateTime Dto = DateTime.Parse(jobRequestParams.DateTo);

                jobRequest = jobRequest
                   .Where(
                    jr => jr.JobDateStart >= Dfrom &&
                    jr.JobDateStart <= Dto);
                Console.WriteLine("it workes");
            }


            if (!string.IsNullOrEmpty(jobRequestParams.OrderBy))
            {
                switch (jobRequestParams.OrderBy)
                {
                    case "created":
                        jobRequest = jobRequest.OrderByDescending(u => u.JobDateStart);
                        break;
                    default:
                        jobRequest = jobRequest.OrderByDescending(u => u.JobDateStart);
                        break;
                }
            }
           
           return await PagedList<JobToRequest>.CreateAsync(jobRequest, jobRequestParams.PageNumber, jobRequestParams.PageSize);
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

        public async Task<PagedList<JobToRequest>> GetJobToRequestByUser(string userId, JobRequestParams jobRequestParams)
        {
            var jobRequest = _context.JobToRequests
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
                .OrderByDescending(u => u.JobDateStart).AsQueryable();

            jobRequest = jobRequest.Where(u => u.AppUserId == userId);



           

            if (!string.IsNullOrEmpty(jobRequestParams.Search))
            {
               jobRequest = jobRequest
                    .Where(jr => jr.ClientLocation.Address1.Contains(jobRequestParams.Search) || 
                    jr.ShiftState.ShiftDetails.ToLower().Contains(jobRequestParams.Search));
            }

          

            if (!string.IsNullOrEmpty(jobRequestParams.OrderBy))
            {
                switch (jobRequestParams.OrderBy)
                {
                    case "created":
                        jobRequest = jobRequest.OrderByDescending(u => u.JobDateStart);
                        break;
                    default:
                        jobRequest = jobRequest.OrderByDescending(u => u.JobDateStart);
                        break;
                }
            }
            jobRequest.Include(x => x.TimeDetail)
            .Include(x => x.ClientLocation)
            .Include(x => x.AttributeDetail)
            .Include(x => x.ShiftState)
            .Include(x => x.JobType)
            .Include(x => x.PaymentType)
            .Include(x => x.Agency)
            .Include(x => x.Grade)
            .Include(x => x.Aria)
            .Include(x => x.AppUser);

            return await PagedList<JobToRequest>.CreateAsync(jobRequest, jobRequestParams.PageNumber, jobRequestParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<JobToRequest> UpdateJobToRequestAsync(JobToRequest jobToRequest)
        {
            var updateJR = await _context.JobToRequests.FirstOrDefaultAsync(cl => cl.Id == jobToRequest.Id);
            updateJR = jobToRequest;
            await _context.SaveChangesAsync();
            var newJobRequest = await GetJobToRequestByIdAsync(updateJR.Id);
            return newJobRequest;
        }
    }
}
