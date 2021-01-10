using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Infrastructure.Data
{
    public class InvitedCandidateRepository : IInvitedCandidateRepository
    {
        private readonly CareManagerContext _context;

        public InvitedCandidateRepository(CareManagerContext context)
        {
            _context = context;
        }
        public async Task<InvitedCandidate> CreateInvitedCandidateAsync(InvitedCandidate invitedCandidates)
        {
            _context.InvitedCandidates.Add(invitedCandidates);
            await _context.SaveChangesAsync();
            var newJobRequest = await GetInvitedCandidateByIdAsync(invitedCandidates.Id);
            return newJobRequest;

        }

        public async Task<InvitedCandidate> DeleteInvitedCandidateAsync(int Id)
        {
            var removeItem = _context.InvitedCandidates
                .Include(x => x.TimeDetail)
                .Include(x => x.ClientLocation)
                .Include(x => x.AttributeDetail)
                .Include(x => x.ShiftState)
                .Include(x => x.JobType)
                .Include(x => x.PaymentType)
                .Include(x => x.Agency)
                .Include(x => x.Grade)
                .Include(x => x.AppUserCandidate)
                .SingleOrDefault(c => c.Id == Id);
            _context.InvitedCandidates.Remove(removeItem);
            await _context.SaveChangesAsync();
            return removeItem;
        }

        public Task<IReadOnlyList<InvitedCandidate>> GetCandidatesInprogress(int jobToRequestId)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<InvitedCandidate>> GetInvitedCandidateByAgency(int agencyId)
        {
            return await _context.InvitedCandidates
                .Where(cl => cl.AgencyId == agencyId)
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

        public async Task<IReadOnlyList<InvitedCandidate>> GetInvitedCandidateByJobId(int jobId)
        {
            return await _context.InvitedCandidates
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

        public async Task<IReadOnlyList<InvitedCandidate>> GetInvitedCandidateByUser(string userId)
        {
            return await _context.InvitedCandidates
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

        public async Task<IReadOnlyList<InvitedCandidate>> InsertInvitedCandidateAsync(int[] candidateId, int jobToRequestId)
        {
            var jobToRequest = await _context.JobToRequests.SingleOrDefaultAsync(c => c.Id == jobToRequestId);
            var jobInvite = new List<InvitedCandidate>();
            foreach (var id in candidateId)
            {
                var candi = await _context.Candidates.SingleOrDefaultAsync(c => c.Id == id);
                var location = await _context.ClientLocations.SingleOrDefaultAsync(c => c.Id == jobToRequest.ClientLocationId);

                var message = $"Please Contact, {location.ManagerName}! at this address {location.Address1}";
                var addressMessage = $"Address:, {location.Address1}, {location.Address2}, {location.Address3}, {location.Address4}, {location.Address5}";
                var currentDate = DateTime.Now;
                var currentTime = currentDate.ToString("HH:mm");

                var iv = new InvitedCandidate
                {
                    Message = message,
                    PublishDate = currentDate,
                    PublishTime = currentTime,
                    JobDate = jobToRequest.JobDateStart,
                    StartTime = jobToRequest.StartTime,
                    EndTime = jobToRequest.EndTime,
                    JobAddress = addressMessage,
                    TimeDetailId = jobToRequest.TimeDetailId,
                    PaymentTypeId = jobToRequest.PaymentTypeId,
                    CandidateId = candi.Id,
                    AgencyId = jobToRequest.AgencyId,
                    JobToRequestId = jobToRequest.Id,
                    GradeId = jobToRequest.GradeId,
                    ClientLocationId = jobToRequest.ClientLocationId,
                    ShiftStateId = jobToRequest.ShiftStateId,
                    AppUserPostedId = jobToRequest.AppUserId,
                    AppUserCandidateId = candi.AppUserId,
                    AriaId = jobToRequest.AriaId,
                    AttributeDetailId = jobToRequest.AttributeDetailId,
                    JobTypeId = jobToRequest.JobTypeId
                };
                _context.InvitedCandidates.Add(iv);
                jobToRequest.ShiftStateId = 2;
                await _context.SaveChangesAsync();
                jobInvite.Add(iv);

            }

            return jobInvite;
        }

        public async Task<InvitedCandidate> UpdateInvitedCandidateAsync(InvitedCandidate invitedCandidates)
        {
            _context.Entry(invitedCandidates).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var newJobRequest = await GetInvitedCandidateByIdAsync(invitedCandidates.Id);
            return newJobRequest;
        }
        private async Task<InvitedCandidate> GetInvitedCandidateByIdAsync(int Id)
        {
            var findRequest = await _context.InvitedCandidates
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
