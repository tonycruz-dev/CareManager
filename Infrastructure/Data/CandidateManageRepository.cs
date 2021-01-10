using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class CandidateManageRepository : ICandidateManageRepository
    {
        private readonly CareManagerContext _context;

        public CandidateManageRepository(CareManagerContext context)
        {
            _context = context;
        }

        public async Task<InvitedCandidate> AcceptInvitesAsync(int inviteId)
        {
            var currentDate = DateTime.Now;
            var currentTime = currentDate.ToString("HH:mm");
            var inviteCandidate = await _context
                .InvitedCandidates
                .Include(icl => icl.ClientLocation)
                .Include(g => g.Grade)
                .Include(c => c.Candidate)
                .Include(p => p.PaymentType)
                .SingleOrDefaultAsync( ic => ic.Id == inviteId);

            inviteCandidate.Accept = true;
            inviteCandidate.Reject = false;
            inviteCandidate.ResponseDate = currentDate;
            inviteCandidate.RespondTime = currentTime;
            _context.Entry(inviteCandidate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var candidateJob = await _context.JobToRequests.FindAsync(inviteCandidate.JobToRequestId);
            if (candidateJob.NumberApplied >= candidateJob.NumberCandidate)
            {
               
                return inviteCandidate;
            }
            else
            {
               
                var numApp = candidateJob.NumberApplied;
                var numOfCand = candidateJob.NumberCandidate - 1;
                if (numApp == numOfCand)
                {
                    candidateJob.ShiftStateId = 3;
                }
                var jobConfirmed = new JobConfirmed
                {
                    GradeDetails = inviteCandidate.Grade.GradeName,
                    PhotoUrl = inviteCandidate.Candidate.PhotoUrl,
                    Message = inviteCandidate.Message,
                    JobDate = inviteCandidate.JobDate,
                    StartTime = inviteCandidate.StartTime,
                    EndTime = inviteCandidate.EndTime,
                    JobAddress = inviteCandidate.JobAddress,
                    PaymentDescription = inviteCandidate.PaymentType.Name,
                    Comment = inviteCandidate.Message,
                    Rating = 0,
                    AttributeDetailId = inviteCandidate.AttributeDetailId,
                    TimeDetailId = inviteCandidate.TimeDetailId,
                    PaymentTypeId = inviteCandidate.PaymentTypeId,
                    CandidateId = inviteCandidate.CandidateId,
                    AgencyId = inviteCandidate.AgencyId,
                    AriaId = inviteCandidate.AriaId,
                    JobTypeId = inviteCandidate.JobTypeId,
                    JobToRequestId = inviteCandidate.JobToRequestId,
                    GradeId = inviteCandidate.GradeId,
                    ClientLocationId = inviteCandidate.ClientLocationId,
                    ShiftStateId = 3,
                    AppUserPostedId = inviteCandidate.AppUserPostedId,
                    AppUserCandidateId = inviteCandidate.AppUserCandidateId
                };
                _context.JobConfirmeds.Add(jobConfirmed);
                candidateJob.NumberApplied = candidateJob.NumberApplied + 1;
                _context.Entry(inviteCandidate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return inviteCandidate;
            }
        }

        public async Task<Candidate> CreateCandidateAsync(Candidate candidate)
        {
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();
            return candidate;
        }

        public async Task<Candidate> DeleteCandidateAsync(int Id)
        {
            var deleteItem = await _context.Candidates.FirstOrDefaultAsync(c => c.Id == Id);
            _context.Candidates.Remove(deleteItem);
            await _context.SaveChangesAsync();
            return deleteItem;
        }

        public async Task<Candidate> GetCandidateAsync(int id)
        {
            return await _context.Candidates.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Candidate> GetCandidateByUserIdAsync(string userId)
        {
            return await _context.Candidates.SingleOrDefaultAsync(c => c.AppUserId == userId);
        }

        public async Task<IReadOnlyList<Candidate>> GetCandidatesAsync()
        {
            return await _context.Candidates.Include(g => g.Grade).ToListAsync();
        }

        public async Task<List<InvitedCandidate>> GetInvitesAsync(int id)
        {
            return await _context.InvitedCandidates
                .Where(inv => inv.CandidateId == id)
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
                .OrderByDescending(o => o.Id).ToListAsync();
        }

        public async Task<List<JobConfirmed>> GetJobConformedAsync(int id)
        {
            return await _context.JobConfirmeds
                .Where(inv => inv.CandidateId == id && inv.ShiftStateId == 3)
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
                .OrderByDescending(o => o.JobDate).ToListAsync();
        }

        public async Task<List<JobConfirmed>> GetJobFinishAsync(int id)
        {
            return await _context.JobConfirmeds
                 .Where(inv => inv.CandidateId == id && inv.ShiftStateId == 4)
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
                 .OrderByDescending(o => o.JobDate).ToListAsync();
        }

        public async Task<IReadOnlyList<Candidate>> GetTopCandidatesAsync(int records)
        {
            return await _context.Candidates.Include(g => g.Grade).Take(records).ToListAsync();
        }

        public async Task<InvitedCandidate> RejectInvitesAsync(int inviteId)
        {
            var currentDate = DateTime.Now;
            var currentTime = currentDate.ToString("HH:mm");
            var jobToAcept = await _context.InvitedCandidates.FindAsync(inviteId);
            jobToAcept.Accept = false;
            jobToAcept.Reject = true;
            jobToAcept.ResponseDate = currentDate;
            jobToAcept.RespondTime = currentTime;
            await _context.SaveChangesAsync(); ;
            return jobToAcept;
        }

        public async Task<Candidate> UpdateCandidateAsync(Candidate candidate)
        {
            _context.Entry(candidate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return candidate;
        }
    }
}
