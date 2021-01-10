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
    public class RepositoryHelper : IRepositoryHelper
    {
        private readonly CareManagerContext _context;

        public RepositoryHelper(CareManagerContext context)
        {
            _context = context;
        }
        public async Task<Agency> GetAgencyByUserIdAsync(string userId)
        {
            return await _context.Agencies.FirstOrDefaultAsync(au => au.AppUserId == userId);
        }

        public async Task<AgencyJobStatus> GetAgencyJobStatesAsync(int Id)
        {
            var agencyJobStates = new AgencyJobStatus();
            agencyJobStates.Pending = await _context.JobToRequests.Where(jr => jr.ShiftStateId == 1 && jr.AgencyId == Id).CountAsync();
            agencyJobStates.InProgress = await _context.JobToRequests.Where(jr => jr.ShiftStateId == 2 && jr.AgencyId == Id).CountAsync();
            agencyJobStates.Booked = await _context.JobToRequests.Where(jr => jr.ShiftStateId == 3 && jr.AgencyId == Id).CountAsync();
            agencyJobStates.Finish = await _context.JobToRequests.Where(jr => jr.ShiftStateId == 4 && jr.AgencyId == Id).CountAsync();
            agencyJobStates.Canceled = await _context.JobToRequests.Where(jr => jr.ShiftStateId == 5 && jr.AgencyId == Id).CountAsync();
            
            return agencyJobStates;
        }

        public async Task<AgencyListLoop> GetAgencyLoop(int agencyId)
        {
            var loopResults = new AgencyListLoop
            {
                ClientLocations = await _context.ClientLocations.Where(cl => cl.AgencyId == agencyId).ToListAsync(),
                Arias = await _context.Arias.ToListAsync(),
                PaymentTypes = await _context.PaymentTypes.ToListAsync(),
                JobTypes = await _context.JobTypes.ToListAsync(),
                Grades = await _context.Grades.ToListAsync(),
                AttributeDetails = await _context.AttributeDetails.ToListAsync(),
                TimeDetails = await _context.TimeDetails.ToListAsync()
            };

            return loopResults;
        }

        public async Task<IReadOnlyList<CandidateBooked>> GetCandidateBookedAsync(int jobRequestId)
        {
            //return await _context.JobConfirmeds
            //    .Include(c => c.Candidate)
            //    .Where(ic => ic.JobToRequestId == jobRequestId)
            //    .Select(cn => cn.Candidate)
            //    .ToListAsync();
            return await _context.JobConfirmeds
                .Include(c => c.Candidate)
                .Where(ic => ic.JobToRequestId == jobRequestId)
                .Select(cn => new CandidateBooked
                {
                    Id = cn.Candidate.Id,
                    FirstName = cn.Candidate.FirstName,
                    LastName = cn.Candidate.LastName,
                    Address1 = cn.Candidate.Address1,
                    Address2 = cn.Candidate.Address2,
                    Address3 = cn.Candidate.Address3,
                    Address4 = cn.Candidate.Address4,
                    Address5 = cn.Candidate.Address5,
                    ContactNumber = cn.Candidate.ContactNumber,
                    Email = cn.Candidate.Email,
                    AccoutNumber = cn.Candidate.AccoutNumber,
                    AccoutName = cn.Candidate.AccoutName,
                    SortCode = cn.Candidate.SortCode,
                    PhotoUrl = cn.Candidate.PhotoUrl,
                    AppUserId = cn.Candidate.AppUserId,
                    GradeId = cn.Candidate.GradeId,
                    JobToRequestId = cn.JobToRequestId,
                    JobConfirmedId = cn.Id,
                    Raiting = cn.Rating,
                    FinishShift = cn.FinishShift ?? default,
                    LostShift = cn.LostShift ?? default,
                    Comment= cn.Comment
                })
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Candidate>> GetCandidateForInviteAsync(int gradeId)
        {
            return await _context.Candidates
                .Include(g => g.Grade)
                .Where(cg => cg.GradeId == gradeId).ToListAsync();
        }

        public async Task<IReadOnlyList<Candidate>> GetCandidateInProgressAsync(int jobRequestId)
        {
            return await _context.InvitedCandidates
                .Include(c => c.Candidate)
                .Where(ic => ic.JobToRequestId == jobRequestId)
                .Select(cn => cn.Candidate)
                .ToListAsync();
            

        }

        public async Task<IReadOnlyList<Candidate>> GetCandidateInvitedAsync(int jobRequestId)
        {
            return await _context.InvitedCandidates
               .Include(c => c.Candidate)
               .Where(ic => ic.JobToRequestId == jobRequestId)
               .Select(cn => cn.Candidate)
               .ToListAsync();
        }

        public async Task<IReadOnlyList<CandidateResponded>> GetCandidateRespondedAsync(int jobRequestId)
        {
            
            return await _context.InvitedCandidates
                .Include(c => c.Candidate)
                .Where(ic => ic.JobToRequestId == jobRequestId && ic.Accept != null)
                .Select(cn =>  new CandidateResponded 
                    {  Id = cn.Candidate.Id,
                       FirstName = cn.Candidate.FirstName,
                       LastName = cn.Candidate.LastName,
                       Address1 = cn.Candidate.Address1,
                       Address2 = cn.Candidate.Address2,
                       Address3 = cn.Candidate.Address3,
                       Address4 = cn.Candidate.Address4,
                       Address5 = cn.Candidate.Address5,
                       ContactNumber = cn.Candidate.ContactNumber,
                       Email = cn.Candidate.Email,
                       AccoutNumber = cn.Candidate.AccoutNumber,
                       AccoutName = cn.Candidate.AccoutName,
                       SortCode = cn.Candidate.SortCode,
                       PhotoUrl = cn.Candidate.PhotoUrl,
                       AppUserId = cn.Candidate.AppUserId,
                       GradeId = cn.Candidate.GradeId,
                       Accept = cn.Accept,
                       Reject = cn.Reject
                })
                .ToListAsync();
        }

        public async Task<HRDashboard> GetHRDashboard()
        {
            var dashboard = new HRDashboard
            {
                TotalJobRequest = await _context.JobToRequests.CountAsync(),
                TotalCandidates = await _context.Candidates.CountAsync(),
                TotalJobConfirmed = await _context.JobToRequests.Where(jc => jc.ShiftStateId == 3).CountAsync(),
                TotalInvitesCandidates = await _context.InvitedCandidates.CountAsync(),
                Agencies = await _context.Agencies.Take(5).ToListAsync(),
                Candidates = await _context.Candidates.Include(g => g.Grade).Take(5).ToListAsync()
            };

            return dashboard;
        }

        public async Task<TimeDetail> GetTimeDetailAsync(int Id)
        {
            return await _context.TimeDetails.FirstOrDefaultAsync(td => td.Id == Id);
        }
        public async Task<IReadOnlyList<TimeDetail>> GetTimeDetailsAsync()
        {
            return await _context.TimeDetails.ToListAsync();
        }

        public async Task<AppUser> UpdateUserPhotoAsync(AppUser appUser)
        {
            _context.Entry(appUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
             return appUser;
        }
    }
}
