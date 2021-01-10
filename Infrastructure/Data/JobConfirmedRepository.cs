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
    public class JobConfirmedRepository : IJobConfirmedRepository
    {
        private readonly CareManagerContext _context;

        public JobConfirmedRepository(CareManagerContext context)
        {
            _context = context;
        }
        public async Task<JobConfirmed> CreateJobConfirmedAsync(JobConfirmed jobConfirmed)
        {
            _context.JobConfirmeds.Add(jobConfirmed);
            await _context.SaveChangesAsync();
            var newJobRequest = await GetJobConfirmedByIdAsync(jobConfirmed.Id);
            return newJobRequest;
        }

        public async Task<JobConfirmed> DeleteJobConfirmedAsync(int Id)
        {
            var removeJR = await _context.JobConfirmeds
                    .Include(x => x.TimeDetail)
                    .Include(x => x.ClientLocation)
                    .Include(x => x.AttributeDetail)
                    .Include(x => x.ShiftState)
                    .Include(x => x.JobType)
                    .Include(x => x.PaymentType)
                    .Include(x => x.Agency)
                    .Include(x => x.Grade)
                    .Include(x => x.Aria)
                    .Include(x => x.AppUserCandidate)
                    .SingleOrDefaultAsync (c => c.Id == Id);
            _context.JobConfirmeds.Remove(removeJR);
            await _context.SaveChangesAsync();
            return removeJR;
        }

        public async Task<JobConfirmed> FinalizedJobConfirmedAsync(ConfirmeFinal confirmeFinal)
        {

            var findjobConfirmed = await _context.JobConfirmeds.FindAsync(confirmeFinal.JobConfirmedId);
            findjobConfirmed.Comment = confirmeFinal.Comment;
            findjobConfirmed.Rating = confirmeFinal.Raiting;
            findjobConfirmed.ShiftStateId = 4;
            findjobConfirmed.FinishShift = true;
            findjobConfirmed.LostShift = false;


            _context.Entry(findjobConfirmed).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var findJobRequest = await _context.JobToRequests.FindAsync(confirmeFinal.JobToRequestId);
            findJobRequest.ShiftStateId = 4;
            _context.Entry(findJobRequest).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var newJobRequest = await GetJobConfirmedByIdAsync(confirmeFinal.JobConfirmedId);
            return newJobRequest;
        }

        public async Task<IReadOnlyList<JobConfirmed>> GetJobConfirmedByAgency(int agencyId)
        {

            return await _context.JobConfirmeds.Where(cl => cl.AgencyId == agencyId)
                    .Include(x => x.TimeDetail)
                   .Include(x => x.ClientLocation)
                   .Include(x => x.AttributeDetail)
                   .Include(x => x.ShiftState)
                   .Include(x => x.JobType)
                   .Include(x => x.PaymentType)
                   .Include(x => x.Agency)
                   .Include(x => x.Grade)
                   .Include(x => x.AppUserCandidate)
                   .Include(x => x.AppUserPosted)
                   .Include(x => x.Aria)
                    .ToListAsync();
        }

        public async Task<IReadOnlyList<JobConfirmed>> GetJobConfirmedByJobId(int jobId)
        {
            return await _context.JobConfirmeds
                 .Where(cl => cl.JobToRequestId == jobId)
                 .Include(x => x.TimeDetail)
                 .Include(x => x.ClientLocation)
                 .Include(x => x.AttributeDetail)
                 .Include(x => x.ShiftState)
                 .Include(x => x.JobType)
                 .Include(x => x.PaymentType)
                 .Include(x => x.Agency)
                 .Include(x => x.Grade)
                 .Include(x => x.AppUserCandidate)
                 .Include(x => x.AppUserPosted)
                 .Include(x => x.Candidate)
                 .Include(x => x.Aria)
                 .ToListAsync();
        }

        public async Task<IReadOnlyList<JobConfirmed>> GetJobConfirmedByUser(string userId)
        {
            return await _context.JobConfirmeds
               .Where(cl => cl.AppUserPostedId == userId)
               .Include(x => x.TimeDetail)
               .Include(x => x.ClientLocation)
               .Include(x => x.AttributeDetail)
               .Include(x => x.ShiftState)
               .Include(x => x.JobType)
               .Include(x => x.PaymentType)
               .Include(x => x.Agency)
               .Include(x => x.Grade)
               .Include(x => x.AppUserCandidate)
               .Include(x => x.AppUserPosted)
               .Include(x => x.Aria)
               .ToListAsync();
        }

        public async Task<JobConfirmed> UpdateJobConfirmedAsync(JobConfirmed jobConfirmed)
        {
            _context.Entry(jobConfirmed).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var newJobRequest = await GetJobConfirmedByIdAsync(jobConfirmed.Id);
            return newJobRequest;
        }
        private async Task<JobConfirmed> GetJobConfirmedByIdAsync(int Id)
        {
            var findRequest = await _context.JobConfirmeds
                   .Include(x => x.TimeDetail)
                   .Include(x => x.ClientLocation)
                   .Include(x => x.AttributeDetail)
                   .Include(x => x.ShiftState)
                   .Include(x => x.JobType)
                   .Include(x => x.PaymentType)
                   .Include(x => x.Agency)
                   .Include(x => x.Grade)
                   .Include(x => x.AppUserCandidate)
                   .Include(x => x.AppUserPosted)
                   .Include(x => x.Aria)
                    .FirstOrDefaultAsync(cl => cl.Id == Id);
            return findRequest;
        }
    }
}
