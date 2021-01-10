using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAgencyPhotoRepository
    {
        Task<AgencyPhoto> GetPhoto(int id);
        Task<IReadOnlyList<AgencyPhoto>> GetPhotos(int id);
        Task<AgencyPhoto> CreatePhotoAsync(AgencyPhoto agencyPhoto);
        Task<AgencyPhoto> DeletePhotoAsync(int Id);
        Task<AgencyPhoto> UpdatePhotoAsync(AgencyPhoto agencyPhoto);
        Task<Agency> GetAgencyByUserIdAsync(string userId);
        Task<Agency> GetAgencyByIdAsync(int Id);
        Task<AgencyPhoto> GetMainPhotoForUserAsync(int Id);
        Task<AppUser> UpdateUserPhotoAsync(AppUser appUser);
    }
}
