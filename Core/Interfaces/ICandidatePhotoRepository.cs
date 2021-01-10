using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICandidatePhotoRepository
    {
        Task<CandidatePhoto> GetPhoto(int id);
        Task<IReadOnlyList<CandidatePhoto>> GetPhotos(int id);
        Task<CandidatePhoto> GetMainPhotoForUser(string userId);
        Task<CandidatePhoto> CreatePhotoAsync(CandidatePhoto invitedCandidates);
        Task<CandidatePhoto> DeletePhotoAsync(int Id);
        Task<CandidatePhoto> UpdatePhotoAsync(CandidatePhoto invitedCandidates);
        Task<Candidate> GetCandidateByUserIdAsync(string userId);
        Task<Candidate> GetCandidateByIdAsync(int Id);
        Task<CandidatePhoto> GetMainPhotoForUserAsync(int Id);
        Task<AppUser> UpdateUserPhotoAsync(AppUser appUser);
        Task<AppUser> UpdateHRUserPhotoAsync(int Id, string url);

    }
}
