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
    public class AgencyPhotoRepository: IAgencyPhotoRepository
    {
        private readonly CareManagerContext _context;

        public AgencyPhotoRepository(CareManagerContext context)
        {
            _context = context;
        }

        public async Task<AgencyPhoto> CreatePhotoAsync(AgencyPhoto agencyPhoto)
        {
            _context.AgencyPhotos.Add(agencyPhoto);
            await _context.SaveChangesAsync();
            return agencyPhoto;
        }

        public async Task<AgencyPhoto> DeletePhotoAsync(int Id)
        {
            var cpd = await _context.AgencyPhotos.FirstOrDefaultAsync(cp => cp.Id == Id);
            _context.AgencyPhotos.Remove(cpd);
            await _context.SaveChangesAsync();
            return cpd;
        }

        public async Task<Agency> GetAgencyByIdAsync(int Id)
        {
            var candidate = await _context.Agencies.FirstOrDefaultAsync(c => c.Id == Id);
            return candidate;
        }

        public async Task<Agency> GetAgencyByUserIdAsync(string UserId)
        {
            var candidate = await _context.Agencies.Include(ci => ci.AgencyPhotos).Where(c => c.AppUserId == UserId).SingleOrDefaultAsync();
            return candidate;
        }

        public async Task<AgencyPhoto> GetMainPhotoForUserAsync(int Id)
        {
            return await _context.AgencyPhotos
                .Where(cp => cp.AgencyId == Id)
                .FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<AgencyPhoto> GetPhoto(int id)
        {
            return await _context.AgencyPhotos.FirstOrDefaultAsync(cu => cu.Id == id);
        }

        public async Task<IReadOnlyList<AgencyPhoto>> GetPhotos(int id)
        {
            return await _context.AgencyPhotos.Where(cp => cp.AgencyId == id).ToListAsync();
        }

        public async Task<AgencyPhoto> UpdatePhotoAsync(AgencyPhoto agencyPhoto)
        {
            _context.Entry(agencyPhoto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return agencyPhoto;
        }
        public async Task<AppUser> UpdateUserPhotoAsync(AppUser appUser)
        {
            _context.Entry(appUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var agency = await _context.Agencies.FirstOrDefaultAsync(ag => ag.AppUserId == appUser.Id);
            if (agency != null)
            {
                agency.LogoUrl = appUser.Avatar;
            }
            _context.Entry(agency).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return appUser;
        }
    }
}
