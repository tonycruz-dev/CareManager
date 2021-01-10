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
    public class CandidatePhotoRepository : ICandidatePhotoRepository
    {
        private readonly CareManagerContext _context;

        public CandidatePhotoRepository(CareManagerContext context)
        {
            _context = context;
        }
        public async Task<CandidatePhoto> CreatePhotoAsync(CandidatePhoto candidatePhoto)
        {
            _context.CandidatePhotos.Add(candidatePhoto);
            await _context.SaveChangesAsync();
            return candidatePhoto;
        }

        public async Task<CandidatePhoto> DeletePhotoAsync(int Id)
        {
            var cpd = await _context.CandidatePhotos.FirstOrDefaultAsync(cp => cp.Id == Id);
            _context.CandidatePhotos.Remove(cpd);
            await _context.SaveChangesAsync();
            return cpd;
        }

        public async Task<Candidate> GetCandidateByIdAsync(int Id)
        {
            return await _context.Candidates.Include(ph => ph.CandidatePhotos)
                .SingleOrDefaultAsync(cp => cp.Id == Id);
        }

        public async Task<Candidate> GetCandidateByUserIdAsync(string userId)
        {
            var candidate = await _context.Candidates.Include(ci => ci.CandidatePhotos).Where(c => c.AppUserId == userId).SingleOrDefaultAsync();
            return candidate;
        }

        public Task<CandidatePhoto> GetMainPhotoForUser(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<CandidatePhoto> GetMainPhotoForUserAsync(int Id)
        {
            return await _context.CandidatePhotos
                .SingleOrDefaultAsync(cp => cp.CandidateId == Id && cp.IsMain == true);
        }

        public async Task<CandidatePhoto> GetPhoto(int id)
        {
            return await _context.CandidatePhotos.FirstOrDefaultAsync(cu => cu.Id == id);
        }

        public async Task<IReadOnlyList<CandidatePhoto>> GetPhotos(int id)
        {
            return await _context.CandidatePhotos.Where(cp => cp.CandidateId == id).ToListAsync() ;
        }

        public async Task<AppUser> UpdateHRUserPhotoAsync(int Id, string url)
        {
            //_context.Entry(appUser).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
            var candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.Id == Id);
            candidate.PhotoUrl = url;
            _context.Entry(candidate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == candidate.AppUserId);
            user.Avatar = url;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<CandidatePhoto> UpdatePhotoAsync(CandidatePhoto candidatePhoto)
        {
            _context.Entry(candidatePhoto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return candidatePhoto;
        }
        public async Task<AppUser> UpdateUserPhotoAsync(AppUser appUser)
        {
            _context.Entry(appUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.AppUserId == appUser.Id);
            candidate.PhotoUrl = appUser.Avatar;
            _context.Entry(candidate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return appUser;
        }
    }
}
